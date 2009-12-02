// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using NUnit.Framework;
using O2.Tool.WebInspectConverter.Converter;

namespace O2.UnitTests.Test_O2CoreLib.RnD.WebInspect
{
    [TestFixture]
    public class Test_WebInspectResults
    {
        private const string webInspectFileWithResults =
            @"E:\O2\_UnitTestSupportFiles\http.localHacmeBank_v2_Websiteaspxlogin.aspx 01-31-2009 22.30.8.scan";

        [Test]
        public void webInspectResults()
        {
            var results = new WebInspectResults();
            results.loadWebInspectScanFiles(webInspectFileWithResults);
            Assert.IsTrue(results.processedWebInspectScanFiles.Count > 0, "No Files Processed");
            Assert.IsTrue(results.webInspectFindings.Count > 0, "No Findings in loaded files");
        }
    }
}
