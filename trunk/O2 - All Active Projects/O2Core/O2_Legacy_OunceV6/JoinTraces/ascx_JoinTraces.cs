using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.CIR;
using O2.Legacy.OunceV6.JoinTraces.classes;
using O2.Legacy.OunceV6.JoinTraces.classes.filters;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Legacy.OunceV6.TraceViewer;

namespace O2.Legacy.OunceV6.JoinTraces
{
    public partial class ascx_JoinTraces : UserControl
    {
        

        public ascx_JoinTraces()
        {
            InitializeComponent();
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            handleDrop(oObject);
        }

       

        /*private void btProcessLoadedFindings_Click(object sender, EventArgs e)
        {
            proccessLoadedFiles();
            cbShowRawData_CheckedChanged(null, null);
            cbShowSinksView_CheckedChanged(null, null);
            cbShowSourcesView_CheckedChanged(null, null);
        }*/

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvAllTraces.SelectedNode != null)
            {
                TreeNode tnSelectedNode = tvAllTraces.SelectedNode;
                if (tnSelectedNode.Tag != null)
                {
                    var otdO2TraceBlock = (O2TraceBlock_OunceV6) tnSelectedNode.Tag;
                    DI.log.info(" Signature:{0}", otdO2TraceBlock.sSignature);
                    DI.log.info(" TraceRoot Text: {0}", otdO2TraceBlock.sTraceRootText);
                    DI.log.info(" Unique Name: {0}", otdO2TraceBlock.sUniqueName);
                }
                else
                {
                    TreeNode tnRootNode = O2Forms.getRootNode(tnSelectedNode);
                    DI.log.debug("Root Node:{0}", tnRootNode);
                    DI.log.debug("Selected Node:{0}", tnSelectedNode);
                    foreach (TreeNode tnTreeNode in tvRawData.Nodes)
                    {
                        if (tnTreeNode.Text == tnSelectedNode.Text)
                            tnSelectedNode.Nodes.Add((TreeNode) tnTreeNode.Clone());
                    }
                }
            }
        }


        private void tvSinksView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvSinksView.SelectedNode != null)
            {
                tbNormalizedTracesFor.Text = tvSinksView.SelectedNode.Text.Split(' ')[0];
                btShowNormalizedTracesFor_Click(null, null);

                if (tvSinksView.SelectedNode.Parent == null && tvSinksView.SelectedNode.Nodes.Count == 0)
                    // means we need to load the data for this root
                {
                    List<TreeNode> ltnTracesNodes =
                        viewing.FromTreeViewCalculateViewForSpecificRootNode(tvSinksView.SelectedNode.Text, tvRawData,
                                                                             "Sinks", tvRawData);
                    tvSinksView.SelectedNode.Nodes.AddRange(ltnTracesNodes.ToArray());
                    tvSinksView.SelectedNode.Tag = tvRawData.Nodes[tvSinksView.SelectedNode.Text].Tag;
                    tvSinksView.SelectedNode.Expand();
                }
                if (tvSinksView.SelectedNode.Parent != null)
                {
                    TreeNode tnCurrentNode = tvSinksView.SelectedNode;
                    if (tnCurrentNode.Tag != null)
                    {
                        var fviFindingViewItem = (FindingViewItem) tnCurrentNode.Tag;
                        ascx_TraceViewer2.setTraceDataAndRefresh(fviFindingViewItem);
                    }
                    return;
                }
                /*
                                  //  TreeNode tnCurrentNode = tvSinksView.SelectedNode;
                                    TreeNode tnPreviousNode = tvSinksView.SelectedNode.Parent;

                                    O2TraceBlock_OunceV6 otbO2TraceBlock_Current = (O2TraceBlock_OunceV6)tnCurrentNode.Tag;
                                    O2TraceBlock_OunceV6 otbO2TraceBlock_Previous = (O2TraceBlock_OunceV6)tnPreviousNode.Tag;
                                    if (otbO2TraceBlock_Current != null && otbO2TraceBlock_Previous != null)
                                        foreach (xsd.AssessmentAssessmentFileFinding fFinding in otbO2TraceBlock_Previous.dSinks.Keys)
                                        {
                                            String sSinkUniqueName = getUniqueSignature(fFinding, Analysis.TraceType.Known_Sink, otbO2TraceBlock_Previous.dSinks[fFinding], true);
                                            if (sSinkUniqueName == otbO2TraceBlock_Current.sUniqueName)
                                            {
                                                FindingViewItem fviFindingViewItem = new FindingViewItem(fFinding, "Finding Text", null, otbO2TraceBlock_Previous.dSinks[fFinding]);
                                                ascx_TraceViewer2.setTraceDataAndRefresh(fviFindingViewItem);
                                                return;
                                            }
                          
                                        }
                                }
                

                                // also show the selected item on the main Treeview
                                if (tvAllTraces.Nodes[tvSinksView.SelectedNode.Text] != null)
                                {
                                    tvAllTraces.SelectedNode = tvAllTraces.Nodes[tvSinksView.SelectedNode.Text];
                                    tvAllTraces.SelectedNode.Expand();
                                }
                        */
            }
        }

        private void tvSourcesView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvSourcesView.SelectedNode != null)
            {
                if (tvSourcesView.SelectedNode.Parent != null)
                {
                    return;
                    /*TreeNode tnCurrentNode = tvSourcesView.SelectedNode;
                    TreeNode tnPreviousNode = tvSourcesView.SelectedNode.Parent;

                    var otbO2TraceBlock_Current = (O2TraceBlock_OunceV6) tnCurrentNode.Tag;
                    var otbO2TraceBlock_Previous = (O2TraceBlock_OunceV6) tnPreviousNode.Tag;
                    if (otbO2TraceBlock_Current != null && otbO2TraceBlock_Previous != null)
                        foreach (AssessmentAssessmentFileFinding fFinding in otbO2TraceBlock_Previous.dSources.Keys)
                        {
                            String sSourceUniqueName = analyzer.getUniqueSignature(fFinding,
                                                                                   TraceType.Known_Sink,
                                                                                   otbO2TraceBlock_Previous.dSources[
                                                                                       fFinding], true);
                            if (sSourceUniqueName == otbO2TraceBlock_Previous.sUniqueName)
                            {
                                var fviFindingViewItem = new FindingViewItem(fFinding, "Finding Text", null,
                                                                             otbO2TraceBlock_Previous.dSources[fFinding]);
                                ascx_TraceViewer1.setTraceDataAndRefresh(fviFindingViewItem);
                            }
                        }*/
                }
            }
        }

        private void lbTargetSavedAssessmentFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lbTargetSavedAssessmentFiles.SelectedItem != null)
                lbTargetSavedAssessmentFiles.Items.Remove(lbTargetSavedAssessmentFiles.SelectedItem);
        }

        private void cbShowRawData_CheckedChanged(object sender, EventArgs e)
        {
            tvAllTraces.Nodes.Clear();
            if (cbShowRawData.Checked && tvRawData != null)
            {
                tvAllTraces.Visible = false;
                foreach (TreeNode tnTreeNode in tvRawData.Nodes)
                    if (tbRawDataFilter.Text == "" || tnTreeNode.Text.IndexOf((string) tbRawDataFilter.Text) > -1)
                        tvAllTraces.Nodes.Add((TreeNode) tnTreeNode.Clone());
                tvAllTraces.Visible = true;
            }
        }

        private void cbShowSourcesView_CheckedChanged(object sender, EventArgs e)
        {
            tvSourcesView.Nodes.Clear();
            if (cbShowSourcesView.Checked && tvSourcesData != null)
            {
                foreach (TreeNode tnTreeNode in tvSourcesData.Nodes)
                    tvSourcesView.Nodes.Add(tnTreeNode);
            }
        }

        private void cbShowSinksView_CheckedChanged(object sender, EventArgs e)
        {
            tvSinksView.Nodes.Clear();
            if (cbShowSinksView.Checked && tvRawData != null)
            {
                tvSinksView.Visible = false;
                //foreach (TreeNode tnTreeNode in tvSinksData.Nodes)
                foreach (TreeNode tnTreeNode in tvRawData.Nodes)
                    if (cbSinksView_OnlyShowEdges.Checked == false ||
                        tvRawData.Nodes[tnTreeNode.Text].Nodes["Sources"].Nodes.Count == 0)
                        if (tbSinksViewFilter.Text == "" || tnTreeNode.Text.IndexOf((string) tbSinksViewFilter.Text) > -1)
                            //tvSinksView.Nodes.Add(tnTreeNode.Text);
                            tvSinksView.Nodes.Add(viewing.getRootNodeToView(tnTreeNode.Text, "Sinks", tvRawData));
                tvSinksView.Visible = true;
            }
        }

        private void cbSinksView_OnlyShowEdges_CheckedChanged(object sender, EventArgs e)
        {
            cbShowSinksView_CheckedChanged(null, null);
        }

        private void cbSourcesView_OnlyShowEdges_CheckedChanged(object sender, EventArgs e)
        {
            cbShowSourcesView_CheckedChanged(null, null);
        }

        private void btShowNormalizedTracesFor_Click(object sender, EventArgs e)
        {
            var nomalizedTraces = analyzer.ResolveNormalizeTraceFor(tbNormalizedTracesFor.Text, JoinTracesUtils.calculateDictionaryWithRawData(tvRawData), cdCirData,
                                              dO2TraceBlock, cbOnlyProcessTracesWithNoCallers.Checked);
            tvNormalizedTracesView.Nodes.Clear();
            foreach (var normalizedTrace in nomalizedTraces)
                tvNormalizedTracesView.Nodes.Add(normalizedTrace);
            viewing.ProcessNormalizedTracesAndDisplayResults(tvProcessedNormalizedTraces, tvNormalizedTracesView);
        }

        

        private void tbSinksViewFilter_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbSinksViewFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                cbShowSinksView_CheckedChanged(null, null);
        }


        private void tvSinksView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Parent == null && e.Node.Tag == null) // means we have not processed this node
            {
                foreach (TreeNode tnChildNode in e.Node.Nodes)
                {
                    if (tnChildNode.Text != "")
                    {
                        var fviFindingViewItem = (FindingViewItem) tnChildNode.Tag;
                        var otbO2TraceBlockOfChildNode = (O2TraceBlock_OunceV6) tvRawData.Nodes[tnChildNode.Text].Tag;
                        analyzer.addCompatibleTracesToNode_recursive(tnChildNode, fviFindingViewItem,
                                                                     otbO2TraceBlockOfChildNode, "Sinks", JoinTracesUtils.calculateDictionaryWithRawData(tvRawData));
                    }
                    //  fviFindingViewItem
                }
            }
        }


        private void tbRawDataFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                cbShowRawData_CheckedChanged(null, null);
        }

        private void tvNormalizedTracesView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            showTraceInViewer(e.Node, ascx_TraceViewer_NormalizedTraces);
        }

        private void tvProcessedNormalizedTraces_AfterSelect(object sender, TreeViewEventArgs e)
        {
            e.Node.ExpandAll();
            showTraceInViewer(e.Node, ascx_TraceViewer_NormalizedTraces);
        }

        private void ascx_JoinTraces_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                ascx_SelectVisiblePanels1.setVisibleControlsColapseState_4Panels_TopRight(scHost, scLeft, scRight,
                                                                                          "Files Loaded", "Findings Viewer for Joinned Traces ",
                                                                                          "Raw Data Viewer",
                                                                                          "Trace Builder");
                ascx_SelectVisiblePanels1.setCheckBox_Checked(2, true);

                //btTest_Click(null, null);
            }
        }

        private void btCreateTraces_Click(object sender, EventArgs e)
        {
            btCreateTraces.Enabled = false;
            DI.log.debug("Creating Traces");
            O2Timer tTimer = new O2Timer("Creating Traces").start();


            string textFilter = tbCreateTracesForKeyword.Text;
            //TreeView _tvRawData = tvRawData;
            ICirData _cdCirData = cdCirData;
            Dictionary<String, O2TraceBlock_OunceV6> _dO2TraceBlock = dO2TraceBlock;
            Dictionary<string, O2TraceBlock_OunceV6> dRawData = JoinTracesUtils.calculateDictionaryWithRawData(tvRawData);
            bool bOnlyProcessTracesWithNoCallers = cbOnlyProcessTracesWithNoCallers.Checked;

            string targetFolder = tbFolderToSaveAssessment.Text;
            string fileNamePrefix = Path.GetFileNameWithoutExtension(lbCirFileLoaded.Text);
            bool bCreateFileWithAllTraces = cbCreateFileWithAllTraces.Checked;
            bool bCreateFileWithUniqueTraces = cbCreateFileWithUniqueTraces.Checked;
            bool bDropDuplicateSmartTraces = cbDropDuplicateSmartTraces.Checked;
            bool bIgnoreRootCallInvocation = cbIgnoreRootCallInvocation.Checked;

            JoinTracesUtils.createAssessessmentFileWithJoinnedTraces(
                textFilter, dRawData, _cdCirData, _dO2TraceBlock,
                bOnlyProcessTracesWithNoCallers, targetFolder, fileNamePrefix, bCreateFileWithAllTraces,
                bCreateFileWithUniqueTraces, bDropDuplicateSmartTraces, bIgnoreRootCallInvocation,previewCreatedTraces,
                sAssessmentFile =>
                    {

                        this.invokeOnThread(
                            () =>
                                {
                                    lbCreatedAssessmentFile.Text = sAssessmentFile;
                                    lbCreatedAssessmentFile.Visible = true;

                                    btCreateTraces.Enabled = true;
                                });
                        tTimer.stop();
                        if (sAssessmentFile!="")
                            findingsViewerfor_JoinnedTraces.loadO2Assessment(sAssessmentFile);
                        return sAssessmentFile;
                    });
                                            
        }       

        private void cbCalculateSourcesView_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbCalculateSinksView_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void tvTempTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvTempTreeView.SelectedNode.Tag != null)
                ascxTraceViewer_JoinnedTraces.setTraceDataAndRefresh((FindingViewItem) tvTempTreeView.SelectedNode.Tag);
        }        

        private void applySpecialFiltersToRawData()
        {
            if (cbSpecialFilter_MapDotNetWebServices.Checked)
                onRawData.dotNet.mapDotNetWebServices(tvRawData);
            if (cbAddSuportForDynamicMethodsOnSinks.Checked)
                onRawData.java.addSuportForDynamicMethodsOnSinks(tvRawData, cdCirData,
                                                                 cbAddGluedTracesAsRealTraces.Checked);

            if (cbMapJavaInterfaces.Checked)
            {
                onRawData.java.mapInterfaces(tvRawData, cdCirData, "interfaces", cbAddGluedTracesAsRealTraces.Checked);
            }
            analyzer.calculateO2TraceBlocksIntoTreeView(dO2TraceBlock, ref tvRawData);
        }

        private void tbCreateTracesForKeyword_TextChanged(object sender, EventArgs e)
        {
        }

        private void btFindSpringAttributes_Click(object sender, EventArgs e)
        {
            onRawData.java.findSpringAttributes(tvRawData);
            analyzer.calculateO2TraceBlocksIntoTreeView(dO2TraceBlock, ref tvRawData);
            //   onRawData.java.addVelocityMappings(tvRawData);
            //   analyzer.calculateO2TraceBlocksIntoTreeView(dO2TraceBlock, ref tvRawData);
        }

        private void btSaveCreatedAssessmentFileIntoFolder_Click(object sender, EventArgs e)
        {
            if (File.Exists(lbCreatedAssessmentFile.Text) && Directory.Exists(tbFolderToSaveAssessment.Text))
            {
                String sTargetFile = Path.Combine(tbFolderToSaveAssessment.Text,
                                                  Path.GetFileNameWithoutExtension(lbCirFileLoaded.Text) +
                                                  ".ALLTRACES.ozasmt");
                File.Copy(lbCreatedAssessmentFile.Text, sTargetFile);
            }
        }

        private void scLeft_Panel2_Paint(object sender, PaintEventArgs e)
        {
                
        }

        private void cbAddGluedTracesAsRealTraces_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btProcessLoadedFiles_Click(object sender, EventArgs e)
        {
            var o2AssessmentDataItemsToProcess = new List<O2AssessmentData_OunceV6>();
            foreach (O2AssessmentData_OunceV6 o2AssessmentData in lbTargetSavedAssessmentFiles.Items)
                o2AssessmentDataItemsToProcess.Add(o2AssessmentData);

            btProcessLoadedFiles.Enabled = false;
            JoinTracesUtils.proccessLoadedFiles(o2AssessmentDataItemsToProcess, cbMakeLostSinksIntoSinks.Checked,
                                (_dO2TraceBlock, _tvRawData) =>
                                    {
                                        dO2TraceBlock = _dO2TraceBlock;
                                        tvRawData = _tvRawData;
                                        this.invokeOnThread(
                                            () =>
                                            { btProcessLoadedFiles.Enabled = true;
                                                laNumberOfTracesProcessed.Text = dO2TraceBlock.Count + " TraceBlock : " +
                                                                                 tvRawData.Nodes.Count + " Nodes";
                                            });
                                        return true;
                                    });
    }

        private void btApplySpecialFilters_Click(object sender, EventArgs e)
        {
            DI.log.info("Applying Special filters and recalculating loaded data");
            applySpecialFiltersToRawData();
        }


        /*            List<String> lsTracesFromSameSequence = new List<string>();
              //      TreeNode tnTreeForTrace_Root = new TreeNode();
              //      o2.analysis.Analysis.SmartTraceFilter stfSmartTraceFilter = o2.analysis.Analysis.SmartTraceFilter.MethodName;
        //            o2.analysis.Analysis.addCallsToNode_Recursive(otbO2TraceBlock_Root, tnTreeForTrace_Root, fviFindingViewItem.oadO2AssessmentDataOunceV6, stfSmartTraceFilter);
                    return lsTracesFromSameSequence;    
                */
    }
}