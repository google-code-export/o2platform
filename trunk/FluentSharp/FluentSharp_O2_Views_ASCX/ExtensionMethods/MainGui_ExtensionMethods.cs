using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentSharp.O2.Views.ASCX.Ascx.MainGUI;
using FluentSharp.O2.DotNetWrappers.ExtensionMethods;
using FluentSharp.O2.Kernel;
using FluentSharp.O2.Kernel.ExtensionMethods;
using System.Windows.Forms;


namespace FluentSharp.O2.Views.ASCX.ExtensionMethods
{
    public static class MainGui_ExtensionMethods
    {
        public static ascx_LogViewer add_LogViewer<T>(this T control)
            where T : Control
        {
            return control.add_Control<ascx_LogViewer>();
        }
    }
}
