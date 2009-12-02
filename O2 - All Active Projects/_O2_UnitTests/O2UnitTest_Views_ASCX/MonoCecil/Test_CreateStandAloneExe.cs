using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using System.IO;
using O2.DotNetWrappers.NetSDK;

namespace O2.UnitTests.Test_O2ViewsASCX.MonoCecil
{
    public class TestClass
    {
        public static void TestMethod()
        {
            Console.WriteLine("This is a testMethod From TestClass");
        }
    }

    [TestFixture]
    public class Test_CreateStandAloneExe
    {
        [Test]
        public void test_ExeCreation_automatic()
        {
            var targetMethod = DI.reflection.getMethod(typeof(TestClass), "TestMethod", null);
            Assert.That(targetMethod != null, "targetMethod was null");
            var exeInPackagedDirectory = StandAloneExe.createPackagedDirectoryForMethod(targetMethod);
            Assert.That(File.Exists(exeInPackagedDirectory), "exeInPackagedDirectory was not created");

            DI.log.info("New exe and pdb are on directory {0}", Path.GetDirectoryName(exeInPackagedDirectory));
        }

        [Test]
        public void test_ExeCreation_manual()
        {
            var targetMethod = DI.reflection.getMethod(typeof (TestClass), "TestMethod",null);
            Assert.That(targetMethod != null, "targetMethod was null");

            var standAloneExe = StandAloneExe.createMainPointingToMethodInfo(targetMethod);

            Assert.That(File.Exists(standAloneExe), "standAloneExe file didn't exist: " + standAloneExe);

            var createdDirectory = CecilAssemblyDependencies.populateDirectoryWithAllDependenciesOfAssembly(standAloneExe);
            Assert.That(Directory.Exists(createdDirectory), "createdDirectory didn't exist");
            var exeInCreatedDirectory = Path.Combine(createdDirectory, Path.GetFileName(standAloneExe));
            Assert.That(File.Exists(exeInCreatedDirectory), "exeInCreatedDirectory diddnt exit");
                    
            var newDirectoryName = DI.config.getTempFolderInTempDirectory(targetMethod.Name);
            Files.copyFilesFromDirectoryToDirectory(createdDirectory, newDirectoryName);
            Files.deleteFolder(createdDirectory);

            var exeInFinalDirectory = Path.Combine(newDirectoryName, Path.GetFileName(standAloneExe));
            var pdbFile = IL.createPdb(exeInFinalDirectory, false);
            Assert.That(File.Exists(pdbFile), "pdbFile was not created");

            DI.log.info("New exe and pdb are on directory {0}", newDirectoryName);
        }
    }
}
