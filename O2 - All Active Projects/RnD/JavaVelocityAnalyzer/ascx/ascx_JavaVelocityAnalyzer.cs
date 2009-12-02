using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Rnd.JavaVelocityAnalyzer.classes;
using O2.Views.ASCX.O2Findings;

namespace O2.Rnd.JavaVelocityAnalyzer.ascx
{
    public partial class ascx_JavaVelocityAnalyzer : UserControl
    {
        private readonly ConsolidatedProcessedVelocityFiles cpvfVelocityFiles = new ConsolidatedProcessedVelocityFiles();

        public ascx_JavaVelocityAnalyzer()
        {
            InitializeComponent();
        }

        private void ascx_JavaVelocityAnalyzer_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                ascx_DropObject1.setText("Velocity Files");
                //        btLoadTestData_Click(null, null);                  
            }
        }

        private void btLoadTestData_Click(object sender, EventArgs e)
        {
            //DebugMsg.bLogCache = true;
            lbLoadedFiles.Items.Clear();
            String sTestFiles = @"E:\OunceWork\VM_templates";
            loadFileOrDirectory(sTestFiles);
            lbLoadedFiles.SelectedIndex = 0;
            //DebugMsg.bLogCache = false;
        }

        public void loadFileOrDirectory(String sFileOrDirectory)
        {
            var lsFilesToLoad = new List<string>();
            String sDirectory = "";
            if (Directory.Exists(sFileOrDirectory))
            {
                lsFilesToLoad = velocityloader.getProcessedVelocityFilesFromFolder(sFileOrDirectory, true);
                sDirectory = sFileOrDirectory;
            }
            if (File.Exists(sFileOrDirectory) && velocityloader.isFileAVelocityProcessedFile(sFileOrDirectory))
            {
                lsFilesToLoad.Add(sFileOrDirectory);
                sDirectory = Path.GetDirectoryName(sFileOrDirectory);
            }
            cpvfVelocityFiles.addProcessedVelocityFiles(lsFilesToLoad, sDirectory);
            lbLoadedFiles.Items.Clear();
            lbLoadedFiles.Items.AddRange(cpvfVelocityFiles.getListWithProcessedLoadedFilesObjects().ToArray());
        }

        private void lbLoadedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLoadedFiles.SelectedItem != null)
            {
                var pvfFile = (ProcessedVelocityFile) lbLoadedFiles.SelectedItem;
                velocityAnalyzer.showProcessedVelocityFileInTreeView(pvfFile, tvProcessedVelocityFile,
                                                                     cbIgnoreComments.Checked);
                tbOriginalFile.Text = Files.getFileContents(pvfFile.sFullPathToOriginalFile);
                clearListBoxAndPopulateWithList(lbVars, pvfFile.getVars(), false);
                clearListBoxAndPopulateWithList(lbMethods, pvfFile.getFunctions(), false);
                clearListBoxAndPopulateWithList(lbDirectives, pvfFile.getDirectives(), false);
                clearListBoxAndPopulateWithList(lbCallsToOtherVmFiles, pvfFile.getReferencesToOtherVmFiles(), false);
                clearListBoxAndPopulateWithList(lbCompleteListOfVars, cpvfVelocityFiles.getCompleteListOfVars(), true);
                clearListBoxAndPopulateWithList(lbCompleteListOfMethods, cpvfVelocityFiles.getCompleteListOfMethods(),
                                                true);
            }
        }

        private void clearListBoxAndPopulateWithList(ListBox lbTargetListBox, List<String> lsData, bool bSort)
        {
            lbTargetListBox.Items.Clear();
            lbTargetListBox.Items.AddRange(lsData.ToArray());
            lbTargetListBox.Sorted = bSort;
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            loadFileOrDirectory(oObject.ToString());
        }

        private void lbLoadedFiles_DoubleClick(object sender, EventArgs e)
        {
            if (cbDoubleClickToRemoveFile.Checked)
            {
                //String sItemToRemove = this.cpvfVelocityFiles lbLoadedFiles.SelectedItem.ToString();
                cpvfVelocityFiles.removeProcessedVelocityFile(
                    ((ProcessedVelocityFile) lbLoadedFiles.SelectedItem).sFullPathToProcessedFile);
                lbLoadedFiles.Items.Remove(lbLoadedFiles.SelectedItem);
            }
        }

        private void btCreateFindingsFromVMFiles_Click(object sender, EventArgs e)
        {
            String sNewAssessmentFile = findingsCreator.createFindingsFromVMFiles(cpvfVelocityFiles, ascx_TraceViewer1);
            ascx_FindingsViewer.openInFloatWindow(sNewAssessmentFile);
            //ascx_ViewAssessmentRun1.loadAssessmentRunXmlFile(sNewAssessmentFile);
        }
    }
}   