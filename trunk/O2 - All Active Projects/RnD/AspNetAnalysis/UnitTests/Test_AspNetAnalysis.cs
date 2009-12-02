// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using NUnit.Framework;
using o2.CirAnalysis.DotNet;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;

namespace o2.aspnetanalysis.UnitTests
{
    [TestFixture]
    class Test_AspNetAnalysis
    {        
        private const string cirDataFile_BigOne =
            @"C:\O2\_UnitTestSupportFiles\complete HacmeBank_v2_Website (with extra ControlFlowGraph info for methods).sln.CirData";   

        private const string clickButtonMappingOzasmt = @"C:\O2\_UnitTestSupportFiles\ClickButtonMappingsFromCirData.ozasmt";
        private const string bothLayersOzasmt = @"C:\O2\_UnitTestSupportFiles\HacmeBank ALL Traces (both layer).ozasmt";

        //private const string webLayerOzasmt = @"C:\O2\_UnitTestSupportFiles\Website (with all callers) ._ALLTRACES.ozasmt";
        //private const string webServicesLayerOzasmt = @"C:\O2\_UnitTestSupportFiles\WS (Only Process Traces With no Callers) ._ALLTRACES.ozasmt";

        private const string resultsFilefor_clickButtonSource_SystemDataSink = @"C:\O2\_UnitTestSupportFiles\ClickButtonTraces_withSink_SystemData.ozasmt";
        private const string resultsFilefor_clickButtonSource_SystemDataSink_withTexBoxMapping = @"C:\O2\_UnitTestSupportFiles\ClickButtonTraces_withSink_SystemData_withTexBoxMapping.ozasmt";

        [Test]
        public void findParameterStaticValueInMethodX()
        {            
            var cirData = CirLoad.loadSerializedO2CirDataObject(cirDataFile_BigOne);
            var result = AspNetAnalysis.findParameterStaticValueInMethodX(cirData);
            var createdAssessment = new O2Assessment();
            createdAssessment.o2Findings = result;
            createdAssessment.save(new O2AssessmentSave_OunceV6(), clickButtonMappingOzasmt);
            Assert.IsNotNull(result, "Result was null");
        }

        [Test]
        public void createClickButtonTraces()
        {
            var o2Assessment = new O2Assessment
                                   {
                                       o2Findings = OzasmtGlue.glueTraceSinkWithSources(new O2AssessmentLoad_OunceV6() , clickButtonMappingOzasmt,
                                                                                        bothLayersOzasmt)
                                   };
            //o2Assessment.o2Findings = AspNetAnalysis.glueClickButtonTraces(clickButtonMappingOzasmt, webLayerOzasmt, webServicesLayerOzasmt);
            Assert.IsTrue(o2Assessment.o2Findings.Count > 0, "no findings calculated");
            o2Assessment.o2Findings = OzasmtFilter.getFindingsWithSink(o2Assessment.o2Findings, "System.Data");
            Assert.IsTrue(o2Assessment.o2Findings.Count > 0, "no System.Data Sinks found");
            o2Assessment.save(new O2AssessmentSave_OunceV6(), resultsFilefor_clickButtonSource_SystemDataSink);
            Assert.IsTrue(File.Exists(resultsFilefor_clickButtonSource_SystemDataSink), "resultsFilefor_clickButtonSource_SystemDataSink doesn't exist");
        }

        [Test]
        public void mapTextBoxWebControlsAsSinks()
        {
            Assert.IsTrue(File.Exists(resultsFilefor_clickButtonSource_SystemDataSink), "resultsFilefor_clickButtonSource_SystemDataSink doesn't exist");
            var findingsToProcess = new O2Assessment(new O2AssessmentLoad_OunceV6(), resultsFilefor_clickButtonSource_SystemDataSink).o2Findings;
            var results = AspNetAnalysis.mapTextBoxWebControlsAsSinks(findingsToProcess);
            Assert.IsTrue(results.Count > 0, "no findings calculated");
            var assessmentWithResults = new O2Assessment { o2Findings = results };
            assessmentWithResults.save(new O2AssessmentSave_OunceV6(), resultsFilefor_clickButtonSource_SystemDataSink_withTexBoxMapping);
        }
    }
}
