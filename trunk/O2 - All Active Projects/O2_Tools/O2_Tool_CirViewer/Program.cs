// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using O2.Core.CIR.Ascx;
using O2.External.SharpDevelop;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.Views;


namespace O2.Tool.CirViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (O2AscxGUI.launch("Cir Viewer"))
            {
                HandleO2MessageOnSD.setO2MessageFileEventListener();
                O2AscxGUI.openAscx(typeof (ascx_CirDataViewer), O2DockState.DockLeft, "Cir Viewer");
            }
        }
    }
}
