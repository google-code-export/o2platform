using NUnit.Framework;
using O2.Core.XRules.Ascx;
using O2.External.WinFormsUI.Forms;

namespace O2.Tool.XRules._UnitTests
{
    [TestFixture]
    public class _Test_XRules_UnitTestExecution
    {
        [Test]
        public void loadGUI()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_XRules_UnitTests));
            O2AscxGUI.waitForAscxGuiClose();
        }

        [Test]
        public void logTest()
        {
            string message = "This is a log Test from _Test_XRules_UnitTestExecution";
            DI.log.debug(message);
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
