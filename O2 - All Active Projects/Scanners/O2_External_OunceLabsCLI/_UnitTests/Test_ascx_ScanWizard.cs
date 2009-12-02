using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.External.WinFormsUI.Forms;
using O2.Scanner.OunceLabsCLI.Ascx;

namespace O2.Scanner.OunceLabsCLI._UnitTests
{
    [TestFixture]
    public class Test_ascx_ScanWizard
    {
        public void createTestScanTargets()
        {
            
        }

        [Test]
        public void loadAscxGUI()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_ScanWizard));
            var ascxScanWizard = O2AscxGUI.getAscx(typeof (ascx_ScanWizard).Name);
            Assert.That(ascxScanWizard != null, "ascxScanWizard was null");
            O2AscxGUI.waitForAscxGuiClose();
        }
    }
}
