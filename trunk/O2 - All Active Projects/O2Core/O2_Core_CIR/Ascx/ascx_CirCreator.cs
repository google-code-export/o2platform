// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_CirCreator : UserControl
    {
        public ascx_CirCreator()
        {
            InitializeComponent();
        }

        private void ascx_CirCreator_Load(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                setUpCirCreationDirectories();
            }
        }

        public void llCreateCirForCurrentO2Module_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            copyFileToCirCreationQueue(DI.config.ExecutingAssembly);
        }        

        public void btCreateCirForSelectedFile_Click(object sender, EventArgs e)
        {            
            createCirDataForFile(directory_CirCreationQueue.getSelectedItem_FullPath(),false, false /*decompileCodeIfNoPdb*/);
        }

        public void directory_CirCreationQueue__onDirectoryClick(string sString)
        {
            btCreateCirForSelectedFile.Enabled = File.Exists(directory_CirCreationQueue.getSelectedItem_FullPath());
            btProcessQueue.Enabled = directory_CirCreationQueue.getFiles().Count > 0;
        }

        public void llDeleteFilesInCreatedCirFilesDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            deleteFilesFromCreatedCirFileDirectory();
        }

        public void llDeleteFilesInCirCreationQueue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            deleteFilesFromCirCreationQueueDirectory();
        }

        public void btProcessQueue_Click(object sender, EventArgs e)
        {
            processCirCreationQueue();
        }

        private void directory_CirCreationQueue__onTreeViewDrop(string sString)
        {
            processCirCreationQueue();
        }

        private void directory_CirCreationQueue__onFileWatchEvent(O2.DotNetWrappers.Windows.FolderWatcher folderWatcher)
        {
            if (folderWatcher.fileDeleted == "")
                processCirCreationQueue();
        }

        
        
        
    }
}
