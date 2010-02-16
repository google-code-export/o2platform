// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Rules.OunceLabs.DataLayer;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;
using O2.Views.ASCX.classes;

namespace O2.Tool.DotNetCallbacksMaker.ascx
{
    public partial class ascx_dotNetCallbacksMaker : UserControl
    {
        public static UInt32 uDbId = 3;

        public ascx_dotNetCallbacksMaker()
        {
            InitializeComponent();
            if (false == DesignMode)
            {
                adDirectory._FileFilter = "*.dll";
                refreshDgbWithExistingCallbacks();
            }
        }

        private void btLoadListOfCallbacksInLddbDatabase_Click(object sender, EventArgs e)
        {
            refreshDgbWithExistingCallbacks();
        }

        private void adDirectory_eDirectoryEvent_Click(string sValue)
        {
        }

        private void adDirectory_eDirectoryEvent_DoubleClick(string sValue)
        {
            if (false == Directory.Exists(sValue))
                lbFilesToSearchCallbackOn.Items.Add(sValue);
        }

        private void btDeleteFile_Click(object sender, EventArgs e)
        {
            lbFilesToSearchCallbackOn.Items.Clear();
        }

        private void dgvCallbacksInLddbDatabase_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        public void refreshDgbWithExistingCallbacks()
        {
            OunceMySql.populateDataGridView_ExistentCallbacks(dgvCallbacksInLddbDatabase);
        }

        private void btAddMethodToList_Click(object sender, EventArgs e)
        {
        }

        private void btAddAllMethodsToList_Click(object sender, EventArgs e)
        {
        }

        private void btRemoveMethodsFromLIst_Click(object sender, EventArgs e)
        {
        }

        private void btProcessFilesAndCalculateTargets_Click(object sender, EventArgs e)
        {
            processFilesInListBoxAndPopulateListBoxWithTargets();
        }

        private void btAddSelectedMethodsAsCallbacks_Click(object sender, EventArgs e)
        {
            var methodsToAddAsCallbacks = new List<String>();
            if (lbListOfPotentialTargets.SelectedItems.Count > 0)
                foreach (String sMethod in lbListOfPotentialTargets.SelectedItems)
                    methodsToAddAsCallbacks.Add(sMethod);
            makeMethodsCallbacks(methodsToAddAsCallbacks);
                    
        }

        

        private void lbFilesToSearchCallbackOn_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void processFilesInListBoxAndPopulateListBoxWithTargets()
        {
            var lsFilesToProcess = new List<string>();
            foreach (String sFile in lbFilesToSearchCallbackOn.Items)
                lsFilesToProcess.Add(sFile);

            OunceCallbacks.SearchMode smSearchMode = OunceCallbacks.SearchMode.WebMethods;
            if (rbDotNet_SearchForPublicMethods.Checked)
                smSearchMode = OunceCallbacks.SearchMode.PublicMethods;
            //ounceCallbacks.searchMode smSearchMode = ounceCallbacks.searchMode.PublicMethods;

            List<String> lsPotentialTargets = OunceCallbacks.DotNet.calculateListOfMethodsFromFiles(lsFilesToProcess,
                                                                                                    smSearchMode);
            lbListOfPotentialTargets.Items.Clear();
            foreach (String sPotentialTarget in lsPotentialTargets)
                lbListOfPotentialTargets.Items.Add(sPotentialTarget);
            lbListOfPotentialTargets.Sorted = true;
        }

        private void btDeleteSelectedCallbaks_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rSelectedRow in dgvCallbacksInLddbDatabase.SelectedRows)
            {
                String sSignatureOfCallbackToDelete =
                    Encoding.ASCII.GetString((byte[]) rSelectedRow.Cells["signature"].Value);
                var uCallbackDbId = (UInt32) rSelectedRow.Cells["db_id"].Value;
                Lddb_OunceV6.action_DeleteCallback(uCallbackDbId, sSignatureOfCallbackToDelete);
            }
            refreshDgbWithExistingCallbacks();
        }

        private void btDeleteAllCallbacks_Click(object sender, EventArgs e)
        {
            Lddb_OunceV6.action_DeleteAllCallbacks();
            refreshDgbWithExistingCallbacks();
        }

        private void ascx_dotNetCallbacksMaker_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
                scPotentialTargets.Panel2Collapsed = true;
        }

        private void lbFilesToSearchCallbackOn_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lbFilesToSearchCallbackOn_DragDrop(object sender, DragEventArgs e)
        {
            var file = Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);
            if (File.Exists(file))
                lbFilesToSearchCallbackOn.Items.Add(file);
        }

        private void btAddAllMethodsAsCallbacks_Click(object sender, EventArgs e)
        {            
            var methodsToAddAsCallbacks = new List<String>();
            foreach (String sMethod in lbListOfPotentialTargets.Items)
                methodsToAddAsCallbacks.Add(sMethod);
            makeMethodsCallbacks(methodsToAddAsCallbacks);
        }

        private void makeMethodsCallbacks(List<string> methodsToAddAsCallbacks)
        {
            pbAddingMethodsProgressBar.Maximum = methodsToAddAsCallbacks.Count;
            pbAddingMethodsProgressBar.Value = 0;

            foreach (var methodToAdd in methodsToAddAsCallbacks)
            {
                Lddb_OunceV6.action_makeMethodACallback(uDbId, methodToAdd);
                pbAddingMethodsProgressBar.Value++;
            }
        }

        private void llDownloadDemoFile_HacmeBank_WebServices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            downloadDemoFile(ConfigurationManager.AppSettings["Hacmebank-WebServices-DLL"]);
        }

        public void downloadDemoFile(String sFileToDownload)
        {
            if (string.IsNullOrEmpty(sFileToDownload))
                DI.log.error("in downloadDemoFile: No file provided");
            else
            {
                string sTargetFile = Path.Combine(DI.config.O2TempDir, Path.GetFileName(sFileToDownload));
                WebRequests.downloadFileUsingAscxDownload(sFileToDownload, sTargetFile,
                    downloadedFile =>
                        {
                            if (File.Exists(downloadedFile))
                                lbFilesToSearchCallbackOn.invokeOnThread(() => lbFilesToSearchCallbackOn.Items.Add(downloadedFile));                            
                        });
            }
        }
    }
}
