﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;

namespace O2.DotNetWrappers.Windows
{
    // These are extension methods that allow the thread safe use of ProgressBar

    public static class O2Forms_ThreadSafe_FlowLayoutPanel
    {        
        public static void ts_Clear(this FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.invokeOnThread(() => flowLayoutPanel.Controls.Clear());
        }

        public static void ts_AddControl(this FlowLayoutPanel flowLayoutPanel, Control control)
        {
            flowLayoutPanel.invokeOnThread(() => flowLayoutPanel.Controls.Add(control));
        }        
    }
}
