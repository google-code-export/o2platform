// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Debugger.Mdbg.O2Debugger.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Interfaces.Views;

namespace O2.Debugger.Mdbg
{
    public class startO2MdbgGui
    {
        public static void Main()
        {
            if (O2AscxGUI.launch())
            {

                O2AscxGUI.openAscx(typeof(ascx_StartOrAttach), O2DockState.DockLeft, "Start or Attach (Into) Process");
                O2AscxGUI.openAscx(typeof (ascx_O2MdbgShell), O2DockState.Document, "O2 Mdbg Shell");
                O2AscxGUI.openAscx(typeof(ascx_DebugggedProcessInfo), O2DockState.DockRight, "Debugged Process Info");
                O2AscxGUI.openAscx(typeof(ascx_Breakpoints), O2DockState.DockRight, "Breakpoints");
                O2AscxGUI.openAscx(typeof(ascx_BreakpointCreator), O2DockState.DockRight, "Breakpoint Creator");
            }

        }
    }
}
