// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.External.Evaluant.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Kernel.Interfaces.Views;
using O2.Tool.FindingsQuery._ScriptSamples;
using O2.Tool.OzasmtQuery._ScriptSamples;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.FindingsQuery
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //  [STAThread]
        private static void Main()
        {
            //SpringExec.loadDefaultConfigFile();
            // new O2DockPanel(typeof (ascx_OzasmtQuery));

            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
            ascx_FindingsViewer.o2AssessmentSave = new O2AssessmentSave_OunceV6();
            ascx_OzasmtQuery.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_OzasmtQuery.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
            ascx_OzasmtQuery.o2AssessmentSave= new O2AssessmentSave_OunceV6();
             
            if (O2AscxGUI.launch("Findings Query"))
            {
                O2AscxGUI.openAscx(typeof (ascx_OzasmtQuery),O2DockState.Document,"Findings Query");

                // load demo file                
                //"Ozasmt Query".invokeOnAscx("loadSampleScripts", new object[]{typeof(OzasmtScriptSamples)});
                // we have to use the direct call due to a bug in Mono
                O2AscxGUI_Ext.invokeOnAscx("Ozasmt Query", "loadSampleScripts", new object[] { typeof(OzasmtScriptSamples) });
                
            }
        }
    }
}
