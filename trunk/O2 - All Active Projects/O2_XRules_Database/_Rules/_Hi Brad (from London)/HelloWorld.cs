using O2.Kernel;
using NUnit.Framework;

namespace O2.XRules.Database._Rules._Hi_Brad__from_London_
{
    [TextFixture]
    public class HelloWorld
    {
        [Test]
        public void sayHello()
        {
            PublicDI.log.showMessageBox("Hello O2 World!");
        }
    }
}
