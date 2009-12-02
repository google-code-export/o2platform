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

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_CirTrace : UserControl
    {
        public ascx_CirTrace()
        {
            InitializeComponent();
        }
        

        private void cirTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var nodeCount = cirTreeView.GetNodeCount(true);
            if (nodeCount < 1000)
                ViewHelpers.onBeforeExpand_tvFunctionMakesCallsTo(e.Node,false);
            else
                DI.log.error("in cirTreeView_BeforeExpand , Max number of items in TreeView:{0}", nodeCount);            
        }
    }
}
