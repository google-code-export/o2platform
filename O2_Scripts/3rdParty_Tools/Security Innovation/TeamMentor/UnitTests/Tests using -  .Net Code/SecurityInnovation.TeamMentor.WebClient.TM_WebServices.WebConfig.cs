using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;    
using NUnit.Framework;  
using O2.Kernel;   
using O2.Kernel.ExtensionMethods; 
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database.Utils;
  
//O2File:Test_TM_Config.cs

namespace O2.SecurityInnovation.TeamMentor
{		  
	[TestFixture]  
    public class Test_TM_WebServices_WebConfig
    {     			 
    	
    	public Test_TM_WebServices_WebConfig()    
    	{    		    		    		
    	} 
    	     	  
    	[Test]  
    	public void loadWebConfigFile()
    	{   
    		var webConfigFile = Test_TM.tmWebSiteFolder.pathCombine("web.config");
    		Assert.That(webConfigFile.fileExists(), "could not find, web.config file: {0}".format(webConfigFile));
    	}    	
	}       
}