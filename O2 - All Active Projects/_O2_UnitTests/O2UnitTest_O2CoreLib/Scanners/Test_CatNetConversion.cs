using System.IO;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Scanner.MsCatNet.Converter;

namespace O2.UnitTests.Test_O2CoreLib.Scanners
{
    [TestFixture]
    public class Test_CatNetConversion
    {
        private const string sCatFileToConvert = @"E:\O2\CAT.NET\HacmeBank.xml";

        private readonly string sOzasmtFileToCreate =
            Path.Combine(DI.config.O2TempDir, Path.GetFileName(sCatFileToConvert) + ".ozasmt");

        [Test]
        public void WasConversionSuccessfull()
        {
            if (File.Exists(sOzasmtFileToCreate))
                File.Delete(sOzasmtFileToCreate);
            var cnConverter = new CatNetConverter(sCatFileToConvert);
            Assert.IsTrue(cnConverter.convert(sOzasmtFileToCreate), "Converter failed");
            Assert.IsTrue(File.Exists(sOzasmtFileToCreate), "sCatFileToConvert file was not created");

            // Check if Ozasmt file is ok

            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), sOzasmtFileToCreate);
            Assert.IsTrue(o2Assessment.o2Findings.Count > 0, "There are no findings in created ozasmt file");
        }

        [Test]
        public void XmlFileLoadedOk()
        {
            Assert.IsTrue(File.Exists(sCatFileToConvert), "Test File To Convert doesn't exist!");
            var cnConverter = new CatNetConverter(sCatFileToConvert);
            Assert.IsNotNull(cnConverter.catNetXml, "Xml Document Object was null");
        }
    }
}