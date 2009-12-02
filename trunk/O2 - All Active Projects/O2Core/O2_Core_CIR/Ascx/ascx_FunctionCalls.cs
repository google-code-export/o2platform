// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.CIR.CirUtils;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_FunctionCalls : UserControl
    {

        public ascx_FunctionCalls()
        {
            InitializeComponent();
        }

        //public ascx_FunctionCalls()
        //{

        //}

        private void tvFunctionMakesCallsTo_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
               
            ViewHelpers.onBeforeExpand_tvFunctionMakesCallsTo(e.Node, cbDontExpandRecursiveCalls.Checked);
        }

        private void tvFunctionIsCalledBy_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ViewHelpers.onBeforeExpand_tvFunctionIsCalledBy(e.Node, cbDontExpandRecursiveCalls.Checked);
        }

        private void tvFunctionInfo_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ViewHelpers.onBeforeExpand_tvFunctionInfo(e.Node);
        }

        private void cbCirFunction_IsTainted_CheckedChanged(object sender, EventArgs e)
        {
            rootCirFunction.IsTainted = cbCirFunction_IsTainted.Checked;
        }

        private void tvClassSuperClasses_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ViewHelpers.onBeforeExpand_tvClassSuperClasses(e.Node);
        }

        private void tvClassIsSuperClassedBy_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            ViewHelpers.onBeforeExpand_tvClassIsSuperClassedBy(e.Node);
        }

        private void cbViewInheritedMethods_CheckedChanged(object sender, EventArgs e)
        {
            viewClassMethods();
        }

        private void cbIgnoreCoreObjectClass_CheckedChanged(object sender, EventArgs e)
        {
            viewClassMethods();
        }

        private void tvFunctionMakesCallsTo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender is TreeView)
            {
                ViewHelpers.raiseSourceCodeReferenceEvent(cbShowLineInSourceFile.Checked, (TreeView) sender, true /* remapLineNumber */);
                DotNetWrappers.Windows.O2Forms.SetFocusOnControl(this, 400);
                DotNetWrappers.Windows.O2Forms.SetFocusOnControl(tvFunctionMakesCallsTo, 410);                
            }
        }

        private void tvFunctionIsCalledBy_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender is TreeView)
            {
                ViewHelpers.raiseSourceCodeReferenceEvent(cbShowLineInSourceFile.Checked, (TreeView)sender, false /* remapLineNumber */);
                DotNetWrappers.Windows.O2Forms.SetFocusOnControl(this, 400);
                DotNetWrappers.Windows.O2Forms.SetFocusOnControl(tvFunctionIsCalledBy, 410);                
            }
        }

        private void tvFunctionInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender is TreeView)
            {
                ViewHelpers.raiseSourceCodeReferenceEvent(cbShowLineInSourceFile.Checked, (TreeView)sender, true /* remapLineNumber */);
                DotNetWrappers.Windows.O2Forms.SetFocusOnControl(this, 400);
                DotNetWrappers.Windows.O2Forms.SetFocusOnControl(tvFunctionIsCalledBy, 410);
            }
        }

                  
    }
}
