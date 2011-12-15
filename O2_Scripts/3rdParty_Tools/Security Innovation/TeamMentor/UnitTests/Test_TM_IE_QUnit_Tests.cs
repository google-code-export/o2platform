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
    public class Test_TM_IE_QUnit_Tests : Test_TM_IE
    {    	    	    
    	public string baseFolder;
    	
    	public Test_TM_IE_QUnit_Tests()
		{			
			baseFolder = "html_pages/_UnitTest_Helpers/TM_qUnit_Tests/";
			var ieKey = "Test_TM_IE_QUnit_Tests_";
			base.set_IE_Object(ieKey);	
			Assert.That(ie.notNull(), "ie object was null");	
			Test_TM.CLOSE_BROWSER_IN_SECONDS = 4;    
			WatiN_IE_ExtensionMethods.WAITFORJSVARIABLE_MAXSLEEPTIMES = 20;
			
		}
		
		public void executeQUnitTestFile(string QUnitFile)
		{
			lock(ie)
    		{
				base.open(QUnitFile +"?time=" + DateTime.Now.Ticks); 						
				var value = ie.waitForJsVariable("UnitTest_Helper_QUnitExecutionCompleted");			
				Assert.That(value.str()=="True","value was not True");    	
				var lastResult = ie.getJsVariable("TM.QUnit.lastExecution");
				Assert.IsNotNull(lastResult, "TM.QUnit.lastExecution");
				var total =  ie.getJsVariable("TM.QUnit.lastExecution.total").str().toInt();
				var passed = 	 ie.getJsVariable("TM.QUnit.lastExecution.passed").str().toInt();
				var failed = ie.getJsVariable("TM.QUnit.lastExecution.failed").str().toInt();
				Assert.That(total > 0, "total was 0");
				Assert.AreEqual(failed ,  0, "Some tests failed");
				Assert.AreEqual(total , passed, "total != pass");			
			}
		}
		
		[Test]
    	public void TM_All_StandAlone_QUnit_tests()  
    	{
    		executeQUnitTestFile(baseFolder + "TM_All_StandAlone_QUnit_tests.html");
    	} 
    	
    	[Test]
    	public void TM_GuiObjects()  
    	{
    		executeQUnitTestFile(baseFolder + "html/TM_GuiObjects.html");
    	}     	    	    	
    	
    	[Test]
    	public void TM_AppliedFilters()  
    	{
    		executeQUnitTestFile(baseFolder + "html/TM_AppliedFilters.html");
    	}    
    	
    	[Test]
    	public void TM_GUI_JQueryUIl()  
    	{
    		executeQUnitTestFile(baseFolder + "html/TM_GUI_JQueryUI.html");
    	}    
    	
    	[TestFixtureTearDown]
    	public void close_IE()
    	{  	
    		lock(ie)
    		{
    			"ok: close_IE (in {0} seconds)".format(Test_TM.CLOSE_BROWSER_IN_SECONDS).jQuery_Append_Body(ie);
    		//	base.close_IE_Object();    		
    		}
    	}
	}
}