// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Interfaces.Views;
using O2.Tool.DotNetCallbacksMaker.ascx;

namespace O2.Tool.DotNetCallbacksMaker
{
    internal static class main
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {            
            if (O2AscxGUI.launch("Dot Net Callback maker"))
                O2AscxGUI.openAscx(typeof (ascx_dotNetCallbacksMaker), O2DockState.Document, "DotNet callback maker");
        }
    }
}
