using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
//using O2.Legacy.CoreLib.WebAutomation;
using O2.Views.ASCX.classes;

namespace O2.UnitTests.Test_O2Debugger.MockObjects
{
    public static class MockObjects_Helpers
    {
        public static string fetchTestFileFromDeployServer_AndUnzipIt(string url)
        {
            List<string> unzipedFiles = WebRequests.downloadZipFileAndExtractFiles(url);
            Assert.That(unzipedFiles != null, "no Unzip files returned");
            Assert.That(unzipedFiles.Count == 1, "There should only be only file inside the zip file");
            Assert.That(File.Exists(unzipedFiles[0]), "unzipedFiles[0] doesn't exist!! " + unzipedFiles[0]);
            return unzipedFiles[0];
        }

        public static string fetchTestFileFromDeployServer(string url)
        {
            string downloadedFile = WebRequests.downloadBinaryFile(url);
            Assert.That(downloadedFile != null, "no Unzip files returned");
            Assert.That(File.Exists(downloadedFile), "downloadedFile doesn't exist" + downloadedFile);
            return downloadedFile;
        }
    }
}