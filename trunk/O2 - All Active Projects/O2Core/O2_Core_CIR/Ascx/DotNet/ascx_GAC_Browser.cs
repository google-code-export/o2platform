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
using O2.Interfaces.DotNet;
using O2.Kernel.CodeUtils;

namespace O2.Core.CIR.Ascx.DotNet
{
    public partial class ascx_GAC_Browser : UserControl
    {
        public ascx_GAC_Browser()
        {
            InitializeComponent();
        }        

        private void llBackUpGAC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GacUtils.backupGac(getBackupFilePath(directory_ToBackupGAC.getCurrentDirectory()));
        }

        private void tvListOfGacAssemblies_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvListOfGacAssemblies.SelectedNode != null && tvListOfGacAssemblies.SelectedNode.Tag is IGacDll)
                Callbacks.raiseRegistedCallbacks(_onGacDllSelected, new object[]
                                                                        {
                                                                                (IGacDll) tvListOfGacAssemblies.SelectedNode.Tag
                                                                        });
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tbGacAssemblyFilter_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}
