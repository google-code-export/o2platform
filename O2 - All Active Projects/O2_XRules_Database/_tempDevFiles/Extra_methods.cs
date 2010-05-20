// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Interfaces.O2Findings;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.XRules.Database._Rules._Interfaces;
using O2.External.IE.ExtensionMethods;
using O2.External.IE.Wrapper;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast; 
using ICSharpCode.SharpDevelop.Dom;
using System.CodeDom;
using O2.Views.ASCX.O2Findings;
using O2.ImportExport.OunceLabs;

//O2Ref:QuickGraph.dll

namespace O2.Script
{
    public static class ExtraMethods2
    {   
    	// O2MediaWikiAPi
    	
    	public static List<string> categories(this O2MediaWikiAPI wikiApi)
    	{
    		return wikiApi.categories(true);
    	}
    	
    	public static List<string> categories(this O2MediaWikiAPI wikiApi, bool autoRemoveCategoryPrefix)
    	{
    		var categoryString = "Category:";
    		var categories = new List<string>();
    		var results = wikiApi.categoriesRaw().attributes("title").values();
    		if (autoRemoveCategoryPrefix)
    		{
    			foreach(var category in results)
    				if(category.starts(categoryString))
    					categories.add(category.Substring(categoryString.size()));
    				else
    					categories.add(category);
    		}
    		else
    			categories = results;
    		return categories;
    	}
    	
    	public static List<XElement> categoriesRaw(this O2MediaWikiAPI wikiApi)
    	{
    		string pages = "";
			string limitVar = "aclimit" ;
			int limitValue = 200;
			string properyType = "generator"; 
			string propertyName = "allcategories" ;
			string continueVarName = "gacfrom";
			string continueValue = "";
			string dataElement = "page";
			int maxItemsToFetch = -1; 
			bool resolveRedirects= false;
			//"info&generator=allcategories",""
			return wikiApi.getQueryContinueResults( pages,  limitVar,  limitValue, properyType, 
													propertyName ,  continueVarName ,  continueValue, 
													dataElement,  maxItemsToFetch,  resolveRedirects);    		
    	}	
    	
    	public static List<string> pagesInCategory(this O2MediaWikiAPI wikiApi, string category)
    	{
    		return wikiApi.pagesInCategory(category, true);
    	}
    
    	public static List<string> pagesInCategory(this O2MediaWikiAPI wikiApi, string category, bool autoAddCategoryPrefix)
    	{
    		if (autoAddCategoryPrefix)
    			category = "Category:" + category;
    		return wikiApi.pagesInCategoryRaw(category).attributes("title").values();
    	}
    	
    	public static List<XElement> pagesInCategoryRaw(this O2MediaWikiAPI wikiApi, string category)
    	{
    		string pages = "&gcmtitle=" + category;
			string limitVar = "cmlimit" ;
			int limitValue = 200;
			string properyType = "generator"; 
			string propertyName = "categorymembers" ;
			string continueVarName = "gcmcontinue";
			string continueValue = "";
			string dataElement = "page";
			int maxItemsToFetch = -1; 
			bool resolveRedirects= false;
			//"info&generator=allcategories",""
			return wikiApi.getQueryContinueResults( pages,  limitVar,  limitValue, properyType, 
													propertyName ,  continueVarName ,  continueValue, 
													dataElement,  maxItemsToFetch,  resolveRedirects);    		
    	}
        
        
        /*public static List<string> uncategorizedPages(this O2MediaWikiAPI wikiApi)
        {
        	//var maxToFetch = 500;			
			//var uri = link.uri();
			//var htmlCode = uri.getHtml();
			//var htmlDocument = htmlCode.htmlDocument();						
        }*/
        
        public static List<string> uncategorizedPages(this O2MediaWikiAPI wikiApi)
		{
			var maxToFetchPerRequest= 500;
			var maxItemsToFetch = 5000;
			return wikiApi.getIndexPhp_UsingXPath_AttributeValues("title=Special:UncategorizedPages",
																  "//div[@class='mw-spcontent']//li//a", "href",
																  "limit",maxToFetchPerRequest,
																  "offset", maxItemsToFetch);							
		}

		
		public static List<string> getIndexPhp_UsingXPath_AttributeValues(this O2MediaWikiAPI wikiApi,
																		  string wikiQueryString, string xPathQuery, string xPathAttribute,
																		  string limitVarName , int maxToFetchPerRequest, 
																		  string offsetVarName, int maxItemsToFetch)
		{
			var currentOffset = 0;
			var result = new List<string>();
			while(result.size() < maxItemsToFetch)
			{
				//var getRequest = "http://www.o2platform.com/index.php?title=Special:UncategorizedPages&limit={0}&offset={1}".format(maxToFetch,currentOffset);
				var getRequest = "{0}&{1}={2}&{3}={4}".format(wikiQueryString,limitVarName, maxToFetchPerRequest, offsetVarName, currentOffset);
				var htmlCode =  wikiApi.getIndexPhp(getRequest); 
				//"uncategorizedPages GET request: {0}".info(getRequest);
				var htmlDocument = htmlCode.htmlDocument(); 
				var pages = htmlDocument.select(xPathQuery).attributes(xPathAttribute).values();
				result.add(pages);
				if (pages.size() == 0 || pages.size() < maxToFetchPerRequest) 
					break;	
				currentOffset+=maxToFetchPerRequest;
			}			
			return result;
		}
		//action=query&generator=categorymembers&gcmtitle=Category:Project%20About&prop=info
        
        
        
        //Html agilitypack
        
        public static HtmlAgilityPack.HtmlDocument htmlDocument(this string htmlCode)
        {
        	var htmlDocument = new HtmlAgilityPack.HtmlDocument();
        	htmlDocument.LoadHtml(htmlCode);
        	return htmlDocument;
        }
        
        public static string html(this HtmlAgilityPack.HtmlDocument htmlDocument)
        {
        	return htmlDocument.DocumentNode.OuterHtml;
        }
        
        
        public static List<HtmlAgilityPack.HtmlNode> select(this HtmlAgilityPack.HtmlDocument htmlDocument, string query)
        {
        	return htmlDocument.DocumentNode.SelectNodes(query).toList<HtmlAgilityPack.HtmlNode>();
        }
        
        public static List<HtmlAgilityPack.HtmlNode> links(this HtmlAgilityPack.HtmlDocument htmlDocument)
        {        	        	 
        	return htmlDocument.select("//a");        	
        }
                
        public static List<T> toList<T>(this IEnumerable list)
        {
        	var results = new List<T>();
        	foreach(var item in list)
        		results.Add((T)item);
        	return results;
        }
        
        // List<HtmlAgilityPack.HtmlNode> 
        
        public static List<string> html(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
        	return htmlNodes.outerHtml();
        }
        
        public static List<string> outerHtml(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
        	var outerHtml= new List<string>();
        	foreach(var htmlNode in htmlNodes)
        		outerHtml.add(htmlNode.outerHtml());
        	return outerHtml;        		
        }
        
        public static string html(this HtmlAgilityPack.HtmlNode htmlNode)
        {
        	return htmlNode.outerHtml();
        }
        
        public static string outerHtml(this HtmlAgilityPack.HtmlNode htmlNode)
        {
        	return htmlNode.OuterHtml;        		
        }
        
        public static string innerHtml(this HtmlAgilityPack.HtmlNode htmlNode)
        {
        	return htmlNode.InnerHtml;        		
        }                
                
        public static List<string> innerHtml(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
        	var outerHtml= new List<string>();
        	foreach(var htmlNode in htmlNodes)
        		outerHtml.add(htmlNode.innerHtml());
        	return outerHtml;        		    		
        }
        
        public static string value(this HtmlAgilityPack.HtmlNode htmlNode)
        {
        	return htmlNode.innerHtml();
        }
        
        public static List<string> values(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
        	return htmlNodes.innerHtml();
        }
        
        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {
        	return htmlNodes.attributes("");
        }
        
        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this List<HtmlAgilityPack.HtmlNode> htmlNodes, string attributeName)
        {
        	var allAttributes = new List<HtmlAgilityPack.HtmlAttribute>();
        	foreach(var htmlNode in htmlNodes)
        		allAttributes.add(htmlNode.attributes(attributeName));
        	return allAttributes;
        }
        
        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this HtmlAgilityPack.HtmlNode htmlNode)
        {
        	return htmlNode.attributes("");
        }
        public static List<HtmlAgilityPack.HtmlAttribute> attributes(this HtmlAgilityPack.HtmlNode htmlNode, string attributeName)
        {
        	var attributes = new List<HtmlAgilityPack.HtmlAttribute>();
        	foreach(var htmlAttribute in htmlNode.Attributes)
        		if (attributeName.valid().isFalse() || htmlAttribute.Name == attributeName)
        			attributes.add(htmlAttribute);
        	return attributes;
        }
        
        public static TreeView add_Node(this TreeView treeView, HtmlAgilityPack.HtmlDocument htmlDocument)
        {
        	return treeView.add_Node(htmlDocument.DocumentNode);
        }
        
        public static TreeView add_Node(this TreeView treeView, HtmlAgilityPack.HtmlNode htmlNode)
        {
        	treeView.rootNode().add_Node(htmlNode);
        	return treeView;
        }
        
        public static TreeNode add_Node(this TreeNode treeNode, HtmlAgilityPack.HtmlNode htmlNode)
        {
        	return treeNode.add_Node(htmlNode.Name, htmlNode,true);        	
        }
        
        public static TreeView add_Nodes(this TreeView treeView, List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {        	
        	treeView.rootNode().add_Nodes(htmlNodes);
        	return treeView;
        }
        
        public static TreeNode add_Nodes(this TreeNode treeNode, List<HtmlAgilityPack.HtmlNode> htmlNodes)
        {        	
        	foreach(var htmlNode in htmlNodes)
        		treeNode.add_Node(htmlNode);
        	return treeNode;
        }
        
        //List<HtmlAgilityPack.HtmlAttribute>
        
        public static List<string> names(this List<HtmlAgilityPack.HtmlAttribute> htmlAttributes)
        {
        	var names = new List<string>();
        	foreach(var htmlAttribute in htmlAttributes)
        		if (names.Contains(htmlAttribute.Name).isFalse())
        			names.add(htmlAttribute.Name);
        	return names;
        }
        
        public static List<string> values(this List<HtmlAgilityPack.HtmlAttribute> htmlAttributes)
        {
        	var values = new List<string>();
        	foreach(var htmlAttribute in htmlAttributes)
        		values.add(htmlAttribute.Value);
        	return values;
        }
        
        
        
        // Controls ExtensionMethods
        
        public static T sendKeys<T>(this T control, string textToSend) where T : Control
        {
        	return (T)control.invokeOnThread(
        		()=>{
        				control.focus();
        				SendKeys.Send(textToSend); 
        				return control;
        			});
        }
        
        public static T sendEnter<T>(this T control) where T : Control
        {
        	return control.sendKeys("".line());
        }
        
        public static ascx_SourceCodeViewer showHtmlNodeLocation(this ascx_SourceCodeViewer codeViewer, HtmlAgilityPack.HtmlNode htmlNode)
        {
        	codeViewer.editor().showHtmlNodeLocation(htmlNode);
        	return codeViewer;
        }
        
        public static ascx_SourceCodeEditor showHtmlNodeLocation(this ascx_SourceCodeEditor codeEditor, HtmlAgilityPack.HtmlNode htmlNode)
        {
        	//show.info(htmlNode);
			//if (htmlNode.Line != null)
			//{					  						
				var startLine = htmlNode.Line;
				var startColumn = htmlNode.LinePosition;
				
				var endLine = startLine; 
				var endColumn = startColumn; 
				
				if (htmlNode.NextSibling != null) 
				{
					endLine = htmlNode.NextSibling.Line;
					endColumn = htmlNode.NextSibling.LinePosition;
				}
				else
					endColumn += htmlNode.html().size();
				"selecting CodeEditor location: {0}:{1} -> {2}:{3}".info(startLine, startColumn, endLine, endColumn);
				codeEditor.clearMarkers();
				codeEditor.selectTextWithColor(startLine, startColumn, endLine, endColumn);
				codeEditor.caret_Line(startLine);
				codeEditor.refresh();
			//}	
			//else
			//{
			//	"in ascx_SourceCodeEditor showHtmlNodeLocation, could not find a location for provided htmlNode".error();
				//show.info(htmlNode);
			//}
			return codeEditor;
        }
        
        
        //Browser 
        
        public static WebBrowser open(this WebBrowser webBrowser, string url)
        {
        	webBrowser.invokeOnThread(()=> webBrowser.Navigate(url));
        	return webBrowser;
        }
        
        // string html extension methods        
        public static string htmlToXml(this string htmlCode)
        {
        	return htmlCode.htmlToXml(true);
        }
        
        public static string htmlToXml(this string htmlCode, bool xmlFormat)
        {
        	try
        	{
        		var stringWriter = new StringWriter();
				var xmlWriter = XmlWriter.Create(stringWriter); 				
				xmlWriter.Flush();
				var htmlDocument = htmlCode.htmlDocument();
				
				htmlDocument.Save(xmlWriter);				
				if (xmlFormat)
					return stringWriter.str().xmlFormat();
				return stringWriter.str();
        	}
        	catch(Exception ex)
        	{
        		ex.log("in string.htmlToXml");
        		return ex.Message;
        	}
        }
    
    
    	// TreeView Extension Methods
    	
    	public static TreeView sort(this TreeView treeView, bool value)
    	{
    		return(TreeView)treeView.invokeOnThread(()=> treeView.Sorted = value);
    	}
    	
    	public static TreeNode set_Text(this TreeNode treeNode, string text)
    	{
    		return (TreeNode)treeNode.treeView().invokeOnThread(
    									()=>{
    											treeNode.Text = text;
    											return treeNode;
    										});
    	}
    	
    	//Colections 
    	
    	public static List<String> sort(this List<String> list)
    	{
    		list.Sort();
    		return list;
    	}
    }	
    
    
    
   
}
		