// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.Rnd.AspectDngHook;


namespace O2.RnD.AspectTests.UnitTests
{
    /// <summary>
    ///This is a test class for utilsTest and is intended
    ///to contain all utilsTest Unit Tests
    ///</summary>
    [TestFixture]
    public class AspectDngUtilsTest
    {        
        [Test]
        public void injectHooksTest_onO2Module()
        {
            String sO2ModuleToTest = @"E:\O2\_Bin_(O2_Binaries)\O2_Tool_CSharpScripts.exe";

            String sTargetFolder = Path.Combine(DI.config.O2TempDir,
                                                Path.GetFileNameWithoutExtension(sO2ModuleToTest));
            if (Directory.Exists(sTargetFolder))
                Files.deleteFolder(sTargetFolder, true);
            Files.copyFilesFromDirectoryToDirectory(Path.GetDirectoryName(sO2ModuleToTest), sTargetFolder);

            String sO2ModuleToTestInTempDirectory = Path.Combine(sTargetFolder, Path.GetFileName(sO2ModuleToTest));

            // move required dlls
            String sAspectDngExe = DngConfig.extractAspectDngExeToTempFolder();
            String sHooksDll = DngConfig.copyCurrentDllToTempFolder();

            File.Copy(sAspectDngExe, Path.Combine(sTargetFolder, Path.GetFileName(sAspectDngExe)));
            File.Copy(sHooksDll, Path.Combine(sTargetFolder, Path.GetFileName(sHooksDll)));

            //dlls to hook     
            if (true)
            {
                var lsDllsToHook = new List<String>();
                lsDllsToHook.Add("O2_Kernel.dll");
                lsDllsToHook.Add("O2_Views_ASCX.dll");

                foreach (String sDll in lsDllsToHook)
                {
                    String sDllToHook = Path.Combine(sTargetFolder, sDll);
                    Assert.IsTrue(File.Exists(sDllToHook), "Copy failed for : " + sDllToHook);
                    String sResult = DngUtils.injectHooks(sDllToHook, "* System.IO.*", "*");
                }
            }
            Debug.WriteLine("sO2ModuleToTestInTempDirectory");
            Debug.WriteLine(sO2ModuleToTestInTempDirectory);
            Assert.IsTrue(sO2ModuleToTestInTempDirectory.IndexOf(".exe") > -1, "no exe to execute");
            Processes.startProcess(sO2ModuleToTestInTempDirectory, "");

            // Assert.IsTrue(File.Exists(sO2ModuleToTestInTempDirectory), "Hooked Does not exist: {0}", sO2ModuleToTestInTempDirectory);

            /*string sTargetAssembly = (String)SpringCore.createTypeAndInvokeMethod(typeof(o2.MonoCecil.CreateTestExes), "createBasicHelloWorldExe");
            Assert.IsTrue(File.Exists(sTargetAssembly), "Target Assembly Does not exist: {0}", sTargetAssembly);

            String sResult = DngUtils.injectHooks(sTargetAssembly);

            Assert.IsTrue(File.Exists(sTargetAssembly), "New Assembly Does not exist: {0}", sTargetAssembly);
             */
        }

        [Test]
        public void injectHooksTest()
        {
            String sHooksDll = DngConfig.copyCurrentDllToTempFolder();
            Assert.IsTrue("" != CecilUtils.getAttributeValueFromAssembly(sHooksDll, "AroundBody", 0).ToString(),
                          "Finding AroundBody");
            String sNewParameterValue = "*AAAAA*";
            Assert.IsTrue(CecilUtils.setAttributeValueFromAssembly(sHooksDll, "AroundBody", 0, sNewParameterValue),
                          "setting attribute");
            Assert.IsTrue(
                sNewParameterValue == CecilUtils.getAttributeValueFromAssembly(sHooksDll, "AroundBody", 0).ToString(),
                "Checking for persistance of changed data");

            var sTargetAssembly = new CreateTestExe().createBasicHelloWorldExe().save();
                //SpringExec.createTypeAndInvokeMethod(typeof (CreateTestExesTest), "createBasicHelloWorldExe");

            String sHookInjectionResult = DngUtils.injectHooks(sTargetAssembly, "*", "*");
            String sProcessExecutionResult = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(sTargetAssembly, "");


            //Assert.IsTrue(File.Exists(sTargetAssembly), "New Assembly Does not exist: {0}", sTargetAssembly);
            DI.log.info("Test Completed");
        }


        [Test]
        public void AspectDNG_CheckIfHooksDllContainsAndAspectDngAspect()
        {
            String sHooksDll = DngConfig.copyCurrentDllToTempFolder();
            Assert.IsTrue(File.Exists(sHooksDll), "AspectDng.exe doesn't exist: {0}", sHooksDll);

            bool bContainsAspect =
                CecilCodeSearch.findInAssembly_CustomAttribute(sHooksDll, "AroundBody") ||
                CecilCodeSearch.findInAssembly_CustomAttribute(sHooksDll, "AroundCall");

            Assert.IsTrue(bContainsAspect, "Hook Dll did not contain an AspectDng aspect");
        }

        [Test]
        public void AspectDNG_CheckIfAspectDngExeIsThereAndIsWorkingAsExpected()
        {
            String sAspectDngExe = DngConfig.extractAspectDngExeToTempFolder();
            Assert.IsTrue(File.Exists(sAspectDngExe), "AspectDng.exe doesn't exist: {0}", sAspectDngExe);

            String sCecilDll = DngConfig.extractCecilDllToTempFolder();
            Assert.IsTrue(File.Exists(sCecilDll), "Cecil.dll doesn't exist: {0}", sCecilDll);

            String sExpectedConsoleOutput = "AspectDNG Copyright (C) 2005 Thomas GIL (DotNetGuru SARL).";
            String sConsoleOutput = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(sAspectDngExe, "");
            Assert.IsTrue(sConsoleOutput != "", "AspectDng execution didn't return any data");
            bool bFoundExpectedContent = (sConsoleOutput.IndexOf(sExpectedConsoleOutput) > -1);
            Assert.IsTrue(bFoundExpectedContent, "Expected default content not found on AspectDng execution");
        }
    }
}
