using System.IO;
using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.CecilDecompiler;
using O2.External.O2Mono.MonoCecil;
using O2.UnitTests.Test_O2ViewsASCX;

namespace O2.UnitTests.Test_O2ViewsASCX.MonoCecil
{
    [TestFixture]
    public class Test_CecilDecompiler
    {
        [Test]
        public void decompileMainModule()
        {
            string sourceCodeFile = DI.config.TempFileNameInTempDirectory + ".cs";
            string testExe = new CreateTestExe().createBasicHelloWorldExe().save();

            Assert.IsTrue(File.Exists(testExe), "Test script was not created");
            string sourceCode = new CecilDecompiler().getSourceCode(testExe);

            Assert.IsNotEmpty(sourceCode, "main module source code was empty");
            Files.WriteFileContent(sourceCodeFile, sourceCode);
            Assert.IsTrue(File.Exists(sourceCodeFile), "Source code was not created");
        }
    }
}