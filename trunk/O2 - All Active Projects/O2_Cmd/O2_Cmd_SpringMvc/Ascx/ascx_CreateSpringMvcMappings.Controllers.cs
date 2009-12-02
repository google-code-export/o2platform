using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Classes;
using O2.Cmd.SpringMvc.Objects;
using O2.Cmd.SpringMvc.PythonScripts;
using O2.Core.CIR.CirCreator.Java;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Cmd.SpringMvc.Ascx
{
    public partial class ascx_CreateSpringMvcMappings
    {
        private bool runOnLoad = true;                
        //public ICirData cirData;
        public string loadedCirDataFile;                        

        public void onLoad()
        {
            if (!DesignMode && runOnLoad)
            {                
                runOnLoad = false;
                springMvcMappings._onTreeViewSelect += tvControllers_AfterSelect;
                tbFolderToSaveMappedMvcControllers.Text = DI.config.O2TempDir;
            }
        }

        /*private void handleDrop(DragEventArgs e)
        {
            O2Thread.mtaThread(
                ()=>
            {
                var fileOrFolder = Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);

                if (false == loadCirDataFile(fileOrFolder))
                    // if (false == loadReferenceFindings(fileOrFolder))
                    loadFileOrFolder(fileOrFolder);
            });
        }*/

        private void handleDropOnDropControl(object oObject, bool processJarFiles, bool deleteTempFiles)
        {
            var workingOnTaskFormName = "Running Spring MVC (Annotations) Analysis Engine";
            O2AscxGUI.workingOnTaskForm_open(workingOnTaskFormName);
            
            O2Thread.mtaThread(
                () =>
                    {
                        Processes.Sleep(500);
                        try
                        {
                            O2AscxGUI.workingOnTaskForm_setText(workingOnTaskFormName, "Prepare files for Analysis (unzip zip, jars, etc..)");
                            var pythonStringTargetFileOrFolder = AnnotationsHelper.getPythonStringTargetFileOrFolder(oObject.ToString(), processJarFiles);
                            //var tempFolder = DI.config.getTempFolderInTempDirectory("unzipedDroppedZip");
                            O2AscxGUI.workingOnTaskForm_setText(workingOnTaskFormName, "Converting files (using Jyhton)");
                            var tempFolderForAnnotationsXmlFiles = AnnotationsHelper.createAnnotationsXmlFilesFromJavaClassFileOrFolder(pythonStringTargetFileOrFolder);
                            var javaXmlFilesToProcess = AnnotationsHelper.calculateFilesToProcess(oObject.ToString(), tempFolderForAnnotationsXmlFiles);
                            O2AscxGUI.workingOnTaskForm_setText(workingOnTaskFormName, "Creating CirData");
                            var cirData = createCirData(javaXmlFilesToProcess);
                            O2AscxGUI.workingOnTaskForm_setText(workingOnTaskFormName, "Mapping Spring Mvc Controllers");

                            var springMvcControllers = createSpringMvcControlersObjectsFromXmlFiles(javaXmlFilesToProcess);

                            springMvcMappings.loadMappedControllers(cirData, springMvcControllers);
                            //showSpringMvcControllers(springMvcControllers);
                            if (deleteTempFiles)
                            {
                                Files.deleteFolder(tempFolderForAnnotationsXmlFiles, true);
                                Files.deleteFolder(pythonStringTargetFileOrFolder, true);
                            }
                            else
                            {
                                DI.log.info(
                                    "Temp files were not deleted:   \n    tempFolderForAnnotationsXmlFiles:{0}\n    pythonStringTargetFileOrFolder:{1} ",
                                    tempFolderForAnnotationsXmlFiles, pythonStringTargetFileOrFolder);
                            }
                            O2AscxGUI.workingOnTaskForm_close(workingOnTaskFormName);                         
                        }
                        catch (Exception ex)
                        {
                            DI.log.error("in handleDropOnDropControl: {0}", ex.Message);
                        }
                    });
        }
        

      
        public List<SpringMvcController> createSpringMvcControlersObjectsFromXmlFiles(List<String> filesToProcess)
        {
            var springMvcControllers = new List<SpringMvcController>();
            foreach (var file in filesToProcess)
                switch (Path.GetExtension(file).ToLower())
                {
                    case ".xml":
                        springMvcControllers.AddRange(
                            LoadSpringMvcData.createSpringMvcControllersFromXmlAttributeFile(file));
                        break;
                        //case ".cirdata":
                        //    loadCirDataFile(file);
                        //    break;
                    default:
                        break;
                }
            return springMvcControllers;
        }


        /*    private bool loadCirDataFile(string fileOrFolder)
            {
                if (File.Exists(fileOrFolder) && Path.GetExtension(fileOrFolder) == ".CirData")
                {
                    loadedCirDataFile = "";
                    springMvcMappings.cirData = null;
                    {
                        var loadedCirDataObject = CirLoad.loadFile(fileOrFolder);
                        if (loadedCirDataObject != null)
                        {
                            loadedCirDataFile = fileOrFolder;
                            springMvcMappings.cirData = loadedCirDataObject;
                        }
                    }
                    setLoadedCirDataFile();
                    return true;
                }
            
                return false;
            }

            private void setLoadedCirDataFile()
            {
                this.invokeOnThread(() => lbLoadedCirDataFile.Text = loadedCirDataFile);
            }
            */
        /* public void loadFileOrFolder(string fileOrFolder)
         {
             //var workingOnTaskForm = O2AscxGUI.openWorkingOnTaskForm("Running Jython Script to map Java class files");
             var tempFolderForAnnotationsXmlFiles = AnnotationsHelper.createAnnotationsXmlFilesFromJavaClassFileOrFolder(fileOrFolder);
             this.invokeOnThread(
                 () =>
                     {
                         foreach (var file in Files.getFilesFromDir_returnFullPath(tempFolderForAnnotationsXmlFiles))
                             lboxClassFilesAnalysed.Items.Add(file);
                         convertXmlAttributeFilesToSpringMvcControllersObjects(tempFolderForAnnotationsXmlFiles);
               //          if (workingOnTaskForm != null)
               //              workingOnTaskForm.close();
                     });
         }

         public void convertXmlAttributeFilesToSpringMvcControllersObjects(string tempFolderForAnnotationsXmlFiles)
         {
             var springMvcControllers = new List<SpringMvcController>();
             foreach (var xmlAttributeFile in Files.getFilesFromDir_returnFullPath(tempFolderForAnnotationsXmlFiles))            
                 springMvcControllers.AddRange(LoadSpringMvcData.createSpringMvcControllersFromXmlAttributeFile(xmlAttributeFile));
             showSpringMvcControllers(springMvcControllers);
         }*/

      

       

        

        private void showDetailsForSpringMvcController(SpringMvcController springMvcController)
        {
            if (springMvcController != null)
            {
                //var methodSignature = string.Format("{0}.{1}", springMvcController.JavaClass,springMvcController.JavaMethod);
                var methodSignature = springMvcController.JavaClassAndFunction;
                if (springMvcMappings.cirData != null && springMvcMappings.cirData.dFunctions_bySignature.ContainsKey(methodSignature))
                {
                    cirFunctionDetails.viewCirFunction(springMvcMappings.cirData.dFunctions_bySignature[methodSignature]);
                }
                else
                    DI.log.error("in showDetailsForSpringMvcController, loaded cirData did not contained signature :{0}", methodSignature);
                // load sourceCode file
                sourceCodeView.gotoLine(springMvcController.FileName, (int)springMvcController.LineNumber - 1);

                showFindingsDetailsForSpringMvcController(springMvcController.JavaClassAndFunction);

                //O2Messages.fileOrFolderSelected(springMvcController.FileName, (int)springMvcController.LineNumber);
            }
        }

        

        

        /*public bool loadReferenceFindings(string ozasmtFileWithReferenceFindings)
        {
            
            if (File.Exists(ozasmtFileWithReferenceFindings) && Path.GetExtension(ozasmtFileWithReferenceFindings).ToLower() == ".ozasmt")
            {
                O2Thread.mtaThread(
                    () =>
                        {
                            var thread =
                                findingsViewerfor_ReferenceFindings.loadO2Assessment(ozasmtFileWithReferenceFindings);
                            thread.Join();
                            this.invokeOnThread(
                                () =>
                                    {
                                        lbNumberOfReferenceFindingsLoaded.Text =
                                            findingsViewerfor_ReferenceFindings.currentO2Findings.Count.ToString();
                                        mapFindingsToSourceCode(findingsViewerfor_ReferenceFindings.currentO2Findings, dgvfunctionsMappings);
                                    });
                        });
                return true;
            }
            return false;
        }*/

        /*public void mapFindingsToSourceCode()
        {
            this.invokeOnThread(() =>mapFindingsToSourceCode(findingsViewerfor_ReferenceFindings.currentO2Findings, dgvfunctionsMappings));
        }

        public void mapFindingsToSourceCode(List<IO2Finding> o2FindingsToMap,DataGridView targetDataGridView)
        {
            DI.log.info("Mapping findings to Source Code");
            var o2time = new O2Timer("Compled mapping").start();
            var mappedMethodsToSourceCode = new Dictionary<string, string>();                        
            foreach (var o2Finding in o2FindingsToMap)
            {
                addFindingToMappedSourceCode(o2Finding, mappedMethodsToSourceCode);
            }
            showMethodsMappedToSourceCodeInDataGridView(mappedMethodsToSourceCode, targetDataGridView);
            o2time.stop();
        }*/

        public void addFindingToMappedSourceCode(IO2Finding o2Finding, Dictionary<string, string> mappedMethodsToSourceCode)
        {
            var findingUniqueTraces = ((O2Finding) o2Finding).getUniqueTraces();
            foreach (O2Trace o2Trace in findingUniqueTraces)
                if (doesSignatureRepresentAMethodOnTheFilenameReference(o2Trace.signature, o2Trace.SourceCode))
                {

                    string sourceCodeReference = string.Format("{0}::{1}", o2Finding.file, o2Finding.lineNumber);

                    if (!mappedMethodsToSourceCode.ContainsKey(o2Finding.vulnName))
                        mappedMethodsToSourceCode.Add(o2Finding.vulnName, sourceCodeReference);
                    else if (mappedMethodsToSourceCode[o2Finding.vulnName] != sourceCodeReference)
                    {
                    }
                }
        }

        public bool doesSignatureRepresentAMethodOnTheFilenameReference(string signature, string sourceCodeLine)
        {
            if (sourceCodeLine == "")
                return false;
            var publicOrPrivate = signature.Trim().ToLower().Substring(0, 6);
            DI.log.info(" {0}   -  '{1}'", publicOrPrivate, sourceCodeLine);
            if (publicOrPrivate == "public" || publicOrPrivate == "privat")
                return true;
            
            //var filteredSignature = new FilteredSignature(o2Finding.vulnName
            return false;
        }

        public static void showMethodsMappedToSourceCodeInDataGridView(Dictionary<string, string> mappedMethodsToSourceCode, DataGridView targetDataGridView)
        {
            targetDataGridView.Columns.Clear();
            O2Forms.addToDataGridView_Column(targetDataGridView, "method",-1);
            O2Forms.addToDataGridView_Column(targetDataGridView, "Filename", -1);
            O2Forms.addToDataGridView_Column(targetDataGridView, "Line", -1);
            foreach(var mapping in mappedMethodsToSourceCode)
            {
                O2Forms.addToDataGridView_Row(targetDataGridView, null, new object[] {mapping.Key, mapping.Value});               
            }
        }

        private ICirData createCirData(List<string> filesToProcess)
        {
            var cirDataAnalysis = new CirDataAnalysis();
            foreach (var fileToProcess in filesToProcess)
            {
                if (fileToProcess != "" && Path.GetExtension(fileToProcess) == ".xml")
                {
                    var tempCirData = CirFactory.createCirDataFromXmlFileWithJavaMetadata(fileToProcess);
                    CirDataAnalysisUtils.addO2CirDataFile(cirDataAnalysis, tempCirData, false /* runRemapXrefs*/);
                }
            }
            CirDataAnalysisUtils.remapXrefs(cirDataAnalysis);
            CirViewingUtils.openCirDataFileInCirViewerControl(cirDataAnalysis, "Cir Viewer");
            return CirDataAnalysisUtils.createCirDataFromCirDataAnalysis(cirDataAnalysis);            
        }


        // map this to a callback
        private void tvControllers_AfterSelect(TreeView tvControllers)
        {
            tvControllers.invokeOnThread(
                () =>
                    {
                        if (tvControllers.SelectedNode != null && tvControllers.SelectedNode.Tag != null)
                        {
                            if (tvControllers.SelectedNode.Tag is SpringMvcController)
                            {
                                showDetailsForSpringMvcController((SpringMvcController) tvControllers.SelectedNode.Tag);                                
                            }
                            else if (tvControllers.SelectedNode.Tag is ICirClass)
                            {
                                var cirClass = (ICirClass) tvControllers.SelectedNode.Tag;
                                cirFunctionDetails.viewCirClass(cirClass);
                                springMvcAutoBindClassesView.showClass(cirClass,springMvcMappings.cirData);
                                showFindingsDetailsForSpringMvcController(cirClass.Signature);
                            }

                            tvControllers.Focus();
                        }
                    });    
        }

        public void showFindingsDetailsForSpringMvcController(string signatureToFilter)
        {
            findingsViewerFor_SelectedController.setFilter1Value("Source");
            findingsViewerFor_SelectedController.setFilter2Value("Sink");
            findingsViewerFor_SelectedController.setFilter1TextValue(signatureToFilter, true);
            findingsViewerFor_SelectedController.expandAllNodes();
        }


    }
}
