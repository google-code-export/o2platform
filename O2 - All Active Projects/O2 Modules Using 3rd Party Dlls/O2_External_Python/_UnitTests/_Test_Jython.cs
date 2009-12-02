using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.Python;
using O2.External.Python.Jython;

namespace O2.External.Python._UnitTests
{
    [TestFixture]
    public class _Test_Jython
    {
        
        [Test]
        public void installJython()
        {
            if (Directory.Exists(JythonConfig._jythonRuntimeDir))
                unInstallJython();
            JythonInstall.installJython();
            Assert.That(Directory.Exists(JythonConfig._jythonRuntimeDir), "Could not find Jython Runtime directory: {0}",
                        JythonConfig._jythonRuntimeDir);
        }

        [Test]
        public void unInstallJython()
        {
            JythonInstall.uninstallJython();
            Assert.That(false == Directory.Exists(JythonConfig._jythonRuntimeDir),
                        "Jython Runtime directory should had been empty: {0}",
                        JythonConfig._jythonRuntimeDir);
        }

        [Test]
        public void InstallJythonOnDiferentDirectory()
        {
            var originalInstallDir = JythonConfig._jythonRuntimeDir;
            var newJythonDirectory = JythonConfig.JythonInstallationDir + "_anotherPlace";
            Assert.That(false == Directory.Exists(newJythonDirectory), "newJythonDirectory should not exist: {0}", newJythonDirectory);

            JythonConfig.JythonInstallationDir += "_anotherPlace";
            Assert.That(Directory.Exists(newJythonDirectory), "newJythonDirectory should exist: {0}", newJythonDirectory);
            unInstallJython();
            Assert.That(false == Directory.Exists(newJythonDirectory), "newJythonDirectory should not exist: {0}", newJythonDirectory);

            JythonConfig._jythonRuntimeDir = originalInstallDir;
        }

        [Test]
        public void testJythonScriptExecution()
        {
            Assert.That(JythonInstall.checkJythonInstallation(), "checkJythonInstallation return false");
            Assert.That(JythonShell.testJython("print 2+3", "5"), "testJython failed");
            Assert.That(false == JythonShell.testJython("print 2+3", "4"), "testJython should had failed");

            var scriptToExecute = "print 2+3";
            var expectedDataReceived = "5" + Environment.NewLine;
            var tempPyScript = DI.config.getTempFileInTempDirectory("py");
            Files.WriteFileContent(tempPyScript, scriptToExecute);
            
            var dataReceived = new JythonShell().executePythonFile(tempPyScript, "");
            Assert.That(dataReceived == expectedDataReceived, "data received did no match expectedDataReceived");
            File.Delete(tempPyScript);
        }

        [Test]
        public void testopenJythonShellOnCmdExe()
        {
            var process = JythonShell.openJythonShellOnCmdExe();
            Assert.That(process != null, "process was null");
            Assert.That(process.HasExited == false, "process.HasExited");
            Processes.Sleep(1000);
            process.Kill();
            process.WaitForExit();
            Assert.That(process.HasExited, "process should had exited by now");
        }


    }
}