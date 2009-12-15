// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using O2.Debugger.Mdbg.O2Debugger.Ascx;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Debugger.Mdbg.O2Debugger
{
    public partial class ascx_O2MdbgShell : UserControl
    {

        //        private O2MDbg_OLD o2MDbgOLD;
        
        

        //public MDbgShell shell;


        public ascx_O2MdbgShell()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                OriginalMDbgMessages.commandExecutionMessage += logCallback;
                startIfNullO2Mdbg();                                            
            }
        }

        private void startIfNullO2Mdbg()
        {
            if (DI.o2MDbg == null)
                DI.o2MDbg = new O2MDbg(onShellStartCallback); 
        }



       /* public ascx_O2MdbgShell(O2MDbg _o2MDbg)
        {
            InitializeComponent();
            DI.o2MDbg = _o2MDbg;
            if (!DesignMode)
            {                                
                OriginalMDbgMessages.commandExecutionMessage += logCallback;
            }

        }*/


        private void btExecuteCommand_Click(object sender, EventArgs e)
        {
            DI.o2MDbg.executeMDbgCommand(tbCommands.Text);
        }

        private void llClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbMDbgOutput.Text = "";
            //     DI.o2MDbg.executeMDbgCommand(O2MDbgCommands.help());
        }

        private void llHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DI.o2MDbg.executeMDbgCommand(O2MDbgCommands.help());
        }

        private void ascx_O2MdbgShell_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
              //  DI.o2MDbg.executeMDbgCommand(O2MDbgCommands.help());
            }
        }

   

        private void cbShowInternalMDbgErrors_CheckedChanged(object sender, EventArgs e)
        {
            DI.o2MDbg.LogInternalMDbgMessage = cbShowInternalMDbgErrors.Checked;
        }





        // private void populateList

        




        private void cbShowCommandExecutionMessage_CheckedChanged(object sender, EventArgs e)
        {
            DI.o2MDbg.LogCommandExecutionMessage = cbShowCommandExecutionMessage.Checked;
        }

        
  

        private void llRefreshButtonsState_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            updateGuiProjectStartOrAttachControls();
        }

        private void tsbContinue_Click(object sender, EventArgs e)
        {
            //btContinueExecution.Enabled = false;
            O2MDbgUtils.continueAttachedProjet();
            updateGuiProjectStartOrAttachControls();
        }

        private void tsbBreak_Click(object sender, EventArgs e)
        {
            //btBreakInto.Enabled = false;
            executeO2DebugAction(O2MDbgUtils.breakIntoAttachedProjet);
        }
        
        private void tsbDetach_Click(object sender, EventArgs e)
        {
            //btDetatchFromProcess.Enabled = false;
            executeO2DebugAction(O2MDbgUtils.detachFromDebuggedProcess);
        }

        private void tspStopProcess_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stopDebuggedProcess);
        }

        

        private void tsbStepInto_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stepInto);
        }       

        private void tsbStepOver_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stepOver);
        }        

        private void tsbStepOut_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stepOut);
        }        

        private void tsbStepIntoAnimated_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stepIntoAnimated);
           
        }

        private void tsbStepOverAnimated_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stepOverAnimated);
          
        }

        private void tsbStepOutAnimated_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stepOutAnimated);
        }


        private void tsbStopAnimationAndContinue_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.stopAnimationAndContinue);
        }

        private void tbCommands_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                if (DI.o2MDbg != null)
                    DI.o2MDbg.executeMDbgCommand(tbCommands.Text);
                else
                {
                    DI.log.error("o2Mdbg object was null, so restarting O2 debugger");
                    startIfNullO2Mdbg();
                }
        }

        private void startOrAttachToProcess_Click(object sender, EventArgs e)
        {
            openControl_StartOrAttachToProcess();
        }

        private void breakpoints_Click(object sender, EventArgs e)
        {
            openControl_Breakpoints();
        }

        private void debugggedProcessInfo_Click(object sender, EventArgs e)
        {
            openControl_DebuggedProcessInfo();
        }

        private void breakpointCreator_Click(object sender, EventArgs e)
        {
            openControl_breakpointCreator();
        }

        private void showCurrentLocation_Click(object sender, EventArgs e)
        {
            executeO2DebugAction(O2MDbgUtils.showCurrentLocation);
        }

        private void llResetOnCommandEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            resetOnCommandEvent();
        }        
                 
    }
}
