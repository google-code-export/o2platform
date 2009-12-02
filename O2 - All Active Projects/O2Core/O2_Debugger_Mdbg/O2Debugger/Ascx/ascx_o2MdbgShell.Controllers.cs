using System;
using O2.Debugger.Mdbg.O2Debugger.Ascx;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Views;

namespace O2.Debugger.Mdbg.O2Debugger
{
    public partial class ascx_O2MdbgShell
    {

        void updateGuiProjectStartOrAttachControls()
        {
            DI.o2MDbg.updateDebuggerRunningAndActiveStatus(updateGuiControls);
        }

        public void updateGuiControls()
        {        
            if (this.okThread(delegate { updateGuiControls(); }))
            {
                laRunningStatus.Text = DI.o2MDbg.debugggerRunning.ToString();
                laActiveStatus.Text = DI.o2MDbg.debugggerActive.ToString();

                tspStopProcess.Enabled = DI.o2MDbg.debugggerActive;
                tsbDetach.Enabled = DI.o2MDbg.debugggerActive;
                tsbBreak.Enabled = DI.o2MDbg.debugggerActive && DI.o2MDbg.debugggerRunning;
                tsbContinue.Enabled = DI.o2MDbg.debugggerActive && !DI.o2MDbg.debugggerRunning;

                if (DI.o2MDbg.debugggerActive)
                    laDebuggedProcessName.Text = (DI.o2MDbg.ActiveProcess != null) ? DI.o2MDbg.ActiveProcess.Name : "";

            }
        }

        private void executeO2DebugAction(O2Thread.FuncThread actionToExecute)
        {
            var threadForAction = actionToExecute();
            // since we cant touch the current thread, lets start a new one that can wait for threadForAction
            O2Thread.mtaThread(() =>
                                   {
                                       if (threadForAction != null && threadForAction.IsAlive)
                                            threadForAction.Join();
                                       // once the threadForAction is complete we can update the controls
                                       updateGuiProjectStartOrAttachControls();
                                   });
            
        }


        public bool logCallback(string message)
        {
            if (tbMDbgOutput.okThread(delegate { logCallback(message); }))
                if (!DI.o2MDbg.ignoreThisSignature(message))
                    tbMDbgOutput.Text = message.Replace("\n", Environment.NewLine) + Environment.NewLine + tbMDbgOutput.Text;

            return true;
        }        

        public void onShellStartCallback()
        {
            DI.o2MDbg.OnCommandExecuted += shell_OnCommandExecuted;
            updateGuiProjectStartOrAttachControls();
            if (ParentForm != null)
                ParentForm.Closed += (sender, e) =>
                                         {
                                             if (DI.o2MDbg != null)
                                                 DI.o2MDbg.stopMDbg();
                                         };
        }


        private void resetOnCommandEvent()
        {
            DI.o2MDbg.resetOnCommandExecuted();
            DI.o2MDbg.OnCommandExecuted += shell_OnCommandExecuted;
        }     

        public void sendCommandtoMDbgShell(string commandToSend)
        {
            DI.o2MDbg.executeMDbgCommand(tbCommands.Text);
        }


        void shell_OnCommandExecuted(string messageExecuted)
        {
            DI.log.debug("O2MDbg Commmand Executed: {0}", messageExecuted);
            DI.o2MDbg.updateDebuggerRunningAndActiveStatus(updateGuiControls);
        }


        private static void openControl_StartOrAttachToProcess()
        {
            O2AscxGUI.openAscxASync(typeof(ascx_StartOrAttach), O2DockState.DockLeft, "Start or Attach (Into) Process");
        }

        private static void openControl_Breakpoints()
        {
            O2AscxGUI.openAscxASync(typeof(ascx_Breakpoints), O2DockState.DockRight, "Breakpoints");
        }

        private static void openControl_DebuggedProcessInfo()
        {
            O2AscxGUI.openAscxASync(typeof(ascx_DebugggedProcessInfo), O2DockState.DockRight, "Debugged Process Info");
        }

        private static void openControl_breakpointCreator()
        {
            O2AscxGUI.openAscxASync(typeof(ascx_BreakpointCreator), O2DockState.DockRight, "Breakpoint Creator");
        }
    }
}
