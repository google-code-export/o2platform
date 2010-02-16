// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;
using O2.Interfaces.Tasks;
using O2.Views.ASCX.classes;
using O2.Views.ASCX.classes.Tasks;
using O2.Views.ASCX.classes.TasksWrappers;
using O2.Views.ASCX.Tasks;

namespace O2.Tool.FilterAssessmentFiles.Ascx
{
    public partial class ascx_FilterWizard : UserControl
    {
        public string defaultQuery = "from O2Finding f in o2Findings select new {f.vulnType, f.vulnName}";
        public string defaultQuery1 = "from O2Finding finding in o2Findings select finding.vulnName";

        public List<IO2Assessment> importedO2AssessmentFiles = new List<IO2Assessment>();
        public int maxNumberOfNLinqQueryRecordsToShow = 2000;

        public ascx_FilterWizard()
        {
            InitializeComponent();
        }


        public void importFileOrFolder(string fileOrFolder)
        {
            if (!String.IsNullOrEmpty(fileOrFolder))
            {
                if (Directory.Exists(fileOrFolder))
                    foreach (string file in Files.getFilesFromDir_returnFullPath(fileOrFolder))
                        importFile(file);
                else
                    importFile(fileOrFolder);
            }
        }

        public void importFile(string file)
        {
            if (File.Exists(file))
                if (Path.GetExtension(file) == ".zip")
                {
                    TaskUtils.executeTask(new Task_Unzip(file, DI.config.TempFolderInTempDirectory),
                                          taskControlHost_ExecutionQueue,
                                          resultObject =>
                                              {
                                                  if (resultObject is List<string>)
                                                      foreach (string item in (List<string>) resultObject)
                                                          importFileOrFolder(item);
                                              });
                    /*
                    string tempFolder = DI.config.getTempFolderInTempDirectory();

                    zipUtils.unzipFile(file, tempFolder);
                    foreach (string unzipedFile in Files.getFilesFromDir_returnFullPath(tempFolder))
                        importFile(unzipedFile);
                    //   Files.deleteFolder(tempFolder);*/
                }
                else if (Path.GetExtension(file) == ".ozasmt")
                {
                    //importedFiles.Add(file);
                    addTask_ImportFile(file, taskControlHost_ExecutionQueue);
                    refreshTreeViewWithCurrentImportedFiles();
                }

            setDragAndDropHelpText(false);
        }

        private void setDragAndDropHelpText(bool value)
        {
            if (lbDragAndDropHelpText.InvokeRequired)
                lbDragAndDropHelpText.Invoke(new EventHandler(delegate { setDragAndDropHelpText(value); }));
            else
                lbDragAndDropHelpText.Visible = value;
        }

        private void refreshTreeViewWithCurrentImportedFiles()
        {
            if (tvImportedFiles.InvokeRequired)
                tvImportedFiles.Invoke(new EventHandler(delegate { refreshTreeViewWithCurrentImportedFiles(); }));
            else
            {
                try
                {
                    tvImportedFiles.Nodes.Clear();
                    foreach (O2Assessment o2Assessment in importedO2AssessmentFiles)
                    {
                        addAssessmentToTreeview(tvImportedFiles, o2Assessment, true);
                    }
                }
                catch (Exception ex)
                {
                     DI.log.ex(ex, "refreshTreeViewWithCurrentImportedFiles", false);
                }
            }
        }

        public static void addAssessmentToTreeview(TreeView treeView, O2Assessment o2Assessment, bool showStats)
        {
            TreeNode newNode = O2Forms.newTreeNode(o2Assessment.name, o2Assessment.name, 0, o2Assessment);
            if (showStats)
                OzasmtStats.populateTreeNodeWithAssessmentStats(newNode, o2Assessment, 1);
            treeView.Nodes.Add(newNode);
        }

        private void btStep1_Click(object sender, EventArgs e)
        {
            setActiveStep(0);
        }

        private void btStep2_Click(object sender, EventArgs e)
        {
            setActiveStep(1);
        }

        private void btStep3_Click(object sender, EventArgs e)
        {
            setActiveStep(2);
        }

        private void btStep4_Click(object sender, EventArgs e)
        {
            setActiveStep(3);
        }

        private void btStep1_Next_Click(object sender, EventArgs e)
        {
            setActiveStep(1);
        }

        private void btStep2_Next_Click(object sender, EventArgs e)
        {
            setActiveStep(2);
        }

        private void btStep2_Prev_Click(object sender, EventArgs e)
        {
            setActiveStep(0);
        }

        private void btStep3_Prev_Click(object sender, EventArgs e)
        {
            setActiveStep(1);
        }

        private void btStep3_Next_Click(object sender, EventArgs e)
        {
            setActiveStep(3);
        }

        private void btStep4_Prev_Click(object sender, EventArgs e)
        {
            setActiveStep(2);
        }

        public void setActiveStep(int step)
        {
            //tabControlWithSteps.TabIndex = step;
            tabControlWithSteps.SelectedIndex = step;
        }

        private void llDownloadDemoFile_HacmeBank_WebServices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebRequests.downloadFileUsingAscxDownload(O2CoreResources.DemoOzasmtFile_Hacmebank_WebServices,
                                                      downloadDemoFileCallback);
        }

        private void llDownloadDemoFile_HacmeBank_Website_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebRequests.downloadFileUsingAscxDownload(O2CoreResources.DemoOzasmtFile_Hacmebank_WebSite,
                                                      downloadDemoFileCallback);
        }

        private void llDownloadDemoFile_WebGoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebRequests.downloadFileUsingAscxDownload(O2CoreResources.DemoOzasmtFile_Hacmebank_WebGoat,
                                                      downloadDemoFileCallback);
        }

        private void downloadDemoFileCallback(String sPathToDownloadedFile)
        {
            O2Forms.executeMethodThreadSafe(this, this, "importFile",
                                            new object[] {sPathToDownloadedFile});
        }

        public void loadAllXmlAssessmentFilesFromDirectory(String sbLoadAllXmlFilesFromDir)
        {
            clearImportedFiles();
            //tvImportedFiles.Nodes.Clear();
            var lsFilesToLoad = new List<string>();
            Files.getListOfAllFilesFromDirectory(lsFilesToLoad, sbLoadAllXmlFilesFromDir, false, "*.xml", false);
            Files.getListOfAllFilesFromDirectory(lsFilesToLoad, sbLoadAllXmlFilesFromDir, false, "*.ozasmt", false);
             DI.log.debug("in loadAllXmlAssessmentFilesFromDirectory: there {0} files to process", lsFilesToLoad.Count);
            foreach (String sFileToLoad in lsFilesToLoad)
                importFile(sFileToLoad);
        }

        private void llClearImportFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearImportedFiles();
        }

        private void clearImportedFiles()
        {
            importedO2AssessmentFiles.Clear();
            refreshTreeViewWithCurrentImportedFiles();
        }

        private void llDownloadDemoFile_FromTempDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadAllXmlAssessmentFilesFromDirectory(DI.config.O2TempDir);
        }

        private void tvImportedFiles_DragDrop(object sender, DragEventArgs e)
        {
            importFileOrFolder(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

        private void tvImportedFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tvImportedFiles_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //  tvImportedFiles.SelectedNode = (TreeNode)e.Item;
            tvImportedFiles.SelectedNode = (TreeNode) e.Item;
            if (tvImportedFiles.SelectedNode.Tag != null)
            {
                DoDragDrop(tvImportedFiles.SelectedNode.Tag, DragDropEffects.Copy);
            }
        }


        private void ascx_FilterWizard_Load(object sender, EventArgs e)
        {
            llDownloadDemoFile_HacmeBank_WebServices_LinkClicked(null, null);
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            refreshTreeViewWithCurrentImportedFiles();
            // foreach (var o2Assessment in importedO2AssessmentFiles)
            //     addTask_ImportFile(o2Assessment, flowLayoutPanel1);

            //addTask_ImportFiles(importedFiles, flowLayoutPanel1);

            //newTask.taskThread.start();
        }

        private void addTask_ImportFile(string fileToImport, Control flowLayoutPanel)
        {
            var newTask = new Task_LoadAssessmentFiles(new O2AssessmentLoad_OunceV6(), fileToImport);
            newTask.onTaskStatusChange += (task, taskStatus) =>
                                              {
                                                  if (taskStatus == TaskStatus.Completed_OK &&
                                                      task.resultsObject is O2Assessment)
                                                  {
                                                      importedO2AssessmentFiles.Add(
                                                          (O2Assessment) task.resultsObject);
                                                      refreshTreeViewWithCurrentImportedFiles();
                                                  }
                                              };
            addTaskToControlAndStartIt(newTask, flowLayoutPanel);
        }

        /*void task_onTaskExecutionCompletion(ITask task, TaskStatus taskStatus)
        {
            if (taskStatus == TaskStatus.Completed_OK && task.resultsObject is O2Assessment)
                importedO2AssessmentFiles.Add((O2Assessment)task.resultsObject);
         
        }*/

        //    addTask_ImportFiles(new List<string>(new[] { fileToImport }), flowLayoutPanel);


        /*private static void addTask_ImportFiles(List<string> filesToImport, Control flowLayoutPanel)
        {
            var newTask = new core.Ascx.CoreControls.ascx_Task(new Task_LoadAssessmentFiles(filesToImport));
            
            flowLayoutPanel.Controls.Add(newTask);
            newTask.startTask();
        }*/

        private static void addTaskToControlAndStartIt(ITask task, Control targetControl)
        {
            if (targetControl.InvokeRequired)
                targetControl.Invoke(new EventHandler(delegate { addTaskToControlAndStartIt(task, targetControl); }));
            else
            {
                var taskControl = new ascx_Task(new TaskThread(task));
                targetControl.Controls.Add(taskControl);
                taskControl.startTask();
            }
        }

        private void lbDragAndDropHelpText_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void lbDragAndDropHelpText_DragDrop(object sender, DragEventArgs e)
        {
            tvImportedFiles_DragDrop(sender, e);
        }


        private void btNLinqQuery_ConsolidateFindings_Click(object sender, EventArgs e)
        {
            lbNLinqQuery_NumberOfFindingsObjectsLoaded.Text = getListOfAllLoadedFindings().Count.ToString();
        }

        public List<IO2Finding> getListOfAllLoadedFindings()
        {
            var allFindings = new List<IO2Finding>();
            foreach (IO2Assessment o2Assessment in importedO2AssessmentFiles)
                allFindings.AddRange(o2Assessment.o2Findings);
            return allFindings;
        }


        private void btCreateFilters_Click(object sender, EventArgs e)
        {
        }

        private void tvImportedFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }
    }
}
