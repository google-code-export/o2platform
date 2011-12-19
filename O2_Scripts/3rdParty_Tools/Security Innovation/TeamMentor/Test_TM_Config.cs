// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

//O2Ref:nunit.framework.dll     

namespace O2.SecurityInnovation.TeamMentor
{				
    public class Test_TM
    {	
    	public static string IPAddress		 = "127.0.0.1";
    	public static int 	 Port		 	 = 12355;
    	public static string tmServer 		 = "http://{0}.:{1}/".format(IPAddress,Port);     	
    	
    	public static string tmEmptyPage 	 = "{0}{1}?time={2}".format(tmServer, "html_pages/emptyPage.html" , DateTime.Now.Ticks);
    	public static string tmWebServices 	 = "{0}{1}".format(tmServer,"Aspx_Pages/TM_WebServices.asmx");
    	public static string currentHomePage = "{0}{1}".format(tmServer, "Aspx_Pages/TM_3_0_with_Panels.aspx");
    	public static string invalidPage 	 = "{0}{1}".format(tmServer, "asdasdasd.aspx");
    	
    	public static string tmSourceCode	  = @"C:\_WorkDir\SI\_TeamMentor-v3.0_Latest\"; 
    	public static string tmWebSiteFolder  =  tmSourceCode.pathCombine(@"Web Applications\TM_Website");
    	public static string tmConfigFile  =  tmWebSiteFolder.pathCombine(@"TmConfig.config");
    	public static string cassiniWebServer =  tmSourceCode.pathCombine(@"WebServer\CassiniDev.exe");    	    	
    	
    	public static int CLOSE_WINDOW_IN_SECONDS = 0;
    	public static int CLOSE_BROWSER_IN_SECONDS = 0;
    	
		public static bool IsWebServerUp = false;  // make this static so that we only try to get the html from the server once
		
   	}
}
