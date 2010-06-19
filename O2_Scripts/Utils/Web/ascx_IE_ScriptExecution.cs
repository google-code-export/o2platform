// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;

using O2.XRules.Database.Utils.O2;

//O2File:ascx_Simple_Script_Editor.cs.o2
//O2File:Scripts_ExtensionMethods.cs


namespace O2.XRules.Database.Utils
{
    public class ascx_IE_ScriptExecution : Control
    {    
		
		
		public static ascx_IE_ScriptExecution launchGui()
		{
			return O2Gui.open<ascx_IE_ScriptExecution>("IE Script Execution", 600,400)
						.buildGui();
		}
		
    	public ascx_IE_ScriptExecution()
    	{
    		
    	}
    	public ascx_IE_ScriptExecution buildGui()
    	{
    		return buildGui("ie.open(\"http://www.google.com\");");
    	}
		public ascx_IE_ScriptExecution buildGui(string customScript)
		{
			var topPanel = this.add_Panel();			

			var script = topPanel.insert_Below<Panel>().add_Script(false);
			script.InvocationParameters.Add("panel",topPanel); 
			script.onCompileExecuteOnce();
			script.set_Command(getScript(customScript));
			return this;
		}
		
		public string getScript(string customScript)
		{						
			return getScriptWrapper().format(customScript);
		}
		
		public string getScriptWrapper()
		{
			var scriptWrapper = "panel.clear();".line() + 
								"var ie = panel.add_IE().silent(true);".line().line() +
								"{0}".line().line() +
								"//O2File:WatiN_IE_ExtensionMethods.cs".line() + 
								"//using O2.XRules.Database.Utils.O2".line() + 
								"//O2Ref:WatiN.Core.1x.dll";
			return scriptWrapper;								
		}
		
    }
}
