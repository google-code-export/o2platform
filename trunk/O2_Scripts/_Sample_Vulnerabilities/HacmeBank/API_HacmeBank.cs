// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods; 
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.IE.ExtensionMethods;
using SHDocVw;
using WatiN.Core;
using O2.XRules.Database.Utils.O2;
using O2.XRules.Database.APIs;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.SharpDevelop.ExtensionMethods;

//O2File:WatiN_IE_ExtensionMethods.cs    
//O2File:WatiN_IE.cs
//O2Ref:Interop.SHDocVw.dll
//O2Ref:WatiN.Core.1x.dll
//O2File:DotNet_ViewState.cs
 
namespace O2.XRules.Database.HacmeBank
{
    public class API_HacmeBank
    {    
    	public string Url_Website { get; set; }
    	public string Url_WebServices { get; set; }
 		
 		public WatiN_IE ie;   
 		
    	public API_HacmeBank() : this("80")
    	{}
 
    	public API_HacmeBank(string websitePort) : this(websitePort, null)
    	{    	    	
    	}
    	
    	public API_HacmeBank(string websitePort, WatiN_IE watinIE) 
    	{
    		Url_Website = "http://localhost:{0}/HacmeBank_v2_Website".format(websitePort);
    		if (watinIE.isNull())
    			ie = "".ie(0,450,800,700);  
    		else
    			ie = watinIE;
    	}
    	
 
    	public API_HacmeBank login()
    	{
    		return(login(1));
    	}
    	
    	public API_HacmeBank login(int id)
    	{
    		switch (id)
    		{
    			case 1:
    				return login("jm", "jm789");    				
    			case 2:
    				return login("jv", "jv789");
    			case 3:
    				return login("jc", "jc789");    				
    		}
    		return this;
    	}
 		public API_HacmeBank loginPage()
 		{
 			var loginUrl = "{0}/aspx/login.aspx".format(Url_Website);
    		ie.open(loginUrl); 
    		return this;
 		}
 		
    	public API_HacmeBank login(string userName, string password)
    	{
    		loginPage();
			ie.field("txtUserName").value(userName);
			ie.field("txtPassword").value(password);
			ie.button("Submit").click();
			return this;
    	}
    	
    	public API_HacmeBank adminSection()
    	{
    		var adminLink = "Admin Section";
    		if (ie.hasLink(adminLink))
    			ie.click(adminLink);
    		else
    			"[API_HacmeBank][adminSection] could not find admin link: {0}".format(adminLink); 
    		return this;
    	}
    	
    	public API_HacmeBank loginToadminSection()
    	{
    		this.adminSection(); 
    		var response = ie.viewState().ViewState_Values[12];
			ie.set_Value("_ctl3:txtResponse", response); 
			ie.click("Login");
			return this;
    	}
    }
}
