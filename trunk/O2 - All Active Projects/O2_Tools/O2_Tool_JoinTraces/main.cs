// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Kernel.Interfaces.Views;
using O2.Legacy.OunceV6.JoinTraces;
using O2.Tool.JoinTraces.ascx;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.JoinTraces
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
            ascx_FindingsViewer.o2AssessmentSave = new O2AssessmentSave_OunceV6();

            if(O2AscxGUI.launch("O2 Tool - Join Traces"))                
            {
                /*O2AscxGUI.openAscx(typeof(ascx_JoinSinksToSources), O2DockState.Document, "Join Sinks to sources (simple mode)");                
                O2AscxGUI.openAscx(typeof(ascx_JoinTraces), O2DockState.Document, "Original Join Traces");
                O2AscxGUI.openAscx(typeof(ascx_JoinDotNetWebServices), O2DockState.Document, "Join Traces (with .Net WebServices support)");                
                 * */
                O2AscxGUI.addControlToMenu(typeof(ascx_JoinSinksToSources), O2DockState.Document, "Join Sinks to sources (simple mode)");
                O2AscxGUI.addControlToMenu(typeof(ascx_JoinTraces), O2DockState.Document, "Original Join Traces");
                O2AscxGUI.addControlToMenu(typeof(ascx_JoinDotNetWebServices), O2DockState.Document, "Join Traces (with .Net WebServices support)");
                O2AscxGUI.openAscx(typeof(ascx_JoinTracesOnInterfaces), O2DockState.Document, "Join Traces On Interfaces");                
                
            }            
        }
    }
}
