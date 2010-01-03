// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows.Forms;
using O2.External.WinFormsUI.Forms;
using O2.Views.ASCX.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6; //O2_ImportExport_OunceLabs.dll

namespace O2.Tool.OzasmtQuery._ScriptSamples
{
    public class A_OzasmtManipulation
    {        
        private const string ozasmtFileToUse = @"E:\O2\Demodata\WebGoat 6.0_Scan_CurrentRules.ozasmt";
            
        private const string findingsViewerControlName_Source = "Findings Viewer Source Data";
     
        public static void openOzasmtFile()
        {
        	ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            // open the source Findings Viewer
            O2AscxGUI.openAscxAsForm(typeof(ascx_FindingsViewer), findingsViewerControlName_Source);	             
            // load assessment file into Source Findings Viewer
            // we have to use the direct call due to a bug in Mono            
            //findingsViewerControlName_Source.invokeOnAscx("loadO2Assessment", new object[] { ozasmtFileToUse });
            O2AscxGUI_Ext.invokeOnAscx(findingsViewerControlName_Source, "loadO2Assessment", new object[] { ozasmtFileToUse });
                    
            MessageBox.Show("Ozasmt File loaded in Findings Viewer Control: " + ozasmtFileToUse);            
        }
                      
    }
}
