// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Controllers;
using O2.Rules.OunceLabs.DataLayer;
using O2.Scanner.MsCatNet.Scan;
using O2.Scanner.OunceLabsCLI.Scan;

namespace O2.Scanners.Ascx
{
    public partial class ascx_WillItScan : UserControl
    {
    //    private RichTextBox rtbOriginalLoggingObject;
        private IScanTarget scCurrentScanTarget;

        public ascx_WillItScan()
        {
            InitializeComponent();
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            addFileOrFolderAsScanTargets(oObject.ToString());
        }
  
        private void lbTargetFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (lbTargetFiles.SelectedItem != null)
                loadInManualTestGui((IScanTarget) lbTargetFiles.SelectedItem);
        }

        private void btCreateCirDumpForSelectedFile_Click(object sender, EventArgs e)
        {
            createCirDumpsForSelectedFile();
        }
      
        private void ascx_WillItScan_Load(object sender, EventArgs e)
        {
            onLoad();
        }

       

        private void lbTargetFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lbTargetFiles.SelectedItem != null && cbDoubleClickToDeleteItem.Checked)
            {
                lbTargetFiles.Items.Remove(lbTargetFiles.SelectedItem);
                unloadCurrentScanTarget();
            }
        }

        private void btCreateAssessmentFiles_Click(object sender, EventArgs e)
        {
            createAssessementFiles();
        }      

        private void cbSourcesAndSinks_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbEnableNoOutOfTheBoxRules_CheckedChanged(object sender, EventArgs e)
        {
            gbNoOutOfTheBoxRules.Enabled = cbEnableNoOutOfTheBoxRules.Checked;
            gbNoRulesScanMode_Manual.Enabled = cbEnableNoOutOfTheBoxRules.Enabled;
        }

        public void updateLabelWithNumberOfRulesInDatabase()
        {
            lbNumberOfRulesInDatabase.Text = Lddb_OunceV6.getNumberOfRulesInRecTable().ToString();
        }

        private void llDownloadDemoFile_HacmeBank_WebServices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            downloadDemoFile(ConfigurationManager.AppSettings["Hacmebank-WebServices-DLL"]);
        }

        private void llDownloadDemoFile_HacmeBank_Website_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            downloadDemoFile(ConfigurationManager.AppSettings["Hacmebank-WebSite-DLL"]);
        }      

        private void btScanWithExistingRules_Click(object sender, EventArgs e)
        {
            scanWithExistingRules();
        }
 
        private void tbWorkDirectory_TextChanged(object sender, EventArgs e)
        {
            if (scCurrentScanTarget != null && Directory.Exists(tbWorkDirectory.Text))
            {
                scCurrentScanTarget.useFileNameOnWorkDirecory = cbAutoAppendTargetName.Checked;
                scCurrentScanTarget.WorkDirectory = tbWorkDirectory.Text;
                adManualTestTempFiles.openDirectory(scCurrentScanTarget.WorkDirectory);
            }
        }

        private void llDeleteDirectoryContents_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cbDeleteDirectoryContentsRecursive.Checked)
                Files.deleteFolder(adManualTestTempFiles.getCurrentDirectory(),
                                   cbDeleteDirectoryContentsRecursive.Checked);
            else
                Files.deleteFilesFromDirThatMatchPattern(adManualTestTempFiles.getCurrentDirectory(), "*.*");
            Files.checkIfDirectoryExistsAndCreateIfNot(adManualTestTempFiles.getCurrentDirectory());
            adManualTestTempFiles.refreshDirectoryView();
        }

        private void llDeleteSeletedFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String sFileToDelete = adManualTestTempFiles.getSelectedItem_FullPath();
            if (File.Exists(sFileToDelete))
            {
                File.Delete(sFileToDelete);
                DI.log.debug("Deleted File: {0}", sFileToDelete);
                adManualTestTempFiles.refreshDirectoryView();
            }
            else
                DI.log.error("Item selected is not a file : {0}", sFileToDelete);
        }

        private void ado_AddFilesOrDirectoryToScanBundle_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            String sItemToLoad = oObject.ToString();
            processTargetFilesDroppedItem(sItemToLoad);
        }
      
        private void llPreCirDumps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (scCurrentScanTarget != null)
            {
                String sPathToCirDumpFiles =
                    Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(scCurrentScanTarget.WorkDirectory,
                                                                            "_CirDumps"));
                CirDumps.preCirDumpGeneration(sPathToCirDumpFiles);
            }
        }

        private void llPostCirDumps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (scCurrentScanTarget != null)
            {
                String sPathToCirDumpFiles =
                    Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(scCurrentScanTarget.WorkDirectory,
                                                                            "_CirDumps"));
                CirDumps.postCirDumpGeneration(sPathToCirDumpFiles);
            }
        }

        private void llRefreshDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adManualTestTempFiles.refreshDirectoryView();
        }

        private void llCreateCirData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            createCirDataViaLabelLink();
        }
        
        private void llRefreshRulesNumber_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            updateLabelWithNumberOfRulesInDatabase();
        }

        private void llDeleteDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Lddb_OunceV6.action_DeleteAllRules();
        }

        private void btCreateCirDumpWithExistingRules_Click(object sender, EventArgs e)
        {
            setStatusOfActionButtons(false);
            //      redirectLogToCustomLogViewer();
            CirDumps.createCirDump(scCurrentScanTarget, scanCompleted_Callback, logCallback,false,
                                   cbStoreControlFlowBlockRawDataInsideCirDataFile.Checked);
        }

        private void btCreateO2RulePacks_Click(object sender, EventArgs e)
        {
            createRulePacks();
        }
       
        private void llTargetFiles_DeleteSelected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lbTargetFiles.SelectedItem != null)
            {
                lbTargetFiles.Items.Remove(lbTargetFiles.SelectedItem);
                unloadCurrentScanTarget();
            }
        }

        private void adManualTestTempFiles_eDirectoryEvent_DoubleClick(string sValue)
        {
            //if (Path.GetExtension(sValue) == ".xml")

            //  HKEY_USERS\S-1-5-21-817711196-3176212830-1286862028-500\Software\Microsoft\Windows\ShellNoRoam\MUICache
            // var sPathToSearchAssessmentRun =  WinRegistry.getKeyValue_Users(@"\Software\Microsoft\Windows\ShellNoRoam\MUICache", "SearchAssessmentRun");
        }

        private void lbTargetFiles_DragDrop(object sender, DragEventArgs e)
        {
            addFileOrFolderAsScanTargets(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

        private void lbTargetFiles_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void btCancelOunceCLIScan_Click(object sender, EventArgs e)
        {
            cancelCurrentOunceCLIScan();
        }
       
    }
}
