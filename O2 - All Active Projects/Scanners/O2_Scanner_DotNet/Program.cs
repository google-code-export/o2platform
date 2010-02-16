// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using O2.Core.XRules.Ascx;
using O2.Core.XRules.XRulesEngine;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Interfaces.Views;
using O2.Scanner.DotNet.Ascx;

namespace O2.Scanner.DotNet
{
    static class Program
    {        
        static void Main()
        {
          //  XRules_Config.PathTo_XRulesDatabase_fromLocalDisk = DI.PathToLocalUnitTestsFiles;
         //   O2AscxGUI.openAscxAsForm(typeof(ascx_PatchAndMonitor));
         //   return;
            if (O2AscxGUI.launch("O2 Scanner - DotNet (Dynamic tracing and patching)"))
            {                
                O2AscxGUI.addControlToMenu(typeof(ascx_DotNetGac));

                O2AscxGUI.addControlToMenu(typeof(ascx_XRules_Editor), O2DockState.Document, "XRules Editor");
                O2AscxGUI.addControlToMenu(typeof(ascx_XRules_Execution), O2DockState.Document, "XRules Execution");

                //var unitTestExecution = "UnitTestExecution";
                //var patchandmonitor = "PatchAndMonitor";
                var xRulesEditor = "XRules Editor";
                //O2AscxGUI.openAscx(typeof(ascx_XRules_UnitTestExecution_BigGUI), O2DockState.DockRight, unitTestExecution);
                //O2AscxGUI.openAscx(typeof(ascx_PatchAndMonitor), O2DockState.Document, patchandmonitor);
                O2AscxGUI.openAscx(typeof(ascx_XRules_Editor), O2DockState.Document, xRulesEditor);
                O2AscxGUI.openAscx(typeof(ascx_XRules_UnitTests), O2DockState.DockRight, "XRules_UnitTests");

                O2AscxGUI.openAscx(typeof(ascx_AssemblyInvoke), O2DockState.DockRight, "Invoke Assesmblies");

                //O2DockUtils.setDocState(unitTestExecution,patchandmonitor, DockStyle.Left);
                //O2DockUtils.setDocState(unitTestExecution, xRulesEditor, DockStyle.Left);
                
            }
        }
    }
}
