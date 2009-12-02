// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Views.ASCX.classes;

namespace O2.Tool.SearchAssessmentRun.Ascx
{
    public partial class ascx_SearchAssessmentRun : UserControl
    {
        private bool bDropDuplicateSmartTraces = true;
        private bool bIgnoreRootCallInvocation = true;
        private Analysis.FindingFilter ffFindingFilter = Analysis.FindingFilter.AllFindings;

        public ascx_SearchAssessmentRun()
        {
            InitializeComponent();
            setupModule();
        }

        public void setupModule()
        {
            ascx_DropObject1.setText("Drop Saved Assessment Files Here");
            wbInfo.Navigate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help/About_SAR.htm"));
            ascx_svpSearchAssessmentRun.setVisibleControlsColapseState_4Panels_TopRight(scHost, scTop, scBottom,
                                                                                        "Loaded Files",
                                                                                        "Search Criteria",
                                                                                        "Search Results",
                                                                                        "Create Custom Assessment Files");
            ascx_svpSearchAssessmentRun.setCheckBox_Left(2, 110);
            ascx_svpSearchAssessmentRun.setCheckBox_Left(3, 230);
            ascx_svpSearchAssessmentRun.setCheckBox_Left(4, 350);
        }

        private void ascx_SearchAssessmentRun_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                //          populateFilterComboBox();
                populateSearchType();

                // once I added the NegativeSearch checkbox the next line crashes (with a stackOverflow exception (I'm doing this now on a enter)
                // dgvSearchCriteria.Rows.Add(new Object[] { "Vuln_Type", ".", false });  // .NET BUG: there is a nasty bug here on the .NET framework which is triggered if I add a row without the bool value ( the checkbox) 
                tbSavedAssessment_FolderName.Text = DI.config.O2TempDir;

                // set callback to view the findings

                ascx_FindingsSearchViewer1.dViewItemCallback += ascx_TraceViewer1.setTraceDataAndRefresh;
            }
        }

        public void loadAssessmentRunFileAndAddItToList(String sPathToFile, bool bCheckSourceCodeReferences)
        {
            if (Path.GetExtension(sPathToFile) == ".zip")
            {
                string tempFolder = DI.config.TempFolderInTempDirectory;
                new zipUtils().unzipFile(sPathToFile, tempFolder);
                foreach (string unzipedFile in Files.getFilesFromDir_returnFullPath(tempFolder))
                    loadAssessmentRunFileAndAddItToList(unzipedFile, bCheckSourceCodeReferences);
            }
            else
            {
                O2AssessmentData_OunceV6 oadO2AssessmentData = null;
                O2Timer tTimer = new O2Timer("Loaded SavedAssessmentFile").start();
                Analysis.loadAssessmentFile(sPathToFile, false, ref oadO2AssessmentData);
                if (oadO2AssessmentData.arAssessmentRun == null)
                {
                    DI.log.error(
                        "Serialized Saved Assessment run was null (are you sure this is an SavedAssessment file? :{0}",
                        sPathToFile);
                    return;
                }
                // Calculate Xrefs into fadAssessmentData                               
                Analysis.populateDictionariesWithXrefsToLoadedAssessment(ffFindingFilter, bDropDuplicateSmartTraces,
                                                                         bIgnoreRootCallInvocation, oadO2AssessmentData);
                if (oadO2AssessmentData != null)
                {
                    foreach (object oItem in lbLoadedAssessmentFiles.Items)
                        if (oItem.ToString() == oadO2AssessmentData.ToString())
                        {
                            DI.log.error(
                                "in loadAssessmentRunFileAndAddItToList, file is already in the list of O2AssessmentRun objects");
                            tTimer.stop();
                            return;
                        }
                    lbLoadedAssessmentFiles.Items.Add(oadO2AssessmentData);
                    //O2Forms.executeMethodThreadSafe(lbLoadedAssessmentFiles,lbLoadedAssessmentFiles.Items, "Add", new object[] { oadO2AssessmentData });
                    //lbTargetSavedAssessmentFiles.Items.Add(oadO2AssessmentData);
                }
                if (bCheckSourceCodeReferences)
                    checkIfSourceCodeReferencesAreValid();
                tTimer.stop();
                ascx_svpSearchAssessmentRun.setCheckBox_Checked(2, true);
            }
        }

        private void dgvSearchCriteria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
                buildSearchCriteriaAndExecuteSearch();
        }

        private void dgvSearchResults_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void btExecuteSearch_Click(object sender, EventArgs e)
        {
            buildSearchCriteriaAndExecuteSearch();
            ascx_svpSearchAssessmentRun.setCheckBox_Checked(3, true);
        }

        public void buildSearchCriteriaAndExecuteSearch()
        {
            var lscSearchCriteria = new List<AnalysisSearch.SearchCriteria>();
            foreach (DataGridViewRow rRow in dgvSearchCriteria.Rows)
            {
                if (rRow.Cells[0].Value != null)
                {
                    String sSearchType = rRow.Cells[0].Value.ToString();
                    if (rRow.Cells[1].Value == null || rRow.Cells[1].Value.ToString() == "")
                        rRow.Cells[1].Value = ".";
                    String sSearchText = rRow.Cells[1].Value.ToString();
                    bool bNegativeSearch = false;
                    if (rRow.Cells[2].Value != null && ((bool) rRow.Cells[2].Value))
                        bNegativeSearch = true;
                    var scSearchCriteria = new AnalysisSearch.SearchCriteria(sSearchText,
                                                                             (AnalysisSearch.SearchType)
                                                                             Enum.Parse(
                                                                                 typeof (AnalysisSearch.SearchType),
                                                                                 sSearchType),
                                                                             cbSearchOnFindingsWithNoTraces.Checked,
                                                                             bNegativeSearch);
//                    scSearchCriteria.bSearchOnFindingsWithNoTraces = cbSearchOnFindingsWithNoTraces.Checked;
                    lscSearchCriteria.Add(scSearchCriteria);
                }
            }
            AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch = ExecuteSearch(lscSearchCriteria);
            loadGuiWithSavedAssessmentSearchData(sasSavedAssessmentSearch);
        }

        public AnalysisSearch.SavedAssessmentSearch ExecuteSearch(List<AnalysisSearch.SearchCriteria> lscSearchCriteria)
        {
            if (lbLoadedAssessmentFiles.Items.Count == 0)
            {
                DI.log.error("in ExecuteSearch: There is no assessment file loaded");
                return null;
            }
            else
            {
                var lfadAssessmentData = new List<O2AssessmentData_OunceV6>();
                foreach (object oItem in lbLoadedAssessmentFiles.Items)
                {
                    lfadAssessmentData.Add((O2AssessmentData_OunceV6) oItem);
                }
                var sasSavedAssessmentSearch = new AnalysisSearch.SavedAssessmentSearch();
                sasSavedAssessmentSearch.setTargetO2AssessmentDataList(lfadAssessmentData);

                sasSavedAssessmentSearch.searchUsingCriteria(lscSearchCriteria);
                vars.set_("PreviousSearch", sasSavedAssessmentSearch);

                return sasSavedAssessmentSearch;
            }
        }

        public void loadGuiWithSavedAssessmentSearchData(AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch)
        {
            if (sasSavedAssessmentSearch != null)
            {
                String sNumberOfResults = String.Format("There were {0} results",
                                                        sasSavedAssessmentSearch.lfrFindingsResults.Count);
                DI.log.info(sNumberOfResults);
                lbNumberOfSearchResults.Text = sNumberOfResults;

                //AnalysisSearch.GUI.populateFindingsResults_TextMatches(sasSavedAssessmentSearch.lfrFindingsResults, lbSearchResults_TextMatches, cbSearchResults_TextMatches_UniqueList.Checked, cbSearchResults_TextMatches_ShowApplicationAndProjectName.Checked);

                //populateFindingsResults_Findings(sasSavedAssessmentSearch.lfrFindingsResults, cbSearchResults_Findings_Filter.Text, cbSearchResults_Findings_UniqueList.Checked);

                ascx_FindingsSearchViewer1.setSavedAssessmentSearchAndLoadData(sasSavedAssessmentSearch);
                //addSearchDataToListBox(sasSavedAssessmentSearch);
                addSearchToPreviousSearchList(sasSavedAssessmentSearch);

                //AnalysisSearch.GUI.populateWithDictionaryOfFilteredFindings_TreeView(tv_CreateSavedAssessment_PerFindingsType, dFilteredFindings);
            }
        }

        /*public void addSearchDataToListBox(AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch)
        {
            

            lbSearchResults_SearchCriteria.Items.Clear();            
            // add main search
            lbSearchResults_SearchCriteria.Items.Add(sasSavedAssessmentSearch);
            // add individual searches
            foreach (AnalysisSearch.SearchCriteria scSearchCriteria in sasSavedAssessmentSearch.lscSearchCriteria)                            
                lbSearchResults_SearchCriteria.Items.Add(scSearchCriteria);            
            // select first item (with is the main search)
            lbSearchResults_SearchCriteria.SelectedIndex = 0;             
        }*/

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            String sItemToLoad = oObject.ToString();
            if (Directory.Exists(sItemToLoad))
                loadAllXmlAssessmentFilesFromDirectory(sItemToLoad);
            if (File.Exists(sItemToLoad))
            {
                loadAssessmentRunFileAndAddItToList(sItemToLoad, true);
            }
        }

        /*  private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            return;
            switch (lbSearchResults_SearchCriteria.SelectedItem.GetType().Name)
            {
                case "SavedAssessmentSearch":
                    AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch = (AnalysisSearch.SavedAssessmentSearch)lbSearchResults_SearchCriteria.SelectedItem;
                    AnalysisSearch.GUI.populateFindingsResults_TextMatches(sasSavedAssessmentSearch.lfrFindingsResults, lbSearchResults_TextMatches, cbSearchResults_TextMatches_UniqueList.Checked, cbSearchResults_TextMatches_ShowApplicationAndProjectName.Checked);
                    populateFindingsResults_Findings(sasSavedAssessmentSearch.lfrFindingsResults,cbSearchResults_Findings_Filter.Text,cbSearchResults_Findings_UniqueList.Checked);
                    break;
                case "SearchCriteria":
                    AnalysisSearch.SearchCriteria scSearchCriteria = (AnalysisSearch.SearchCriteria)lbSearchResults_SearchCriteria.SelectedItem;
                    AnalysisSearch.GUI.populateFindingsResults_TextMatches(scSearchCriteria.lfrFindingsResults, lbSearchResults_TextMatches, cbSearchResults_TextMatches_UniqueList.Checked, cbSearchResults_TextMatches_ShowApplicationAndProjectName.Checked);
                    populateFindingsResults_Findings(scSearchCriteria.lfrFindingsResults,cbSearchResults_Findings_Filter.Text,cbSearchResults_Findings_UniqueList.Checked);
                    break;
            }
        }*/
        // populateFindingsResults_TextMatches(lfrFindingsResults,lbSearchResults_TextMatches, cbSearchResults_TextMatches_UniqueList.Checked);
        // populateFindingsResults_TextMatches(lfrFindingsResults,lbSearchResults_TextMatches, cbSearchResults_TextMatches_UniqueList.Checked);

        private void cbSearchResults_Findings_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadGuiWithLastSearch();

            //if (lbSearchResults_Findings.Tag != null)
            //{
            //    populateFindingsResults_Findings((List<AnalysisSearch.FindingsResult>)lbSearchResults_Findings.Tag, cbSearchResults_Findings_Filter.Text, cbSearchResults_Findings_UniqueList.Checked);
            //
            //           }
        }


        private void lbPreviousSearches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPreviousSearches.SelectedItem != null)
            {
                var sasSavedAssessmentSearch = (AnalysisSearch.SavedAssessmentSearch) lbPreviousSearches.SelectedItem;
                dgvSearchCriteria.Rows.Clear();
                foreach (AnalysisSearch.SearchCriteria scSearchCriteria in sasSavedAssessmentSearch.lscSearchCriteria)
                    dgvSearchCriteria.Rows.Add(new object[]
                                                   {
                                                       scSearchCriteria.stSearchType.ToString(),
                                                       scSearchCriteria.sSearchText
                                                   });
            }
        }

        public void addSearchToPreviousSearchList(
            AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch_ToAddToList)
        {
            bool bSearchAlreadyOnList = false;
            tbSavedAssessmentFileName.Text =
                sasSavedAssessmentSearch_ToAddToList.ToString().Replace('|', '_').Replace(':', '_').Replace('\\', '_').
                    Replace('/', '_') + ".xml";
            foreach (Object oItem in lbPreviousSearches.Items)
            {
                var sasSavedAssessmentSearch = (AnalysisSearch.SavedAssessmentSearch) oItem;
                if (sasSavedAssessmentSearch.ToString() == sasSavedAssessmentSearch_ToAddToList.ToString())
                    bSearchAlreadyOnList = true;
            }
            if (false == bSearchAlreadyOnList)
                lbPreviousSearches.Items.Add(sasSavedAssessmentSearch_ToAddToList);
        }

        private void dgvSearchCriteria_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }


        /*     private void lbSearchResults_TextMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSearchResults_TextMatches.SelectedItem != null)
            {
                AnalysisSearch.FindingsResult frFindingResult = (AnalysisSearch.FindingsResult)lbSearchResults_TextMatches.SelectedItem;
                ascx_TraceViewer1.setTraceDataAndRefresh(new o2.analysis.FindingViewItem(frFindingResult.fFinding, frFindingResult.sStringThatMatchedCriteria, frFindingResult, frFindingResult.oadO2AssessmentData));
            //    tvSmartTrace.Tag = 
            //    refreshSmartTraceTreeView();
                lbSearchResults_TextMatches.Focus();
            }
        }*/


        private void ascx_DropObject1_Load(object sender, EventArgs e)
        {
        }

        private void lbTargetSavedAssessmentFiles_DoubleClick(object sender, EventArgs e)
        {
            if (lbLoadedAssessmentFiles.SelectedItem != null && cbUnLoadAssessmentFilesOnDoubleClick.Checked)
                lbLoadedAssessmentFiles.Items.Remove(lbLoadedAssessmentFiles.SelectedItem);
        }

        private void btCreateAssessmentRunWithSearchResults_Click(object sender, EventArgs e)
        {
            //       if (lbSearchResults_SearchCriteria.Items.Count > 0)
            {
                var targetDir = tbSavedAssessment_FolderName.Text.Replace(@"\\", @"\");
                Files.checkIfDirectoryExistsAndCreateIfNot(targetDir);
                var targetfileName = tbSavedAssessmentFileName.Text.Replace('|', '_').Replace(':', '_').Replace('\\', '_').Replace('/','_');
                String sTargetPath = Path.Combine(targetDir,targetfileName);
                AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch =
                    ascx_FindingsSearchViewer1.sasSavedAssessmentSearch;
                // (AnalysisSearch.SavedAssessmentSearch)lbSearchResults_SearchCriteria.Items[0];
                CustomAssessmentFile.saveAssessmentSearchResultsAsNewAssessmentRunFile(sasSavedAssessmentSearch,
                                                                                       sTargetPath,
                                                                                       cbCreateFileWithAllTraces.Checked,
                                                                                       cbCreateFileWithUniqueTraces.
                                                                                           Checked,
                                                                                       cbDropDuplicateSmartTraces.
                                                                                           Checked,
                                                                                       cbIgnoreRootCallInvocation.
                                                                                           Checked);
            }
        }


        public void loadAllXmlAssessmentFilesFromDirectory(String sbLoadAllXmlFilesFromDir)
        {
            O2Timer tTimer = new O2Timer("Loading all Assessment files from Directory").start();
            lbLoadedAssessmentFiles.Items.Clear();
            var lsFilesToLoad = new List<string>();
            Files.getListOfAllFilesFromDirectory(lsFilesToLoad, sbLoadAllXmlFilesFromDir, false, "*.xml", false);
            Files.getListOfAllFilesFromDirectory(lsFilesToLoad, sbLoadAllXmlFilesFromDir, false, "*.ozasmt", false);
            DI.log.debug("in loadAllXmlAssessmentFilesFromDirectory: there {0} files to process", lsFilesToLoad.Count);
            foreach (String sFileToLoad in lsFilesToLoad)
                loadAssessmentRunFileAndAddItToList(sFileToLoad, false);

            checkIfSourceCodeReferencesAreValid();
            tTimer.stop();
        }

        public void reloadGuiWithLastSearch()
        {
            if (vars.get("PreviousSearch") != null &&
                vars.get("PreviousSearch").GetType().Name == "SavedAssessmentSearch")
            {
                var sasSavedAssessmentSearch = (AnalysisSearch.SavedAssessmentSearch) vars.get("PreviousSearch");
                loadGuiWithSavedAssessmentSearchData(sasSavedAssessmentSearch);
            }
        }

        private void cbSearchResults_TextMatches_UniqueList_CheckedChanged(object sender, EventArgs e)
        {
            cbSearchResults_Findings_Filter_SelectedIndexChanged(null, null);
            reloadGuiWithLastSearch();
        }

        private void cbSearchResults_Findings_UniqueList_CheckedChanged(object sender, EventArgs e)
        {
            cbSearchResults_Findings_Filter_SelectedIndexChanged(null, null);
            //   reloadGuiWithLastSearch();
        }


        public void clearSearchCriteria()
        {
            dgvSearchCriteria.Rows.Clear();
        }

        public void addSearchCriteria(String sSearchType, String sSearchCriteria, bool bClearSearchCriteria)
        {
            addSearchCriteria(sSearchType, sSearchCriteria, false, bClearSearchCriteria);
        }

        public void addSearchCriteria(String sSearchType, String sSearchCriteria, bool bNegativeSearch,
                                      bool bClearSearchCriteria)
        {
            if (bClearSearchCriteria)
                clearSearchCriteria();
            dgvSearchCriteria.Rows.Add(new object[] {sSearchType, sSearchCriteria, bNegativeSearch});
        }

        public void runSearch()
        {
            buildSearchCriteriaAndExecuteSearch();
        }

        private void btCreateAssessmentRun_WithSelectedFindingsType_Click(object sender, EventArgs e)
        {
            //           if (lbSearchResults_SearchCriteria.Items.Count > 0)
            //           {
            var dFilteredFindings = new Dictionary<string, List<FindingViewItem>>();
            foreach (TreeNode tnTreeNode in tv_CreateSavedAssessment_PerFindingsType.Nodes)
                if (tnTreeNode.Checked)
                    dFilteredFindings.Add(tnTreeNode.Text, (List<FindingViewItem>) tnTreeNode.Tag);

            var targetDir = tbSavedAssessment_FolderName.Text.Replace(@"\\", @"\");
            Files.checkIfDirectoryExistsAndCreateIfNot(targetDir);
            var targetfileName = tbSavedAssessmentFileName.Text.Replace('|', '_').Replace(':', '_').Replace('\\', '_').Replace('/', '_');

            String sTargetPath = Path.Combine(targetDir, targetfileName);//tbSavedAssessment_FolderName.Text, tbSavedAssessmentFileName.Text);
            AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch =
                ascx_FindingsSearchViewer1.sasSavedAssessmentSearch;
            // (AnalysisSearch.SavedAssessmentSearch)lbSearchResults_SearchCriteria.Items[0];
            CustomAssessmentFile.create_CustomSavedAssessmentRunFile_From_FindingsResult_Dictionary(dFilteredFindings,
                                                                                                    sTargetPath);
            //         }*/
        }

        public void populateFindingsResults_Findings(List<AnalysisSearch.FindingsResult> lfrFindingsResults,
                                                     String sFindingFilter, bool bUniqueList)
        {
            Dictionary<String, List<FindingViewItem>> dFilteredFindings = null;
            List<FindingViewItem> lviFindingViewItem =
                AnalysisSearch.calculateDictionaryWithFilteredFindingsResults(lfrFindingsResults, ref dFilteredFindings,
                                                                              sFindingFilter, bUniqueList);
            //AnalysisSearch.GUI.populateWithListOfFilteredFindings_ListBox(lbSearchResults_Findings, lviFindingViewItem, dFilteredFindings);
            //AnalysisSearch.GUI.populateWithListOfFilteredFindings_TreeView(tvSearchResults_Findings, lviFindingViewItem, dFilteredFindings);
            //lbSearchResults_Findings.Tag = lfrFindingsResults;
            AnalysisSearch.GUI.populateWithDictionaryOfFilteredFindings_TreeView(
                tv_CreateSavedAssessment_PerFindingsType, dFilteredFindings);
            tv_CreateSavedAssessment_PerFindingsType.Sort();
            tv_CreateSavedAssessment_PerFindingsType.Tag = lfrFindingsResults;
        }


        private void scHost_Enter(object sender, EventArgs e)
        {
            if (dgvSearchCriteria.Rows.Count == 1)
                dgvSearchCriteria.Rows.Add(new Object[] {"Vuln_Type", ".", false});
        }


        public ListBox getObject_ListBox_PreviousSearches()
        {
            return lbPreviousSearches;
        }


        public ListBox getObject_ListBox_TargetSavedAssessmentFiles()
        {
            return lbLoadedAssessmentFiles;
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
            O2Forms.executeMethodThreadSafe(this, this, "loadAssessmentRunFileAndAddItToList",
                                            new object[] {sPathToDownloadedFile, true});
        }


        public void checkIfSourceCodeReferencesAreValid()
        {
            Application.DoEvents();
            lbFixSourceCodeFilereferences.Visible = false;
            lbAssessmentFilesThatNeedSourceCodePathFixing.Visible = false;
            lbAssessmentFilesThatNeedSourceCodePathFixing.Items.Clear();
            foreach (O2AssessmentData_OunceV6 oadO2AssessmentData in lbLoadedAssessmentFiles.Items)
            {
                if (oadO2AssessmentData != null)
                    if (false == SourceCodeFiles.areAllSourceCodeReferencesInAssessmentFileValid(oadO2AssessmentData))
                    {
                        // first try to fix this file using current Source Code Mappings:
                        SourceCodeFiles.tryToFixSourceCodeReferences(oadO2AssessmentData);
                        // then check again
                        if (false ==
                            SourceCodeFiles.areAllSourceCodeReferencesInAssessmentFileValid(oadO2AssessmentData))
                            // and if there are still problems add it to the list of assessment files that need source code fixing
                            lbAssessmentFilesThatNeedSourceCodePathFixing.Items.Add(oadO2AssessmentData);
                    }
            }
            if (lbAssessmentFilesThatNeedSourceCodePathFixing.Items.Count > 0)
            {
                lbFixSourceCodeFilereferences.Visible = true;
                lbAssessmentFilesThatNeedSourceCodePathFixing.Visible = true;
            }

            //if (dgvSearchCriteria.Rows.Count == 0) // if there is no query in there (means this is the first time we are running this), so add the default search
            addSearchCriteria("Vuln_Type", ".", true);
            btExecuteSearch_Click(null, null);
        }

        private void lbAssessmentFilesThatNeedSourceCodePathFixing_DoubleClick(object sender, EventArgs e)
        {
            //ascx_SourceCodeFileReferences scfrSourceCodeFileReferences = (ascx_SourceCodeFileReferences)Exec.openNewWindowWithControl(typeof(ascx_SourceCodeFileReferences),"Fix Source Code File References");
            var scfrSourceCodeFileReferences =
                (ascx_SourceCodeFileReferences)
                O2DockPanel.loadControl(typeof (ascx_SourceCodeFileReferences), true, "Fix Source Code File References");
            scfrSourceCodeFileReferences.dCallbackToRefreshFilesMappings = checkIfSourceCodeReferencesAreValid;
            scfrSourceCodeFileReferences.loadAssessmentFile(
                (O2AssessmentData_OunceV6) lbAssessmentFilesThatNeedSourceCodePathFixing.SelectedItem);
        }

        private void scTop_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ado_RemoveFindingsFromSearches_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            DI.log.error("Not impletemented yet");
        }


        public void populateSearchType()
        {
            lbAvailableSearchType.Items.AddRange(AnalysisSearch.getListWithSearchTypes().ToArray());
            foreach (DataGridViewColumn cColumn in dgvSearchCriteria.Columns)
            {
                String asd = cColumn.Name;
            }
            if (dgvSearchCriteria.Columns["sName"] != null)
            {
                var dgvColumnComboBox = (DataGridViewComboBoxColumn) dgvSearchCriteria.Columns["sName"];
                dgvColumnComboBox.Items.AddRange(AnalysisSearch.getListWithSearchTypes().ToArray());
                //((DataGridViewComboBoxCell)dgvSearchCriteria.Columns["name"].CellTemplate).Items.AddRange(AnalysisSearch.getListWithSearchTypes().ToArray());
            }
        }

/*        private void populateFilterComboBox()
        {
            // change to ENUM
            cbSearchResults_Findings_Filter.Items.AddRange(AnalysisSearch.getListWithSearchFilters().ToArray());
            cbSearchResults_Findings_Filter.SelectedIndex = 0;
        }*/

        private void llDownloadDemoFile_FromTempDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadAllXmlAssessmentFilesFromDirectory(DI.config.O2TempDir);
        }

        private void tbSearchTextToAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addSearchCriteria(lbAvailableSearchType.SelectedItem.ToString(), tbSearchTextToAdd.Text,
                                  cbSearchTypeNegative.Checked);
        }

        private void btAddSearchCriteria_Click(object sender, EventArgs e)
        {
            addSearchCriteria(lbAvailableSearchType.SelectedItem.ToString(), tbSearchTextToAdd.Text,
                              cbSearchTypeNegative.Checked);
        }

        private void llSearchCriteria_DeleteSelectedRow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dgvSearchCriteria.SelectedRows.Count > 0)
                foreach (DataGridViewRow rRow in dgvSearchCriteria.SelectedRows)
                    dgvSearchCriteria.Rows.Remove(rRow);
            else if (dgvSearchCriteria.SelectedCells.Count == 1)
            {
                DataGridViewRow rRowToDelete = dgvSearchCriteria.Rows[dgvSearchCriteria.SelectedCells[0].RowIndex];
                dgvSearchCriteria.Rows.Remove(rRowToDelete);
            }
        }

        private void llSearchCriteria_ClearSearchCriteria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearSearchCriteria();
        }

        private void cbSearchResults_TextMatches_ShowApplicationAndProjectName_CheckedChanged(object sender, EventArgs e)
        {
            reloadGuiWithLastSearch();
        }

        /*  private void tvSearchResults_Findings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvSearchResults_Findings.SelectedNode != null)
            {
                o2.analysis.FindingViewItem fviFindingViewItem = (o2.analysis.FindingViewItem)tvSearchResults_Findings.SelectedNode.Tag;
                ascx_TraceViewer1.setTraceDataAndRefresh(fviFindingViewItem);
                tvSearchResults_Findings.Focus();
                // tvSmartTrace.Tag = fviFindingViewItem;
                // refreshSmartTraceTreeView();
            }
        }*/

        private void tvSearchResults_Findings_MouseDown(object sender, MouseEventArgs e)
        {
            /*          if (false == cbSearchResults_Findings_DeleteOnDoubleClick.Checked && lbSearchResults_Findings.SelectedItem != null)  // only start the D&D if we are not in the cbSearchResults_Findings_DeleteOnDoubleClick mode
            {
                DoDragDrop(lbSearchResults_Findings.SelectedItem, DragDropEffects.Copy);
                lbSearchResults_Findings_SelectedIndexChanged(null, null);
            }*/
        }

        private void tvSearchResults_Findings_DoubleClick(object sender, EventArgs e)
        {
            /*       if (lbSearchResults_Findings.SelectedItem != null)
            {
                if (cbSearchResults_Findings_Filter.Text == "Sink")
                {
                    addSearchCriteria("Sink_Text", lbSearchResults_Findings.Text, true, false);
                }
                // remove item from both listbox and treeview (the one used to create saved assessment files
                if (tv_CreateSavedAssessment_PerFindingsType.Nodes.ContainsKey(lbSearchResults_Findings.SelectedItem.ToString()))
                {
                    tv_CreateSavedAssessment_PerFindingsType.Nodes.RemoveByKey(lbSearchResults_Findings.SelectedItem.ToString());
                }
                lbSearchResults_Findings.Items.Remove(lbSearchResults_Findings.SelectedItem);
            }*/
        }

        private void btCreateCustomAssessmentFiles_LoadData_Click(object sender, EventArgs e)
        {
            loadDataForCreateCustomAssessmentFile();
        }

        public void loadDataForCreateCustomAssessmentFile()
        {
            List<String> lsCurrentFilters = ascx_FindingsSearchViewer1.getCurrentFilters();
            //if (lsCurrentFilters.Count > 0)
            List<string> currentFilters = ascx_FindingsSearchViewer1.getSelectedFilters();
            if (currentFilters.Count > 0)
            {
                bool bUniqueList = true;
                String sFindingFilter = currentFilters[0];
                Dictionary<String, List<FindingViewItem>> dFilteredFindings = null;
                List<FindingViewItem> lviFindingViewItem = AnalysisSearch.calculateDictionaryWithFilteredFindingsResults
                    (
                    ascx_FindingsSearchViewer1.sasSavedAssessmentSearch.lfrFindingsResults, ref dFilteredFindings,
                    sFindingFilter, bUniqueList);

                AnalysisSearch.GUI.populateWithDictionaryOfFilteredFindings_TreeView(
                    tv_CreateSavedAssessment_PerFindingsType, dFilteredFindings);
            }
        }

        private void cbIgnoreRootCallInvocation_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cbCreateFileWithUniqueTraces_CheckedChanged(object sender, EventArgs e)
        {
            cbDropDuplicateSmartTraces.Enabled = cbCreateFileWithUniqueTraces.Checked;
            cbIgnoreRootCallInvocation.Enabled = cbCreateFileWithUniqueTraces.Checked;
        }

        private void lbLoadedAssessmentFiles_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lbLoadedAssessmentFiles_DragDrop(object sender, DragEventArgs e)
        {
            loadAssessmentRunFileAndAddItToList(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e),true);
        }
    }
}
