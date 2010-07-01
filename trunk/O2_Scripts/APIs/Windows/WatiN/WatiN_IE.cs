﻿// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using WatiN.Core;
using SHDocVw;

//O2Ref:WatiN.Core.1x.dll
//O2Ref:Interop.SHDocVw.dll
//O2Ref:System.Xml.Linq.dll
//O2Ref:System.Xml.dll

namespace O2.XRules.Database.APIs
{
    public class WatiN_IE
	{
		public Thread IEThread { get; set; }
		public IE IE { get; set; }
		public SHDocVw.InternetExplorerClass InternetExplorer { get; set; }
		public AutoResetEvent WaitForIELaunch {get;set;}
		public AutoResetEvent WaitForIEClose {get;set;}
 		public static bool FlashingEnabled { get; set; }
 		public static bool ScrollOnFlash { get; set; } 		
 		public static int FlashingCount { get; set; }
 		
		public int maxExecutionWaitTime = 5000;
 
 		
 		
		public WatiN_IE()
		{
			WaitForIEClose = new AutoResetEvent(false);
			WaitForIELaunch = new AutoResetEvent(false);			
			FlashingEnabled = true;
			ScrollOnFlash = false;
			FlashingCount = 3;
		}				
 
		public WatiN_IE(InternetExplorer ieInstanceToAttach) : this()
 		{ 			
			attachTo(ieInstanceToAttach);			
		}	
 
		public WatiN_IE(System.Windows.Forms.WebBrowser webBrowser) : this()
		{
			attachTo(webBrowser);
		}
 
		public WatiN_IE createIEObject()
		{
			return createIEObject("about:blank");
		}
 
		public WatiN_IE createIEObject(string url)
		{			
			return createIEObject(url, 0,0,500,500);    							
		}
		public WatiN_IE attachTo(System.Windows.Forms.WebBrowser webBrowser)
		{
			// need to do this or the attach is not going to work 						
			WatiN.Core.Settings.AutoStartDialogWatcher = false;  
			attachTo(webBrowser.ActiveXInstance as InternetExplorer);						
			return this;
		}
 
		public WatiN_IE attachTo(InternetExplorer ieInstanceToAttach)
		{
			IEThread = O2Thread.staThread(
    			()=>{
    					try
						{	
							//WatiN.Core.Settings.AutoStartDialogWatcher = false;
 
							WaitForIEClose.Reset();
							"attaching to IE with LocationName '{0}'".info(ieInstanceToAttach.LocationName);    																
 
							IE = new IE(ieInstanceToAttach);																					
 
							//this doesn't work on external processes attach							
							//InternetExplorer = (SHDocVw.InternetExplorerClass)IE.InternetExplorer;														
 
							(ieInstanceToAttach as DWebBrowserEvents2_Event).OnQuit +=
							()=> 
								{
									//"ON WatiN_IE attachTo Quit EVENT".debug();
									close();
								};						
 
							WaitForIELaunch.Set();    					
 
	   					WaitForIEClose.WaitOne();    								   			
	   					//"AFTER WaitForIEClose".error();
			   		}
			   		catch(Exception ex)
			   		{
			   			ex.log("in attachTo(InternetExplorer)",true);
			   			WaitForIELaunch.Set();
	   					WaitForIEClose.Set();
	   				}
					});		
			//"before WaitForIELaunch".error();		
			WaitForIELaunch.WaitOne();	
			//"after WaitForIELaunch".error();
			return this;
		}
 
		public WatiN_IE createIEObject(string url, int top, int left, int width, int height)
		{			
			IEThread = O2Thread.staThread(
    			()=>{
					"launching a new WatIN InternetExplorer Process".info();    		
			   		try
			   		{
			   			if (url.valid().isFalse())
			   				url = "about:blank";
				   		Settings.MakeNewIeInstanceVisible = false;
							IE = new IE(url);										
							InternetExplorer = (SHDocVw.InternetExplorerClass)IE.InternetExplorer;
							InternetExplorer.Top= top;
							InternetExplorer.Left= left;
							InternetExplorer.Width= width;
							InternetExplorer.Height= height;
							InternetExplorer.Visible = true;
 
							InternetExplorer.OnQuit += close;											
							WaitForIELaunch.Set();    					
	   					WaitForIEClose.WaitOne();    					
	   				}
	   				catch(Exception ex)
	   				{
	   					ex.log("in WatiN_IE createIEObject");
	   					WaitForIELaunch.Set();
	   					WaitForIEClose.Set();
	   				}
					});			
			WaitForIELaunch.WaitOne();	
			return this;
		}
 
		public void close()
		{
			"closing WatiN_IE".info();
			try
			{
				IE.Close();
			}
			catch(Exception ex)
			{
				ex.log("in WatiN_IE.close");
			}
			detach();
		}
 
		public void detach()
		{
			WaitForIEClose.Set();
		}
 
		public WatiN_IE execute(MethodInvoker callback)
		{			
			var executionComplete = new AutoResetEvent(false);
			IEThread.invoke(
				()=>{
						try
						{							
							callback();
						}
						catch(Exception ex)
						{
							ex.log("in WatiN_IE execute");
						}
						executionComplete.Set();
					});
			if (executionComplete.WaitOne(maxExecutionWaitTime).isFalse())
				"in WatiN_IE executeOnThread, maxExecutionWaitTime ({0} ms} was reached for action".error(maxExecutionWaitTime);
			return this;
		}		
 
		public T execute<T>(Func<T> callback)
		{
			object returnData = null;
			var executionComplete = new AutoResetEvent(false);
			IEThread.invoke(
				()=>{
						try
						{	
							returnData = callback();						
						}
						catch(Exception ex)
						{
							ex.log("in WatiN_IE execute<T>");
						}
						executionComplete.Set();
					});
			if (executionComplete.WaitOne(maxExecutionWaitTime).isFalse())
				"in WatiN_IE executeOnThread, maxExecutionWaitTime ({0} ms} was reached for action".error(maxExecutionWaitTime);
 
			if (returnData is T)
				return (T)returnData;
 
			return default(T);			
		}	
 
		public static List<InternetExplorer> ieInstances()
		{
			var ieInstances = new List<InternetExplorer>();
			ShellWindows shellWindows = new ShellWindowsClass(); 
			for(int i = 0 ; i < shellWindows.Count ; i++)
			{
				if (shellWindows.Item(i) is InternetExplorer)
				{
					var instance = (InternetExplorer)shellWindows.Item(i);
					//make sure it is a browser instance (since we don't want the Windows Explorer cases)
					if(instance.FullName.contains("IEXPLORE.EXE"))
						ieInstances.Add(instance);
				}
			}
			return ieInstances;
		}
 
		public static WatiN_IE attachTo(string locationName)
		{	
			var ieInstance = ieInstances().locationName(locationName);
			if (ieInstance!= null)
			{
				return new WatiN_IE(ieInstance);				
			}
			"in WatiN_IE attachTo(...) could not find an instance of IE to attach with locationName='{0}'".error(locationName);
			return null;
		}
 
		public static void stopAllIEProcesses()
		{
			Processes.getProcessesCalled("iexplore").stop();
		}
	}
 
 
	public static class InternetExplorer_ExtensionMethods
	{
		// InternetExplorer extension methods
    	public static InternetExplorer locationName(this List<InternetExplorer> ieInstances, string locationToMatch)
    	{
    		if (ieInstances.size() >0 )
    		{
    			foreach(var instance in ieInstances)
    				if (instance.LocationName == locationToMatch)
    					return instance;
    			"in I InternetExplorer locationName(...) it was not possible to find an IE instance with LocationName == '{0}'".debug(locationToMatch);
    		}
    		return null;
    	}
    	public static List<string> locationNames(this List<InternetExplorer> ieInstances)
    	{
    		return (from instance in ieInstances
    				select instance.LocationName).toList();    		
    	}
 
    	public static List<string> locationUrls(this List<InternetExplorer> ieInstances)
    	{
    		return (from instance in ieInstances
    				select instance.LocationURL).toList();    		
    	}
	}	
 
 }
