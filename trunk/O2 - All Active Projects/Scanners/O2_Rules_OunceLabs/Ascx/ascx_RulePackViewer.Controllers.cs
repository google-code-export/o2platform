using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.Rules;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;
using System.Drawing;

namespace O2.Rules.OunceLabs.Ascx
{
    public enum RulesViewMode
    {
        AllRules,
        OnlyTagged,
        OnlyNotInDb,
        OnlyNotInDbAndMapped,
        OnlyTaggedAndInDb
    }

    public partial class ascx_RulePackViewer
    {
        

        public O2RulePack currentO2RulePack = new O2RulePack();
        public Dictionary<string, List<IO2Rule>> indexedCurrentO2Rules = new Dictionary<string, List<IO2Rule>>();
        public List<IO2Rule> rulesToShow = new List<IO2Rule>();
        public SupportedLanguage currentLanguage = SupportedLanguage.DotNet; 
        public ascx_RuleEditor ruleEditor;
        public RulesViewMode viewMode;

        private static bool runOnLoad = true;

        private void onLoad()
        {
            if (cbLanguages.Items.Count == 0 || runOnLoad && DesignMode == false)
            {
                cbTypeOfRuleToView.SelectedIndex = 0;
                addColumnNames();
                cbLanguages.Items.AddRange(MiscUtils_OunceV6.getStringListWithSupportedLanguages().ToArray());
                cbLanguages.Text = "DotNet"; // default to dotnet
                functionsViewer.setCheckBoxesState(true);
                viewMode = RulesViewMode.AllRules;
                runOnLoad = false;
                loadMySqlDetails();
            }
            
        }

        private void setRulesViewMode()
        {
            if (rbViewMode_AllRules.Checked)
                viewMode = RulesViewMode.AllRules;
            else if (rbViewMode_OnlyNotInDb.Checked)
                viewMode = RulesViewMode.OnlyNotInDb;
            else if (rbViewMode_OnlyNotInDbAndMapped.Checked)
                viewMode = RulesViewMode.OnlyNotInDbAndMapped;
            else if (rbViewMode_OnlyTaggedRules.Checked)
                viewMode = RulesViewMode.OnlyTagged;
            else if (rbViewMode_TaggedAndInDb.Checked)
                viewMode = RulesViewMode.OnlyTaggedAndInDb;

            refreshRulesViewer(cbTypeOfRuleToView.Text, tbSignatureFilter.Text);

            //refreshRulesViewer();
        }

        private void saveCurrentFilter(string typeOfRule, string signatureFilter, O2Thread.FuncVoid onComplete)
        {
            O2Thread.mtaThread(
                () =>
                    {                        
                        var o2RulePack = new O2RulePack("All loaded rules", rulesToShow);
                        var savedFile = O2RulePackUtils.saveRulePack(typeOfRule, signatureFilter, o2RulePack);
                        DI.log.showMessageBox("Current filtered rules saved to: " + savedFile);
                        onComplete();
                    });
        }

        public Thread saveAllLoadedRules(O2Thread.FuncVoid onComplete)
        {
            return O2Thread.mtaThread(
                () =>
                    {
                        var savedFile = O2RulePackUtils.saveRulePack(currentO2RulePack);
                        DI.log.showMessageBox("All loaded rules rules saved to: " + savedFile);
                        if (onComplete != null)
                            onComplete();
                    });
        }

        public Thread importFromLocalMySqlDatabase()
        {
            return importFromLocalMySqlDatabase(true, true, true,true, true, true, true, true, null);
        }

        public Thread importFromLocalMySqlDatabase(
            bool addSources, bool addSinks, bool addCallbacks, bool addPropagateTaint, bool addDontPropagateTaint, bool addAnyHigh, bool addAnyMedium, bool addAnyLow,
            O2Thread.FuncVoid onComplete)
        {
            return O2Thread.mtaThread(
                () =>
                    {
                        try
                        {
                            var timer = new O2Timer("Loaded rules from DB").start();
                            var o2Rules =
                                new MySqlRules_OunceV6().createO2RulesForAllLddbEntriesForLanguage(
                                    currentLanguage,
                                    addSources, addSinks, addCallbacks, addPropagateTaint, addDontPropagateTaint, addAnyHigh, addAnyMedium, addAnyLow
                                    );
                            setRulesAsLoadedFromDb(o2Rules);
                            setCurrentRulePack(new O2RulePack("MySql_Dump", o2Rules));
                            timer.stop();
                        }
                        catch (Exception ex)
                        {
                            DI.log.error("in importFromLocalMySqlDatabase:{0}", ex.Message);
                        }
                        if (onComplete != null)
                            onComplete();
                    });
        }

        private static void setRulesAsLoadedFromDb(IEnumerable<IO2Rule> o2Rules)
        {
            foreach(var o2Rule in o2Rules)
                o2Rule.FromDb = true;
        }

        public void setCurrentRulePack(O2RulePack o2RulePack)
        {
            if (o2RulePack == null)
                return;
            currentO2RulePack = o2RulePack;
            indexedCurrentO2Rules = IndexedO2Rules.indexAll(currentO2RulePack.getIO2Rules());
        }

        public void refreshRulesViewer()
        {
            this.invokeOnThread(
                () =>
                    {
                        laOnlyShowingRulesFor1Signature.Visible = false;
                        if (cbTypeOfRuleToView.Text != "All")
                            cbTypeOfRuleToView.Text = "All";
                        else
                            refreshRulesViewer("All", tbSignatureFilter.Text);
                    });
            //return refreshRulesViewer("All", "");
        }

        public Thread refreshRulesViewer(string ruleType, string signatureFilter)        
        {
            return O2Thread.mtaThread(
                () =>
                    {
                        if (currentO2RulePack == null)
                            return;
                        rulesToShow = filterO2RulesByTypeAndFilter(currentO2RulePack.getIO2Rules(), signatureFilter, mapStringToO2RuleType(ruleType));
                        populateListViewWithRules();
                    });
        }

        private static O2RuleType mapStringToO2RuleType(string ruleType)
        {
            switch (ruleType)
            {
                case "All":
                    return O2RuleType.All;
                case "Callbacks":
                    return O2RuleType.Callback;
                case "Sources":
                    return O2RuleType.Source;
                case "Sinks":
                    return O2RuleType.Sink;
                case "Lost Sinks":
                    return O2RuleType.LostSink;
                case "Propagate Taint":
                    return O2RuleType.PropageTaint;
                case "Dont Propagate Taint":
                    return O2RuleType.DontPropagateTaint;
                case "Not Mapped":
                    return O2RuleType.NotMapped;
                case "Not a Source":
                    return O2RuleType.NotASource;
                case "To Be Deleted":
                    return O2RuleType.ToBeDeleted;
                default:
                    return O2RuleType.All;
            }
        }

        public Thread loadO2RulePack(string pathToO2RulePackFileToLoad)
        {
            return O2Thread.mtaThread(
                () =>
                    {

                        this.invokeOnThread(() => laLoadingRulePack.Visible = true);
                        setCurrentRulePack(O2RulePackUtils.loadRulePack(pathToO2RulePackFileToLoad));
                        this.invokeOnThread(() =>
                                                {                                                    
                                                    laLoadingRulePack.Visible = false;
                                                    refreshRulesViewer();
                                                });
                        
                    });
        }

        

        private DataGridViewRow addO2RuleToDataGridView(IO2Rule o2Rule)
        {
            object[] row = getRowDataFromO2Fule(o2Rule);
                            
            var newRowId = dgvRules.Rows.Add(row);
            var newRow = dgvRules.Rows[newRowId];
            newRow.Tag = o2Rule;
            if (cbColorCodeRules.Checked)
                newRow.DefaultCellStyle.ForeColor = OzasmtUtils.getTraceColorBasedOnRuleType(o2Rule);
            return newRow;
        }

        private static object[] getRowDataFromO2Fule(IO2Rule o2Rule)
        {
            return new object[]
                       {
                           o2Rule.RuleType, 
                           o2Rule.VulnType, 
                           o2Rule.Severity, 
                           o2Rule.Signature,                                                                          
                           o2Rule.Param, 
                           o2Rule.Return, 
                           o2Rule.FromArgs, 
                           o2Rule.ToArgs, 
                           o2Rule.DbId,
                           o2Rule.Comments,
                           o2Rule.FromDb,
                           o2Rule.Tagged
                       };
        }

        private void addColumnNames()
        {
            dgvRules.Columns.Add("O2RuleType", "O2RuleType");
            dgvRules.Columns.Add("VulnType", "VulnType");
            dgvRules.Columns.Add("Severity", "Severity");
            dgvRules.Columns.Add("Signature", "Signature");                        
            dgvRules.Columns.Add("Param", "Param");
            dgvRules.Columns.Add("Return", "Return");
            dgvRules.Columns.Add("FromArgs", "FromArgs");
            dgvRules.Columns.Add("ToArgs", "ToArgs");
            dgvRules.Columns.Add("DbId", "DbId");
            dgvRules.Columns.Add("Comments", "Comments");
            dgvRules.Columns.Add("DB?", "DB?");
            dgvRules.Columns.Add("Tagged", "Tagged");
            applyDataGridViewSizes();
        }
        // ReSharper disable PossibleNullReferenceException
        private void applyDataGridViewSizes()
        {
            if (dgvRules.Columns.Count > 0)
            {                
                dgvRules.Columns["O2RuleType"].Width = 50;
                dgvRules.Columns["VulnType"].Width = 100;   
                dgvRules.Columns["Severity"].Width = 30;
                //dgvRules.Columns["Signature"].Width = 30;                                                     
                dgvRules.Columns["Param"].Width = 30;
                dgvRules.Columns["Return"].Width = 30;
                dgvRules.Columns["FromArgs"].Width = 30;
                dgvRules.Columns["ToArgs"].Width = 30;
                dgvRules.Columns["DbId"].Width = 15;
                dgvRules.Columns["Comments"].Width = 30;
                dgvRules.Columns["DB?"].Width = 30;
                dgvRules.Columns["Tagged"].Width = 40;
            }
        }

        // ReSharper restore PossibleNullReferenceException


        public List<IO2Rule> filterO2RulesByTypeAndFilter(List<IO2Rule> o2RulesToFilter, string signatureFilter, O2RuleType o2RuleType)
        {                                        
            
            // apply Rule View Mode
            switch (viewMode)
            {
                case RulesViewMode.AllRules:
                    break;                      // don't do anything
                case RulesViewMode.OnlyNotInDb:
                    o2RulesToFilter = (from IO2Rule o2Rule in o2RulesToFilter where o2Rule.FromDb == false select o2Rule).ToList();
                    break;
                case RulesViewMode.OnlyNotInDbAndMapped:
                    o2RulesToFilter = (from IO2Rule o2Rule in o2RulesToFilter 
                                       where (o2Rule.FromDb == false && o2Rule.RuleType!=O2RuleType.NotMapped) 
                                       select o2Rule).ToList();
                    break;
                case RulesViewMode.OnlyTagged:
                    o2RulesToFilter = (from IO2Rule o2Rule in o2RulesToFilter where o2Rule.Tagged select o2Rule).ToList();
                    break;
                case RulesViewMode.OnlyTaggedAndInDb:
                    o2RulesToFilter = (from IO2Rule o2Rule in o2RulesToFilter where (o2Rule.Tagged && o2Rule.FromDb) select o2Rule).ToList();
                    break;                
                    
            }
            var o2RulesThatMatchTheFilter = new List<IO2Rule>();
            foreach (var o2Rule in o2RulesToFilter)            
                if (doesO2RuleMatchTypeAndFilter(o2Rule, o2RuleType, signatureFilter))
                    o2RulesThatMatchTheFilter.Add(o2Rule);            
            return o2RulesThatMatchTheFilter;
        }

        private static bool doesO2RuleMatchTypeAndFilter(IO2Rule o2Rule, O2RuleType o2RuleType, string signatureFilter)
        {
            if (o2RuleType == O2RuleType.All || o2Rule.RuleType == o2RuleType)
                if (signatureFilter == "" || RegEx.findStringInString(o2Rule.Signature, signatureFilter) || o2Rule.Signature.IndexOf(signatureFilter) > -1)
                    return true;
            return false;
        }

        private void editCurrentSelectedRows()
        {
            O2Thread.mtaThread(
                () =>
                    {
                        if (ruleEditor == null)
                        {
                            return;
                         //   var openThread = openRulesEditor();
                         //   openThread.Join(); // wait for it to finish
                        }
                        this.invokeOnThread(
                            () =>
                                {
                                    if (dgvRules.SelectedRows.Count == 1)
                                    {
                                        if (dgvRules.SelectedRows[0].Tag != null &&
                                            dgvRules.SelectedRows[0].Tag is IO2Rule)
                                        {
                                            var currentRow = dgvRules.SelectedRows[0];
                                            ruleEditor.editRule(
                                                (IO2Rule)currentRow.Tag, o2SavedRule => onRuleSave(o2SavedRule,currentRow));
                                        }
                                    }
                                    else
                                    {
                                        var o2RulesToEdit = new List<IO2Rule>();
                                        foreach (DataGridViewRow row in dgvRules.SelectedRows)
                                            if (row.Tag != null && row.Tag is IO2Rule)
                                                o2RulesToEdit.Add((IO2Rule) row.Tag);
                                        if (o2RulesToEdit.Count >0)
                                            ruleEditor.EditRules(o2RulesToEdit, o2SavedRule => onRuleSave(o2SavedRule, null));
                                        else
                                            ruleEditor.newRule();
                                    }
                                });
                    });
        }

        public void editRule(string signatureOfRuleToEdit)
        {
            
            if (signatureOfRuleToEdit != "" && indexedCurrentO2Rules.ContainsKey(signatureOfRuleToEdit))
            {
                O2Thread.mtaThread(
                    () =>
                        {

                            //        if (ruleEditor == null)  // this was quite annoying so for this rules editor doesn't open by default
                    //        {
                    //            return;

                                //var openThread = openRulesEditor();  //make sure the rule editor is visible                                    
                                //openThread.Join(); // wait for it to finish
                    //        }

                            showRule(signatureOfRuleToEdit,true, false,false);  // in this case we have to override the current filters 
                        });
            }
           else
            {
                this.invokeOnThread(
                    () =>
                        {        
                            // handle the case where there is a partial match between the provided text and an existing rule                                                
                            if (signatureOfRuleToEdit != "")
                            {
                                var matchedSignatures = from string rule in indexedCurrentO2Rules.Keys
                                                        where rule.Contains(signatureOfRuleToEdit)
                                                        select rule;
                                if (matchedSignatures.Count() > 0)
                                {
                                    showRules(matchedSignatures.ToList());
                                    return;
                                }
                            }
                            laOnlyShowingRulesFor1Signature.Visible = true;
                            dgvRules.Rows.Clear();
                        });
                if (ruleEditor != null)
                    ruleEditor.newRule();
            
           
                /*
                if (ruleEditor != null)
                {
                    if (ruleEditor.ParentForm != null)
                        // ReSharper disable PossibleNullReferenceException
                        ruleEditor.ParentForm.invokeOnThread(() => ruleEditor.ParentForm.Close());
                        // ReSharper restore PossibleNullReferenceException
                }
                 * */
            }
            
        }
        public void showRule(string signatureOfRuleToShow)
        {
            showRule(signatureOfRuleToShow, false, false,false);
        }

        public void showRules(List<string> signaturesOfRuleToShow)
        {
            showRules(signaturesOfRuleToShow, false /*raiseEditCurrentSelectedRows*/,true /*applyCurrentFilters */, false /*removeNotMachedRulesOfSameType*/);
        }

        public void showRule(string signatureOfRuleToShow, bool raiseEditCurrentSelectedRows, bool applyCurrentFilters, bool removeNotMachedRulesOfSameType)
        {
            var signaturesOfRulesToShow = new List<string> { signatureOfRuleToShow };
            showRules(signaturesOfRulesToShow, raiseEditCurrentSelectedRows, applyCurrentFilters, removeNotMachedRulesOfSameType);
        }

        public void showRules(List<string> signaturesOfRulesToShow, bool raiseEditCurrentSelectedRows, bool applyCurrentFilters, bool removeNotMachedRulesOfSameType)
        {
            this.invokeOnThread(
                () =>
                    {
                        var filterByO2RuleType = O2RuleType.All;
                        var filterByText = "";
                        if (applyCurrentFilters)
                        {
                            filterByO2RuleType = mapStringToO2RuleType(cbTypeOfRuleToView.Text);
                            filterByText = tbSignatureFilter.Text;
                        }
                        laOnlyShowingRulesFor1Signature.Visible = true;
                        dgvRules.Rows.Clear();
                        var o2RulesFiltered = new List<IO2Rule>();                        
                        foreach (var ruleSignature in signaturesOfRulesToShow)
                            if (indexedCurrentO2Rules.ContainsKey(ruleSignature))
                                foreach (var o2Rule in indexedCurrentO2Rules[ruleSignature])
                                    if (doesO2RuleMatchTypeAndFilter(o2Rule, filterByO2RuleType, filterByText))                                
                                        o2RulesFiltered.Add(o2Rule);                                    

                        // see if we need to remove the rules that match the selected O2RuleType but are not being shown
                        if (removeNotMachedRulesOfSameType && filterByO2RuleType != O2RuleType.All)
                        {
                            var o2RulesToRemove = new List<O2Rule>();
                            foreach (var o2Rule in currentO2RulePack.o2Rules)
                                if (o2Rule.RuleType == filterByO2RuleType)
                                    if (false == o2RulesFiltered.Contains(o2Rule))
                                        o2RulesToRemove.Add(o2Rule);
                            DI.log.info("Going to remove {0} rules from currentO2RulePack", o2RulesToRemove.Count);
                            // removing them
                            foreach (var o2RuleToDelete in o2RulesToRemove)
                                currentO2RulePack.o2Rules.Remove(o2RuleToDelete);
                            // reindex 
                            indexedCurrentO2Rules = IndexedO2Rules.indexAll(currentO2RulePack.getIO2Rules());
                            showRules(o2RulesFiltered, true /* showRulesInFunctionsViewer*/, raiseEditCurrentSelectedRows);                                                                                       
                            
                        }
                        else
                            showRules(o2RulesFiltered, false  /* showRulesInFunctionsViewer*/, raiseEditCurrentSelectedRows);                                                                                       

                    });
        }


        private Thread openRulesEditor()
        {
            return O2Thread.mtaThread(
                () =>
                    {
                        if (ruleEditor == null)
                        {
                            O2Messages.openControlInGUISync(typeof (ascx_RuleEditor), O2DockState.Float, "Rule Editor");
                            var ascxControl = O2Messages.getAscxSync("Rule Editor");
                            if (ascxControl != null && ascxControl is ascx_RuleEditor)
                            {
                                ruleEditor = (ascx_RuleEditor) ascxControl;
                                if (ruleEditor.ParentForm != null)
                                    ruleEditor.ParentForm.Closed += (sender, e) => {ruleEditor = null;};
                                editCurrentSelectedRows();
                            }
                        }
                        else                        
                            editCurrentSelectedRows();                        
                    });
        }

        private void setSignatureFilter(string newSignatureText)
        {
            this.invokeOnThread(
                () =>
                {
                    tbSignatureFilter.Text = newSignatureText;
                    refreshRulesViewer(cbTypeOfRuleToView.Text, tbSignatureFilter.Text);
                });
        }

        
        private void removeAllLoadedRules()
        {
            setCurrentRulePack(new O2RulePack("RulePack"));
            refreshRulesViewer();
        }


        private void showRulesWithSinksAndPropagateTaint()
        {
            var sinksWithPropagateTaint = new List<IO2Rule>();
            var currentSinks = new List<IO2Rule>();
            foreach (var o2Rule in currentO2RulePack.o2Rules)
                if (o2Rule.RuleType == O2RuleType.Sink)
                    currentSinks.Add(o2Rule);
            foreach (var sinkRule in currentSinks)
                foreach (var o2Rule in indexedCurrentO2Rules[sinkRule.Signature])
                    if (o2Rule.RuleType == O2RuleType.PropageTaint)
                    {
                        sinksWithPropagateTaint.Add(sinkRule);
                        sinksWithPropagateTaint.Add(o2Rule);
                    }
            showRules(sinksWithPropagateTaint);
        }

        public void showRules(List<IO2Rule> o2RulesToShow)
        {
            showRules(o2RulesToShow, true, false);
        }

        public void showRules(List<IO2Rule> o2RulesToShow, bool showRulesInFunctionsViewer, bool raiseEditCurrentSelectedRows)
        {
            rulesToShow = o2RulesToShow; // just to make sure that rulesToShow has the current viewed rules            

            if (showRulesInFunctionsViewer)
            {
                var rulesSignatures = IndexedO2Rules.getRulesSignatures(o2RulesToShow);
                functionsViewer.showSignatures(rulesSignatures);
            }
            this.invokeOnThread(
                () =>
                    {                        
                        laNotAllRulesShow.Visible = false;
                        if (dgvRules.Columns.Count == 0)
                            addColumnNames();
                        dgvRules.Visible = false;
                        dgvRules.Rows.Clear();
                        foreach (var o2Rule in o2RulesToShow)
                        {
                            addO2RuleToDataGridView(o2Rule);
                            if (dgvRules.Rows.Count > 7500)
                            {
                                laNotAllRulesShow.Visible = true;
                                break;
                            }
                        }
                        updateShowedRulesStats(o2RulesToShow);
                        dgvRules.Visible = true;
                        if (raiseEditCurrentSelectedRows)
                            editCurrentSelectedRows();
                    });
        }

        private void updateShowedRulesStats(ICollection<IO2Rule> o2RulesToShow)
        {
            this.invokeOnThread(
                () =>
                    {
                        laNumberOfRulesLoaded.Text =
                            "# Rules Loaded:" + currentO2RulePack.o2Rules.Count + "  " +
                            "# Rules After Filter " + o2RulesToShow.Count;
                    });
        }

        public void populateListViewWithRules()
        {
            if (rulesToShow == null)
                return;
            showRules(rulesToShow);            
        }

        private static List<IO2Rule> addRulesToDatabase(IEnumerable<IO2Rule> rulesToAdd)
        {
            DI.log.info("Adding {0} rules to the database", rulesToAdd.Count());
            return new MySqlRules_OunceV6().addRulesToDatabase(rulesToAdd);
        }

        private void deleteRulesFromDatabase(IEnumerable<IO2Rule> rulesToDelete)
        {
            DI.log.info("Deleting {0} rules from the database", rulesToDelete.Count());
            new MySqlRules_OunceV6().deleteRulesFromDatabase(rulesToDelete);
            foreach (O2Rule deletedRule in rulesToDelete)
                currentO2RulePack.o2Rules.Remove(deletedRule);
            this.invokeOnThread(() => refreshRulesViewer(cbTypeOfRuleToView.Text, tbSignatureFilter.Text));
        }
       //  rbApplyChangesTo_AllLoadedRules.Checked,
        //        rbApplyChangesTo_CurrentFilteredRules.Checked,
         //       rbApplyChangesTo_SelectedRules.Checked
        private IEnumerable<IO2Rule> getRules(bool getAllLoadedRules, bool getCurrentFilteredRules, bool getSelectedRules)
        {
            if (getAllLoadedRules)
                return currentO2RulePack.getIO2Rules();
            var o2FilteredRules = new List<IO2Rule>();

            if (getCurrentFilteredRules)
                foreach (DataGridViewRow row in dgvRules.Rows)
                    if (row.Tag != null && row.Tag is IO2Rule)
                        o2FilteredRules.Add((IO2Rule) row.Tag);
            if (getSelectedRules)
                foreach (DataGridViewRow row in dgvRules.SelectedRows)
                    if (row.Tag != null && row.Tag is IO2Rule)
                        o2FilteredRules.Add((IO2Rule) row.Tag);
            return o2FilteredRules;
        }

        private void clearSelectedChangedRules()
        {
            foreach(ListViewItem selectedItem in lvChangedRules.SelectedItems)
                lvChangedRules.Items.Remove(selectedItem);
            updateLabelWithNumberOfChangedRules();
        }

        private void clearChangedRulesList()
        {
            lvChangedRules.Items.Clear();
            updateLabelWithNumberOfChangedRules();
        }

        private void refreshChangedRulesList()
        {
            var currentChangedRulesList = new List<IO2Rule>();
            foreach (ListViewItem listViewItem in lvChangedRules.Items)
                if (listViewItem.Tag is IO2Rule)
                    currentChangedRulesList.Add((IO2Rule) listViewItem.Tag);

            lvChangedRules.Items.Clear();
            addRulesToChangedRulesList(currentChangedRulesList);
        }

        private void addRulesToChangedRulesList_BatchMode(List<IO2Rule> o2RulesToAdd)
        {
            var listViewItems = new List<ListViewItem>();
            foreach(var o2RuleToAdd in o2RulesToAdd)
                listViewItems.Add(new ListViewItem(new[] {o2RuleToAdd.RuleType.ToString(), o2RuleToAdd.Signature})
                                       {Tag = o2RuleToAdd});
            lvChangedRules.Items.AddRange(listViewItems.ToArray());
            updateLabelWithNumberOfChangedRules();
        }

        private IO2Rule addRuleToChangedRulesList(IO2Rule o2RuleToAdd)
        {
            return addRuleToChangedRulesList(o2RuleToAdd, true);
        }

        private IO2Rule addRuleToChangedRulesList(IO2Rule o2RuleToAdd, bool checkIfRuleIsAlreadyInList)
        {
            // see if this rule has already been added            
            if (false == checkIfRuleIsAlreadyInList || false == isRuleAllreadyOnChangedRulesList(o2RuleToAdd))
            {
                var listViewItem = new ListViewItem(new[] {o2RuleToAdd.RuleType.ToString(), o2RuleToAdd.Signature})
                                       {Tag = o2RuleToAdd};
                lvChangedRules.Items.Add(listViewItem);
                // handle special case of deleting rules
                if (o2RuleToAdd.RuleType == O2RuleType.ToBeDeleted)
                {
                    foreach (var o2RuleWithSameSignatureAsRuleToDelete in indexedCurrentO2Rules[o2RuleToAdd.Signature])
                        if (o2RuleWithSameSignatureAsRuleToDelete.FromDb &&
                            o2RuleWithSameSignatureAsRuleToDelete.RuleType != O2RuleType.ToBeDeleted &&
                            false == isRuleAllreadyOnChangedRulesList(o2RuleWithSameSignatureAsRuleToDelete))
                            addRuleToChangedRulesList(o2RuleWithSameSignatureAsRuleToDelete);
                }
                // handle special case of DontPropagateTaint & Propagate Taint
                /*else if (o2RuleToAdd.RuleType == O2RuleType.DontPropagateTaint)
                {
                    foreach (var o2RuleToChange in indexedCurrentO2Rules[o2RuleToAdd.Signature])
                        if (o2RuleToChange.RuleType != o2RuleToAdd.RuleType && false == isRuleAllreadyOnChangedRulesList(o2RuleToChange))
                        {
                            o2RuleToChange.RuleType = O2RuleType.ToBeDeleted;
                            addRuleToChangedRulesList(o2RuleToChange);
                        }
                        //addRuleToChangedRulesList(o2RuleWithSameSignatureAsRuleToDelete);
                }*/
                updateLabelWithNumberOfChangedRules();
            }
            return o2RuleToAdd;
        }
        
        private void addRulesToChangedRulesList(IEnumerable<IO2Rule> rulesToAdd)
        {
            lvChangedRules.Visible = false;
            var listViewItems = new List<ListViewItem>();
            foreach (var ruleToAdd in rulesToAdd)
                if (false == isRuleAllreadyOnChangedRulesList(ruleToAdd))
                    listViewItems.Add(new ListViewItem(new[] { ruleToAdd.RuleType.ToString(), ruleToAdd.Signature }) { Tag = ruleToAdd });
            
           // addRuleToChangedRulesList(ruleToAdd);
            lvChangedRules.Items.AddRange(listViewItems.ToArray());
            DI.log.info("There are {0} rules loaded in the ListView lvChangedRules",lvChangedRules.Items.Count);
            lvChangedRules.Visible = true;
            updateLabelWithNumberOfChangedRules();
        }

        private void updateLabelWithNumberOfChangedRules()
        {
            lbNumberOfChangedRules.Text = lvChangedRules.Items.Count.ToString();
        }

        // this is not a very optimized way of doing this, so changed it if there are performance issues when adding too many rules
        private bool isRuleAllreadyOnChangedRulesList(IO2Rule o2RuleToAdd)
        {
            foreach (ListViewItem listViewItem in lvChangedRules.Items)
                if (listViewItem.Tag is IO2Rule && (IO2Rule)listViewItem.Tag == o2RuleToAdd)
                    return true;
            return false;
        }

        private IEnumerable<IO2Rule> getChangedRulesList()
        {
            var rules = new List<IO2Rule>();
            foreach (ListViewItem item in lvChangedRules.Items)
                if (item.Tag is IO2Rule)
                    rules.Add((IO2Rule)item.Tag);
            return rules;
        }


        private void onRuleSave(IO2Rule o2SavedRule, DataGridViewRow currentRow)
        {
            if (currentRow!= null)
                currentRow.SetValues(getRowDataFromO2Fule(o2SavedRule));
            addRuleToChangedRulesList(o2SavedRule);
        }

        private bool handleDrop_list_IO2Rule( List<IO2Rule> o2Rules)
        {
            if (o2Rules != null)
            {
                setCurrentRulePack(new O2RulePack("Dropped Findings", o2Rules));
                refreshRulesViewer();
                return true;
            }
            return false;
        }

        private bool handleDrop_list_ICirFunction(List<ICirFunction> cirFunctions)
        {
            if (cirFunctions != null)
            {
                this.invokeOnThread(
                    () => addCirFunctionsAsRules(cirFunctions,
                                                 cbCirDataDrop_KeepRulesLoadedFromDatabase.Checked,
                                                 cbCirDataDrop_AddExternalMethodsAsSourcesAndSinks.Checked,
                                                 cbCirDataDrop_AddInternalMethodsWithNoCallersAsCallbacks.Checked,
                                                 cbCirDataDrop_DontMarkAsCallbackOrSinksMethodsWithNoParameters.Checked,
                                                 cbCirDataDrop_DontAddIfRuleSignatureAlreadyExists.Checked,
                                                 cbCirDataDrop_DontAddIfThereAreNoCallersAndCallees.Checked,
                                                 tbCirDataDrop_NewRulesVulnType.Text));
                return true;
            }
            return false;
        }

        private bool handleDrop_list_IO2Finding(List<IO2Finding> o2Findings)
        {
            if (o2Findings != null)
            {
                    DI.log.info("A list with {0} of Findings was dropped", o2Findings.Count);
                        addFindingsAsRules(o2Findings,                            
                                           cbFindingsDrop_KeepRulesLoadedFromDatabase.Checked);
                return true;
            }
            return false;
        }

        private bool handleDrop_list_FilteredSignature(List<FilteredSignature> filteredSignatures)
        {
            if (filteredSignatures != null)
            {
                var signaturesToShow = new List<string>();
                foreach (var filteredSignature in filteredSignatures)
                    signaturesToShow.Add(filteredSignature.sSignature);
                showRules(signaturesToShow, false, true, true);

                return true;
            }
            return false;
        }

        private void handleDrop_loadFileAsRulePack(DragEventArgs e)
        {
            var fileToLoad = Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);
            if (fileToLoad != "")
            {
                if (Path.GetExtension(fileToLoad) == ".as")
                {
                    importASfile(fileToLoad);
                }
                else
                    loadO2RulePack(fileToLoad);
            }
        }

        public void importASfile(string fileToLoad)
        {
            if (false == File.Exists(fileToLoad))
                DI.log.error("could not find summary file: {0}", fileToLoad);
            var fileContents = Files.getFileContents(fileToLoad);
        	DI.log.info("Loaded file size: {0}",fileContents.Length);
        	var rules = fileContents.Split(new []{Environment.NewLine},StringSplitOptions.None);
        	DI.log.info("There are {0} rules", rules.Length);
        	
        	var rulesCreated = new List<IO2Rule>();
        	foreach(var rule in rules)
        	{
        		var ruleDetails = rule.Split('\t');
        		var ruleDetailsLength =  ruleDetails.Length;
        		var ruleSignature = (ruleDetailsLength>0)  ? ruleDetails[0] : "";
        		var ruleTaintFrom = (ruleDetailsLength>1)  ? ruleDetails[1] : "";
        		var ruleTaintTo   = (ruleDetailsLength>2)  ? ruleDetails[2] : "";
                DI.log.info("{0} - {1} - {2}", ruleSignature, ruleTaintFrom, ruleTaintTo);
                rulesCreated.Add(createASRule(ruleSignature, ruleTaintFrom, ruleTaintTo));
        	}
            var newRulePack = new O2RulePack("ASummary Rule Pack", rulesCreated);
            setCurrentRulePack(newRulePack);
            refreshRulesViewer();
        }

        public static IO2Rule createASRule(string ruleSignature, string ruleTaintFrom, string ruleTaintTo)
        {
            var o2Rule = new O2Rule();
            o2Rule.DbId = "2";
            o2Rule.Signature = ruleSignature;
            o2Rule.FromArgs = ruleTaintFrom;
            o2Rule.ToArgs = ruleTaintTo;
            if (o2Rule.ToArgs == "none")
                o2Rule.RuleType = O2RuleType.DontPropagateTaint;
            else
                o2Rule.RuleType = O2RuleType.PropageTaint;
            if (o2Rule.ToArgs.IndexOf(".*") > -1)
            {
                o2Rule.Return = "1";
                o2Rule.ToArgs = o2Rule.ToArgs.Replace(".*", "");
            }
            else
                o2Rule.Return = "0";
            o2Rule.VulnType = "Summary Driven Rule";
            return o2Rule;
        }

        private bool handleDrop_IO2Trace(IO2Trace o2Trace)
        {
            if (o2Trace != null)
            {
                showRule(o2Trace.signature);
                return true;
            }
            return false;
        }

        private void handleDrop(DragEventArgs e)
        {            
            if (!handleDrop_IO2Trace(
                     (IO2Trace)Dnd.tryToGetObjectFromDroppedObject(e, typeof(O2.DotNetWrappers.O2Findings.O2Trace))))
                if (!handleDrop_list_IO2Rule(
                         (List<IO2Rule>) Dnd.tryToGetObjectFromDroppedObject(e, typeof (List<IO2Rule>))))
                    if (!handleDrop_list_ICirFunction(
                             (List<ICirFunction>) Dnd.tryToGetObjectFromDroppedObject(e, typeof (List<ICirFunction>))))
                        if (!handleDrop_list_IO2Finding(
                                 (List<IO2Finding>) Dnd.tryToGetObjectFromDroppedObject(e, typeof (List<IO2Finding>))))
                            if (!handleDrop_list_FilteredSignature(
                                     (List<FilteredSignature>)
                                     Dnd.tryToGetObjectFromDroppedObject(e, typeof (List<FilteredSignature>))))
                                // finally see if it is a file  and and 
                                handleDrop_loadFileAsRulePack(e);

        }

        private void addCirFunctionsAsRules(
            List<ICirFunction> cirFunctionsToProcess, bool keepRulesLoadedFromDatabase, 
            bool bAddExternalMethodsAsSourcesAndSinks,bool bAddInternalMethodsWithNoCallersAsCallbacks,
            bool bDontMarkAsCallbackOrSinksMethodsWithNoParameters,
            bool bdontAddIfRuleSignatureAlreadyExists, bool bDontAddIfThereAreNoCallersAndCallees,
            string newRulesVulnType)
        {            
            // for performance reasons run this on a separade thread
            O2Thread.mtaThread(
                () =>
                    {
                        // before adding more rules, clear the current list (the return null is to force the Sync version  of invokeOnThread )
                        this.invokeOnThread(()=>
                                                {
                                                    clearChangedRulesList();
                                                    return null;
                                                });
                        
                        var newCallbackSignature = "O2.AutoMapping";
                        DI.log.info("adding {0} cirFunctions as rules", cirFunctionsToProcess.Count);

                        // get rules that we will keep (if there are rules loaded from Db and keepRulesLoadedFromDatabase is set)
                        var newO2Rules = (keepRulesLoadedFromDatabase)
                                             ? O2RulePackUtils.getRulesThatAreFromDB(currentO2RulePack)
                                             : new List<IO2Rule>();

                        //update index
                        indexedCurrentO2Rules = IndexedO2Rules.indexAll(newO2Rules);

                        if (keepRulesLoadedFromDatabase)
                            foreach (var o2Rule in newO2Rules)
                                o2Rule.Tagged = false;

                        var currentDatabaseID = MiscUtils_OunceV6.getIdForSuportedLanguage(currentLanguage).ToString();

                        var listOfChangedRules = new List<IO2Rule>();
                        var functionsToProcess = cirFunctionsToProcess.Count;
                        var functionsProcessed = 0;
                        foreach (var cirFunction in cirFunctionsToProcess)
                        {
                            var functionSignature = new FilteredSignature(cirFunction).sSignature;                            
                            // first check if there are any callers or callees on this function
                            //bDontAddIfThereAreNoCallersAndCallees
                            if (bDontAddIfThereAreNoCallersAndCallees && cirFunction.FunctionIsCalledBy.Count == 0 &&
                                cirFunction.FunctionsCalledUniqueList.Count == 0)
                            {
                                // don't add
                            }
                            else 
                                // then check if this already exists on the database
                                if (bdontAddIfRuleSignatureAlreadyExists &&
                                     indexedCurrentO2Rules.ContainsKey(functionSignature))
                                {
                                    foreach (var o2Rule in indexedCurrentO2Rules[functionSignature])
                                        o2Rule.Tagged = true;
                                }
                                // if not, then we will need ot create a new rule for this function
                                else
                                {                                    
                                    bool addAsNotMappedRule = false;
                                    //var functionSignature = new FilteredSignature(cirFunction).sSignature;

                                    // handle special cases 


                                    //bAddInternalMethodsWithNoCallersAsCallbacks (or the function is explicitly marked with cirFunction.IsTainted
                                    if (cirFunction.IsTainted || 
                                        (bAddInternalMethodsWithNoCallersAsCallbacks && cirFunction.HasControlFlowGraph && cirFunction.FunctionIsCalledBy.Count == 0))
                                    {
                                        //bDontMarkAsCallbackMethodsWithNoParameters
                                        if (false == bDontMarkAsCallbackOrSinksMethodsWithNoParameters ||
                                            cirFunction.FunctionSignature.IndexOf("()") == - 1)
                                            //cirFunction.FunctionParameters.Count > 0)  // can't use this since it is not 100% reliable
                                        {
                                            var newCallback = new O2Rule(O2RuleType.Callback,
                                                                         newCallbackSignature + ".Callback",
                                                                         functionSignature, currentDatabaseID, true);
                                            listOfChangedRules.Add(newCallback);
                                            newO2Rules.Add(newCallback);
                                        }
                                        else
                                            addAsNotMappedRule = true;
                                    }
                                    //bAddExternalMethodsAsSourcesAndSinks  - function calls nobody but it is called by others
                                    else if (bAddExternalMethodsAsSourcesAndSinks &&
                                             cirFunction.FunctionsCalledUniqueList.Count == 0 &&
                                             cirFunction.FunctionIsCalledBy.Count > 0)
                                    {
                                        // when importing CirData created by Core on Java, there is a bug that causes the ReturnType NOT to be populated 
                                        // this is why we use the cirFunction.FunctionSignature.IndexOf(":void") > -1 below instead of the more correct if (string.IsNullOrEmpty(cirFunction.ReturnType))
                                        //if (string.IsNullOrEmpty(cirFunction.ReturnType))
                                        //    DI.log.info("Method had empty cirFunction.ReturnType: {0}", cirFunction.FunctionSignature);
                                        // Only add source if the return parameter is not void
                                        if (!(cirFunction.FunctionSignature.IndexOf(":void") > -1 || cirFunction.ReturnType == "void" || cirFunction.ReturnType == "System.Void"))
                                        {
                                            var newSource = new O2Rule(O2RuleType.Source,
                                                                       newCallbackSignature + ".Source",
                                                                       functionSignature, currentDatabaseID, true);
                                            listOfChangedRules.Add(newSource);
                                            newO2Rules.Add(newSource);
                                        }
                                        else
                                            DI.log.info("Method Not marked as Source: {0}", cirFunction.FunctionSignature);
                                        // for sinks check for bDontMarkAsCallbackOrSinksMethodsWithNoParameters
                                        if (false == bDontMarkAsCallbackOrSinksMethodsWithNoParameters ||
                                            cirFunction.FunctionSignature.IndexOf("()") == -1)
                                            //cirFunction.FunctionParameters.Count > 0)
                                        {

                                            var newSink = new O2Rule(O2RuleType.Sink, newCallbackSignature + ".Sink",
                                                                     functionSignature, currentDatabaseID, true);
                                            listOfChangedRules.Add(newSink);
                                            newO2Rules.Add(newSink);
                                        }
                                        //else                                            
                                        //    DI.log.info("Method Not marked as Sink: ", cirFunction.FunctionSignature);
                                    }
                                    else
                                    {
                                        addAsNotMappedRule = true;
                                    }

                                    if (addAsNotMappedRule)
                                    {
                                        // add as NotMapped
                                        var newO2Rule = new O2Rule(newRulesVulnType, functionSignature, currentDatabaseID) { Tagged = true };
                                        newO2Rules.Add(newO2Rule);
                                    }
                                }
                            // Counter
                            if ((functionsProcessed++)%200 == 0)
                                DI.log.info("In addCirFunctionsAsRules: {0} / {1} cir Functions processed",
                                            functionsProcessed, functionsToProcess);
                        }

                        // make the current rule pack the one with the rules we have just loaded
                            DI.log.info("{0} rules created", newO2Rules.Count);
                            var newRulePack = new O2RulePack("Rule Pack", newO2Rules);
                        setCurrentRulePack(newRulePack);

                        // we need to back to the GUI thread for the updates
                        this.invokeOnThread(
                            () =>
                                {
                                    // hide this ListView for performance reasons
                                    lvChangedRules.Visible = false;
                                    dgvRules.Visible = false;
                                    // add rules
                                    addRulesToChangedRulesList_BatchMode(listOfChangedRules);

                                  //  foreach (var changedO2Rule in listOfChangedRules)
                                  //      addRuleToChangedRulesList(changedO2Rule, false /*checkIfRuleIsAlreadyInList*/);
                                    
                                    // if we are in view all mode changed it to OnlyTaggedRules
                                    if (rbViewMode_AllRules.Checked)
                                        rbViewMode_OnlyTaggedRules.Checked = true;
                                    else
                                        setRulesViewMode(); // trigger refresh

                                    // now that all rules have been added make it visible again
                                    lvChangedRules.Visible = true;
                                    dgvRules.Visible = true;
                                });                                                                        
                    });
        }


        private void addFindingsAsRules(List<IO2Finding> o2Findings, bool bKeepRulesLoadedFromDatabase)
        {
            DI.log.info("Mapping {0} Findings to current loaded rule pack", o2Findings.Count);
            var newRulePack = RulesAndFindingsUtils.mapFindingsToCurrentRulePack(
                currentO2RulePack, o2Findings, MiscUtils_OunceV6.getIdForSuportedLanguage(currentLanguage).ToString(),
                bKeepRulesLoadedFromDatabase);

            setCurrentRulePack(newRulePack);
            refreshRulesViewer();
        }


        // to be removed once the addFindingsAsRules is completed
        /*private void reloadCurrentRulePackWithOnlyRulesThatMatchDroppedFindings(List<IO2Finding> o2Findings)
        {
            O2Thread.mtaThread(
                () =>
                {
                    DI.log.info("Reloading Current RulePack With Only Rules That Match Dropped Findings");
                    // first create rulepack with a rule for every unique signature in a trace
                    var newRulePack = RulesAndFindingsUtils.createRulePackThatMatchFindings(currentO2RulePack, o2Findings, MiscUtils_OunceV6.getIdForSuportedLanguage(currentLanguage).ToString());
                    // set the current rule and reindex it                        
                    setCurrentRulePack(newRulePack);
                    // now make sure that all sources and sinks in the loaded o2Findings are mapped as such in the rules                        
                    RulesAndFindingsUtils.mapInRulePack_FindingsSourcesAndSinks(newRulePack, indexedCurrentO2Rules, o2Findings, MiscUtils_OunceV6.getIdForSuportedLanguage(currentLanguage).ToString());

                    DI.log.info("New rule pack calculated (with {0} rules, reloading it now", newRulePack.o2Rules.Count);
                    refreshRulesViewer();
                }
                );
        }*/

        private void changeSelectedRulesTo(O2RuleType newRuleType, string newVulnType)
        {
            this.invokeOnThread(
                () =>
                    {
                        foreach(DataGridViewRow selectedRow in dgvRules.SelectedRows)
                        {
                            if (selectedRow.Tag is IO2Rule)
                            {
                                var o2RuleToModify = (IO2Rule) selectedRow.Tag;

                                // if required, delete the current rule
                                if (o2RuleToModify.FromDb &&  o2RuleToModify.RuleType != O2RuleType.NotMapped && newRuleType != O2RuleType.ToBeDeleted && cbOnChangeDeletePreviewRule.Checked)
                                {
                                    var clonedRule = O2RulePackUtils.cloneRule(o2RuleToModify);
                                    clonedRule.RuleType = O2RuleType.ToBeDeleted;
                                    addRuleToChangedRulesList(clonedRule);
                                }
                                o2RuleToModify.RuleType = newRuleType;
                                o2RuleToModify.VulnType = newVulnType;
                                switch (o2RuleToModify.RuleType)
                                {
                                    case O2RuleType.PropageTaint:
                                        o2RuleToModify.FromArgs = "all";
                                        o2RuleToModify.ToArgs = "all";
                                        o2RuleToModify.Return = "1";
                                        break;
                                    case O2RuleType.DontPropagateTaint:
                                        o2RuleToModify.FromArgs = "none";
                                        o2RuleToModify.ToArgs = "none";
                                        o2RuleToModify.Return = "0";
                                        break;
                                    default:
                                        o2RuleToModify.FromArgs = "";
                                        o2RuleToModify.ToArgs = "";
                                        o2RuleToModify.Return = "";
                                        break;
                                }
                                selectedRow.SetValues(getRowDataFromO2Fule(o2RuleToModify));
                                addRuleToChangedRulesList(o2RuleToModify);
                            }                            
                        }
                        refreshChangedRulesList();
                    });
        }
        private void newRule()
        {
            var newO2Rule = new O2Rule();
            newO2Rule.Signature = "new rule";
            if (dgvRules.SelectedRows.Count > 0)
                foreach (DataGridViewRow selectedRow in dgvRules.SelectedRows)               // we have to do this because dgvRules.SelectedRows.Clear() doesn't work
                    selectedRow.Selected = false;
            var newRuleRow = addO2RuleToDataGridView(newO2Rule);
            currentO2RulePack.o2Rules.Add(newO2Rule);
            newRuleRow.Selected = true;
            openRulesEditor();
        }

        private void loadMySqlDetails()
        {
            DI.log.info("OunceMySql.MySqlDatabaseName: {0}", OunceMySql.MySqlDatabaseName);
            DI.log.info("OunceMySql.MySqlLoginPassword: {0}", OunceMySql.MySqlLoginPassword);
            DI.log.info("OunceMySql.MySqlLoginUsername: {0}", OunceMySql.MySqlLoginUsername);
            DI.log.info("OunceMySql.MySqlServerIP: {0}", OunceMySql.MySqlServerIP);
            DI.log.info("OunceMySql.MySqlServerPort: {0}", OunceMySql.MySqlServerPort);
            tbMySqlPassword.Text = OunceMySql.MySqlLoginPassword;
            tbMySqlUsername.Text = OunceMySql.MySqlLoginUsername;
            tbMySqlPort.Text = OunceMySql.MySqlServerPort;
            tbMySqlIPAddress.Text = OunceMySql.MySqlServerIP; 
        }
    }
    
}

