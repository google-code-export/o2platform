// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Glee.Drawing;
using O2.DotNetWrappers.Windows;
using O2.Legacy.OunceV6.GLEEGraphWiz.GleeUtils;

namespace O2.Legacy.OunceV6.GLEEGraphWiz.Ascx
{
    public partial class ascx_GraphDataMappedToCustomRules : UserControl
    {
        public ascx_GraphDataMappedToCustomRules()
        {
            InitializeComponent();
            if (false == DesignMode)
            {
                /*       cbShowSources.Checked = true;
                cbShowSinks.Checked = true;
                cbShowVertices.Checked = false;*/
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        public void showO2GraphData(O2Graph.GraphData fgdGraphData)
        {
            lboxNodes.Items.Clear();
            foreach (DictionaryEntry deNode in fgdGraphData.gGraph.NodeMap)
            {
                var nNode = (Node) deNode.Value;
                if (false == lboxNodes.Items.Contains(nNode.Attr.Label))
                    lboxNodes.Items.Add(nNode.Attr.Label);
            }

            lBoxEdges.Items.Clear();
            foreach (Edge eEdge in fgdGraphData.gGraph.Edges)
            {
                if (false == lBoxEdges.Items.Contains(eEdge.ToString()))
                    // lBoxEdges.Items.Add(eEdge.ToString());
                    lBoxEdges.Items.Add(eEdge.Target);
            }
        }

        /*    public void populateControlsWithGraphWizData(AdjacencyGraph<String, MarkedEdge<String, String>> g)
        {
            int iIndex = 0;
            if (cbShowVertices.Checked)
            {
                dgvVertices.Columns.Clear();
                dgvVertices.Columns.Add("ID", "ID");
                dgvVertices.Columns.Add("Name", "Name");
                foreach (String sVertix in g.Vertices)
                    dgvVertices.Rows.Add(new Object[] { (iIndex++).ToString(), sVertix });
            }

            if (cbShowSinks.Checked)
            {
                dgvSinks.Columns.Clear();
                dgvSinks.Columns.Add("MethodName", "Sink");
                dgvSinks.Columns.Add("vuln_id", "vuln_id");
                dgvSinks.Columns.Add("actionObject", "actionObject");                
                dgvSinks.Columns["MethodName"].SortMode = DataGridViewColumnSortMode.Automatic;
                dgvSinks.Columns["vuln_id"].Width = 30;
                dgvSinks.Columns["actionObject"].Width = 80;

                foreach (String sVertix in g.Vertices)
                {
                    bool bInSource = false;
                    foreach (MarkedEdge<String, String> eEdge in g.Edges)
                    {
                        if (eEdge.Source == sVertix)
                            bInSource = true;
                    }
                    if (false == bInSource)

                        dgvSinks.Rows.Add(new Object[] { sVertix });
                }                
            }
            if (cbShowSources.Checked)
            {
                dgvSources.Columns.Clear();
                dgvSources.Columns.Add("MethodName", "Source");                
                dgvSources.Columns.Add("vuln_id", "vuln_id");
                dgvSources.Columns.Add("actionObject", "actionObject");
                dgvSources.Columns["MethodName"].SortMode = DataGridViewColumnSortMode.Automatic;
                dgvSources.Columns["vuln_id"].Width = 30;
                dgvSources.Columns["actionObject"].Width = 80;                
                
                foreach (String sVertix in g.Vertices)
                {
                    bool bInSource = false;
                    foreach (MarkedEdge<String, String> eEdge in g.Edges)
                    {
                        // if (eEdge.Source == sVertix)
                        if (eEdge.Target == sVertix)
                            bInSource = true;
                    }
                    if (false == bInSource)

                        dgvSources.Rows.Add(new Object[] { sVertix });
                }
            }
        }

        public void cleanDgvStats()
        {
            dgvGraphStats.Columns.Clear();
            dgvGraphStats.Columns.Add("Item", "Item");
            dgvGraphStats.Columns.Add("Value", "Value");
        }

        public void dgvStatsAddRow(String sItem, String sValue)
        {
            dgvGraphStats.Rows.Add(new Object[] { sItem, sValue });
        }

        public void showGraphStats(AdjacencyGraph<String, MarkedEdge<String, String>>  adAdjacencyGraph)
        {            
            //Application.DoEvents();

            populateControlsWithGraphWizData(adAdjacencyGraph);
            colorCodeDataGridViewItemsBasedOnLddbEntries(dgvSources);
            colorCodeDataGridViewItemsBasedOnLddbEntries(dgvSinks);
        }

        private void btResolveVdbMappingsForSources_Click(object sender, EventArgs e)
        {
            colorCodeDataGridViewItemsBasedOnLddbEntries(dgvSources);
        }

        private void btResolveVdbMappingsForSinks_Click(object sender, EventArgs e)
        {
            colorCodeDataGridViewItemsBasedOnLddbEntries(dgvSinks);
        }
        */

        /*
        public void colorCodeDataGridViewItemsBasedOnLddbEntries(DataGridView dgvTargetDataGridView)
        {
            bool bHaveResolvedDb = false;
            foreach (DataGridViewRow dgvRow in dgvTargetDataGridView.Rows)
            {
                String sMethodId = dgvRow.Cells["MethodName"].Value.ToString();
                String sDbId = cbLanguageDbId.Text;
                UInt32 uId = o2.datalayer.mysql.OunceMyql.lddb.action_getVulnIdThatMatchesSignature(sDbId, sMethodId, false);
                if (false == bHaveResolvedDb && uId > 0)
                {
                    UInt32 uLanguageDbId = o2.datalayer.mysql.OunceMyql.lddb.action_getDbIDfromVuln_id(uId);
                    if (uLanguageDbId > 0)
                    {
                        cbLanguageDbId.Text = uLanguageDbId.ToString();
                        bHaveResolvedDb = true;
                    }
                }
                dgvRow.Cells["vuln_id"].Value = uId.ToString();
                dgvRow.Cells["actionObject"].Value = o2.datalayer.mysql.OunceMyql.lddb.action_getActionObjectDetailsForVulnID(uId);
            }
        }
         */


        /*
        private void btDeleteSinkVbdActionObject_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvrSelectedRow in dgvSinks.SelectedRows)
            {
                String sVulnId = (String)dgvrSelectedRow.Cells["vuln_id"].Value;
                UInt32 uVulnId = 0;
                if (UInt32.TryParse(sVulnId, out uVulnId))
                    o2.datalayer.mysql.OunceMyql.lddb.action_deleteSignatureAndActionObject(uVulnId);
            }
            //refresh list
            colorCodeDataGridViewItemsBasedOnLddbEntries(dgvSinks);
        }

        private void btMarkAllUnmarked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in dgvSinks.Rows)
                markMethodAsSink(
                    (String)dgvRow.Cells["MethodName"].Value,
                    (String)dgvRow.Cells["vuln_id"].Value,
                    (String)dgvRow.Cells["actionObject"].Value,
                    cbLanguageDbId.Text);
        }

        private void btMarkMethodAsSink_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in dgvSinks.SelectedRows)
            {
                markMethodAsSink(
                     (String)dgvRow.Cells["MethodName"].Value,
                    (String)dgvRow.Cells["vuln_id"].Value,
                    (String)dgvRow.Cells["actionObject"].Value,
                    cbLanguageDbId.Text);
            }
        }

        private void markMethodAsSink(String sMethodName, String sVulnId, String sActionObject, String LanguageDbId)
        {

            if (sActionObject == "")
            {
                o2.datalayer.mysql.OunceMyql.lddb.action_makeMethod_Sink(LanguageDbId, sMethodName, sVulnId,true);

                //refresh list
                colorCodeDataGridViewItemsBasedOnLddbEntries(dgvSinks);
            }
            else
                 DI.log.error("Method is already mapped");
        }

        private void dgvSinks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinks.SelectedRows.Count > 0)
            {
                lbSelectedSinkMethod.Text = dgvSinks.SelectedRows[0].Cells["MethodName"].Value.ToString();
                events.raiseEvent_ShowCustomRulesDetails_MethodSignature(cbLanguageDbId.Text, dgvSinks.SelectedRows[0].Cells["MethodName"].Value.ToString());       // only show details for the first selected row
            }
        }
        */
        /* private void btMarkMethodAsCallBack_Click(object sender, EventArgs e)
        {
          //  String sMethod = dgvVertices.SelectedCells[0].Value.ToString();

          //  o2.datalayer.mysql.OunceMyql.lddb.action_makeMethodACallback(UInt32.Parse(cbLanguageDbId.Text), sMethod);
          //  return;

            if (dgvSinks.SelectedRows.Count > 0)
                o2.datalayer.mysql.OunceMyql.lddb.action_makeMethodACallback(UInt32.Parse(cbLanguageDbId.Text), lbSelectedSinkMethod.Text);
        }*/

        private void btNodes_CopyToClipboard_Click(object sender, EventArgs e)
        {
            O2Forms.copyListBoxItemsToClipboard((ListBox) lboxNodes);
        }

        private void btEdges_CopyToClipboard_Click(object sender, EventArgs e)
        {
            O2Forms.copyListBoxItemsToClipboard((ListBox) lBoxEdges);
        }
    }
}
