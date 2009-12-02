// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.CIR;


namespace O2.Scanner.OunceLabsCLI.ScanTargets
{
    public class CustomProjectsJ2EE
    {
        public static bool bCopyReferencesToNewProject;

        /*
                    bool bScanAfterProjectBuild = false;
                    String sSourceProject = @"E:\_AppsToScan_\webgoat\Ounce_Project\WebGoat - Manual.ppf";
                    String sTargetFolder = @"E:\_AppsToScan_\webgoat\Ounce_Project\test7";
                    String sSourcePathToJavaFiles  = @"E:\_AppsToScan_\webgoat\project\JavaSource";
                    String sSourcePathToJspFiles = @"E:\_AppsToScan_\webgoat\project\WebContent" ;
                    String sSourceF1CirData = @"E:\_AppsToScan_\webgoat\Ounce_Project\WebGoat - Manual.CirData";
                    String sFunctionFilter = "org.owasp";
                    String sNewProjectName = "F1 Custom Project" + Path.GetFileName(sTargetFolder);
    		
                    //List<String> lsStartClasses = new List<String>(new String[] {@"Manual.lessons.CrossSiteScripting.CrossSiteScripting_jsp" , @"Manual.lessons.CrossSiteScripting.EditProfile_jsp"});
                    List<String> lsStartClasses = CustomProjects_J2EE.calculateListOfJspFilesFromProject(sSourceProject);
                    String sNewProjectFile = CustomProjects_J2EE.createOunceProjectForAllReferencesOf_Class(sNewProjectName,sSourceProject, sTargetFolder, sSourcePathToJavaFiles, sSourcePathToJspFiles, lsStartClasses, sFunctionFilter, sSourceF1CirData);
                     DI.log.info("All Done: {0}",sNewProjectFile );
    		
    		
                    String sNewApplicationFile = sNewProjectFile + ".paf";		
                    ounceAnalysis.createTempApplicationFileForProject(sNewProjectFile,sNewApplicationFile);		
                    if (bScanAfterProjectBuild)
                    {
    			
                        new ounceCore.scanAndReturnXmlAssessmentFile().scanProject_ReturnXmlAssessment(sNewApplicationFile, sNewProjectFile);
                    }
                    */

        public static String createCleanOunceProjectFromExistingProject(String sSourceProject, String sTargetFolder)
        {
            try
            {
                if (sSourceProject == "")
                    sSourceProject = @"E:\_AppsToScan_\webgoat\Ounce_Project\WebGoat - Manual.ppf";
                if (sTargetFolder == "")
                    sTargetFolder = @"E:\_SPRING_WP_\_Custom_Analysis_of_Spring_2.53";


                //ounceCore_5_03.f1OunceProject fopF1OunceProject = new ounceCore_5_03.f1OunceProject(sSourceProject);
                //List<String> lsFilesInProject = fopF1OunceProject.getProjectFiles();
                // the following is needed so that the current add-on doesn't need a reference to the prexisInterop.dll
                String sProjectDirectory = Path.GetDirectoryName(sSourceProject);
// need to fix this are remote dependencies with O2 Scan modules
//List<OunceProject.Source> lsSources = null;
                List<String> lsProjectFileSearchFilter = null;
                List<String> lsFilesInProject = null;
                String sLanguageType = null;
                String sProjectName = null;
                bool bDataLoadedOk = false;
// need to fix this are remote dependencies with O2 Scan modules
//                o2.ounce.core.Core.OunceProject.GetSourcesAndFileSearchFilter_fromProjectFile(sSourceProject,
//                    ref lsSources, ref lsProjectFileSearchFilter, ref sLanguageType, ref sProjectName, ref bDataLoadedOk);
/*
                if (lsSources.Count > 2)
                     DI.log.error("Aborting only J2EE project with two source paths are supported (one class and one web)");
                else
                {
                    String sSourcePathToJavaFiles = "";
                    String sSourcePathToJspFiles = "";
                    if (lsSources.Count == 1)
                        sSourcePathToJspFiles = sSourcePathToJavaFiles = Path.GetFullPath(Path.Combine(sProjectDirectory, lsSources[0].sPath));
                    else
                        foreach (o2.ounce.core.Core.OunceProject.Source sSource in lsSources)
                            if (sSource.sWeb == "false")
                                sSourcePathToJavaFiles = Path.GetFullPath(Path.Combine(sProjectDirectory, sSource.sPath));
                            else
                                sSourcePathToJspFiles = Path.GetFullPath(Path.Combine(sProjectDirectory, sSource.sPath));

                    String sNewProjectName = "o2_" + Path.GetFileName(sSourceProject);
                    sTargetFolder = Path.Combine(sTargetFolder, Path.GetFileNameWithoutExtension(sNewProjectName));
                    String sTargetProject = Path.Combine(sTargetFolder, sNewProjectName);


                    lsFilesInProject = getListOfFilesFromJavaAndJspSourceDirs(sSourcePathToJavaFiles, sSourcePathToJspFiles);
                    CustomProjectsJ2EE.bCopyReferencesToNewProject = true;
                    createCustomProject(sNewProjectName, lsFilesInProject, sSourceProject, sTargetFolder, sTargetProject, sSourcePathToJavaFiles, sSourcePathToJspFiles);
                    return sTargetProject;
                }
 */
            }
            catch (Exception ex)
            {
                DI.log.debug("in createCleanOunceProjectFromExistingProject: {0}", ex.Message);
            }
            return "";
        }

        public static String createOunceProjectForAllReferencesOf_Class(String sNewProjectName, String sSourceProject,
                                                                        String sTargetFolder,
                                                                        String sSourcePathToJavaFiles,
                                                                        String sSourcePathToJspFiles,
                                                                        List<String> lsStartClasses,
                                                                        String sFunctionFilter, String sSourceF1CirData)
        {
            Files.checkIfDirectoryExistsAndCreateIfNot(sTargetFolder);
            String sTargetProject = Path.Combine(sTargetFolder, Path.GetFileName(sSourceProject));
            if (sSourceProject == sTargetProject) // make sure we don't override the Source project
            {
                DI.log.debug("Error: sTargetF1CirDataFile == sSourceProject - {0} ", sTargetProject);
                return "";
            }

            var fcdCirData = new CirData();
            fcdCirData = CirLoad.loadSerializedO2CirDataObject(sSourceF1CirData);

            List<ICirClass> lccTargetCompilationClasses = null; // = new List<CirClass>();
            List<String> lsFunctionsCalled = null; // = new List<string>();


            // this might not be needed
            // need to do this since the SymbolDef are not automatically mapped in the Symbols table
            //var dFunctionsNames = new Dictionary<string, string>();
            //foreach (ICirFunction cfCirFunction in fcdCirData.dFunctions_bySignature.Values)
            //    if (cfCirFunction != null && false == dFunctionsNames.ContainsKey(cfCirFunction.FunctionSignature))
            //        dFunctionsNames.Add(cfCirFunction.FunctionSignature, cfCirFunction.SymbolDef);

            // calculate Target classes
            foreach (String sStartClass in lsStartClasses)
                calculateTargetComplilationClassFromStartClassSignature(sStartClass, ref lccTargetCompilationClasses,
                                                                        ref lsFunctionsCalled, sFunctionFilter,
                                                                        /*dFunctionsNames, */fcdCirData);

            // calculate full paths to class files to include in custom project
            List<String> lsPathsToClassFilesSourceCode =
                getListOfFilesToScanFromListOfClasses_J2EE(sSourcePathToJavaFiles, sSourcePathToJspFiles,
                                                           lccTargetCompilationClasses, fcdCirData);
            DI.log.debug(
                "Completed calculation of class files to include:  there where {0} classes with {1} functions called",
                lccTargetCompilationClasses.Count, lsFunctionsCalled.Count);


            createCustomProject(sNewProjectName, lsPathsToClassFilesSourceCode, sSourceProject, sTargetFolder,
                                sTargetProject, sSourcePathToJavaFiles, sSourcePathToJspFiles);

            return sTargetProject;
        }

        public static void calculateTargetComplilationClassFromStartClassSignature(
            String sStartClassSignature, ref List<ICirClass> lccTargetCompilationClasses,
            ref List<String> lsFunctionsCalled, String sResultsFilter, /*Dictionary<String, String> dFunctionsNames,*/
            ICirData fcdCirData)
        {
            if (lccTargetCompilationClasses == null)
                lccTargetCompilationClasses = new List<ICirClass>();
            if (lsFunctionsCalled == null)
                lsFunctionsCalled = new List<string>();
            ICirClass ccTargetCirClass = null;
            if (fcdCirData.dClasses_bySignature.ContainsKey(sStartClassSignature))
                ccTargetCirClass = fcdCirData.dClasses_bySignature[sStartClassSignature];
            else
            {
                foreach (ICirClass ccCirClass in fcdCirData.dClasses_bySignature.Values)
                {
                    if (ccCirClass.Signature.IndexOf(sStartClassSignature) > -1)
                    {
                        //ccTargetCirClass = fcdCirData.dClasses_bySignature[sCirClass];
                        lccTargetCompilationClasses.Add(ccCirClass);
                        break;
                    }
                }
            }

            calculateListofMethodsCalledByClass_Recursive(ccTargetCirClass, lccTargetCompilationClasses,
                                                          lsFunctionsCalled, sResultsFilter, /*dFunctionsNames,*/ fcdCirData);


            //foreach (String sFunctionCalled in lsFunctionsCalled)
            //     DI.log.debug(getSymbol(sFunctionCalled));     
        }

        public static void createCustomProject(String sNewProjectName, List<String> lsPathsToClassFilesSourceCode,
                                               String sSourceProject, String sTargetFolder, String sTargetProject,
                                               String sSourcePathToJavaFiles, String sSourcePathToJspFiles)
        {
            if (lsPathsToClassFilesSourceCode == null || lsPathsToClassFilesSourceCode.Count == 0)
                DI.log.error("in createCustomProject: There were no source code files in the provided project");
            else
            {
                String sTargetPathToJavaFiles = Path.Combine(sTargetFolder, Path.GetFileName(sSourcePathToJavaFiles));
                String sTargetPathToJspFiles = Path.Combine(sTargetFolder, Path.GetFileName(sSourcePathToJspFiles));
                Files.checkIfDirectoryExistsAndCreateIfNot(sTargetPathToJavaFiles);
                Files.checkIfDirectoryExistsAndCreateIfNot(sTargetPathToJspFiles);

                // load Source project
                var xdXmlDocument = new XmlDocument();
                xdXmlDocument.Load(sSourceProject);

                String name = xdXmlDocument.DocumentElement.Name;

                // set project name
                //   if (xdXmlDocument.DocumentElement["Project"] != null)
                if (xdXmlDocument.DocumentElement.Attributes["name"] != null)

                    xdXmlDocument.DocumentElement.Attributes["name"].Value = sNewProjectName;

                // remove current Source elements
                XmlNodeList xnlSources = xdXmlDocument.GetElementsByTagName("Source");
                for (int i = 0; xnlSources.Count > 0; i++)
                    xdXmlDocument.DocumentElement.RemoveChild(xnlSources[0]);
                xdXmlDocument.DocumentElement.AppendChild(getSourceElement(xdXmlDocument, sTargetPathToJavaFiles,
                                                                           "false", "false"));
                xdXmlDocument.DocumentElement.AppendChild(getSourceElement(xdXmlDocument, sTargetPathToJspFiles, "false",
                                                                           "true"));


                // create target Project Xml File
                xdXmlDocument.Save(sTargetProject);

                // copy references
                if (bCopyReferencesToNewProject)
                    copyJavaReferencesFromSourceProjectToTargetFolderAndUpdateNewProjectFile(sSourceProject,
                                                                                             sTargetProject,
                                                                                             sTargetPathToJspFiles);
                else
                    fixJavaReferencesOnNewProjectFile(sSourceProject, sTargetProject, sTargetPathToJspFiles);


                // copy source code files into TargetDirectories
                foreach (String sFile in lsPathsToClassFilesSourceCode)
                {
                    String sNewFile = sFile;
                    if (sFile.IndexOf(sSourcePathToJavaFiles) > -1)
                        sNewFile = sFile.Replace(sSourcePathToJavaFiles, sTargetPathToJavaFiles);
                    else if (sFile.IndexOf(sSourcePathToJspFiles) > -1)
                        sNewFile = sFile.Replace(sSourcePathToJspFiles, sTargetPathToJspFiles);
                    else
                    {
                        DI.log.error("File to scan outside provided Java and Jsp Paths: {0}", sFile);
                    }
                    // DI.log.info(" new file: {0}",sNewFile);
                    String sDirectory = Path.GetDirectoryName(sNewFile);
                    Files.checkIfDirectoryExistsAndCreateIfNot(sDirectory);
                    File.Copy(sFile, sNewFile, true);
                }
            }
        }

        public static XmlElement getSourceElement(XmlDocument xdXmlDocument, String sPath, String sExclude, String sWeb)
        {
            XmlElement xeSource = xdXmlDocument.CreateElement("Source");
            xeSource.SetAttribute("path", sPath);
            xeSource.SetAttribute("exclude", sExclude);
            xeSource.SetAttribute("web", sWeb);
            return xeSource;
        }


        public static List<String> getListOfFilesToScanFromListOfClasses_J2EE(String sSourcePathToJavaFiles,
                                                                              String sSourcePathToJspFiles,
                                                                              List<ICirClass> lccTargetCompilationClasses,
                                                                              ICirData fcdCirData)
        {
            var sPathsToClassFilesSourceCode = new List<string>();

            foreach (ICirClass ccCirClass in lccTargetCompilationClasses)
            {
                if (ccCirClass != null)
                {
                    String sFilePath = ccCirClass.Signature;
                    sFilePath = sFilePath.Replace(".", @"\");
                    if (sFilePath.IndexOf("_jsp") > -1)
                    {
                        bool bFoundFile = false;
                        sFilePath = sFilePath.Substring(sFilePath.IndexOf(@"\"));
                        sFilePath = sFilePath.Replace("_jsp", ".jsp");
                        foreach (String sFile in fcdCirData.lFiles)
                        {
                            if (sFile.IndexOf(sFilePath) > -1)
                            {
                                sPathsToClassFilesSourceCode.Add(sFile);
                                bFoundFile = true;
                                break;
                            }
                        }
                        if (false == bFoundFile)
                            DI.log.error(
                                "in createOunceProjectForAllReferencesOf_Class, could not resolve jsp file: {0}",
                                sFilePath);
                    }
                    else
                    {
                        sFilePath = Path.Combine(sSourcePathToJavaFiles, sFilePath + ".java");
                        if (fcdCirData.lFiles.Contains(sFilePath))
                            sPathsToClassFilesSourceCode.Add(sFilePath);

                        else
                            DI.log.error(
                                "in createOunceProjectForAllReferencesOf_Class, could not resolve class to file: {0}",
                                ccCirClass.Signature);
                    }
                }
            }
            return sPathsToClassFilesSourceCode;
        }

        public static void calculateListofMethodsCalledByClass_Recursive(
            ICirClass ccClass, List<ICirClass> lccTargetCompilationClasses, List<String> lsFunctionsCalled,
            String sResultsFilter/*, Dictionary<String, String> dFunctionsNames*/, ICirData fcdCirData)
        {
            if (ccClass != null)
                foreach (ICirFunction cfCirFunction in ccClass.dFunctions.Values)
                    foreach (ICirFunction cirFunctionCalled in cfCirFunction.FunctionsCalledUniqueList)
                    {
                        String sName = cirFunctionCalled.FunctionSignature;
                        //    if (sName.IndexOf("()") == -1)                          // don't add functions that receive no parameters
                        if (sResultsFilter == "" || sName.IndexOf(sResultsFilter) > -1)
                        {
                            if (false == lsFunctionsCalled.Contains(cirFunctionCalled.FunctionSignature))
                            {
                                lsFunctionsCalled.Add(cirFunctionCalled.FunctionSignature);
                                ICirClass ccCalledClass = getCirClassObjectFromFunctionSignature(cirFunctionCalled.FunctionSignature,fcdCirData);
                                if (false == lccTargetCompilationClasses.Contains(ccCalledClass))
                                    lccTargetCompilationClasses.Add(ccCalledClass);
                                calculateListofMethodsCalledByClass_Recursive(ccCalledClass, lccTargetCompilationClasses,
                                                                              lsFunctionsCalled, sResultsFilter,
                                                                              /*dFunctionsNames,*/ fcdCirData);
                                //     getCirClassObjectFromFunctionDef(sFunctionCalled,dFunctionsNames, fcdCirData);
                            }
                        }
                    }
        }


        public static ICirClass getCirClassObjectFromFunctionSignature(String functionSignature,
                                                                 //Dictionary<String, String> dFunctionsNames,
                                                                 ICirData fcdCirData)
        {
            //String sSignatureOfFunctionCalled = sFunctionDef;

            //if (dFunctionsNames.ContainsKey(functionSignature))
            if (fcdCirData.dFunctions_bySignature.ContainsKey(functionSignature))
            {
                ICirFunction cirFunction = fcdCirData.dFunctions_bySignature[functionSignature];
                    //fcdCirData.dFunctions_bySymbolDef[dFunctionsNames[sSignatureOfFunctionCalled]];
                return cirFunction.ParentClass;
            }
            DI.log.error("in getCirClassObjectFromFunctionDef, could not resolve class object");
            //else
            return null;
        }

        public static void fixJavaReferencesOnNewProjectFile(String sSourceProject, String sTargetProject,
                                                             String sTargetPathToJspFiles)
        {
            String sSourceBaseDirectory = Path.GetDirectoryName(sSourceProject);
            String sTargetBaseDirectory = Path.GetDirectoryName(sTargetProject);
            String sNewResolvedClassPathString = "";
            var xdXmlDocument = new XmlDocument();
            xdXmlDocument.Load(sSourceProject);
            if (xdXmlDocument.DocumentElement != null & xdXmlDocument.DocumentElement["Configuration"] != null &&
                xdXmlDocument.DocumentElement["Configuration"].Attributes != null &&
                xdXmlDocument.DocumentElement["Configuration"].Attributes["class_path"] != null)
            {
                XmlElement xeConfiguration = xdXmlDocument.DocumentElement["Configuration"];
                String sCompleteClassPath = xeConfiguration.Attributes["class_path"].Value;
                String[] asClassPath = sCompleteClassPath.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (String sClassPathReference in asClassPath)
                {
                    String sResolvedPathToSourceReference =
                        Path.GetFullPath(Path.Combine(sSourceBaseDirectory, sClassPathReference));
                    //sResolvedPathToSourceReference = sResolvedPathToSourceReference.Replace(sSourceBaseDirectory,sTargetBaseDirectory);
                    sNewResolvedClassPathString += sResolvedPathToSourceReference + ";";
                }
                DI.log.info(sNewResolvedClassPathString);
                xdXmlDocument.Load(sTargetProject);
                xdXmlDocument.DocumentElement.Attributes["web_context_root_path"].Value = sTargetPathToJspFiles;

                xdXmlDocument.DocumentElement["Configuration"].Attributes["class_path"].Value =
                    sNewResolvedClassPathString;
                xdXmlDocument.Save(sTargetProject);
            }
            DI.log.info("Completed copying all references to new project location");
        }

        public static void copyJavaReferencesFromSourceProjectToTargetFolderAndUpdateNewProjectFile(
            String sSourceProject, String sTargetProject, String sTargetPathToJspFiles)
        {
            try
            {
                String sDefaultReferencesDirectory = "_AllClassPathReferences\\";
                String sSourceBaseDirectory = Path.GetDirectoryName(sSourceProject);
                String sTargetBaseDirectory = Path.GetDirectoryName(sTargetProject);
                String sTargetReferencesDirectory = Path.Combine(sTargetBaseDirectory, sDefaultReferencesDirectory);
                Files.deleteFolder(sTargetReferencesDirectory);
                Files.checkIfDirectoryExistsAndCreateIfNot(sTargetReferencesDirectory);

                String sNewResolvedClassPathString = sDefaultReferencesDirectory + ";";
                var xdXmlDocument = new XmlDocument();
                xdXmlDocument.Load(sSourceProject);
                if (xdXmlDocument.DocumentElement != null & xdXmlDocument.DocumentElement["Configuration"] != null &&
                    xdXmlDocument.DocumentElement["Configuration"].Attributes != null &&
                    xdXmlDocument.DocumentElement["Configuration"].Attributes["class_path"] != null)
                {
                    XmlElement xeConfiguration = xdXmlDocument.DocumentElement["Configuration"];
                    String sCompleteClassPath = xeConfiguration.Attributes["class_path"].Value;
                    String[] asClassPath = sCompleteClassPath.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String sClassPathReference in asClassPath)
                    {
                        String sResolvedPathToSourceReference =
                            Path.GetFullPath(Path.Combine(sSourceBaseDirectory, sClassPathReference));
                        if (Directory.Exists(sResolvedPathToSourceReference))
                        {
                            var lsClassFiles = new List<String>();
                            Files.getListOfAllFilesFromDirectory(lsClassFiles, sResolvedPathToSourceReference, true,
                                                                 "*.class", false);
                            for (int i = 0; i < lsClassFiles.Count; i++)
                            {
                                String sSourceClassFile = lsClassFiles[i];
                                String sTargetClassFile = lsClassFiles[i].Replace(sResolvedPathToSourceReference,
                                                                                  sTargetReferencesDirectory);
                                Files.checkIfDirectoryExistsAndCreateIfNot(Path.GetDirectoryName(sTargetClassFile));
                                File.Copy(sSourceClassFile, sTargetClassFile, true);
                            }
                            // DI.log.info("Is a Directory: {0}, {1}", sResolvedPathToSourceReference, lsClassFiles[0]);
                        }
                        else // it is a file
                        {
                            String sTargetFile = Path.Combine(sTargetReferencesDirectory,
                                                              Path.GetFileName(sResolvedPathToSourceReference));
                            if (File.Exists(sResolvedPathToSourceReference) == false)
                                DI.log.error(
                                    "in copyJavaReferencesFromSourceProjectToTargetFolderAndUpdateNewProjectFile: Could not find referenced file: {0}",
                                    sResolvedPathToSourceReference);
                            else
                            {
                                if (File.Exists(sTargetFile))
                                {
                                    DI.log.error(
                                        "in copyJavaReferencesFromSourceProjectToTargetFolderAndUpdateNewProjectFile: Duplicate Jar in orginal jar dependency list (going to copy with an extra underscore :{0}",
                                        sTargetFile);
                                    sTargetFile = sTargetFile + "_" + Path.GetExtension(sTargetFile);
                                    if (File.Exists(sTargetFile))
                                        DI.log.error(
                                            "in copyJavaReferencesFromSourceProjectToTargetFolderAndUpdateNewProjectFile: file with extra _ on extension also exited!!!");
                                }
                                File.Copy(sResolvedPathToSourceReference, sTargetFile, true);
                                sNewResolvedClassPathString +=
                                    Path.Combine(sDefaultReferencesDirectory,
                                                 Path.GetFileName(sResolvedPathToSourceReference)) + ";";
                            }
                        }
                    }
                    DI.log.info(sNewResolvedClassPathString);
                    xdXmlDocument.Load(sTargetProject);
                    if (xdXmlDocument.DocumentElement.Attributes["web_context_root_path"] != null)
                        xdXmlDocument.DocumentElement.Attributes["web_context_root_path"].Value = sTargetPathToJspFiles;

                    xdXmlDocument.DocumentElement["Configuration"].Attributes["class_path"].Value =
                        sNewResolvedClassPathString;
                    xdXmlDocument.Save(sTargetProject);
                    DI.log.info("Completed copying all references to new project location");
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in copyJavaReferencesFromSourceProjectToTargetFolderAndUpdateNewProjectFile:{0}",
                             ex.Message);
            }
        }

/*
        public static List<String> calculateListOfFilesFromSourceProject_WithoutFilesFromTargetProject(String sSourceProject, String sTargetProject)
        {
            List<String> lsFilesInSourceProject = new List<string>();
            List<String> lsFilesInTargetProject = new List<string>();
            //
            //     Files.getListOfAllFilesFromDirectory(lsFilesInSourceProject,
            return null;
        }

        public static List<String> calculateListOfFilesFromProject_WithoutProvidedList(String sSourceProject, List<String> lsListOfFilesToNotInclude)
        {
            return null;
        }
        public static List<String> calculateListOfFilesFromProject_JavaAndJsp(String sSourceProject)
        {
            return null;
        }
 */

        public static List<String> calculateListOfJspFilesFromProject(String sSourceProject)
        {
            var lsListOfJsps = new List<String>();
            var xdXmlDocument = new XmlDocument();
            xdXmlDocument.Load(sSourceProject);
            // remove current Source elements
            XmlNodeList xnlSources = xdXmlDocument.GetElementsByTagName("Source");
            foreach (XmlNode xnSource in xnlSources)
                if (xnSource.Attributes["web"].Value == "true")
                {
                    var lsTempListOfJsps = new List<String>();
                    String sPathToSearch =
                        Path.GetFullPath(Path.Combine(Path.GetDirectoryName(sSourceProject),
                                                      xnSource.Attributes["path"].Value));
                    Files.getListOfAllFilesFromDirectory(lsTempListOfJsps, sPathToSearch, true, "*.jsp", false);
                    for (int i = 0; i < lsTempListOfJsps.Count; i++)
                        lsListOfJsps.Add(
                            lsTempListOfJsps[i].Replace(sPathToSearch, "").Replace(".jsp", "_jsp").Replace('\\', '.'));
                    DI.log.info(sPathToSearch);
                }
            foreach (String sJspFile in lsListOfJsps)
            {
                DI.log.info(sJspFile);
            }
            DI.log.debug("in calculateListOfJspFilesFromProject, # jsp Files found: {0}", lsListOfJsps.Count);
            return lsListOfJsps;
        }

        public static List<String> getListOfFilesFromJavaAndJspSourceDirs(String sSourcePathToJavaFiles,
                                                                          String sSourcePathToJspFiles)
        {
            var lsFilesInProject = new List<string>();
            Files.getListOfAllFilesFromDirectory(lsFilesInProject, sSourcePathToJavaFiles, true, "*.jsp", false);
            Files.getListOfAllFilesFromDirectory(lsFilesInProject, sSourcePathToJspFiles, true, "*.java", false);
            return lsFilesInProject;
        }
    }
}
