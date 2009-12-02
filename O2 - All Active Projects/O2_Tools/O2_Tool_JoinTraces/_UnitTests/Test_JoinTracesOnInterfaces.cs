using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using O2.DotNetWrappers.O2Misc;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Tool.JoinTraces.ascx;
using O2.Views.ASCX.O2Findings;
using O2.DotNetWrappers.Windows;

namespace O2.Tool.JoinTraces._UnitTests
{
    [TestFixture]
    public class Test_JoinTracesOnInterfaces
    {
        private static readonly string baseFindings = Path.Combine(DI.config.O2TempDir, "petclinic - O2 scan.ozasmt");
        private static readonly string baseCir = Path.Combine(DI.config.O2TempDir, "petClinic.CirData");

        [SetUp]
        public void setUp()
        {
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            if (false == File.Exists(baseFindings))
                Files.WriteFileContent(baseFindings, testFiles.petclinic___O2_scan);
            if (false == File.Exists(baseCir))
                Files.WriteFileContent(baseCir, testFiles.petClinic);            

            Assert.That(File.Exists(baseFindings), "baseFindings file didn't exist: " + baseFindings);
            Assert.That(File.Exists(baseCir), "baseFindings file didn't exist: " + baseCir);
        }
   

        [Test]
        public void loadTestData()
        {

            O2AscxGUI.openAscxAsForm(typeof(ascx_JoinTracesOnInterfaces));
            var joinTracesControl = (ascx_JoinTracesOnInterfaces) O2AscxGUI.getAscx("ascx_JoinTracesOnInterfaces");
            Assert.That(joinTracesControl != null, "joinTracesControl object was null");
            var baseFindingsControl = joinTracesControl.getBaseFindingsControl();
           // var barCirDataViewerControl = joinTracesControl.getBaseCirDataViewerControl();
            // load base Findings
            var loadThread = joinTracesControl.loadBaseFindings(baseFindings);
            loadThread.Join();            
            Assert.That(baseFindingsControl.currentO2Findings.Count > 0, "no findings Loaded");
            // load base Cir
            joinTracesControl.loadBaseCir(baseCir);

            // calculateSourcesMappedToInterfaces 

            joinTracesControl.calculateSourcesMappedToInterfaces(true);

            O2AscxGUI.close();
            //O2AscxGUI.waitForAscxGuiClose();
        }
    }
}
