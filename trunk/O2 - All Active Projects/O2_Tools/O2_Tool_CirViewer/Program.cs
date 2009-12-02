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