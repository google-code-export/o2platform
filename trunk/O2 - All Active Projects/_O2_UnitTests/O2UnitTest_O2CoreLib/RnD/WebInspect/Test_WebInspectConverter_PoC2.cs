using System.IO;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.O2Findings.DotNet;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Tool.WebInspectConverter.Converter;

namespace O2.UnitTests.Test_O2CoreLib.RnD.WebInspect
{
    [TestFixture]
    public class Test_WebInspectConverter_PoC2
    {
        private const string webInspectFileWithResults =
            @"E:\O2\_UnitTestSupportFiles\WebInspect.HacmeBank_ScanWithSqlInjections.scan.cf33627d-39cc-42a4-bdf4-410bd5f8a525.xml";


        private const string ozasmtHacmeBankScanWithDefaultRules =
            @"E:\O2\_UnitTestSupportFiles\HacmeBank_v2_Website_OsaScan_CurrentRules.ozasmt";

        private const string ozasmtWithHacmeBankWebControlMappings =
            @"E:\O2\_UnitTestSupportFiles\1.ozasmtWithHacmeBankWebControlMappings.ozasmt";

        private const string ozasmtFileWebInspectMappings = @"E:\O2\_UnitTestSupportFiles\2.WebInspectMappings.ozasmt";

        private const string ozasmtWithWebInspectToOunceMappings =
            @"E:\O2\_UnitTestSupportFiles\3.ozasmtWithWebInspectToOunceMappings.ozasmt";

        private const string ozasmtWithWebInspectToOunceMappings_UniqueTraces =
            @"E:\O2\_UnitTestSupportFiles\4.ozasmtWithWebInspectToOunceMappings_UniqueTraces.ozasmt";

        [Test]
        public void mapWebInspectMappingsToOzamstFindings()
        {
            // process Ounce Assessment file
            string workOzasmtFile = ozasmtHacmeBankScanWithDefaultRules;
            Assert.IsTrue(File.Exists(workOzasmtFile), "ozasmtHacmeBankScanWithDefaultRules could not be found");
            var o2AssessmentOunceScan = new O2Assessment(new O2AssessmentLoad_OunceV6(), workOzasmtFile);
            o2AssessmentOunceScan.o2Findings = AspNetAnalysis.findWebControlSources(o2AssessmentOunceScan.o2Findings);
            Assert.IsTrue(o2AssessmentOunceScan.o2Findings.Count > 0, "There were no Findings calculated");
            o2AssessmentOunceScan.save(new O2AssessmentSave_OunceV6(),ozasmtWithHacmeBankWebControlMappings);


            // process WebInspect file

            string workWebInspectFile = webInspectFileWithResults;
            Assert.IsTrue(File.Exists(workWebInspectFile), "webInspectFileWithResults does not exist");
            var o2AssessmentWebInspectScan = new O2Assessment()
                                                 {
                                                     o2Findings =
                                                         WebInspectConverter.
                                                         loadWebInspectResultsAndReturnO2FindingsFor_SqlInjection_PoC2(
                                                         workWebInspectFile)
                                                 };
            Assert.IsTrue(o2AssessmentWebInspectScan.o2Findings.Count > 0, "No O2 findings created");
            o2AssessmentWebInspectScan.save(new O2AssessmentSave_OunceV6(),ozasmtFileWebInspectMappings);

            var o2AssessmentGluedOnTraceName = new O2Assessment()
                                                   {
                                                       o2Findings =
                                                           OzasmtGlue.glueOnTraceNames(new O2AssessmentLoad_OunceV6(), ozasmtFileWebInspectMappings,
                                                                                       ozasmtWithHacmeBankWebControlMappings,
                                                                                       "Spring MVC Glue")
                                                   };
            Assert.IsTrue(o2AssessmentGluedOnTraceName.o2Findings.Count > 0, "No Glued Findings created");
            o2AssessmentGluedOnTraceName.save(new O2AssessmentSave_OunceV6(),ozasmtWithWebInspectToOunceMappings);
            Analysis.createAssessmentFileWithAllTraces(true, false, ozasmtWithWebInspectToOunceMappings,
                                                       //  ozasmtWithWebInspectToOunceMappings);

                                                       ozasmtWithWebInspectToOunceMappings_UniqueTraces);
            //
        }
    }
}