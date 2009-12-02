using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Rnd.Tool.ScanQueue;
using O2.Rnd.Tool.ScanQueue.Queues;

namespace O2.Rnd.Tool.ScanQueue.Utils
{
    internal class FoldersWatched
    {
        public static FolderWatcher folderWatcher_DropQueue;
        public static FolderWatcher folderWatcher_ScanQueue;
        public static FolderWatcher folderWatcher_ScanResults;

        //var folderWatcher = new FolderWatcher(folderToTest, fileChangeCallback);
        public static void setupFolderWatchers(string folderFor_DropQueue, string folderFor_ScanQueue,
                                               string folderFor_ScanResults)
        {
            folderWatcher_DropQueue = new FolderWatcher(resolvePathToWatchedFolder(folderFor_DropQueue),
                                                        fileChangeIn_DropQueue);
            folderWatcher_ScanQueue = new FolderWatcher(resolvePathToWatchedFolder(folderFor_ScanQueue),
                                                        fileChangeIn_ScanQueue);
            folderWatcher_ScanResults = new FolderWatcher(resolvePathToWatchedFolder(folderFor_ScanResults),
                                                          fileChangeIn_ScanResults);
        }

        public static void fileChangeIn_DropQueue(FolderWatcher folderWatcher)
        {
            DropQueue.processDropQueueFolder();
            // DI.log.info("fileChangeIn_DropQueue that changed: {0}", folderWatcher.file);
        }

        public static void fileChangeIn_ScanQueue(FolderWatcher folderWatcher)
        {
            Queues.ScanQueue.processScanQueueFolder();
        }

        public static void fileChangeIn_ScanResults(FolderWatcher folderWatcher)
        {
            DI.log.info("fileChangeIn_ScanResults that changed: {0}", folderWatcher.file);
        }

        /* folderWatcher_DropQueue = setupFolderWatch(DI.config.O2TempDir, folderFor_DropQueue,
                                          CallbacksForFolderChanges.fileChangeIn_DropQueue);
             folderWatcher_ScanQueue = setupFolderWatch(DI.config.O2TempDir, folderFor_ScanQueue,
                                          CallbacksForFolderChanges.fileChangeIn_ScanQueue);
             folderWatcher_ScanResults = setupFolderWatch(DI.config.O2TempDir, folderFor_ScanResults,
                                          CallbacksForFolderChanges.fileChangeIn_ScanResults);*
         }

         public static FolderWatcher setupFolderWatch(string rootPath, string folder, Callbacks.dMethod_String fileChangeCallback)
         {
             return setupFolderWatch(Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(rootPath, folder)), fileChangeCallback);
         }

         public static FolderWatcher setupFolderWatch(string folderToWatch, Callbacks.dMethod_String fileChangeCallback)
         {
             //  Files.checkIfDirectoryExistsAndCreateIfNot(folderToWatch);
             FolderWatcher.startFolderWatcherOnFolder(folderToWatch);
             FolderWatcher.onFolderChange_FileThatChanged += fileChangeCallback;
             return folderToWatch;
         }*/
        //}

        public static string resolvePathToWatchedFolder(string folder)
        {
            return Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(DI.config.O2TempDir, folder));
        }
    }
}