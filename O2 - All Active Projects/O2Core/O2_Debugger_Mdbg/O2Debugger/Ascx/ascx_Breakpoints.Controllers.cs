// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    partial class ascx_Breakpoints
    {
        private bool runOnLoad = true;

        private void onLoad()
        {
            if (!DesignMode && runOnLoad)
            {
                KO2MessageQueue.getO2KernelQueue().onMessages += ascx_Breakpoints_onMessages; // Handle O2 messages   
                runOnLoad = false;
            }
        }

        void ascx_Breakpoints_onMessages(IO2Message o2Message)
        {
            //switch (o2Message.GetType().Name)
            //{
            //    case "IM_SelectedTypeOrMethod":
            //    case "IM_O2MdbgActions":
            //        break;
            if (o2Message is IM_SelectedTypeOrMethod)
            {
                O2Thread.mtaThread(() =>
                                       {
                                           string breakPointSignature =
                                               DI.o2MDbg.BreakPoints.
                                                   getBreakpointSignatureFromO2SelectedTypeOrMethodMessage(
                                                   o2Message);
                                           if (breakPointSignature != "")
                                               O2Forms.setTextBoxValue_ThreadSafeWay(breakPointSignature,
                                                                                     tbBreakPointSignature);
                                       }
                    );
            }
            /*else if (o2Message is IM_O2MdbgAction)
            {
                var o2MdbgAction = (IM_O2MdbgAction)o2Message;
                if (o2MdbgAction.o2MdbgAction == IM_O2MdbgActions.startDebugSession)
                    setOnBreakpointEvent();
            }*/

        }

        private void addBreakpoint(string breakpointSignature)
        {
            DI.o2MDbg.executeMDbgCommand("b " + breakpointSignature);            
        }

       /* void CorProcess_OnBreakpoint(object sender, Debugging.CorDebug.CorBreakpointEventArgs e)
        {
            if (DateTime.Now.Millisecond < 100)
                DI.log.info("*** BREAKPOINT >>  " + O2MDbgBreakPoint.getActiveFrameFunctionName(e));            
            e.Continue = DI.o2MDbg.AutoContinueOnBreakPointEvent;
        }*/

        private void refreshBreakPointList()
        {
            if (this.okThread(delegate { refreshBreakPointList(); }))
            {
                lbCurrentBreakpoints.Items.Clear();
                var activeBreakpoints = DI.o2MDbg.BreakPoints.getActiveBreakpoints();
                foreach (var breakpoint in activeBreakpoints)
                    if (lbCurrentBreakpoints.Items.Count < 2000)
                        lbCurrentBreakpoints.Items.Add(breakpoint);
                    else
                    {
                        DI.log.error("only the first 2000 (of {0}) breakpoints were displayed", activeBreakpoints.Count);
                        break;
                    }
            }
        }
        

        public void setBreakpointTarget(string breakpointTarget)
        {
            if (ExtensionMethods.okThread((Control) tbBreakPointSignature, delegate { setBreakpointTarget(breakpointTarget); }))
                tbBreakPointSignature.Text = breakpointTarget;
        }

        /*public void setOnBreakpointEvent()
        {
            if (DI.o2MDbg.ActiveProcess == null)
            {
                DI.log.error("ActiveProcess == null, cannot set onBreakpoint event");
            }
            else
            {
                DI.log.info("adding OnBreakpoint callback for active process");
                DI.o2MDbg.ActiveProcess.CorProcess.OnBreakpoint += CorProcess_OnBreakpoint;
            }
        }*/
    }
}
