// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.XRules.Database.Utils;
using HTTPProxyServer;

//O2File:API_WinProxy.cs
//O2File:API_WinProxy.cs 
//O2File:Program.cs  
//O2File:CacheEntry.cs
//O2File:CacheKey.cs
//O2File:ProxyCache.cs
//O2File:ProxyServer.cs

//O2File:_Extra_methods_Files.cs
//O2File:_Extra_methods_Reflection.cs
//O2File:_Extra_methods_WinForms_Controls.cs
//O2File:_Extra_methods_WinForms_TreeView.cs

//_O2Ref:System.Configuration.dll
  
  
namespace O2.XRules.Database.APIs
{
	public class O2_Web_Proxy_Test
	{				
		public void launchTestGui()
		{
			var o2WebProxy = new O2_Web_Proxy();
			o2WebProxy.createTestGui("Testing O2 Web Proxy".popupWindow());
			o2WebProxy.Port = 8082;
			o2WebProxy.startWebProxy();
		}
	}
	
    public class O2_Web_Proxy
    {    
    	public string CertLocation { get; set; }
    	public object WafRule	   { get; set; }
    	public string IP	   { get; set; }
    	public int Port	   { get; set; }
    	
    	public int DEFAULT_PORT = 8081;
    	
    	public O2_Web_Proxy()
    	{
    		config();
    	}
    	
    	public O2_Web_Proxy config()
    	{
    		CertLocation = @"cert.cer".local();
    		IP = "127.0.0.1";
    		Port = this.DEFAULT_PORT;    		
    		return this;
    	}
	}
	
	public static class O2_Web_Proxy_ExtensionMethods_ProxyActions
	{
		public static string proxyLocation(this O2_Web_Proxy o2WebProxy)
		{
			return "http://{0}:{1}".format(o2WebProxy.IP, o2WebProxy.Port);
		}
	}
	
	public static class O2_Web_Proxy_ExtensionMethods_Utils
	{			
		public static O2_Web_Proxy setBrowserProxy(this O2_Web_Proxy o2WebProxy)
		{
			var proxyLocation = o2WebProxy.proxyLocation();
			"setting proxy location to: {0}".info(proxyLocation);
			API_WinProxy.SetProxy(proxyLocation);
			return o2WebProxy;
		}
		
		public static O2_Web_Proxy clearBrowserProxy(this O2_Web_Proxy o2WebProxy)
		{
			"remove proxy proxy settings (ie setting it to \"\"".info();
			API_WinProxy.SetProxy("");
			return o2WebProxy;
		}		
		
		public static bool startWebProxy(this O2_Web_Proxy o2WebProxy)
		{
			//if (o2WebProxy.Port == o2WebProxy.DEFAULT_PORT)				
				o2WebProxy.Port = o2WebProxy.DEFAULT_PORT + 1000.randomNumber();
			return o2WebProxy.startWebProxy(o2WebProxy.Port);
		}
		
		public static bool startWebProxy(this O2_Web_Proxy o2WebProxy, int port)
		{
			o2WebProxy.Port = port;
			ProxyServer.Server.ListeningPort = o2WebProxy.Port;
			o2WebProxy.setBrowserProxy();
			if (ProxyServer.Server.Start(o2WebProxy.CertLocation))
			{
				"Proxy started Ok on ip:port: {0}".info(o2WebProxy.proxyLocation());
				return true;
			}			
			"Proxy failed to start on ip:port: {0}".error(o2WebProxy.proxyLocation());		
			return false;
		}	
		
		public static O2_Web_Proxy stopWebProxy(this O2_Web_Proxy o2WebProxy)
		{ 
			o2WebProxy.clearBrowserProxy();
			"Stopping Web Proxy".info();
			ProxyServer.Server.Stop();
			return o2WebProxy;
		 }
	}
	
	public static class O2_Web_Proxy_ExtensionMethods_GuiHelpers
	{
		public static bool loadWafRule(this O2_Web_Proxy o2WebProxy, string wafRuleFile)
		{
			return o2WebProxy.loadWafRule(wafRuleFile, false);
		}
		
		public static bool loadWafRule(this O2_Web_Proxy o2WebProxy, string wafRuleFile, bool logCallBacks)
		{				
			if (wafRuleFile.fileExists().isFalse())
				wafRuleFile = wafRuleFile.local();
			if (wafRuleFile.fileExists().isFalse())
				"[O2_Web_Proxy] in loadWafRule, could not find rule file: {0}".error(wafRuleFile);	
			else 
			{
				"[O2_Web_Proxy]: loading Waf Rule file: {0}".info(wafRuleFile);
				//var assembly = new O2.DotNetWrappers.DotNet.CompileEngine().compileSourceFile(ruleToLoad);
				var assembly = wafRuleFile.compile();
				if (assembly.isNull())
					"failed to compiled rule: {0}".error(wafRuleFile); 
				else
				{
					"[O2_Web_Proxy]: Compiled ok".info();
					var ruleType = assembly.type(wafRuleFile.fileName_WithoutExtension());
					if (ruleType.isNull())
					{
						"failed to find type: {0}".error(wafRuleFile.fileName_WithoutExtension()); 						
					}
					else
					{
						"[O2_Web_Proxy]: Found rule type ok".info();
						o2WebProxy.WafRule = ruleType.ctor();
						o2WebProxy.WafRule.prop("LogCallbacks", logCallBacks);
						"[O2_Web_Proxy]: Rule Loaded".info();
						//setWafRuleLinkViewerCallback();
						//browserPanel.set_Text("Browser with rule loaded: {0}".format(ruleToLoad.fileName_WithoutExtension())); 						
						 
						ProxyServer.InterceptWebRequest		= (webRequest)=> 							{ o2WebProxy.WafRule.invoke("InterceptWebRequest", webRequest);};
						ProxyServer.OnResponseReceived 		= (requestResponseData)=> 					{ o2WebProxy.WafRule.invoke("InterceptOnResponseReceived", requestResponseData);};
						ProxyServer.InterceptRemoteUrl		= (remoteUrl) => 							{ return (string) o2WebProxy.WafRule.invoke("InterceptRemoteUrl",remoteUrl); };
						ProxyServer.InterceptResponseHtml	= (uri) => 		 							{ return (bool)   o2WebProxy.WafRule.invoke("InterceptResponseHtml", uri); };  
						ProxyServer.HtmlContentReplace		= (uri,content)=>							{ return (string) o2WebProxy.WafRule.invoke("HtmlContentReplace", uri, content);} ; 
						
						return true;
					}
				}
				//var wafRule = "WAF_Rule.cs".local().compile().types()[0].ctor();			
				//WafRule.o2WebProxy = wafRuleFile.compile().type("fileName_WithoutExtension").ctor();			
				//object wafRuleObject = 	"WAF_Rule_NoPorto.cs".local().compile().types()[0].ctor();	 
				
			}
			//setWafRuleLinkViewerCallback();
			return false;
		}
	}
	
	public static class O2_Web_Proxy_ExtensionMethods_WafRules
	{
		public static T createTestGui<T>(this O2_Web_Proxy o2WebProxy,  T panel)		
			where T : Control
		{
			var topPanel = panel.clear().add_Panel();
			topPanel.insert_LogViewer();
			
			var actionsPanel = topPanel.insert_Above(40,"actions");
			var browserPanel = topPanel.add_GroupBox("Browser");
			var browser = browserPanel.add_WebBrowser_Control().add_NavigationBar();			
			var links = topPanel.insert_Right(200).add_TreeView();			
			var sourceCodeViewer = links.insert_Below().add_SourceCodeViewer();			
			var rules = topPanel.insert_Left(400,"WAF Rules").add_TreeView(); 			

			links.add_ContextMenu().add_MenuItem("clear list", ()=> links.clear());									
			links.afterSelect<string>(
				(htmlContent)=> {
									"Content for {0} with size {1}".info(links.selected().str(), htmlContent.size());
									htmlContent.info();
									sourceCodeViewer.set_Text(htmlContent, ".html");
								});
			
			Action setWafRuleLinkViewerCallback = 
						()=>{								
								Action<HttpWebRequest,HttpWebResponse, string> onRequestReceived = 
									(webRequest, webResponse, htmlContent)=> 
										links.add_Node(webRequest.RequestUri.str(),htmlContent );
										
								o2WebProxy.WafRule.prop("OnRequestReceived" , onRequestReceived);																
							};
		
			Action<string> loadWafRule = 	
				(wafRuleFile)=> { 
									O2Thread.mtaThread(
										()=>{
												o2WebProxy.loadWafRule(wafRuleFile); 
												setWafRuleLinkViewerCallback(); 
											});
								};
								
			rules.onDrop(
				(fileOrFolder)=>{ 
									if (fileOrFolder.fileExists())
										rules.add_File(fileOrFolder);
									else
									{
										rules.clear();
										rules.add_Files(fileOrFolder);
									}
								 });
			
			rules.add_ContextMenu().add_MenuItem("Edit Rule", ()=> (rules.selected().get_Tag() as String).showInCodeEditor()); 
			
			rules.add_Files("WAF_Rule_NoGoogle.cs".local().directoryName().files());
			//rules.add_Node("WAF_Rule_NoPorto.cs", "WAF_Rule_NoPorto.cs".local());
			
			o2WebProxy.loadWafRule("WAF_Rule.cs", true);
			
			actionsPanel.add_Label("O2 Proxy")		
						.append_Link("Proxy Start", ()=> o2WebProxy.startWebProxy())
						.append_Link("Proxy Stop",  ()=> o2WebProxy.stopWebProxy()) 
						.append_Link("Set Browser Proxy",  ()=> o2WebProxy.setBrowserProxy())
						.append_Link("Clear Browser Proxy",  ()=> o2WebProxy.clearBrowserProxy())
						.append_Link("Stop Current Process", ()=> Processes.getCurrentProcess().stop());

		
			rules.afterSelect<string>  (loadWafRule);
			rules.onDoubleClick<string>(loadWafRule); //(file) => { o2WebProxy.loadWafRule(file); setWafRuleLinkViewerCallback(); } );
			setWafRuleLinkViewerCallback();
			return panel;
		}
	}
}
	