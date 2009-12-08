using O2.Kernel;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework;

namespace O2.XRules.Database._Rules._Hi_Brad__from_London_
{
    [TestFixture]
    public class HelloWorld
    {
        [Test]
        public void sayHello()
        {
            PublicDI.log.showMessageBox("Hello O2 World!");
        }
    }
}
