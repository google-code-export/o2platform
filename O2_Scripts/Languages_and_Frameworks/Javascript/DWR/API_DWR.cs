// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;

namespace O2.XRules.Database.APIs
{
    public class API_DWR
    {   
    	public string Url {get;set;}
    	public string Cookie {get;set;}
    	public string HttpSessionId {get;set;}
    	public string ScriptSessionID {get;set;}
    	
		public API_DWR(string url, string cookie,string httpSessionId, string scriptSessionId)
		{
			Url = url;
			Cookie = cookie;
			HttpSessionId = httpSessionId;
			ScriptSessionID = scriptSessionId;
		}
		
		//public string submit( string scriptName, string methodName)
		//{}
		public string dwrRequest( string scriptName, string methodName, params string[] parameters)
		{
			return get(scriptName, methodName, parameters);
		}
		
		public string get( string scriptName, string methodName, params string[] parameters)
		{	
				//var url = "http://10.40.3.22:8080/portal-front/dwr/call/plaincall/a.dwr";
				var postData = 	"callCount=1".line() + 
							   	"windowName= ".line() + 
							   	"c0-scriptName={0}".line().format(scriptName) +
							 	"c0-methodName={0}".line().format(methodName) +  
							 	"c0-id=0".line() +								
								"batchId=1".line() +
								"page=".line() +
								"httpSessionId={0}".line().format(HttpSessionId) +
								"scriptSessionId={0}".line().format(ScriptSessionID);
				for(int i=0; i < parameters.size() ; i++)
					postData+= "c0-param{0}={1}".line().format(i,parameters[i]);
				//("c0-param0=aaa").line() +								
				var html = new Web().getUrlContents_POST(Url,"",Cookie,postData);
				return html;
			 }
		
    }
}
