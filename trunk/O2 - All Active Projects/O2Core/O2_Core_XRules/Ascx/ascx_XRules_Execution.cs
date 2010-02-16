// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Interfaces.XRules;

namespace O2.Core.XRules.Ascx
{
    public partial class ascx_XRules_Execution : UserControl
    {
        public ascx_XRules_Execution()
        {
            InitializeComponent();
        }

        private void ascx_XRules_Execution_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void btCompileRules_Click(object sender, EventArgs e)
        {
            compileXRules();
        }

        private void btExecuteXRule_Click(object sender, EventArgs e)
        {
            executeSelectedXRule();
        }

       

        private void lbLoadedArtifacts_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lbLoadedArtifacts_DragDrop(object sender, DragEventArgs e)
        {
            handleDrop(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e), cbLoadFileAsObject.Checked);
        }

        private void lbCompiledXRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCompiledXRules.SelectedItem != null && lbCompiledXRules.SelectedItem is ILoadedXRule)
            {
                showXRuleDetails((ILoadedXRule)lbCompiledXRules.SelectedItem);
            }
        }

        private void lbXRule_MethodsAvailable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            executeSelectedXRule();
        }

        private void llClearLoadedList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lbLoadedArtifacts.Items.Clear();
        }

        private void lbXRule_MethodsAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbRecompileRulesOnGlobalRecompileEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRecompileRulesOnGlobalRecompileEvent.Checked != recompileRulesOnGlobalRecompileEvent)
                setRecompileRulesOnGlobalRecompileEvent(cbRecompileRulesOnGlobalRecompileEvent.Checked);
        }

        

    }
}
