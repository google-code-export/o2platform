// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.External.O2Mono.MonoCecil;
using O2.Interfaces.CIR;

namespace O2.UnitTests.Test_O2CoreCIR.Test_Objects
{
    [TestFixture]
    public class Test_CirDataAnalysis
    {
        private CirDataAnalysis cirDataAnalysis;

        [SetUp]
        public void createCirDataAnalysisObject()
        {
            var cirFactory = new CirFactory();
            cirDataAnalysis = cirFactory.createCirDataAnalysisObject(new List<String>
                                                           {
                                                               Assembly.GetExecutingAssembly().Location,
                                                               DI.config.ExecutingAssembly
                                                           });

            Assert.That(cirDataAnalysis != null, "cirDataAnalysis was null");
        }

        [Test]
        public void test_MultipleCirClassAndFunctionLists()
        {            
            // *** test classes
            var numberOfCirClasses = cirDataAnalysis.dCirClass_bySignature.Count;
            Assert.That(numberOfCirClasses > 0, "numberOfCirClasses is 0");
            DI.log.info("There are {0} classes in the loaded CirDataAnalysis file", numberOfCirClasses);
            // CirClasses<ICirClass>();          
            var cirDataListWithICirClass = cirDataAnalysis.CirClasses<ICirClass>();
            Assert.That(cirDataListWithICirClass != null, "cirDataListWithICirClass was null");            
            Assert.That(cirDataListWithICirClass.Count == numberOfCirClasses,
                        "wrong value in cirDataListWithICirClass.Count: " + cirDataListWithICirClass.Count);
            //CirClasses<String>();
            var cirDataListWithString = cirDataAnalysis.CirClasses<String>();
            Assert.That(cirDataListWithString != null, "cirDataListWithString was null");            
            Assert.That(cirDataListWithString.Count == numberOfCirClasses,
                        "wrong value in cirDataListWithString.Count: " + cirDataListWithICirClass.Count);


            // *** test functions
            var numberOfCirFunctions = cirDataAnalysis.dCirFunction_bySignature.Count;
            Assert.That(numberOfCirClasses > 0, "numberOfCirClasses is 0");
            DI.log.info("There are {0} funtions in the loaded CirDataAnalysis file", numberOfCirFunctions);

            //CirFunctions<ICirFunction>();
            Assert.That(numberOfCirFunctions == cirDataAnalysis.CirFunctions_RawList<ICirFunction>().Count,
                        "Prob with CirFunctions<ICirFunction>()");

            //CirFunctions<String>();
            Assert.That(numberOfCirFunctions == cirDataAnalysis.CirFunctions_RawList<String>().Count,
                        "Prob with CirFunctions<String>() :" + cirDataAnalysis.CirFunctions<String>().Count);            
        }
    }
}
