// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.XRules.Classes;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.Core.XRules.Ascx
{
    public partial class ascx_XRules_UnitTests : UserControl
    {
        public ascx_XRules_UnitTests()
        {
            InitializeComponent();
        }

        private void tvXRulesFromUnitTests_DragDrop(object sender, DragEventArgs e)
        {
            handleDrop(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }        

        private void tvXRulesFromUnitTests_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }                
        
        private void tvXRulesFromUnitTests_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            executeSelectedNode();
        }

        private void llExecuteAllLoadedTests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            executeAllLoadedTests();
        }

        private void llClearResultsView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flowLayoutPanelWithResults.clear();
        }

        private void tvXRulesFromUnitTests_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                executeSelectedNode();
        }

        private void ascx_XRules_UnitTests_Load(object sender, EventArgs e)
        {
            onLoad();
        }                              
    }
}
