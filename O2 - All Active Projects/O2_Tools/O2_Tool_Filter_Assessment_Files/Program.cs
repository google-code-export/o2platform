// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
