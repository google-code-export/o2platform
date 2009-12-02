using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel;
using O2.Kernel.Objects;
using O2.UnitTests.Test_O2Kernel;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;

namespace O2.UnitTests.Test_O2Kernel.Test_O2AppDomainFactory
{
    [TestFixture]
    public class Test_LauchO2AscxInAppDomain
    {
        private const string appDomainName = "testLaunchO2AscxGui";
        
        /*[SetUp]
        public void createAppDomain()
        {
            O2AppDomainFactory.create(appDomainName);
        }*/

        [Test]
        public void launchO2AscxGui()
        {
            O2AppDomainFactory.create(appDomainName); // move this here so that we can execute this test from O2
            
            
            Assert.That(PublicDI.appDomainsControledByO2Kernel.ContainsKey(appDomainName), "Cound not find appDomainName:{0}", appDomainName);
            var o2AppDomainFactory = PublicDI.appDomainsControledByO2Kernel[appDomainName];
            Assert.That(o2AppDomainFactory.Name == appDomainName, "o2AppDomainFactory.Name != appDomainName");
            PublicDI.log.info("Created appDomain Name: {0}", o2AppDomainFactory.Name);
            // StringsAndLists.showListContents(o2AppDomainFactory.FilesInAppDomainBaseDirectory);

            var o2AscxGuiForm = o2AppDomainFactory.getProxyObject("O2AscxGUI O2_External_WinFormsUI");
            
            Assert.That(o2AscxGuiForm != null, "o2AscxGuiForm object was null");
            var lauchResult  = o2AppDomainFactory.invoke(o2AscxGuiForm, "launch");
            Assert.That(lauchResult != null && lauchResult is bool && (bool) lauchResult, "prob with lauchResult");
            o2AppDomainFactory.invoke(o2AscxGuiForm, "logDebug",new object[] {"message from Unit Test"});
            

            Processes.Sleep(2000);
            var closeResult = o2AppDomainFactory.invoke(o2AscxGuiForm, "close");
            Assert.That(closeResult != null && closeResult is bool && (bool)closeResult, "prob with closeResult");
            //DI.log.info("type : {0}", o2AscxGuiForm.GetType().FullName);
            
            //appDomainName
        }
    }
}