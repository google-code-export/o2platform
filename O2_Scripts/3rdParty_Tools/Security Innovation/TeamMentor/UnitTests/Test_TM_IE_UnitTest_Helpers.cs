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
    public class Test_TM_IE_UnitTest_Helpers : Test_TM_IE
    {    	    	    	    	    	
    	public Test_TM_IE_UnitTest_Helpers()
		{
			var ieKey = "Test_TM_IE_WebSite";
			base.set_IE_Object(ieKey);	
			Assert.That(ie.notNull(), "ie object was null");	
		}
		
    	[Test]
    	public string GuiObjects_CreateMappingsTable()  
    	{								
			base.open("html_pages/_UnitTest_Helpers/GuiObjects_CreateMappingsTable.html?time=" + DateTime.Now.Ticks); 						
			var value = ie.waitForJsVariable("UnitTest_Helper_Loaded");			
			Assert.That(value.str()=="True","value was not True");
    		return "ok: open_IE_and_get_HomePage";    		
    	} 
    	    	
    	[Test]
    	public string GuiObjects_ViewFolderStructure()  
    	{								
			base.open("html_pages/_UnitTest_Helpers/GuiObjects_ViewFolderStructure.html?time=" + DateTime.Now.Ticks); 						
			var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
			Assert.That(value.str()=="True","value was not True");
    		return "ok: open_IE_and_get_HomePage";    		
    	} 
    	
    	[Test]
    	public string GuiObjects_ViewFolderStructure_with_GuidandeItemsGuids()  
    	{								
			base.open("html_pages/_UnitTest_Helpers/GuiObjects_ViewFolderStructure_with_GuidandeItemsGuids.html?time=" + DateTime.Now.Ticks); 						
			var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
			Assert.That(value.str()=="True","value was not True");
    		return "ok: open_IE_and_get_HomePage";    		
    	} 
    	
    	[Test]
    	public string LibrariesFoldersViews_And_GuidanceItems_Guids()  
    	{					
    		var guidanceItemsDiv_DEFAULT_VALUE = "GuidanceItems will go here";
			base.open("html_pages/_UnitTest_Helpers/LibrariesFoldersViews_And_GuidanceItems_Guids.html?time=" + DateTime.Now.Ticks); 						
			var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
			Assert.That(value.str()=="True","value was not True");
			ie.eval("var guidanceItemsDiv = $('#guidanceItems').html()");
			var guidanceItemsDiv = ie.getJsVariable("guidanceItemsDiv").str();
			Assert.That(guidanceItemsDiv.notNull(), "guidanceItemsDiv was null");
			Assert.AreEqual(guidanceItemsDiv, guidanceItemsDiv_DEFAULT_VALUE, "guidanceItemsDiv default value");
			ie.eval("$('.library').eq(0).click()");
			ie.eval("var guidanceItemsDiv = $('#guidanceItems').html()");
			guidanceItemsDiv = ie.getJsVariable("guidanceItemsDiv").str();
			Assert.That(guidanceItemsDiv.contains("Showing") && guidanceItemsDiv.contains("GuidanceItems"), "guidanceItemsDiv didn't contain expected two words");			
    		return "ok: LibrariesFoldersViews_And_GuidanceItems_Guids";    		
    	} 

		[Test]
		public string LibrariesFoldersViews_And_GuidanceItems_Guids_Mode_B()  
    	{					    		
			base.open("html_pages/_UnitTest_Helpers/LibrariesFoldersViews_And_GuidanceItems_Guids_Mode_B.html?time=" + DateTime.Now.Ticks); 						
			var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
			Assert.That(value.str()=="True","value was not True");
			ie.eval("var guidanceItemsDiv = $('#guidanceItems').html()");
			var guidanceItemsDiv = ie.getJsVariable("guidanceItemsDiv").str();
			Assert.That(guidanceItemsDiv.contains("Showing") && guidanceItemsDiv.contains("GuidanceItems"), "guidanceItemsDiv didn't contain expected two words");			

			return "ok: LibrariesFoldersViews_And_GuidanceItems_Guids_Mode_B";
		}

    	
    	[Test]
    	public string close_IE()
    	{
    		Test_TM.CLOSE_BROWSER_IN_SECONDS = 4;
    		base.close_IE_Object();    		
    		return "ok: close_IE (in {0} seconds)".format(Test_TM.CLOSE_BROWSER_IN_SECONDS)
    					.jQuery_Append_Body(ie);
    	}
    	
    	
    }
}