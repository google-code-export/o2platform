using System;
using System.IO;
using System.Windows.Forms;
using O2.Core.XRules.XRulesEngine;
using O2.External.SharpDevelop.Ascx;
using O2.Kernel;
using O2.Kernel.CodeUtils;

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

        private void directoryWithXRulesDatabase__onDirectoryDoubleClick(string fileOrDir)
        {
            loadFile(fileOrDir);
        }

        private void directoryWithLocalXRules__onDirectoryClick(string fileOrDir)
        {
            loadFile(fileOrDir);
        }

        private void btCreateRuleFromTemplate_Click(object sender, EventArgs e)
        {
            if (lbCurrentXRulesTemplates.SelectedItem != null)
            {
                var templateFile = Path.Combine(XRules_Config.PathTo_XRulesTemplates,lbCurrentXRulesTemplates.SelectedItem.ToString());
                createNewRuleFromTemplate(templateFile, tbNewRuleName.Text);
            }
        }

        private void llReloadLocalXRulesDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadXRuleDatabase();
            O2Messages.dotNetAssemblyAvailable("");  // simulate this event so that we trigger XRules recompilation (if XRules_Excution is open)
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
                loadFile(selectedFile);                
            }
        }                        
    }
}