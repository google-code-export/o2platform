// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;

namespace O2.Cmd.FindingsFilter.Ascx
{
    public partial class ascx_FindingsFilter : UserControl
    {
        public ascx_FindingsFilter()
        {
            beforeInitializeComponent();
            InitializeComponent();
        }

                       

        private void lbTargetAssessmentFiles_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lbAvailableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            execSelectedFilter();
        }

        private void execSelectedFilter()
        {
            if (tvAvailableFilters.SelectedNode != null && tvAvailableFilters.SelectedNode.Tag is MethodInfo)
                applyFilter((MethodInfo) tvAvailableFilters.SelectedNode.Tag);
        }

        private void btApplyFilter_Click(object sender, EventArgs e)
        {
            execSelectedFilter();
        }        

        private void ascx_FindingsFilter_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void tvAvailableFilters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void tvAvailableFilters_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            execSelectedFilter();
        }

        private void btEditFilters_Click(object sender, EventArgs e)
        {

        }
    }
}
