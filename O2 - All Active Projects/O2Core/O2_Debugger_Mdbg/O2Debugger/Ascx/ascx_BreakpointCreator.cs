// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_BreakpointCreator : UserControl
    {
        public ascx_BreakpointCreator()
        {
            InitializeComponent();
        }

        private void btCalculate_Click(object sender, EventArgs e)
        {
            calculateBreakpoints();    
        }

        

        private void ascx_BreakpointCreator_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                KO2MessageQueue.getO2KernelQueue().onMessages += ascx_BreakpointCreator_onMessages;               
            }
        }

        private void lbModulesInDebuggeeProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbModulesInDebuggeeProcess.SelectedItem != null)
                calculateStatsForModule(lbModulesInDebuggeeProcess.SelectedItem.ToString());
        }

        private void btAddBreakpointOnAllMethods_Click(object sender, EventArgs e)
        {
            if (lbModulesInDebuggeeProcess.SelectedItem != null)
                addBreakpointsToAllMethodsInModule(lbModulesInDebuggeeProcess.SelectedItem.ToString(), cbAddBreakpointsVerboseMessages.Checked);
        }

        private void btShowBreakpoints_Click(object sender, EventArgs e)
        {
            var currentActiveBreakpoints = DI.o2MDbg.BreakPoints.getActiveBreakpoints();
            functionsViewer.showSignatures(currentActiveBreakpoints);
        }
    }
}
