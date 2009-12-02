using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using System.IO;
using O2.UnitTests.Test_O2Debugger.MockObjects;

namespace O2.UnitTests.Test_O2Debugger.O2DebuggerMdbg
{
    [TestFixture]
    public class Test_TraceAnimations
    {
        private static readonly MockObjects_CompiledExe mockObjects_CompiledExe =
            new MockObjects_CompiledExe(MockObjects_CompiledExe.with_HelloWorldSample(),false);

        public bool showAnimateLog = true;

        private O2MDbg o2MDbg;

        [SetUp]
        public void startO2MDbgAndRunTestProcess()
        {
            o2MDbg = new O2MDbg();
            o2MDbg.o2MdbgIsReady.WaitOne(); // make sure the o2Mdbg is ready
            Assert.That(File.Exists(mockObjects_CompiledExe.PathToCreatedAssemblyFile),"Test exe file was not created");                        
            //  Processes.Sleep(1000);  // give it 1s to start the process           
            
            Assert.That(!o2MDbg.IsActive && !o2MDbg.IsRunning, "At this stage both o2MDbg.IsActive and o2MDbg.IsRunning should be false");
            Assert.That(o2MDbg.lastCommandExecutionMessage != "", "o2MDbg.lastCommandExecutionMessage was empty");

            // start test process
            o2MDbg.execSync(O2MDbgCommands.run(mockObjects_CompiledExe.PathToCreatedAssemblyFile));
            //Processes.Sleep(1000);  // give it 1s to start
            Assert.That(o2MDbg.IsActive && !o2MDbg.IsRunning, "At this stage both o2MDbg.IsActive should be true and o2MDbg.IsRunning should be false");
        }


        [Test]
        public void test_AnimateOver()
        {            
            List<String> instructionsExecuted = o2MDbg.animateOver(0);
            Assert.That(instructionsExecuted.Count > 0, "There were no instructionsExecuted");
            if (showAnimateLog)
            {
                DI.log.debug("*******************  Instructions executed on animateOver (*******************");
                StringsAndLists.showListContents(instructionsExecuted);
            }
        }

        [Test]
        public void test_AnimateInto()
        {
            List<String> instructionsExecuted = o2MDbg.animateInto(0);
            Assert.That(instructionsExecuted.Count > 0, "There were no instructionsExecuted");
            if (showAnimateLog)
            {
                DI.log.debug("*******************  Instructions executed on animateInto (*******************");
                StringsAndLists.showListContents(instructionsExecuted);
            }
        }

        [Test]
        public void test_AnimateBack()
        {
            List<String> instructionsExecuted = o2MDbg.animateBack(0);
            Assert.That(instructionsExecuted.Count > 0, "There were no instructionsExecuted");
            if (showAnimateLog)
            {
                DI.log.debug("*******************  Instructions executed on test_AnimateBack (*******************");
                StringsAndLists.showListContents(instructionsExecuted);
            }
        }

        [TearDown]
        public void stopO2MDbg()
        {
            o2MDbg.stopMDbg();
        }

    }
}