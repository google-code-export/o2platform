using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database._Rules._Interfaces;
using O2.Views.ASCX.ExtensionMethods;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Extensions;
//O2Ref:Dimebrain.TweetSharp.dll

namespace O2.XRules.Database._Rules.APIs
{
    public class O2TwitterAPI
    {
        public string Username { get; set; }
        internal string Password { get; set; }
        public TwitterUser UserLoggedIn { get; set; }
        public ITwitterStatuses Statuses { get; set; }
        public IFluentTwitter Twitter { get; set;}
        public bool IsLoggedIn { get;set; }
        //public TwitterResult LastResult { get; set;} 
		
		public O2TwitterAPI()
		{
		}		        
        
        public bool login(ICredential credential)
		{
			return login(credential.UserName, credential.Password);
		}
		
        public bool login(string username, string password)        
        {
        	Username = username;
            Password = password;           
            return login();
        }
        
        public bool login()
        {
        	try
        	{
        		"login to Twitter under user:{0}".info(Username);
            	this.Twitter = FluentTwitter.CreateRequest().AuthenticateAs(Username, Password); //.Statuses.OnUserTimeline().AsJson();            	            	
            	var response = this.Twitter.Account().VerifyCredentials().AsJson().Request();            	            	
            	IsLoggedIn = response.ok();
            	if (IsLoggedIn)
            	{            		
            		this.Statuses = this.Twitter.Statuses();
            		this.UserLoggedIn = response.AsUser();
            		"Sucessfully connected to twitter user: '{0}' (id:{1})".info(this.UserLoggedIn.Name, this.UserLoggedIn.Id);
            	}
            	else
            		"Failed to connect to twitter user {0}".error(Username);
            		
            	return IsLoggedIn;
            }
            catch(Exception ex)
            {
            	ex.log("[in O2TwitterAPI.login");
            }
            return false;
        }       

        
    }

    public static class Twitter_ExtensionClasses
    {
    
    	public static List<TwitterStatus> tweets(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.tweets(20);
    	}
    	
    	public static List<TwitterStatus> tweets(this O2TwitterAPI twitterAPI, int itemsToFetch)
    	{
    		return twitterAPI.user_Timeline(itemsToFetch);
    	}
    	
    	public static List<string> tweetsText(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.tweetsText(20);
    	}
    	
    	public static List<string> tweetsText(this O2TwitterAPI twitterAPI, int itemsToFetch)
    	{
    		return (from twitterStatus in twitterAPI.user_Timeline(itemsToFetch)
    				select twitterStatus.Text)
    			   .ToList();
    	}
    	
    	public static List<TwitterStatus> user_Timeline(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.user_Timeline(20);
    	}
    	
    	public static List<TwitterStatus> user_Timeline(this O2TwitterAPI twitterAPI, int itemsToFetch)
    	{
    		try
    		{
    			//twitterAPI.Statuses = twitterAPI.Twitter.Statuses();
    			return twitterAPI.Statuses
	    						 .OnUserTimeline()
	    						 .Take(itemsToFetch)
	    						 .AsJson()
	    						 .Request()
	    						 .AsStatuses()			
					 			 .ToList();
			}
			catch(Exception ex)
			{
				ex.log("in O2TwitterAPI.user_Timeline", true);
				return new List<TwitterStatus>();
			}			
    	}
    	
    	public static List<TwitterStatus> home(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.home(20);
    	}
    	
    	public static List<TwitterStatus> home(this O2TwitterAPI twitterAPI, int itemsToFetch)
    	{
    		return twitterAPI.home_Timeline(itemsToFetch);
    	}
    	
    	public static List<string> homeText(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.homeText(20);
    	}
    	
    	public static List<string> homeText(this O2TwitterAPI twitterAPI, int itemsToFetch)
    	{
    		return (from twitterStatus in twitterAPI.home_Timeline(itemsToFetch)
    				select twitterStatus.Text)
    			   .ToList();
    	}
    	
    	public static List<TwitterStatus> home_Timeline(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.home_Timeline(20);
    	}
    	
    	public static List<TwitterStatus> home_Timeline(this O2TwitterAPI twitterAPI, int itemsToFetch)
    	{
    		return twitterAPI.Statuses
    						 .OnHomeTimeline()
    						 .Take(itemsToFetch)
    						 .AsJson()
    						 .Request()
    						 .AsStatuses()
    						 .ToList();
    	}
    	
    	public static List<TwitterUser> followers(this O2TwitterAPI twitterAPI)
    	{
    		return  twitterAPI.Twitter
    						  .Users()
    						  .GetFollowers()
    						  .AsJson()
    						  .Request()
    						  .AsUsers()
    						  .ToList();
    	}
    	
    	public static List<TwitterUser> following(this O2TwitterAPI twitterAPI)
    	{
    		return  twitterAPI.Twitter
    						  .Users()
    						  .GetFriends()
    						  .AsJson()
    						  .Request()
    						  .AsUsers()
    						  .ToList();
    	}
    	    	
    	
    	/*public static List<TwitterStatus> timeline(this O2TwitterAPI twitterAPI)
    	{
    		return twitterAPI.Statuses.OnUserTimeline().AsJson().Request().AsStatuses().ToList();
    	}*/
    	
    	public static List<string> str(this List<TwitterStatus> twitterStatuses)
    	{
    		var tweets = new List<String>();
    		foreach(var twitterStatus in twitterStatuses)
    			tweets.add("{0} : {1} : {2}".format(twitterStatus.CreatedDate ,  twitterStatus.User.Name, twitterStatus.Text));
    		return tweets;
    	}
    	
    	public static List<string> str(this List<TwitterUser> twitterUsers)
    	{
    		var users = new List<String>();
    		foreach(var twitterUser in twitterUsers)
    			users.add("{0} : {1} : {2}".format(twitterUser.Name ,  twitterUser.Location, twitterUser.Description));
    		return users;
    	}
    	
    	public static bool newTweet(this O2TwitterAPI twitterAPI, string tweetText)
    	{
    		return twitterAPI.update(tweetText);
    	}
		public static bool update(this O2TwitterAPI twitterAPI, string updateString)
        {	
        	// not sure why I can 't user the twitterAPI.Statuses for this (but I'm getting an error on the next request to user_timeline)
        	var result = FluentTwitter.CreateRequest().AuthenticateAs(twitterAPI.Username, twitterAPI.Password).Statuses()
            						  .Update(updateString).AsJson().Request();
            return result.ok();
        }
        
        public static bool ok(this TwitterResult result)
        {
            return result.ResponseHttpStatusDescription == "OK";
        }
        
        // GUI
        
        public static T add_TableList_With_Tweets<T>(this T control, List<TwitterStatus> twitterStatuses)
        	where T : Control
        {
	        control.clear();
			var tableList = control.add_TableList();																			
			tableList.add_Columns(new List<string> {"Date","User","Tweet Text"});
			foreach(var twitterStatus in twitterStatuses)   
				tableList.add_Row(new List<string> {twitterStatus.CreatedDate.str() ,  twitterStatus.User.Name, twitterStatus.Text });
            tableList.makeColumnWidthMatchCellWidth();
        	return control;
        }
        
        public static T add_TableList_With_Users<T>(this T control, List<TwitterUser> twitterUsers)
        	where T : Control
        {
	        control.clear();
			var tableList = control.add_TableList();																			
			tableList.add_Columns(new List<string> {"Name","Location","Description"});
            
			foreach(var twitterUser in twitterUsers)   
				tableList.add_Row(new List<string> {twitterUser.Name ,  twitterUser.Location, twitterUser.Description});
            tableList.makeColumnWidthMatchCellWidth();
        	return control;
        }
        
        
    }
}
