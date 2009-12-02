// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using NUnit.Framework;
using O2.Core.CIR.Ascx;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.External.WinFormsUI.Forms;
using O2.Kernel;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.WCF;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;

namespace O2.UnitTests.Test_O2CoreCIR.Test_RemoteLoad
{
    [TestFixture]
    public class Test_HostCirDataOnSepareteProcess
    {
        private const string newO2KernelProcessName = "O2Kernel_WithCirData";        

        [Test]
        public void test_openCirDataControlDirectly()
        {
            // get control
            var ascxControlName = "Cir Analysis";
            O2AscxGUI.openAscxAsForm(typeof(ascx_CirAnalysis),ascxControlName);
            var cirAnalysis = (ascx_CirAnalysis)O2AscxGUI.getAscx(ascxControlName);
            Assert.That(cirAnalysis != null, "cirAnalysis was null");
            //process O2Kernel (i.e convert it into CirData)
            Assert.That(cirAnalysis.getCirDataAnalysisObject().dCirClass.Count == 0, "At this stage there should be no classes loaded");
            cirAnalysis.loadO2CirDataFile(DI.config.ExecutingAssembly);
            Assert.That(cirAnalysis.getCirDataAnalysisObject().dCirClass.Count > 0, "There were no classes loaded in CirDataAnalysis object");
            if (cirAnalysis.ParentForm!=null)
                cirAnalysis.ParentForm.Close();
        }

        [Test]
        public void test_openCirDataControlUsingO2Messages()
        {
            // get control
            var ascxControlName = "Cir Analysis";
            O2Messages.openAscxAsForm(typeof(ascx_CirAnalysis).FullName, ascxControlName);

            O2Messages.executeOnAscxSync(ascxControlName, "loadO2CirDataFile", new [] {DI.config.ExecutingAssembly});
            var returnedData = O2Messages.executeOnAscxSync(ascxControlName, "getCirDataAnalysisObject", new string[0]);
            Assert.That(returnedData != null && returnedData is ICirDataAnalysis, "probs with returned data");
            var cirData = (ICirDataAnalysis) returnedData;
            Assert.That(cirData.dCirClass.Count > 0, "There were no classes loaded in CirDataAnalysis object");
            O2Messages.closeAscxParent(ascxControlName);                          
        }
        
        [Test]
        public void test_openCirDataControlUsingLocalWcfHost()
        {
            O2WcfUtils.createWcfHostForThisO2KernelProcess();
            IO2WcfKernelMessage o2WcfProxy = O2WcfUtils.getClientProxyForThisO2KernelProcess();
            var ascxControlName = "Cir Analysis";            
            Assert.That(o2WcfProxy.allOK(), "o2WcfProxy.allOK() returned false");
            DI.log.info("o2WcfProxy.allOK() = {0}", o2WcfProxy.allOK());
            Assert.That(Processes.getCurrentProcessID() == o2WcfProxy.getO2KernelProcessId(), "Processes ID should be the same");

            o2WcfProxy.O2Messages("openAscxAsForm", new object[] { typeof(ascx_CirAnalysis).FullName, ascxControlName });

            o2WcfProxy.O2Messages("executeOnAscxSync", new object[] {ascxControlName, "loadO2CirDataFile" , new [] { DI.config.ExecutingAssembly }});
            //o2WcfProxy.O2Messages("getAscxSync", new object[] {ascxControlName});

            //var cirClasses = o2WcfProxy.O2Messages("executeOnAscxSync", new object[] { ascxControlName, "getClasses", new string[0]});
            var cirClasses = o2WcfProxy.getStringListFromAscxControl(ascxControlName, "getClasses", new string[0]);
            
            Assert.That(cirClasses != null, "prob with fetched cirClasses object");
            Assert.That(cirClasses.Count > 0, "There were no classes returned in cirClasses");
            DI.log.info("There were {0} classes returned" , cirClasses.Count);

            var cirFunctions = o2WcfProxy.getStringListFromAscxControl(ascxControlName, "getFunctions", new string[0]);
            Assert.That(cirFunctions != null && cirFunctions.Count > 0, "prob with fetched cirFunctions object");
            DI.log.info("There were {0} functions returned", cirFunctions.Count);

            o2WcfProxy.O2Messages("closeAscxParent", new object[] { ascxControlName});           

            WCF_DI.o2WcfKernelMessage.stopHost();
            //o2WcfProxy.closeO2KernelProcess();
        }

        [Test]
        public void test_openCirDataControlOnSeparateAppDomainViaWcf()
        {
            // First we need to figure out which DLL has the type we need
            var targetAssemblies = new List<string> { typeof (ascx_CirAnalysis).Assembly.Location , typeof(O2AscxGUI).Assembly.Location};
            //Assert.That(File.Exists(targetAssembly), "could not find assembly: " + targetAssembly);

            var dependentAssemblies = new CecilAssemblyDependencies(targetAssemblies, new List<string> { DI.config.hardCodedO2LocalBuildDir }).calculateDependencies();
            Assert.That(dependentAssemblies.Count > 0,"There we no dependencies calculated");
            DI.log.info("There are {0} dependent assemblies", dependentAssemblies.Count);
            // StringsAndLists.showListContents(dependentAssemblies);  // uncoment to see list of dlls
            var wcfHost = O2WcfUtils.createWcfHostForThisO2KernelProcess();
            IO2WcfKernelMessage o2WcfProxy = O2WcfUtils.getClientProxyForThisO2KernelProcess();

            runTestsViaWcfProxy(o2WcfProxy);
            // now that all tests are completed close host
            wcfHost.stopHost();            
            
        }
    

        /// <summary>
        /// this code is just about the same as the one in test_createAppDomainWithRequiredDlls (but the fact that it is on  separate process
        /// </summary>
        [Test, Ignore("Need O2_Kernel.exe wrapper")]
        public void test_openCirDataControlOnRemoteWcfHost()
        {

            DI.log.info("Creating new O2Kernel Process with Name: {0}", DI.config.O2KernelAssemblyName);
            Processes.startProcess(DI.config.O2KernelAssemblyName, newO2KernelProcessName);
            IO2WcfKernelMessage o2WcfProxy = O2WcfUtils.createClientProxy(newO2KernelProcessName);
            Assert.That(o2WcfProxy.allOK(), "o2WcfProxy.allOK() returned false");
            DI.log.info("o2WcfProxy.allOK() = {0}", o2WcfProxy.allOK());
            // confirm that o2WcfProxy is running of a different process
            var currentProcessID = Processes.getCurrentProcessID();
            var remoteProcessID = o2WcfProxy.getO2KernelProcessId();
            DI.log.info("Processes ID -> Current:  {0} remote: {1}", currentProcessID, remoteProcessID);
            Assert.That(currentProcessID != remoteProcessID, "Processes ID should not be the same");

            runTestsViaWcfProxy(o2WcfProxy);
            // close remote proxy and process            
            var process = Processes.getProcess(o2WcfProxy.getO2KernelProcessId());
            o2WcfProxy.closeO2KernelProcess();
            process.WaitForExit();      // wait for its exit 
        }

        public void runTestsViaWcfProxy(IO2WcfKernelMessage o2WcfProxy)
        {
            // first thing to do is to create a new AppDomain that has all the required dlls for the two main types we want to load ascx_CirAnalysis and O2AscxGUI
            var testAppDomain = "AppDomainWithCirData";
            var targetAssemblies = new List<string> { typeof(ascx_CirAnalysis).Assembly.Location, typeof(O2AscxGUI).Assembly.Location };
            var dependentAssemblies = new CecilAssemblyDependencies(targetAssemblies, new List<string> { DI.config.hardCodedO2LocalBuildDir }).calculateDependencies();
            o2WcfProxy.createAppDomainWithDlls(testAppDomain, new List<string>(dependentAssemblies.Values));
            
            // remote O2 Kernel is all set, now let's open the ascx_CirAnalyis control
            var ascxControlName = "Cir Analysis";
            o2WcfProxy.invokeOnAppDomainObject(testAppDomain, typeof(O2AscxGUI).FullName, "openAscxAsForm", new object[] { typeof(ascx_CirAnalysis).FullName, ascxControlName });

            // make sure it is there
            Assert.That((bool)o2WcfProxy.invokeOnAppDomainObject(testAppDomain, typeof(O2AscxGUI).FullName, "isAscxLoaded", new object[] { ascxControlName }), "isAscxLoaded should be true here");
            Assert.That(false == (bool)o2WcfProxy.invokeOnAppDomainObject(testAppDomain, typeof(O2AscxGUI).FullName, "isAscxLoaded", new object[] { "dummyControlName" }), "isAscxLoaded should be false here");

            // now load some CirData into it
            o2WcfProxy.O2Messages("executeOnAscxSync", new object[] { ascxControlName, "loadO2CirDataFile", new [] { DI.config.ExecutingAssembly } });

            // and retrive the data as string lists
            var cirClasses = o2WcfProxy.getStringListFromAscxControl(ascxControlName, "getClasses", new string[0]);
            var cirFunctions = o2WcfProxy.getStringListFromAscxControl(ascxControlName, "getFunctions", new string[0]);

            Assert.That(cirClasses.Count > 0 && cirFunctions.Count >0, "There were no classes or functions returned in cirClasses");
            DI.log.info("There were {0} classes returned", cirClasses.Count);                        
            DI.log.info("There were {0} functions returned", cirFunctions.Count);                        
                        
            // close the control and its parant form
            o2WcfProxy.invokeOnAppDomainObject(testAppDomain, typeof(O2AscxGUI).FullName, "closeAscxParent", new object[] { ascxControlName });            
        }
    }
}
