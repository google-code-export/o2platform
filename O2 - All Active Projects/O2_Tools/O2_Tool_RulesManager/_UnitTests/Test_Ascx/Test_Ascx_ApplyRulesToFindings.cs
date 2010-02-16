// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.Rules.OunceLabs.Ascx;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.RulesManager._UnitTests
{
    [TestFixture]
    public class Test_Ascx_ApplyRulesToFindings
    {
        private const string applyRulesToFindingsControlName = "Apply Rules To Findings";
        private const string ozasmtFile = @"E:\O2\Demodata\JoinTracesData\Joinned_HacmeBank_WebSite.ozasmt";
        private const string rulePackFile = @"E:\O2\Demodata\RulePacks\from_601_Sinks_(2995)_.O2RulePack";
        private ascx_ApplyRulesToFindings applyRulesToFindingsControl;
        private ascx_RulePackViewer rulePackViewerControl;
        private ascx_FindingsViewer sourceFindingsViewerControl;
        private ascx_FindingsViewer resultsFindingsViewerControl;

        [SetUp]
        public void openGUI()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_ApplyRulesToFindings), applyRulesToFindingsControlName);
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            PublicDI.log.LogRedirectionTarget = null;  //so that the GUI messages go the debug
            loadTestData();            
        }

        public void loadTestData()
        {
            applyRulesToFindingsControl = (ascx_ApplyRulesToFindings)O2AscxGUI.getAscx(applyRulesToFindingsControlName);
            var thread = applyRulesToFindingsControl.loadO2RulePack(rulePackFile);
            thread.Join();
            rulePackViewerControl = applyRulesToFindingsControl.getRulePackViewerControl();
            Assert.That(rulePackViewerControl.currentO2RulePack.o2Rules.Count > 0, "There were no rules loaded");
            sourceFindingsViewerControl = applyRulesToFindingsControl.getSourceFindingsViewerControl();
            
            thread = sourceFindingsViewerControl.loadO2Assessment(ozasmtFile);
            thread.Join();
            Assert.That(sourceFindingsViewerControl.currentO2Findings.Count > 0, "There are no Findings loaded in the Source FindingsViewer");
            resultsFindingsViewerControl = applyRulesToFindingsControl.getResultsFindingsViewerControl();
        }

        [Test]
        public void test_ApplyingRulesToFindings()
        {
            bool addFindingsWithNoMatches = true;
            List<IO2Finding> mappedFidings = null ;
            // applying filter
             var thread = applyRulesToFindingsControl.executeFilter(
                 ascx_ApplyRulesToFindings.AvailableFilters.BasicSinksMapping,addFindingsWithNoMatches,
                 _mappedFidings => mappedFidings = _mappedFidings);
            thread.Join();
            Assert.That(mappedFidings != null, "mappedFidings was null");
            Assert.That(mappedFidings.Count > 0, "mappedFidings had no findings");
            resultsFindingsViewerControl.loadO2Findings(mappedFidings);
            Assert.That(resultsFindingsViewerControl.currentO2Findings.Count > 0,
                        "There were no findings in resultsFindingsViewerControl");            
        }

        [TearDown]
        public void closeGui()
        {            
            O2AscxGUI.closeAscxParent(applyRulesToFindingsControlName);
        }
    }
}
