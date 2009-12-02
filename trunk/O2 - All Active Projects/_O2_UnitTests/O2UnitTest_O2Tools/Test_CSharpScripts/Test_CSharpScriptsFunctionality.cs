using System;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.External.SharpDevelop;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.MonoCecil;
using O2.Tool.CSharpScripts;

namespace O2.UnitTests.Test_O2Tools.Test_CSharpScripts
{
    [TestFixture]
    public class Test_CSharpScriptsFunctionality
    {
        private O2WcfServer wcfServer;
        private O2WcfClient wcfClient;
        private string ascx_ScriptsFolderControl;
        private string ascx_AssemblyInvokeControl;
        private string ascx_O2MdbgShellControl;
        //private string scriptsFolderControl = "Scripts Folder";


        public string checkIfControlIsLoaded(Type locationOfMethod, string methodThatReturnsControlName)
        {
            // first get the Name
            var controlMame = (String)wcfClient.getProperty(locationOfMethod, methodThatReturnsControlName);
            Assert.IsNotNull(controlMame, "checkIfControlIsLoaded returned nulll for " + methodThatReturnsControlName);
            // then check that it is loaded
            Assert.That(wcfClient.o2GuiAscx.isAscxLoaded(controlMame), "ascx was not loaded: " + controlMame);
            return controlMame;
        }


        [SetUp]
        public void launchGuiViaMain()
        {
            var wcfHostName = "loadCSharpScriptsGui";
            wcfServer = new O2WcfServer(wcfHostName);
            wcfClient = new O2WcfClient(wcfHostName);
            Assert.That(wcfServer.AllOK, "wcfServer was not OK");
            Assert.That(wcfClient.AllOK, "wcfClient was not OK");
            WCFCecilAssemblyDependencies.createAppDomainViaWcfClient(wcfClient, typeof(StartCSharpScriptGui));
            wcfClient.invoke(typeof(StartCSharpScriptGui), "Main", new object[0]);

            // check if the three original controls are loaded
            Processes.Sleep(5000);
            ascx_ScriptsFolderControl = checkIfControlIsLoaded(typeof(StartCSharpScriptGui), "ascx_ScriptsFolderName");
            ascx_AssemblyInvokeControl = checkIfControlIsLoaded(typeof(StartCSharpScriptGui), "ascx_AssemblyInvokeName");
            ascx_O2MdbgShellControl = checkIfControlIsLoaded(typeof(StartCSharpScriptGui), "ascx_O2MdbgShellName");

            //Assert.That(wcfClient.o2GuiAscx.isAscxLoaded(scriptsFolderControl), "'Scripts Folder' ascx was not loaded");

            DI.log.LogRedirectionTarget = null;  // make this null so that we get all DI.log messages (since the O2AscxGui sets this to capture the logs for its LogViewer
            //wcfClient.o2GuiAscx.
              //O2AscxGUI.isAscxLoaded() 
        }

        [Test, Ignore("Need to fix test to reflect the fact that the script name contains the full path")]
        public void loadSampleFilesInScriptEditor()
        {
            var sampleScriptNames = wcfClient.o2GuiAscx.invokeAndGetStringList(ascx_ScriptsFolderControl, "getSampleScriptNames");
            Assert.That(sampleScriptNames != null, "sampleScriptNames was null");
            DI.log.info("There are {0} script samples", sampleScriptNames.Count);
            Assert.That(sampleScriptNames.Count > 2, "There should be at least 3 scripts");
            foreach (var sampleScript in sampleScriptNames)
            {
                var sampleScriptContent = (String)wcfClient.o2GuiAscx.invoke(ascx_ScriptsFolderControl, "getSampleScriptContent", new object[] { sampleScript });
                DI.log.info("sampleScript: {0} file size {1}", sampleScript, sampleScriptContent.Length);
            }

          //  Processes.Sleep(2000); // give some time for the event to fire

            // the 1st script is seleced by default
            var scriptToLoad = sampleScriptNames[0];
            
            // get name of new control that will have the selected file (this is a dynamic name since it depends on the name of the file to load)
            var ascx_ScriptsControl = HandleO2MessageOnSD.getScriptEditorControlName(scriptToLoad);
            Assert.IsNotNull(ascx_ScriptsControl, "scriptControlName was null");
            //var ascx_ScriptsControl = (String)wcfClient.getProperty(typeof(StartCSharpScriptGui), "ascx_ScriptsName");
            //Assert.IsNotNull(ascx_ScriptsControl, "scriptControlName was null");
            // check that is was loaded                       
            Assert.That(wcfClient.o2GuiAscx.isAscxLoaded(ascx_ScriptsControl), "scriptControlName: '" + ascx_ScriptsControl + "' ascx was not loaded");
            // and that it has content (i.e. the source code was loaded
            var sourceCodeOfFileLoaded = (String)wcfClient.o2GuiAscx.invoke(ascx_ScriptsControl, "getSourceCode");
            Assert.That(false == string.IsNullOrEmpty(sourceCodeOfFileLoaded), "sourceCodeOfFileLoaded was null or empty");

            // now on the ascx_AssemblyInvokeControl get a list of classes loaded
            var typesLoadedInAssemblyInvokeControl = wcfClient.o2GuiAscx.invokeAndGetStringList(ascx_AssemblyInvokeControl, "getTypes");
            Assert.IsNotNull(typesLoadedInAssemblyInvokeControl, "typesLoadedInAssemblyInvokeControl was null");
            Assert.That(typesLoadedInAssemblyInvokeControl.Count > 0, "There were no types in typesLoadedInAssemblyInvokeControl");            
            // and now get the functions loaded 
            var methodsLoadedInAssemblyInvokeControl = wcfClient.o2GuiAscx.invokeAndGetStringList(ascx_AssemblyInvokeControl, "getMethods");
            Assert.IsNotNull(methodsLoadedInAssemblyInvokeControl, "methodsLoadedInAssemblyInvokeControl was null");
            Assert.That(methodsLoadedInAssemblyInvokeControl.Count > 0, "There were no methods in methodsLoadedInAssemblyInvokeControl");

            DI.log.info("There are {0} types Loaded In AssemblyInvoke Control", typesLoadedInAssemblyInvokeControl.Count);
            DI.log.info("There are {0} functions Loaded In AssemblyInvoke Control", methodsLoadedInAssemblyInvokeControl.Count);

            /* can't thes this since we don't have unique names for Document tabs
            // as a final test just select another file
            var anotherSampleFileToTest = sampleScriptNames[2];
            wcfClient.o2GuiAscx.invoke(ascx_ScriptsFolderControl, "selectSampleScript", new object[] { anotherSampleFileToTest });
            // check that is was loaded                       
            var anotherSampleFileToTestControl = (String)wcfClient.invoke(typeof(StartCSharpScriptGui), "getScriptEditorControlName", new object[] { anotherSampleFileToTest });
            Assert.IsNotNull(anotherSampleFileToTestControl, "scriptControlName was null");
            Assert.That(wcfClient.o2GuiAscx.isAscxLoaded(anotherSampleFileToTestControl), "scriptControlName: '" + anotherSampleFileToTestControl + "' ascx was not loaded");
            
            // manually trigger a compiple (although this shouldn't be needed since sample files are auto compile on load
            wcfClient.o2GuiAscx.clickButton(anotherSampleFileToTestControl, "btSourceCode_Compile");
             * */
        }

        [TearDown]
        public void closeGui()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            //wcfClient.o2GuiAscx.waitForAscxGuiClose();
            wcfClient.o2GuiAscx.close();
            wcfClient.close();
            wcfServer.close();  
        }
    }
}
