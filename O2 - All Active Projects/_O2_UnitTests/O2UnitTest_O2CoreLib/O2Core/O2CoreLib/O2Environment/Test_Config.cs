// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
