// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Controllers;
using O2.Rnd.Tool.ScanQueue;
using O2.Rnd.Tool.ScanQueue.Utils;
using O2.Scanner.OunceLabsCLI.ScanTargets;

namespace O2.Rnd.Tool.ScanQueue.Queues
{
    public class ScanQueue
    {
        public static ScanThread currentScanThread = new ScanThread();

        public static void addFileToScanQueue(string fileToScan)
        {
            DI.log.debug("addFileToScanQueue : {0}", fileToScan);
            List<IScanTarget> scanTargets = CreateScanTarget.createScanTargetsFromFileOrFolder(fileToScan);
            foreach (IScanTarget scanTarget in scanTargets)
            {
                DI.log.debug("scanTarget : {0}", scanTarget);
                var serializedScanTargetXml = Serialize.createSerializedXmlStringFromObject(scanTarget);
                string serializedScanTargetFilename = Path.Combine(FoldersWatched.folderWatcher_ScanQueue.folderWatched,
                                                                   Path.GetFileNameWithoutExtension(
                                                                       scanTarget.Target));
                serializedScanTargetFilename += ".xml";
                Files.WriteFileContent(serializedScanTargetFilename, serializedScanTargetXml);
            }
        }

        public static void callbackOnScanCompletion()
        {
            //o2.scanners.
        }

        internal static void processScanQueueFolder()
        {
            DI.log.info("Processing ScanQueue folder : {0}", FoldersWatched.folderWatcher_ScanQueue.folderWatched);
            foreach (var file in  Files.getFilesFromDir_returnFullPath(FoldersWatched.folderWatcher_ScanQueue.folderWatched))
            {
                if (false == currentScanThread.active)
                {
                }
                DI.log.info("  Scan Queue item :{0}:, file");
            }
            //ScanQueue.addFileToScanQueue(file);
        }
    }
}
