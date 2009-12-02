using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Tool.JoinTraces.ascx
{
    public partial class ascx_JoinTracesOnInterfaces : UserControl
    {
        public ascx_JoinTracesOnInterfaces()
        {
            InitializeComponent();
        }

        private void btCalculateSourcesMappedToInterfaces_Click(object sender, EventArgs e)
        {
            calculateSourcesMappedToInterfaces(cbIncludeOriginalFindings.Checked);
                
        }

        private void findingsViewer_DynamicJoin__onTraceSelected(IO2Trace o2TraceSelected)
        {
            dynamicJoin_onTraceSelected(o2TraceSelected);
        }
         
             
    }
}
