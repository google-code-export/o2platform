using System;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Kernel.Interfaces.Views;
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