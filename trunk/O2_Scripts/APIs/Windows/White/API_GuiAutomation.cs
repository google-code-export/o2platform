// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using White.Core;
using White.Core.UIA;
using White.Core.UIItems;
using White.Core.Factory;
using White.Core.InputDevices;
using White.Core.UIItems.Finders;
using White.Core.UIItems.TabItems;
using White.Core.UIItems.TreeItems;
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
//O2Ref:O2_Misc_Microsoft_MPL_Libs.dll

namespace O2.XRules.Database.APIs
{
    public class API_GuiAutomation
    {        	
    	public Process TargetProcess { get; set; }
    	public Application Application { get; set; }
    	
    	//Global static var
    	public static int Mouse_Move_SkipValue = 1;		
    	public static int Mouse_Move_SleepValue = 1;	// modify if the mouse moves too fast
    	
        public API_GuiAutomation()
    	{    	
    	}
    	
    	public API_GuiAutomation(Process process)
		{
			TargetProcess = process;
			this.attach(TargetProcess);
		}
		
		public API_GuiAutomation(string title)
		{
			this.attach(title);			
		}
		
		public API_GuiAutomation(int processId)
		{
			this.attach(processId);			
		}
		
		public static API_GuiAutomation currentProcess()
		{
			return new API_GuiAutomation(O2.DotNetWrappers.Windows.Processes.getCurrentProcessID());
		}
    }
    
    public static class API_GuiAutomation_ExtensionMethods
    {
    	public static API_GuiAutomation attach(this API_GuiAutomation guiAutomation, Process process)
    	{
    		guiAutomation.Application = Application.Attach(process);
    		return guiAutomation;
    	}    	    	
    	
    	public static API_GuiAutomation attach(this API_GuiAutomation guiAutomation, string title)
    	{
    		guiAutomation.Application = Application.Attach(title);
    		guiAutomation.TargetProcess = guiAutomation.Application.Process;
    		return guiAutomation;
    	}    	    	
    	
    	public static API_GuiAutomation attach(this API_GuiAutomation guiAutomation, int processId)
    	{
    		guiAutomation.Application = Application.Attach(processId);
    		guiAutomation.TargetProcess = guiAutomation.Application.Process;
    		return guiAutomation;
    	}
    	
    	public static API_GuiAutomation launch(this API_GuiAutomation guiAutomation, string executable)
    	{
    		guiAutomation.Application = Application.Launch(executable);
    		guiAutomation.TargetProcess = guiAutomation.Application.Process;
    		return guiAutomation;
    	}
    	
    	public static API_GuiAutomation waitWhileBusy(this API_GuiAutomation guiAutomation)
    	{
    		guiAutomation.Application.WaitWhileBusy();
    		return guiAutomation;
    	}    	    	    	    	
    	
    	public static API_GuiAutomation stop(this API_GuiAutomation guiAutomation)
    	{
    		guiAutomation.Application.Kill();
    		return guiAutomation;
    	}
    }
    
    public static class API_GuiAutomation_ExtensionMethods_UIItemContainer
    {
    	public static List<IUIItem> items(this UIItemContainer container)    		
    	{
    		return container.Items;    			   
    	}
    	
    	public static List<T> items<T>(this UIItemContainer container)
    		where T : IUIItem
    	{
    		return (from item in container.Items
    				where item is T
    				select (T)item).toList();
    	}
    	
    	public static List<Tab> tabs<T>(this T container)
    		where T : UIItemContainer
    	{
    		return container.Tabs;    			   
    	} 
    	
    	public static T find<T>(this IUIItemContainer container, string text)
    		where T : UIItem
    	{
    		return container.get<T>(text);
    	}
    	
    	public static T get<T>(this IUIItemContainer container, string text)
    		where T : UIItem
    	{
    		var match = container.Get<T>(text);
    		if (match.notNull())
    			return match;
    		return container.Get<T>(SearchCriteria.ByText(text));
    	}    	    	
    }	
    
    public static class API_GuiAutomation_ExtensionMethods_IUIItem
    {
    	public static List<string> names(this List<IUIItem> uiItems)
    	{
    		return (from uiItem in uiItems
    			 	select uiItem.Name).toList();
    	}
    	
    	public static string name(this IUIItem uiItem)
    	{
    		return uiItem.Name;
    	}
    	
    	public static T setFocus<T>(this T uiItem)
    		where T : IUIItem
    	{
    		uiItem.Focus();
    		return uiItem;    		
    	}
    	
    	public static T wait<T>(this T uiItem, int miliseconds)
    		where T : IUIItem
    	{
    		return 	uiItem.wait(miliseconds, false);
    	}
    	
    	public static T wait<T>(this T uiItem, int miliseconds, bool showInLog)
    		where T : IUIItem
    	{
    		uiItem.sleep(miliseconds);
    		return uiItem;    		
    	}
    }
    
    public static class API_GuiAutomation_ExtensionMethods_UIItem_Helpers
    {    	
    	public static Button button(this UIItemContainer container, string text)    		
    	{
    		return container.get<Button>(text);
    	}
    	 
    	public static List<Button> buttons(this UIItemContainer container)    		
    	{
    		return container.items<Button>();
    	}
    	
    	public static List<ITabPage> tabPages(this UIItemContainer container)    		
    	{
    		return (from tab in container.Tabs
		    		from tabPage in tab.Pages
		    		select tabPage).toList();
    	}
    	public static ITabPage tabPage(this UIItemContainer container, string name)
    	{
    		foreach(var tabPage in container.tabPages())
    			if (tabPage.Name == name)
    				return tabPage;
    		return null;
    	}
    	
    	public static List<Tree> treeViews(this UIItemContainer container)    		
    	{
    		return container.items<Tree>();
    	}
    	
    	
    	public static List<TreeNode> treeNodes(this Tree tree)    		
    	{
    		return tree.Nodes;
    	}
    	
    }
    public static class API_GuiAutomation_ExtensionMethods_Mouse
    {
    	public static T click<T>(this T uiItem)
    		where T : IUIItem
    	{
    		uiItem.Click();
    		return uiItem;    		
    	}
    	
    	public static T doubleClick<T>(this T uiItem)
    		where T : IUIItem
    	{
    		uiItem.DoubleClick();
    		return uiItem;    		
    	}
    	
    	public static T rightClick<T>(this T uiItem)
    		where T : IUIItem
    	{
    		uiItem.RightClick();
    		return uiItem;    		
    	}
    	
    	public static T mouse<T>(this T uiItem)
    		where T : IUIItem
    	{
    		return uiItem.mouse_MoveTo();
    	}
    	
    	public static T mouse_MoveTo<T>(this T uiItem)
    		where T : IUIItem
    	{
    		//Mouse.Instance.Location = uiItem.Bounds.Center();
    		var location = uiItem.Bounds.Center();
    		Mouse.Instance.mouse_MoveTo(location.X, location.Y,true);
    		return uiItem;    		
    	}

			
		public static T mouse_MoveTo_WinForm<T>(this T control)		
			where T : System.Windows.Forms.Control 
		{
			var location1 = control.PointToScreen(System.Drawing.Point.Empty); 
			var xPos = (double)location1.X + control.width()/2;
			var yPos = (double)location1.Y  + control.height()/2;
			Mouse.Instance.mouse_MoveTo(xPos, yPos, true);		
			return control;
		}
    	
    	public static API_GuiAutomation mouse_MoveTo(this API_GuiAutomation guiAutomation, double x, double y)
    	{
    		Mouse.Instance.mouse_MoveTo(x, y, true);
    		return guiAutomation;
    	}    	
    	
    	public static Mouse mouse_MoveTo(this Mouse mouse, double x, double y, bool animate)
    	{
    		System.Windows.Forms.Application.DoEvents();
    		"__moving mouse to:{0} {1}".debug(x,y);
    		return 	mouse.mouse_MoveBy(x - mouse.Location.X , y - mouse.Location.Y, animate);
    	}
    	
    	public static Mouse mouse_MoveBy(this Mouse mouse, double x, double y, bool animate)
    	{
    		var originalX = mouse.Location.X;
    		var originalY = mouse.Location.Y;
    		"__moving mouse by:{0} {1}".debug(x,y);
    		if (animate)
    		{    			    		
	    		if (x != 0 || y != 0)
	    		{    			
	    			double currentX = mouse.Location.X;
	    			double currentY = mouse.Location.Y;
	    			//"__Start position mouse to :{0}x{1}".debug(currentX, currentY);
	    			double numberOfSteps = (Math.Abs(x) > Math.Abs(y)) ? Math.Abs(x) : Math.Abs(y);
	    			double xStep = ((x != 0) ? x / numberOfSteps : 0) * API_GuiAutomation.Mouse_Move_SkipValue;
	    			double yStep = ((y != 0) ? y / numberOfSteps : 0) * API_GuiAutomation.Mouse_Move_SkipValue;
	    			for(int i =0 ; i < numberOfSteps ; i += API_GuiAutomation.Mouse_Move_SkipValue)  
	    			{
	    				currentX += xStep; //(x >0) ? -xStep : -xStep;
	    				currentY += yStep; //(y >0) ? -yStep : -yStep;
	    				//"Moving mouse to :{0}x{1}".info(currentX, currentY);
	    				mouse.Location = new System.Windows.Point(currentX, currentY);
	    				//System.Windows.Forms.Application.DoEvents();
	    				if (API_GuiAutomation.Mouse_Move_SleepValue >0)
	    					mouse.sleep(API_GuiAutomation.Mouse_Move_SleepValue,false);
	    			}
	    			
	    		}
    		}
    		// this makese sure we always end up in the desired location (namely for the cases where the Mouse_Move_SkipValue is quite high)
    		mouse.Location = new System.Windows.Point(originalX + x, originalY + y);    			
    		return mouse;    		
    	}    	    	
    	
    }
    
    public static class API_GuiAutomation_ExtensionMethods_Window
    {	
    	public static List<Window> windows(this API_GuiAutomation guiAutomation)
    	{
    		return guiAutomation.Application.GetWindows();
    	}
    	
    	public static List<string> names(this List<Window> windows)
    	{
    		return (from window in windows
    		        select window.Name).toList();
    	}
    	
    	public static Window window(this API_GuiAutomation guiAutomation, string windowName)
    	{    		
    		foreach(var window in guiAutomation.windows())
    			if (window.Name == windowName) 
    				return window;
    		return null;
    	}
    	
    	public static Window bringToFront(this Window window)
    	{
    		var windowHandle = window.AutomationElement.Current.NativeWindowHandle;
    		WindowsInput.Native.NativeMethods.SetForegroundWindow(new IntPtr(windowHandle));  
    		return window;    			
    	}
    }
    
    public static class API_GuiAutomation_ExtensionMethods_Find
    {
    	public static AutomationElement.AutomationElementInformation findElement_Image(this API_GuiAutomation guiAutomation)
    	{
    		var processID = guiAutomation.TargetProcess.Id;
    		var finder = new AutomationElementFinder(AutomationElement.RootElement);
			var automationElementInformation =  finder.FindWindow(SearchCriteria.ByControlType(ControlType.Image),processID).Current;
			return automationElementInformation;
		}
		
		public static Window findWindow_viaImage(this API_GuiAutomation guiAutomation)
    	{
    		return guiAutomation.Application.GetWindow(SearchCriteria.ByControlType(ControlType.Image),InitializeOption.NoCache);
    	}
    	
    	
	}
}