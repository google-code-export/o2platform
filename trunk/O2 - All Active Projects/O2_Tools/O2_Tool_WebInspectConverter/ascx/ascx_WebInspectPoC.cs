// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.O2Findings.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Tool.WebInspectConverter.classes;
using O2.Tool.WebInspectConverter.Converter;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.O2Findings;


namespace O2.Tool.WebInspectConverter.ascx
{
    public partial class ascx_WebInspectPoC : UserControl
    {
        public ascx_WebInspectPoC()
        {
            InitializeComponent();
        }

        private void ascx_WebInspectPoC_Load(object sender, EventArgs e)
        {
            //     btEditFindings_Click(null, null);
        }

        private void btStep_LoadWebInspectFiles_Click(object sender, EventArgs e)
        {
            /*O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_FileMappings),
                                                           true,
                                                           PoC.dockContentTitle_WebInspectScanFiles);*/
     //       var webInspectScanFiles =
     //           (ascx_FileMappings) O2AscxGUI.getAscx(PoC.dockContentTitle_WebInspectScanFiles);
   /*         if (webInspectScanFiles != null)
      //      {
                webInspectScanFiles.setExtensionsToShow("scan");
                webInspectScanFiles.clearMappings();
//                webInspectScanFiles.addFolder(@"E:\OunceWork\HP_WebInspect\HacmeBankScans");
                webInspectScanFiles.expandTree();
*/

                O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_WebInspectResults),
                                                               true,
                                                               PoC.dockContentTitle_WebInspectResults);
                //       O2DockPanel.setDockState(PoC.dockContentTitle_WebInspectScanFiles, DockState.Document);
          //  }
        }


   /*     private void btEditFindings_Click(object sender, EventArgs e)
        {
            var findingsViewer = (ascx_FindingsViewer)O2AscxGUI.getAscx(PoC.dockContentTitle_FindingsViewer);
            var findingEditor = (ascx_FindingEditor)O2AscxGUI.getAscx(PoC.dockContentTitle_FindingEditor);
            // register callback
            findingsViewer.registerSelectedFindingEventCallback(findingEditor.loadO2Finding);
            findingsViewer.loadO2Assessment(PoC.testOzasmtFindingsFile);
        }

        private void btNewFindingEditor_Click(object sender, EventArgs e)
        {
            O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_FindingEditor), false, "Finding Editor");
        }

        private void btNewFindingsViewer_Click(object sender, EventArgs e)
        {
            O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_FindingsViewer), false, "Findings Viewer");
        }*/

        

        private void dropObject_WebInspectFiles_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            if (File.Exists(oObject.ToString()))
            {
                processWebInspectFileAndShowResults(oObject.ToString());                
            }
        }

        private void processWebInspectFileAndShowResults(string fileToLoad)
        {
            var webInspectResults = new WebInspectResults();
            webInspectResults.loadWebInspectScanFiles(fileToLoad);
            WebInspectWindowsFormsUtils.showWebInspectResultsInO2DockWindow(webInspectResults);
            WebInspectWindowsFormsUtils.showFindingsCreatedFromWebInspectResults(webInspectResults);            
        }    

        private void dropObject_WebInspectFiles_Load(object sender, EventArgs e)
        {
        }
        

      

        private void btLoadMappingsGui_Click(object sender, EventArgs e)
        {
            O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_WebInspectOzasmMapperGui), false,
                                                           "Finding Editor");
        }

        private void dropObject_OunceOzasmt_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            if (File.Exists(oObject.ToString()))
            {
                var o2AssessmentOunceScan = new O2Assessment(new O2AssessmentLoad_OunceV6(),oObject.ToString());
                o2AssessmentOunceScan.o2Findings = AspNetAnalysis.findWebControlSources(o2AssessmentOunceScan.o2Findings);


                O2DockPanel.addAscxControlToO2GuiWithDockPanel(typeof (ascx_FindingsViewer),
                                                               true,
                                                               PoC.dockContentTitle_FindingsViewer);

                var findingsViewer = (ascx_FindingsViewer)O2AscxGUI.getAscx(PoC.dockContentTitle_FindingsViewer);
                findingsViewer.loadO2Assessment(o2AssessmentOunceScan);
                findingsViewer.setFilter1Value("vulnName");
                findingsViewer.setFilter2Value("(no Filter)");
            }
        }

        private void dropObject_WebInspectResults_eDnDAction_ObjectDataReceived_Event(object droppedObject)
        {
            if (droppedObject is WebInspectResults )
                WebInspectWindowsFormsUtils.showFindingsCreatedFromWebInspectResults((WebInspectResults)droppedObject);
        }

        private void dropObject_OunceOzasmt_Load(object sender, EventArgs e)
        {

        }
    }
}
