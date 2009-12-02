using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;


namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Windows
{
    [TestFixture]
    public class Test_FileWatcher
    {
        private readonly AutoResetEvent folderChangeDetected = new AutoResetEvent(false);        
        
        [Test]
        public void test_FolderWatcher()
        {            
            string folderToTest = Path.Combine(DI.config.O2TempDir, "FileWatcherDir");
            DI.log.info(folderToTest);
            string testFile = Path.Combine(folderToTest, "testFile.Txt");
            Files.checkIfDirectoryExistsAndCreateIfNot(folderToTest);
            var folderWatcher = new FolderWatcher(folderToTest, delegate { folderChangeDetected.Set(); });

            Files.WriteFileContent(testFile, "test Content");
            folderChangeDetected.WaitOne();            
            List<string> filesInAffectedFolder = Files.getFilesFromDir(folderToTest);
            Assert.IsTrue(folderWatcher.file != "",
                          "(on folderWatcher obj)No Changes were detected on target folder: " + folderToTest);
            Assert.IsTrue(filesInAffectedFolder.Count > 0,
                          "(on folderWatcher obj) There were no files on target folder");                        
        }
    }
}