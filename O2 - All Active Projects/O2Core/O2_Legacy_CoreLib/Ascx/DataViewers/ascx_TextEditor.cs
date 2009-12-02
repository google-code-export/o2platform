// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_TextEditor : UserControl
    {
        public ascx_TextEditor()
        {
            InitializeComponent();
        }

        /*public ascx_TextEditor(String sUrl)
        {
            InitializeComponent();
            
        }*/

        private void btSelectFileToOpen_Click(object sender, EventArgs e)
        {
            var ofdOpenFileDialog = new OpenFileDialog();
            //ofdOpenFileDialog.SelectedPath = sDirectoryOfFilesToLoad;
            DialogResult drDialogResult = ofdOpenFileDialog.ShowDialog();
            if (drDialogResult == DialogResult.OK)
            {
                tbPathToFileToOpen.Text = ofdOpenFileDialog.FileName;
            }
        }

        private void tbPathToFileToOpen_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(tbPathToFileToOpen.Text))
                rtbText.Text = Files.getFileContents(tbPathToFileToOpen.Text);
        }

        public void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            String sReceivedData = oVar.ToString();
            if (sReceivedData.Length < 255 && File.Exists(sReceivedData))
                rtbText.Text = Files.getFileContents(sReceivedData);
            else
                rtbText.Text = sReceivedData;
        }
    }
}
