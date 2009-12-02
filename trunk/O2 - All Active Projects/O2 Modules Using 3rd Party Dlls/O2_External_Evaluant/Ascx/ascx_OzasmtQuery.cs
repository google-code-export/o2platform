using System;
using System.Drawing;
using System.Windows.Forms;
using O2.Views.ASCX.classes;
using O2.Views.ASCX.O2Findings;

namespace O2.External.Evaluant.Ascx
{
    public partial class ascx_OzasmtQuery : UserControl
    {
        //public string defaultQuery1 = "from O2Finding finding in o2Findings select finding.vulnName";
        //public string defaultQuery = "from O2Finding f in o2Findings select new {f.vulnType, f.vulnName}";

        
        /*public List<O2Finding> getListOfAllLoadedFindings()
        {
            var allFindings = new List<O2Finding>();
            foreach (O2Assessment o2Assessment in importedO2AssessmentFiles)
                allFindings.AddRange(o2Assessment.o2Findings);
            return allFindings;
        }*/


        public ascx_OzasmtQuery()
        {
            InitializeComponent();            
        }

        public ascx_OzasmtQuery(string ozasmtFileToLoad) :this()
        {
            loadOzasmtFile(ozasmtFileToLoad);
        }

        private void btRunNLinqQuery_Click(object sender, EventArgs e)
        {
            runNLinkQuery();
        }

        private void tbNLinqQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
                runNLinkQuery();
            }
        }

        private void tbNLinqQuery_TextChanged(object sender, EventArgs e)
        {
            checkIfQueryIsValid();
        }

        private void tbMaxRecordsToShow_TextChanged(object sender, EventArgs e)
        {
            try
            {
                maxNumberOfNLinqQueryRecordsToShow = Int32.Parse(tbMaxRecordsToShow.Text);
                if (maxNumberOfNLinqQueryRecordsToShow == 0)
                    tbMaxRecordsToShow.Text = "1000";
                tbMaxRecordsToShow.BackColor = Color.White;
            }
            catch (Exception)
            {
                DI.log.error("could not convert {0} to an int" + tbMaxRecordsToShow.Text);
                tbMaxRecordsToShow.BackColor = Color.LightPink;
            }
        }

        private void tbNLinqQuery_DragDrop(object sender, DragEventArgs e)
        {
            processDroppedObject(e);
        }
       
        private void tbNLinqQuery_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void nLinqQueryResults_DragDrop(object sender, DragEventArgs e)
        {
            processDroppedObject(e);
        }

        private void nLinqQueryResults_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void cbScriptLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNLinqQuery.Text = OzasmtLinq.getO2FindingLinqScript(cbScriptLibrary.Text);
            runNLinkQuery();
        }

        private void ascx_OzasmtQuery_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void llHideTaskControlHost_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            scMainGuiAndTasksHost.Panel2Collapsed = true;
        }
  
        private void lbDragAndDropHelpText_DragDrop(object sender, DragEventArgs e)
        {
            processDroppedObject(e);
        }

        private void lbDragAndDropHelpText_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void llLoadWebGoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebRequests.downloadFileUsingAscxDownload(O2CoreResources.DemoOzasmtFile_Hacmebank_WebGoat, loadOzasmtFile);
        }

        private void llLoadHacmeBank_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebRequests.downloadFileUsingAscxDownload(O2CoreResources.DemoOzasmtFile_Hacmebank_WebServices,
                                                      loadOzasmtFile);
        }
       
        private void btSaveResults_Click(object sender, EventArgs e)
        {
            saveCurrentFindindsAsAssessmentFile();
        }

        private void nLinqQueryResults_SelectionChanged(object sender, EventArgs e)
        {
            cbOnlySaveSelectedFindings.Visible = (nLinqQueryResults.SelectedRows.Count > 0);
        }

        private void llObjectWithAllFindings_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbAllowDragOfFindingsLinks.Checked)
                DoDragDrop(lastSearchResult, DragDropEffects.Copy);
        }
   
        private void llClearLoadedFindingsList_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            removeAllLoadedFindings();
        }

        private void llObjectWithAllFindings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //ascx_FindingsViewer.openInFloatWindow(lastSearchResult, "Findings Viewer for " + cbScriptLibrary.Text);                   
            ascx_FindingsViewer.openInFloatWindow(getListOfFindingsFromCurrentRows(),"Findings Viewer for " + cbScriptLibrary.Text);
        }

        private void nLinqQueryResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openSelectedRowsAsFindings();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openSelectedRowsAsFindings();
        }

        private void nLinqQueryResults_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lbNLinqQuery_NumberOfResults.Text = "new size: " + nLinqQueryResults.Rows.Count;
        }
    }
}