using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.XRules.Ascx;
using O2.External.WinFormsUI.Forms;

namespace O2.Scanner.DotNet._UnitTests
{
    [TestFixture]
    public class _Test_ascx_XRules_UnitTests
    {
        [Test]
        public void testLoadingInGUI_Dll()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_XRules_UnitTests), "ascx_XRules_UnitTests");
            var ascxXRulesUnitTest = (ascx_XRules_UnitTests)O2AscxGUI.getAscx("ascx_XRules_UnitTests");
            Assert.That(ascxXRulesUnitTest != null, "ascxXRulesUnitTest was null");
            //var targetAssembly = Path.Combine(DI.config.hardCodedO2LocalBuildDir, "_O2_Scanner_DotNet.exe");
            var targetAssembly = Path.Combine(DI.config.hardCodedO2LocalBuildDir, "O2_Core_XRules.dll");
            DI.log.info("Setting targetAssembly to {0}", targetAssembly);
            ascxXRulesUnitTest.loadFile(targetAssembly);
            ascxXRulesUnitTest.XRulesTreeView_ExpandAll();
            O2AscxGUI.waitForAscxGuiClose();
        }

        [Test]
        public void testLoadingInGUI_CSharpFile()
        {
            var targetCSharpFile = Path.Combine(DI.config.hardCodedO2LocalBuildDir, @"_UnitTests\CSharp_Tests\_Sample_UnitTests.cs");
            Assert.That(File.Exists(targetCSharpFile),"Could not find targetCSharpFile");
            // open ascx
            O2AscxGUI.openAscxAsForm(typeof(ascx_XRules_UnitTests), "ascx_XRules_UnitTests");
            var ascxXRulesUnitTest = (ascx_XRules_UnitTests)O2AscxGUI.getAscx("ascx_XRules_UnitTests");
            Assert.That(ascxXRulesUnitTest != null, "ascxXRulesUnitTest was null");            
            // load file
            DI.log.info("Setting targetCSharpFile to {0}", targetCSharpFile);
            ascxXRulesUnitTest.loadFile(targetCSharpFile);
            ascxXRulesUnitTest.XRulesTreeView_ExpandAll();

            // let user see what is going on
            O2AscxGUI.waitForAscxGuiClose();
        }        
    }
}