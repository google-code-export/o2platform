// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
