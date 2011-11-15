// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; 
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database.Utils;
//O2File:API_IE_ExecutionGUI.cs
//O2File:WatiN_IE_ExtensionMethods.cs    
//O2Ref:WatiN.Core.1x.dll

namespace O2.XRules.Database.APIs 
{	
	
    public class IE_TeamProfessor : API_IE_ExecutionGUI
    {   
    	
    	public IE_TeamProfessor(WatiN_IE ie) : base(ie)
    	{
    	 	config();
    	}
    	
    	public IE_TeamProfessor(Control hostControl)	: base(hostControl) 
    	{    		
    		config();
    	}  
    	
    	public void config()
    	{
    		this.TargetServer = "https://teamprofessor.securityinnovation.com";    		
    		IE_TeamProfessor_ExtensionMethods.ie = this.ie;
    	}    	    	
    	
    	public IE_TeamProfessor open(string virtualPath)
    	{
    		base.open(virtualPath);
    		return this;
    	}
    }
    
    
    public static class IE_TeamProfessor_ExtensionMethods
    {
    	
    	public static WatiN_IE ie ;
    	
    	public static IE_TeamProfessor login(this IE_TeamProfessor ieTeamProfessor, string username, string password)
    	{        		
    		ieTeamProfessor.logoff();
	    	ie.field("User Name").flash().value(username);
			ie.field("Password").flash().value(password);
			ie.button("Enter").flash().click(); 
			return ieTeamProfessor;		
		}
		
		public static IE_TeamProfessor logoff(this IE_TeamProfessor ieTeamProfessor)
		{
			return ieTeamProfessor.open("ed/logoff.asp");			
		}			
		
		public static IE_TeamProfessor myProfile(this IE_TeamProfessor ieTeamProfessor)
		{
			return ieTeamProfessor.open("Portal/UserProfile/UserProfile.aspx");			
		}		
		
		public static IE_TeamProfessor administration(this IE_TeamProfessor ieTeamProfessor)
		{
			return ieTeamProfessor.open("Manager");			
		}	
		
		public static IE_TeamProfessor userList(this IE_TeamProfessor ieTeamProfessor)
		{
			return ieTeamProfessor.open("Manager/User/List.aspx");			
		}
		
		public static IE_TeamProfessor viewProfile(this IE_TeamProfessor ieTeamProfessor, int profileId)
		{
			return ieTeamProfessor.viewProfile("Person|{0}".format(profileId));
		}
		
		public static IE_TeamProfessor viewProfile(this IE_TeamProfessor ieTeamProfessor, string profileValue)
		{			
			ieTeamProfessor.open("Manager/User/UserProfile.aspx?key={0}".format(profileValue.urlEncode()));
			return ieTeamProfessor;			
		}						
    }
    
    public static class IE_TeamProfessor_Actions
    {    					
		
		[ShowInGui(Folder ="root")]
		public static API_IE_ExecutionGUI homePage(this IE_TeamProfessor ieTeamProfessor)
		{
			return ieTeamProfessor.open("ed/portal"); 
		}					
		
		
	}    
}   