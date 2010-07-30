// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Zip;
using O2.External.Python.Jython;
using O2.Interfaces.CIR;
using O2.Interfaces.O2Findings;
using O2.DotNetWrappers.Windows;

namespace O2.Cmd.SpringMvc.PythonScripts
{
    public class AnnotationsHelper
    {
        private static string jythonAnnotationScriptArgumentsFormat = "\"{0}\" \"{1}\"";

        public static string jythonAnnotationScript = Path.GetFullPath(@"PythonScripts\Annotations.py");        
        public static string tempFolderForAnnotationsXmlFiles = getDefaultTempFolderForAnnotationXmlFiles();

        private static string getDefaultTempFolderForAnnotationXmlFiles()
        {
            return Path.Combine(DI.config.O2TempDir, "_tempXmlAnnotations");
        }

                                                                                       
        public static string getJythonExecutionArguments(string targetFileOrFolder)
        {
            return string.Format(jythonAnnotationScriptArgumentsFormat, tempFolderForAnnotationsXmlFiles,
                                                targetFileOrFolder);
        }

        /// <summary>
        /// this will createAnnotationsXmlFileFromJavaClassFileOrFolder
        /// before execution it will delete all files from tempFolderForAnnotationsXmlFiles  
        /// </summary>
        /// <param name="targetFileOrFolder"></param>
        /// <returns>tempFolderForAnnotationsXmlFiles</returns>
        public static string createAnnotationsXmlFilesFromJavaClassFileOrFolder(string targetFileOrFolder)
        {
            tempFolderForAnnotationsXmlFiles = getDefaultTempFolderForAnnotationXmlFiles() + "_" + Files.getTempFileName();
            Files.checkIfDirectoryExistsAndCreateIfNot(tempFolderForAnnotationsXmlFiles);
            Files.deleteAllFilesFromDir(tempFolderForAnnotationsXmlFiles);
            executeJythonScript(targetFileOrFolder);
            return tempFolderForAnnotationsXmlFiles;
        }

        public static string executeJythonScript(string targetFileOrFolder)
        {
            Files.checkIfDirectoryExistsAndCreateIfNot(tempFolderForAnnotationsXmlFiles);
            return JythonExec.executePythonFile(jythonAnnotationScript, getJythonExecutionArguments(targetFileOrFolder));
        }

        public static void mapXmlFilesToCirData(string pathCirDataFile, string pathToClassFiles, string pathToRootClassFolder)
        {
            var cirData = CirLoad.loadFile(pathCirDataFile);
            DI.log.info("There are {0} functions loaded", cirData.dFunctions_bySignature.Keys.Count);
            var attributeXmlFiles = getAttributeXmlFiles(pathToClassFiles, pathToRootClassFolder);

            var numberOfControllersMapped = 0;
            foreach (var attributeXmlFile in attributeXmlFiles.Keys)
            {
                var resolvedParentClass = attributeXmlFile.Replace("\\", ".").Replace(".class.JavaAttributes.xml", "");
                if (cirData.dClasses_bySignature.ContainsKey(resolvedParentClass))
                {
                    //DI.log.info(" we have a match : {0} -> {1}",  resolvedParentClass , attributeXmlFiles[attributeXmlFile]);
                    XDocument xDoc = XDocument.Load(attributeXmlFiles[attributeXmlFile]);
                    foreach (var cirFunction in cirData.dClasses_bySignature[resolvedParentClass].dFunctions.Values)
                    {
                        var xmlMethodElements = from xElement
                                            in xDoc.Elements("JavaAttributeMappings").Elements("class").Elements("method")
                                                where xElement.Attribute("name").Value == cirFunction.FunctionName
                                                select xElement;
                        // for now map all xmlMethods to the same CirData  (this could create a false positive if there are controllers with the same name (but diferent signature
                        foreach (var xMethodElement in xmlMethodElements)
                            if (SpringMVCAttributes.addTaintedInfoToCirFunction(xMethodElement, cirFunction))
                                numberOfControllersMapped++;
                        if (cirFunction.IsTainted && xmlMethodElements.Count() > 1)
                            DI.log.error("DOUBLE MAPPING since cirFunction.IsTained && xmlMethodElements.Count() >1 :  {0} -> {1}", resolvedParentClass, cirFunction.FunctionName);
                    }

                }
                else
                    if (resolvedParentClass.IndexOf('$') == -1)
                        DI.log.error(" we DONT have a match : {0}", resolvedParentClass);

            }
            DI.log.info("There were {0} controllers mappings added", numberOfControllersMapped);
            var newCirDataFile = pathCirDataFile + ".WithSpringMvcControllersAsCallbacks.CirData";
            CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, newCirDataFile);
        }

        public static void mapXmlFilesToFindings(string pathToClassFiles, string pathToRootClassFolder, string pathToOzasmtFile, IO2AssessmentLoad o2AssessmentLoad)
        {            
            var attributeXmlFiles = getAttributeXmlFiles(pathToClassFiles, pathToRootClassFolder);
            var o2Assessment = new O2Assessment (o2AssessmentLoad,pathToOzasmtFile);
            mapJavaAttributesToTraces(o2Assessment, attributeXmlFiles);            
        }
        
        public static void mapJavaAttributesToTraces(IO2Assessment o2Assessment, Dictionary<string, string> attributesXmlFiles)
        {
            DI.log.debug("Mapping Java Attributes to Traces");
            //var testFindings = from O2Finding finding in o2Assessment.o2Findings where finding.Source.Contains("BugController") select (IO2Finding)finding;
            //var testFindings = o2Assessment.o2Findings;
            DI.log.debug("There are {0} findings to process", o2Assessment.o2Findings.Count());

            foreach (O2Finding finding in o2Assessment.o2Findings)
            {
                var filteredSignature = new FilteredSignature(finding.Source);
                var className = filteredSignature.sFunctionClass;
                var fileToFind = string.Format("{0}.class.JavaAttributes.xml", className.Replace(".", "\\"));
                if (attributesXmlFiles.ContainsKey(fileToFind))
                    mapJavaAttributesToFinding(finding, attributesXmlFiles[fileToFind]);
                //DI.log.info("Found: {0} - > {1}", 	fileToFind , attributesXmlFiles[fileToFind]);
                //else
                //	DI.log.error("could NOT find Xml Attribute file for: {0}", 	fileToFind);
                //DI.log.info(fileToFind);
            }

            //var findingsWithSpringMVCControllersAsSources = new List<IO2Finding>();


            // save temp assessment file			
//            var o2FindingsOfTypeO2SpringMvcController = (from o2Finding in o2Assessment.o2Findings where o2Finding.vulnType == "O2.SpringMvc.Controller" select o2Finding).ToList();
//            DI.log.debug("There are {0}  o2FindingsOfTypeO2SpringMvcController");

/*            O2.Views.ASCX.O2Findings.ascx_FindingsViewer.openInFloatWindow(o2FindingsOfTypeO2SpringMvcController);
            saveFindingsInNewO2AssessmentFile(o2FindingsOfTypeO2SpringMvcController, pathToOzasmtFile + "_SpringMvcController.ozasmt");
 * */
        }


        public static bool mapJavaAttributesToFinding(O2Finding o2Finding, string xmlAttributeFile)
        {
            var source = o2Finding.Source;
            var filteredSignature = new FilteredSignature(source);
            //DI.log.info(filteredSignature.sFunctionClass + "  -  " + filteredSignature.sFunctionName);

            var xClassElement = getClassDataFromXmlAttributeFile(xmlAttributeFile, filteredSignature.sFunctionClass);
            if (xClassElement != null)
            {
                SpringMVCAttributes.addClassAttributesToFinding(xClassElement, o2Finding);
                var xMethodElement = getMethodDataFromXmlAttributeFile(xClassElement, filteredSignature.sFunctionName);
                if (xMethodElement != null)
                {
                    SpringMVCAttributes.addMethodAttributesToFinding(xMethodElement, o2Finding);
                    return true;
                    // DI.log.info("have xElement");
                }
            }
            return false;
            //DI.log.info("mapping finding {0} with xml file {1}", o2Finding.ToString(), xmlAttributeFile);
        }

        public static XElement getClassDataFromXmlAttributeFile(string xmlAttributeFile, string classToFind)
        {
            XDocument xDoc = XDocument.Load(xmlAttributeFile);

            var classElement = from xElement
                                in xDoc.Elements("JavaAttributeMappings").Elements("class")
                               where xElement.Attribute("name").Value == classToFind
                               select xElement;
            if (classElement.Count() == 1)
                return classElement.ToList()[0];

            DI.log.error("Count not find class XElement for:{0}", classToFind);
            return null;
            /*			
                        foreach(var xElement in xDoc.Elements().Elements())				
                            foreach(var xmlAttribute in xElement.Attributes())
                                if (xmlAttribute.Name =="name" && xmlAttribute.Value == classToFind)
                                    return xElement;
                        DI.log.error("Count not find class XElement for:{0}", classToFind);*/

        }

        public static XElement getMethodDataFromXmlAttributeFile(XElement xClassElement, string methodToFind)
        {
            methodToFind = methodToFind.Replace("<", "&lt;").Replace(">", "&gt;");
            foreach (var xElement in xClassElement.Elements())
                foreach (var xmlAttribute in xElement.Attributes())
                    if (xmlAttribute.Name == "name" && xmlAttribute.Value == methodToFind)
                        return xElement;
            DI.log.error("Could not find method XElement for:{0}", methodToFind);
            return null;
        }

       

        public static Dictionary<string, string> getAttributeXmlFiles(string pathToDirectoryToSearchForFiles, string pathToRootClassFolder)
        {
            var xmlFiles = Files.getFilesFromDir_returnFullPath(pathToDirectoryToSearchForFiles, "*.JavaAttributes.xml", true);
            DI.log.info("Found {0} Xml files with Java attributes", xmlFiles.Count);
            // create a dictionary that has the found xml files with the pathToRootClassFolder (so that we can map it directly to the java class name)
            var attributesXmlFiles = new Dictionary<string, string>();
            foreach (var xmlFile in xmlFiles)
                attributesXmlFiles.Add(xmlFile.Replace(pathToRootClassFolder, ""), xmlFile);
            return attributesXmlFiles;
        }


        public static String getPythonStringTargetFileOrFolder(string fileOrFolderToProcess, bool processJarFiles)
        {
            var tempFolderToHoldTargetSites = DI.config.getTempFolderInTempDirectory("JythonTargetFiles");
            Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(tempFolderToHoldTargetSites, "Config"));
            if (Directory.Exists(fileOrFolderToProcess))
            {
                foreach (var fileToProcess in Files.getFilesFromDir_returnFullPath(fileOrFolderToProcess, "*.*", true))
                    getPythonStringTargetFile(fileToProcess, tempFolderToHoldTargetSites, processJarFiles);
            }
            else
                getPythonStringTargetFile(fileOrFolderToProcess, tempFolderToHoldTargetSites, processJarFiles);
            return tempFolderToHoldTargetSites;
        }

        public static void getPythonStringTargetFile(string fileToProcess, string targetFolder, bool processJarFiles)
        {
            //if (Directory.Exists(fileOrFolderToProcess))
            //    return fileOrFolderToProcess;
            if (File.Exists(fileToProcess))
            {
                var extension = Path.GetExtension(fileToProcess).ToLower();
                switch (extension)
                {
                    case ".zip":
                    case ".jar":
                    case ".war":
                        if (extension == ".jar" && false == processJarFiles)     // handle the case where we don't want to process the Jar files
                            return;
                        targetFolder = Path.Combine(targetFolder, Path.GetFileName(fileToProcess).Replace(".", "_"));
                        var unzipedFiles = new zipUtils().unzipFileAndReturnListOfUnzipedFiles(fileToProcess, targetFolder);
                        foreach (var unzipedFile in unzipedFiles)
                            getPythonStringTargetFile(unzipedFile, targetFolder, processJarFiles);
                        break;
                    case ".class":
                        var targetFile = fileToProcess.Replace(':','_').Replace('/','_').Replace('\\','_');
                        Files.Copy(fileToProcess, Path.Combine(targetFolder, targetFile));
                        break;
                }
            }
        }

        public static List<string> calculateFilesToProcess(string fileOrFolderToProcess, string targetFolder)
        {
            return Files.getFilesFromDir_returnFullPath(targetFolder, "*.xml", true);
        }

    }

    class SpringMVCAttributes
    {        

        public static bool addTaintedInfoToCirFunction(XElement xMethodElement, ICirFunction cirFunction)
        {
            var methodParametersAnnotations = from xElement in xMethodElement.Elements("methodAttribute")
                                                                             .Elements("methodAnnotation")
                                              where xElement.Attribute("typeName").Value == "org.springframework.web.bind.annotation.RequestMapping"
                                              select xElement;
            if (methodParametersAnnotations.Count() > 0)
            {
                //log.info("in addTaintedInfoToCirFunction, found controller for function: {0}", cirFunction.FunctionSignature);
                cirFunction.IsTainted = true;
                return true;
            }
            return false;
        }

        public static void addClassAttributesToFinding(XElement xClassElement, O2Finding o2Finding)
        {
            //var pathToSource = o2Finding.getPathToSource();
            //var numberOfTraces = pathToSource.Count;
            //if (pathToSource.Count >1)
            //{
            //	var rootTrace = pathToSource[numberOfTraces-1];				
            var classAnnotations = from xelement in xClassElement.Elements("attribute").Elements("annotation") select xelement;
            if (classAnnotations.Count() > 0)
            {
                var annotationsTrace = new O2Trace("Annotations for class: " + xClassElement.Attribute("name").Value);
                o2Finding.o2Traces.Insert(0, annotationsTrace);
                foreach (var annotation in classAnnotations)
                    annotationsTrace.childTraces.Add(new O2Trace(annotation.Attribute("toString").Value, TraceType.Type_4));
            }
            //}
        }

        public static void addMethodAttributesToFinding(XElement xMethodElement, O2Finding o2Finding)
        {
            var pathToSource = o2Finding.getPathToSource();
            var numberOfTraces = pathToSource.Count;
            if (pathToSource.Count > 1)
            {
                var rootTrace = pathToSource[numberOfTraces - 1];
                // add annotations in Method's Parameters
                var methodParametersAnnotations = from xelement in xMethodElement.Elements("methodParameterAnnotation") select xelement;
                if (methodParametersAnnotations.Count() > 0)
                {
                    var methodParametersAttributes = new O2Trace("Spring MVC - Method Parameters Attributes");
                    foreach (var annotation in methodParametersAnnotations)
                    {
                        if (annotation.Attribute("toString") == null)
                            methodParametersAttributes.childTraces.Add(new O2Trace("no attribute"));
                        else
                        {
                            //var annotationTrace1 = new O2Trace(annotation.Attribute("toString").Value, TraceType.Type_4);
                            var annotationTrace = new O2Trace(annotation.Attribute("typeName").Value, TraceType.Type_4);
                            foreach (var member in annotation.Elements("member"))
                            {
                                //annotationTrace.childTraces.Add(new O2Trace(annotation.Attribute("typeName").Value, TraceType.Type_6));
                                var memberTraceText = string.Format("{0}={1}", member.Attribute("memberName").Value, member.Attribute("memberValue").Value);
                                annotationTrace.childTraces.Add(new O2Trace(memberTraceText, TraceType.Type_6));
                            }
                            methodParametersAttributes.childTraces.Add(annotationTrace);
                        }
                    }
                    rootTrace.childTraces.Insert(0, methodParametersAttributes);
                }

                // add annotations in Method 				
                var methodAnnotations = from xelement in xMethodElement.Elements("methodAttribute").Elements("methodAnnotation") select xelement;
                if (methodAnnotations.Count() > 0)
                {
                    var methodAttributes = new O2Trace("Spring MVC - Method Attributes");
                    foreach (var annotation in methodAnnotations)
                    {
                        var annotationTrace = new O2Trace(annotation.Attribute("typeName").Value, TraceType.Type_4);
                        foreach (var member in annotation.Elements("member"))
                        {
                            var memberTraceText = string.Format("{0}={1}", member.Attribute("memberName").Value, member.Attribute("memberValue").Value);
                            annotationTrace.childTraces.Add(new O2Trace(memberTraceText, TraceType.Type_6));
                        }
                        methodAttributes.childTraces.Insert(0, annotationTrace);

                        // handle special case of org.springframework.web.bind.annotation.RequestMapping (which we want to make the source)
                        if (annotation.Attribute("typeName").Value == "org.springframework.web.bind.annotation.RequestMapping")
                        {
                            //annotationTrace.traceType = TraceType.Source;
                            o2Finding.vulnType = "O2.SpringMvc.Controller";
                            o2Finding.vulnName = o2Finding.Sink;
                        }
                    }
                    rootTrace.childTraces.Insert(0, methodAttributes);
                }

                //else
                //	log.error("There are no method Attributes for method: " + xMethodElement.ToString());

            }
        }       
    }
}
