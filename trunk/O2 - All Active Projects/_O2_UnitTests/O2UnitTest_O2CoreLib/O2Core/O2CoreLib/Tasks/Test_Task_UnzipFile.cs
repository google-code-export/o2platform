using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Tasks;
using O2.UnitTests.Test_O2Debugger.MockObjects;
using O2.Views.ASCX.classes;
using O2.Views.ASCX.classes.Tasks;
using O2.Views.ASCX.classes.TasksWrappers;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.Tasks
{
    [TestFixture]
    public class Test_Task_UnzipFile
    {
        // ReSharper disable ConditionIsAlwaysTrueOrFalse
        // ReSharper disable MemberCanBeMadeStatic
        private void checkIfExecutionIsAsync(ref bool taskCallbackWasInvoked)
        {
            Assert.That(taskCallbackWasInvoked == false,
                        "on Async execution taskCallbackWasInvoked should be false here");
            int maxSleeps = 20;

            while (false == taskCallbackWasInvoked && maxSleeps-- > 0)
                Thread.Sleep(250);
            Assert.That(taskCallbackWasInvoked, "taskCallbackWasInvoked == false");
        }

        [Test]
        public void Test_unzipFileUsingTask()
        {
            // TODO: need to fix this task DI model
            //DI.config.setDI("O2_Views_Controlers.dll", "DI", "taskControl", new ascx_Task());
            bool taskCallbackWasInvoked = false;

            string fileToFecthAndUnzip = O2CoreResources.DemoOzasmtFile_Hacmebank_WebServices;
            List<string> unzipedFiles = null;

            // tests async call
            TaskUtils.executeTask(new Task_Unzip(fileToFecthAndUnzip), resultsObject =>
                                                                           {
                                                                               unzipedFiles =
                                                                                   (List<string>) resultsObject;
                                                                               taskCallbackWasInvoked = true;
                                                                           });
            checkIfExecutionIsAsync(ref taskCallbackWasInvoked);
            Assert.That(unzipedFiles.Count == 1, "There should only be 1 unzipped file");
        }

        // ReSharper restore MemberCanBeMadeStatic
        // ReSharper restore ConditionIsAlwaysTrueOrFalse

        [Test]
        public void Test_unzipingFileUsingTaskAndDirectFetch()
        {
            bool taskCallbackWasInvoked = false;
            string fileToFecthAndUnzip = O2CoreResources.DemoOzasmtFile_Hacmebank_WebServices;
            string directlyFecthedFile =
                MockObjects_Helpers.fetchTestFileFromDeployServer_AndUnzipIt(fileToFecthAndUnzip);
            Assert.That(File.Exists(directlyFecthedFile), "directlyFecthedFile doesn't exist");

            var task = new Task_Unzip(fileToFecthAndUnzip);
            TaskUtils.executeTask(task,
                                  (_task, taskStatus) =>
                                      {
                                          if (taskStatus == TaskStatus.Completed_OK ||
                                              taskStatus == TaskStatus.Completed_Failed)
                                          {
                                              // Thread.Sleep(300); // if the zip file is local we will need to wait a little bit so that the assert below (which tests for async execution) has time to be tested
                                              taskCallbackWasInvoked = true;
                                          }
                                      });

            /*Assert.That(taskCallbackWasInvoked == false, "on Async execution taskCallbackWasInvoked should be false here");
            var maxSleeps = 5;
            while (false == taskCallbackWasInvoked && maxSleeps-- > 0)            
                Thread.Sleep(250);

            Assert.That(taskCallbackWasInvoked, "taskCallbackWasInvoked == false");
             */
            checkIfExecutionIsAsync(ref taskCallbackWasInvoked);

            // check that we received the unzipped file ok
            Assert.That(task.taskStatus == TaskStatus.Completed_OK, "task.getTaskStatus() != TaskStatus.Completed_OK");
            Assert.That(task.resultsObject != null, "results object == null");
            Assert.That(task.resultsObject is List<string>, "results object should be an List<string> object");
            Assert.That(((List<string>) task.resultsObject).Count == 1, "There should only be 1 unzipped file");
            string fileFetchedUsingTask = ((List<string>) task.resultsObject)[0];
            Assert.That(File.Exists(fileFetchedUsingTask),
                        "resultsObject should be a file that exists on this: " + fileFetchedUsingTask);

            // compare both files
            string fileContents_DirectlyFetched = Files.getFileContents(directlyFecthedFile);
            Assert.That(fileContents_DirectlyFetched.Length > 0, " fileContents_DirectlyFetched size was 0");
            string fileContents_TaskFetched = Files.getFileContents(fileFetchedUsingTask);
            Assert.That(fileContents_TaskFetched.Length > 0, " fileContents_DirectlyFetched size was 0");
            Assert.That(fileContents_DirectlyFetched == fileContents_TaskFetched,
                        "File contents of two fetched files don't match");
            //Assert.That((string)task.resultsObject == directlyFecthedFile, "task.resultsObject != directlyFecthedFile");
        }
    }
}