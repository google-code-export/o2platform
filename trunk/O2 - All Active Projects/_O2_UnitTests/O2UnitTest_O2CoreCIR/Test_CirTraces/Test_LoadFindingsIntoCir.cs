// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.CIR;

namespace O2.UnitTests.Test_O2CoreCIR.Test_CirTraces
{
    [TestFixture]
    public class Test_LoadFindingsIntoCir
    {
        private const string findingsFile = @"E:\O2\Demodata\JoinTracesData\O2_Kernel\O2_Kernel.exe.paf_CallBacksOnEdges_And_ExternalSinks.ozasmt";
        private const string fileToCreateCirFrom = @"E:\O2\Demodata\JoinTracesData\O2_Kernel\O2_Kernel.exe";

        [Test, Ignore("This is NOT WORKING! (namely the signature mappings between Cir and Ounce (see info messages))")]
        public void test_PopulateCirWithTraces()
        {
            // get Assessment Data
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), findingsFile);
            Assert.That(o2Assessment.o2Findings.Count > 0, "There were no findings in o2Assessment");
            DI.log.info("There are {0} findings in the assessment loaded", o2Assessment.o2Findings.Count);
            var uniqueListOfSignatures = OzasmtUtils.getUniqueListOfSignatures(o2Assessment.o2Findings);
            Assert.That(uniqueListOfSignatures.Count() > 0, "uniqueListOfSignatures.Count ==0 ");
            DI.log.info("There are {0} unique signatures ", uniqueListOfSignatures.Count());
            
            // get cir data
            var cirDataAnalysis = new CirDataAnalysis();
            CirDataAnalysisUtils.loadFileIntoCirDataAnalysisObject(fileToCreateCirFrom,cirDataAnalysis);
            CirDataAnalysisUtils.remapIsCalledByXrefs(cirDataAnalysis);

            Assert.That(cirDataAnalysis.dCirFunction_bySignature.Count > 0, "cirDataAnalysis.dCirFunction_bySignature.Count == 0");

            // need to convert to Ozasmt signature format
            var cirMappedFunctions = new Dictionary<string, ICirFunction>();
            foreach (var cirFunction in cirDataAnalysis.dCirFunction_bySignature.Values)
            {              
                if (cirFunction.FunctionSignature.IndexOf("O2AppDomainFactory>") > -1)
                {
                }
                var filteredSignature = new FilteredSignature(cirFunction);
                cirMappedFunctions.Add(filteredSignature.sSignature, cirFunction);
            }
            var matches = new List<String>();
            foreach (var sig in cirMappedFunctions.Keys)
                if (sig.IndexOf("IndexOf") > -1)
                    matches.Add(sig);

            //var matches = new List<String>();
       /*     foreach (var cirFunction in cirMappedFunctions.Values)
                foreach (var called in cirFunction.FunctionsCalledUniqueList)
                    if (called.FunctionSignature.IndexOf("System.Object::.ctor") > -1)
                    {
                        matches.Add(called.FunctionSignature);
                        var asd = cirDataAnalysis.dCirFunction_bySignature.ContainsKey(called.FunctionSignature);
                    }*/


            foreach (var signature in uniqueListOfSignatures)            
                if (false == cirMappedFunctions.ContainsKey(signature))
                    DI.log.info("NO MATCH:" + signature);
                
            foreach(O2Finding o2Finding in o2Assessment.o2Findings)
            {
                var source = o2Finding.Sink;
                if (source != "" && false == cirMappedFunctions.ContainsKey(source))
                    DI.log.info("NO MATCH for Source:" + source);

                var sink = o2Finding.Sink;
                if (sink != "" && false == cirMappedFunctions.ContainsKey(sink))
                    DI.log.info("NO MATCH for Sink:" + sink);
            }

            /*foreach (var signature in uniqueListOfSignatures)
                DI.log.info(signature);
            return;*/
            
            
        }
    }
}
