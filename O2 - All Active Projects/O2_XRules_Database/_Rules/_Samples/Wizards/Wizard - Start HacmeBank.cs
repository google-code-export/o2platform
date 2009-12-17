// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.Views.ASCX.MerlinWizard;
using O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods;
using O2.Tool.HostLocalWebsite.ascx;
//O2Tag_AddReferenceFile:merlin.dll
using Merlin;
using MerlinStepLibrary;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework; 
//O2Ref:System.Web.dll
using System.Windows.Forms;
//O2Ref:C:\O2\O2 - All Active Projects\_Bin_(O2_Binaries)\_HostLocalWebsite (O2 Tool).exe
using O2.Tool.HostLocalWebsite.classes;


namespace O2.Script
{
	[TestFixture]
    public class Wizard_Start_HacmeBank
    {    
    	private static IO2Log log = PublicDI.log;
		private static string hacmeBankRootFolder = @"C:\O2\O2 Demos\HacmeBank_v2.0 (Dinis version - 7 Dec 08)";
		private static string hacmeBankUrl; // = @"http://127.0.0.1/HacmeBank_v2_Website";
		ascx_HostLocalWebsite webServicesHost;
    	ascx_HostLocalWebsite webSiteHost;

		[Test]
        public string runWizard()
        {               
        	var o2Wizard = new O2Wizard("Start Hacmebank");			
        	
        	o2Wizard.Steps.add_Control(typeof(ascx_HostLocalWebsite),"ascx_HostLocalWebsite","");        	
        	
        	
			o2Wizard.Steps.add_SelectFolder("Config HacmeBank folder dir", hacmeBankRootFolder, (newValue) => hacmeBankRootFolder = newValue);			
        	o2Wizard.Steps.add_WebBrowser("Browser","Viewing Hacmebank folder in a WebBrowser", hacmeBankRootFolder);
        	o2Wizard.Steps.add_Control(typeof(ascx_HostLocalWebsite), "HacmeBank - WebServices","HacmeBank - WebServices", startWebServices);
        	o2Wizard.Steps.add_Control(typeof(ascx_HostLocalWebsite), "HacmeBank - WebSite","HacmeBank - WebServices", startWebSite);        
			o2Wizard.Steps.add_WebBrowser("Browser","Exploit Hacmebank :)", hacmeBankUrl, openHomePage);
        	o2Wizard.Steps.add_Action("Closing Down WebServers", closeWebServers);
        	o2Wizard.start();
        	
        	return "ok..";
        }
    	    
    	public void startWebServices(IStep step)
    	{       		
    		var websiteFolder = hacmeBankRootFolder + @"\HacmeBank_v2_WS";    		
    		webServicesHost = (ascx_HostLocalWebsite)step.FirstControl;    		
    		webServicesHost.startWebServerOnDirectory(websiteFolder,"80");
    	}
    	
    	public void startWebSite(IStep step)
    	{
    		var websiteFolder = hacmeBankRootFolder + @"\HacmeBank_v2_Website";    		
    		webSiteHost = (ascx_HostLocalWebsite)step.FirstControl;    		
    		hacmeBankUrl = webSiteHost.startWebServerOnDirectory(websiteFolder);    		    		
    	}
    	
    	public void openHomePage(IStep step)
    	{
    		var webBrowser = (WebBrowser)step.UI;
    		webBrowser.Navigate(hacmeBankUrl);    		
    	}
    	
    	public void closeWebServers(IStep step)
    	{
    		step.setText("Closing Web Servers");
    		step.appendLine(":");
    		webServicesHost.webservices.StopWebService();
    		webSiteHost.webservices.StopWebService();
    		step.appendLine("Done..");
    	}
    }
}
