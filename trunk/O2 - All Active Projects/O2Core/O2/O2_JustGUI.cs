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