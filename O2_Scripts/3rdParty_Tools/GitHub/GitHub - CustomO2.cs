// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using O2.XRules.Database.Utils;
//O2File:Watin_IE_ExtensionMethods.cs
//O2File:CustomO2.cs  
//O2File:API_GitHub.cs
//O2File:MsysGit.cs
//O2File:ascx_AskUserForLoginDetails.cs
//O2Ref:WindowsFormsIntegration.dll
//O2Ref:RibbonControlsLibrary.dll
//O2Ref:WatiN.Core.1x.dll

namespace O2.XRules.Database.APIs
{
	public class GitHub_CustomO2_Launcher
	{
		public void showGui()
		{
			new GitHub_CustomO2().buildGui();
		}
	}
	
    public class GitHub_CustomO2
    {    
    	public WPF_Ribbon Ribbon {get;set;}
    	public WatiN_IE IE { get; set;}
    	public API_GitHub GitHub {get; set;}
    	
    	public GitHub_CustomO2()
    	{    		
    	}   
    	
    	public GitHub_CustomO2 buildGui()
    	{
    		var topPanel = O2Gui.open<Panel>("GitHub", 1024,600);
    		return buildGui(topPanel);
    	}
    	
    	public GitHub_CustomO2 buildGui(Panel topPanel)
    	{
    		return buildGui(topPanel,false);
    	}
    	
    	public GitHub_CustomO2 buildGui(Panel topPanel, bool onlyAddIe)
    	{  			    		
    		if (onlyAddIe.isFalse())
    		{
				Ribbon = topPanel.add_Ribbon_Above(); 
				Ribbon.title("Git Hub API");								  
				  
				add_Tab_GitHub_Website();
				add_Tab_GitHub_Setup();
				 
				var customO2 = Ribbon.add_Tab("Custom O2");
				customO2.add_RibbonGroup("Group")
						.add_RibbonButton_Script("IE Automation","ascx_IE_ScriptExecution.cs");	 		 
							
				//ribbon.add_Tab_BrowserAutomation();
				Ribbon.add_Tab_MiscTools();
			}
			configureIE();
			IE = topPanel.add_IE();	
			return this;
    	}
    	
    	public GitHub_CustomO2 add_Tab_GitHub_Website()
    	{			
			var gitHubWebsite = Ribbon.add_Tab("GitHub WebSite");
			gitHubWebsite.add_Group("Pages")
						 .add_Button("HomePage", ()=> this.homePage())
						 .add_Button("Login", ()=> this.login());		
			return this;
						 
    	}
    	
    	public GitHub_CustomO2 add_Tab_GitHub_Setup()
    	{			
    		var gitHubSetup = Ribbon.add_Tab("GitHub Setup");
			gitHubSetup.add_Group("Putty")
					   .add_Button("Install MsysGit", ()=> new MsysGit())
					   .add_Button("Install TortoiseGit", ()=>GitHub.install_TortoiseGit())
					   .add_Button("Run PuttyGen", ()=> GitHub.putty_generateKeys());
			gitHubSetup.add_Group("TortoiseSVN")
					   .add_Button("Git Clonet", ()=> GitHub.gitClone());
					   
			return this;
    	}
    	public void configureIE()
    	{
    		"Adding trusted zones to IE".info();
    		"github.com".makeDomainTrusted("");
    		"github.com".makeDomainTrusted("*");
    	}
    }
    
    public static class GitHub_CustomO2_IE_Helpers
    {
    	public static GitHub_CustomO2 openLink(this GitHub_CustomO2 gitHub, string link)
		{
			gitHub.IE.link(link).click();
    		return gitHub;
		}
	}
	
    public static class GitHub_CustomO2_IE
    {    	    
		public static bool inGitHub(this GitHub_CustomO2 gitHub)
    	{
    		return gitHub.IE.url().contains("github.com");
    	}
    	
    	public static bool isLoggedIn(this GitHub_CustomO2 gitHub)
    	{
    		return gitHub.inGitHub() && gitHub.IE.hasLink("Log Out");
    	}
    	
    	public static GitHub_CustomO2 login(this GitHub_CustomO2 gitHub)
    	{
    		var gitHubLogin = @"C:\O2\_USERDATA\accounts.xml".credential("github"); 
    		if (gitHubLogin.isNull())
    			gitHubLogin  = ascx_AskUserForLoginDetails.ask();
    		if (gitHubLogin.notNull())
    			return  gitHub.login(gitHubLogin.username(), gitHubLogin.password()); 
    		"No login credentials were provided".error();
    		return gitHub;
    	}
    	
    	public static GitHub_CustomO2 login(this GitHub_CustomO2 gitHub, string username, string password)
    	{
    		var ie = gitHub.IE;
			ie.open("https://github.com/login");
			ie.field("login").value(username);
			ie.field("password").value(password); 
			ie.button("Log in").click();
			return gitHub; 
		}		
		
		public static GitHub_CustomO2 homePage(this GitHub_CustomO2 gitHub)
    	{
    		gitHub.IE.open("http://github.com");
    		return gitHub;
    	}   
    	
		public static GitHub_CustomO2 accountSettings(this GitHub_CustomO2 gitHub)
		{
			return gitHub.openLink("Account Settings");
		}						
		
		public static GitHub_CustomO2 add_SSH_PublicKey(this GitHub_CustomO2 gitHub)
		{		
			var ie = gitHub.IE;
			var keyPath = "What is the path to the public key to use?".askUser();
			if (keyPath.fileExists().isFalse())
			{
				"no key file provided".error();
			}
			else
			{
				var title = System.IO.Path.GetFileNameWithoutExtension(keyPath);
				var key = keyPath.fileContents();
				gitHub.homePage();
				gitHub.accountSettings();
				ie.link("SSH Public Keys").click();
				ie.link("Add another public key").click(); 	
				ie.field("public_key[title]").value(title); 
				ie.field("public_key[key]").value(key);
				ie.button("<SPAN>Add key</SPAN>").click();	 
			}
			return gitHub;
		}
    }
}