// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using NUnit.Framework;

namespace O2.webinspect.UnitTests
{
    // NOte: Need to reimplement this test
    //[TestFixture]
    /*public class test_WebInspectConverter_PoC1
    {
        // HacmeBank Trace with 
        private const string ozasmtFileWithOunceMappings =
            @"E:\O2\_UnitTestSupportFiles\ClickButtonTraces_withSink_SystemData_withTexBoxMapping.ozasmt";

        private const string ozasmtFileWebInspectMappings = @"E:\O2\_UnitTestSupportFiles\WebInspectMappings.ozasmt";

        private const string ozasmtFileWebInpectMappings_forHacmeBank =
            @"E:\O2\_UnitTestSupportFiles\WebInspectMappings_forHacmeBank.ozasmt";

        private const string ozasmtFileWithMappingBetweenOunceAndWebInspect =
            @"E:\O2\_UnitTestSupportFiles\ozasmtFileWithMappingBetweenOunceAndWebInspect.ozasmt";


        private const string webInspectFileWithResults =
            @"E:\O2\_UnitTestSupportFiles\WebInspect.HacmeBank_ScanWithSqlInjections.scan.cf33627d-39cc-42a4-bdf4-410bd5f8a525.xml";

        [Test]
        public void mapWebInspectMappingsToOzamstFindings()
        {
            //Assert.IsTrue(File.Exists(ozasmtFileWithOunceFindings), "ozasmtFileWithOunceFindings does not exist");

            //var o2Assessment = new O2Assessment(ozasmtFileWithOunceFindings);
            Assert.IsTrue(File.Exists(webInspectFileWithResults), "webInspectFileWithResults does not exist");

            var results =
                WebInspectConverter.loadWebInspectResultsAndReturnO2FindingsFor_SqlInjection_PoC1(
                    webInspectFileWithResults);
            Assert.IsTrue(results.Count > 0, "No O2 findings created");

            var o2Asssesment = new O2Assessment {o2Findings = results};
            o2Asssesment.save(ozasmtFileWebInspectMappings);

            HacmeBankRules.mapFunctionInUrlToAscx(o2Asssesment.o2Findings);
            o2Asssesment.save(ozasmtFileWebInpectMappings_forHacmeBank);

            var gluedResults = OzasmtGlue.glueTraceSinkWithSources(ozasmtFileWebInpectMappings_forHacmeBank,
                                                                   ozasmtFileWithOunceMappings);
            Assert.IsTrue(gluedResults.Count > 0, "No Glued findings created");

            o2Asssesment = new O2Assessment {o2Findings = gluedResults};
            o2Asssesment.save(ozasmtFileWithMappingBetweenOunceAndWebInspect);

            //var aspNetPagesInFindings = AspNetAnalysis.getAspNetPageFromO2Findings(o2Assessment.o2Findings);
            //Assert.IsTrue(aspNetPagesInFindings.Count > 0 ,"No Asp.Net pages discovered in ozasmt file");
        }
    }*/
}
