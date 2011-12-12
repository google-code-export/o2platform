// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using NUnit.Framework;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database.Utils;

//O2File:_Extra_methods_Collections.cs
//O2File:_Extra_methods_Web.cs
//O2File:_Extra_methods_Misc.cs
//O2File:_Extra_methods_WinForms_Controls.cs

//O2Ref:nunit.framework.dll

namespace O2.SecurityInnovation.TeamMentor
{			
	//[TestFixture]
    public class Test_TM
    {	
    	public static string IPAddress		 = "127.0.0.1";
    	public static int 	 Port		 	 = 12355;
    	public static string tmServer 		 = "http://{0}.:{1}/".format(IPAddress,Port);     	
    	
    	public static string tmEmptyPage 	 = "{0}{1}?time={2}".format(tmServer, "html_pages/emptyPage.html" , DateTime.Now.Ticks);
    	public static string tmWebServices 	 = "{0}{1}".format(tmServer,"Aspx_Pages/TM_WebServices.asmx");
    	public static string currentHomePage = "{0}{1}".format(tmServer, "Aspx_Pages/TM_3_0_with_Panels.aspx");
    	public static string invalidPage 	 = "{0}{1}".format(tmServer, "asdasdasd.aspx");
    	
    	public static string tmFolder 	 	  = @"C:\_WorkDir\SI\_TeamMentor-v3.0_Latest\"; 
    	public static string tmWebSiteFolder  =  tmFolder.pathCombine(@"Web Applications\TM_Website");
    	public static string cassiniWebServer =  tmFolder.pathCombine(@"WebServer\CassiniDev.exe");
    	
    	
    	public static int CLOSE_WINDOW_IN_SECONDS = 0;
    	public static int CLOSE_BROWSER_IN_SECONDS = 0;
    	
    	//[Test]	
    	public void check_Config_Settings()
    	{
    		Assert.That(tmServer.isUri(), 			"tmServer not Uri");    		
    		Assert.That(tmEmptyPage.isUri(), 		"tmEmptyPage not Uri");
    		Assert.That(invalidPage.isUri(), 		"invalidPage not Uri");
    		Assert.That(tmWebServices.isUri(), 		"tmWebServices not Uri");
    		Assert.That(currentHomePage.isUri(), 	"currentHomePage not Uri");
    		    		
    		Assert.That(tmServer.html().valid(), 		"tmServer html not valid");
    		Assert.That(tmEmptyPage.html().valid(), 	"tmEmptyPage html not valid");
    		Assert.That(invalidPage.html().inValid(), 	"invalidPage html valid");
    		Assert.That(tmWebServices.html().valid(), 	"tmWebServices html not valid");
    		Assert.That(currentHomePage.html().valid(), "currentHomePage html not valid");    		
    	}
   	}
}
