using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Tool.SearchAssessmentRun;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    public partial class ascx_EditAssessmentRun : UserControl
    {
        private List<FindingViewItem> lfviCurrentListOfFindingViewItems = new List<FindingViewItem>();

        public ascx_EditAssessmentRun()
        {
            InitializeComponent();
        }

        private void ascx_DropObject1_Load(object sender, EventArgs e)
        {
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            if (oObject.GetType().Name == "List`1")
            {
                foreach (Object oItem in (List<Object>) oObject)
                {
                    if (oItem.GetType().Name == "o2.analysis.FindingViewItem")
                    {
                        addFindingViewItemToList((FindingViewItem) oItem);
                        //loadCurrentListOfFindingsInListBox();
                        cbSearchResults_Findings_Filter_SelectedIndexChanged(null, null);
                    }
                    else
                        DI.log.error(
                            "in ascx_DropObject1_eDnDAction_ObjectDataReceived_Event: Item droped was of the wrong type: {0}",
                            oObject.GetType().Name);
                }
            }
        }

        private void btCreateAssessmentRunWithSearchResults_Click(object sender, EventArgs e)
        {
            var sasSavedAssessmentSearch = new AnalysisSearch.SavedAssessmentSearch();
            // foreach (o2.analysis.FindingViewItem FindingViewItem in lbSearchResults_Findings.Items)
            // {
            //     o2.analysis.FindingViewItem fviFindingViewItem = (o2.analysis.FindingViewItem)oFindingViewItem;
            //     sasSavedAssessmentSearch.lfrFindingsResults.Add(fviFindingViewItem.frFindingResult);
            // }
            sasSavedAssessmentSearch.lfrFindingsResults = getListOfFindingsResultsFromCurrentListOfViewSearchItems();
            CustomAssessmentFile.saveAssessmentSearchResultsAsNewAssessmentRunFile(sasSavedAssessmentSearch, "",
                                                                                   cbCreateFileWithAllTraces.Checked,
                                                                                   cbCreateFileWithUniqueTraces.Checked,
                                                                                   cbDropDuplicateSmartTraces.Checked,
                                                                                   cbIgnoreRootCallInvocation.Checked);
        }

        private void cbSearchResults_Findings_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ounceAnalysis_search.gui.populateFindingsResults_Findings(getListOfFindingsResultsFromCurrentListOfViewSearchItems(), lbSearchResults_Findings, cbSearchResults_Findings_Filter.Text, cbSearchResults_Findings_UniqueList.Checked);
        }

        private void lbSearchResults_Findings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSearchResults_Findings.SelectedItem != null)
                if (lbSearchResults_Findings.SelectedItem.GetType().Name == "o2.analysis.FindingViewItem")
                {
                    var fviFindingViewItem = (FindingViewItem) lbSearchResults_Findings.SelectedItem;
                    atvTraceViewer.setTraceDataAndRefresh(fviFindingViewItem);
                }
        }

        private void cbSearchResults_Findings_UniqueList_CheckedChanged(object sender, EventArgs e)
        {
            cbSearchResults_Findings_Filter_SelectedIndexChanged(null, null);
        }

        private void ascx_EditAssessmentRun_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                configureFilterComboBox();
            }
        }

        private void configureFilterComboBox()
        {
            cbSearchResults_Findings_Filter.Items.AddRange(AnalysisSearch.getListWithSearchFilters().ToArray());
            cbSearchResults_Findings_Filter.SelectedIndex = 0;
        }

        private void lbSearchResults_Findings_DoubleClick(object sender, EventArgs e)
        {
            if (lbSearchResults_Findings.SelectedItem != null)
                lbSearchResults_Findings.Items.Remove(lbSearchResults_Findings.SelectedItem);
        }

        private void btResetList_Click(object sender, EventArgs e)
        {
            lfviCurrentListOfFindingViewItems = new List<FindingViewItem>();
            loadCurrentListOfFindingsInListBox();
        }

        public void loadCurrentListOfFindingsInListBox()
        {
            lbSearchResults_Findings.Items.Clear();
            foreach (FindingViewItem fviFindingViewItem in lfviCurrentListOfFindingViewItems)
                lbSearchResults_Findings.Items.Add(fviFindingViewItem);
        }

        public void addFindingViewItemToList(FindingViewItem fviFindingViewItem)
        {
            lfviCurrentListOfFindingViewItems.Add(fviFindingViewItem);
            DI.log.debug("{0} item added to list", fviFindingViewItem.sText);
        }

        public void removeFindingViewItemToList(FindingViewItem fviFindingViewItem)
        {
            if (lfviCurrentListOfFindingViewItems.Contains(fviFindingViewItem))
                lfviCurrentListOfFindingViewItems.Remove(fviFindingViewItem);
            else
                DI.log.error("{0} doesn't exist in lis5", fviFindingViewItem.sText);
        }

        public List<AnalysisSearch.FindingsResult> getListOfFindingsResultsFromCurrentListOfViewSearchItems()
        {
            var lfrFindingsResults = new List<AnalysisSearch.FindingsResult>();
            foreach (FindingViewItem fviFindingViewItem in lfviCurrentListOfFindingViewItems)
                lfrFindingsResults.Add(fviFindingViewItem.frFindingResult);
            return lfrFindingsResults;
        }
    }
}