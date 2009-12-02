using System.IO;
using NUnit.Framework;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.O2Environment
{
    [TestFixture]
    public class Test_Config
    {
        [Test]
        public void test_DefaultValues()
        {
            Assert.That(!string.IsNullOrEmpty(DI.config.O2TempDir), "Config.O2TempDir was null or empty");
            Assert.That(Directory.Exists(DI.config.O2TempDir), "Config.O2TempDir doesn't exist " + DI.config.O2TempDir);
        }
    }
}