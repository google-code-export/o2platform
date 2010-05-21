// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.Interfaces.O2Core;
using O2.DotNetWrappers.ExtensionMethods;

using JoeBlogs;
using JoeBlogs.Structs;
//O2Ref:WordPress_XmlRpc_JoeBlogs.dll


namespace O2.Script
{
    public class WordPressAPI
    {    
    	public string WordPressServer {get; set;}
        public string WordPressXmlRpc { get; set; } 
    	public WordPressWrapper wordPressWrapper { get; set; }
    	public bool LoggedIn { get; set; }    	    

    	public WordPressAPI(string wordPressServer)
    	{               
            WordPressXmlRpc = wordPressServer;
            if (WordPressXmlRpc.Contains("/xmlrpc.php").isFalse())
                WordPressXmlRpc += "/xmlrpc.php";
            if (WordPressXmlRpc.lower().starts("http").isFalse())
                WordPressXmlRpc = "http://" + WordPressXmlRpc;
            WordPressServer = WordPressXmlRpc.replace("/xmlrpc.php","");
    	}    	    	  
    	    	
    }
    
    public static class WordPressAPI_ExtensionMethods
    {
    
    	public static WordPressAPI login(this WordPressAPI wpApi, string username, string password)
    	{
            wpApi.wordPressWrapper = new WordPressWrapper(wpApi.WordPressXmlRpc, username, password);    		
    		wpApi.LoggedIn = wpApi.loggedIn();
    		return wpApi;
    	}  
    	//DC: need to find a better way to do this
    	public static bool loggedIn(this WordPressAPI wpApi)
    	{
    		try
    		{
    			wpApi.wordPressWrapper.GetUserInfo();
    			return true;
    		}
    		catch
    		{
    			return false;
    		}
    	}
    	
    	public static string post(this WordPressAPI wpApi, string title, string body)
    	{
    	
    		var post = new Post();
			post.dateCreated = DateTime.Now;
			post.title = title;
			post.description = body;
			return wpApi.wordPressWrapper.NewPost(post, true);
//			return wpApi;
		}
		
		public static string post_from_MediaWiki(this WordPressAPI wpApi, O2MediaWikiAPI wikiApi, string mediaWikiPage)
		{
			return wpApi.post_from_MediaWiki(wikiApi, mediaWikiPage, mediaWikiPage);
		}
		
		public static string post_from_MediaWiki(this WordPressAPI wpApi, O2MediaWikiAPI wikiApi, string mediaWikiPage, string postTitle)
		{
			try
			{
				var code = wikiApi.parsePage(mediaWikiPage);  
				var htmlDocument = new HtmlAgilityPack.HtmlDocument();
				htmlDocument.LoadHtml(code);
				
				// remove MediaWiki comments
				foreach(var node in  htmlDocument.DocumentNode.ChildNodes)
					if (node is HtmlAgilityPack.HtmlCommentNode)
						htmlDocument.DocumentNode.RemoveChild(node);
				
				// fix images links
				foreach(var a in htmlDocument.DocumentNode.SelectNodes("//img"))
				{
					var src = a.Attributes["src"];
					if (src!= null)	
						if (src.Value.starts("/"))
							src.Value = wikiApi.HostUrl + src.Value; 		
				}
				
				// fix a href links
				foreach(var a in htmlDocument.DocumentNode.SelectNodes("//a"))
				{
					var href = a.Attributes["href"];
					if (href!= null)	
						if (href.Value.starts("/"))
							href.Value = wikiApi.HostUrl + href.Value; 		
				}
					
				var postBody = htmlDocument.DocumentNode.OuterHtml;
                var messageToAppend = "[automatic O2 comment]:" + "<hr>" +
                                      "<b>Note:</b> This blog post was created by an <a href='http://www.o2platform.com'>O2 Script</a> and is a copy of the MediaWiki page with the title <i>'{0}'</i> located at: <a href='{1}'>{1}</a>"
                                      .format(mediaWikiPage, wikiApi.pageUrl(mediaWikiPage)).line() +
                                      "<b>Exported on</b>:{0}"
                                      .format(DateTime.Now.ToShortDateString());
								      
				postBody = postBody.add(messageToAppend);
				return wpApi.post(postTitle, postBody);
			}
			catch(Exception ex)
			{
                ex.logWithStackTrace("in WordPressAPI.post_from_MediaWiki, for MediaWikiAPI page '{0}'".format(mediaWikiPage));
				return "";
			}			
		}
    }
        
}
