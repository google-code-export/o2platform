// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.Rules;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_RuleEditor
    {
        // events
        public O2Thread.FuncVoidT1<IO2Rule> onRuleChange;
        // local vars
        private bool runOnLoad = true;
        private O2Thread.FuncVoidT1<IO2Rule> onSave;
        private IO2Rule o2LoadedRule;
        

        private List<IO2Rule> o2LoadedRules =new List<IO2Rule>();

        public void onLoad()
        {
            if (runOnLoad && DesignMode == false)
            {                
                //cbRuleType                
                foreach (O2RuleType ruleType in Enum.GetValues(typeof(O2RuleType)))
                    if (ruleType != O2RuleType.All)
                        cbRuleType.Items.Add(ruleType);                
                cbRuleType.SelectedItem = 0;               
                // cbSeverity
                cbSeverity.Items.AddRange(new object[]{"High","Medium","Low"});

                // add empty entries for the cases where there are multiple rules loaded
                cbRuleType.Items.Add("");           
                cbSeverity.Items.Add("");

                runOnLoad = false;
            }
        }

        public void editRule(IO2Rule ruleToEdit, O2Thread.FuncVoidT1<IO2Rule> _onSave)
        {
            this.invokeOnThread(
                () =>
                    {
                        laDataSaved.Visible = false;
                        btSaveRuleChanges.Enabled = true;
                        btSaveChangesToAllRules.Enabled = false;
                        laUnsavedChanges.Visible = false;
                        laNumberOfRulesLoaded.Text = "1 rule loaded";
                        onSave = _onSave;
                        o2LoadedRule = ruleToEdit;
                        loadRuleIntoEditor(o2LoadedRule);
                        o2LoadedRules.Clear();
                        o2LoadedRules.Add(o2LoadedRule);       
                    });
        }

        private void loadRuleIntoEditor(IO2Rule ruleToEdit)
        {
            this.invokeOnThread(
                () =>
                    {
                        cbRuleType.Text = ruleToEdit.RuleType.ToString();
                        tbDbId.Text = ruleToEdit.DbId;
                        tbComment.Text = ruleToEdit.Comments;
                        tbFromParam.Text = ruleToEdit.FromArgs;
                        tbParam.Text = ruleToEdit.Param;
                        tbReturn.Text = ruleToEdit.Return;
                        cbSeverity.Text = ruleToEdit.Severity;
                        tbSignature.Text = ruleToEdit.Signature;
                        tbToParam.Text = ruleToEdit.ToArgs;
                        tbVulnerabilityType.Text = ruleToEdit.VulnType;
                    });
        }

        private void saveCurrentRule()
        {
            this.invokeOnThread(
                () =>
                    {
                        if (o2LoadedRule==null)
                            return;
                        btSaveRuleChanges.Enabled = false;
                        o2LoadedRule.DbId = tbDbId.Text;
                        o2LoadedRule.Comments = tbComment.Text;
                        o2LoadedRule.FromArgs = tbFromParam.Text;
                        o2LoadedRule.Param = tbParam.Text;
                        o2LoadedRule.Return = tbReturn.Text;
                        o2LoadedRule.Severity = cbSeverity.Text;
                        o2LoadedRule.Signature = tbSignature.Text;
                        o2LoadedRule.ToArgs = tbToParam.Text;
                        o2LoadedRule.VulnType = tbVulnerabilityType.Text;
                        if (cbRuleType.Text == "")
                            o2LoadedRule.RuleType = O2RuleType.NotMapped;
                        else
                            o2LoadedRule.RuleType = (O2RuleType) Enum.Parse(typeof (O2RuleType), cbRuleType.Text);
                        onSave(o2LoadedRule);
                        btSaveRuleChanges.Enabled = true;
                        laUnsavedChanges.Visible = false;
                        laDataSaved.Visible = true;
                    });
        }


        private void saveCurrentRules()
        {
            this.invokeOnThread(
                () =>
                {
                    btSaveChangesToAllRules.Enabled = false;
                    //tbDbId
                    if (tbDbId.Text != "")
                        updateLoadedRulesWithValue("DbId", tbDbId.Text);
                    //tbComment
                    if (tbComment.Text != "")
                        updateLoadedRulesWithValue("Comments", tbComment.Text);
                    //tbFromParam
                    if (tbFromParam.Text != "")
                        updateLoadedRulesWithValue("FromArgs", tbFromParam.Text);
                    //tbParam
                    if (tbParam.Text != "")
                        updateLoadedRulesWithValue("Param", tbParam.Text);
                    //tbReturn
                    if (tbReturn.Text != "")
                        updateLoadedRulesWithValue("Return", tbReturn.Text);
                    //cbSeverity
                    if (cbSeverity.Text != "")
                        updateLoadedRulesWithValue("Severity", cbSeverity.Text);
                    //tbSignature
                    if (tbSignature.Text != "")
                        updateLoadedRulesWithValue("Signature", tbSignature.Text);
                    //tbToParam
                    if (tbToParam.Text != "")
                        updateLoadedRulesWithValue("ToArgs", tbToParam.Text);
                    //tbVulnerabilityType
                    if (tbVulnerabilityType.Text != "")
                        updateLoadedRulesWithValue("VulnType", tbVulnerabilityType.Text);
                    //cbRuleType
                    if (cbRuleType.Text != "")
                        updateLoadedRulesWithValue("RuleType", (O2RuleType)Enum.Parse(typeof(O2RuleType), cbRuleType.Text));
                   
                    btSaveChangesToAllRules.Enabled = true;
                    laDataSaved.Visible = true;
                    laUnsavedChanges.Visible = false;
                });
        }

        private void updateLoadedRulesWithValue(string propertyToSet, object propertyValue)
        {
            foreach (var o2Rule in o2LoadedRules)
            {
                DI.reflection.setProperty(propertyToSet, o2Rule, propertyValue);
                onSave(o2Rule);
            }
        }

        public void EditRules(List<IO2Rule> o2RulesToEdit, O2Thread.FuncVoidT1<IO2Rule> _onSave)
        {
            this.invokeOnThread(
                () =>
                    {
                        onSave = _onSave;
                        newRule();
                        laDataSaved.Visible = false;
                        btSaveRuleChanges.Enabled = false;
                        btSaveChangesToAllRules.Enabled = true;
                        laNumberOfRulesLoaded.Text = o2RulesToEdit.Count +  " rule loaded";
                        o2LoadedRules = o2RulesToEdit;
                        o2LoadedRule = null;
                    });
        }

        public void newRule()
        {
            this.invokeOnThread(
                () =>
                    {
                        loadRuleIntoEditor(new O2Rule());
                        cbRuleType.Text = "";
                        cbSeverity.Text = "";                      
                        tbDbId.Text = "";
                        tbSignature.Text = "";
                        laDataSaved.Visible = false;
                        btSaveRuleChanges.Enabled = false;
                        btSaveChangesToAllRules.Enabled = false;
                        laUnsavedChanges.Visible = false;
                    });
        }       

    }
}
