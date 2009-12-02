// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.CodeUtils;
using O2.Kernel.Objects;
using O2.UnitTests.Test_O2Kernel;

namespace O2.UnitTests.Test_O2Kernel.Test_O2AppDomainFactory
{
    [TestFixture]
    public class Test_StartAllFormsAndControlsOnUniqueAppDomains
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

        #endregion

        private const string o2DllToProcess = "O2_Kernel";

        private static string hardCodedO2DevelopmentLib = DI.hardCodedO2LocalBuildDir;
        // we need to use this since the Unit test does't copy all Dlls to its test enviroment

        private const bool onTestCompletionDeleteTempFilesCreated = true;
        //private string fullPathToDllToProcess = Path.Combine(Config.getCurrentO2Directory(), o2DllToProcess);
        

        [Test]
        public void createAppDomainAndAllDependencies()
        {
            string fullPathToDllToProcess = Path.Combine(hardCodedO2DevelopmentLib, o2DllToProcess + ".exe");
            // AppDomainUtils.findDllInCurrentAppDomain(o2DllToProcess);
            DI.log.debug("For the dll: {0}", o2DllToProcess);

            Dictionary<string, string> assemblyDependencies =
                new CecilAssemblyDependencies(fullPathToDllToProcess).calculateDependencies();
            DI.log.debug("There are {0} assembly dependencies to load", assemblyDependencies.Count);
            var o2AppDomainFactory = new O2AppDomainFactory();

            Assert.That(o2AppDomainFactory.load(assemblyDependencies).Count == 0,
                        "There were assemblyDependencies that were not loaded");


            DI.log.d("List of loaded Assemblies");
            foreach (string assembly in o2AppDomainFactory.getAssemblies(true))
                DI.log.d("  -  " + assembly);

            AppDomainUtils.closeAppDomain(o2AppDomainFactory.appDomain, onTestCompletionDeleteTempFilesCreated);
        }

        /*public O2AppDomainFactory createAppDomainInTempFolder()
        {
            // create new appdomain, set it to temp temp directory
            
        }*/

        // the loading strategy is that we will first try to load the assembly direcly (and that should work for the assemblies in the GAC)
        [Test]
        public void createAppDomainAndLoadDll_MainTestCase()
        {
            string fullPathToDllToProcess = DI.config.ExecutingAssembly;  //Path.Combine(hardCodedO2DevelopmentLib, o2DllToProcess + ".exe");
            //AppDomainUtils.findDllInCurrentAppDomain(o2DllToProcess);
            Assert.That(File.Exists(fullPathToDllToProcess),
                        "fullPathToDllToProcess doesn't exist:" + fullPathToDllToProcess);

            // get test AppDomain in temp folder
            var o2AppDomainFactory = new O2AppDomainFactory();

            // load dll from it (this will fail until all dependencies are resolved
            Assert.That(o2AppDomainFactory.load(o2DllToProcess, fullPathToDllToProcess, true),
                        "Dll failed to load into AppDomain");

            // get assemblyDependencies 
            Dictionary<string, string> assemblyDependencies =
                new CecilAssemblyDependencies(fullPathToDllToProcess).calculateDependencies();
            DI.log.debug("There are {0} assembly dependencies to load", assemblyDependencies.Count);
            // load them and abort if we were not able to load all)
            Assert.That(o2AppDomainFactory.load(assemblyDependencies).Count == 0,
                        "There were assemblyDependencies that were not loaded");

            // double check that all is OK by dinamcally invoking some methods on the new AppDomain                           
            Assert.That(null != o2AppDomainFactory.invokeMethod("nameOfCurrentDomainStatic O2Proxy O2_Kernel", new object[0]),
                        "Could not invoke methods inside O2_CoreLib");
            o2AppDomainFactory.invokeMethod("logInfo O2Proxy O2_Kernel",
                                            new object[] {"Hello from createAppDomainInTempFolder UnitTest"});

            AppDomainUtils.closeAppDomain(o2AppDomainFactory.appDomain, onTestCompletionDeleteTempFilesCreated);
        }

        [Test]
        public void findAllDependenciesForDll()
        {
            string fullPathToDllToProcess = DI.config.ExecutingAssembly; //Path.Combine(hardCodedO2DevelopmentLib, o2DllToProcess + ".exe");
            // AppDomainUtils.findDllInCurrentAppDomain(o2DllToProcess);
            var cecilAssemblyDependencies = new CecilAssemblyDependencies(fullPathToDllToProcess);
            Dictionary<string, string> assemblyDependencies = cecilAssemblyDependencies.calculateDependencies();
            Assert.That(assemblyDependencies != null && assemblyDependencies.Count > 0,
                        " problem calculating assemblyDependencies");

            DI.log.debug("There are {0} assembly dependencies to resolve", assemblyDependencies.Count);
            foreach (string assemblyToResolve in assemblyDependencies.Keys)
                DI.log.info("{0} : {1}", assemblyDependencies[assemblyToResolve], assemblyToResolve);
        }
    }
}
