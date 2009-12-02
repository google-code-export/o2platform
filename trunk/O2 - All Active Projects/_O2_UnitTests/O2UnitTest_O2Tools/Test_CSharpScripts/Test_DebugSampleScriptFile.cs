// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Debugger.Mdbg.O2Debugger;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.Ascx;
using O2.External.O2Mono.MonoCecil;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Kernel;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.Objects;
using O2.Views.ASCX.SourceCodeEdit;

namespace O2.UnitTests.Test_O2Tools.Test_CSharpScripts
{
    [TestFixture]
    public class Test_DebugSampleScriptFile_DirectInvocation   // after this works, write the same test via Wcf
    {
        private const string ascx_ScriptControl = "Script to Debug";
        private const string ascx_ScriptsFolderControl = "Scripts Folder";
        private const string ascx_AssemblyInvokeControl = "Dynamically Invoke methods from Compiled Assembly";
        private const string ascx_O2MdbgShellName = "O2 .Net Debuggger";

        private int currentSelectedLine = 10;

        private string assemblyToExecute;

        [SetUp]
        public void loadTestGui()
        {
            // lauch Gui
            O2AscxGUI.launch();
            DI.log.LogRedirectionTarget = null; // so that we get all DI.log messages
            O2AscxGUI.setLogViewerDockState(O2DockState.DockBottom);
            // load the controls we need
            O2AscxGUI.openAscx(typeof(ascx_Scripts), O2DockState.Document, ascx_ScriptControl);
            O2AscxGUI.openAscx(typeof(ascx_ScriptsFolder), O2DockState.DockLeft, ascx_ScriptsFolderControl);
            O2AscxGUI.openAscx(typeof(ascx_AssemblyInvoke), O2DockState.DockRight, ascx_AssemblyInvokeControl);
            O2AscxGUI.openAscx(typeof(ascx_O2MdbgShell), O2DockState.DockBottom, ascx_O2MdbgShellName);
            // make sure they are loaded
            Assert.That(O2AscxGUI.isAscxLoaded(ascx_ScriptControl), "ascxScriptControl was not loaded");
            Assert.That(O2AscxGUI.isAscxLoaded(ascx_ScriptsFolderControl), "ascxScriptFolderControl was not loaded");
            Assert.That(O2AscxGUI.isAscxLoaded(ascx_AssemblyInvokeControl), "ascx_AssemblyInvokeControl was not loaded");
            Assert.That(O2AscxGUI.isAscxLoaded(ascx_O2MdbgShellName), "ascx_AssemblyInvokeControl was not loaded");

            // load sample script in scripts folder
            ascx_ScriptsFolderControl.invokeOnAscx("loadSampleScripts");            
            var sampleScriptNames = O2AscxGUI.invokeAndGetStringList(ascx_ScriptsFolderControl, "getSampleScriptNames");
            Assert.That(sampleScriptNames.Count > 2, "There should be at least 3 scripts");
            var targetSampleScript = sampleScriptNames[0];
            var targetSampleScriptContents = (string)O2AscxGUI.invokeOnAscxControl(ascx_ScriptsFolderControl, "getSampleScriptContent", new object[] { targetSampleScript });
            var scriptFile = (string)O2AscxGUI.invokeOnAscxControl(ascx_ScriptsFolderControl, "createTempScriptFile", new object[] { Path.Combine(DI.config.O2TempDir, targetSampleScript), targetSampleScriptContents });
            Assert.IsNotNull(scriptFile, "scriptFile was null");
            Assert.That(File.Exists(scriptFile), "scriptFile didn't exist");
            O2AscxGUI.invokeOnAscxControl(ascx_ScriptControl, "loadSourceCodeFile", new object[] { scriptFile });

            // and confirm that its contents are the same as the content retrieved from the ascx_ScriptsFolderControl
            var scriptContentsFromAscxScriptsControl = (string)O2AscxGUI.invokeOnAscxControl(ascx_ScriptControl, "getSourceCode");
            Assert.IsNotNull(scriptContentsFromAscxScriptsControl, "scriptContentsFromAscxScriptsControl was null");
            Assert.That(scriptContentsFromAscxScriptsControl == targetSampleScriptContents, "Source code didn't match");

            // let's wait a bit so that the compile event can trigger
            Processes.Sleep(1000);

            // the above load should have triggered the auto compile of the source code file, so lets get its type
            var compiledTypes = O2AscxGUI.invokeAndGetStringList(ascx_AssemblyInvokeControl, "getTypes");
            Assert.That(compiledTypes != null && compiledTypes.Count > 0, "prob with types");

            // now that we have the type we need to use it to configure the exe settings (since we cant debug a Dll
            var typeName = compiledTypes[0];

            DI.log.info("There were {0} methods compiled", compiledTypes.Count);

            // final test is to make sure the assembly file to execute exists
            // first configure the exe compilation mode (which needs the name of the type that holds the Main method
            O2AscxGUI.invokeOnAscxControl(ascx_ScriptControl, "setExeCompilationMode", new object[] { typeName });

            // then simulate a compile by clicking on the compile button
            O2AscxGUI.clickButton(ascx_ScriptControl, "btSourceCode_Compile");
            // let's wait a bit so that the compile event can trigger
            Processes.Sleep(1000);

            // now get the compile assembly name which should be an *.exe
            assemblyToExecute = (string)O2AscxGUI.invokeOnAscxControl(ascx_AssemblyInvokeControl, "getAssemblyLocation");
            DI.log.info("The assembly to debug is: {0}", assemblyToExecute);
            Assert.That(assemblyToExecute != null && File.Exists(assemblyToExecute) && Path.GetExtension(assemblyToExecute) == ".exe", "prob with assemblyToExecute");
            // and make sure all supporting dlls are in temp folder (namely O2_Kernel.dll
            makeSureAllDependentAssembliesExistInTempDirectory(assemblyToExecute);

            //so that it is null, select a line of the document
            setSelectedLineNumber(scriptFile, currentSelectedLine);
            Assert.That(currentSelectedLine == getSelectedLineNumber(), "currentSelectedLine didn't match");

        }

        public void makeSureAllDependentAssembliesExistInTempDirectory(string targetAssembly)
        {
            CecilAssemblyDependencies.copyAssemblyDependenciesToAssemblyDirectory(targetAssembly);
        }

        public void setSelectedLineNumber(string filename, int lineNumber)
        {
            O2AscxGUI.invokeOnAscxControl(ascx_ScriptControl, "setSelectedLineNumber", new object[] { filename, lineNumber });
        }

        public int getSelectedLineNumber()
        {
            return (int)O2AscxGUI.invokeOnAscxControl(ascx_ScriptControl, "getSelectedLineNumber");
        }

        [Test]
        public void test_startSampleFileUnderDebuggerAndStepIntoIt()
        {
            // before we start the process we need to set the callback for global O2Messages (so that we pick up the OnBreakpoint message)
            PublicDI.o2MessageQueue.onMessages += o2Message =>
            {
                DI.log.info("message received: {0}", o2Message.messageText);
                if (o2Message is IM_O2MdbgAction)
                {
                    var o2MDbgAction = (IM_O2MdbgAction)o2Message;
                    if (o2MDbgAction.o2MdbgAction == IM_O2MdbgActions.breakEvent)
                    {
                        var filename = o2MDbgAction.filename;
                        var line = o2MDbgAction.line;
                        DI.log.info("SOURCECODE REF -> {0} : {1})", line, filename);
                        setSelectedLineNumber(filename, line);
                    }
                }
            };

            // start process under debuggger
            var startThread = O2MDbgUtils.startProcessUnderDebugger(assemblyToExecute);
            // wait for it completes execution
            startThread.Join();
            Assert.That(O2MDbgUtils.IsActive(), "Debugger should be active");
            Assert.That(false == O2MDbgUtils.IsRunning(), "Debugger should Not be running");

            var selectedLine = getSelectedLineNumber();

            O2MDbgUtils.stepIntoAnimated();

            /*      Assert.That(selectedLine != currentSelectedLine,
                              "After debugger started the selectedLine != currentSelectedLine: " + selectedLine + " != " +
                              currentSelectedLine);*/



            //    Processes.Sleep(3000);
            //O2AscxGUI.invokeOnAscxControl(ascxScriptControl,)
            O2AscxGUI.logInfo("all ready");
        }


        [TearDown]
        public void closeGui()
        {
            O2AscxGUI.close();
            //O2AscxGUI.waitForAscxGuiClose();
        }
    }
}
