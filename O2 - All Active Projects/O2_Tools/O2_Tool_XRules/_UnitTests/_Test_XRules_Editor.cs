// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using NUnit.Framework;
using O2.Core.XRules.Ascx;
using O2.Core.XRules.XRulesEngine;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;
using System.IO;

namespace O2.Tool.XRules._UnitTests
{
    [TestFixture]
    public class _Test_XRules_Editor
    {
        private ascx_XRules_Editor xRulesEditor = null;
        [SetUp]
        public void setupGui()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_XRules_Editor));
            xRulesEditor = (ascx_XRules_Editor)O2AscxGUI.getAscx("ascx_XRules_Editor");
            Assert.That(xRulesEditor!= null , "xRulesEditor was null");            
        }

        [Test]
        public void editRule()
        {
            xRulesEditor.invokeOnThread(() => editRule_Thread());
            
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
            DI.log.info("all done");
        }

        public void editRule_Thread()
        {
            xRulesEditor.openSourceDirectory(XRules_Config.PathTo_XRulesTemplates);
            var xRulesFiles = xRulesEditor.getXRulesSourceFilesInCurrentDirectory();
            Assert.That(xRulesFiles.Count > 0);
            DI.log.info("There are {0} templates files", xRulesFiles.Count);
            xRulesEditor.openXRule(xRulesFiles[0]);

            // test rule creation from template
            var newRuleFile = xRulesEditor.createNewRuleFromTemplate(xRulesFiles[0], "o2 unit test rule");
            Assert.That(File.Exists(newRuleFile), "new rule file didn't exist:" + newRuleFile);
            File.Delete(newRuleFile);
            Assert.That(false == File.Exists(newRuleFile), "new rule shouldn't exist any more");
        }

    }
}
