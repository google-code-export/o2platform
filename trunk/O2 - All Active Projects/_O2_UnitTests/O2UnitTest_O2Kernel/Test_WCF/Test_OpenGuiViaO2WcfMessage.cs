// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.Kernel.WCF;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;

namespace O2.UnitTests.Test_O2Kernel.Test_WCF
{
    [TestFixture,Ignore("NEED TO WRITE A WRAPPER ON O2_Kernel.Exe for this to work")]
    public class Test_OpenGuiViaO2WcfMessage
    {        
        IO2WcfKernelMessage o2WcfProxy;

        [SetUp]
        public void CreateRemoteO2KernelAndGetProxy()
        {
            var newO2KernelProcessName = "Client_O2Kernel";
            DI.log.info("Creating new O2Kernel Process with Name: {0}", DI.config.O2KernelAssemblyName);
            Processes.startProcess(DI.config.O2KernelAssemblyName, newO2KernelProcessName);
            o2WcfProxy = O2WcfUtils.createClientProxy(newO2KernelProcessName);
            Assert.That(o2WcfProxy.allOK(), "o2WcfProxy.allOK() returned false");
            Assert.That(!string.IsNullOrEmpty(o2WcfProxy.getName()), "o2WcfProxy.getName() was null or empty");
        }

        [Test]
        public void createGuiOnRemoteO2Kernel()
        {
            DI.log.info("Creating empty AscxGui via Remote O2Kernel");
            var newAppDomain = "o2AscxGui";
            var dllToLoadInNewAppDomain = new List<string> { "O2_External_WinFormsUI.dll", "WeifenLuo.WinFormsUI.Docking.dll", "O2_Kernel.exe", "O2_DotNetWrappers.dll" };
            Assert.That(o2WcfProxy.createAppDomainWithDlls(newAppDomain, dllToLoadInNewAppDomain), "AppDomain Creation Failed") ;
            //Assert.Fail("Add check for isGuiAvailable");
            Assert.That(null != o2WcfProxy.invokeOnAppDomainObject(newAppDomain, "O2AscxGUI O2_External_WinFormsUI","launch",null), "invokeOnAppDomainObject failed");            
            Assert.That((bool)o2WcfProxy.invokeOnAppDomainObject(newAppDomain, "O2AscxGUI", "isGuiLoaded", new object[0]), "isGuiLoaded should be true here");
            DI.log.info("O2AscxGUI.isGuiLoaded = {0}", o2WcfProxy.invokeOnAppDomainObject(newAppDomain, "O2AscxGUI", "isGuiLoaded", new object[0]));
            
        }

        [TearDown]
        public void CloseRemoteO2Kernel()
        {            
            o2WcfProxy.closeO2KernelProcess();
        }
    }
}
