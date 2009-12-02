// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;


namespace O2.UnitTests.Test_O2Kernel.Test_WCF
{
    [TestFixture, Ignore ("Needs O2Kernel.exe proxy")]
    public class Test_SendingMessagesToAnotherO2Kernel
    {
        private const string newO2KernelProcessName = "Client_O2Kernel";
        IO2WcfKernelMessage o2WcfProxy;

        [SetUp]
        public void createNewO2Kernel()
        {            
            DI.log.info("Creating new O2Kernel Process with Name: {0}", DI.config.O2KernelAssemblyName);
            Processes.startProcess(DI.config.O2KernelAssemblyName, newO2KernelProcessName);
        }

        [Test]
        public void sendMessagesToNewKernel()
        {
            DI.log.info("in sendMessagesToNewKernel");
            o2WcfProxy = O2WcfUtils.createClientProxy(newO2KernelProcessName);
            Assert.That(o2WcfProxy.allOK(), "o2WcfProxy.allOK() returned false");
            Assert.That(!string.IsNullOrEmpty(o2WcfProxy.getName()), "o2WcfProxy.getName() was null or empty");
            DI.log.info("o2WcfProxy Name: {0}", o2WcfProxy.getName());
            o2WcfProxy.o2ShellCommand("echo Hello_from_another_process");
        }

        [TearDown]
        public void closeNewO2Kernel()
        {
            var processId = o2WcfProxy.getO2KernelProcessId();
            DI.log.info("o2WcfProxy ProcessID: {0}", o2WcfProxy.getO2KernelProcessId());
            Assert.That(Processes.getProcess(processId) != null, "could not find process with ID " + processId);
            o2WcfProxy.closeO2KernelProcess();
            Processes.Sleep(2000);
            Assert.That(Processes.getProcess(processId) == null, "Process should not be there after o2WcfProxy.closeO2KernelProcess()");
        }

    }
}
