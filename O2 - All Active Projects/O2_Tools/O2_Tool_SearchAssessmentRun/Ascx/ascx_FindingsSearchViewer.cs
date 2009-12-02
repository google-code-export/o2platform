// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Views.ASCX.DataViewers;


namespace O2.Tool.SearchAssessmentRun.Ascx
{
    public partial class ascx_FindingsSearchViewer : UserControl
    {
        #region Delegates

        public delegate void dViewFindingViewItem_Callback(FindingViewItem fviFindingViewItem);

        #endregion

        public dViewFindingViewItem_Callback dViewItemCallback;
        public AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch;
        private bool bInvokeAfterSelectFortvFilteredFindings= true;

        public ascx_FindingsSearchViewer()
        {
            InitializeComponent();
        }

        private void lbFilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ascx_FindingsSearchViewer_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                populateListBoxWithCurrentFilters();
                tvSearchFilters.AllowDrop = true;
                ascx_FunctionsViewer1.eNodeEvent_AfterSelect = asfFunctionsViewer_AfterSelected;
                ascx_FunctionsViewer1.eNodeEvent_CheckClickEvent = asfFunctionsViewer_CheckClickEvent;
                //flpSelectedFindings.SetFlowBreak(
            }
        }

        private void populateListBoxWithCurrentFilters()
        {
            lbFilterList.Items.AddRange(AnalysisSearch.getListWithSearchFilters().ToArray());
        }

        private void lbFilterList_DoubleClick(object sender, EventArgs e)
        {
            var btNewButton = new Button();
            btNewButton.Text = lbFilterList.SelectedItem.ToString();
        }

        private void lbFilterList_MouseDown(object sender, MouseEventArgs e)
        {
            if (false == cbFilterList_UseDoubleClickToAddFilter.Checked)
                DoDragDrop(lbFilterList.SelectedItem, DragDropEffects.Copy);
        }

        private void tvCurrentFilters_DragDrop(object sender, DragEventArgs e)
        {
            TreeNodeCollection tnTargetTreeNodeCollection = tvSearchFilters.Nodes;
            TreeNode tnDraggedTarget = O2Forms.getTreeNodeAtDroppedOverPoint(tvSearchFilters, e.X, e.Y);

            if (tnDraggedTarget != null)
            {
                tnTargetTreeNodeCollection = tnDraggedTarget.Nodes;
                // DI.log.error("tnDraggedTarget: {0}", tnDraggedTarget.Text);
            }

            var sDroppedData = (String) Dnd.getGetObjectFromDroppedData(e, "String");
            if (sDroppedData != null)
            {
                if (tnDraggedTarget != null)
                    tvSearchFilters.SelectedNode = tvSearchFilters.Nodes.Insert(tnDraggedTarget.Index - 1, sDroppedData);
                else
                    tnTargetTreeNodeCollection.Add(sDroppedData);

                //             tvCurrentFilters.ExpandAll();                
            }
            else
            {
                var tnDroppedData = (TreeNode) Dnd.getGetObjectFromDroppedData(e, "TreeNode");
                if (tnDroppedData != null)
                {
                    if (tvSearchFilters.SelectedNode != tnDroppedData && tnDraggedTarget != null)
                    {
                        tvSearchFilters.Nodes.Remove(tnDroppedData);
                        tvSearchFilters.Nodes.Insert(tnDraggedTarget.Index + 1, tnDroppedData);
                        tvSearchFilters.SelectedNode = tnDroppedData;
                    }
                }
            }
            tvSearchFilters.ExpandAll();
            loadTreeViewWithSearchResults();
        }

        private void tvCurrentFilters_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tvCurrentFilters_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void tvCurrentFilters_DragOver(object sender, DragEventArgs e)
        {
            //    DI.log.info("x:{0} y:{1}", e.X, e.Y);
            TreeNode tnDraggedTarget = O2Forms.getTreeNodeAtDroppedOverPoint(tvSearchFilters, e.X, e.Y);
            if (tnDraggedTarget != null)
            {
                tvSearchFilters.SelectedNode = tnDraggedTarget;
            }
        }

        private void lbFilterList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbFilterList.SelectedItem != null)
                tvSearchFilters.Nodes.Add(lbFilterList.SelectedItem.ToString());
        }

        private void llCurrentFilter_RemoveSelected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tvSearchFilters.SelectedNode != null)
            {
                tvSearchFilters.Nodes.Remove(tvSearchFilters.SelectedNode);
                loadTreeViewWithSearchResults();
            }
        }

        private void llCurrentFilter_RemoveAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tvSearchFilters.Nodes.Clear();
        }

        private void tvCurrentFilters_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
                if (tvSearchFilters.SelectedNode != null)
                    DoDragDrop(tvSearchFilters.SelectedNode, DragDropEffects.Copy);
        }

        private void tvCurrentFilters_MouseMove(object sender, MouseEventArgs e)
        {
            //       if (tvCurrentFilters.SelectedNode != null)
            //           DoDragDrop(tvCurrentFilters.SelectedNode, DragDropEffects.Copy);
        }

        private void tvCurrentFilters_DoubleClick(object sender, EventArgs e)
        {
        }

        public void loadAssessmentFileAndShowData(String sAssessmentFileToLoad)
        {
            loadAssessmentFileAndShowData(sAssessmentFileToLoad, ".");
        }

        public void loadAssessmentFileAndShowData(String sAssessmentFileToLoad, String sDefaultSearchCriteriaText)
        {
            sasSavedAssessmentSearch = new AnalysisSearch.SavedAssessmentSearch(sAssessmentFileToLoad);
            sasSavedAssessmentSearch.searchUsingCriteria(sDefaultSearchCriteriaText);

            loadTreeViewWithSearchResults();
        }

        public void setSavedAssessmentSearchAndLoadData(AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch)
        {
            this.sasSavedAssessmentSearch = sasSavedAssessmentSearch;
            if (tvSearchFilters.Nodes.Count == 0)
                tvSearchFilters.Nodes.Add("Search Text");
            /*
            if (sasSavedAssessmentSearch.lscSearchCriteria.Count > 0)
            {
                tvSearchFilters.Nodes.Clear();
                //tvSearchFilters.Nodes.Add(sasSavedAssessmentSearch.lscSearchCriteria[sasSavedAssessmentSearch.lscSearchCriteria.Count - 1].stSearchType.ToString());
                tvSearchFilters.Nodes.Add("Search Text");
            }*/
            loadTreeViewWithSearchResults();
        }


        private void loadTreeViewWithSearchResults()
        {
            tvFilteredFindings.Visible = false;
            tvFilteredFindings.Nodes.Clear();


            List<AnalysisSearch.FindingsResult> lfrFindingsResults = sasSavedAssessmentSearch.lfrFindingsResults;

            bool bUniqueList = true;
            TreeNodeCollection tnvTargetTreeNodeCollection = tvFilteredFindings.Nodes;
            var lsFiltersToApply = new List<string>();
            foreach (TreeNode tnSearchFilter in tvSearchFilters.Nodes)
                lsFiltersToApply.Add(tnSearchFilter.Text);
            populateTreeNodeCollectionWithFilteredSearch_Recursive(tnvTargetTreeNodeCollection, lfrFindingsResults,
                                                                   bUniqueList, lsFiltersToApply, 0);
            foreach (TreeNode tnTreeNode in tvFilteredFindings.Nodes)
                tnTreeNode.Expand();

            populateFunctionViewerWithTreeViewData(ascx_FunctionsViewer1, tvFilteredFindings);
            //tvFilteredFindings.ExpandAll();

            //foreach (TreeNode tnSearchFilter in tvSearchFilters.Nodes)
            //{

            //   List<TreeNode> ltnFilteredNodes = populateTreeNodeCollectionWithFilteredSearch(tnvTargetTreeNodeCollection, lfrFindingsResults, tnSearchFilter.Text, bUniqueList);

            //   List<AnalysisSearch.FindingsResult> lfrNewBatchOfFindingsResults = new List<AnalysisSearch.FindingsResult>();                
            //}
            tvFilteredFindings.Sort();
            tvFilteredFindings.Visible = true;
            /*foreach (AnalysisSearch.FindingsResult frFindingsResult in sasSavedAssessmentSearch.lfrFindingsResults)
            {
                
            }*/
        }

        public static Int32 getImageIdFromFilterAndFinding(String sFindingFilter, FindingViewItem fviFindingViewItem)
        {
            switch (sFindingFilter)
            {
                case "Confidence":
                    switch (fviFindingViewItem.fFinding.confidence)
                    {
                        case 0:
                            return 8;
                        case 1:
                            return 6;
                        case 2:
                            return 7;
                        case 3:
                            return 4;
                    }
                    break;
                case "Severity":
                    switch (fviFindingViewItem.fFinding.severity)
                    {
                        case 0:
                            return 1;
                        case 1:
                            return 3;
                        case 2:
                            return 2;
                        case 3:
                            return 4;
                    }
                    break;
                case "Sink":
                    if (fviFindingViewItem.fFinding.Trace != null)
                        return 10;
                    else
                        return 0;
            }
            return 9;
        }

        public static Color getColorFromFilter(String sFindingFilter)
        {
            switch (sFindingFilter)
            {
                case "Lost Sink":
                    return Color.DarkOrange;
                case "Known Sink":
                case "Sink":
                    return Color.Red;
                case "Source":
                    return Color.DarkRed;
                default:
                    return Color.Black;
            }
        }

        /*//case 5: // Analysis.TraceType.Lost_Sink:
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Microsoft.Glee.Drawing.Color.DarkOrange;
                                        break;
                                    case 2: // Analysis.TraceType.Source:
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Microsoft.Glee.Drawing.Color.DarkRed;
                                        break;
                                    case 3: // Analysis.TraceType.Known_Sink:
                                        nNode.Attr.Fontcolor = nNode.Attr.Color = Microsoft.Glee.Drawing.Color.Red;
        */

        public static List<TreeNode> populateTreeNodeCollectionWithFilteredSearch_Recursive(
            TreeNodeCollection trnTreeNodeCollection, List<AnalysisSearch.FindingsResult> lfrFindingsResults,
            bool bUniqueList, List<String> lsFiltersToApply, int iCurrentIndex)
        {
            if (iCurrentIndex < lsFiltersToApply.Count)
            {
                Dictionary<String, List<FindingViewItem>> dFilteredFindings = null;
                String sFindingFilter = lsFiltersToApply[iCurrentIndex];
                var ltnFilteredNodes = new List<TreeNode>();
                List<FindingViewItem> lviFindingViewItem =
                    AnalysisSearch.calculateDictionaryWithFilteredFindingsResults(lfrFindingsResults,
                                                                                  ref dFilteredFindings, sFindingFilter,
                                                                                  bUniqueList);
                foreach (String sFilteredFinding in dFilteredFindings.Keys)
                {
                    String sNodeName = sFilteredFinding;
                    if (sFindingFilter != "Confidence" && sFindingFilter != "Severity")
                        sNodeName = sFindingFilter + ": " + sNodeName;
                    Color cColor = getColorFromFilter(sFindingFilter);
                    if (bUniqueList)
                    {
                        Int32 iImageId = getImageIdFromFilterAndFinding(sFindingFilter,
                                                                        dFilteredFindings[sFilteredFinding][0]);
                        sNodeName = String.Format("{0}             ({1})", sNodeName,
                                                  dFilteredFindings[sFilteredFinding].Count);
                        TreeNode tnFilteredFinding = O2Forms.newTreeNode(sNodeName, sFilteredFinding, iImageId,
                                                                         dFilteredFindings[sFilteredFinding]);
                        tnFilteredFinding.ForeColor = cColor;
                        trnTreeNodeCollection.Add(tnFilteredFinding);
                        //frFindingsResult.sStringThatMatchedCriteria);                    
                        ltnFilteredNodes.Add(tnFilteredFinding);
                    }
                    else
                    {
                        foreach (FindingViewItem fviFindingViewItem in dFilteredFindings[sFilteredFinding])
                        {
                            Int32 iImageId = getImageIdFromFilterAndFinding(sFindingFilter, fviFindingViewItem);
                            //String sNodeName = String.Format(sNodeName;
                            TreeNode tnFilteredFinding = O2Forms.newTreeNode(sNodeName, sFilteredFinding, iImageId,
                                                                             fviFindingViewItem);
                            tnFilteredFinding.ForeColor = cColor;
                            trnTreeNodeCollection.Add(tnFilteredFinding);
                            //frFindingsResult.sStringThatMatchedCriteria);                    
                            ltnFilteredNodes.Add(tnFilteredFinding);
                        }
                    }
                }

                if (++iCurrentIndex < lsFiltersToApply.Count)
                {
                    foreach (TreeNode tnTreeNode in ltnFilteredNodes)
                    {
                        /*                        List<AnalysisSearch.FindingsResult> lfrTreeNodeFindingsResults = new List<AnalysisSearch.FindingsResult>();
                                                foreach (FindingViewItem fviFindingViewItem in dFilteredFindings[tnTreeNode.Tag.ToString()])// ((Dictionary<String, List<FindingViewItem>>)tnTreeNode.Tag).Values)
                                                    lfrTreeNodeFindingsResults.Add(fviFindingViewItem.frFindingResult);

                          */
                        populateTreeNodeCollectionWithFilteredSearch_Recursive(
                            tnTreeNode.Nodes,
                            getFindingsResultsListFromTreeNodeTag(tnTreeNode),
                            bUniqueList,
                            lsFiltersToApply,
                            iCurrentIndex);
                    }
                }
                return ltnFilteredNodes;
            }
            return null;
        }


        public static List<AnalysisSearch.FindingsResult> getFindingsResultsListFromTreeNodeTag(TreeNode tnTreeNode)
        {
            var lfrTreeNodeFindingsResults = new List<AnalysisSearch.FindingsResult>();
            if (tnTreeNode.Tag != null)
            {
                var lviFindingViewItem = (List<FindingViewItem>) tnTreeNode.Tag;
                foreach (FindingViewItem fviFindingViewItem in lviFindingViewItem)
                    // ((Dictionary<String, List<FindingViewItem>>)tnTreeNode.Tag).Values)
                    lfrTreeNodeFindingsResults.Add(fviFindingViewItem.frFindingResult);
            }
            return lfrTreeNodeFindingsResults;
        }

        private void tvFilteredFindings_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode tnDraggedTarget = O2Forms.getTreeNodeAtDroppedOverPoint(tvFilteredFindings, e.X, e.Y);
            if (tnDraggedTarget != null)
            {
                var sDroppedData = (String) Dnd.getGetObjectFromDroppedData(e, "String");
                if (sDroppedData != null)
                {
                    bool bUniqueList = cbUniqueListWhenDroppingFilter.Checked;
                    var lsFiltersToApply = new List<string>(new[] {sDroppedData});
                    int iCurrentIndex = 0;
                    populateTreeNodeCollectionWithFilteredSearch_Recursive(
                        tnDraggedTarget.Nodes,
                        getFindingsResultsListFromTreeNodeTag(tnDraggedTarget),
                        bUniqueList,
                        lsFiltersToApply,
                        iCurrentIndex);

                    tnDraggedTarget.Expand();
                }
            }
        }

        private void tvFilteredFindings_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tvFilteredFindings_DragOver(object sender, DragEventArgs e)
        {
            TreeNode tnDraggedTarget = O2Forms.getTreeNodeAtDroppedOverPoint(tvFilteredFindings, e.X, e.Y);
            if (tnDraggedTarget != null)
                tvFilteredFindings.SelectedNode = tnDraggedTarget;
        }

        private void tvFilteredFindings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (bInvokeAfterSelectFortvFilteredFindings)
            {
                if (tvFilteredFindings.SelectedNode != null && tvFilteredFindings.SelectedNode.Tag != null &&
                    dViewItemCallback != null)
                {
                    if (tvFilteredFindings.SelectedNode.Tag.GetType().Name == "FindingViewItem")
                    {
                        var fviFindingViewItem = (FindingViewItem) tvFilteredFindings.SelectedNode.Tag;
                        dViewItemCallback.Invoke(fviFindingViewItem);
                    }
                    else
                    {
                        var fviFindingViewItem = (List<FindingViewItem>) tvFilteredFindings.SelectedNode.Tag;
                        if (fviFindingViewItem.Count > 0) // show the first one
                            dViewItemCallback.Invoke(fviFindingViewItem[0]);
                    }

                    tvFilteredFindings.Focus();
                    // tvSmartTrace.Tag = fviFindingViewItem;
                    // refreshSmartTraceTreeView();
                }
            }
        }

        private void llFilteredFindings_RemoveSelected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tvFilteredFindings.SelectedNode != null)
                tvFilteredFindings.Nodes.Remove(tvFilteredFindings.SelectedNode);
        }


        public static void populateFunctionViewerWithTreeViewData(ascx_FunctionsViewer afvFunctionViewer,
                                                                  TreeView tvWithData)
        {
            var lsSignatures = new List<string>();
            foreach (TreeNode tnTreeNode in tvWithData.Nodes)
                lsSignatures.Add(tnTreeNode.Name);
            afvFunctionViewer.showSignatures(lsSignatures);
        }

        public void asfFunctionsViewer_AfterSelected(String sItemSelected)
        {
            TreeNode tnNodeWithInfo = tvFilteredFindings.Nodes[sItemSelected];
            if (tnNodeWithInfo != null)
            {
                tvFilteredFindings.SelectedNode = tnNodeWithInfo;
                tvFilteredFindings.TopNode = tnNodeWithInfo;
                ascx_FunctionsViewer1.Focus();
            }
        }


        public List<String> getSelectedFilters()
        {
            var lsSelectedFilters = new List<string>();
            foreach (TreeNode filter in tvSearchFilters.Nodes)
                lsSelectedFilters.Add(filter.Text);
            return lsSelectedFilters;
        }


        public List<String> getCurrentFilters()
        {
            var lsCurrentFilters = new List<string>();
            foreach (String sFilter in lbFilterList.Items)
                lsCurrentFilters.Add(sFilter);
            return lsCurrentFilters;
        }

        public void asfFunctionsViewer_CheckClickEvent(List<String> lsString)
        {
        }

        private void tvFilteredFindings_ItemDrag(object sender, ItemDragEventArgs e)
        {
            bInvokeAfterSelectFortvFilteredFindings = false;
            tvFilteredFindings.SelectedNode = (TreeNode)e.Item;
            DoDragDrop(e.Item, DragDropEffects.Copy);
            bInvokeAfterSelectFortvFilteredFindings = true;
        }

        /*

         * Dictionary<String, List<o2.analysis.FindingViewItem>> dFilteredFindings = null;
            List<o2.analysis.FindingViewItem> lviFindingViewItem = o2.analysis.AnalysisSearch.calculateDictionaryWithFilteredFindingsResults(lfrFindingsResults,ref  dFilteredFindings, sFindingFilter, bUniqueList);
            //o2.analysis.AnalysisSearch.GUI.populateWithListOfFilteredFindings_ListBox(lbSearchResults_Findings, lviFindingViewItem, dFilteredFindings);
            o2.analysis.AnalysisSearch.GUI.populateWithListOfFilteredFindings_TreeView(tvSearchResults_Findings, lviFindingViewItem, dFilteredFindings);
            //lbSearchResults_Findings.Tag = lfrFindingsResults;
            o2.analysis.AnalysisSearch.GUI.populateWithDictionaryOfFilteredFindings_TreeView(tv_CreateSavedAssessment_PerFindingsType, dFilteredFindings);
         */
    }
}
