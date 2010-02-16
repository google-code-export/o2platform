// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using NUnit.Framework;
using O2.Interfaces.O2Findings;
using O2.Tool.WebInspectConverter.Converter;

namespace O2.UnitTests.Test_O2CoreLib.RnD.WebInspect
{
    [TestFixture]
    public class Test_WebInspectToOzasmt
    {
        private const string webInspectFileWithResults =
            @"E:\O2\_UnitTestSupportFiles\http.localHacmeBank_v2_Websiteaspxlogin.aspx 01-31-2009 22.30.8.scan";

        [Test]
        public void webInspectToOzasmt()
        {
            var webInspectResults = new WebInspectResults();
            webInspectResults.loadWebInspectScanFiles(webInspectFileWithResults);
            Assert.IsTrue(webInspectResults.processedWebInspectScanFiles.Count > 0, "No Files Processed");
            Assert.IsTrue(webInspectResults.webInspectFindings.Count > 0, "No WebInspectFindings in webInspectResults");
            List<IO2Finding> o2Findings = WebInspectToOzasmt.createO2FindingsFromWebInspectResults(webInspectResults);
            Assert.IsTrue(o2Findings.Count > 0, "There were no findings created from webInspectResults");
        }
    }
}
