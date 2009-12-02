// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.External.WinFormsUI.Ascx;
using O2.External.WinFormsUI.Forms;

namespace O2.External.WinFormsUI._UnitTests
{
    [TestFixture]
    public class _Test_Ascx_ViewO2Config
    {
        [Test]
        public void loadGui()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_ViewO2Config));
            var viewO2ConfigAscx = O2AscxGUI.getAscx("ascx_ViewO2Config");
            Assert.That(viewO2ConfigAscx != null, "viewO2ConfigAscx was NULl");

            O2AscxGUI.waitForAscxGuiClose();

            DI.log.info("all done");
        }
    }
}
