// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.O2Findings.DotNet;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Tool.WebInspectConverter.Converter;

namespace O2.Tool.WebInspectConverter.ascx
{
    public partial class ascx_WebInspectOzasmMapperGui : UserControl
    {
        private readonly WebInspectResults webInspectResults = new WebInspectResults();

        public ascx_WebInspectOzasmMapperGui()
        {
            InitializeComponent();
        }

        private void fidingsViewer_MappedFile_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                fidingsViewer_MappedFile.setViewMode_Simple();                
                fidingsViewer_OunceOzasmt.setViewMode_Simple();
                fidingsViewer_WebInspectOzasmt.setViewMode_Simple();
            }
        }

        private void dropObject_WebInspectFile_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            if (File.Exists(oObject.ToString()))
                loadWebInspectFile(oObject.ToString());
            else if (Directory.Exists(oObject.ToString()))
                foreach (var file in Files.getFilesFromDir_returnFullPath(oObject.ToString()))
                {
                    loadWebInspectFile(file);
                    loadOunceOzasmtFile(file);
                }
        }

        private void dropObject_OunceOzasmtFile_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            if (File.Exists(oObject.ToString()))
                loadOunceOzasmtFile(oObject.ToString());
            else if (Directory.Exists(oObject.ToString()))
                foreach (var file in Files.getFilesFromDir_returnFullPath(oObject.ToString()))
                {
                    loadWebInspectFile(file);
                    loadOunceOzasmtFile(file);
                }
        }

        private void btGlueTraces_Click(object sender, EventArgs e)
        {
            glueTrace();
        }

        public void loadWebInspectFile(string fileToLoad)
        {
            webInspectResults.loadWebInspectScanFiles(fileToLoad);
            fidingsViewer_WebInspectOzasmt.clearO2Findings();
            fidingsViewer_WebInspectOzasmt.loadO2Findings(
                WebInspectToOzasmt.createO2FindingsFromWebInspectResults(webInspectResults));
            glueTrace();
        }

        public void loadOunceOzasmtFile(string fileToLoad)
        {
            var o2AssessmentOunceScan = new O2Assessment(new O2AssessmentLoad_OunceV6(),fileToLoad);
            o2AssessmentOunceScan.o2Findings = AspNetAnalysis.findWebControlSources(o2AssessmentOunceScan.o2Findings);
            if (o2AssessmentOunceScan.o2Findings.Count > 0)
                fidingsViewer_OunceOzasmt.loadO2Assessment(o2AssessmentOunceScan);
            glueTrace();
        }

        public void glueTrace()
        {
            if (fidingsViewer_WebInspectOzasmt.currentO2Findings.Count > 0 &&
                fidingsViewer_OunceOzasmt.currentO2Findings.Count > 0)
                fidingsViewer_MappedFile.loadO2Findings(
                    OzasmtGlue.glueOnTraceNames(fidingsViewer_WebInspectOzasmt.currentO2Findings, fidingsViewer_OunceOzasmt.currentO2Findings,"Spring MVC Glue"));
        }

        private void dropObject_OunceOzasmtFile_Load(object sender, EventArgs e)
        {            
        }
    }
}
