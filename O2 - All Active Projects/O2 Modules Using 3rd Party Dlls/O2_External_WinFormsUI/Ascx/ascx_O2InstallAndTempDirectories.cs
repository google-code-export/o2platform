using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;
using O2.DotNetWrappers.Windows;

namespace O2.External.WinFormsUI.Ascx
{
    public partial class ascx_O2InstallAndTempDirectories : UserControl
    {
        public ascx_O2InstallAndTempDirectories()
        {
            InitializeComponent();
        }

        private void btChangeTempDir_Click(object sender, EventArgs e)
        {
            PublicDI.config.O2TempDir = tbCurrentO2TempDir.Text;
            DI.config.O2TempDir = tbCurrentO2TempDir.Text;
            directoryWithO2TempDir.setDirectory(PublicDI.config.O2TempDir);
        }

        private void btDeleteTempFolderContents_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show(
                    "Are you sure you want to delete the entire contents of the folder " + DI.config.O2TempDir + " ?",
                    "Confirm O2 Temp Folder deletion (after deletion, an empty folder will be created)",
                    MessageBoxButtons.YesNo))
            {
                O2Thread.mtaThread(
                    () =>
                        {
                            this.invokeOnThread(() => lbMessage_DeletingTempFolder.Visible = true);
                            Files.deleteFolder(DI.config.O2TempDir,true);
                            Files.checkIfDirectoryExistsAndCreateIfNot(DI.config.O2TempDir, true);
                            this.invokeOnThread(() => lbMessage_O2TempFolderContentsDeleted.Visible = true);
                        }
                    );
            }
        }

        private void ascx_O2InstallAndTempDirectories_Load(object sender, EventArgs e)
        {
            lbMessage_DeletingTempFolder.Visible = false;
            lbMessage_O2TempFolderContentsDeleted.Visible = false;
            if (PublicDI.config.O2TempDir != DI.config.O2TempDir)
                DI.log.showMessageBox("Something wrong with the config files since PublicDI.config.O2TempDir != DI.config.O2TempDir");
            directoryWithO2TempDir.setDirectory(tbCurrentO2TempDir.Text = DI.config.O2TempDir);
            directoryWithO2Install.setDirectory(tbCurrentO2InstallDirectory.Text = DI.config.CurrentExecutableDirectory);            
        }
        
        

        
    }
}
