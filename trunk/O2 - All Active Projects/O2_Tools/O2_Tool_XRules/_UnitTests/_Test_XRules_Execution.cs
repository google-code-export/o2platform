using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.XRules.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.Views;

namespace O2.Tool.XRules._UnitTests
{
    [TestFixture]
    public class _Test_XRules_Execution
    {
        private ascx_XRules_Editor xRulesEditor;
        private ascx_XRules_Execution xRulesExecution;
        

        [Test]
        public void editRule_InGUI()
        {
            O2AscxGUI.launch("test Rule execution");
            xRulesEditor = (ascx_XRules_Editor)O2AscxGUI.openAscx(typeof(ascx_XRules_Editor));
            xRulesExecution = (ascx_XRules_Execution)O2AscxGUI.openAscx(typeof(ascx_XRules_Execution), O2DockState.DockLeft, "Rules Execution");
            Assert.That(xRulesEditor != null, "xRulesEditor was null");
            Assert.That(xRulesExecution != null, "xRulesEditor was null");            
            O2AscxGUI.waitForAscxGuiClose();
        }

        [Test]
        public void compileRules_InGUI()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_XRules_Execution));
            O2AscxGUI.waitForAscxGuiClose();
        }
    }
}