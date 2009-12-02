using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Tool.WebInspectConverter.classes;
using O2.Tool.WebInspectConverter.Converter;
using O2.DotNetWrappers.Windows;

namespace O2.Tool.WebInspectConverter.ascx
{
    public partial class ascx_WebInspectResults : UserControl
    {
        private WebInspectResults webInspectResults = new WebInspectResults();

        public ascx_WebInspectResults()
        {
            InitializeComponent();
        }

        private void lbLoadedFiles_DragDrop(object sender, DragEventArgs e)
        {
            var droppedFile = Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);
            if (Directory.Exists(droppedFile))            
                foreach (var file in Files.getFilesFromDir_returnFullPath(droppedFile))
                    loadWebInspectScanFile(file);                    
            else
                loadWebInspectScanFile(droppedFile);
        }

        private void lbLoadedFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        public void loadWebInspectScanFile(string fileToLoad)
        {
            webInspectResults.loadWebInspectScanFiles(fileToLoad);
            showLoadedFindings();
        }

        public void showLoadedFindings()
        {
            WebInspectWindowsFormsUtils.showWebInspectResultsInTableList(webInspectResults,
                                                                         tableListWithWebInspectFindings);
            lbLoadedFiles.Items.Clear();
            lbLoadedFiles.Items.AddRange(webInspectResults.processedWebInspectScanFiles.ToArray());
        }

        private void llClearResults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            webInspectResults = new WebInspectResults();
            showLoadedFindings();
        }

        private void linkLabel1_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(webInspectResults, DragDropEffects.Copy);
        }
    }
}