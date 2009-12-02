// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Reflection;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Rnd.Tool.ScanQueue.Queues;
using O2.Rnd.Tool.ScanQueue.Utils;
using O2.Views.ASCX.CoreControls;

namespace O2.Rnd.Tool.ScanQueue.Ascx
{
    public partial class ascx_ScanQueue : UserControl
    {
        public bool runTimeConfig;

        public ascx_ScanQueue()
        {
            InitializeComponent();
        }

        public string folderFor_DropQueue { get; set; }
        public string folderFor_ScanQueue { get; set; }
        public string folderFor_ScanResults { get; set; }

        private void ascx_ScanQueue_Load(object sender, EventArgs e)
        {
            if (DesignMode == false && runTimeConfig == false)
            {
                FoldersWatched.setupFolderWatchers(folderFor_DropQueue, folderFor_ScanQueue, folderFor_ScanResults);

                setupDirectoryControl(directoryDropQueue, FoldersWatched.folderWatcher_DropQueue.ToString());
                setupDirectoryControl(directoryScanQueue, FoldersWatched.folderWatcher_ScanQueue.ToString());
                setupDirectoryControl(directoryScanResults, FoldersWatched.folderWatcher_ScanResults.ToString());

                runTimeConfig = true;
            }
        }

        private void Test_Click(object sender, EventArgs e)
        {
            Files.Copy(Assembly.GetExecutingAssembly().Location, FoldersWatched.folderWatcher_DropQueue.folderWatched);
        }

        public void setupDirectoryControl(ascx_Directory directory, string folderToShow)
        {
            directory.simpleMode();
            directory._WatchFolder = true;
            directory.setMoveOnDrag(true);
            directory.openDirectory(folderToShow);
        }

        private void btProcessScanQueue_Click(object sender, EventArgs e)
        {
            Queues.ScanQueue.processScanQueueFolder();
        }

        private void btProcessDropQueue_Click(object sender, EventArgs e)
        {
            DropQueue.processDropQueueFolder();
        }
    }
}
