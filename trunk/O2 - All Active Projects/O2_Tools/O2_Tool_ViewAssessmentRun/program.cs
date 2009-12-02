using System;
using O2.External.WinFormsUI.O2Environment;

namespace O2.Tool.ViewAssessmentRun
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            //SpringExec.loadDefaultConfigFile();
            new O2DockPanel(typeof (Ascx.ascx_ViewAssessmentRun));
        }
    }
}