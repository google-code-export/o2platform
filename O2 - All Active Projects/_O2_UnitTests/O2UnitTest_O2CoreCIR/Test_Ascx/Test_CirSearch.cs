using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using O2.Core.CIR.Ascx;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.External.O2Mono.MonoCecil;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.Views;

namespace O2.UnitTests.Test_O2CoreCIR.Test_Ascx
{
    [TestFixture]
    public class Test_CirSearch
    {
        public const string cirDataViewerControlName = "Cir Data Viewer";

        private ascx_CirDataViewer cirDataViewer;

        public void createCirDataObject()
        {
            var cirFactory = new CirFactory();
            ICirData cirData = new CirData();
            DI.log.info("using assembly:{0} and O2_Kernel.dll", Assembly.GetExecutingAssembly().Location);
            cirFactory.processAssemblyDefinition(cirData, Assembly.GetExecutingAssembly().Location);
            cirFactory.processAssemblyDefinition(cirData, DI.config.ExecutingAssembly);            
            Assert.That(cirData.dClasses_bySignature.Count > 0, "There were no classes in cirData object");
            Assert.That(cirData.dFunctions_bySignature.Count > 0, "There were no function in cirData object");
            O2Messages.setCirData(cirData);
            //CirFactoryUtils.showCirDataStats();
        }

        [SetUp]
        public void openGui()
        {
            DI.log.info("Opening GUI");
            // first set up GUI
            if (O2AscxGUI.launch())
            {        
                O2AscxGUI.setLogViewerDockState(O2DockState.DockBottom);
                cirDataViewer = (ascx_CirDataViewer)O2Messages.openControlInGUISync(typeof(ascx_CirDataViewer), O2DockState.Document, cirDataViewerControlName);
                Assert.That(cirDataViewer != null, "cirDataViewer was null");
                // Then create CirData and fire global O2Message showCirDataStats
                createCirDataObject();
            }
        }

        [Test]
        public void test_ShowAllLoadedClasses()
        {
            cirDataViewer.showLoadedClasses();
            //var cirDataAnalysis = cirDataViewer.getCirDataAnalysisObject();
            //var signaturesToShow = cirDataAnalysis.CirClasses<string>();                      

            //cirDataViewer.showSignatures(signaturesToShow);
            //cirDataViewer.showClasses();
            DI.log.info("ready to start");
        }        

        [TearDown]
        public void closeGui()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();                        
        }

    }
}