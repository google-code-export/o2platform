// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.UnitTests.Test_Ozasmt.Test_Ozasmt
{
    [TestFixture]
    public class Test_O2Assessment
    {
        public IO2AssessmentLoad o2AssessmentLoad = new O2AssessmentLoad_OunceV6();
        public IO2AssessmentSave o2AssessmentSave = new O2AssessmentSave_OunceV6();

        #region Setup/Teardown

        [SetUp]
        public void init()
        {
            try
            {
                String sTargetFolder = DI.config.O2TempDir;
                Assert.IsTrue(Directory.Exists(sTargetFolder), "On Init, Target Folder doesn't exist");
                sFileToCreate = Path.Combine(sTargetFolder, sCustomAssessmentFileName);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #endregion

        private const string sCustomAssessmentFileName = "TestCustomAssessmentFile.ozasmt";
        private String sFileToCreate;


        [Test]
        public void CreateCustomAssessmentFile() // Test to see if we can sucessfully create custom findings
        {
            const string name = "Test Name";

            var o2Assessment = new O2Assessment {name = name};
            Assert.IsTrue(o2Assessment.save(o2AssessmentSave, sFileToCreate), "SaveAssessmentRun failed");

            var o2AssessmentLoaded = new O2Assessment(o2AssessmentLoad,sFileToCreate);
            Assert.IsTrue(name == o2AssessmentLoaded.name, "Name matches");
        }
    }
}
