// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.UnitTests.Test_Ozasmt.Test_Ozasmt
{
    [TestFixture]
    public class Test_OzasmtNew
    {
        public IO2AssessmentLoad o2AssessmentLoad = new O2AssessmentLoad_OunceV6();
        public IO2AssessmentSave o2AssessmentSave = new O2AssessmentSave_OunceV6();
        
        private const string testOzasmtFile =
            @"E:\O2\_UnitTestSupportFiles\Website (with all callers) ._ALLTRACES.ozasmt";

        private readonly string pathToSaveFilesCreated = Path.Combine(DI.config.O2TempDir,
                                                                      "_Test_OzasmtNew");

        [Test]
        public void createOneO2AssessmentPerUniqueTracesSignature()
        {
            Assert.IsTrue(File.Exists(testOzasmtFile), "testOzasmtFile didn't exist");
            Directory.CreateDirectory(pathToSaveFilesCreated);
            Assert.IsTrue(Directory.Exists(pathToSaveFilesCreated), "pathToSaveFilesCreated could not be created");
            Files.deleteFilesFromDirThatMatchPattern(pathToSaveFilesCreated, "*.ozasmt");


            Dictionary<String, List<IO2Trace>> allTraces = OzasmtUtils.getDictionaryWithO2AllSubTraces(o2AssessmentLoad,testOzasmtFile);
            DI.log.info("there are {0} traces", allTraces.Count);
            DI.log.info("going to save traces here : {0}", pathToSaveFilesCreated);
            DI.log.info("before there are {0} *.ozasnt files ",
                        Files.getFilesFromDir(pathToSaveFilesCreated, "*.ozasmt").Count);

            foreach (string signature in allTraces.Keys)
            {
                //var fileName = (signature != "") ? signature : "_EmptySignature";
                string fileName = signature.Replace(':', '_').Replace('<', '_').Replace('>', '_');
                o2AssessmentSave = new O2AssessmentSave_OunceV6();
                OzasmtNew.createO2AssessmentFromTraces(o2AssessmentSave,Path.Combine(pathToSaveFilesCreated, fileName + ".ozasmt"),
                                                       allTraces[signature]);
            }
            DI.log.info("after build there are {0} *.ozasnt files ",
                        Files.getFilesFromDir(pathToSaveFilesCreated, "*.ozasmt").Count);
            // for now delete all files so that we don't keep them on disk
            Files.deleteFilesFromDirThatMatchPattern(pathToSaveFilesCreated, "*.ozasmt");
            DI.log.info("after delete there are {0} *.ozasnt files ",
                        Files.getFilesFromDir(pathToSaveFilesCreated, "*.ozasmt").Count);
            Files.deleteFolder(pathToSaveFilesCreated);
            Assert.That(!Directory.Exists(pathToSaveFilesCreated), " pathToSaveFilesCreated still exists");
            //log.info("going to save traces here : {0}", pathToSaveFilesCreated);
            Assert.Ignore("Todo: reload all files again , " +
                          "Compress them into 1 file , " +
                          "Compare with original file and see if they match");
        }
    }
}
