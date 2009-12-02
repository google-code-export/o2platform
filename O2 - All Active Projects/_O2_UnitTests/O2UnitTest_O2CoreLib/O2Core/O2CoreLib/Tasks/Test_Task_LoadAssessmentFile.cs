using NUnit.Framework;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.UnitTests.Test_O2Debugger.MockObjects;
using O2.Views.ASCX.classes.TasksWrappers;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Tasks
{
    [TestFixture]
    public class Test_Task_LoadAssessmentFile
    {
        [Test]
        public void Test_UsingLocalllyCreatedOzamstFile()
        {
            string sourceFile = MockObjects_Ozasmt.getOzasmtXmlFile();
            var task = new Task_LoadAssessmentFiles(new O2AssessmentLoad_OunceV6(),sourceFile);
            Assert.That(task.execute(), "Task execution failed");
        }
    }
}