// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Interfaces.Views;
using O2.Tool.SearchAssessmentRun.Ascx;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.SearchAssessmentRun
{
    internal static class Program
    {        
        private static void Main()
        {
            if (O2AscxGUI.launch("O2 Tool - SearchAssessmentRun"))
            {
                O2AscxGUI.openAscx(typeof (ascx_SearchAssessmentRun), O2DockState.Document,"Search Assessment Run");

                // set-up load engines
                ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
                ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
                O2AscxGUI.openAscx(typeof(ascx_FindingsViewer), O2DockState.DockBottomAutoHide, "Findings Viewer");
            }
        }
    }
}
