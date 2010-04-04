// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.XRules.Database.O2Utils;
using O2.External.SharpDevelop.ExtensionMethods;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Extensions;
//O2Ref:Dimebrain.TweetSharp.dll

namespace O2.Script
{	
	public class test
	{
		public void testPost()
		{
			var twitterAPI = new O2TwitterAPI("useo2","---");	
			if (twitterAPI.login().isFalse())
				"login failed".error();				
			else
			{
				if (twitterAPI.update("This is a test using O2TwitterAPI"))
					"Post OK".debug();
				else
					"Post error".error();				
			}											
		}
	}

	public class O2TwitterAPI
	{
		public string Username { get; set;}
		public string Password { get; set;}
		public ITwitterStatuses Statuses { get; set;} 
		//public TwitterResult LastResult { get; set;} 
		
		public O2TwitterAPI(string username, string password)
		{
			Username = username;
			Password = password;
		}
		
		public bool login()
		{
			var twitter = FluentTwitter.CreateRequest().AuthenticateAs(Username,Password); //.Statuses.OnUserTimeline().AsJson();
			Statuses = twitter.Statuses();
			//var jason =  statuses.OnUserTimeline().AsJson();
			var result = Statuses.OnUserTimeline().AsJson().Request();
			//show.info(LastResult);
			return checkResponse(result);
		}
		
		public bool update(string updateString)
		{
			var result = Statuses.Update(updateString).AsJson().Request();
			return checkResponse(result);
		}
		
		public bool checkResponse(TwitterResult result)
		{
			return result.ResponseHttpStatusDescription == "OK";
		}
	}

    public static class Twitter_ExtensionClasses
    {    
    	
    }
}