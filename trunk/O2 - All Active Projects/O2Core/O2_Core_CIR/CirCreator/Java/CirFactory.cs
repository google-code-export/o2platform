// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Cmd.SpringMvc.Xsd;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.Interfaces.CIR;

namespace O2.Core.CIR.CirCreator.Java
{
    public class CirFactory
    {
        public static ICirData createCirDataFromXmlFileWithJavaMetadata(string xmlFileWithJavaMetadata)
        {
            var cirData = new CirData();
            try
            {
                DI.log.info("createCirDataFromXmlFileWithJavaMetadata for {0}", xmlFileWithJavaMetadata);
                var javaMetadata =
                    (JavaAttributeMappings)
                    Serialize.getDeSerializedObjectFromXmlFile(xmlFileWithJavaMetadata, typeof (JavaAttributeMappings));
                if (javaMetadata == null || javaMetadata.@class == null)
                {
                    DI.log.error(
                        "in createCirDataFromXmlFileWithJavaMetadata, could not convert file into JavaMetadata XSD-drive .Net class: {0}",
                        xmlFileWithJavaMetadata);
                    return null;
                }

                foreach (var _class in javaMetadata.@class)
                {
                    var cirClass = (CirClass) cirData.addClass(_class.name);
                    if (cirClass != null)
                    {
                        cirClass.bClassHasMethodsWithControlFlowGraphs = true;
                        cirClass.File = _class.sourceFile;
                        cirClass.FileLine = "0";                        

                        // map superclasses and Interfaces (which in the current version of the the CirData are all stored on the same location
                        if (_class.superclass != null)
                            foreach (var superClass in _class.superclass)
                                //CirDataUtils.addSuperClassMapping(cirData, cirClass,superClass.name);
                                cirClass.dSuperClasses.Add(superClass.name, null);//cirData.getClass(superClass.name));
                        if (_class.@interface != null)
                            foreach (var @interface in _class.@interface)
                                //CirDataUtils.addSuperClassMapping(cirData,cirClass, @interface.name);
                                cirClass.dSuperClasses.Add(@interface.name, null); //cirData.getClass(@interface.name));
                        if (_class.method != null)
                            foreach (var _method in _class.method)
                            {
                                var functionSignature = createSignatureFromMethodData(_class.name, _method.name, _method.descriptor);
                                var cirFunction = (CirFunction)cirClass.addFunction(functionSignature);
                                cirFunction.File = _class.sourceFile;
                                cirFunction.FileLine = _method.lineNumber.ToString();
                                if (_method.methodCalled != null)
                                {
                                    cirFunction.HasControlFlowGraph = true;
                                    foreach (var _calledMethod in _method.methodCalled)
                                    {
                                        var calledFunctionSignature = createSignatureFromMethodData(_calledMethod.@class,
                                                                                                    _calledMethod.name,
                                                                                                    _calledMethod.descriptor);
                                        cirFunction.addCalledFunction(calledFunctionSignature, cirFunction.File, _calledMethod.lineNumber);
                                    }
                                }
                            }                        
                    }
                }
                //cirData.remapXRefs();
            }
            catch (Exception ex)
            {
                DI.log.error("in createCirDataFromXmlFileWithJavaMetadata:{0}",ex.Message);
            }
            return cirData;
        }

        private static String createSignatureFromMethodData(String methodClass, string methodName, string methodDescriptor)
        {

            return string.Format("{0}.{1}{2}", methodClass, decodeMethodString(methodName), decodeMethodString(methodDescriptor));
        }

        private static string decodeMethodString(string stringToDecode)
        {            
            stringToDecode = stringToDecode.Replace('/', '.');
            stringToDecode = stringToDecode.Replace("&lt;", "<");
            stringToDecode = stringToDecode.Replace("&gt;", ">");
            stringToDecode = stringToDecode.Replace(")V", ")void");
            stringToDecode = stringToDecode.Replace(")I",")int");            
            stringToDecode = stringToDecode.Replace("(L", "(");
            stringToDecode = stringToDecode.Replace(")L", ")");
            stringToDecode = stringToDecode.Replace("(IL", "(int;");
            stringToDecode = stringToDecode.Replace(";IL", ";int;");
            stringToDecode = stringToDecode.Replace(";I", ";int");
            stringToDecode = stringToDecode.Replace("(I;", "(int;");
            stringToDecode = stringToDecode.Replace("(I)", "(int)");
            stringToDecode = stringToDecode.Replace(";L", ";");            
            stringToDecode = stringToDecode.Replace("(Z", "(bool;");
            stringToDecode = stringToDecode.Replace(";ZL", ";bool;");
            stringToDecode = stringToDecode.Replace(")Z", "):bool");
            

            stringToDecode = stringToDecode.Replace(")", "):");
            stringToDecode = stringToDecode.Replace(";)", ")");
            
            stringToDecode = stringToDecode.TrimEnd(new[] { ';' });            
            return stringToDecode;
        }
        
    }
}
