// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirCreator.DotNet
{
    public class CirCecilCompare
    {
        public static bool areEqual_MethodDefinitionAndCirFunction(MethodDefinition cecilMethodDefintion, ICirFunction cirFunction)
        {
            try
            {

                if (cecilMethodDefintion == null || cirFunction == null)
                    return false;
                var cirFunctionProperties = new Dictionary<string, object>();
                foreach (var property in DI.reflection.getProperties(cirFunction.GetType()))
                {
                    cirFunctionProperties.Add(property.Name, DI.reflection.getProperty(property.Name, cirFunction));
                    //      DI.log.info("prop: {0} = {1}", property.Name, cirFunctionProperties[property.Name]);
                }

                var methodsCalledInsideMethod = CecilUtils.getMethodsCalledInsideMethod(cecilMethodDefintion);
                var moduleName = (cecilMethodDefintion.DeclaringType.Module != null) ? cecilMethodDefintion.DeclaringType.Module.Assembly.Name.ToString() : "[NullModule]";

                foreach (var property in cirFunctionProperties.Keys)
                    switch (property)
                    {
                        case "FunctionSignature":                            
                            if (cirFunctionProperties[property].ToString() !=  String.Format("{0}!{1}", moduleName , cecilMethodDefintion))
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: cirFunction[FunctionSignature]!=cecilMethodDefintion");
                                return false;
                            }
                            break;

                        case "IsPrivate":
                        case "IsStatic":
                        case "IsConstructor":
                        case "IsUnmanaged":
                        case "IsUnmanagedExport":
                        case "IsVirtual":
                        case "IsSetter":
                        case "IsGetter":
                        case "IsRuntime":
                        case "IsPublic":
                        case "IsPInvokeImpl":
                        case "IsNative":
                        case "IsManaged":
                        case "IsInternalCall":
                        case "IsIL":
                        case "IsAbstract":
                        case "HasSecurity":
                        case "HasBody":
                            if ((bool) cirFunctionProperties[property] !=
                                (bool) DI.reflection.getProperty(property, cecilMethodDefintion))
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: (bool)cirFunctionProperties[" + property + 
                                    "] != (bool)DI.reflection.getProperty(" + property + ", cecilMethodDefintion)");
                                return false;
                            }
                            break;
                        case "ParentClass":
                            var cirClass1 = (CirClass) cirFunctionProperties[property];                        
                            if (cirClass1.Signature != CirFactoryUtils.getTypeUniqueSignatureFromTypeReference(cecilMethodDefintion.DeclaringType))
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: cirFunction[ParentClass]!=cecilMethodDefintion == " + cecilMethodDefintion.DeclaringType.FullName);
                                return false;
                            }
                            break;
                        case "ParentClassFullName":
                            var parentClassFullName = (string)cirFunctionProperties[property];
                            if (parentClassFullName != cecilMethodDefintion.DeclaringType.FullName)
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: cirFunction[ParentClassFullName]!=cecilMethodDefintion.FullName  " + cecilMethodDefintion.DeclaringType.FullName);
                                return false;
                            }
                            break;
                        case "ParentClassName":
                            var parentClassName = (string)cirFunctionProperties[property];
                            if (parentClassName != cecilMethodDefintion.DeclaringType.Name)
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: cirFunction[parentClassName]!=cecilMethodDefintion.FullName  " + cecilMethodDefintion.DeclaringType.Name);
                                return false;
                            }
                            break;
                        case "FunctionParameters":
                            var cirFunctionParameters = (List<ICirFunctionParameter>)cirFunctionProperties[property];
                            if (cecilMethodDefintion.Parameters.Count != cirFunctionParameters.Count)
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: the FunctionParameters count didn't match");
                                return false;
                            }
                            foreach (ParameterDefinition parameter in cecilMethodDefintion.Parameters)                                
                                if (false == ViewHelpers.getCirParameterTypeStringList(cirFunctionParameters).Contains(parameter.ParameterType.FullName))
                                {
                                    DI.log.error(
                                        "in areEqual_MethodDefinitionAndCirFunction: the FunctionParameters signatures didn't match");
                                    return false;
                                }
                            break;
                        case "FunctionsCalledUniqueList":
                            var functionsCalledUniqueList = (List<ICirFunction>)cirFunctionProperties[property];
                            foreach (MethodCalled methodCalled in methodsCalledInsideMethod)
                                if (false == ViewHelpers.getCirFunctionStringList(functionsCalledUniqueList).Contains(CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(methodCalled.memberReference)))
                                {
                                    DI.log.error(
                                        "in areEqual_MethodDefinitionAndCirFunction: there was a missing function in the FunctionsCalledUniqueList");
                                    return false;
                                }
                            break;
                        case "FunctionsCalledSequence":
                            var functionsCalledSequence = (List<ICirFunction>)cirFunctionProperties[property];
                            if (functionsCalledSequence.Count != methodsCalledInsideMethod.Count)
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: functionsCalledSequence.Count != methodsCalledInsideMethod.Count");
                                return false;
                            }
                            for (int i = 0; i < methodsCalledInsideMethod.Count; i++)
                            {
                                
                                if (CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(methodsCalledInsideMethod[i].memberReference) != ViewHelpers.getCirFunctionStringList(functionsCalledSequence)[i])
                                {
                                    DI.log.error(
                                        "in areEqual_MethodDefinitionAndCirFunction: the FunctionsCalledSequence does match");
                                    return false;
                                }
                            }
                            break;
                        case "ReturnType":
                            if (cecilMethodDefintion.ReturnType.ReturnType.FullName != cirFunctionProperties[property].ToString())
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: ReturnType don't match");
                                return false;
                            }
                            break;
                        case "CecilSignature":                            
                            if (cecilMethodDefintion.ToString() != cirFunctionProperties[property].ToString())
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: CecilSignature din't match");
                                return false;
                            }
                            break;
                        case "FunctionNameAndParameters":
                            if (CecilUtils.getMethodNameAndParameters(cecilMethodDefintion) != cirFunctionProperties[property].ToString())
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: FunctionNameAndParameters din't match");
                                return false;
                            }
                            break;
                        case "FunctionName":
                            if (cecilMethodDefintion.Name != cirFunctionProperties[property].ToString())
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: FunctionName din't match");
                                return false;
                            }
                            break;
                        case "Module":                            
                            if (moduleName != cirFunctionProperties[property].ToString())
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: Module din't match");
                                return false;
                            }
                            break;
                        case "ClassNameFunctionNameAndParameters":
                            var classNameFunctionNameAndParameters = cirFunctionProperties[property].ToString();
                            var expectedClassNameFunctionNameAndParameters = string.Format("{0}.{1}",
                                                                               cirFunction.ParentClassFullName,
                                                                               cirFunction.FunctionNameAndParameters);
                            if (classNameFunctionNameAndParameters != expectedClassNameFunctionNameAndParameters)
                            {
                                DI.log.error(
                                    "in areEqual_MethodDefinitionAndCirFunction: classNameFunctionNameAndParameters din't match");
                                return false;
                            }
                            break;                            
                            // ignore these ones
                        case "IsSource":
                        case "IsSink":
                        case "FunctionParameterTypes":
                        case "lcfgBasicBlocks":
                        case "SymbolDef":
                        case "ReflectionSignature":
                        case "O2MDbgSignature":
                        case "UsedTypes":
                        case "FunctionIsCalledBy":
                        case "dSsaVariables":
                        case "dVariables":
                        case "HasControlFlowGraph":
                        case "OnlyShowFunctionNameInToString":
                        case "HasBeenProcessedByCirFactory":
                            break;
                            // case "":
                            //break;
                        default:
                            DI.log.error("       property not handled: {0}", property);
                            break;
                    }                
                return true;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in areEqual_MethodDefinitionAndCirFunction", true);
                return false;
            }            
        }
    }
}
