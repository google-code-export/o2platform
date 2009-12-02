using System;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_RichTextEditor : UserControl
    {
        public ascx_RichTextEditor()
        {
            InitializeComponent();
        }

        public ascx_RichTextEditor(String sUrl)
        {
            InitializeComponent();
            OpenFile(sUrl);
        }

        private void btSelectFileToOpen_Click(object sender, EventArgs e)
        {
            var ofdOpenFileDialog = new OpenFileDialog();
            //ofdOpenFileDialog.SelectedPath = sDirectoryOfFilesToLoad;
            DialogResult drDialogResult = ofdOpenFileDialog.ShowDialog();
            if (drDialogResult == DialogResult.OK)
            {
                OpenFile(ofdOpenFileDialog.FileName);
            }
        }

        private void tbPathToFileToOpen_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(tbPathToFileToOpen.Text))
                OpenFile(tbPathToFileToOpen.Text);
        }

        public void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            String sReceivedData = oVar.ToString();
            if (sReceivedData.Length < 255 && File.Exists(sReceivedData))
                OpenFile(sReceivedData);
            else
                rtbText.Text = sReceivedData;
        }

        public void OpenFile(String sFileToOpen)
        {
            if (File.Exists(sFileToOpen))
            {
                tbPathToFileToOpen.Text = sFileToOpen;
                rtbText.Text = Files.getFileContents(sFileToOpen);
            }
        }

        private void rtbText_TextChanged(object sender, EventArgs e)
        {
            if (cbAutoSaveOnChange.Checked)
                saveFile(tbPathToFileToOpen.Text);
        }

        public void saveFile(string sPathToSaveFileTo)
        {
            switch (Path.GetExtension(sPathToSaveFileTo))
            {
                case "rtf":
                    rtbText.SaveFile(sPathToSaveFileTo);
                    break;
                    //case ".txt":
                default:
                    Files.WriteFileContent(sPathToSaveFileTo, rtbText.Text);
                    break;
            }
        }

        public RichTextBox getRichTextBoxbObject()
        {
            return rtbText;
        }
    }
}