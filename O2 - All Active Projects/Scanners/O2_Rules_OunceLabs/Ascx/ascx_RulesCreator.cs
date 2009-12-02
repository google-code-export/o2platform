// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Rules.OunceLabs.DataLayer;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_RulesCreator : UserControl
    {
        public ascx_RulesCreator()
        {
            InitializeComponent();
        }

        private void createRulesForSignatures(ruleType rRuleType)
        {
            var dgvRowsToRemove = new List<DataGridViewRow>();
            String sVuln_id, sActionObjectSignature, sSeverity, sVuln_type;
            foreach (DataGridViewRow dgvRow in dgvTargetMethods.Rows)
            {
                UInt32 uDbId;
                if (UInt32.TryParse(dgvRow.Cells["db_id"].Value.ToString(), out uDbId))
                {
                    String sMethodSignature = dgvRow.Cells["signature"].Value.ToString();
                    // String sVulnName = "";                    

                    switch (rRuleType)
                    {
                        case ruleType.TaintPropagator:
                            if (Lddb_OunceV6.action_makeMethod_TaintPropagator(uDbId, sMethodSignature,"all","all","1"))
                                dgvRowsToRemove.Add(dgvRow);
                            break;

                        case ruleType.Callback:
                            if (Lddb_OunceV6.action_makeMethodACallback(uDbId, sMethodSignature))
                                dgvRowsToRemove.Add(dgvRow);
                            break;
                        case ruleType.Source:
                            sVuln_id = "0";
                            sActionObjectSignature = "InputAnyTainted";
                            sSeverity = "High";
                            sVuln_type = "Vulnerability.F1.Source";
                            if (cbEditCustomRules_vuln_type.Text != "")
                                sVuln_type = cbEditCustomRules_vuln_type.Text;
                            if (Lddb_OunceV6.action_makeMethod_Source(uDbId.ToString(), sMethodSignature, sVuln_id,
                                                              sActionObjectSignature, sSeverity, sVuln_type))
                                dgvRowsToRemove.Add(dgvRow);
                            break;
                        case ruleType.Sink:
                            sVuln_id = "0";
                            sActionObjectSignature = "OutputAnyNotValidated";
                            sSeverity = "High";
                            sVuln_type = "Vulnerability.F1.Sink";
                            if (cbEditCustomRules_vuln_type.Text != "")
                                sVuln_type = cbEditCustomRules_vuln_type.Text;
                            if (Lddb_OunceV6.action_makeMethod_Sink(uDbId.ToString(), sMethodSignature, sVuln_id,
                                                            sActionObjectSignature, sSeverity, sVuln_type, true))
                                dgvRowsToRemove.Add(dgvRow);
                            break;
                        case ruleType.Validator:
                            if (Lddb_OunceV6.action_makeMethod_Validator(uDbId, sMethodSignature))
                                dgvRowsToRemove.Add(dgvRow);
                            break;
                        case ruleType.NotPropagateTaint:
                            if (Lddb_OunceV6.action_makeMethod_NotPropagateTaint(uDbId, sMethodSignature))
                                dgvRowsToRemove.Add(dgvRow);
                            break;
                        default:
                            break;
                    }
                }
                else
                    DI.log.error("in btMarkMethodsAs_TaintPropagator_Click error converting {0} to an UInt32",
                                 dgvRow.Cells["Db_id"].Value.ToString());
            }

            foreach (DataGridViewRow dgvRowToRemove in dgvRowsToRemove)
                dgvTargetMethods.Rows.Remove(dgvRowToRemove);
        }

        public void btMarkMethodsAs_TaintPropagator_Click(object sender, EventArgs e)
        {
            createRulesForSignatures(ruleType.TaintPropagator);
        }

        public void btMarkMethodsAs_Validator_Click(object sender, EventArgs e)
        {
            createRulesForSignatures(ruleType.Validator);
        }

        public void btMarkMethodsAs_NotPropagateTaint_Click(object sender, EventArgs e)
        {
            createRulesForSignatures(ruleType.NotPropagateTaint);
        }

        // need to make this method generic since apart from the call to lddb everything is the same
        public void btMarkMethodsAs_Callback_Click(object sender, EventArgs e)
        {
            createRulesForSignatures(ruleType.Callback);
        }

        public void btMarkMethodsAs_Sink_Click(object sender, EventArgs e)
        {
            createRulesForSignatures(ruleType.Sink);
        }

        public void btMarkMethodsAs_Source_Click(object sender, EventArgs e)
        {
            createRulesForSignatures(ruleType.Source);
        }

        public void ascx_OunceRulesCreator_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
                populateEditCustomRulesComboBoxes();
        }

        public void populateEditCustomRulesComboBoxes()
        {
            UInt32[] auTraces = Lddb_OunceV6.action_getDistinct_trace(true);
            String[] sVulnTypes = Lddb_OunceV6.action_getDistinct_vuln_type(true);
            String[] sSignatures = Lddb_OunceV6.action_getDistinct_signature(true);
            String[] sSeverity = Lddb_OunceV6.action_getDistinct_severity(true);

            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_Trace, auTraces);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_vuln_type, sVulnTypes);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_Signature, sSignatures);
            O2Forms.populateControlItemCollectionWithArray(cbEditCustomRules_Severity, sSeverity);
        }

        public void clearTargetslist()
        {
            dgvTargetMethods.Rows.Clear();

            dgvTargetMethods.Columns["db_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTargetMethods.Columns["db_id"].Width = 35;
        }

        public void addMethodToTargetsList(String sDb_id, String sSignature, bool bClearList)
        {
            if (bClearList)
                clearTargetslist();
            addMethodToTargetsList(sDb_id, sSignature);
        }

        public void addMethodToTargetsList(String sDb_id, String sSignature)
        {
            if (sSignature == "") // replace this with a clear method
                clearTargetslist();
            else
                O2Forms.addToDataGridView_Row(dgvTargetMethods, null, new[] {sDb_id, sSignature});
        }

        #region Nested type: ruleType

        private enum ruleType
        {
            Callback,
            TaintPropagator,
            NotPropagateTaint,
            Validator,
            Sink,
            Source
        }

        #endregion
    }
}
