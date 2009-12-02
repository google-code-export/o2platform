// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.DotNetWrappers.Windows;
using O2.Rnd.Tool.ScanQueue;
using O2.Rnd.Tool.ScanQueue.Utils;

namespace O2.Rnd.Tool.ScanQueue.Queues
{
    public class DropQueue
    {
        public static void processDropQueueFolder()
        {
            DI.log.info("Processing DropQueue folder : {0}", FoldersWatched.folderWatcher_DropQueue.folderWatched);
            foreach (var file in Files.getFilesFromDir_returnFullPath(FoldersWatched.folderWatcher_DropQueue.folderWatched))
                ScanQueue.addFileToScanQueue(file);
        }
    }
}
