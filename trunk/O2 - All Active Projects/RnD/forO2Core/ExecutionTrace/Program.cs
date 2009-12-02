using System;
using O2.External.WinFormsUI.O2Environment;
using O2.Rnd.ExecutionTrace.ascx;

namespace O2.Rnd.ExecutionTrace
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //  SpringExec.loadConfigAndStartGUI("SpringConfig.xml");
            //SpringExec.loadDefaultConfigFile();
            new O2DockPanel(typeof (ascx_ExecutionTrace));
        }
    }
}