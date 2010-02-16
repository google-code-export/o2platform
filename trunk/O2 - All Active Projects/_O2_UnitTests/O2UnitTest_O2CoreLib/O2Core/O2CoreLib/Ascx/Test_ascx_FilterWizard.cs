// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using NUnit.Framework;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;
using O2.UnitTests.Test_O2Debugger.MockObjects;
using O2.Views.ASCX.classes;
using O2.Views.ASCX.classes.TasksWrappers;


namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Ascx
{
    [TestFixture]
    public class Test_ascx_FilterWizard
    {
        [Test]
        public void test_OzasmtFileUsingTask()
        {
            string ozasmtFile =
                MockObjects_Helpers.fetchTestFileFromDeployServer_AndUnzipIt(
                    O2CoreResources.DemoOzasmtFile_Hacmebank_WebServices);
            Assert.That(File.Exists(ozasmtFile), "demoFileContents does not exist");
            var task = new Task_LoadAssessmentFiles(new O2AssessmentLoad_OunceV6(), ozasmtFile);
            Assert.That(task.execute(), "Task execution failed");
            Assert.That(task.resultsObject is IO2Assessment, "resultsObject was not O2Assessment");
            var resultsObject = (IO2Assessment) task.resultsObject;
            Assert.That(resultsObject.o2Findings.Count > 0, "No Findings");
            Assert.That(resultsObject.name != null, "results.name was null");
       /*     Assert.That(resultsObject.lastOzasmtImportFile != null, "results.name was null");
            Assert.That(resultsObject.lastOzasmtImportTimeSpan.Milliseconds > 0,
                        "lastOzasmtImportTimeSpan.Milliseconds = 0");
            Assert.That(resultsObject.lastOzasmtImportFileSize > 0, "lastOzasmtImportFileSize = 0");*/
        }
    }
}
