// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using NUnit.Framework;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel.Interfaces.CIR;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.UnitTests.Test_O2CoreCIR.Test_Ascx
{
    [TestFixture]
    public class Test_SavingAndLoadingCirDataFiles
    {
        readonly CirFactory cirFactory = new CirFactory();
               
        [Test]
        public void test_SaveCirDataFile()
        {
            ICirData cirData = new CirData();
            cirFactory.processAssemblyDefinition(cirData, DI.config.ExecutingAssembly);
            var savedCirDataFile = DI.config.getTempFileInTempDirectory("CirData");
            Assert.That(false   == File.Exists(savedCirDataFile), "savedCirDataFile shouldn't exist here");
            CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, savedCirDataFile);
            Assert.That(File.Exists(savedCirDataFile), "savedCirDataFile exist here");
            File.Delete(savedCirDataFile);
            Assert.That(false == File.Exists(savedCirDataFile), "savedCirDataFile Should be deleted");
            DI.log.info("all done");
        }


        [Test, 
         Ignore ("Run manually since this test will create a CirDump file for all O2 releated assemblies")]
        public void test_SaveAndLoadAllO2Modules()
        {
            var directoryToSaveCidDataFiles = Path.Combine(DI.config.O2TempDir, "_O2_CirData_Files");
            Files.checkIfDirectoryExistsAndCreateIfNot(directoryToSaveCidDataFiles);
            List<string> targetAssemblies = Files.getFilesFromDir_returnFullPath(DI.config.hardCodedO2LocalBuildDir, "*.exe");
            targetAssemblies.AddRange(Files.getFilesFromDir_returnFullPath(DI.config.hardCodedO2LocalBuildDir, "*.dll"));
            foreach (var assemblyToProcess in targetAssemblies)
            {
                DI.log.info("Processing file: {0}", Path.GetFileName(assemblyToProcess));
                ICirData cirData = new CirData();
                cirFactory.processAssemblyDefinition(cirData, assemblyToProcess);
                Assert.That(cirData.dClasses_bySignature.Count > 0 && cirData.dFunctions_bySignature.Count > 0,
                            "There we no CirData results for :" + assemblyToProcess);
                var savedCirDataFile = Path.Combine(directoryToSaveCidDataFiles,
                                                    Path.GetFileName(assemblyToProcess) + ".CirData");
                CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, savedCirDataFile);
                Assert.That(File.Exists(savedCirDataFile), "Saved CirData file Didn't exist: " + savedCirDataFile);

                ICirData cirData2 = CirLoad.loadFile(savedCirDataFile);
                Assert.That(cirData2 != null, "cirData2 was null");
                Assert.That(cirData.dClasses_bySignature.Count == cirData2.dClasses_bySignature.Count,
                            "dClasses_bySignature Count didnt match");
                Assert.That(cirData.dFunctions_bySignature.Count == cirData2.dFunctions_bySignature.Count,
                            "dFunctions_bySignature Count didnt match");
                // comment this to delete created files 
                //File.Delete(savedCirDataFile);                
            }
        }

        [Test, 
         Ignore("Run manually since this takes quite a bit of time :) (specially the save of all cirDataWithAllAssemblies which creates a 250m CirData file) ")]
        public void test_CreateCirDataFilesForDotNetFramework()
        {
            var dotNetFrameworkDirectory = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727";
            string directoryToSaveCidDataFiles = Path.Combine(DI.config.O2TempDir, "_CirDataFilesFor_DotNetFramework2_0_50727");
            Files.checkIfDirectoryExistsAndCreateIfNot(directoryToSaveCidDataFiles);    
            List<string> targetAssemblies = Files.getFilesFromDir_returnFullPath(dotNetFrameworkDirectory, "*.dll");
            //targetAssemblies.AddRange(Files.getFilesFromDir_returnFullPath(DI.hardCodedO2DeploymentDir, "*.dll"));
            ICirData cirDataWithAllAssemblies = new CirData();
            foreach (var assemblyToProcess in targetAssemblies)
            {
                if (CecilUtils.isDotNetAssembly(assemblyToProcess))
                {
                    DI.log.info("Processing file: {0}", Path.GetFileName(assemblyToProcess));
                    ICirData cirData = new CirData();
                    cirFactory.processAssemblyDefinition(cirData, assemblyToProcess);
                    cirFactory.processAssemblyDefinition(cirDataWithAllAssemblies, assemblyToProcess);
                    var savedCirDataFile = Path.Combine(directoryToSaveCidDataFiles,
                                                        Path.GetFileName(assemblyToProcess) + ".CirData");
                    CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, savedCirDataFile);
                }
            }
            DI.log.info("Almost there, now saving cirDataWithAllAssemblies ");
            var fileWithCirDataWithAllAssemblies = Path.Combine(directoryToSaveCidDataFiles, "cirDataWithAllAssemblies.CirData");
            CirDataUtils.saveSerializedO2CirDataObjectToFile(cirDataWithAllAssemblies, fileWithCirDataWithAllAssemblies);                
            DI.log.info("Done ..");
        }
         
    }
}
