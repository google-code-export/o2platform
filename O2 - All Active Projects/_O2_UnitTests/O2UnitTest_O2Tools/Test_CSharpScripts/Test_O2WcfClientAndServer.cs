// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Interfaces.Views;
using O2.Kernel.CodeUtils;
using O2.Kernel.Objects;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;
using O2.Kernel.WCF.MonoCecil;
using O2.Tool.CSharpScripts;

namespace O2.UnitTests.Test_O2Tools.Test_CSharpScripts
{
    [TestFixture]
    public class Test_O2WcfClientAndServer
    {

        [Test]
        public void Test_AppDomainLoadingAndUnloading()
        {
            List<String> dependentAssemblies = CecilAssemblyDependencies.getListOfDependenciesForType(typeof(StartCSharpScriptGui));
            var appDomainTempDirectory = DI.config.TempFolderInTempDirectory;
            var o2AppDomainFactory = new O2AppDomainFactory("testAppDomain", appDomainTempDirectory, dependentAssemblies);

            //o2AppDomainFactory.unLoadAppDomain();
            o2AppDomainFactory.unLoadAppDomainAndDeleteTempFolder();
        }

        [Test]
        public void loadCSharpScriptsGuiOnSeparateAppDomainUsingWCF_UsingO2WcfObject()
        {
            var wcfHostName = "loadCSharpScriptsGui";
            var wcfServer = new O2WcfServer(wcfHostName);
            var wcfClient = new O2WcfClient(wcfHostName);
            Assert.That(wcfServer.AllOK, "wcfServer was not OK");
            Assert.That(wcfClient.AllOK, "wcfClient was not OK");

            WCFCecilAssemblyDependencies.createAppDomainViaWcfClient(wcfClient,typeof(StartCSharpScriptGui));
            DI.log.info("**UnitTest: using main (with all controls currently loaded via it ");
            //if (false)
            {
                wcfClient.invoke(typeof (StartCSharpScriptGui), "Main", new object[0]);
                Processes.Sleep(2000);
                wcfClient.o2GuiAscx.close();
                wcfClient.o2GuiAscx.waitForAscxGuiClose();
            }
            DI.log.info("**UnitTest: just the gui with one ascx_Scripts module");
            //if (false)
            {
                wcfClient.o2GuiAscx.launch();
                wcfClient.o2GuiAscx.openAscx(typeof (ascx_Scripts), O2DockState.Document, "ascx_Scripts");
                wcfClient.o2GuiAscx.close();
                wcfClient.o2GuiAscx.waitForAscxGuiClose();
            }

            DI.log.info("**UnitTest: just the ascx_Scripts as a stand alone form");
            //if (false)
            {
                wcfClient.o2GuiAscx.openAscxAsForm(typeof (ascx_Scripts), "ascx_Scripts");
                Processes.Sleep(2000);
                wcfClient.o2GuiAscx.closeAscxParent("ascx_Scripts");
                wcfClient.o2GuiAscx.waitForAscxGuiClose();
            }
            //O2AscxGUI.closeAscxParent("ascx_Scripts");

            wcfClient.close();
            wcfServer.close();  
            
        }

        [Test]
        public void loadCSharpScriptsGuiOnSeparateAppDomainUsingWCF_ManualWay()
        {                        
            // create Wcf Host
            var wcfHostName = "loadCSharpScriptsGui";
            var wcfHost = O2WcfUtils.createWcfHostAndStartIt(wcfHostName);                        
            IO2WcfKernelMessage o2WcfClientProxy = wcfHost.getClientProxy();
            Assert.That(o2WcfClientProxy.allOK(), "o2WcfProxy was not OK");

            // create separateAppDomain
            var testAppDomain = "AppDomainWithCSharpScripts";
            var targetAssemblies = new List<string> { typeof(StartCSharpScriptGui).Assembly.Location };
            var directoryWithSourceAssemblies = DI.config.hardCodedO2LocalBuildDir;
            var dependentAssemblies = new CecilAssemblyDependencies(targetAssemblies, new List<string> { directoryWithSourceAssemblies }).calculateDependencies();
            o2WcfClientProxy.createAppDomainWithDlls(testAppDomain, new List<string>(dependentAssemblies.Values));

            // will just open an empty O2 Gui
            //o2WcfClientProxy.invokeOnAppDomainObject(testAppDomain, "O2AscxGUI", "launch",null);

            // open the CSharpScripts O2 Module by simulating the Main execution invocation
            o2WcfClientProxy.invokeOnAppDomainObject(testAppDomain, typeof (StartCSharpScriptGui).FullName, "Main", new object[0]);

            o2WcfClientProxy.invokeOnAppDomainObject(testAppDomain, "O2AscxGUI", "logInfo", new[] {"Hello from Unit test"}); 
         //   Processes.Sleep(2000);
            o2WcfClientProxy.invokeOnAppDomainObject(testAppDomain, "O2AscxGUI", "close", null);            
          //  o2WcfClientProxy.invokeOnAppDomainObject(testAppDomain);
            //O2WcfUtils.
            wcfHost.stopHost();
        }
       
        public enum wcfO2AscxGUI
        {
            logInfo,
            close
        }
    }
}
