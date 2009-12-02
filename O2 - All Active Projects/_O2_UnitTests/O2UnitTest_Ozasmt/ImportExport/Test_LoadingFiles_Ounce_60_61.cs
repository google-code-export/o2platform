using NUnit.Framework;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Kernel.Interfaces.O2Findings;
using O2.UnitTests.Test_Ozasmt._SampleScans;
using O2.Views.ASCX.O2Findings;
using System.IO;

namespace O2.UnitTests.Test_Ozasmt.ImportExport
{
    [TestFixture]
    public class Test_LoadingFiles_Ounce_60_61
    {
        
        private const string findingsViewerControlOzasmt60 = "Findings Viewer for Ozasmt 6.0";
        private const string findingsViewerControlOzasmt61 = "Findings Viewer for Ozasmt 6.1";
        
        readonly string ozasmt60 = SampleScripts.getFileWithSampleScript(typeof(SampleScans), "SampleScan_Ozasmt_6_0", ".ozasmt");
        readonly string ozasmt61 = SampleScripts.getFileWithSampleScript(typeof(SampleScans), "SampleScan_Ozasmt_6_1", ".ozasmt");

        [SetUp]
        public void setUp()
        {
            // setup DI            

           // O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsViewer), findingsViewerControlOzasmt60);
          //  O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsViewer), findingsViewerControlOzasmt61);
            
            Assert.That(File.Exists(ozasmt60), "ozasmt60 file didn't exist");
            Assert.That(File.Exists(ozasmt61), "ozasmt61 file didn't exist");
            //DI.log.info("ozasmt60: {0}", ozasmt60);
            
        }

        [Test]
        public void test_OzastFilesIntoO2Findings()
        {
            // set-up o2AssessmentLoad engines
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());

            // open FindingsViewer controls
            O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsViewer), findingsViewerControlOzasmt60);
            O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsViewer), findingsViewerControlOzasmt61);

            // load ozasmt files in it 
            findingsViewerControlOzasmt60.invokeOnAscx("loadO2Assessment", new object[] { ozasmt60 });
            findingsViewerControlOzasmt61.invokeOnAscx("loadO2Assessment", new object[] { ozasmt61 });

            // wait for FindingsViewer controls to close
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.closeAscxParent(findingsViewerControlOzasmt61);
            O2AscxGUI.closeAscxParent(findingsViewerControlOzasmt60);
            //O2AscxGUI.close();
            //O2AscxGUI.waitForAscxGuiClose();
        }

        [Test]
        public void test_loadOzastFiles()
        {
            // using Ounce 6.0 engine
            loadOzastFilesUsingEngine(new O2AssessmentLoad_OunceV6(), ozasmt60, false /*expectLoadFail*/);
            loadOzastFilesUsingEngine(new O2AssessmentLoad_OunceV6(), ozasmt61, true /*expectLoadFail*/);
            // using Ounce 6.1 ending
            loadOzastFilesUsingEngine(new O2AssessmentLoad_OunceV6_1(), ozasmt60, true /*expectLoadFail*/);
            loadOzastFilesUsingEngine(new O2AssessmentLoad_OunceV6_1(), ozasmt61, false /*expectLoadFail*/);
        }
        
        public bool loadOzastFilesUsingEngine(IO2AssessmentLoad o2LoadEngineToUse, string fileToLoad, bool expectLoadFail)
        {
            DI.log.info("Loading file {0} using engine {1}",Path.GetFileName(fileToLoad), o2LoadEngineToUse.engineName);
            var timer = new O2Timer("File loaded").start();
            var o2Assessment = o2LoadEngineToUse.loadFile(fileToLoad);
            if (expectLoadFail)
                Assert.That(o2Assessment == null ,"on this file for this engine the, o2Assessment was expected to be null");
            else
            {
                Assert.That(o2Assessment != null, "o2Assessment was null");
                Assert.That(o2Assessment.o2Findings.Count > 0, "o2Assessment.o2Findings.Count  == 0");
                DI.log.info("There were {0} findings in file loaded: {0}", o2Assessment.o2Findings.Count);
            }            
            timer.stop();            
            return true;
        }

        [TearDown]
        public void closeGui()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            //O2AscxGUI.close();
        }

        // to use when creating new schemas
        [Test,Ignore("To be used to create the c# file from an XSD")]
        public void createClassFromXsd()
        {
            var xsdFileToProcess =
                @"E:\O2\_SourceCode_O2\O2Core\O2_Core_Ozasmt\Ozasmt_OunceV6_1\xsd_Ozasmt_OunceV6_1.xsd";
            DevUtils.createCSharpFileFromXsd(xsdFileToProcess);
        }
    }
}
