// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
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
