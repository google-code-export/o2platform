﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace O2.API.Visualization.Ascx
{
    public partial class ascx_Xaml_Host : UserControl
    {
        public ascx_Xaml_Host()
        {
            InitializeComponent();
        }

        public ElementHost element()
        {
            return elementHost;
        }

        public System.Windows.Controls.Label showLabel()
        {
            var maLabel = new Xaml.maLabel();
            elementHost.Child = maLabel;
            return maLabel.label1;
        }
    }
}
