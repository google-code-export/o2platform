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

//O2File:C:\O2\_XRules_Local\WebAutomation\O2MediaWikiAPI.cs
//O2File:C:\O2\_XRules_Local\ExtensionMethods\extra_WebAutomation.cs
//O2File:C:\O2\_XRules_Local\ExtensionMethods\extra_VisualizationAPI.cs

namespace O2.Script
{

	public class OwaspWikiApi : O2MediaWikiAPI
	{
		public OwaspWikiApi() 
		{
			init("http://www.owasp.org/api.php");
			this.Styles = owaspStyles();
		}
		
		// dynamically (one per session) grab the current header scripts used in OWASP
		public string owaspStyles()
		{
			var page = @"http://www.owasp.org/index.php?title=Special:UserLogin";  
			var codeToParse = page.uri().getHtml(); 
			
			var htmlDocument = new HtmlAgilityPack.HtmlDocument();
			htmlDocument.LoadHtml(codeToParse);
								
			var headerText = "".line().line();
			foreach(var node in htmlDocument.DocumentNode.SelectNodes("//link[@type='text/css']"))
			{
				var html = node.OuterHtml.line();
				html = html.replace("href=\"/","href=\""+ this.HostUrl);
				headerText += html;// + "</link>";
			}
			var scripts =  htmlDocument.DocumentNode.SelectNodes("//head//script");
			foreach(var node in htmlDocument.DocumentNode.SelectNodes("//head//script")) 
			{
				var html = node.OuterHtml.line();
				if (html.contains("ga.js").isFalse() && html.contains("_gat").isFalse())
					headerText += html;
			}
			
			headerText += scripts[0].OuterHtml.line();
			headerText += scripts[1].OuterHtml.line();
			headerText += scripts[2].OuterHtml.line();
			headerText += scripts[3].OuterHtml.line();
			headerText += scripts[4].OuterHtml.line();
			
			return headerText.line().line();
		}
    }
}
