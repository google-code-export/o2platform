// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using System.Threading;
using NUnit.Framework;
using O2.Core.CIR.Ascx;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.Forms;
using O2.Interfaces.Views;
using O2.Kernel.CodeUtils;

namespace O2.UnitTests.Test_O2CoreCIR.Test_Cir
{
    [TestFixture]
    public class Test_CirCreatorAscxControl
    {       
        private const string cirAnalysisControlName = "Cir Creator";

        [SetUp]
        public void openGui()
        {            
            DI.log.info("Opening GUI");
            if (O2AscxGUI.launch())
            {
                O2AscxGUI.setLogViewerDockState(O2DockState.DockBottom);                                                
                O2Messages.openControlInGUISync(typeof(ascx_CirCreator), O2DockState.Document, cirAnalysisControlName);
                //DI.log.info("after launch");
                //O2AscxGUI.logInfo("from Test_CirCreatorAscxControl");  // this is actually a hack since there is still a bug in the thread invocation which makes sometimes the getGuiAscx below to fail
            }
        }

        [Test]
        public void test_CirCreatorWorkflow()
        {
            var guiControlReady = new AutoResetEvent(false);
            ascx_CirCreator cirCreator = null;
            O2Messages.getAscx(cirAnalysisControlName, ascxControl =>
                                                              {
                                                                  if (ascxControl is ascx_CirCreator)
                                                                  {
                                                                      cirCreator = (ascx_CirCreator)ascxControl;
                                                                      guiControlReady.Set();
                                                                  }
                                                                  else
                                                                  {
                                                                      DI.log.error("Could not get cirCreator control, aborting!");                                                                      
                                                                      return;
                                                                  }
                                                                  
                                                              });            
            guiControlReady.WaitOne(); //give it 2 seconds to setup
            Assert.That(cirCreator != null,"CirCreator control was null");

            // now that we have the control run the Automation tests
            O2AscxGUI.logInfo("Hello from UnitTests : " + cirCreator.GetType().FullName);
            // get directories controls
            var directory_CirCreationQueue = cirCreator.getDirectoryControlFor_CirCreationQueue();
            var directory_CreatedCirFiles = cirCreator.getDirectoryControlFor_CreatedCirFiles();
            // make sure there are no files in the queue
            Assert.That(directory_CirCreationQueue.getFiles().Count == 0,
                        "There should be no files in the directory Queue");

            var convertTarget = DI.config.ExecutingAssembly;
            var createdCirDataFile = Path.Combine(directory_CreatedCirFiles.getCurrentDirectory(),
                                                 Path.GetFileName(convertTarget) + ".CirData");

            if (File.Exists(createdCirDataFile))
            {
                DI.log.info("target createdCirDataFile already exists, so deleting it : " + createdCirDataFile);
                Files.deleteFile(createdCirDataFile);
            }            
            Assert.That(false == File.Exists(createdCirDataFile) ,"createdCirDataFile should not exist: " + createdCirDataFile);
            // get the number of files in CreatedCirFiles
            var filesInCreatedCirFilesDir = directory_CreatedCirFiles.getFiles().Count;


            // this will copy the file and trigger the CirCreationQueue
            Files.Copy(convertTarget, directory_CirCreationQueue.getCurrentDirectory());

            Assert.That(directory_CirCreationQueue.getFiles().Count == 1,
                        "Now there should be one file in the directory Queue");


            // wait some time to allow conversion to take place  (ideally we should be hooking into the directory_CreatedCirFiles._onFileWatchEvent
            Thread.Sleep(2000);


            

            Assert.That(File.Exists(createdCirDataFile), "createdCirDataFile shouold be ther now: " + createdCirDataFile);
            Assert.That(filesInCreatedCirFilesDir + 1 == directory_CreatedCirFiles.getFiles().Count,
                        "There should only had been one extra file in the CreatedCirFiles dir");
            
        }
        

        [TearDown]
        public void closeGui()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }
    }
}
