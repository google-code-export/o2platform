// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
//O2File:C:\O2\_XRules_Local\ExtensionMethods\extra_WebAutomation.cs

namespace O2.Script
{
    public class O2MediaWikiAPI
    {   
    	public string HostUrl { get; set; }		
    	public string ApiPhp { get; set; }
    	public string IndexPhp { get; set; }
    	public string ReturnFormat { get; set; }
		public string Styles { get; set; }    	
		
		public string Login_Result { get; set; }    	
		public string Login_UserId { get; set; }    	
		public string Login_Username { get; set; }    	
		public string Login_Token { get; set; }    	
		public string Login_CookiePrefix { get; set; }    	
		public string Login_SessionId { get; set; }    	
		public string Login_Cookie { get; set; }		
		
		public O2MediaWikiAPI()
		{
			
		}
		
    	public O2MediaWikiAPI(string apiPhp)     		
    	{
    	  	init(apiPhp);
    	}
    	
    	public void init(string apiPhp)
    	{
    		init(apiPhp.lower().replace("api.php",""), apiPhp,apiPhp.lower().replace("api.php","index.php"));
    	}
    	
    	public void init(string hostUrl, string apiPhp, string indexPhp)
    	{
    		HostUrl = hostUrl;
    		ApiPhp = apiPhp;
    		IndexPhp = indexPhp;
    		ReturnFormat = "xml";
    		Styles = "";
    		//ReturnFormat = "yaml";
    		//ReturnFormat = "json";
    		//ReturnFormat = "php";
    		//ReturnFormat = "wddx";
    		//ReturnFormat = "rawfm";    		
    		//ReturnFormat = "txt";        		
    		//ReturnFormat = "dbg";    
    	}
    	
    	
    	
    	public bool okApiSpec()
    	{
    		return apiSpec() != "";
    	}
    	
    	public string apiSpec()
    	{
            return new Web().getUrlContents(ApiPhp);
    	}
    	
    	public void format(string returnFormat)
    	{
    		ReturnFormat = returnFormat;
    	}
    			  
		
		public string parsePage_Raw(string page)
		{
			try
			{
				var getData = "action=parse&page={0}&format=xml".format(page);			
				return getApiPhp(getData);
            }
            catch (Exception ex)
            {
                ex.log("in O2MediaWikiAPI.parsePage_Raw");
                return null;
            }
        }

        public string parsePage(string page)
        {
            try
            {
                var data = parsePage_Raw(page);
				var value = data.xRoot().element("parse").element("text").value();
				return value.fixCRLF();				
			}
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.parsePage");				
			}
            return null;
		}
		
        public string parseText_Raw(string textToParse)
		{
			try
			{
				var getData = "action=parse&text={0}&format=xml".format(textToParse.urlEncode());			
				var data = postApiPhp(getData);				
				if (data.starts("\n"))              // fix prob with some wikis that return a enter at the top
					data = data.trim();
                return data;
            }            
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.parseText_Raw");
				return null;
			}
		}

		public string parseText(string textToParse)
		{
			try
			{
                var data = parseText_Raw(textToParse);
                if (data != null)
                {
                    var value = data.xRoot().element("parse").element("text").value();
                    return value.fixCRLF();
                }
			}
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.parseText");				
			}
            return null;
		}
		
		public string raw(string page)
		{			
			var getData = "action=raw&title={0}".format(page.urlEncode());
			return getIndexPhp(getData).fixCRLF();
			//var getData = "action=raw&title={0}&format=xml".format(page);			
			//var data = get(getData);
			//return data.xRoot();//.element("parse").element("text").value();			
		}
		
		public string getIndexPhp(string getData)
		{			
			try
			{				
				var uri = new Uri("{0}?{1}".format(IndexPhp,getData));		
				return getRequest(uri);
			}			
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.getIndexPhp");
				return "";
			}
			
		}
		
		public string getApiPhp(string getData)
		{	
			try
			{
				var uri = new Uri("{0}?{1}".format(ApiPhp,getData));		
				return getRequest(uri);
			}			
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.getApiPhp");
				return "";
			}
		}
		
		public string getRequest(Uri uri)
		{
			try
			{
				"sending GET request with: {0}".format(uri.str()).debug();
                var responseData = new Web().getUrlContents(uri.str(), Login_Cookie, false);
				if (responseData != null && responseData.valid())
					"responseData size: {0}".format(responseData.size()).info(); 
				else
					"invalid response data".error();				
				return responseData.trim();
			}			
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.get");
				return "";
			}
		}
		
		public string postApiPhp(string postData)
		{
			try
			{
				"sending POST request with: {0}".format(postData).debug();
                return new Web().getUrlContents_POST(ApiPhp, Login_Cookie, postData);
			}		
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.postApiPhp");
				return "";
			}
		}
		
		
    }
    
    public static class O2MediaWikiAPI_ExtensionMethods
    {	
    	#region login & security 
    	
    	public static string editToken(this O2MediaWikiAPI wikiApi, string page)
    	{
    		return wikiApi.token("edit",page);
    	}
    	
    	public static string token(this O2MediaWikiAPI wikiApi, string action, string page)
    	{
    		var xmlData = wikiApi.getApiPhp("action=query&prop=info&intoken={0}&titles={1}&format=xml".format(action,page)); 
    		return xmlData.xRoot().elementsAll().element("page").attribute("edittoken").value();
    	}
    	
    	public static bool login(this O2MediaWikiAPI wikiApi, string username, string password)
		{
			var postData = @"action=login&lgname={0}&lgpassword={1}&format=xml".format(username,password);			
			var login = wikiApi.postApiPhp(postData).xRoot().element("login");
			wikiApi.Login_Result = login.attribute("result").value();
			if (wikiApi.Login_Result == "Success")
			{
				wikiApi.Login_UserId = login.attribute("lguserid").value();
				wikiApi.Login_Username = login.attribute("lgusername").value();
				wikiApi.Login_Token = login.attribute("lgtoken").value();
				wikiApi.Login_CookiePrefix = login.attribute("cookieprefix").value();
				wikiApi.Login_SessionId = login.attribute("sessionid").value();																

				wikiApi.Login_Cookie = "{0}UserName={1};{0}UserID={2};{0}Token={3};{0}_session={4}".format(
										wikiApi.Login_CookiePrefix, wikiApi.Login_Username,wikiApi.Login_UserId,
										wikiApi.Login_Token, wikiApi.Login_SessionId);																		
				return true;
			}
			else
			{
				wikiApi.Login_UserId = "";
				wikiApi.Login_Username = "";
				wikiApi.Login_Token = "";
				wikiApi.Login_CookiePrefix = "";
				wikiApi.Login_SessionId = "";
				return false;
			}
		} 
    	
    	#endregion
    	
		#region create and edit pages
		
		
		//note: the createonly option doesn't seem to be working
		public static string createPage(this O2MediaWikiAPI wikiApi,string page, string pageContent)
    	{
    		var urlData = "action=edit&format=xml&createonly&token={0}&title={1}&text={2}".format(wikiApi.editToken(page).urlEncode(),page.urlEncode(),pageContent.urlEncode()); 
    		return wikiApi.postApiPhp(urlData);
    	}
		
		public static string editPage(this O2MediaWikiAPI wikiApi,string page, string pageContent)
    	{
    		var urlData = "action=edit&format=xml&nocreate&token={0}&title={1}&text={2}".format(wikiApi.editToken(page).urlEncode(),page.urlEncode(),pageContent.urlEncode()); 
    		return wikiApi.postApiPhp(urlData);
    	}
		
		public static bool exists(this O2MediaWikiAPI wikiApi,string page)
		{
			return wikiApi.raw(page).valid();
		}
	
		
		#endregion
    	
    	#region get wrappers
    	public static string html(this O2MediaWikiAPI wikiApi, string page)
    	{
    		return wikiApi.getPageHtml(page);
    	}
    	    	
    	public static string getPageHtml(this O2MediaWikiAPI wikiApi, string page)
    	{    	
    		var htmlCode = wikiApi.getIndexPhp("action=render&title={0}".format(page));
    		return wikiApi.wrapOnHtmlPage(htmlCode);
    	}

		public static string id(this O2MediaWikiAPI wikiApi, int id)
		{
			return wikiApi.id(id.str());
		}
		
		public static string id(this O2MediaWikiAPI wikiApi, string id)
    	{
    		return wikiApi.getRevision(id);
    	}    	
    	
		public static string getRevision(this O2MediaWikiAPI wikiApi, string id)
    	{
    		var htmlCode = wikiApi.getIndexPhp("action=render&oldid={0}".format(id));
    		return wikiApi.wrapOnHtmlPage(htmlCode);
    	}    	    	
    	
    	public static string wikiText(this O2MediaWikiAPI wikiApi, string page)
    	{
    		return wikiApi.getPageWikiText(page); 
    	}    	    	
    	
    	public static string getPageWikiText(this O2MediaWikiAPI wikiApi, string page)
    	{
    		return wikiApi.raw(page);
    	}
    	
    	#endregion    	    	
    	
    	#region  action=query&prop=*  - helpers
    	
    	public static string action_query_prop(this O2MediaWikiAPI wikiApi,string query, string pages)
    	{
    		var urlData = "action=query&prop={0}&titles={1}&format={2}".format(query,pages,wikiApi.ReturnFormat);
    		return wikiApi.getApiPhp(urlData);
    	}
    	
    	public static List<XElement> getQueryContinueResults(this O2MediaWikiAPI wikiApi, string pages, int rvlimit, 
    														 string propertyName , string continueVarName , string continueValue, 
    														 string dataElement)
    	{
    		var results = new List<XElement>();
    		var cmd = "{0}&rvlimit={1}".format(propertyName,rvlimit);
    		cmd += "&redirects";		// to automatically resolve redirects
    		if (continueValue != "")
    			cmd += "&{0}={1}".format(continueVarName, continueValue);
    		var data = wikiApi.action_query_prop(cmd,pages).xRoot();
    		if (data.elements("query-continue").size() == 0)
    			continueValue = "";
    		else
    			continueValue = data.elements("query-continue").element(propertyName).attribute(continueVarName).Value;
    		
    		results.AddRange(data.elementsAll(dataElement));
    		
    		//continueValue.error();
    		if (continueValue != "")
    			results.AddRange(wikiApi.getQueryContinueResults(pages,rvlimit, propertyName, continueVarName, continueValue, dataElement));
    								//wikiApi.templates(pages, rvlimit, continueValue)
    							//);
    		return results;    		
    	}
    	
    	#endregion 
    	
    	#region  action=query&prop=*  -specific
    	
    	public static XElement info(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.action_query_prop("info",pages).xRoot();
    	}
    	
    	public static List<string> revisions(this O2MediaWikiAPI wikiApi, string page)
    	{
			var revisions = from revision in wikiApi.revisionsRaw(page)
			   		        select (revision.attribute("revid").value() + " - " + 
				   		            revision.attribute("user").value() + " - " + 
				   		            revision.attribute("timestamp").value());    	
			return revisions.ToList();    	
    	}
    	
    	public static List<string> revisionsIds(this O2MediaWikiAPI wikiApi, string page)
    	{
			var revisions = from revision in wikiApi.revisionsRaw(page)
			   		        select (revision.attribute("revid").value());
			return revisions.ToList();    	
    	}
    	
    	public static List<string> revisionsPages(this O2MediaWikiAPI wikiApi, string page)
    	{
    		var pages = new List<string>();
			foreach(var id in wikiApi.revisionsIds(page))
				pages.add(wikiApi.getRevision(id));
			return pages;
    	}
    	
    	public static List<XElement> revisionsRaw(this O2MediaWikiAPI wikiApi, string page)    	
    	{
    		var propertyName = "revisions";
    		var continueVarName = "rvstartid";    			
    		var dataElement = "rev";    		
			var rvlimit = 100;
			
    		return wikiApi.getQueryContinueResults(page, rvlimit, propertyName, continueVarName, "", dataElement);    											   
    	}
    	
    	public static List<String> links(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		var links = new List<String>();
    		links.AddRange(wikiApi.links_Internal(pages));
    		links.AddRange(wikiApi.links_External(pages));
    		return links;
    	}
    	
    	public static List<String> links_Internal(this O2MediaWikiAPI wikiApi, string pages)
    	{    		
    		try	
    		{
	    		pages = pages.replace(" ","_");
	    		var internalLinks = wikiApi.linksRaw_Internal(pages).attributes("title").values();
	    		
	    		// map the internal links that are linked has external
	    		var externalLinks = wikiApi.links_External(pages);
	    		foreach(var externalLink in externalLinks)
	    			if (externalLink.starts(wikiApi.IndexPhp))
	    			{
	    				var newLink = externalLink.replace(wikiApi.IndexPhp,"");
	    				if (newLink.starts("/"))
	    					newLink = newLink.removeFirstChar();    				
	    				var indexOfSharp = newLink.index("#");
	    				if (indexOfSharp > -1)
	    					newLink = newLink.Substring(0, indexOfSharp);
	    				internalLinks.add(newLink);
	    			}
	    		return internalLinks;	    		
    		}		
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.links_Internal");
				return new List<String>();
			}
    	}
    	
    	public static List<XElement> linksRaw_Internal(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		var propertyName = "links";
    		var continueVarName = "plcontinue";    			
    		var dataElement = "pl";    		
			var rvlimit = 100;
			
    		return wikiApi.getQueryContinueResults(pages, rvlimit, propertyName, continueVarName, 
    											   "", dataElement);
    		//var xml = wikiApi.action_query_prop("links",pages);
    		//return xml.xRoot().elementsAll("pl");//.attributes("title").values();  		
    	}

		public static List<string> links_External(this O2MediaWikiAPI wikiApi, string pages)
		{
			try
			{
				return wikiApi.linksRaw_External(pages).values();
			}		
			catch(Exception ex)
			{
				ex.log("in O2MediaWikiAPI.links_External");
				return new List<String>();
			}
		}
		
		public static List<XElement> linksRaw_External(this O2MediaWikiAPI wikiApi, string pages)
    	{    		
    		var propertyName = "extlinks";
    		var continueVarName = "eloffset";    		
    		var dataElement = "el";    		
    		var rvlimit = 10;
    		
    		return wikiApi.getQueryContinueResults(pages, rvlimit, propertyName, continueVarName, "", dataElement);
    	}    	
    	
    	
    	public static XElement langlinks(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.action_query_prop("langlinks",pages).xRoot();
    	}
    	
    	public static List<XElement> images(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		var xml =  wikiApi.action_query_prop("images",pages);
    		return xml.xRoot().elementsAll("im");
    	}
    	
    	public static XElement imageinfo(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.action_query_prop("imageinfo",pages).xRoot();
    	}
    	
    	public static List<string> templates(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.templatesRaw(pages).attributes("title").values();
    	}
    	
    	public static List<XElement> templatesRaw(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		var propertyName = "templates";
    		var continueVarName = "tlcontinue";    			
    		var dataElement = "tl";    		
			var rvlimit = 100;
			
    		return wikiApi.getQueryContinueResults(pages, rvlimit, propertyName, continueVarName, 
    											   "", dataElement);
    	}    	    	    	    	
    	
    	public static string categories(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.action_query_prop("categories",pages);
    	}    	    	
    	
    	public static string categoryinfo(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.action_query_prop("categoryinfo",pages);
    	}
    	
    	public static string duplicatefiles(this O2MediaWikiAPI wikiApi, string pages)
    	{
    		return wikiApi.action_query_prop("duplicatefiles",pages);
    	}
    	
		#endregion    		
		
		#region action=query&list=*
    	
    	public static string action_query_list(this O2MediaWikiAPI wikiApi,string query)
    	{
    		var urlData = "action=query&list={0}&format={1}".format(query,wikiApi.ReturnFormat);
    		return wikiApi.getApiPhp(urlData);
    	}
    	
    	public static string allusers(this O2MediaWikiAPI wikiApi)
    	{
    		return wikiApi.action_query_list("allusers");
    	}
    	
    	public static List<string> recentChanges(this O2MediaWikiAPI wikiApi)
    	{
    		return wikiApi.recentChanges(100);
    	}
    	
    	public static List<string> recentChanges(this O2MediaWikiAPI wikiApi, int itemsToFetch)
    	{    		
    		var changes = new List<String>();
    		foreach(var change in wikiApi.recentChangesRaw(itemsToFetch))
    		{
    			var text = "page:\"{0}\" type:{1} by:{2} at:{3}".format(change.attribute("title").value(), 
    											     change.attribute("type").value(),
    											     change.attribute("user").value(),
    											     change.attribute("timestamp").value());
    			changes.add(text);
    		}
    		return changes;    			
    	}
    	
    	public static List<XElement> recentChangesRaw(this O2MediaWikiAPI wikiApi)
    	{
    		return wikiApi.recentChangesRaw(100);	
    	}
    	
    	public static List<XElement> recentChangesRaw(this O2MediaWikiAPI wikiApi, int itemsToFetch)
		{
			return wikiApi.action_query_list("recentchanges&rcprop=user|title|ids|type|timestamp&rclimit=" + itemsToFetch.str()).xRoot().elementsAll("rc");
	
		}				
		
		public static string exUrlUsage(this O2MediaWikiAPI wikiApi, string url)
		{
			var queryList = "exturlusage&euquery={0}".format(url);
			return wikiApi.action_query_list(queryList);
			//api.php?action=query&list=exturlusage&euquery=www.mediawiki.org
		}
		//allimages, allpages, alllinks, allcategories, , backlinks, blocks, categorymembers, deletedrevs, embeddedin, imageusage, logevents, , search, usercontribs, watchlist, watchlistraw, exturlusage, users, random, protectedtitles
		
		#endregion
		
		#region mist
		
		
		  

		#endregion
		
		
		
    }
    
    public static class Wiki_Html_Utils
    {
    	public static string wrapOnHtmlPage(this O2MediaWikiAPI wikiApi, string htmlCode)
    	{
    		
/*			<!--[if lt IE 5.5000]><link rel="stylesheet" href="/skins/gumax/IE50Fixes.css?207" type="text/css" media="screen" /><![endif]--> 
			<!--[if IE 5.5000]><link rel="stylesheet" href="/skins/gumax/IE55Fixes.css?207" type="text/css" media="screen" /><![endif]--> 
			<!--[if IE 6]><link rel="stylesheet" href="/skins/gumax/IE60Fixes.css?207" type="text/css" media="screen" /><![endif]--> 
			<!--[if IE 7]><link rel="stylesheet" href="/skins/gumax/IE70Fixes.css?207" type="text/css" media="screen" /><![endif]--> 
			<link rel="stylesheet" href="/skins/gumax/gumax_print.css?207" type="text/css" media="print" /> 
			<link rel="stylesheet" href="/index.php?title=MediaWiki:Common.css&amp;usemsgcache=yes&amp;ctype=text%2Fcss&amp;smaxage=18000&amp;action=raw&amp;maxage=18000" type="text/css" /> 
			<link rel="stylesheet" href="/index.php?title=MediaWiki:Print.css&amp;usemsgcache=yes&amp;ctype=text%2Fcss&amp;smaxage=18000&amp;action=raw&amp;maxage=18000" type="text/css" media="print" /> 
			<link rel="stylesheet" href="/index.php?title=MediaWiki:Gumax.css&amp;usemsgcache=yes&amp;ctype=text%2Fcss&amp;smaxage=18000&amp;action=raw&amp;maxage=18000" type="text/css" /> 
			<link rel="stylesheet" href="/index.php?title=-&amp;action=raw&amp;maxage=18000&amp;gen=css" type="text/css" /> 
*/			
    		return "<html><head><base href=\"{0}\"> {1} </head><body>{2}</body></html>".format(wikiApi.HostUrl, wikiApi.Styles, htmlCode);
    	}
    }
}
