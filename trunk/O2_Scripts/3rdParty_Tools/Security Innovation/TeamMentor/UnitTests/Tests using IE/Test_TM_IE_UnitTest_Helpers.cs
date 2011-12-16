// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Linq;
using System.Collections;
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
			var ieKey = "Test_TM_IE_UnitTest_Helpers";
			base.set_IE_Object(ieKey);	
			Assert.That(ie.notNull(), "ie object was null");	
			Test_TM.CLOSE_BROWSER_IN_SECONDS = 4;    
			WatiN_IE_ExtensionMethods.WAITFORJSVARIABLE_MAXSLEEPTIMES = 20;
		}
		
    	[Ignore][Test]
    	public void GuiObjects_CreateMappingsTable()  
    	{
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/GuiObjects/GuiObjects_CreateMappingsTable.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_Loaded");			
				Assert.That(value.str()=="True","value was not True");    	
			}
    	} 
    	    	
    	[Ignore][Test]
    	public void GuiObjects_ViewFolderStructure()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/GuiObjects/GuiObjects_ViewFolderStructure.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
				Assert.That(value.str()=="True","value was not True");    		
			}
    	} 
    	
    	[Ignore][Test]
    	public void GuiObjects_ViewFolderStructure_with_GuidandeItemsGuids()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/GuiObjects/GuiObjects_ViewFolderStructure_with_GuidandeItemsGuids.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
				Assert.That(value.str()=="True","value was not True");    		
			}
    	} 
    	
    	[Ignore][Test]
    	public void LibrariesFoldersViews_And_GuidanceItems_Guids()  
    	{		
    		lock(ie)
    		{
	    		var guidanceItemsDiv_DEFAULT_VALUE = "GuidanceItems will go here";
				base.open("html_pages/_UnitTest_Helpers/GuiObjects/LibrariesFoldersViews_And_GuidanceItems_Guids.html?time=" + DateTime.Now.Ticks); 						
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
			}
    	} 

		[Ignore][Test]
		public void LibrariesFoldersViews_And_GuidanceItems_Guids_Mode_B()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/GuiObjects/LibrariesFoldersViews_And_GuidanceItems_Guids_Mode_B.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_Loaded").str();			
				Assert.That(value.str()=="True","value was not True");
				ie.eval("var guidanceItemsDiv = $('#guidanceItems').html()");
				var guidanceItemsDiv = ie.getJsVariable("guidanceItemsDiv").str();
				Assert.That(guidanceItemsDiv.contains("Showing") && guidanceItemsDiv.contains("GuidanceItems"), "guidanceItemsDiv didn't contain expected two words");
			}
		}
		
		[Ignore][Test]
		public void SlickGrid_ViewUniqueStrings()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/DataGrids/SlickGrid_View_UniqueStrings.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_SlickGridLoaded").str();			
				Assert.AreEqual		(value ,"True",															"value was not True");
				Assert.IsNotNull	(ie.getJsVariable("TM.testGrid"), 										"TM.testGrid");			
				Assert.AreEqual		(ie.getJsVariable("TM.testGrid.slickGridVersion").str(), "2.0a1",   	"TM.testGrid.slickGridVersion version didn't match");
				Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()"),							 	"TM.testGrid.getData");										
				Assert.AreEqual		(ie.getJsVariable("TM.testGrid.getData()[0].id").str(), "0", 			"TM.testGrid.getData()[0].id should be 0");			
				Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].uniqueString"), 			"TM.testGrid.getData()[0].uniqueString");
				Assert.That			(ie.getJsVariable("TM.testGrid.getData()[0].uniqueString").str().isGuid(), 	"TM.testGrid.getData()[0].uniqueString should be a guid");			
			}
		}		
		
		[Ignore][Test]
		public void SlickGrid_View_GuidandeItemsMappings()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/DataGrids/SlickGrid_View_GuidandeItemsMappings.html?time=" + DateTime.Now.Ticks);										
				var value = ie.waitForJsVariable("UnitTest_Helper_SlickGridLoaded").str();						
				Assert.AreEqual		(value ,"True",	"UnitTest_Helper_SlickGridLoaded value was not True");
				Assert.IsNotNull	(ie.getJsVariable("TM.testGrid"), 									"TM.testGrid");			
				Assert.AreEqual		(ie.getJsVariable("TM.testGrid.slickGridVersion").str(), "2.0a1",   "TM.testGrid.slickGridVersion version didn't match");
				Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()"),							"TM.testGrid.getData");						
				Assert.That			(ie.getJsVariable("TM.testGrid.getData()") is IEnumerable, 			"TM.testGrid.getData not collection");									
				
				Action testIfTableIsEmpty = 
					()=>{
							Assert.IsNull		(ie.getJsVariable("TM.testGrid.getData()[0]"), 						"TM.testGrid.getData()[0] should be null here");
						};
						
				Action testIfRowsHaveData = 
					()=>{			
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0]"), 						"TM.testGrid.getData()[0] should NOT be null here");
							Assert.AreEqual		(ie.getJsVariable("TM.testGrid.getData()[0].Id").str(), "0", 		"TM.testGrid.getData()[0].Id should be 0");			
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].MappingIndexes"), 		"TM.testGrid.getData()[0].MappingIndexes");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].Guid"), 				"TM.testGrid.getData()[0].Guid");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].LibraryId"), 			"TM.testGrid.getData()[0].LibraryId should be a guid");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].Title"), 				"TM.testGrid.getData()[0].Title");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].Technology"), 			"TM.testGrid.getData()[0].Technology");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].Phase"), 				"TM.testGrid.getData()[0].Phase");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].Type"), 				"TM.testGrid.getData()[0].Type");
							Assert.IsNotNull	(ie.getJsVariable("TM.testGrid.getData()[0].Category"), 			"TM.testGrid.getData()[0].Category");
						};	
				
				testIfTableIsEmpty();
				
				ie.eval("loadDataUsingUniqueStrings()");					
				testIfRowsHaveData();			
				
				ie.eval("clearTable()");
				testIfTableIsEmpty();
				
				ie.eval("loadDataUsingMappedData()");
				testIfRowsHaveData();			
			}
		}	
		
		[Ignore][Test]
		public void DataTable_View_GuidanceItemsMappings()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/DataGrids/DataTable_View_GuidanceItemsMappings.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_DataTable").str();			
				Assert.That(value.str()=="True","value was not True");			
			}
		}

		[Ignore][Test]
		public void DataTable_View_GuidanceItemsMappings_using_TM_API()  
    	{		
    		lock(ie)
    		{
				base.open("html_pages/_UnitTest_Helpers/DataGrids/DataTable_View_GuidanceItemsMappings_using_TM_API.html?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_DataTable").str();			
				Assert.That(value.str()=="True","value was not True");			
			}
		} 
		
    	[Test]
    	public void Panel_LibrariesView()
    	{
    		lock(ie)
    		{
				base.open("html_Pages/_UnitTest_Helpers/GuiObjects/Panel_LibrariesView.html?time=" + DateTime.Now.Ticks); 
				var value = ie.waitForJsVariable("TM.Debug.UnitTest_Message").str();			
				Assert.AreEqual(value.str(), "Test Complete","UnitTest_Message");			
			}
    	}
    	
    	[TestFixtureTearDown]
    	public void close_IE()
    	{  	
    		lock(ie)
    		{
    			"ok: close_IE (in {0} seconds)".format(Test_TM.CLOSE_BROWSER_IN_SECONDS).jQuery_Append_Body(ie);
    			base.close_IE_Object();    		
    		}
    	}
    	
    	
    }
}