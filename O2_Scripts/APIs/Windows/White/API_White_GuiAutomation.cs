// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.DotNetWrappers.ExtensionMethods;
using White.Core;
using White.Core.UIItems;
using White.Core.Factory;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.AutomationElementSearch;
using System.Windows.Automation;

//O2Ref:White.Core.dll
//O2Ref:Bricks.dll
//O2Ref:Bricks.RuntimeFramework.dll
//O2Ref:Castle.Core.dll 
//O2Ref:Castle.DynamicProxy2.dll
//O2Ref:UIAutomationTypes.dll
//O2Ref:UIAutomationClient.dll

namespace O2.XRules.Database.APIs
{
    public class API_White_GuiAutomation
    {        	
    	public Process TargetProcess { get; set; }
    	public Application Application { get; set; }
    	
        public API_White_GuiAutomation()
    	{    	
    	}
    	
    	public API_White_GuiAutomation(Process process)
		{
			TargetProcess = process;
			this.attach(TargetProcess);
		}
		
		public API_White_GuiAutomation(string title)
		{
			this.attach(title);			
		}
		
		public API_White_GuiAutomation(int processId)
		{
			this.attach(processId);			
		}
    }
    
    public static class API_White_GuiAutomation_ExtensionMethods
    {
    	public static API_White_GuiAutomation attach(this API_White_GuiAutomation guiAutomation, Process process)
    	{
    		guiAutomation.Application = Application.Attach(process);
    		return guiAutomation;
    	}    	    	
    	
    	public static API_White_GuiAutomation attach(this API_White_GuiAutomation guiAutomation, string title)
    	{
    		guiAutomation.Application = Application.Attach(title);
    		guiAutomation.TargetProcess = guiAutomation.Application.Process;
    		return guiAutomation;
    	}    	    	
    	
    	public static API_White_GuiAutomation attach(this API_White_GuiAutomation guiAutomation, int processId)
    	{
    		guiAutomation.Application = Application.Attach(processId);
    		guiAutomation.TargetProcess = guiAutomation.Application.Process;
    		return guiAutomation;
    	}
    	
    	public static API_White_GuiAutomation launch(this API_White_GuiAutomation guiAutomation, string executable)
    	{
    		guiAutomation.Application = Application.Launch(executable);
    		guiAutomation.TargetProcess = guiAutomation.Application.Process;
    		return guiAutomation;
    	}
    	
    	public static API_White_GuiAutomation waitWhileBusy(this API_White_GuiAutomation guiAutomation)
    	{
    		guiAutomation.Application.WaitWhileBusy();
    		return guiAutomation;
    	}
    	
    	public static List<Window> windows(this API_White_GuiAutomation guiAutomation)
    	{
    		return guiAutomation.Application.GetWindows();
    	}
    	
    	
    	public static API_White_GuiAutomation stop(this API_White_GuiAutomation guiAutomation)
    	{
    		guiAutomation.Application.Kill();
    		return guiAutomation;
    	}
    }
    
    public static class API_White_GuiAutomation_ExtensionMethods_Window
    {
    	public static List<IUIItem> items(this Window window)
    	{
    		return window.Items;    			   
    	}
    }
    
    public static class API_White_GuiAutomation_ExtensionMethods_Find
    {
    	public static AutomationElement.AutomationElementInformation findElement_Image(this API_White_GuiAutomation guiAutomation)
    	{
    		var processID = guiAutomation.TargetProcess.Id;
    		var finder = new AutomationElementFinder(AutomationElement.RootElement);
			var automationElementInformation =  finder.FindWindow(SearchCriteria.ByControlType(ControlType.Image),processID).Current;
			return automationElementInformation;
		}
		
		public static Window findWindow_viaImage(this API_White_GuiAutomation guiAutomation)
    	{
    		return guiAutomation.Application.GetWindow(SearchCriteria.ByControlType(ControlType.Image),InitializeOption.NoCache);
    	}
	}
}