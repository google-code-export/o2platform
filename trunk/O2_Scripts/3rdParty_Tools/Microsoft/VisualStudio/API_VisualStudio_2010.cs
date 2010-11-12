// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using White.Core.UIItems.WindowItems;
//O2File:API_GuiAutomation.cs
//O2Ref:WindowsFormsIntegration.dll
//O2Ref:RibbonControlsLibrary.dll 
//O2Ref:White.Core.dll

using O2.XRules.Database.Utils;
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs
{
    public class API_VisualStudio_2010
    {   
    	public string VisualStudioExe { get; set; }
    	
    	public API_GuiAutomation VS_Process { get; set; }
		public Window VS_MainWindow  { get; set; }

		//public static string MAIN_WINDOW_TITLE = "Start Page - Microsoft Visual Studio";
		
		public API_VisualStudio_2010()
		{
			VisualStudioExe = @"C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe";
		}
		
		public API_VisualStudio_2010 attach()
		{
			VS_Process = new API_GuiAutomation("devenv");			
			if (VS_Process.TargetProcess.notNull())
				VS_MainWindow = VS_Process.windows()[0];//MAIN_WINDOW_TITLE);
			else
				start();
			return this;
		}
		
		public API_VisualStudio_2010 start()
		{
			 VS_Process = VisualStudioExe.startProcess().automation(); 
			 VS_MainWindow = VS_Process.windows()[0];
			 return this;
		}			    	
    	
    	public API_VisualStudio_2010 close()
    	{
    		VS_Process.TargetProcess.close(); 
    		return this;
    	}
    	
    	public API_VisualStudio_2010 closeInNSeconds(int seconds)
    	{
    		VS_Process.TargetProcess.closeInNSeconds(seconds); 
    		return this;
    		
    	}
    	public API_VisualStudio_2010 move(int left, int top, int width, int height)
    	{
    		VS_MainWindow.move(left, top, width, height);
    		return this;
    	}
    	
    }
    
    public class API_VisualStudio_2010_ExtensionMethods
    {   
    }

}
