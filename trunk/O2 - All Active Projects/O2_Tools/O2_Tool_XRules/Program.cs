// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Cmd.XRules;
using O2.Core.FileViewers.Ascx;
using O2.Core.FileViewers.Ascx.O2Rules;
using O2.Core.FileViewers.Ascx.tests;
using O2.Core.XRules.Ascx;
using O2.Core.XRules.XRulesEngine;
using O2.Debugger.Mdbg;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Debugger.Mdbg.O2Debugger.Ascx;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;
using O2.Tool.XRules.classes;
using O2.Views.ASCX.O2Findings;
using O2.Core.CIR.Ascx;

namespace O2.Tool.XRules
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {
            if (false) // to force linking to the O2_Cmd_XRules module
                XRulesWrapper.help();

            OunceAvailableEngines.addAvailableEnginesToControl(typeof(ascx_FindingsViewer));
            XRules_Config.xRulesDatabase = new KXRulesDatabase();
            if (O2AscxGUI.launch("O2 XRules", 1000, 800))
            {
                HandleO2MessageOnMdbg.setO2MessageMdbgListener();       // be able to handle Debugger events             
                HandleO2MessageOnSD.setO2MessageFileEventListener();    // be able to handle open file events

                O2AscxGUI.addControlToMenu(typeof(ascx_TilesDefinition_xml));
                O2AscxGUI.addControlToMenu(typeof(ascx_J2EE_web_xml));
                O2AscxGUI.addControlToMenu(typeof(ascx_Validation_xml));
                O2AscxGUI.addControlToMenu(typeof(ascx_Struts_config_xml));
                O2AscxGUI.addControlToMenu(typeof(ascx_StrutsMappings_ManualMapping));

                O2AscxGUI.addControlToMenu(typeof(ascx_FindingsViewer));
                O2AscxGUI.addControlToMenu(typeof(ascx_CirDataViewer));
                //O2AscxGUI.addControlToMenu(typeof(ascx_DotNet_Dependencies));

                //O2AscxGUI.openAscx(typeof(ascx_XRules_Editor));
                //O2AscxGUI.openAscx(typeof(ascx_XRules_Execution));

                O2AscxGUI.addControlToMenu(typeof(ascx_SvnBrowser), O2DockState.Float, "SVN Browser - O2 Source Code");

                O2AscxGUI.addControlToMenu(typeof(ascx_O2Rules_Struts), O2DockState.Document, "O2 Rules Struts");

                O2AscxGUI.openAscx(typeof(ascx_XRules_Execution), O2DockState.Document, "XRules Execution");
                O2AscxGUI.openAscx(typeof(ascx_XRules_Editor), O2DockState.Document, "XRules Editor");

                O2AscxGUI.openAscx(typeof(ascx_XRules_UnitTests), O2DockState.DockRight, "XRules & UnitTests");

                O2AscxGUI.addControlToMenu(typeof(ascx_AssemblyInvoke), O2DockState.DockRight, "Invoke Assesmblies");
                O2AscxGUI.addControlToMenu(typeof(ascx_O2MdbgShell), O2DockState.DockBottom, "O2 Debugger");

                O2AscxGUI.addControlToMenu(typeof(ascx_Variables), O2DockState.DockBottom, "O2 Mdbg - Variables");
                O2AscxGUI.addControlToMenu(typeof(ascx_FindingsCreator), O2DockState.DockBottom, "O2 Mdbg - FindingsCreator");
                O2AscxGUI.addControlToMenu(typeof(ascx_Breakpoints), O2DockState.DockBottom, "O2 Mdbg - Breakpoints");
                O2AscxGUI.addControlToMenu(typeof(ascx_StackTraceAndThreads), O2DockState.DockBottom, "O2 Mdbg - StackTrace and Threads..");
            }            
        }                
    }
}
