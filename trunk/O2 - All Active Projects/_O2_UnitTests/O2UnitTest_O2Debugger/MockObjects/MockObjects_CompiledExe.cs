// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.External.SharpDevelop.ScriptSamples;
using O2.UnitTests.Test_O2Debugger;
using O2.Views.ASCX.SourceCodeEdit;

namespace O2.UnitTests.Test_O2Debugger.MockObjects
{
    [TestFixture]
    public class MockObjects_CompiledExe
    {
        #region Setup/Teardown

        [TearDown]
        public void killProcess()
        {
            try
            {
                Process processToKill = Processes.getProcessCalled(compiledAssemblySettings.testProcessName);
                processToKill.Kill();
                processToKill.WaitForExit();
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "on killProcess");
            }
            //Processes.killProcess(compiledAssemblySettings.testProcessName);
            Files.deleteFilesFromDirThatMatchPattern(
                Path.GetDirectoryName(compiledAssemblySettings.pathToCreatedAssemblyFile),
                Path.GetFileNameWithoutExtension(compiledAssemblySettings.pathToCreatedAssemblyFile) + ".*");
        }

        #endregion
       
        public static bool dontCompileIfTargetAssemblyFileAlreadyExists;


        public CompiledAssemblySettings compiledAssemblySettings;

        // use for Unit tests
        public MockObjects_CompiledExe()
        {
            compiledAssemblySettings = for_UnitTest_HotMethodPatch();
        }

        public MockObjects_CompiledExe(CompiledAssemblySettings _compiledAssemblySettings)
            : this(_compiledAssemblySettings, true)
        {
        }

        public MockObjects_CompiledExe(CompiledAssemblySettings _compiledAssemblySettings, bool startProcess)
        {
            compiledAssemblySettings = _compiledAssemblySettings;            
            if (startProcess)
                startTestProcess();
            else
            {
                compileTestFileAndCheckIfAllIsStillThere();
            }
        }

        public Process TestProcess
        {
            get { return compiledAssemblySettings.testProcess; }
        }

        public string PathToCreatedAssemblyFile
        {
            get { return compiledAssemblySettings.pathToCreatedAssemblyFile; }
        }

        public static CompiledAssemblySettings with_HelloWorldSample()
        {
            return new CompiledAssemblySettings(
                O2SampleScripts._1_helloWorld,
                "HelloWorld",
                "O2.Views.ASCX.SourceCodeEdit.ScriptSamples.HelloWorlds",
                dontCompileIfTargetAssemblyFileAlreadyExists,
                (pathToCreatedAssemblyFile) => true);
        }

        public static CompiledAssemblySettings for_UnitTest_SimpleCalls()
        {
            return new CompiledAssemblySettings(
                O2SampleScripts._4_For_UnitTest_SimpleCalls,
                "For_UnitTest_SimpleCalls",
                "O2.Views.ASCX.SourceCodeEdit.ScriptSamples.For_UnitTest_SimpleCalls",
                dontCompileIfTargetAssemblyFileAlreadyExists,
                (pathToCreatedAssemblyFile) => true);
        }

        public static CompiledAssemblySettings for_UnitTest_HotMethodPatch()
        {
            return new CompiledAssemblySettings(
                O2SampleScripts._2_For_UnitTest_HotMethodPatch,
                "For_UnitTest_HotMethodPatch",
                "O2.Views.ASCX.SourceCodeEdit.ScriptSamples.For_UnitTest_HotMethodPatch",
                dontCompileIfTargetAssemblyFileAlreadyExists,
                (pathToCreatedAssemblyFile) =>
                    {
                        List<MethodInfo> methodsInAssembly = DI.reflection.getMethods(pathToCreatedAssemblyFile);
                        //DebugMsg.showListContents(Reflection.getMethods(testAssemblyFile));                        
                        Assert.That(methodsInAssembly[0].ToString() == "Void Main()",
                                    "prob in method 0: " + methodsInAssembly[0]);
                        Assert.That(methodsInAssembly[1].ToString() == "Void methodRunningInThreadA()",
                                    "prob in method 1: " + methodsInAssembly[1]);
                        Assert.That(methodsInAssembly[2].ToString() == "Void methodRunningInThreadB()",
                                    "prob in method 2: " + methodsInAssembly[2]);
                        Assert.That(methodsInAssembly[3].ToString() == "System.String messageForThreadB()",
                                    "prob in method 3: " + methodsInAssembly[3]);
                        return true;
                    });
        }

        public class CompiledAssemblySettings
        {
            // compile vars
            private readonly Func<string, bool> compilationCreationTests;
            public bool dontCompileIfFileExists;
            public string exeMainClass;
            public string outputAssemblyName;
            // post compile vars
            public string pathToCreatedAssemblyFile;
            public string sourceCode;
            // post execution cars
            public Process testProcess;
            public string testProcessName;

            public CompiledAssemblySettings(string _sourceCode, string _outputAssemblyName,
                                            string _exeMainClass, bool _dontCompileIfFileExists,
                                            Func<string, bool> _compilationCreationTests)
            {
                sourceCode = _sourceCode;
                outputAssemblyName = _outputAssemblyName;
                dontCompileIfFileExists = _dontCompileIfFileExists;
                exeMainClass = _exeMainClass;
                pathToCreatedAssemblyFile = Path.Combine(DI.config.O2TempDir,
                                                         outputAssemblyName + ((exeMainClass == "") ? ".dll" : ".exe"));
                compilationCreationTests = _compilationCreationTests;
            }

            public bool checkIfAssemblyWasCreatedOK()
            {
                return compilationCreationTests(pathToCreatedAssemblyFile);
            }
        }


        internal void WaitForExit()
        {
            compiledAssemblySettings.testProcess.WaitForExit();
        }

        [Test]
        public void compileTestFileAndCheckIfAllIsStillThere()
        {
            if (false == compiledAssemblySettings.dontCompileIfFileExists ||
                false == File.Exists(compiledAssemblySettings.pathToCreatedAssemblyFile))
            {
                Assembly compiledExeFile = new CompileEngine().compileSourceCode(compiledAssemblySettings.sourceCode,
                                                                     compiledAssemblySettings.exeMainClass,
                                                                     compiledAssemblySettings.outputAssemblyName);
                // copy all dependentDlls to current directly (so that we can load this exe)

                Assert.That(compiledExeFile != null, "in MockObjects_CompiledExe.compileTestFileAndCheckIfAllIsStillThere Compiled Assembly was null");
                CecilAssemblyDependencies.copyAssemblyDependenciesToAssemblyDirectory(compiledExeFile.Location,
                                                                                      new List<string>
                                                                                          {
                                                                                              DI.config.
                                                                                                  hardCodedO2LocalBuildDir
                                                                                          });

                Assert.That(compiledExeFile != null, "Compilation failed");
                Assert.That(File.Exists(compiledExeFile.Location), "Could not find assembly on disk!");
                DI.log.info("test Assembly file compiled into: {0}", compiledExeFile);
                compiledAssemblySettings.pathToCreatedAssemblyFile = compiledExeFile.Location;
                Assert.That(compiledAssemblySettings.checkIfAssemblyWasCreatedOK(), "Created assembly was not OK");
            }
        }

        [Test]
        public void startTestProcess()
        {
            if (false == File.Exists(compiledAssemblySettings.pathToCreatedAssemblyFile))
                compileTestFileAndCheckIfAllIsStillThere();
            Assert.That(File.Exists(compiledAssemblySettings.pathToCreatedAssemblyFile),
                        "Could not find assembly on disk!");
            DI.log.i("File is ok: " + compiledAssemblySettings.pathToCreatedAssemblyFile);
            compiledAssemblySettings.testProcess =
                Processes.startProcess(compiledAssemblySettings.pathToCreatedAssemblyFile);
            compiledAssemblySettings.testProcessName = compiledAssemblySettings.testProcess.ProcessName;
            Assert.That(compiledAssemblySettings.testProcess != null, "testProcess was null");
        }
    }
}
