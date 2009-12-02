// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Core.XRules.XRulesEngine;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.XRules;
using O2.Kernel.InterfacesBaseImpl;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.Core.XRules.Ascx
{
    partial class ascx_XRules_Execution
    {
        public bool recompileRulesOnGlobalRecompileEvent;
        public Dictionary<Type, object> loadedArtifacts = new Dictionary<Type, object>();

        private bool runOnLoad = true;

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                XRules_DatabaseSetup.installXRulesDatabase();                                                        
                runOnLoad = false;
                // for performance reasons don't compile on load
                //compileXRules();
                KO2MessageQueue.getO2KernelQueue().onMessages += o2Kernel_onMessages;
                findingsViewer_XRulesExecution._ShowNoEnginesLoadedAlert = false;
                setRecompileRulesOnGlobalRecompileEvent(false);
            }
        }

        void o2Kernel_onMessages(IO2Message o2Message)
        {
            // if there is a new assembly check if we need to recompile the current rules
            if (o2Message is IM_DotNetAssemblyAvailable)
            {
                if (recompileRulesOnGlobalRecompileEvent)
                // we could add logic here to see if the current dll is a rule (for new, trigger compilation for all IM_DotNetAssemblyAvailable events);                 
                    compileXRules();
                //loadAssembly(((IM_DotNetAssemblyAvailable)o2Message).pathToAssembly);
            }
        }

        public void setRecompileRulesOnGlobalRecompileEvent(bool value)
        {
            recompileRulesOnGlobalRecompileEvent = value;
            cbRecompileRulesOnGlobalRecompileEvent.invokeOnThread(
                () => cbRecompileRulesOnGlobalRecompileEvent.Checked = value);
        }

        public void compileXRules()
        {
            //clearView();
            // fire ASync compilation process       
            setGuiEnableState(false);
            XRules_Compilation.compileXRules(addXRulesToView, setCurrentTask, setRulesCompilationProgressBarMaxValue, incrementRulesCompilationProgressbar);
        }

        private void setGuiEnableState(bool state)
        {
            this.invokeOnThread(
                () =>
                    {
                        btReCompileRules.Enabled = state;
                        btExecuteXRule.Enabled = state;
                        lbCompiledXRules.Enabled = state;                        
                        lbXRule_MethodsAvailable.Enabled = state;
                    });
        }

        /*public void clearView()
        {
            this.invokeOnThread(() => lbCompiledXRules.Items.Clear());
        }*/

        public void setCurrentTask(string currentTask)
        {
            this.invokeOnThread(() => lbCurrentTask.Text = currentTask);
        }

        public void addXRulesToView(List<IXRule> xRules)
        {
            var loadedXRules = XRules_Execution.getLoadedXRules(xRules);
            
            this.invokeOnThread(
                () =>
                    {
                        try
                        {

                            var currentSelectedItem = lbCompiledXRules.SelectedItem;
                            lbCompiledXRules.Enabled = false;
                            lbCompiledXRules.Items.Clear();
                            lbCompiledXRules.Items.AddRange(loadedXRules.ToArray());
                            if (lbCompiledXRules.Items.Count > 0)
                            {
                                if (currentSelectedItem == null)
                                    lbCompiledXRules.SelectedIndex = 0;
                                else
                                    foreach (var newItem in lbCompiledXRules.Items)
                                        if (newItem.ToString() == currentSelectedItem.ToString())
                                        {
                                            lbCompiledXRules.SelectedItem = newItem;
                                            return;
                                        }
                            }
                        }
                        finally
                        {
                            DI.log.info("Completed addXRulesToView");
                            //lbCompiledXRules.Enabled = true;
                            setGuiEnableState(true);
                        }
                    });            
        }

        public void setRulesCompilationProgressBarMaxValue(int maxValue)
        {
            this.invokeOnThread(
                () =>
                    {
                        progressBar_RulesCompilation.Value = 0;
                        progressBar_RulesCompilation.Maximum = maxValue;
                    });
        }

        public void incrementRulesCompilationProgressbar()
        {
            this.invokeOnThread(
                () =>
                    {
                        progressBar_RulesCompilation.Increment(1);
                        progressBar_RulesCompilation.Refresh();
                    }
                );
        }

        private void handleDrop(string fileOrFolder, bool loadFileAsObject)
        {
            if (XRules_Config.xRulesDatabase != null)
            {
                XRules_Config.xRulesDatabase.loadArtifact(fileOrFolder, loadedArtifacts, loadFileAsObject);
                lbLoadedArtifacts.Items.Clear();
                foreach (var artifact in loadedArtifacts.Values)  //getLoadedArtifacts())
                    lbLoadedArtifacts.Items.Add(artifact);
            }
            else
                DI.log.info("There is no xRulesDatabase available");
        }               

        public void showXRuleDetails(ILoadedXRule xLoadedRule)
        {
            try
            {
                var currentSelectedIndex = lbXRule_MethodsAvailable.SelectedIndex;  // I really should use the selectedItem but it is throwing a weird error
                lbXRule_MethodsAvailable.Enabled = false;
                lbXRule_MethodsAvailable.Items.Clear();
                //methodsInCurrentRule = new Dictionary<string, MethodInfo>();

                foreach (var attribute in xLoadedRule.methods.Keys)
                    lbXRule_MethodsAvailable.Items.Add(attribute);

                Application.DoEvents();
                if (lbXRule_MethodsAvailable.Items.Count > 0)
                    if (currentSelectedIndex > -1)// != null)
                    {
                        lbXRule_MethodsAvailable.SelectedIndex = currentSelectedIndex;
                        autoExecuteMethodIfRequired();


                        /*foreach (var newItem in lbXRule_MethodsAvailable.Items)
                            if (newItem.ToString() == currentSelectedItem.ToString())
                                lbXRule_MethodsAvailable.SelectedItem = newItem;*/
                    }
                    else
                        lbXRule_MethodsAvailable.SelectedIndex = 0;
                /*foreach(var method in DI.reflection.getMethods(XRule.GetType(), new XRuleAttribute()))
                {          
                                
                    lbXRule_MethodsAvailable.Items.Add(method);
                }*/
            }
            catch (Exception ex)
            {
                DI.log.error("in showXRuleDetails: {0}", ex);

            }
            lbXRule_MethodsAvailable.Enabled = true;
        }

        public void autoExecuteMethodIfRequired()
        {
            this.invokeOnThread(
                () =>
                    {
                        if (cbAutoExecuteLastMethod.Checked)
                            executeSelectedXRule();
                    });
        }

        private void executeXRuleMethod(ILoadedXRule xLoadedRule, XRuleAttribute attribute)
        {
            if (xLoadedRule.methods.ContainsKey(attribute))
            {                
                //var _loadedArtifacts = getLoadedArtifacts();

                var methodToExecute = xLoadedRule.methods[attribute];
                var methodParameters = new List<Object>();
                foreach(var parameter in methodToExecute.GetParameters())
                    if (loadedArtifacts.ContainsKey(parameter.ParameterType))
                        methodParameters.Add(loadedArtifacts[parameter.ParameterType]);               
                DI.reflection.invokeASync(xLoadedRule.XRule, methodToExecute, methodParameters.ToArray(), onXRuleExecutionCompletion);                
            }
        }

      /*  public Dictionary<Type, object> getLoadedArtifacts()
        {
            var loadedArtifacts = new Dictionary<Type, object>();
            foreach (var artifact in lbLoadedArtifacts.Items)
                if (loadedArtifacts.ContainsKey(artifact.GetType()))
                    DI.log.error("in executeXRuleMethod, there were more than one artifact of the type: {0} (only the first one will be used", artifact.GetType());
                else
                    loadedArtifacts.Add(artifact.GetType(), artifact);
            return loadedArtifacts;
        }*/

        public void onXRuleExecutionCompletion(object methodExecutionReturnData)
        {
            DI.log.info("on onXRuleExecutionCompletion");
            if (methodExecutionReturnData != null)
            {
                DI.log.info("methodExecutionReturnData has type: {0}", methodExecutionReturnData.GetType().FullName);
                if (methodExecutionReturnData is List<IO2Finding>)
                    findingsViewer_XRulesExecution.loadO2Findings((List<IO2Finding>)methodExecutionReturnData, true);
                else //if (methodExecutionReturnData is string)
                    addXRuleExecutionLogEntry(methodExecutionReturnData.ToString());
            }
            else
                addXRuleExecutionLogEntry("[null]");
        }

        public void clearXRuleExecutionLog()
        {
            this.invokeOnThread(() => tbXRuleExecutionLog.Text = "");
        }

        public void addXRuleExecutionLogEntry(string text)
        {
            this.invokeOnThread(() => tbXRuleExecutionLog.Text = string.Format("{0}{1}{2}", text, Environment.NewLine, tbXRuleExecutionLog.Text));
        }

        private void executeSelectedXRule()
        {
            if (lbCompiledXRules.SelectedItem != null && lbCompiledXRules.SelectedItem is ILoadedXRule &&
                lbXRule_MethodsAvailable.SelectedItem != null && lbXRule_MethodsAvailable.SelectedItem is XRuleAttribute)
                executeXRuleMethod((ILoadedXRule)lbCompiledXRules.SelectedItem, (XRuleAttribute)lbXRule_MethodsAvailable.SelectedItem);
        }
 
    }
}
