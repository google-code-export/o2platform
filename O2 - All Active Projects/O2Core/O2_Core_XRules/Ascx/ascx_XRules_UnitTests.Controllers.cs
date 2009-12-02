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
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.Kernel.Interfaces.Messages;
using O2.DotNetWrappers.Windows;

namespace O2.Core.XRules.Ascx
{
    public partial class ascx_XRules_UnitTests
    {
        private bool runOnLoad = true;

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {                
                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;
                runOnLoad = false;
            }
        }

        void o2MessageQueue_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_DotNetAssemblyAvailable)
            {
                var dotNetAssemblyAvailable = (IM_DotNetAssemblyAvailable)o2Message;
                loadFile(dotNetAssemblyAvailable.pathToAssembly);                
                XRulesTreeView_ExpandAll();
                if (cbAutoExecuteUnitTestAfterLoad.Checked)
                    executeAllLoadedTests();
            }
        }

        public void clearLoadedAssemblies()
        {
            tvXRulesFromUnitTests.invokeOnThread(() => tvXRulesFromUnitTests.Nodes.Clear());
        }

        public void handleDrop(string fileOrFolder)
        {
            if (Directory.Exists(fileOrFolder))
                loadDirectory(fileOrFolder);    
            else
                loadFile(fileOrFolder);
        }

        public void loadDirectory(string directoryToLoad)
        {
            loadDirectory(directoryToLoad, true);
        }

        public void loadDirectory(string directoryToLoad, bool clearPreviousLoadedList)
        {
            foreach (var file in Files.getFilesFromDir_returnFullPath(directoryToLoad))
                loadFile(file, clearPreviousLoadedList);
        }

        public void loadFile(string fileToLoad)
        {
            loadFile(fileToLoad,true);
        }

        public void loadFile(string fileToLoad, bool clearPreviousLoadedList)
        {
            if (clearPreviousLoadedList)
                clearLoadedAssemblies();
            UnitTestExecutionViewHelpers.mapUnitTestToXRules(fileToLoad, tvXRulesFromUnitTests);
        }

        public TreeView getXRulesTreeView()
        {
            return tvXRulesFromUnitTests;
        }

        public void XRulesTreeView_ExpandAll()
        {
            tvXRulesFromUnitTests.invokeOnThread(() => tvXRulesFromUnitTests.ExpandAll());
        }
        
        public void executionResult(bool result, object resultData)
        {
            progressBarExecutionStatus.ts_Increment(1);            
            if (result)
            {
                DI.log.debug("Execution result was: true");
                UnitTestExecutionViewHelpers.addResultToFlowLayoutPanel(flowLayoutPanelWithResults,resultData, Color.LightGreen);
            }
            else
            {
                DI.log.debug("Execution result was: false");
                UnitTestExecutionViewHelpers.addResultToFlowLayoutPanel(flowLayoutPanelWithResults,resultData, Color.Salmon);
            }
        }

        public void onComplete()
        {
            progressBarExecutionStatus.ts_Value(progressBarExecutionStatus.Maximum);            
        }

        //private void executeXRulesInSelectedNode()
        //{
        //    UnitTestExecutionViewHelpers.mapLoadedAssembliesUnitTestsToXRules(tvXRulesFromUnitTests);
        //}

        public void resetFlowPanelLayoutGUI()
        {
            flowLayoutPanelWithResults.ts_Clear();
            progressBarExecutionStatus.ts_Value(0);
        }

        public void executeSelectedNode()
        {
            resetFlowPanelLayoutGUI();
            //UnitTestExecution.executeXRuleInSelectedTreeViewNode(tvXRulesFromUnitTests, executionResult, onComplete);            
            var methodsToExecute = UnitTestSupport.getMethodsToExecuteFromSelectedTreeViewNode(tvXRulesFromUnitTests);
            executeMethods(methodsToExecute);            
        }


        private void executeAllLoadedTests()
        {
            resetFlowPanelLayoutGUI();
            var methodsToExecute = UnitTestSupport.getMethodsToExecuteFromTreeView(tvXRulesFromUnitTests);
            executeMethods(methodsToExecute);       
        }

        public void executeMethods(List<MethodInfo> methodsToExecute)
        {            
            progressBarExecutionStatus.ts_Maximum(methodsToExecute.Count);
            UnitTestExecution.executeXRuleMethods(methodsToExecute, executionResult, onComplete);
        }
    }
}
