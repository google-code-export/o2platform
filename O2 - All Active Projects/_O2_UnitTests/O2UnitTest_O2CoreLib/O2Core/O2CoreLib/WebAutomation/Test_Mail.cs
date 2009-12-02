using NUnit.Framework;
using O2.DotNetWrappers.Network;

namespace O2.UnitTests.Test_O2CoreLib.O2Core.O2CoreLib.WebAutomation
{
    [TestFixture]
    public class Test_Mail
    {
        private readonly string mailServer = "mail.ouncelabs.com"; // DI.config.sEmailHost; //  "mail.ouncelabs.com";

        [Test]
        public void test_isMailServerOnline()
        {            
            Assert.That(Mail.isMailServerOnline(mailServer), "mail " + mailServer + " server was not line");
            Assert.That(Mail.isMailServerOnline(mailServer + "aa") == false,
                        "bad mail server name should had thrown a false return");
        }
    }
}