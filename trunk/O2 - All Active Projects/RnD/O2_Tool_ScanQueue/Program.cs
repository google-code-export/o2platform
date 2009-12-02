using System;
using O2.External.WinFormsUI.O2Environment;
using O2.Rnd.Tool.ScanQueue.Ascx;

namespace O2.Rnd.Tool.ScanQueue
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            new O2DockPanel(typeof(ascx_ScanQueue));
            //SpringExec.loadDefaultConfigFile();
            //SpringExec.loadConfigAndStartGUI("O2_Tool_ScanQueue.xml");
        }
    }
}