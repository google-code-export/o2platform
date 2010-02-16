// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.Core.XRules.Classes;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.Interfaces.Messages;
using O2.Interfaces.XRules;
using O2.Kernel;

namespace O2.Core.XRules.Ascx
{
    public partial class ascx_XRules_UnitTestExecution_BigGUI
    {
        private bool runOnLoad = true;

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                //directory_targetAssemblies.openDirectory(DI.config.CurrentExecutableDirectory);
                directory_targetAssemblies.openDirectory(@"E:\O2\_Bin_(O2_Binaries)\_UnitTests");
                //directory_targetAssemblies.setFileFilter("_O2_UnitTests*.dll");
                directory_targetAssemblies.setFileFilter("*.dll");

                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;

                runOnLoad = false;
            }
        }

        void o2MessageQueue_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_DotNetAssemblyAvailable)
            {
                var dotNetAssemblyAvailable = (IM_DotNetAssemblyAvailable) o2Message;
                if (cbAddAssemblyOnAssemblyCompileEvent.Checked && File.Exists(dotNetAssemblyAvailable.pathToAssembly))
                {
                    loadFile(dotNetAssemblyAvailable.pathToAssembly, true);
                    mapLoadedAssembliesIntoXRules();
                    tvXRules_IfPossibleAutoExpandAll();
                    if (cbAutoExecuteUnitTestAfterLoad.Checked)                    
                        executeAllLoadedXRules();                    
                }
                // we could add logic here to see if the current dll is a rule (for new, trigger compilation for all IM_DotNetAssemblyAvailable events);                 
                //    compileXRules();
            }
        }

        private void tvXRules_IfPossibleAutoExpandAll()
        {
            tvXRules.invokeOnThread(
                () =>
                    {
                        if (tvXRules.Nodes.Count < 5)
                            tvXRules.ExpandAll();
                    });
        }

        private void executeAllLoadedXRules()
        {
            DI.log.debug("Executing All Loaded XRules");
            flowLayoutPanelWithResults.ts_Clear();            
            var loadedXRules = (List<ILoadedXRule>)tvXRules.Tag;
            if (loadedXRules != null)
            {
                DI.log.debug("There are {0} LoadedXRule to execute", loadedXRules.Count);
                foreach (var loadedXRule in loadedXRules)
                    UnitTestExecution.executeXRuleMethods(loadedXRule, executionResult, onComplete);
            }
        }

        public void mapLoadedAssembliesIntoXRules()
        {
            UnitTestExecutionViewHelpers.mapAssembliesIntoXRules(tvAssembliesToLookForUnitTests, tvXRules);
        }
         
        public void executionResult(bool result, object returnData)
        {
            if (result)
            {
                DI.log.debug("Execution result was: true");                
                UnitTestExecutionViewHelpers.addResultToFlowLayoutPanel(flowLayoutPanelWithResults, returnData,  Color.Green);
            }
            else
            {
                DI.log.debug("Execution result was: false");
                UnitTestExecutionViewHelpers.addResultToFlowLayoutPanel(flowLayoutPanelWithResults, returnData, Color.Red);
            }
        }

        public void onComplete()
        {
        }

        public void handleDrop(string fileOrFolder)
        {
            if (System.IO.File.Exists(fileOrFolder))
            {
                loadFile(fileOrFolder,true);                
            }
            mapLoadedAssembliesIntoXRules();
            //UnitTestExecutionViewHelpers.mapAssembliesIntoXRules(tvAssembliesToLookForUnitTests, tvXRules);
        }

        public void loadFile(string fileToLoad, bool clearLoadedList)
        {
            if (UnitTestSupport.doesAssemblyReferenceNUnit(fileToLoad))
                UnitTestExecutionViewHelpers.addAssembliesWithUnitTestsToTreeView(new List<string> { fileToLoad }, tvAssembliesToLookForUnitTests, clearLoadedList);
        }

        public void tvXRules_executeSelectedNode()
        {
            UnitTestExecution.executeXRuleInSelectedTreeViewNode(tvXRules,executionResult, onComplete);
        }

    }
}
