// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient; 
using System.Collections.Generic;    
using System.Diagnostics;   
using System.Text;
using O2.Kernel; 
using O2.Kernel.ExtensionMethods;   
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database.Utils;
using O2.XRules.Database.APIs;  
using NUnit.Framework; 
using SecurityInnovation.TeamMentor.WebClient.WebServices; 
using SecurityInnovation.TeamMentor.Authentication.ExtensionMethods;
using SecurityInnovation.TeamMentor.Authentication.WebServices.AuthorizationRules;
using SecurityInnovation.TeamMentor.WebClient;

//O2File:API_Moq_HttpContext.cs

//O2File:TM_Test_XmlDatabase.cs 

namespace O2.SecurityInnovation.TeamMentor.WebClient.JavascriptProxy_XmlDatabase
{		  
	[TestFixture] 
    public class Test_Authentication : TM_Test_XmlDatabase
    {
    	
	    static Test_Authentication()
     	{
     		TMConfig.BaseFolder = Test_TM.tmWebSiteFolder;    		
     	} 
     	
    	public Test_Authentication() 
    	{     		    		    	 	    	     		
			var httpContextApi = new API_Moq_HttpContext();   
			HttpContextFactory.Context = httpContextApi.HttpContextBase;
			//HttpContextFactory.Current.SetCurrentUserRoles(UserGroup.Admin);
			
			//UserGroup.Admin.setThreadPrincipalWithRoles(); // set current user as Admin
			
			//liveWS_tmWebServices.javascriptProxy.adminSessionID = TMLoginHelper.login_As_Admin(); //set this here					
    	}     	
    	

		[Test]
		public void tmWebServices_Login_PwdInClearText()
		{
			var sessionId = tmWebServices.Login_PwdInClearText("admin","changeme");
			Assert.That(sessionId != Guid.Empty,"sessionID was empty");			
		}
		
		[Test]
		public void checkLoginSessionValues()
		{
			var user = "admin";
			var pwd = "changeme";
			var sessionId = tmWebServices.Login_PwdInClearText(user, pwd);
			Assert.That(sessionId != Guid.Empty,"sessionID was empty");
			Assert.AreEqual(tmWebServices.sessionID, sessionId, "tmXmlDatabase.sessionId");			
			Assert.AreEqual(user, tmWebServices.currentUser.UserName, "tmXmlDatabase.currentUser");			
		}
		
		[Test]
		public void tmWebServices_Current_SessionID_Current_User_GetCurrentUserRoles()
		{
			//create test user
			var user = "test_user_aaa";
			var pwd = "bb";						
			var newUser = tmXmlDatabase.newUser_ClearTextPassword(user, pwd);
			
			//test on tmXmlDatabase
			var sessionId = tmXmlDatabase.login_PwdInClearText(user, pwd); 
			Assert.That(sessionId != Guid.Empty, "sessionId was empty");
			var userRoles = sessionId.session_UserRoles(); 
			Assert.AreEqual(userRoles.size(), 1, "userRoles size");
			Assert.AreEqual(UserRole.ReadArticlesTitles, userRoles[0], "first userRole");						
			
			//test on tmWebServices
			sessionId = tmWebServices.Login_PwdInClearText(user, pwd);
			Assert.AreEqual(sessionId, tmWebServices.Current_SessionID(), "tmWebServices.CurrentSessionID");
			Assert.AreEqual(user, tmWebServices.Current_User().UserName, "tmWebServices.CurrentSessionID");
			var roles = tmWebServices.GetCurrentUserRoles();			
			Assert.AreEqual(roles.size(), 1, "userRoles size");
			Assert.AreEqual("ReadArticlesTitles", roles[0], "first userRole");						
			 
			"deleting user".info(); 
			UserGroup.Admin.setThreadPrincipalWithRoles(); // set current user as Admin
			
			//delete user
			Assert.That(tmWebServices.javascriptProxy.DeleteUser(newUser).isTrue() , "failed to test user");						
			
		}
/*    	 
    	///**********************
		///***  TMLoginHelper methods
		///*** 
    	
		[Test]
    	public string deleteAllUsers_ExceptAdmin()
    	{
    		var adminSessionID = TMLoginHelper.login_As_Admin();
    		var users = authentication.users(adminSessionID); 
			"There are {0} users in the database".info(users.size());
			foreach(var user in users)
				if (user.UserName!= "admin")
				{
					"deleting user: '{0}' with ID: '{1}'".info(user.UserName, user.UserID); 
					authentication.DeleteUser(adminSessionID,user.UserID);
				}
			users =  authentication.users(adminSessionID);			
			Assert.That(users.size() == 1 , "There should be 1 users in the db and there were {0}".format(users.size()));
			return "ok: deleteAllUsers_ExceptTestOnes";
    	}
    	
    	[Test] 
    	public string checkIfUsersExist_Reader_and_Test()
    	{
    		var adminSessionID = TMLoginHelper.login_As_Admin();
    		if (authentication.user(adminSessionID,"test").isNull()) 
    		{
    			"Creating user 'test'".info();
				authentication.CreateUser(adminSessionID ,"test" , new TMUtils().createPasswordHash("test", "123qwe") ,"email@nowhere.com" ,"test" ,"..." ,"note"); 
			}
			Assert.That(authentication.user(adminSessionID,"test").notNull(), "failed to create/get user 'test'");
			if (authentication.user(adminSessionID,"reader").isNull()) 
			{
				"Creating user 'reader'".info();
				authentication.CreateUser(adminSessionID ,"reader" , new TMUtils().createPasswordHash("reader", "123qwe") ,"email@nowhere.com" ,"reader" ,"..." ,"note"); 	
			}
			Assert.That(authentication.user(adminSessionID,"reader").notNull(), "failed to create/get user 'reader'");	

    		return "ok: checkIfUsersExist_Reader_and_Test";
    	}
    	

    	[Test] 
    	public Guid TMHelper_login_As_Admin()
    	{
    		var adminSessionID = TMLoginHelper.login_As_Admin();
    		Assert.That(adminSessionID.notNull() && adminSessionID != Guid.Empty, "Failed to login_As_Admin");
    		return adminSessionID;
    	}
    	
    	[Test] 
    	public Guid TMHelper_login_As_Reader()
    	{
    		var adminSessionID = TMLoginHelper.login_As_Reader();
    		Assert.That(adminSessionID.notNull() && adminSessionID != Guid.Empty, "Failed to login_As_Reader");
    		return adminSessionID;
    	
    	}
    	[Test] 
    	public Guid TMHelper_login_As_Test()
    	{
    		var adminSessionID = TMLoginHelper.login_As_Test();
    		Assert.That(adminSessionID.notNull() && adminSessionID != Guid.Empty, "Failed to login_As_Test");
    		return adminSessionID;
    	}
    	
    	///**********************
		///*** webMethod_IsRbacDisabled 
		///*** 
    	[Test]
    	public string webMethod_IsRbacDisabled()
    	{   
    		var rbacDisabled = TMLoginHelper.authentication.IsRbacDisabled();
			Assert.That(rbacDisabled.isFalse(), "RCBAC is Disabled");
			return "RBAC checks are enabled";
    	}
    	
    	
    	///**********************
		///*** webMethod_IsUserAdmin
		///*** 
    	[Test]
    	public string webMethod_IsUserAdmin()
    	{   
    		var adminGuid = TMLoginHelper.login_As_Admin(); 
    		var readerGuid = TMLoginHelper.login_As_Reader(); 
    		Assert.That(adminGuid != Guid.Empty && readerGuid!=Guid.Empty , "could not get test SessionID Guids");
    		
			teamMentorSecurity.CredentialsValue = new Credentials() {AdminSessionID = adminGuid } ;
			var response = teamMentorSecurity.IsUserAdmin();
			Assert.That(response, "Is User Admin was false");
			
			teamMentorSecurity.CredentialsValue = new Credentials() {AdminSessionID = readerGuid } ;
			response = teamMentorSecurity.IsUserAdmin();
			Assert.That(response.isFalse(), "Is User Reader was maked as Admin");
			
			return "ok: webMethod_IsUserAdmin";
		}    	
    	
    	///**********************
		///*** webMethod_IsUserAdmin
		///*** 
    	[Test]
    	public string webMethod_LoginUsingSoapCredentials()
    	{   
    	 	var adminSessionID = Guid.NewGuid();
    	 	teamMentorSecurity.CredentialsValue = new Credentials() {AdminSessionID = adminSessionID } ;
    	 	Assert.That(teamMentorSecurity.IsUserAdmin().isFalse() ,"random adminSessionID should not be an admin");    	 	
			//login as Admin    		
    		teamMentorSecurity.CredentialsValue = new Credentials() { User = "admin",Password = TMLoginHelper.password(1) , AdminSessionID= adminSessionID};    					
    		//"teamMentorSecurity.LoginUsingSoapCredentials():{0}".debug(teamMentorSecurity.LoginUsingSoapCredentials());
			Assert.That(onlineStorage.LoginUsingSoapCredentials(), "(via soap) failed to login as admin");
			Assert.That(teamMentorSecurity.IsUserAdmin() ," adminSessionID after correct authentication be be an admin");    	 	
			//fail to login as admin
			teamMentorSecurity.CredentialsValue = new Credentials() { User = "admin",Password = 10.randomLetters() };						
			Assert.That(onlineStorage.LoginUsingSoapCredentials().isFalse(), "should had not successed");
			Assert.That(teamMentorSecurity.IsUserAdmin().isFalse() ," adminSessionID after faild correct authentication be be NOT an admin");    	 	
			return "ok: webMethod_LoginUsingSoapCredentials";
		}
		
		///**********************
		///*** webMethods_DemandPrivileges_Admin_and_DemandPrivileges_ReadArticles
		///*** 
    	[Test]
    	public string webMethods_DemandPrivileges_Admin_and_DemandPrivileges_ReadArticles()
		{
			var randomSessionID = Guid.NewGuid();			//TMLoginHelper.login_As_Admin();  ;//Guid.NewGuid();			
			teamMentorSecurity.CredentialsValue = new Credentials() {AdminSessionID = randomSessionID } ;
			
			try { teamMentorSecurity.DemandPrivileges_Admin(); } 
			catch(Exception ex) { Assert.That(ex.Message.contains("Request for principal permission failed"), "wrong exception");}
			
			try { teamMentorSecurity.DemandPrivileges_ReadArticles(); } 
			catch(Exception ex) { Assert.That(ex.Message.contains("Request for principal permission failed"), "wrong exception."); }
						
			var adminSessionID = TMLoginHelper.login_As_Admin();  
			teamMentorSecurity.CredentialsValue = new Credentials() {AdminSessionID = adminSessionID } ;
			teamMentorSecurity.DemandPrivileges_Admin();
			
			var readerSessionID = TMLoginHelper.login_As_Reader();  
			teamMentorSecurity.CredentialsValue = new Credentials() {AdminSessionID = readerSessionID } ;
			teamMentorSecurity.DemandPrivileges_ReadArticles();
						
			return "ok: webMethods_DemandPrivileges_Admin_and_DemandPrivileges_ReadArticles";
		}
    	    
    }
    */
    	
    }
}
