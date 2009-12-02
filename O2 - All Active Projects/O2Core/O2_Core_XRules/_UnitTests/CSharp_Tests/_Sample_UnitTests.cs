//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;

namespace O2.Core.XRules._UnitTests.CSharp_Tests
{
    [TestFixture]
    public class _Sample_UnitTests
    {
        [Test]
        public void test1()
        {
            Assert.That(true);
        }

        [Test]
        public void test2_willSucessed()
        {
            Assert.That(1==1);
        }

        [Test]
        public void test1_willFail()
        {
            Assert.Fail("This is a message");
        }
    }
}