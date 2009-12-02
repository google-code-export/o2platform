// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using System.IO;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.External.WinFormsUI.Ascx
{
    public partial class ascx_ViewO2Config : UserControl
    {
        public ascx_ViewO2Config()
        {
            InitializeComponent();
        }

        private void ascx_ViewO2Config_Load(object sender, EventArgs e)
        {
            loadCurrentO2ConfigEnvironment();
        }
        
        private void btCreateLocalO2ConfigFile_Click(object sender, EventArgs e)
        {
            createLocalO2ConfigFile();
        }
       
        private void llRefreshMainO2ConfigFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadCurrentO2ConfigEnvironment();
        }

        private void cbManuallyEditO2ConfigFile_CheckedChanged(object sender, EventArgs e)
        {
            setManualEditMode(cbManuallyEditO2ConfigFile.Checked);
        }
        
        private void btRestoreToDefaultValues_Click(object sender, EventArgs e)
        {
            O2ConfigLoader.createDefaultO2ConfigFile();
        }

        private void btSaveMainO2ConfigFile_Click(object sender, EventArgs e)
        {
            saveMainO2ConfigFile();
        }
        
        private void llRefreshMemoryViewOfO2ConfigFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showMemoryViewOfO2ConfigFiles();
        }

        private void tcO2ConfigFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcO2ConfigFiles.SelectedTab == tpCurrentO2ConfigValuesInMemory)
                showMemoryViewOfO2ConfigFiles();
        }

        private void btSaveLocalConfigFile_Click(object sender, EventArgs e)
        {
            saveLocalO2ConfigFile(tbLocationOfLocalO2ConfigFile.Text, tbLocalO2ConfigFile.Text);
        }

        private void tbLocalO2ConfigFile_TextChanged(object sender, EventArgs e)
        {
            lbContentsBreakLocalO2ConfigSchema.Visible = doesTextBreaksO2ConfigSchema(tbLocalO2ConfigFile.Text);
            btSaveLocalConfigFile.Enabled = ! lbContentsBreakLocalO2ConfigSchema.Visible;
        }

        private void tbMainO2ConfigFile_TextChanged(object sender, EventArgs e)
        {
            lbContentsBreakMainO2ConfigSchema.Visible = doesTextBreaksO2ConfigSchema(tbMainO2ConfigFile.Text);
            btSaveMainO2ConfigFile.Enabled = !lbContentsBreakMainO2ConfigSchema.Visible;
        }
                
    }
}
