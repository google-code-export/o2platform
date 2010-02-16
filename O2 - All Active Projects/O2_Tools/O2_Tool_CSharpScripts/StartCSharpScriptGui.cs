// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using O2.Debugger.Mdbg.O2Debugger;
using O2.Debugger.Mdbg.O2Debugger.Ascx;
using O2.External.O2Mono.Ascx;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Interfaces.Messages;
using O2.Interfaces.Views;
using O2.Kernel.CodeUtils;
using O2.Kernel.InterfacesBaseImpl;
using O2.Views.ASCX.SourceCodeEdit;

namespace O2.Tool.CSharpScripts
{
    [Serializable]
    public class StartCSharpScriptGui
    {
        // these will be used by the Unit tests to find the loaded controls
        //public static string ascx_ScriptsName { get { return "Script File"; } }
        public static string ascx_ScriptsFolderName { get { return "Scripts Folder"; } }
        public static string ascx_AssemblyInvokeName { get { return "Dynamic Invocation of Compiled Assemblies"; } }
        public static string ascx_O2MdbgShellName { get { return "O2 Mdbg Shell"; } }
        public static string ascx_BreakpointsName { get { return "Breakpoints"; } }
        public static string ascx_BreakpointCreatorName { get { return "Breakpoints Creator"; } }
        public static string ascx_CurrentFrameDetailsName { get { return "Current Frame Details"; } }
        public static string ascx_FindingsCreatorName { get { return "Findings Creator"; } }
        
        //public static string ascx_ScriptControl { get; set; }
                    //[STAThread]

        private static void Main()
        
        {
            if (O2Messages.openAscxGui())
            {
                
                KO2MessageQueue.getO2KernelQueue().onMessages += ascx_Scripts_onMessages;

                // O2 Debugger 
                O2AscxGUI.openAscx(typeof(ascx_O2MdbgShell), O2DockState.DockBottom, ascx_O2MdbgShellName); //this needs to be opened before the ascx_Breakpoints
                

                // scripts controls
                //O2AscxGUI.openAscx(typeof(ascx_Scripts), O2DockState.Document, ascx_ScriptsName); // this needs to loaded before ascx_ScriptsFolder
                var scriptsFolder = (ascx_ScriptsFolder)O2AscxGUI.openAscx(typeof(ascx_ScriptsFolder), O2DockState.DockLeft, ascx_ScriptsFolderName);
				scriptsFolder.loadSampleScripts();
				
                O2AscxGUI.openAscx(typeof(ascx_AssemblyInvoke), O2DockState.DockRight, ascx_AssemblyInvokeName);
                /*O2AscxGUI.openAscx(typeof(ascx_Breakpoints), O2DockState.DockRightAutoHide, ascx_BreakpointsName);
                O2AscxGUI.openAscx(typeof(ascx_BreakpointCreator), O2DockState.DockRightAutoHide, ascx_BreakpointCreatorName);
                O2AscxGUI.openAscx(typeof(ascx_Variables), O2DockState.DockRightAutoHide, ascx_CurrentFrameDetailsName);
                O2AscxGUI.openAscx(typeof(ascx_FindingsCreator), O2DockState.DockRight, ascx_FindingsCreatorName);*/
                O2AscxGUI.addControlToMenu(typeof(ascx_Breakpoints), O2DockState.DockRightAutoHide, ascx_BreakpointsName);
                O2AscxGUI.addControlToMenu(typeof(ascx_BreakpointCreator), O2DockState.DockRightAutoHide, ascx_BreakpointCreatorName);
                O2AscxGUI.addControlToMenu(typeof(ascx_Variables), O2DockState.DockRightAutoHide, ascx_CurrentFrameDetailsName);
                O2AscxGUI.addControlToMenu(typeof(ascx_FindingsCreator), O2DockState.DockRight, ascx_FindingsCreatorName);              
            }            
        }
        

        static void ascx_Scripts_onMessages(IO2Message o2Message)
        {
            HandleO2MessageOnSD.o2MessageHelper_Handle_IM_FileOrFolderSelected(o2Message);

            if (o2Message is IM_O2MdbgAction)
            {
                var o2MDbgAction = (IM_O2MdbgAction)o2Message;
                switch(o2MDbgAction.o2MdbgAction)
                {
                    case IM_O2MdbgActions.breakEvent:
                
                    var filename = o2MDbgAction.filename;
                    var line = o2MDbgAction.line;
                    DI.log.info("SOURCECODE REF -> {0} : {1})", line, filename);
                    HandleO2MessageOnSD.setSelectedLineNumber(filename, line);
                        break;
                    case IM_O2MdbgActions.debugProcessRequest:
                        O2MDbgUtils.startProcessUnderDebugger(o2MDbgAction.filename);
                        break;
                    case IM_O2MdbgActions.debugMethodInfoRequest:
                        O2MDbgUtils.debugMethod(o2MDbgAction.method, o2MDbgAction.loadDllsFrom);
                        break;
                }
                
                
            }
        }
    }
}
