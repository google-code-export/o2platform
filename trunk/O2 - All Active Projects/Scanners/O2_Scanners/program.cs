// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Interfaces.Views;
using O2.Scanners.Ascx;

namespace O2.Scanners
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (O2AscxGUI.launch("O2 Scanners"))
                O2AscxGUI.openAscx(typeof(ascx_WillItScan), O2DockState.Document, "O2 Scanners");            
            //SpringExec.loadDefaultConfigFile();
//            o2.Scanners.MSCatNet.Utils.Convert.sConvertMsCatNetResultsFileIntoOzasmt(@"C:\O2\_temp\O2_WillItScan\HacmeBank_WebServices_App_Code\HacmeBank_WebServices_App_Code.dll_MSCatNet.xml");
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            String sFormTitle = Exec.getFormTitle_forClickOnce("Will It Scan");

            Exec.execControl(sFormTitle, typeof(o2.willitscan.ascx.ascx_WillItScan));*/
        }
    }
}
