// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Evaluant.NLinq.Memory;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.External.Evaluant;
using O2.Kernel.Interfaces.O2Findings;
using O2.Views.ASCX;
using O2.Views.ASCX.classes.Tasks;
using O2.Views.ASCX.classes.TasksWrappers;
using O2.Views.ASCX.O2Findings;

namespace O2.External.Evaluant.Ascx
{
    public partial class ascx_OzasmtQuery
    {
        public static List<IO2AssessmentLoad> o2AssessmentLoadEngines  = new List<IO2AssessmentLoad>();
        public static IO2AssessmentSave o2AssessmentSave { get; set; }

        private bool runOnLoad = true;
        public List<IO2Finding> lastSearchResult = new List<IO2Finding>();
        public List<IO2Finding> loadedO2Findings = new List<IO2Finding>();
        private Dictionary<DataRow, object> mappingOfDataRowsToObjects;
        public int maxNumberOfNLinqQueryRecordsToShow { get; set; }

        private void onLoad()
        {
            if (runOnLoad && DesignMode == false)
            {
                OzasmtLinq.populateComboBoxWithO2FindingsLinqScriptLibraryTitles(cbScriptLibrary);
                if (cbScriptLibrary.Items.Count > 0)
                    cbScriptLibrary.SelectedIndex = 0;
                /*if (scriptLibrary != null && scriptLibrary.Count > 0)
                {
                    cbScriptLibrary.Items.AddRange(scriptLibrary.ToArray());
                    cbScriptLibrary.SelectedIndex = 0;
                }*/
                setViewMode(false); // Simple mode
                hideTaskHostControl();
                checkIfQueryIsValid();
                updateMaxRecordsToShow();
                updateCountOfLoadedFindings();
                cbScriptLibrary.Focus();
                runOnLoad = false;
            }
        }

        private void runNLinkQuery()
        {
            nLinqQueryResults.DataSource = null;
            if (loadedO2Findings.Count > 0)
            {
                try
                {
                    var timer = new O2Timer();
                    var results =
                        (List<object>)O2NLinkQuery.runQuery(tbNLinqQuery.Text, "o2Findings", loadedO2Findings);
                    tryToExtractO2FindingsFromResultsForOzasmtSave(results);
                    if (results == null || results.Count == 0)
                    {
                        DI.log.debug("Query result was null");
                        lbNLinqQuery_NumberOfResults.Text = "..";
                    }
                    else
                    {
                        lbNLinqQuery_NumberOfResults.Text = results.Count.ToString();
                        if (results.Count > maxNumberOfNLinqQueryRecordsToShow)
                        {
                            lbNLinqQuery_NumberOfResults.ForeColor = Color.Red;
                            lbNLinqQuery_NumberOfResults.Text += string.Format(" Results (only showing {0} findings)",
                                                                               maxNumberOfNLinqQueryRecordsToShow);
                        }
                        else
                        {
                            lbNLinqQuery_NumberOfResults.ForeColor = Color.Black;
                            lbNLinqQuery_NumberOfResults.Text += " Results ";
                        }
                        nLinqQueryResults.DataSource = OzasmtLinq.getDataTableFromO2FindingsLinqQuery(results,
                                                                                                      maxNumberOfNLinqQueryRecordsToShow,
                                                                                                      ref
                                                                                                          mappingOfDataRowsToObjects);
                        //finding => new [] {results});
                    }
                    lbNLinqQuery_ExecutionTime.Text = timer.stop();
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, " in executeNLinkQuery");
                }
            }
        }

        private void tryToExtractO2FindingsFromResultsForOzasmtSave(ICollection<object> results)
        {
            lastSearchResult = new List<IO2Finding>();
            IEnumerable<Variant> o2FindingsVariant = results.OfType<Variant>();

            foreach (Variant item in o2FindingsVariant)
            {
                lastSearchResult.AddRange(item.members.Values.OfType<IO2Finding>());
                /*var findings = item.members.Values.OfType<O2Finding>();
                foreach (var finding in findings)
                //if (item is O2Finding)
                    lastSearchResult.Add(finding);*/
            }

            lastSearchResult.AddRange(results.OfType<IO2Finding>());
            /*var o2Findings = results.OfType<O2Finding>();

            foreach (var o2Finding in o2Findings)
            {
                lastSearchResult.Add(o2Finding);
            }*/
            DI.log.info("There are {0} findings that can be saved", lastSearchResult.Count);

            if (lastSearchResult.Count > results.Count)
                DI.log.error(
                    "Something went wrong on the tryToExtractO2FindingsFromResultsForOzasmtSave calculation since we have more findings in lastSearchResult than in the original results collection!");
            else if (lastSearchResult.Count < results.Count && lastSearchResult.Count > 0)
                DI.log.error(
                    "Something went wrong on the tryToExtractO2FindingsFromResultsForOzasmtSave calculation since we have LESS findings in lastSearchResult than in the original results collection!");

            btSaveResults.Visible = (lastSearchResult.Count == results.Count);
            // var obe = results.<O2Finding>();
            // lastSearchResult = (List<O2Finding>)results.OfType<O2Finding>();
        }

        public void checkIfQueryIsValid()
        {
            if (tbNLinqQuery.InvokeRequired)
                tbNLinqQuery.Invoke(new EventHandler(delegate { checkIfQueryIsValid(); }));
            else
                tbNLinqQuery.BackColor = O2NLinkQuery.IsQueryValid(
                                             tbNLinqQuery.Text, "o2Findings",
                                             new List<O2Finding>(new[] { new O2Finding() }),
                                             cbShowCompilationErrorDetails.Checked)
                                             ?
                                                 Color.PaleGreen
                                             : Color.MistyRose;
        }

        private void processDroppedObject(DragEventArgs e)
        {
            string file = Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);
            if (!string.IsNullOrEmpty(file))
                loadOzasmtFile(file);
            else
            {
                object droppedObject = Dnd.tryToGetObjectFromDroppedObject(e);
                if (droppedObject is List<IO2Finding>)
                    loadO2Findings((List<IO2Finding>)droppedObject);
                else if (droppedObject is IO2Assessment)
                    loadO2Assessment(((IO2Assessment)droppedObject));
            }
        }

        public void loadOzasmtFile(string assessmentFile)
        {
            if (scMainGuiAndTasksHost.InvokeRequired)
                scMainGuiAndTasksHost.Invoke(new EventHandler(delegate { loadOzasmtFile(assessmentFile); }));
            else
            {
                scMainGuiAndTasksHost.Panel2Collapsed = false;

                if (Path.GetExtension(assessmentFile) == ".zip")
                    TaskUtils.unzipFileAndInvokeCallback(assessmentFile, taskHostControl, loadOzasmtFile);
                else
                {
                    scMainGuiAndTasksHost.Panel2MinSize = 60;
                    var o2AssessmentLoadEngine = getAssessmentLoadEngine(assessmentFile);
                    if (o2AssessmentLoadEngine!= null)
                        TaskUtils.executeTask(new Task_LoadAssessmentFiles(o2AssessmentLoadEngine, assessmentFile), taskHostControl,
                                          results =>
                                              {
                                                  if (results is List<IO2Finding>)
                                                      loadedO2Findings.AddRange((List<IO2Finding>)results);
                                                  if (results is IO2Assessment)
                                                      loadedO2Findings.AddRange(
                                                          ((IO2Assessment)results).o2Findings);

                                                  updateCountOfLoadedFindings();

                                                  hideTaskHostControl();
                                              });

                    //            var o2AssessmentFile = new O2Assessment(assessmentFile);
                }
                cbScriptLibrary.Focus();
            }
        }

        private static IO2AssessmentLoad getAssessmentLoadEngine(string assessmentFile)
        {
            foreach (var o2AssessmentLoadEngine in o2AssessmentLoadEngines)
                if (o2AssessmentLoadEngine.canLoadFile(assessmentFile))
                    return o2AssessmentLoadEngine;
            return null;
        }

        public void loadO2Assessment(IO2Assessment o2Assessment)
        {
            loadedO2Findings.AddRange(o2Assessment.o2Findings);
            updateCountOfLoadedFindings();
        }

        public void loadO2Findings(List<IO2Finding> o2Findings)
        {
            updateCountOfLoadedFindings();
        }

        public void updateCountOfLoadedFindings()
        {
            if (lbNumberOfFindingsObjectsLoaded.InvokeRequired)
                lbNumberOfFindingsObjectsLoaded.Invoke(new EventHandler(delegate { updateCountOfLoadedFindings(); }));
            else
            {
                lbNumberOfFindingsObjectsLoaded.Text = loadedO2Findings.Count.ToString();
                if (loadedO2Findings.Count > 0)
                {
                    runNLinkQuery();
                    lbDragAndDropHelpText.Visible = false;
                }
                //scQueryAndResults.Panel1.Enabled = (loadedO2Findings.Count > 0);
                cbScriptLibrary.Enabled = (loadedO2Findings.Count > 0);
            }
        }

        private void setViewMode(bool viewMode) // true is simple mode where only the results are shown
        {
            scQueryAndResults.Panel1Collapsed = viewMode;
            llChangeViewMode.Text = (scQueryAndResults.Panel1Collapsed) ? "Show Query" : "Hide Query";
        }

        public void hideTaskHostControl()
        {
            if (scMainGuiAndTasksHost.InvokeRequired)
                scMainGuiAndTasksHost.Invoke(new EventHandler(delegate { hideTaskHostControl(); }));
            else
                scMainGuiAndTasksHost.Panel2Collapsed = true;
        }

        private void updateMaxRecordsToShow()
        {
            if (tbMaxRecordsToShow.InvokeRequired)
                tbMaxRecordsToShow.Invoke(new EventHandler(delegate { updateMaxRecordsToShow(); }));
            else
                tbMaxRecordsToShow.Text = maxNumberOfNLinqQueryRecordsToShow.ToString();
        }

        public void removeAllLoadedFindings()
        {
            if (nLinqQueryResults.InvokeRequired)
                nLinqQueryResults.Invoke(new EventHandler(delegate { removeAllLoadedFindings(); }));
            else
                loadedO2Findings.Clear();
            nLinqQueryResults.DataSource = null;
            lbNumberOfFindingsObjectsLoaded.Text = "0";
            lbNLinqQuery_ExecutionTime.Text = "..";
            lbNLinqQuery_NumberOfResults.Text = "..";
        }

        public void saveCurrentResultsInOzasmtFile(string fileToSaveFindings)
        {
            if (lastSearchResult.Count > 0)
            {
                new O2Assessment{ o2Findings = lastSearchResult }.save(o2AssessmentSave,fileToSaveFindings);
                DI.log.debug("{0} findings where saved to file: {1}", lastSearchResult.Count, fileToSaveFindings);
            }
        }

        public void runAllQueriesAndSaveItsResults()
        {
            foreach (string script in cbScriptLibrary.Items)
            {
                DI.log.info("Script to execute: {0}", script);
                cbScriptLibrary.Text = script;
                runNLinkQuery();
                string tempFile = DI.config.getTempFileInTempDirectory("ozasmt");
                string fileToSaveFindings = Path.Combine(Path.GetDirectoryName(tempFile),
                                                         cbScriptLibrary.Text + "  -  " + Path.GetFileName(tempFile));
                saveCurrentResultsInOzasmtFile(fileToSaveFindings);
            }
        }

        public void saveCurrentFindindsAsAssessmentFile()
        {
            string tempFile = DI.config.getTempFileInTempDirectory("ozasmt");
            string fileToSaveFindings = O2Forms.askUserForFileToSave(Path.GetDirectoryName(tempFile),
                                                                     cbScriptLibrary.Text + "  -  " +
                                                                     Path.GetFileName(tempFile));
            if (fileToSaveFindings != "")
                saveCurrentResultsInOzasmtFile(fileToSaveFindings);
        }

        public List<IO2Finding> getListOfFindingsFromCurrentRows()
        {
            var o2Findings = new List<IO2Finding>();
            try
            {
                foreach (DataGridViewRow dataGridViewRow in nLinqQueryResults.Rows)
                    o2Findings.AddRange(getListOfFidingsFromRow(dataGridViewRow));
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in getListOfFindingsFromCurrentRows");
            }
            return o2Findings;
        }

        public List<IO2Finding> getListOfFindingsFromSelectedRows()
        {
            var o2Findings = new List<IO2Finding>();
            try
            {
                if (mappingOfDataRowsToObjects != null && nLinqQueryResults.SelectedRows.Count > 0)
                    if (nLinqQueryResults.SelectedRows[0].DataBoundItem is DataRowView)
                        foreach (DataGridViewRow dataGridViewRow in nLinqQueryResults.SelectedRows)
                            o2Findings.AddRange(getListOfFidingsFromRow(dataGridViewRow));
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in getListOfFindingsFromSelectedRows");
            }
            return o2Findings;
        }

        public List<IO2Finding> getListOfFidingsFromRow(DataGridViewRow dataGridViewRow)
        {
            var o2Findings = new List<IO2Finding>();
            //var dataRowView = (DataRowView) nLinqQueryResults.SelectedRows[0].DataBoundItem;
            var dataRowView = (DataRowView)dataGridViewRow.DataBoundItem;
            if (mappingOfDataRowsToObjects.ContainsKey(dataRowView.Row))
            {
                object findingObject = mappingOfDataRowsToObjects[dataRowView.Row];
                if (findingObject is IO2Finding)
                    o2Findings.Add((IO2Finding)findingObject);
                else if (findingObject is Variant)
                    o2Findings.AddRange(((Variant)findingObject).members.Values.OfType<IO2Finding>());
            }
            return (o2Findings);
        }

        public void openSelectedRowsAsFindings()
        {
            List<IO2Finding> o2Findings = getListOfFindingsFromSelectedRows();
            if (o2Findings.Count == 1)
                ascx_FindingEditor.openInFloatWindow(o2Findings[0]);
            else
                ascx_FindingsViewer.openInFloatWindow(o2Findings);            
        }

        public void loadSourceCodeFile(string sPathToSourceCodeFileToLoad)
        {
            sourceCodeEditor.loadSourceCodeFile(sPathToSourceCodeFileToLoad);
        }

        public void loadSampleScripts(Type resourcesObjectWithSampleScripts)
        {
            sourceCodeEditor.loadSampleScripts(resourcesObjectWithSampleScripts);
        }
    }
}
