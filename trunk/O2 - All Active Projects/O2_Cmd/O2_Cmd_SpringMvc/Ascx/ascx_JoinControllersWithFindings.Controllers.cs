// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Cmd.SpringMvc.Objects;
using O2.Cmd.SpringMvc.Scripts;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Cmd.SpringMvc.Ascx
{
    partial class ascx_JoinControllersWithFindings
    {
        private bool runOnLoad = true;
        public string tempFolderForJoinnedTraces = DI.config.getTempFolderInTempDirectory("_TempSpringMvcJoinedTraces");        
        public void onLoad()
        {

            if (false == DesignMode && runOnLoad)
            {
                runOnLoad = false;
//                directoryWithTempJoinData.openDirectory(tempFolderForJoinnedTraces);
                btLoadTestData_Click(null,null);
                //splitContainer_withTempFileAndJoinTraces.Panel1Collapsed = true;                
            }
        }


        private void generateJspTrace(List<IO2Finding> o2Findings)
        {
            var createdFindings = TraceCreator.generateJspTraces(o2Findings, tempFolderForJoinnedTraces);
            findingsViewerWith_JoinResults.loadO2Assessment(createdFindings);
        }


        private void onSpringMvcMappingsTreeSelect(TreeView treeView)
        {
            treeView.invokeOnThread(
                () =>
                {
                    if (treeView.SelectedNode != null && treeView.SelectedNode.Tag != null)
                    {
                        if (treeView.SelectedNode.Tag is SpringMvcController)
                            showDetailsForSpringMvcController((SpringMvcController)treeView.SelectedNode.Tag);
                    }
                });
        }

        public void showDetailsForSpringMvcController(SpringMvcController springMvcController)
        {
            //var mappedLineNumber = ViewHelpers.GetMappedLineNumber(springMvcController.JavaFunction, springMvcController.)
            sourceCodeEditor_withController.gotoLine(springMvcController.FileName, (int)springMvcController.LineNumber);
            findingsViewerWith_JoinResults.setFilter1Value("Source");
            findingsViewerWith_JoinResults.setFilter2Value("Sink");
            findingsViewerWith_JoinResults.setFilter1TextValue(springMvcController.JavaClassAndFunction,true);
            findingsViewerWith_JoinResults.expandAllNodes();
        }



    }
}
