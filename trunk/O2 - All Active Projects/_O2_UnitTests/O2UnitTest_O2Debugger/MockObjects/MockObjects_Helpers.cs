// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
//using O2.Legacy.CoreLib.WebAutomation;
using O2.Views.ASCX.classes;
using O2.DotNetWrappers.Network;

namespace O2.UnitTests.Test_O2Debugger.MockObjects
{
    public static class MockObjects_Helpers
    {
        public static string fetchTestFileFromDeployServer_AndUnzipIt(string url)
        {
            List<string> unzipedFiles = Web.downloadZipFileAndExtractFiles(url);
            Assert.That(unzipedFiles != null, "no Unzip files returned");
            Assert.That(unzipedFiles.Count == 1, "There should only be only file inside the zip file");
            Assert.That(File.Exists(unzipedFiles[0]), "unzipedFiles[0] doesn't exist!! " + unzipedFiles[0]);
            return unzipedFiles[0];
        }

        public static string fetchTestFileFromDeployServer(string url)
        {
            string downloadedFile = Web.downloadBinaryFile(url);
            Assert.That(downloadedFile != null, "no Unzip files returned");
            Assert.That(File.Exists(downloadedFile), "downloadedFile doesn't exist" + downloadedFile);
            return downloadedFile;
        }
    }
}
