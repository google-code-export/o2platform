using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.Python.IronPython;

namespace O2.External.Python._UnitTests
{
    [TestFixture]
    public class _Test_IronPython
    {
        [Test]
        public void installIronPython()
        {
            if (Directory.Exists(IronPythonConfig._IronPythonRuntimeDir))
                unInstallIronPython();
            IronPythonInstall.installIronPython();
            Assert.That(Directory.Exists(IronPythonConfig._IronPythonRuntimeDir), "Could not find IronPython Runtime directory: {0}",
                        IronPythonConfig._IronPythonRuntimeDir);
        }

        [Test]
        public void unInstallIronPython()
        {
            IronPythonInstall.uninstallIronPython();
            Assert.That(false == Directory.Exists(IronPythonConfig._IronPythonRuntimeDir),
                        "IronPython Runtime directory should had been empty: {0}",
                        IronPythonConfig._IronPythonRuntimeDir);
        }

        [Test]
        public void InstallIronPythonOnDiferentDirectory()
        {
            var originalInstallDir = IronPythonConfig._IronPythonRuntimeDir;
            var newIronPythonDirectory = IronPythonConfig.IronPythonInstallationDir + "_anotherPlace";
            Assert.That(false == Directory.Exists(newIronPythonDirectory), "newIronPythonDirectory should not exist: {0}", newIronPythonDirectory);

            IronPythonConfig.IronPythonInstallationDir += "_anotherPlace";
            Assert.That(Directory.Exists(newIronPythonDirectory), "newIronPythonDirectory should exist: {0}", newIronPythonDirectory);
            unInstallIronPython();
            Assert.That(false == Directory.Exists(newIronPythonDirectory), "newIronPythonDirectory should not exist: {0}", newIronPythonDirectory);

            IronPythonConfig._IronPythonRuntimeDir = originalInstallDir;
        }

        [Test]
        public void testIronPythonScriptExecution()
        {
            var expectedDataReceived = "5" + Environment.NewLine;
            Assert.That(IronPythonInstall.checkIronPythonInstallation(), "checkIronPythonInstallation return false");
            Assert.That(IronPythonShell.testIronPython("print 2+3", expectedDataReceived), "testIronPython failed");
            Assert.That(false == IronPythonShell.testIronPython("print 2+3", "4"), "testIronPython should had failed");
            
            var scriptToExecute = "print 2+3";
            
            var tempPyScript = DI.config.getTempFileInTempDirectory("py");
            Files.WriteFileContent(tempPyScript, scriptToExecute);

            var dataReceived = new IronPythonShell().executePythonFile(tempPyScript, "");
            Assert.That(dataReceived == expectedDataReceived, "data received did no match expectedDataReceived");
            File.Delete(tempPyScript);
        }

        [Test]
        public void testopenIronPythonShellOnCmdExe()
        {
            var process = IronPythonShell.openIronPythonShellOnCmdExe();
            Assert.That(process != null, "process was null");
            Assert.That(process.HasExited == false, "process.HasExited");
            Processes.Sleep(1000);
            process.Kill();
            process.WaitForExit();
            Assert.That(process.HasExited, "process should had exited by now");
        }
    }
}