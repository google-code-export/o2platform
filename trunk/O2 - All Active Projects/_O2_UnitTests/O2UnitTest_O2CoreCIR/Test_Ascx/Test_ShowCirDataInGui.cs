// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Reflection;
using NUnit.Framework;
using O2.Core.CIR.Ascx;
using O2.Core.CIR.CirCreator.DotNet;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.Views;

namespace O2.UnitTests.Test_O2CoreCIR.Test_Cir
{
    [TestFixture]
    public class Test_ShowCirDataInGui
    {
        private ICirData cirData;
        readonly CirFactory cirFactory = new CirFactory();
        #region Setup/Teardown

        [SetUp]
        public void createCirDataObject()
        {
            cirData = new CirData();            
            DI.log.info("using assembly:{0} and O2_Kernel.dll", Assembly.GetExecutingAssembly().Location);
            cirFactory.processAssemblyDefinition(cirData,Assembly.GetExecutingAssembly().Location);
            cirFactory.processAssemblyDefinition(cirData, DI.config.ExecutingAssembly);
            /* // this will load all dlls in the O2 dir
            foreach(var file in Files.getFilesFromDir_returnFullPath(DI.hardCodedO2DeploymentDir,"*.dll"))
                cirFactory.processAssemblyDefinition(cirData, CecilUtils.getAssembly(file));*/

            Assert.That(cirData.dClasses_bySignature.Count > 0, "There were no classes in cirData object");
            Assert.That(cirData.dFunctions_bySignature.Count > 0, "There were no function in cirData object");
            CirFactoryUtils.showCirDataStats(cirData);
        }

        [TearDown]
        public void closeGui()
        {
           // O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.close();
        }

        #endregion
        

        [Test]
        public void Test_LoadFindingsInGui()
        {
            DI.log.info("Opening GUI");
            if (O2AscxGUI.launch())
            {                
                O2Messages.openControlInGUI(typeof(ascx_CirViewer_CirData), O2DockState.Document, "Cir Viewer");
                var cirAnalysisControlName = "Cir Analysis";
                O2Messages.openControlInGUI(typeof(ascx_CirAnalysis), O2DockState.DockTop, cirAnalysisControlName);
                O2Messages.getAscx(cirAnalysisControlName, actionsToExecuteOnCirAnalysisControl);

                //O2Messages.setCirData(cirData);
            }            
        }

        public void actionsToExecuteOnCirAnalysisControl(object ascxControl)
        {
            if (ascxControl is ascx_CirAnalysis)
            {
                var cirAnalysis = (ascx_CirAnalysis) ascxControl;
                O2AscxGUI.logInfo(cirAnalysis.GetType().FullName);
                cirAnalysis.loadO2CirDataFile(DI.config.ExecutingAssembly);
                //DI.log.info();
            }
        }
    }
}
