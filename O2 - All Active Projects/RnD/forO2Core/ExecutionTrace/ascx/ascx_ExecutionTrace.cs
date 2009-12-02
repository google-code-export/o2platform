using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;


namespace O2.Rnd.ExecutionTrace.ascx
{
    public partial class ascx_ExecutionTrace : UserControl
    {
        //public static String[] _asStackMethodsToFilter;
        public static List<String> _lsStackMethodsToFilter = new List<string>();
        public static String _sDirectoryToMonitor = "";

        public ascx_ExecutionTrace()
        {
            InitializeComponent();
        }

        public static String sDirectoryToMonitor
        {
            set { _sDirectoryToMonitor = value; }
        }


        public static List<String> lsStackMethodsToFilter
        {
            set { _lsStackMethodsToFilter = value; }
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            //o2.MonoCecil.utils.testCreateAssembly(tbDemoText.Text);
        }

        private void ascx_ExecutionTrace_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                tbDirectoryToMonitor.Text = _sDirectoryToMonitor;
                //    configDirectoryToMonitor(tbDirectoryToMonitor.Text);
                //    LoadFilesFromDirectory(tbDirectoryToMonitor.Text);
            }
        }

        public void configDirectoryToMonitor(String sTargetDirectory)
        {
            if (Directory.Exists(sTargetDirectory))
            {
                var fswWatcher = new FileSystemWatcher(sTargetDirectory);
                fswWatcher.Changed -= fswWatcher_Changed;
                fswWatcher.Changed += fswWatcher_Changed;
                fswWatcher.EnableRaisingEvents = true;
            }
        }

        private void fswWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (lbFilesInDirectory.InvokeRequired)
                lbFilesInDirectory.Invoke(new EventHandler(delegate { fswWatcher_Changed(sender, e); }));
            else
            {
                lbFilesInDirectory.Items.Add(e.Name);
                lbFilesInDirectory.SelectedIndex = lbFilesInDirectory.Items.Count - 1;
            }
            //O2Forms.executeMethodThreadSafe(lbFilesInDirectory,lbFilesInDirectory.Items, "Add", new object[] { e.Name });            
            //lbFilesInDirectory.Items.Add(e.Name);
            // throw new NotImplementedException();
        }

        private void lbFilesInDirectory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFilesInDirectory.SelectedItem != null)
                showFileContents(tbDirectoryToMonitor.Text, lbFilesInDirectory.SelectedItem.ToString());
        }

        public void showFileContents(String sDirectory, String sFile)
        {
            if (cbShowTrace.Checked)
            {
                String sFileToOpen = Path.Combine(sDirectory, sFile);

                if (cbFilterTraceMethods.Checked)
                    sRemoveTracesFromFile(sFileToOpen);

                wbFileContents.Navigate(sFileToOpen);
                tbFileContents.Text = Files.getFileContents(sFileToOpen);
            }
        }

        public void sRemoveTracesFromFile(String sFileToFix)
        {
            String sAspectDngCallMethodSignature = "_AspectDNG_";
            String[] asLines = Files.getFileContents(sFileToFix).Split(new[] {Environment.NewLine},
                                                                       StringSplitOptions.None);
            var sbFilteredResults = new StringBuilder();
            foreach (String sLine in asLines)
                if (false == _lsStackMethodsToFilter.Contains(sLine) &&
                    sLine.IndexOf(sAspectDngCallMethodSignature) == -1)
                    sbFilteredResults.AppendLine(sLine);
            Files.WriteFileContent(sFileToFix, sbFilteredResults.ToString());
        }

        public void LoadFilesFromDirectory(String sDirectoryToLoad)
        {
            lbFilesInDirectory.Items.Clear();
            O2Forms.loadListBoxWithFilesFromDir(lbFilesInDirectory, sDirectoryToLoad);
            if (lbFilesInDirectory.Items.Count > 0)
                lbFilesInDirectory.SelectedIndex = 0;
        }

        private void btDeleteLogFileFromDirectory_Click(object sender, EventArgs e)
        {
            Files.deleteFilesFromDirThatMatchPattern(tbDirectoryToMonitor.Text, "*.xml");
            LoadFilesFromDirectory(tbDirectoryToMonitor.Text);
        }

        private void tbDirectoryToMonitor_TextChanged(object sender, EventArgs e)
        {
            configDirectoryToMonitor(tbDirectoryToMonitor.Text);
            LoadFilesFromDirectory(tbDirectoryToMonitor.Text);
        }
    }
}