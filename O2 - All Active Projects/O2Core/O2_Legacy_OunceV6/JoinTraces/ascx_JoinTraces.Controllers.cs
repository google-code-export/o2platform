using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.CIR;
using O2.Legacy.OunceV6.JoinTraces.classes;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Legacy.OunceV6.TraceViewer;

namespace O2.Legacy.OunceV6.JoinTraces
{
    public partial class ascx_JoinTraces
    {
        // Make these generic (since it exists on both ascx)
        
        public ICirData cdCirData = new CirData();
        private Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock = new Dictionary<string, O2TraceBlock_OunceV6>();        
        public int iMaxNumberOfCallsInOneTrace = 52; // use this to control massive recursive calls
        public int iMaxRepeatedCallsInPreviousTraces = 32; // use this to control massive recursive calls
        public TreeView tvRawData;
        public TreeView tvSinksData;
        public TreeView tvSourcesData;

        private void handleDrop(object oObject)
        {
            lbTargetSavedAssessmentFiles.Enabled = false;
            btProcessLoadedFiles.Enabled = false;
            String sItemToLoad = oObject.ToString();
            if (Directory.Exists(sItemToLoad))
            {
                foreach (String sFile in Files.getFilesFromDir_returnFullPath(sItemToLoad))
                {
                    if (cbLoadCirDumpOnFolderDrop.Checked || Path.GetExtension(sFile) != ".CirData")
                        LoadFile(sFile);
                }
            }
            else
            {
                if (File.Exists(sItemToLoad))
                    LoadFile(sItemToLoad);
            }
            lbTargetSavedAssessmentFiles.Enabled = true;
            btProcessLoadedFiles.Enabled = true;
        }


        public void LoadFile(String sFileToLoad)
        {
            if (Path.GetExtension(sFileToLoad).ToLower() == ".cirdata")
            {
                cdCirData = CirLoad.loadSerializedO2CirDataObject(sFileToLoad);
                lbCirFileLoaded.Text = Path.GetFileName(sFileToLoad);
            }
            else
            {                
                if (Path.GetExtension(sFileToLoad).ToLower() == ".xml" ||
                    Path.GetExtension(sFileToLoad).ToLower() == ".ozasmt")
                {
                    var oadO2AssessmentDataOunceV6 = JoinTracesUtils.loadAssessmentRunFileAndAddItToList(sFileToLoad);
                    if (oadO2AssessmentDataOunceV6 != null)
                    {
                        foreach (object oItem in lbTargetSavedAssessmentFiles.Items)
                            if (oItem.ToString() == oadO2AssessmentDataOunceV6.ToString())
                            {
                                DI.log.error(
                                    "in loadAssessmentRunFileAndAddItToList, file is already in the list of F1AssessmentRun objects");                                
                                return;
                            }
                        lbTargetSavedAssessmentFiles.Items.Add(oadO2AssessmentDataOunceV6);
                    }
                }
                else
                    DI.log.debug("Skipping loading file (since it is one of type: .cirdata, .xml or .ozasmt: {0}",
                                 sFileToLoad);
            }
        }
        

        // ReSharper disable ConditionIsAlwaysTrueOrFalse
        // ReSharper restore ConditionIsAlwaysTrueOrFalse

        public void showTraceInViewer(TreeNode tnTreeNodeToProcess, ascx_TraceViewer atvTracetViewer)
        {
            if (tnTreeNodeToProcess != null)
            {
                if (tvAllTraces != null && tvAllTraces.Nodes[tnTreeNodeToProcess.Text] != null)
                {
                    tvAllTraces.SelectedNode = tvAllTraces.Nodes[tnTreeNodeToProcess.Text];
                    tvAllTraces.SelectedNode.ExpandAll();
                }
                if (tnTreeNodeToProcess.Tag != null && tnTreeNodeToProcess.Tag.GetType().Name == "FindingViewItem")
                {
                    atvTracetViewer.setTraceDataAndRefresh((FindingViewItem)tnTreeNodeToProcess.Tag);
                }
            }
        }

        //(tbCreateTracesForKeyword.Text,cdCirData,dO2TraceBlock,cbOnlyProcessTracesWithNoCallers.Checked


        private void previewCreatedTraces(List<TreeNode> ltnNormalizedTraces)
        {
            O2Thread.mtaThread(
                () =>
                this.invokeOnThread(
                    () =>
                    {
                        if (cbPreviewCreatedTraces.Checked)
                        {
                            tvTempTreeView.Nodes.Clear();
                            foreach (TreeNode tnTreeNode in ltnNormalizedTraces)
                                tvTempTreeView.Nodes.Add(tnTreeNode);
                        }
                    }));
        }
    }
}
