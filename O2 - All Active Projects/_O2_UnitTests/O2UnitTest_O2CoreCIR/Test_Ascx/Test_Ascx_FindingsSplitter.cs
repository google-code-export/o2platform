// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.Core.CIR.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Views.ASCX.O2Findings;

namespace O2.UnitTests.Test_O2CoreCIR.Test_Ascx
{
    [TestFixture]
    public class Test_Ascx_FindingsSplitter
    {
        string targetAssessbly = DI.config.ExecutingAssembly;               // this will point to O2Kernel
        const string findingsSplitterControlName = "Findings Splitter";
        private ascx_FindingsSplitter findingsSplitter;
        private ascx_CirDataViewer cirDataViewer;
        private ascx_FindingsViewer findingsViewer;

        [SetUp]
        public void openGui()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsSplitter), findingsSplitterControlName);
            findingsSplitter = (ascx_FindingsSplitter)O2AscxGUI.getAscx(findingsSplitterControlName);
            cirDataViewer = findingsSplitter.getCirDataViewer_ToProcess();
            findingsViewer = findingsSplitter.getFindingsViewer_toProcess();
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
        }

        [Test]
        public void test_SplitAssessments()
        {
            DI.log.info("Target assesmbly: {0}", targetAssessbly);
            cirDataViewer.loadFile(targetAssessbly);
        }

        [TearDown]
        public void closeGUI()
        {
            O2AscxGUI.close();
            //O2AscxGUI.waitForAscxGuiClose();            
        }
    }
}
