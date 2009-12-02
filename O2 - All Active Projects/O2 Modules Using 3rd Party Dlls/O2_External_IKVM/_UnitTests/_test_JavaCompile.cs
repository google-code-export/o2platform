using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.External.IKVM.IKVM;

namespace O2.External.IKVM._UnitTests
{
    [TestFixture]
    public class _test_JavaCompile
    {
        public string testJavaFile = Path.GetFullPath(@"_UnitTests\javaCompileTest.java");
        public string expectedCompiledTestJavaFile = Path.GetFullPath(@"_UnitTests\JavaCompileTest.class");
        public string expectedExecutionResult = "Execution OK" + Environment.NewLine; 

        [Test]
        public void test_checkIfJavaPathIsCorrectlySet()
        {
            Assert.That(IKVMInstall.checkIfJavaPathIsCorrectlySet(), "checkIfJavaPathIsCorrectlySet failed");
        }

        [Test]
        public void installIKVM()
        {
            if (Directory.Exists(IKVMConfig._IKVMRuntimeDir))
                unInstallIKVM();
            IKVMInstall.installIKVM();
            Assert.That(Directory.Exists(IKVMConfig._IKVMRuntimeDir), "Could not find Java Runtime directory: {0}",
                        IKVMConfig._IKVMRuntimeDir);
        }

        [Test]
        public void unInstallIKVM()
        {
            IKVMInstall.uninstallIKVM();
            Assert.That(false == Directory.Exists(IKVMConfig._IKVMRuntimeDir),
                        "Java Runtime directory should had been empty: {0}",
                        IKVMConfig._IKVMRuntimeDir);
        }

        
        [Test]
        public void InstallJavaOnDiferentDirectory()
        {
            var originalInstallDir = IKVMConfig._IKVMRuntimeDir;
            var newJavaDirectory = IKVMConfig.IKVMInstallationDir + "_anotherPlace";
            Assert.That(false == Directory.Exists(newJavaDirectory), "newJavaDirectory should not exist: {0}", newJavaDirectory);

            IKVMConfig.IKVMInstallationDir += "_anotherPlace";
            Assert.That(Directory.Exists(newJavaDirectory), "newJavaDirectory should exist: {0}", newJavaDirectory);
            unInstallIKVM();
            Assert.That(false == Directory.Exists(newJavaDirectory), "newJavaDirectory should not exist: {0}", newJavaDirectory);

            IKVMConfig._IKVMRuntimeDir = originalInstallDir;
        }
        
        [Test]
        public void testJavaExecution()
        {
            Assert.That(IKVMInstall.checkIKVMInstallation(), "checkIKVMInstallation return false");
            Assert.That(File.Exists(testJavaFile), "testJavaFile didn't exist:{0}", testJavaFile);                            
            
            Files.deleteFile(expectedCompiledTestJavaFile);

            JavaCompile.compileJavaFile(testJavaFile);
            Assert.That(File.Exists(expectedCompiledTestJavaFile), "expectedCompiledTestJavaFile didn't exist:{0}", expectedCompiledTestJavaFile);

            var executionResponse = JavaExec.executeJavaFile(expectedCompiledTestJavaFile);
            Assert.That(executionResponse == expectedExecutionResult,
                        "executionResponse != expectedExecutionResult: {0}, {1}", executionResponse,
                        expectedExecutionResult);
            DI.log.info(executionResponse);
            
            /*Assert.That(JavaShell.testJava("print 2+3", "5"), "testJava failed");
            Assert.That(false == JavaShell.testJava("print 2+3", "4"), "testJava should had failed");

            var scriptToExecute = "print 2+3";
            var expectedDataReceived = "5" + Environment.NewLine;
            var tempPyScript = DI.config.getTempFileInTempDirectory("py");
            Files.WriteFileContent(tempPyScript, scriptToExecute);
            
            var dataReceived = new JavaShell().executeJavaFile(tempPyScript, "");
            Assert.That(dataReceived == expectedDataReceived, "data received did no match expectedDataReceived");
            File.Delete(tempPyScript);*/
        }

        [Test]
        public void _test_createJarStubForDotNetDll()
        {
            var targetDll = Path.Combine(DI.config.CurrentExecutableDirectory, DI.config.O2KernelAssemblyName);
            Assert.That(File.Exists(targetDll));
            //var jarStubFile = JavaCompile.createJarStubForDotNetDll(targetDll,IKVMConfig.jarStubsCacheDir);
            //Assert.That(File.Exists(jarStubFile), "jarStubFile didn't exist: {0}", jarStubFile);
            var dotNetV2Dir = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727";
            var dotNetV3Dir = @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0";
            var msCorLibDll = Path.Combine(dotNetV2Dir,"mscorlib.dll");
            Assert.That(File.Exists(msCorLibDll), "msCorLibDll didn't exist: {0}", msCorLibDll);
            var msCorLibjarStubFile = JavaCompile.createJarStubForDotNetDll(msCorLibDll, IKVMConfig.jarStubsCacheDir);
            Assert.That(File.Exists(msCorLibjarStubFile), "msCorLibjarStubFile didn't exist: {0}", msCorLibjarStubFile);

            JavaCompile.createJarStubForDotNetDll(Path.Combine(dotNetV2Dir, "System.dll"), IKVMConfig.jarStubsCacheDir);
            //JavaCompile.createJarStubForDotNetDll(Path.Combine(dotNetV3Dir, "System.Core.dll"), IKVMConfig.jarStubsCacheDir);
            //JavaCompile.createJarStubForDotNetDll(Path.Combine(dotNetV3Dir, "System.ServiceModel"), IKVMConfig.jarStubsCacheDir);
            JavaCompile.createJarStubForDotNetDll(Path.Combine(dotNetV2Dir, "System.Windows.Forms"), IKVMConfig.jarStubsCacheDir);
            
        }

        [Test]
        public void _test_createJarStubForAllO2Assemblies()
        {
            var o2Timer = new O2Timer("created Jar Stubs for all O2Assemblies").start();
            var o2Assemblies = CompileEngine.getListOfO2AssembliesInExecutionDir();
            foreach(var o2Assembly in o2Assemblies)
            {
                var jarStubFile = JavaCompile.createJarStubForDotNetDll(o2Assembly, IKVMConfig.jarStubsCacheDir);
                Assert.That(File.Exists(jarStubFile), "jarStubFile didn't exist: {0}", jarStubFile);                
            }
            DI.log.info("\n...all done\n");
            o2Timer.stop();
            
        }

        /*
        [Test]
        public void testopenJavaShellOnCmdExe()
        {
            var process = JavaShell.openJavaShellOnCmdExe();
            Assert.That(process != null, "process was null");
            Assert.That(process.HasExited == false, "process.HasExited");
            Processes.Sleep(1000);
            process.Kill();
            process.WaitForExit();
            Assert.That(process.HasExited, "process should had exited by now");
        }

        */
    }
}    