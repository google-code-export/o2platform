using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Tool.JoinTraces.ascx;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.JoinTraces._UnitTests
{
    [TestFixture]
    public class Test_JoinSinksToSources
    {
        private const string joinSinksToSourcesControl = "Join Sinks To Sources";
        private const string testfile = @"E:\O2\Demodata\WebGoat 6.0_Scan_CurrentRules.ozasmt";

        [SetUp]
        public void launchGUI()
        {
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            O2AscxGUI.openAscxAsForm(typeof(ascx_JoinSinksToSources), joinSinksToSourcesControl);
        }

        [Test]
        public void loadTestAssessments()
        {
            var joinSinksToSources =  (ascx_JoinSinksToSources)O2AscxGUI.getAscx(joinSinksToSourcesControl);
            joinSinksToSources.getFindingsViewerObjectFor_Sinks().setFilter1Value("KnownSink");
            joinSinksToSources.getFindingsViewerObjectFor_Sources().setFilter1Value("KnownSink");    
            var thread = joinSinksToSources.loadFileIntoSinks(testfile);
            Assert.That(thread != null,"thread was null");
            thread.Join();
            thread = joinSinksToSources.loadFileIntoSources(testfile);
            thread.Join();
            var findingsIn_Sinks = joinSinksToSources.getFindingsIn_Sinks();
            var findingsIn_Sources = joinSinksToSources.getFindingsIn_Sources();            
            Assert.That(findingsIn_Sinks.Count() > 0, "findingsIn_Sinks.Count() == 0");
            Assert.That(findingsIn_Sources.Count() > 0, "findingsIn_Sources.Count() == 0");

            var testSignature = "AAATestSignature()";
            var sinkId = 0;
            var source = 1;
            ((O2Finding)findingsIn_Sinks[sinkId]).Sink = testSignature;
            Assert.That(((O2Finding)findingsIn_Sinks[sinkId]).Sink == testSignature, "Sink set didn't match testSignature");
            ((O2Finding)findingsIn_Sources[source]).Source = testSignature;
            Assert.That(((O2Finding)findingsIn_Sources[source]).Source == testSignature, "Sink set didn't match testSignature");
            joinSinksToSources.refreshSinks();
            joinSinksToSources.refreshSources();            
            joinSinksToSources.calculateJoinnedTraces();
            var o2Findings = joinSinksToSources.getFindingsViewerObjectFor_JoinnedTraces().getFindingsFromTreeView();
            Assert.That(o2Findings.Count == 1, "There should only be 1 finding in the trace created");
            Assert.That(((O2Finding)o2Findings[0]).Sink == testSignature, "There should only be 1 finding in the trace created");
        }

        [TearDown]
        public void closeGUI()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.closeAscxParent(joinSinksToSourcesControl);
        }
    }
}
