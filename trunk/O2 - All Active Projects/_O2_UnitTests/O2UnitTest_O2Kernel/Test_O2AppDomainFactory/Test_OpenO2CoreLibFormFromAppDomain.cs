// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.CodeUtils;
using O2.Kernel.Objects;
using O2.UnitTests.Test_O2Kernel;

namespace O2.UnitTests.Test_O2Kernel.Test_O2AppDomainFactory
{
    [TestFixture]
    public class Test_OpenO2CoreLibFormFromAppDomain
    {
        #region Setup/Teardown

        [SetUp]
        public void checkIfHardCodedO2DevelopmentLibPathExists()
            // this will not work when this test is invoked from the O2 GUI outside the current dev box
        {
            Assert.That(Directory.Exists(hardCodedO2DevelopmentLib),
                        "In test setup: hardCodedO2LocalBuildDir doesn't exist in this box: " +
                        hardCodedO2DevelopmentLib);
        }

        [TearDown]
        public void closeAppDomainAndDeletedBaseDirectory()
        {
            AppDomainUtils.closeAppDomain(o2AppDomainFactory.appDomain, true /*onTestCompletionDeleteTempFilesCreated*/);
        }

        #endregion


        private const string assemblyToProcess = "O2_Tool_CSharpScripts"; //the o2GuiControl has moved from "O2_CoreLib";

        private static readonly string hardCodedO2DevelopmentLib = DI.hardCodedO2LocalBuildDir;
        // we need to use this since the Unit test does't copy all Dlls to its test enviroment

        private static readonly string fullPathToAssemblyToProcess = Path.Combine(hardCodedO2DevelopmentLib, assemblyToProcess + ".exe");
        public static bool autoCloseForm = true;
        private O2AppDomainFactory o2AppDomainFactory;


        [Test]
        public void Test_CreateAppDomainWithO2CoreLibDll()
        {
            const string dynamicCommand_InitialTest = "nameOfCurrentDomain O2Proxy O2_Kernel";
            // get test AppDomain in temp folder
            o2AppDomainFactory =
                CecilAssemblyDependencies.getO2AppDomainFactoryOnTempDirWithAllDependenciesResolved(
                    fullPathToAssemblyToProcess);
            Assert.That(o2AppDomainFactory != null, "o2AppDomainFactory was null");
            // send message to DebugView (will not show in NUnit since this is from a different AppDomain)
            o2AppDomainFactory.invokeMethod("logInfo O2Proxy O2_Kernel",
                                            new object[]
                                                {"Testing appDomain from Test_OpenO2CoreLibFormFromAppDomain UnitTest"});

            //Assert.That(o2AppDomainFactory.load(appDomainProxyDll, fullPathToAppDomainProxyDll, true), "problem loading appDomainProxyDll");            
            // do a quick dynamic invokation to confirm that all is 
            Assert.That(
                o2AppDomainFactory.appDomain.FriendlyName ==
                (string) o2AppDomainFactory.invokeMethod(dynamicCommand_InitialTest),
                "Problem doing dynamic invoke of method nameOfCurrentDomain which returns the current appDomain name");
            DI.log.info("o2AppDomainFactory is created ready for use :)");
        }

        [Test]
        public void Test_OpenO2CorLibForm()
        {
            Test_CreateAppDomainWithO2CoreLibDll();
            // do a quick dynamic invocation to confirm that all is OK                         

            //o2AppDomainFactory.proxyInvokeInstance("O2_CoreLib", "Log", "info", new object[] { "test" });     

            Assert.That(
                o2AppDomainFactory.appDomain.FriendlyName ==
                (string) o2AppDomainFactory.proxyInvokeInstance("O2_Kernel", "O2Proxy", "nameOfCurrentDomain"),
                "nameOfCurrentDomain  test failed");


            // get proxy for o2GuiForm
            object o2GuiWithDockPanel = o2AppDomainFactory.proxyInvokeInstance("O2_External_WinFormsUI", "O2GuiWithDockPanel", "");
            Assert.That(o2GuiWithDockPanel != null, "o2GuiWithDockPanel was null");
            Assert.That(o2GuiWithDockPanel.GetType().Name == "O2GuiWithDockPanel",
                        "o2GuiWithDockPanel had the wrong type: " + o2GuiWithDockPanel.GetType());

            if (!autoCloseForm)
                o2AppDomainFactory.showMessageBox("Ready to start?");

            //start o2GuiWithDockPanel on a separate STAthread            
            O2Thread.staThread(() => ((Form) o2GuiWithDockPanel).ShowDialog());

            o2AppDomainFactory.proxyInvokeStatic("O2_External_WinFormsUI", "O2GuiWithDockPanel", "logDebug",
                                                 new object[] {"Good Morning you have 10 seconds for your tests"});

            if (!autoCloseForm)
            {
                int maxSleeps = 5;
                while (maxSleeps-- > 0)
                {
                    Thread.Sleep(10000);
                    if (DialogResult.No ==
                        (DialogResult)
                        o2AppDomainFactory.proxyInvokeStatic("O2_External_WinFormsUI", "O2GuiWithDockPanel", "showMessageBox",
                                                             new object[]
                                                                 {
                                                                     "your 10 seconds are up, Do you want another 10 Seconds? (you can ask for more " +
                                                                     maxSleeps + " extensions",
                                                                     "Message from O2 AppDomain Central",
                                                                     MessageBoxButtons.YesNo
                                                                 }))
                        break;
                }
                // show final message (using a simpler call syntax :) (using extension methods)
                o2AppDomainFactory.showMessageBox("Time's up closing all Forms", "Final Message", MessageBoxButtons.OK);
            }
            // close Form
            Thread.Sleep(1000);
            ((Form) o2GuiWithDockPanel).Close();
            if (autoCloseForm)
                Assert.Ignore(
                    "Success: change value of autoCloseForm to see some dynamic intercation between this unit test and the O2GuiWithDockPanel form");
            Assert.Ignore(
                "Success: remember to put the value of autoCloseForm to false, or you will have those popups all the time ");
        }
    }
}
