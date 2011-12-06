// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework; 
using O2.Kernel;
using O2.XRules.Database.APIs;
using O2.XRules.Database.Utils;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.ExtensionMethods;

//O2File:Test_TM_IE.cs
 
namespace O2.SecurityInnovation.TeamMentor
{	
	[TestFixture]
    public class Test_TM_IE_WebSite : Test_TM_IE
    {    	    	    	    	    	
    	public Test_TM_IE_WebSite()
		{
			var ieKey = "Test_TM_IE_WebSite";
			base.set_IE_Object(ieKey);	
		}
		
    	[Test]
    	public string open_IE_and_get_HomePage()  
    	{			
			Assert.That(ie.notNull(), "ie object was null");			
			ie.open(Test_TM.tmServer); 
			
			//wait and check Javascript variable TM.HomePageLoaded			
			var homePageLoaded = ie.waitForJsVariable("TM.HomePageLoaded");
			Assert.AreEqual(homePageLoaded, "TM: HomePage Loaded", "homePageLoaded");
			
			//check page url 	 			
			Assert.AreEqual(ie.url(),Test_TM.currentHomePage , "currentHomePage");						
			
			//wait and check Javascript variable TM.DataTableLoaded
			Assert.That(ie.getJsVariable("TM.DataTableLoaded").isNull(), "TM.DataTableLoaded should be null (at this stage)");			
			var dataTableLoadedMessage = ie.waitForJsVariable("TM.DataTableLoaded");
			Assert.AreEqual(dataTableLoadedMessage, "TM: DataTable Loaded", "dataTableLoadedMessage");
			
			//check links
			var linksTexts = ie.links().texts();			
			Assert.That(linksTexts.contains("Login"),"Missed Link: Login");
			Assert.That(linksTexts.contains("Sign Up"),"Missed Link: Sign Up");			
    		return "ok: open_IE_and_get_HomePage";    		
    	} 
    	    	
    	
    	[Test]
    	public string close_IE()
    	{
    		base.close_IE_Object();    		
    		return "ok: close_IE";
    	}
    	
    	
    }
}