﻿using System;
using System.Diagnostics;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods; 
//O2File:Tool_API.cs

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs
using O2.XRules.Database.Utils;

namespace O2.XRules.Database.APIs
{
	public class Install_PE_Test
	{
		public void test()
		{
			new ProcessExplorer().start();
		}
	}
	public class ProcessExplorer : Tool_API 
	{	
		public ProcessExplorer() : this(true)
		{
		}
		
		public ProcessExplorer(bool installNow)
		{
			config("ProcessExplorer", "ProcessExplorer v14.1", "ProcessExplorer.zip");			
    		Install_Uri = "http://download.sysinternals.com/Files/ProcessExplorer.zip".uri();
    		if (installNow)
    			install();
    		//Install_Dir = @"C:\strawberry\";
		}
		
		
		public bool install()
		{
			"Installing {0}".info(ToolName);
			return installFromZip_Web(); 						
		}
		
		public Process start()
		{
			if (install())
				return Install_Dir.pathCombine("procexp.exe").startProcess();
			return null;
		}		
	}
}