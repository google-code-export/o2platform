using System;
using System.IO;
using System.Windows.Forms;
using O2.Legacy.CoreLib;

//using O2.core.Tasks;

namespace O2.Legacy.CoreLib.Ascx.Utils
{
    public partial class ascx_Unzip : UserControl
    {
        public ascx_Unzip()
        {
            InitializeComponent();
         //   directoryWithUnzipedFiles._onTreeViewDrop += unzipFile;
            directoryWithUnzipedFiles._ProcessDroppedObjects = false;
        }

        private void ascx_Unzip_Load(object sender, EventArgs e)
        {
        }

        private void directoryWithUnzipedFiles_Load(object sender, EventArgs e)
        {
            directoryWithUnzipedFiles.openDirectory(DI.o2CorLibConfig.TempFolderInTempDirectory);
        }

     /*   public void unzipFile(string fileToUnzip)
        {
            if (File.Exists(fileToUnzip))
            {
                string targetDirectory = Path.Combine(directoryWithUnzipedFiles.getCurrentDirectory(),
                                                      Path.GetFileNameWithoutExtension(fileToUnzip));
                tasksHostControl.Controls.Clear();
                TaskUtils.executeTask(new Task_Unzip(fileToUnzip, targetDirectory), tasksHostControl,
                                      (resultObject) => directoryWithUnzipedFiles.refreshDirectoryView());
            }
        }*/
    }
}