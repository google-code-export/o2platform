using System;
using O2.External.WinFormsUI.O2Environment;
using O2.Tool.FilterAssessmentFiles.Ascx;

namespace O2.Tool.FilterAssessmentFiles
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //SpringExec.loadDefaultConfigFile();
            new O2DockPanel(typeof (ascx_FilterWizard));
        }
    }
}