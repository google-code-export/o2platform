// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using NUnit.Framework;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database.Utils;

//O2File:Test_TM_Config.cs

namespace O2.SecurityInnovation.TeamMentor
{			
	[TestFixture]
    public class Test_TM_Setup
    {	    
    	[SetUp]
    	public void startServers()
    	{
    		startServer(Test_TM.Port, Test_TM.tmWebSiteFolder);	
    	}
    	 
    	[Test]	
    	public void check_Config_Settings()
    	{
    		Assert.That(Test_TM.tmServer.isUri(), 				"tmServer not Uri");    		
    		Assert.That(Test_TM.tmEmptyPage.isUri(), 			"tmEmptyPage not Uri");
    		Assert.That(Test_TM.invalidPage.isUri(), 			"invalidPage not Uri");
    		Assert.That(Test_TM.tmWebServices.isUri(), 			"tmWebServices not Uri");
    		Assert.That(Test_TM.currentHomePage.isUri(), 		"currentHomePage not Uri");
    		Assert.That(Test_TM.tmFolder.dirExists(),			"Test.TM.tmFolder not found");
    		Assert.That(Test_TM.tmWebSiteFolder.dirExists(),	"Test.TM.tmWebSiteFolder not found");
    		Assert.That(Test_TM.cassiniWebServer.fileExists(),	"Test.TM.cassiniWebServer not found");     		
    	}
    	
    	public void startServer(int port, string folder)
    	{
    		var parameters = "/port:{0} /portMode:Specific /path:\"{1}\"".format(port,folder);
			Test_TM.cassiniWebServer.startProcess(parameters);		    		
    	}
    	
    	[Test]	
    	public void startLocalWebServer()
    	{
    		var webServerProcessName= "CassiniDev";    		
    		 
    		Action<int,string> stopAndStartServer =  
    			(port, folder)=>{    			
    								var currentServers = Processes.getProcessesCalled(webServerProcessName);
    								foreach(var currentServer in currentServers)
    								{
    									currentServer.stop();
//    									Processes.getProcessesCalled(webServerProcessName).stop();						    			
						    			currentServer.WaitForExit();
						    		}
						    		Assert.That(Processes.getProcessCalled(webServerProcessName).isNull(), "There should be no {0} processes at this stage".format(webServerProcessName));    		
						    		startServer(port, folder);
						    		Assert.That(Processes.getProcessesCalled(webServerProcessName).size()==1, "There should be 1 {0} processes at this stage".format(webServerProcessName));
						    		Assert.That("http://127.0.0.1:{0}/".format(port).html().valid(), "could not get html from website");
								};	
			
			stopAndStartServer(Test_TM.Port+1000, Test_TM.tmWebSiteFolder);			
			stopAndStartServer(Test_TM.Port, Test_TM.tmWebSiteFolder);									    		
    	}
    	
    	[Test]
    	public void check_TM_Urls()
    	{
    		Assert.That(Test_TM.tmServer.html().valid(), 		"tmServer html not valid");
    		Assert.That(Test_TM.tmEmptyPage.html().valid(), 	"tmEmptyPage html not valid");
    		Assert.That(Test_TM.invalidPage.html().inValid(), 	"invalidPage html valid");
    		Assert.That(Test_TM.tmWebServices.html().valid(), 	"tmWebServices html not valid");
    		Assert.That(Test_TM.currentHomePage.html().valid(), "currentHomePage html not valid");    		
    	}
   	}
}
