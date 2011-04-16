// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.XRules.Database.Utils;

//O2File:API_GuiAutomation.cs
//O2Ref:White.Core.dll
//O2Ref:WatiN.Core.1x.dll

namespace O2.XRules.Database.APIs
{

	/*public class API_RDP_Test
	{
		public void launch()
		{
			
		}
	}*/
	
    public class API_RDP 
    {
    	public void launchRdpClient(string ipAddress)
    	{
    		var terminalServicesClient = Processes.startProcess("mstsc.exe");
			var guiAutomation = new API_GuiAutomation(terminalServicesClient);
			var window = guiAutomation.window("Remote Desktop Connection");
			window.textBox("Computer:").set_Text(ipAddress);
			window.button("Connect").mouse().click();
    	}
    }
}