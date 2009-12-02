// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace O2.Views.ASCX.CoreControls
{
    public partial class ascx_O2ObjectModel : UserControl
    {
        public ascx_O2ObjectModel()
        {
            InitializeComponent();
        }

        private void llRefreshFunctionsViewer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            refreshViews();
        }

        private void ascx_O2ObjectModel_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void cbHideCSharpGeneratedMethods_CheckedChanged(object sender, EventArgs e)
        {
            refreshO2ObjectModelView(cbHideCSharpGeneratedMethods.Checked);
        }

        private void tbFilterBy_MethodType_KeyUp(object sender, KeyEventArgs e)
        {
            showFilteredMethods(e);
        }        

        private void tbFilterBy_MethodName_KeyUp(object sender, KeyEventArgs e)
        {
            showFilteredMethods(e);
        }

        private void tbFilterBy_ParameterType_KeyUp(object sender, KeyEventArgs e)
        {
            showFilteredMethods(e);
        }

        private void tbFilterBy_ReturnType_KeyUp(object sender, KeyEventArgs e)
        {
            showFilteredMethods(e);
        }
        
     
    }
}
