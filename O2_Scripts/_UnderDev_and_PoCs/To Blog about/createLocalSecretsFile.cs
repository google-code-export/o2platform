// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.Script
{
    public class CreateLocalScriptFile
    {    
    	public static void test()
    	{    	
    		var secretData = new SecretData();
    		secretData.BTOpenZone = new LoginDetails("aaaa","bbb");
    		var textBox = new System.Windows.Forms.TextBox();
    		var targetFile = @"C:\O2\_USERDATA\secretData.xml";    		
    		secretData.serialize(targetFile);
    		targetFile.fileContents().info();    		
    		
    	}
    	    	    	    	    
    }
    
    public class SecretData
    {
    	public LoginDetails BTOpenZone { get; set; }
    	
    	public SecretData()
    	{}
    }
    
    public class LoginDetails
    {
    	public string UserName { get; set; }
    	public string Password { get; set; }
    	
    	public LoginDetails()
    	{}
    	
    	public LoginDetails(string username, string password)
    	{
    		UserName = username;
    		Password = password;
    		
    	}
    }
}
