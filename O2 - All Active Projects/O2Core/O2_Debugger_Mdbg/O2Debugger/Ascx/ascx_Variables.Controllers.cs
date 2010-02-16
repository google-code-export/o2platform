// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.Messages;
using O2.Kernel;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_Variables
    {
        bool runOnLoad = true;

        void onLoad()
        {
            if (runOnLoad && DesignMode == false)
            {
                PublicDI.o2MessageQueue.onMessages += ascx_CurrentFrameDetails_onMessages;
                runOnLoad = false;
            }
        }

        void ascx_CurrentFrameDetails_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_O2MdbgAction)
            {
                var o2MDbgAction = (IM_O2MdbgAction)o2Message;
                switch (o2MDbgAction.o2MdbgAction)
                {
                    case IM_O2MdbgActions.breakEvent:
                        showFrameVariables();
                        break;                    
                }
            }
        }

        private void onVariablesTreeViewBeforeExpand(TreeNode treeNode)
        {
            if (treeNode.Tag != null && treeNode.Tag is O2MDbgVariable)
            {
                O2Thread.mtaThread(() =>
                {
                    var o2MDbgVariable = (O2MDbgVariable)treeNode.Tag;
                    var o2MDbgvariables = DI.o2MDbg.sessionData.getCurrentFrameVariable(o2MDbgVariable);
                    populateTreeNodeCollectionWithVariables(tvVariables, treeNode.Nodes, o2MDbgvariables);
                });
                //  thread.Join();
            }
        }

        public void showFrameVariables()
        {
            var expandDepth = 0;
            var canDoFunceval = true;
            var o2MDbgvariables = DI.o2MDbg.sessionData.getCurrentFrameVariables(expandDepth, canDoFunceval);
            if (o2MDbgvariables.Count > 0)
                populateTreeNodeCollectionWithVariables(tvVariables, tvVariables.Nodes, o2MDbgvariables);
            
        }

        public static void populateTreeNodeCollectionWithVariables(TreeView targetTreeView, TreeNodeCollection nodes, List<O2MDbgVariable> o2MDbgvariables)
        {
            targetTreeView.invokeOnThread(
                () =>
                {
                    nodes.Clear();
                    foreach (var o2MDbgvariable in o2MDbgvariables)
                    {

                        // var nameLvSubItem = new ListViewItem.ListViewSubItem() 
                        var nodeText =
                        string.Format("{0} = {1}  : {2}", o2MDbgvariable.name, o2MDbgvariable.value,
                                      o2MDbgvariable.type);
                        var newTreeNode = O2Forms.newTreeNode(nodes, nodeText, 0, o2MDbgvariable);
                        if (o2MDbgvariable.complexType)
                            newTreeNode.Nodes.Add("DymmyNode");
                    }
                });
        }

        /*lvVariables.invokeOnThread(
                () =>
                    {
        */        

        private void showVariablesDetails(O2MDbgVariable o2MDbgVariable)
        {
            this.invokeOnThread(
                () =>
                {
                    laVariableType.Text = o2MDbgVariable.type;
                    laVariableFullName.Text = o2MDbgVariable.fullName;
                    tbVariableValue.Text = o2MDbgVariable.value;
                });
        }
        

        private void ascx_Variables_Load(object sender, EventArgs e)
        {
            tvVariables.Sort();
        }
    }
}
