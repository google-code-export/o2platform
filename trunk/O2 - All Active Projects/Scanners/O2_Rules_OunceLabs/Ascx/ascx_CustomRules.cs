// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Rules.OunceLabs.DataLayer;
using O2.Rules.OunceLabs.DataLayer_OunceV6;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_CustomRules : UserControl
    {
        private readonly Dictionary<String, DataGridView> dLddbDataGridViews = new Dictionary<string, DataGridView>();

        public ascx_CustomRules()
        {
            InitializeComponent();
            // TO DO: add dispose code and check that we are not registering more than once for this event
            MySqlEvents.eShowCustomRulesDetails_MethodSignature +=
                ascx_CustomRules_eShowCustomRulesDetails_MethodSignature;
        }

        private void ascx_CustomRules_eShowCustomRulesDetails_MethodSignature(String sDbId, String sMethodSignature)
        {
            O2Forms.setObjectTextValueThreadSafe(sDbId, cbEditCustomRules_DbId);
            O2Forms.setObjectTextValueThreadSafe(sDbId, lbCurrentMethodDbId);
            O2Forms.setObjectTextValueThreadSafe(sMethodSignature, lbCurrentMethodSignature);
            //   updateRelatedActionObjectDataGridView(sVar);
        }


        private void lbCurrentMethodSignature_TextChanged(object sender, EventArgs e)
        {
            UInt32 uVulnId = Lddb_OunceV6.action_getVulnIdThatMatchesSignature(lbCurrentMethodDbId.Text,
                                                                       lbCurrentMethodSignature.Text, false);
            lbSelectedMethodVulnId.Text = uVulnId.ToString();
            lbSelectedActionObjectId.Text = "";

            if (uVulnId > 0)
                populateTableWithMethodSignatureAndActionObjectId(lbCurrentMethodDbId.Text, lbSelectedMethodVulnId.Text,
                                                                  lbSelectedActionObjectId.Text);
            else
            {
                lbSelectedActionObjectId.Text = "";
                dgvLddb_actionobjects.Columns.Clear();
                dgvLddb_rec.Columns.Clear();
            }
        }


        private void deleteCustomRule(int iCustomRuleId)
        {
        }


        private void dtDeleteCustomRule_Click(object sender, EventArgs e)
        {
            DI.log.debug("Not implemented at the moment");
            /*   if (dgvCustomRules.SelectedCells.Count > 0)
                {
                    object a = dgvCustomRules.SelectedCells[0].Value;
                    UInt32 iCustomRuleToDelete = (UInt32)dgvCustomRules.SelectedCells[0].Value;
                    String strStep1 = String.Format("DELETE from rec 		 WHERE vuln_id={0}", iCustomRuleToDelete);
                    o2.ounce.datalayer.mysql.OunceMySql.executeSqlQuery(strStep1,true);

                    String strStep2 = String.Format("DELETE from property_xref  WHERE db_ref=2 and object_ref={0} and object_type='vulnerability' and added=true", iCustomRuleToDelete);
                    o2.ounce.datalayer.mysql.OunceMySql.executeSqlQuery(strStep1,true);
                    //String strStep3 = "DELETE from taint_info WHERE id=38006";
                }
                */
        }

        private void populateTableWithMethodSignatureAndActionObjectId(String sDbId, String sVulnId,
                                                                       String sActionObjectId)
        {
            OunceMySql.populateDataGridViewWithLddbData("rec", sDbId, "vuln_id", sVulnId, dgvLddb_rec);
            OunceMySql.populateDataGridViewWithLddbData("actionobjects", sDbId, "vuln_id", sVulnId, dgvLddb_actionobjects);
            if (dgvLddb_actionobjects.Rows.Count == 0)
                //  if there are no actionObjects for this entry force the update (for example the case with taint propagators
                updateTablesWithRelatedActionObjectId();
        }

        private void populateDataGridViewWithTableMapppingToActionObjectId(String sTableToSee, String sActionObjectId)
        {
            OunceMySql.populateDataGridViewWithLddbData(sTableToSee, "", "ao_id", sActionObjectId,
                                                       dLddbDataGridViews[sTableToSee]);
        }

        //private void updateRelatedActionObjectDataGridView(String sMethodSignature)
        //{
        //    o2.ounce.datalayer.mysql.OunceMySql.populateDataGridView_MethodSignature(sMethodSignature, dgvCustomRulesForMethodSignature);            
        //   dgvEditCustomRules_relatedActionObjects.Enabled = false;

        /*            if (rbEditCustomRule_ActionObject.Checked)
                    {
                        String sActionObjectString = rbEditCustomRule_ActionObject.Text;
                        String[] sSplittedActionObject = sActionObjectString.Split(new String[] { ":" }, StringSplitOptions.None);
                        if (sSplittedActionObject.Length == 2)
                        {
                            UInt32 uActionObjectID = UInt32.Parse(sSplittedActionObject[1].Trim());
                            o2.ounce.datalayer.mysql.OunceMySql.populateDataGridView_ExistentActionsObjectsForActionObject(uActionObjectID, dgvEditCustomRules_relatedActionObjects);
                        }
                    }
                    else if (rbEditCustomRule_Source.Checked)
                        o2.ounce.datalayer.mysql.OunceMySql.populateDataGridView_ExistentActionsObjectsForSignature(rbEditCustomRule_Source.Text, dgvEditCustomRules_relatedActionObjects, true);
                    else if (rbEditCustomRule_Sink.Checked)
                        o2.ounce.datalayer.mysql.OunceMySql.populateDataGridView_ExistentActionsObjectsForSignature(rbEditCustomRule_Sink.Text, dgvEditCustomRules_relatedActionObjects, true);

                    if (dgvEditCustomRules_relatedActionObjects.Rows.Count > 0)
                        dgvEditCustomRules_relatedActionObjects.Enabled = true;
         * */
        //        }


        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
        }

        private void btEditCustomRule_markSinkAsTaintPropagator_Click(object sender, EventArgs e)
        {
        }

        private void btDeleteSourceActionObject_Click(object sender, EventArgs e)
        {
        }

        private void btMarkMethodAsSink_Click(object sender, EventArgs e)
        {
            String sMethodSignature = lbCurrentMethodSignature.Text;
            String sVulnId = lbSelectedMethodVulnId.Text;
            String sActionObjectSignature = cbEditCustomRules_Signature.Text;
            String sSeverity = cbEditCustomRules_Severity.Text;
            String sLanguageId = cbEditCustomRules_DbId.Text;
            String sVulnType = cbEditCustomRules_vuln_type.Text;

            Lddb_OunceV6.action_makeMethod_Sink(sLanguageId, sMethodSignature, sVulnId, sActionObjectSignature, sSeverity,
                                        sVulnType, true);
        }

        private void btDeleteSelectedActionObject_Click(object sender, EventArgs e)
        {
            DI.log.debug("Deleting all objects realted to vuln_id: {0}", lbSelectedMethodVulnId.Text);
            UInt32 uVulnId = 0;
            if (UInt32.TryParse(lbSelectedMethodVulnId.Text, out uVulnId))
            {
                Lddb_OunceV6.action_deleteSignatureAndActionObject(uVulnId);
                lbSelectedMethodVulnId.Text = "";
                lbCurrentMethodDbId.Text = "";
                lbSelectedActionObjectId.Text = "";
            }
            /*

             DI.log.debug("Deleting ActionObject Object for Custom rule: \n" +
                            "MethodSignature: {0}" +
                            "MethodVulnId: {1}" +
                            "Db_id: {2}" +
                            "ActionObjectID: {3}" +
                            "ActionObjectSignature: {4}",
                            lbCurrentMethodSignature.Text, lbSelectedMethodVulnId.Text,lbCurrentMethodDbId.Text,  lbSelectedActionObjectId.Text,
                            dgvLddb_actionobjects.SelectedRows[0].Cells["signature"].Value.ToString()
                            );
            Lddb_OunceV6.action_DeleteActionId(UInt32.Parse(lbSelectedActionObjectId.Text));            
             */
            populateTableWithMethodSignatureAndActionObjectId(lbCurrentMethodDbId.Text, lbSelectedMethodVulnId.Text,
                                                              lbSelectedActionObjectId.Text);
        }

        private void ascx_DropObject1_Load(object sender, EventArgs e)
        {
        }


        private void btBuildTablesView_Click(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        public void buildDynamicLddbTableView()
        {
            var lsLddbTablesToProcess = new List<string>();
            foreach (Control cControl in scActionObjectsAndSelectViews.Panel2.Controls)
                if ("CheckBox" == cControl.GetType().Name)
                {
                    var cbCheckBox = (CheckBox) cControl;
                    if (cbCheckBox.Checked)
                        lsLddbTablesToProcess.Add(cbCheckBox.Text);
                }

            gbLddbTables.Controls.Clear();
            dLddbDataGridViews.Clear();

            Control cHostControl = gbLddbTables;

            if (lsLddbTablesToProcess.Count > 0)
            {
                // Int32 iRowsToBuild = lsLddbTablesToProcess.Count / 2 + 1;
                var lVerticalSplitContainersWithTables = new List<SplitContainer>();
                // for (int iRow = 0; iRow < iRowsToBuild; iRow++)
                while (lsLddbTablesToProcess.Count > 0)
                {
                    if (lsLddbTablesToProcess.Count == 1)
                        lVerticalSplitContainersWithTables.Add(
                            createVerticalSplitContainerForLddbTables(lsLddbTablesToProcess[0], null));
                    else
                    {
                        lVerticalSplitContainersWithTables.Add(
                            createVerticalSplitContainerForLddbTables(lsLddbTablesToProcess[0], lsLddbTablesToProcess[1]));
                        lsLddbTablesToProcess.RemoveAt(0);
                    }
                    lsLddbTablesToProcess.RemoveAt(0);
                }
                // Int32 iColumnsToBuild = lVerticalSplitContainersWithTables.Count / 2 + 1;


                /*          splitContainer1.Panel1.Controls.Clear();
                          splitContainer1.Panel2.Controls.Clear();
                          if (lVerticalSplitContainersWithTables.Count == 1)
                          {
                              splitContainer1.Panel1.Controls.Add(lVerticalSplitContainersWithTables[0]);
                            //  Label lLabel = new Label();
                            //  lLabel.Text = "aaaaaaaaaaaaaaaa";
                            //  splitContainer1.Panel1.Controls.Add(lLabel);
                          }
                          if (lVerticalSplitContainersWithTables.Count == 2)
                          {
                              splitContainer1.Panel1.Controls.Add(lVerticalSplitContainersWithTables[0]);
                              splitContainer1.Panel2.Controls.Add(lVerticalSplitContainersWithTables[1]);
                          }*/

                //        cHostControl.Controls.Add(lVerticalSplitContainersWithTables[0]);

                //for (int iColumn = 0; iColumn < iColumnsToBuild; iColumn++)
                while (lVerticalSplitContainersWithTables.Count > 0)
                {
                    if (lVerticalSplitContainersWithTables.Count == 1)
                        cHostControl.Controls.Add(lVerticalSplitContainersWithTables[0]);
                        //createHorizontalSplitContainerForLddbTables(cHostControl, lVerticalSplitContainersWithTables[0], null);
                    else
                        createHorizontalSplitContainerForLddbTables(ref cHostControl,
                                                                    lVerticalSplitContainersWithTables[0]);
                    lVerticalSplitContainersWithTables.RemoveAt(0);
                }
                // foreach (String sLddbTable in lsLddbTablesToProcess)
                // {
                //
                //               }
            }
        }

        public void createHorizontalSplitContainerForLddbTables(ref Control cHostControl, SplitContainer sTopPPanel)
        {
            var scHorizontalSplitContainer = new SplitContainer();
            scHorizontalSplitContainer.Dock = DockStyle.Fill;
            scHorizontalSplitContainer.BorderStyle = BorderStyle.Fixed3D;
            scHorizontalSplitContainer.Orientation = Orientation.Horizontal;
            scHorizontalSplitContainer.Panel1.Controls.Add(sTopPPanel);
            cHostControl.Controls.Add(scHorizontalSplitContainer);
            cHostControl = scHorizontalSplitContainer.Panel2;
        }

        public SplitContainer createVerticalSplitContainerForLddbTables(String sLeftPanelTable, String sRightPanelTable)
        {
            var sVerticalSplitContainer = new SplitContainer();
            sVerticalSplitContainer.BorderStyle = BorderStyle.Fixed3D;
            sVerticalSplitContainer.Dock = DockStyle.Fill;
            if (sRightPanelTable == null)
                populateSplitContainerPanelWithLddbTableData(sVerticalSplitContainer.Panel1, sLeftPanelTable);
            else
            {
                populateSplitContainerPanelWithLddbTableData(sVerticalSplitContainer.Panel1, sLeftPanelTable);
                populateSplitContainerPanelWithLddbTableData(sVerticalSplitContainer.Panel2, sRightPanelTable);
            }
            return sVerticalSplitContainer;
        }

        public void populateSplitContainerPanelWithLddbTableData(Panel pTargetPanel, String sLddbTableName)
        {
            var lTableName = new Label();
            lTableName.Text = sLddbTableName;
            var dgvcsDataGridViewCellStyle = new DataGridViewCellStyle();
            var dgvLddbTable = new DataGridView();
            dgvcsDataGridViewCellStyle.BackColor = Color.Black;
            dgvcsDataGridViewCellStyle.SelectionBackColor = Color.Gray;
            dgvLddbTable.AlternatingRowsDefaultCellStyle = dgvcsDataGridViewCellStyle;
            dgvLddbTable.DefaultCellStyle = dgvcsDataGridViewCellStyle;
            dgvLddbTable.RowsDefaultCellStyle = dgvcsDataGridViewCellStyle;
            dgvLddbTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLddbTable.Left = 5;
            dgvLddbTable.Top = 20;
            dgvLddbTable.Width = pTargetPanel.Width - dgvLddbTable.Left*2;
            dgvLddbTable.Height = pTargetPanel.Height - dgvLddbTable.Top - 10;
            dgvLddbTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            dLddbDataGridViews.Add(sLddbTableName, dgvLddbTable);
            pTargetPanel.Controls.Add(dgvLddbTable);

            pTargetPanel.Controls.Add(lTableName);
        }

        private void cbLddb_rec_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_actionobjects_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_io_info_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_taint_info_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_ao_options_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cdLddb_validation_descriptor_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_sink_info_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_source_info_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_stored_writeable_alias_info_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void cbLddb_writes_through_info_CheckedChanged(object sender, EventArgs e)
        {
            buildDynamicLddbTableView();
        }

        private void dgvLddb_rec_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DI.log.error("dgvSqlQueryResults_DataError: {0}", e.Exception.Message);
        }

        private void ascx_CustomRules_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                populateEditCustomRulesComboBoxes();
                buildDynamicLddbTableView();
            }
        }

        private void dgvLddb_actionobjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLddb_actionobjects.SelectedRows.Count == 1)
                if (dgvLddb_actionobjects.SelectedRows[0].Cells["id"] != null)
                {
                    lbSelectedActionObjectId.Text = dgvLddb_actionobjects.SelectedRows[0].Cells["id"].Value.ToString();
                    updateTablesWithRelatedActionObjectId();
                }
        }

        private void updateTablesWithRelatedActionObjectId()
        {
            foreach (String sTableName in dLddbDataGridViews.Keys)
            {
                switch (sTableName)
                {
                    case "stored_writeable_alias_info":
                    case "taint_info":
                    case "writes_through_info":
                        OunceMySql.populateDataGridViewWithLddbData(sTableName, lbCurrentMethodDbId.Text, "vuln_id",
                                                                   lbSelectedMethodVulnId.Text,
                                                                   dLddbDataGridViews[sTableName]);
                        break;
                    case "actionobjects":
                    case "rec":
                        break;
                    case "validation_descriptor":
                        OunceMySql.populateDataGridViewWithLddbData(sTableName, lbCurrentMethodDbId.Text, "record_id",
                                                                   lbSelectedMethodVulnId.Text,
                                                                   dLddbDataGridViews[sTableName]);
                        break;
                    default:
                        if (lbSelectedActionObjectId.Text != "")
                            populateDataGridViewWithTableMapppingToActionObjectId(sTableName,
                                                                                  lbSelectedActionObjectId.Text);
                        else
                            dLddbDataGridViews[sTableName].Columns.Clear();
                        //               populateDataGridViewWithTableMapppingToActionObjectId(sTableName, "-1");            // query with no results
                        break;
                }
            }
        }

        public void populateEditCustomRulesComboBoxes()
        {
            UInt32[] auDbId = Lddb_OunceV6.action_getDistinct_db_id();
            UInt32[] auTraces = Lddb_OunceV6.action_getDistinct_trace(true);
            String[] sVulnTypes = Lddb_OunceV6.action_getDistinct_vuln_type(true);
            String[] sSignatures = Lddb_OunceV6.action_getDistinct_signature(true);
            String[] sSeverity = Lddb_OunceV6.action_getDistinct_severity(true);

            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_DbId, auDbId);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_Trace, auTraces);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_vuln_type, sVulnTypes);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_Signature, sSignatures);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_Severity, sSeverity);
        }

        private void tpCurrentRules_Click(object sender, EventArgs e)
        {
        }

        private void cbEditCustomRules_vuln_type_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dgvLddb_rec_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if ("System.Windows.Forms.DataGridViewImageCell" == e.Column.CellType.ToString())
            {
                ((DataGridView) sender).Columns.Remove(e.Column);
                //     DI.log.debug("{0} column is of DataGridViewImageCell type, so it will be deleted", e.Column.Name);
            }
        }
    }
}
