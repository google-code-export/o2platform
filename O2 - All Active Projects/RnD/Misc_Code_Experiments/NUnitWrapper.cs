// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using O2.Kernel.Objects;

namespace O2.Rnd.Misc_Code_Experiments
{
    public class NUnitWrapper
    {
        public static string pathToNUnitBinFolder = @"C:\Program Files\NUnit\Bin";
        public AppDomain appDomain;
        public Assembly nUnitRunnerDll;

        // public string nUnitGuiRunnerDllName = "nunit-gui-runner.dll";
        // public string nUnitUtilDllName = "nunit.util.dll";

        public NUnitWrapper()
        {
        }

        public NUnitWrapper(string _pathToNUnitBinFolder)
        {
            pathToNUnitBinFolder = _pathToNUnitBinFolder;
        }

        public string NUnitGuiRunnerDll
        {
            get { return Path.Combine(pathToNUnitBinFolder, "nunit-gui-runner.dll"); }
        }

        public string nUnitUtilDll
        {
            get { return Path.Combine(pathToNUnitBinFolder, "nunit.util.dll"); }
        }

        public string nUnitUikitDll
        {
            get { return Path.Combine(pathToNUnitBinFolder, "nunit.uikit.dll"); }
        }


        /* public void startNunitInSeparateAppDomain(string fileToLoad)
        {
           // var args = new[] {  fileToLoad}; // (O2CorLib.dll) assembly that will be loaded and searched for Unit tests                                        
            var args = new string[] {}; 
            string exeToRun = Path.Combine(pathToNUnitBinFolder, "nunit-x86.exe");
            appDomain = AppDomainUtils.newAppDomain_ExecuteAssembly("nUnitExec", pathToNUnitBinFolder, exeToRun, args);
        }*/


        // ReSharper restore SuggestUseVarKeywordEvident


        /*  public bool isAttached
        {
            get { return NUnitControl != null; }
        }*/

        /*   public Control NUnitControl
        {
            get
            {
                if (nUnitRunnerDll == null)
                    loadNUnitDll();

                //return null;
                var label = new Label {Text = "aaaaaaaaaa"};
                return label;
            }
        }*/


        // On this one lets not use var keywords (since that will make it easier to understand what is going on)
        // ReSharper disable SuggestUseVarKeywordEvident             
        public Form getMainForm()
        {
            var nUnitGuiRunner = new O2ObjectFactory(NUnitGuiRunnerDll);
            var nUnitUtil = new O2ObjectFactory(nUnitUtilDll);
            var nUnitUikit = new O2ObjectFactory(nUnitUikitDll);


            // [code to implement] public static int Main(string[] args)
            // [code to implement] GuiOptions guiOptions = new GuiOptions(args);
            //var args = new[] { Config.getExecutingAssembly() }; // assembly that will be loaded and searched for Unit tests                                        
            var args = new[] { nUnitUtilDll };
            O2Object guiOptions = nUnitGuiRunner.ctor("GuiOptions", new[] { args });


            // [code to implement] 
            /*
            ServiceManager.Services.AddService(new SettingsService())            
            ServiceManager.Services.AddService(new DomainManager());
            ServiceManager.Services.AddService(new RecentFilesService());
            ServiceManager.Services.AddService(new TestLoader(new GuiTestEventDispatcher()));
            ServiceManager.Services.AddService(new AddinRegistry());
            ServiceManager.Services.AddService(new AddinManager());
            ServiceManager.Services.AddService(new TestAgency());*/

            O2Object serviceManager_Services = nUnitUtil.staticTypeGetProperty("ServiceManager", "Services");
            serviceManager_Services.call("AddService", new[] { nUnitUtil.ctor("SettingsService") });
            serviceManager_Services.call("AddService", new[] { nUnitUtil.ctor("DomainManager") });
            serviceManager_Services.call("AddService", new[] { nUnitUtil.ctor("RecentFilesService") });
            serviceManager_Services.call("AddService",
                                         new[]
                                             {
                                                 nUnitUtil.ctor("TestLoader",
                                                                new[] {nUnitUikit.ctor("GuiTestEventDispatcher")})
                                             });
            serviceManager_Services.call("AddService", new[] { nUnitUtil.ctor("AddinRegistry") });
            serviceManager_Services.call("AddService", new[] { nUnitUtil.ctor("AddinManager") });
            serviceManager_Services.call("AddService", new[] { nUnitUtil.ctor("TestAgency") });

            // [code to implement]  ServiceManager.Services.InitializeServices();
            serviceManager_Services.call("InitializeServices");

            // [code to implement] AppContainer container = new AppContainer();
            O2Object container = nUnitUikit.ctor("AppContainer");

            // [don't need to implement this one] AmbientProperties serviceInstance = new AmbientProperties();            
            var serviceInstance = new AmbientProperties();

            // [code to implement] container.Services.AddService(typeof(AmbientProperties), serviceInstance);
            O2Object container_services = container.get("Services");
            container_services.call("AddService", new object[] { typeof(AmbientProperties), serviceInstance });


            //  [code to implement] NUnitForm component = new NUnitForm(guiOptions);
            O2Object component = nUnitGuiRunner.ctor("NUnitForm", new[] { guiOptions });

            // [code to implement] container.Add(component);
            container.call("Add", new object[] { component });


            //  [code to implement]   new GuiAttachedConsole();
            nUnitUikit.ctor("GuiAttachedConsole");

            return (Form)component.Obj;

            /*return (form is Form) ? (Form) form.Obj : null;
            if (form is Form)
                return (Form) form.Obj;
            else
                return null;                */
        }

        public object execMainForm()
        {
            //[original code] public static int Main(string[] args)
            // NUnit.Gui.AppEntry.Main

            var nUnitGuiRunner = new O2ObjectFactory(NUnitGuiRunnerDll);
            var args = new[] { DI.config.ExecutingAssembly };
            // assembly that will be loaded and searched for Unit tests                                        
            return nUnitGuiRunner.call("AppEntry", "Main", new object[] { args });
        }

    }

    [TestFixture]
    public class Test_NUnitWrapper
    {
        [Test]
        [STAThread]
        [Ignore("Will start a new AppDomain with NUnit, need to reimplement it with the new O2 AppDomain subSystem")]
        public void test_CreateFormObject()
        {
            var nUnitWrapper = new NUnitWrapper();
            Assert.That(File.Exists(nUnitWrapper.NUnitGuiRunnerDll), "Nunit Gui Runner executable was not found: {0}",
                        nUnitWrapper.NUnitGuiRunnerDll);

            //nUnitWrapper.execMainForm();
            var appDomainSetup = new AppDomainSetup
                                     {
                                         ApplicationBase = NUnitWrapper.pathToNUnitBinFolder
                                     };
            //appDomainSetup.
            AppDomain appDomain = AppDomain.CreateDomain("nUnitExec", null, appDomainSetup);
            appDomain.ExecuteAssembly(Path.Combine(NUnitWrapper.pathToNUnitBinFolder, "nunit-x86.exe"));

            // Form mainForm = nUnitWrapper.getMainForm();
            // Assert.That(mainForm != null, "mainForm == null");
            // mainForm.ShowDialog();
            //Application.Run(mainForm);
        }
    }
}
