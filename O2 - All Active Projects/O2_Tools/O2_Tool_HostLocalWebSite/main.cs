using System;
using System.Reflection;
using System.Windows.Forms;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Kernel.Interfaces.Views;
using O2.Tool.HostLocalWebsite.ascx;
using O2.Tool.HostLocalWebsite.ascx;

namespace O2.Tool.HostLocalWebsite
{
    internal static class main
    {        
        private static void Main(String[] asArgs)
        {

            if (O2AscxGUI.launch("O2 Tool - Host Local Website"))
            {
                O2AscxGUI.openAscx(typeof (ascx_HostLocalWebsite), O2DockState.Document, "Host Local Website");
            }
        }
    }
}