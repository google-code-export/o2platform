using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.XRules.Database.O2Utils;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.XRules.Database.ExtensionMethods
{
    public static class Scripts_ExecutionMethods
    {
        public static ascx_Simple_Script_Editor add_ScriptExecution(this Control hostControl)
        {
            return hostControl.add_Script();
        }

        public static ascx_Simple_Script_Editor add_Script(this Control hostControl)
        {
            return hostControl.add<ascx_Simple_Script_Editor>();           
        }

    }
}
