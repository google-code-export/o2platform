// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace O2.Scanner.DotNet.Ascx
{
    public partial class ascx_PatchAndMonitor : UserControl
    {
        public ascx_PatchAndMonitor()
        {
            InitializeComponent();
        }

        private void ascx_PatchAndMonitor_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void btInstallHookIntoNewAssembly_Click(object sender, EventArgs e)
        {
            llHookedAssembly.Enabled = true;
            insertHooksIntoNewAssembly();
        }

        private void llTestCodeAssembly_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(sourceCodeEditor.compiledAssembly.Location, DragDropEffects.Copy);
        }

        private void llHookedAssembly_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(hookedAssembly, DragDropEffects.Copy);            
        }

        private void llHookedAssembly_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }      
    }
}
