// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.External.WinFormsUI.Forms;

namespace O2
{
    internal static class O2_JustGUI
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            O2AscxGUI.launch();
            //new O2DockPanel();
            //SpringExec.loadDefaultConfigFile();
        }
    }
}
