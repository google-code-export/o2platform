// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using O2.Core.XRules.XRulesEngine;
using O2.External.SharpDevelop.Ascx;
using O2.Kernel;
using O2.Kernel.CodeUtils;
using O2.DotNetWrappers.DotNet;

namespace O2.Core.XRules.Ascx
{
    public partial class ascx_XRules_Editor : UserControl
    {
        public ascx_XRules_Editor()
        {
            InitializeComponent();
        }

        private void ascx_XRules_Editor_Load(object sender, EventArgs e)
        {
            onLoad();
        }
        
        
        private void btCreateRuleFromTemplate_Click(object sender, EventArgs e)
        {
            if (lbCurrentXRulesTemplates.SelectedItem != null)
            {
                var templateFile = Path.Combine(XRules_Config.PathTo_XRulesTemplates,lbCurrentXRulesTemplates.SelectedItem.ToString());
                createNewRuleFromTemplate(templateFile, tbNewRuleName.Text);
            }
        }
        

        private void llRemoveSelectedSourceCodeFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tcTabControlWithRulesSource.SelectedTab != null)
                removeFileInTab(tcTabControlWithRulesSource.SelectedTab);
            /*
            != null && tcTabControlWithRulesSource.SelectedTab.Controls.Count == 1
                && tcTabControlWithRulesSource.SelectedTab.Controls[0] is ascx_SourceCodeEditor)
            {
                removeFile(tcTabControlWithRulesSource.SelectedTab,(ascx_SourceCodeEditor) tcTabControlWithRulesSource.SelectedTab.Controls[0])
            }*/
                
        }

        private void llReloadSelectedSourceCodeFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tcTabControlWithRulesSource.SelectedTab != null && tcTabControlWithRulesSource.SelectedTab.Controls.Count == 1
                && tcTabControlWithRulesSource.SelectedTab.Controls[0] is ascx_SourceCodeEditor)
                reloadFile((ascx_SourceCodeEditor) tcTabControlWithRulesSource.SelectedTab.Controls[0]);    
        }

        private void btLoadUnitTestFromLocalO2Development_Click(object sender, EventArgs e)
        {
            XRules_Config.PathTo_XRulesDatabase_fromLocalDisk = DI.PathToLocalUnitTestsFiles;
            loadXRuleDatabase();
        }

        private void btLoadXRulesUnitTests_Click(object sender, EventArgs e)
        {
            XRules_Config.PathTo_XRulesDatabase_fromLocalDisk = DI.PathToLocalXRulesUnitTestsFiles;
            loadXRuleDatabase();
        }

        private void tbFileToOpen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var selectedFile = ((ToolStripTextBox)sender).Text;
                PublicDI.log.info("Opening file: {0}", selectedFile);
                loadFile(selectedFile,false);                
            }
        }

        private void directoryWithLocalXRules__onDirectoryDoubleClick(string fileOrDir)
        {
            var fileOpened = loadFile(fileOrDir, true);                           
        }

        private void directoryWithXRulesDatabase__onDirectoryDoubleClick(string fileOrDir)
        {
            loadFile(fileOrDir, true);        
        }

        private void directoryWithLocalXRules__onDirectoryClick(string fileOrDir)
        {   
            CompileEngine.addExtraFileReferencesToSelectedNode(directoryWithLocalXRules.getTreeView(), fileOrDir);
                
        }

        private void directoryWithXRulesDatabase__onDirectoryClick(string fileOrDir)
        {
            CompileEngine.addExtraFileReferencesToSelectedNode(directoryWithXRulesDatabase.getTreeView(), fileOrDir);            
        }

        private void cbShowFileContentsOnMouseOver_CheckedChanged(object sender, EventArgs e)
        {
            directoryWithLocalXRules._ShowFileContentsOnTopTip = cbShowFileContentsOnMouseOver.Checked;
            directoryWithXRulesDatabase._ShowFileContentsOnTopTip = cbShowFileContentsOnMouseOver.Checked;
        }
       
        private void llReloadXRules_Click(object sender, EventArgs e)
        {
            loadXRuleDatabase();
            O2Messages.dotNetAssemblyAvailable("");  // simulate this event so that we trigger XRules recompilation (if XRules_Excution is open)
        }

        private void btBackupLocalFiles_Click(object sender, EventArgs e)
        {

        }

        private void btSyncViaSvn_Click(object sender, EventArgs e)
        {

        }

        private void btBrowseSVN_Click(object sender, EventArgs e)
        {
            
        }
      
    }
}
