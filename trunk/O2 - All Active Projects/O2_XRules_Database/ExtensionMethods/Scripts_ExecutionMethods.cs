using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.XRules.Database.O2Utils;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;

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

        public static ascx_Simple_Script_Editor add_Script(this Control hostControl, bool codeCompleteSupport)
        {
            return (ascx_Simple_Script_Editor)hostControl.invokeOnThread(
                () =>
                {
                    var scriptControl = new ascx_Simple_Script_Editor(codeCompleteSupport);
                    scriptControl.fill();
                    hostControl.add(scriptControl);
                    return scriptControl;
                });
        }

        public static ascx_Simple_Script_Editor set_Command(this ascx_Simple_Script_Editor scriptEditor, string commandText)
        {
            return (ascx_Simple_Script_Editor)scriptEditor.invokeOnThread(
                () =>
                {
                    scriptEditor.commandsToExecute.set_Text(commandText);
                    return scriptEditor;
                });
        }
    }
}
