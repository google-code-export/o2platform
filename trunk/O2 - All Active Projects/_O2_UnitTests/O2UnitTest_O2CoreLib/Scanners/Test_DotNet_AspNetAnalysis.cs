using System.IO;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.O2Findings.DotNet;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;

namespace O2.UnitTests.Test_O2CoreLib.Scanners
{
    [TestFixture]
    public class Test_DotNet_AspNetAnalysis
    {
        private const string dllToUseInTests =
            @"E:\O2\_UnitTestSupportFiles\App_Web_ilvrfsd3_From_HacmeBank_Website.dll";

        private const string ozasmtHacmeBankScanWithDefaultRules =
            @"E:\O2\_UnitTestSupportFiles\HacmeBank_v2_Website_OsaScan_CurrentRules.ozasmt";

        private const string ozasmtWithHacmeBankWebControlMappings =
            @"E:\O2\_UnitTestSupportFiles\ozasmtWithHacmeBankWebControlMappings.ozasmt";

        [Test]
        public void findWebControlSources()
        {
            Assert.IsTrue(File.Exists(ozasmtHacmeBankScanWithDefaultRules),
                          "ozasmtHacmeBankScanWithDefaultRules could not be found");

            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtHacmeBankScanWithDefaultRules);
            o2Assessment.o2Findings = AspNetAnalysis.findWebControlSources(o2Assessment.o2Findings);
            Assert.IsTrue(o2Assessment.o2Findings.Count > 0, "There were no Findings calculated");
            o2Assessment.save(new O2AssessmentSave_OunceV6(),ozasmtWithHacmeBankWebControlMappings);
        }
    }
}