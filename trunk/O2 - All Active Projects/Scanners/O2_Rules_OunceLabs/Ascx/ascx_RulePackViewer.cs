using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.Rules;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_RulePackViewer : UserControl
    {
        public ascx_RulePackViewer()
        {
            InitializeComponent();
        }

        private void cbTypeOfRuleToView_SelectedIndexChanged(object sender, EventArgs e)
        {            
            refreshRulesViewer(cbTypeOfRuleToView.Text,tbSignatureFilter.Text);
        }       

        private void ascx_RulePackViewer_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void dgvRules_Resize(object sender, EventArgs e)
        {
            applyDataGridViewSizes();
        }

        private void dgvRules_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void dgvRules_DragDrop(object sender, DragEventArgs e)
        {            
            handleDrop(e);            
            dgvRules.Focus();
        }
       
        private void tbSignatureFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
                refreshRulesViewer(cbTypeOfRuleToView.Text, tbSignatureFilter.Text);
        }
        // bool addSources, bool addSinks, bool addPropagateTaint, bool addDontPropagateTaint,
        //bool addAnyHigh, bool addAnyMedium, bool addAnyLow,
        private void btImportFromLocalMySqlOunceDatabase_Click(object sender, EventArgs e)
        {
            buttonClick_importFromLocalMySqlDatabase();
        }

        private void buttonClick_importFromLocalMySqlDatabase()
        {
            btImportFromLocalMySqlOunceDatabase.Enabled = false;
            btImportFromLocalMySql.Enabled = false;
            laImportingRulesFromLocalMySqlDB.Visible = true;                        
            importFromLocalMySqlDatabase(
                cbMySqlImport_Sources.Checked, cbMySqlImport_Sinks.Checked, cbMySqlImport_Callbacks.Checked,
                cbMySqlImport_PropagateTaint.Checked,cbMySqlImport_DontPropagateTaint.Checked,
                cbMySqlImport_AnyHigh.Checked,cbMySqlImport_AnyMedium.Checked,cbMySqlImport_AnyLow.Checked,

                () => this.invokeOnThread(
                          () =>
                              {
                                  btImportFromLocalMySqlOunceDatabase.Enabled = true;
                                  laImportingRulesFromLocalMySqlDB.Visible = false;
                                  btImportFromLocalMySql.Enabled = true;
                                  refreshRulesViewer(cbTypeOfRuleToView.Text, tbSignatureFilter.Text);
                              }));
        }

        private void btSaveCurrentFilter_Click(object sender, EventArgs e)
        {
            btSaveCurrentFilter.Enabled = false;
            saveCurrentFilter(cbTypeOfRuleToView.Text, tbSignatureFilter.Text,
                () => this.invokeOnThread(() => btSaveCurrentFilter.Enabled = true));
        }

       
        private void btSaveAllLoadedRules_Click(object sender, EventArgs e)
        {
            btSaveAllLoadedRules.Enabled = false;
            saveAllLoadedRules( () => this.invokeOnThread(()=>btSaveAllLoadedRules.Enabled = true));
        }

        private void btEditSelectedRule_Click(object sender, EventArgs e)
        {
            openRulesEditor();
        }
        

        private void dgvRules_SelectionChanged(object sender, EventArgs e)
        {
            editCurrentSelectedRows();
        }

       

        private void btRefreshView_Click(object sender, EventArgs e)
        {
            refreshRulesViewer();
        }

        private void dgvRules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvRules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            openRulesEditor();
        }

        private void tsRulePackViewer_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void tsRulePackViewer_DragDrop(object sender, DragEventArgs e)
        {
            var objectDropped = Dnd.tryToGetObjectFromDroppedObject(e);
            setSignatureFilter(objectDropped.ToString());
            
        }
       
        private void btRemoveAllLoadedRules_Click(object sender, EventArgs e)
        {
            removeAllLoadedRules();
        }

        private void dgvRules_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
/*            var o2RulesDeleted = new List<IO2Rule>();
            if (e.Row.Tag != null && e.Row.Tag is IO2Rule)
                o2RulesDeleted.Add((IO2Rule)e.Row.Tag);
            deleteRules(o2RulesDeleted);*/
        }

        private void llShowRulesWithSinksAndPropagateTaint_Click(object sender, EventArgs e)
        {
            showRulesWithSinksAndPropagateTaint();
        }        

        private void functionsViewer__onAfterSelect(object oObject)
        {
            if (oObject != null)
                if (oObject is FilteredSignature)
                {
                    var filteredSignature = (FilteredSignature)oObject;
                    showRule(filteredSignature.sSignature);
                }
                else
                {
                    if (oObject is List<FilteredSignature>)
                    {
                        var filteredSignatures = (List<FilteredSignature>) oObject;
                        var signaturesToShow = new List<string>();
                        foreach (var filteredSignature in filteredSignatures)
                            signaturesToShow.Add(filteredSignature.sSignature);
                        showRules(signaturesToShow,false,true,false);
                    }                    
                }

        }

        private void functionsViewer__onDoubleClick(object oObject)
        {

        }

        private void llClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gbMySqlImportPreferences.Visible = false;
        }

        private void btPreferences_Click(object sender, EventArgs e)
        {
            //gbMySqlImportPreferences.Visible = !gbMySqlImportPreferences.Visible;
            scRulesAndMySqlProperties.Panel2Collapsed = !scRulesAndMySqlProperties.Panel2Collapsed;
        }

        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLanguage = MiscUtils_OunceV6.getSupportedLanguageFromLanguageName(cbLanguages.Text);
        }

        private void cbMySqlImport_Sinks_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btImportFromLocalMySql_Click(object sender, EventArgs e)
        {
            buttonClick_importFromLocalMySqlDatabase();
        }

        private void btMySqlSync_AddSelectedRulesToDatabase_Click(object sender, EventArgs e)
        {
            var rulesNotProcessed = addRulesToDatabase(getChangedRulesList());
            clearChangedRulesList();
            addRulesToChangedRulesList(rulesNotProcessed);
            
        }

        private void btMySqlSync_DeleteSelectedRulesFromDatabase_Click(object sender, EventArgs e)
        {
            deleteRulesFromDatabase(getChangedRulesList());
            clearChangedRulesList();
        }
        

        private void scRulesAndMySqlProperties_Resize(object sender, EventArgs e)
        {
            try
            {
                scRulesAndMySqlProperties.Panel2MinSize = 250;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private void llClearChangedRules_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearChangedRulesList();
        }
       

        private void llRefreshChangedRules_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            refreshChangedRulesList();
        }

       

        private void llDragSelectedRules_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(getRules(false, false, true), DragDropEffects.Copy);
        }

        private void llDragAllLoadedRules_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(getRules(true, false, false),DragDropEffects.Copy);
        }

        private void llDragFilteredRules_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(getRules(false, true, false), DragDropEffects.Copy);
        }

        private void lvChangedRules_DragDrop(object sender, DragEventArgs e)
        {
            var droppedData = Dnd.tryToGetObjectFromDroppedObject(e);
            if (droppedData is List<IO2Rule>)
            addRulesToChangedRulesList((List<IO2Rule>)droppedData);
        }

      

        private void lvChangedRules_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lvChangedRules_Resize(object sender, EventArgs e)
        {

        }

        private void rbViewMode_AllRules_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewMode_AllRules.Checked)
                setRulesViewMode();
        }

        private void rbViewMode_OnlyTaggedRules_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewMode_OnlyTaggedRules.Checked)
                setRulesViewMode();
        }

        private void rbViewMode_OnlyNotInDb_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewMode_OnlyNotInDb.Checked)
                setRulesViewMode();
        }

        private void rbViewMode_OnlyNotInDbAndMapped_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewMode_OnlyNotInDbAndMapped.Checked)
                setRulesViewMode();
        }

        private void rbViewMode_TaggedAndInDb_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViewMode_TaggedAndInDb.Checked)
              setRulesViewMode();
        }

        private void llChangeRulesTo_Source_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeSelectedRulesTo(O2RuleType.Source, tbChangeRulesTo_NewVulnName.Text + ".Source");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeSelectedRulesTo(O2RuleType.Sink, tbChangeRulesTo_NewVulnName.Text + ".Sink");
        }

        private void llChangeRulesTo_Callback_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeSelectedRulesTo(O2RuleType.Callback, tbChangeRulesTo_NewVulnName.Text + ".Callback");
        }

        private void llChangeRulesTo_TaintPropagator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeSelectedRulesTo(O2RuleType.PropageTaint, tbChangeRulesTo_NewVulnName.Text + ".PropagateTaint");
        }

        private void llChangeRulesTo_DontPropagateTaint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeSelectedRulesTo(O2RuleType.DontPropagateTaint, tbChangeRulesTo_NewVulnName.Text + ".DontPropagateTaint");
        }

        private void llChangeRulesTo_ToBeDeleted_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeSelectedRulesTo(O2RuleType.ToBeDeleted, tbChangeRulesTo_NewVulnName.Text + ".ToBeDeleted");
            refreshChangedRulesList();
        }
        
        private void llShowRulesWithSinksAndPropagateTaint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showRulesWithSinksAndPropagateTaint();
        }

        private void btNewRule_Click(object sender, EventArgs e)
        {
            newRule();
        }

        private void llClearSelectedChangedRules_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearSelectedChangedRules();
        }

        private void cbShowMySqlPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowMySqlPassword.Checked)
                tbMySqlPassword.PasswordChar = '\0';
            else
                tbMySqlPassword.PasswordChar = '*';
        }

        private void tbMySqlUsername_TextChanged(object sender, EventArgs e)
        {
            
            OunceMySql.MySqlLoginUsername = tbMySqlUsername.Text; 
        }

        private void tbMySqlPassword_TextChanged(object sender, EventArgs e)
        {
            OunceMySql.MySqlLoginPassword = tbMySqlPassword.Text;
        }

        private void tbMySqlIPAddress_TextChanged(object sender, EventArgs e)
        {
            OunceMySql.MySqlServerIP = tbMySqlIPAddress.Text;
        }

        private void tbMySqlPort_TextChanged(object sender, EventArgs e)
        {
            OunceMySql.MySqlServerPort = tbMySqlPort.Text;
        }

        

        


        
    }
}
   