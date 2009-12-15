// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_Variables : UserControl
    {        
        public ascx_Variables()
        {
            InitializeComponent();
            onLoad();            
        }

        private void tvVariables_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            onVariablesTreeViewBeforeExpand(e.Node);
        }

        private void tbExecuteOnFrame_TextChanged(object sender, EventArgs e)
        {

        }

        private void tvVariables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is O2MDbgVariable)
                showVariablesDetails((O2MDbgVariable) e.Node.Tag);
        }

        private void btExecuteOnFrame_Click(object sender, EventArgs e)
        {
            O2Thread.mtaThread(()=>
                                   {
                                       DI.log.debug("Executing: {0}", tbExecuteOnFrame.Text);
                                       var result = O2MDbgUtils.execute(tbExecuteOnFrame.Text);                                       
                                       DI.log.debug("Execution result: {0}", (result ?? "<null>"));
                                   });              
        }

        private void btChangeVariableValue_Click(object sender, EventArgs e)
        {
            if (tvVariables.SelectedNode != null && tvVariables.SelectedNode.Tag is O2MDbgVariable)
                O2MDbgUtils.setVariableValue((O2MDbgVariable) tvVariables.SelectedNode.Tag, tbVariableValue.Text);
        }

        private void btGetVariableValue_Click(object sender, EventArgs e)
        {
            if (tvVariables.SelectedNode != null && tvVariables.SelectedNode.Tag is O2MDbgVariable)
            {
                O2MDbgUtils.showVariableValue((O2MDbgVariable)tvVariables.SelectedNode.Tag);
            }
        }

        private void llReloadData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showFrameVariables();
        }       
    }
}
