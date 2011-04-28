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
using O2.XRules.Database.Utils;
using O2.XRules.Database.Languages_and_Frameworks.Javascript;
using Jint.Expressions;
//O2Ref:Jint.dll
//O2File:Jint_ExtensionMethods.cs
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs
{
	public class DWR_Session
	{
		public string Url {get;set;}
    	public string Cookie {get;set;}
    	public string HttpSessionId {get;set;}
    	public string ScriptSessionId {get;set;}
    	
    	public DWR_Session()
    	{
    		Url = "";	
    		Cookie = "";
    		HttpSessionId = "";
    		ScriptSessionId = "";
    	}
    	
    	public DWR_Session(string url)
    	{
    		Url = url;
    	}
    	
    	public DWR_Session(string url, string cookie,string httpSessionId, string scriptSessionId) : this(url)
		{
		
			Cookie = cookie;
			HttpSessionId = httpSessionId;
			ScriptSessionId = scriptSessionId;
		}
    	
	}
	
	
	public class DWR_Request
	{
		public DWR_Session Dwr_Session { get; set; }
		public int CallCount { get; set; }
		public string WindowName { get; set; }
		public string ScriptName { get; set; }
		public string MethodName { get; set; }
		public List<string> Parameters { get; set; }
		public int Id { get; set; }
		public int BatchId { get; set; }
		public string Page { get; set; }		
		public string ResponseData { get; set; }		
		public DateTime RequestStartDate  { get; set; }
		public DateTime RequestEndDate  { get; set; }		
		public double RequestTotalSeconds { get; set; }
		public bool processRequestData { get; set; }
		public bool RemoveAllowScriptTagRemoting {get;set;}
		
		public DWR_Request()
		{
			processRequestData = true;
			RemoveAllowScriptTagRemoting = true;
		}
		
		public DWR_Request(DWR_Session dwr_Session) : this()
		{
			Dwr_Session = dwr_Session;
		}
		
		public DWR_Request(DWR_Session dwr_Session, string scriptName, string methodName, params string[] parameters) : this(dwr_Session)
		{
			CallCount = 1;
			WindowName = "";
			ScriptName = scriptName;
			MethodName = methodName;			
			Id= 0;
			BatchId = 0;
			Page = "";			
			Parameters = (parameters.notNull() && parameters.size() > 0)  
								? parameters.toList()
								: new List<String>();			
		}						
	}
	
	public class DWR_Class
	{
		public string ClassName { get; set;}
		public string Path { get; set;}
		public string SourceCode { get; set;}
		public List<DWR_Function> Functions { get; set; }
		
		public DWR_Class()
		{
			ClassName = "";
			Path = "";
			Functions = new List<DWR_Function>();
		}
		
		public override string ToString()
		{
			return this.ClassName;
		}
	}
	
	public class DWR_Function
	{
		public string ClassName { get; set;}
		public string FunctionName { get; set;}
		public List<string> Parameters { get; set;}
		public string SourceCode { get; set;}
		
		public DWR_Function()
		{
			ClassName = "";
			FunctionName = "";
			SourceCode = "";
			Parameters = new List<string>();			
		}
		
		public DWR_Function(string functionName) : this()
		{
			FunctionName = functionName;
		}
		
		public DWR_Function(string className, string functionName) : this(functionName)
		{
			ClassName = className;
		}
		
		public DWR_Function(string className, string functionName, List<string> parameters) : this(className, functionName)
		{	
			Parameters = parameters;
		}
		
		public override string ToString()
    	{
    		if (Parameters.size()>0)
    			return "{0}( {1} )".format(FunctionName, Parameters.Aggregate((a,b)=> "{0} , {1}".format(a,b)));
    		return "{0}()".format(FunctionName);
    	}
	}
	
    public class API_DWR
    {   
    	public DWR_Session Dwr_Session { get; set; }
    	
    	public API_DWR(string url)
    	{
    		Dwr_Session = new DWR_Session(url);
    	}
    	
		public API_DWR(string url, string cookie,string httpSessionId, string scriptSessionId) : this (url)
		{
			Dwr_Session.Cookie = cookie;
			Dwr_Session.HttpSessionId =  httpSessionId;
			Dwr_Session.ScriptSessionId = scriptSessionId;
		}
		
		public DWR_Request invoke( DWR_Function dwrFunction)
		{				
			return invoke(dwrFunction.ClassName, dwrFunction.FunctionName, dwrFunction.Parameters.getDefaultValues());
		}
		
		public DWR_Request invoke( string className, string methodName, params string[] parameters)
		{			
			return dwrRequest(className, methodName, parameters);
		}
		
		public DWR_Request dwrRequest( string className, string methodName, params string[] parameters)
		{
			var dwr_Request = new DWR_Request(Dwr_Session, className, methodName,parameters);			
			dwr_Request.RequestStartDate = DateTime.Now;
			dwr_Request.makeRequest();			
			dwr_Request.RequestEndDate = DateTime.Now;
			dwr_Request.RequestTotalSeconds = (dwr_Request.RequestEndDate - dwr_Request.RequestStartDate).TotalSeconds;
			if (dwr_Request.processRequestData)
				dwr_Request.processRequestData();
			return dwr_Request;
		}				
    }

	public static class DWR_Request_ExtensionMethods
	{
		public static string createPostRequestData(this DWR_Request dwr_Request)
		{
			return dwr_Request.createRequestData("".line());
		}
		
		public static string createGetRequestData(this DWR_Request dwr_Request)
		{
			return dwr_Request.createRequestData("&");
		}
		
		public static string createRequestData(this DWR_Request dwr_Request, string separator)
		{
			var data =  ("callCount={0}" + separator + 
						   	 "windowName={1}" + separator + 
						   	 "c0-scriptName={2}" + separator + 
						 	 "c0-methodName={3}" + separator + 
						 	 "c0-id={4}" + separator + 
							 "batchId={5}" + separator + 
							 "page={6}" + separator + 
							 "httpSessionId={7}" + separator + 
							 "scriptSessionId={8}" + separator
							).format(dwr_Request.CallCount, 
									 dwr_Request.WindowName, 
									 dwr_Request.ScriptName,
									 dwr_Request.MethodName, 
									 dwr_Request.Id, 
									 dwr_Request.BatchId, 
									 dwr_Request.Page,
							         dwr_Request.Dwr_Session.HttpSessionId, 
							         dwr_Request.Dwr_Session.ScriptSessionId);
			for(int i=0; i < dwr_Request.Parameters.size() ; i++)
					data+= "c0-param{0}={1}{2}".format(i,dwr_Request.Parameters[i], separator);
			return data;
		}
		
		public static DWR_Request makeRequest(this DWR_Request dwr_Request)
		{
			return dwr_Request.makeCall_Plain();
		}
		
		public static DWR_Request makeCall_Plain(this DWR_Request dwr_Request)
		{
			//dwr_Request.createGetRequestData().info();
			var responseData = dwr_Request.Dwr_Session.makeCall_Plain( dwr_Request.Dwr_Session.Cookie,	
																	  dwr_Request.createPostRequestData(),
																	  dwr_Request.ScriptName,
																	  dwr_Request.MethodName);
			dwr_Request.ResponseData = responseData;
			return dwr_Request;
		}
		
		public static DWR_Request processRequestData(this DWR_Request dwr_Request)
		{
			if (dwr_Request.ResponseData.valid())
			{
				dwr_Request.handleNewScriptSession();
				var processedData = dwr_Request.ResponseData;
				if (dwr_Request.RemoveAllowScriptTagRemoting)
					processedData = processedData.remove("throw 'allowScriptTagRemoting is false.';".line());				
					
				dwr_Request.ResponseData =  processedData;
			}
			else
				"in processRequestData, there was no data in dwr_Request.RequestData".error();
			return dwr_Request;
		}
		
		public static DWR_Request handleNewScriptSession(this DWR_Request dwr_Request)
		{
			foreach(var line in dwr_Request.ResponseData.split_onLines())
			if (line.contains("handleNewScriptSession"))
			{
				var newScriptSessionId =  line.split("\"")[1];
				dwr_Request.Dwr_Session.ScriptSessionId = newScriptSessionId;
				"found and mapped new ScriptSessionId: {0}".info(newScriptSessionId);
			}	
			return dwr_Request;
		}


		//I was trying to do the above search for handleNewScriptSession using jint but it wasn't working (part of the problem was going upwards once found the PropertyExpression with handleNewScriptSession
		/*
		treeView.jint_configureTreeViewFor_JintView();
		
		
		var jintCompiled = responseData.jint_Compile();  
		var handleNewScriptSession = (from memberExpression in jintCompiled.statements<MemberExpression>(true) 							  
									  where memberExpression.Member is PropertyExpression  &&
									        (memberExpression.Member as PropertyExpression).Text == "handleNewScriptSession" 
									  select memberExpression
									  ).first(); */

		
		public static string postRequest_safeFileName(this DWR_Request dwr_Request, DWR_Function function, string folder )
		{
			return folder.pathCombine("{0}.{1} - {2}.xml".format(function.ClassName,function, dwr_Request.RequestEndDate).safeFileName(240-folder.size()));
		}
		
		public static string saveRequestToFolder(this DWR_Request dwr_Request, DWR_Function function, string folder)
		{
			try
			{
				var fileName = dwr_Request.postRequest_safeFileName(function,folder);
				dwr_Request.serialize(fileName);
				return fileName;
			}
			catch
			{
				return Files.Copy(dwr_Request.serialize(), folder);
			}
		}
	}
	
	public static class DWR_Session_ExtensionMethods
	{
		public static string makeCall_Plain(this DWR_Session dwr_Session, string cookie, string postData)
		{
			return dwr_Session.makeCall_Plain(cookie, postData, "AAAAAA","BBB");
		}
		
		public static string makeCall_Plain(this DWR_Session dwr_Session, string cookie, string postData, string scriptName, string methodName)
		{
			var url = (dwr_Session.Url.contains(".dwr")) ? dwr_Session.Url : dwr_Session.Url +  "/dwr/call/plaincall/{0}.{1}.dwr".format(scriptName,methodName) ;
			return dwr_Session.makeCall_Plain(cookie, postData, url.uri());
		}
		
		public static string makeCall_Plain(this DWR_Session dwr_Session, string cookie, string postData, Uri uri)
		{
			//show.info(dwr_Session);			
			"[DWR][PlainCall] invoking: {0}".info(uri);			
			var html = new Web().getUrlContents_POST(uri.str(),"",cookie, postData);			
			return html;
		}
	}
	
	public static class DRW_Class_ExtensionMethods
	{	
		public static DWR_Class dwr_mapJavascriptIntoDwrClass(this string uriOrJavascript) 
		{
			return	uriOrJavascript.dwr_mapJavascriptIntoDwrClass(dwr_mapJavascriptIntoDwrClass);
		}
		
		public static DWR_Class dwr_mapJavascriptIntoDwrClass(this string uriOrJavascript, Func<DWR_Class, string,DWR_Class> mapingFunction) 
		{
			var dwr_Class = new DWR_Class();
			var javascript = "";
			if (uriOrJavascript.isUri())
			{
				"[DWR] fetching data from: {0}".info(uriOrJavascript);
				var html = uriOrJavascript.uri().getHtml(); 
				html = html.replace("new this()" , "new this___()"); // jint bug
				//var dwrClass = html.dwr_mapJavascriptIntoDwrClass();
				dwr_Class.SourceCode = html;
				javascript = html;
			}
			else
				javascript = uriOrJavascript;
			return mapingFunction(dwr_Class,javascript);
		}
		
		public static DWR_Class dwr_mapJavascriptIntoDwrClass(this DWR_Class dwrClass, string javascript) 
		{
			var	compiledJavascript = javascript.jint_Compile();		
			var functions = compiledJavascript.functions();
			foreach(var function in functions)
			{
				var statements = function.statements(true);
				if (statements.size() == 16 && 
					statements[0].typeName() == "FunctionExpression" && statements[6].str() == "_path" && 
					statements[7].typeName() == "Identifier" && statements[10].str() == "arguments" ) // this signature finds the dwr methods (which should always have the same format)
				{										
					var className = statements[8].str();
					var functionName = statements[9].str();
					var javascriptFunction = (FunctionExpression)statements[0];
					var parameters = javascriptFunction.Parameters;
					var dwrFunction = new DWR_Function(className, functionName, parameters);
					dwrFunction.SourceCode = javascriptFunction.Source.Code; 
					dwrClass.Functions.add(dwrFunction); 	
					//"Mapped function: {0}.{1}".debug(className, functionName);
				}									
			};
			if (dwrClass.Functions.size()>0)
				dwrClass.ClassName = dwrClass.Functions[0].ClassName; // need to change this with code that gets the value from the AST
			return dwrClass;
		}
		
		// previous way of doing it (a bit more type safe but doesn't work the local version of DWR and the DWR test server
		/*
		public static DWR_Class dwr_mapJavascriptIntoDwrClass(this DWR_Class dwr_Class, string javascript) 
		{
			var	compiledJavascript = javascript.jint_Compile();			
			
			foreach(var statement in compiledJavascript.statements()) 
			{
				try
				{
					switch(statement.typeName())
					{		
						case "IfStatement":
							break; 
						case "ExpressionStatement":
							var memberExpression = (MemberExpression)statement.statements()[1].statements()[1];
							
							var varName = ((PropertyExpression)memberExpression.Member).Text;
							var varClass = ((Identifier)memberExpression.Previous).Text;
							
							var assigment = statement.statements()[1].statements()[2];
							
							switch(assigment.typeName())
							{	
								case "ValueExpression": 
									var varValue = (assigment as ValueExpression).Value.str() ; 
									"[DWR] Variable Assigment: {0}.{1} = {2}: ".info(varClass, varName, varValue);
									dwr_Class.ClassName = varClass;
									dwr_Class.Path = varValue;    
									//return   
									break; 
								case "FunctionExpression":
									var function = (FunctionExpression) assigment;											
									var dwrFunction = new DWR_Function(varClass, varName, function.Parameters);
									dwrFunction.SourceCode = function.Source.Code;
									dwr_Class.Functions.add(dwrFunction); 
									"[DWR] Function: {0}".info(dwrFunction.FunctionName);
									break;
									
								default:
									"Not supported Jint type: {0}".error(assigment.typeName());//assigment.firstSourceCodeReference().Code); 
									break;
							}
							break;
						case "Program":
							break;
						default:
							"Not supported Jint type: {0}".error(statement.typeName());
							break;
					}					
				}
				catch(Exception ex)
				{
					ex.log("[DWR] parsing javascript");
				}
			}
			return dwr_Class;
		}	
		*/
	}    
	
	public static class DWR_Function_ExtensionMethods
	{
		public static string[] getDefaultValues(this List<string> valueTypes)
		{
			var defaultValues = new string[valueTypes.size()];
			for(int i=0 ; i < valueTypes.size() ; i ++)
			{ 
				var defaultValue = valueTypes[i];
				if (defaultValue.starts("enum["))
				{
					defaultValue = defaultValue.remove("enum[").removeLastChar().split(",")[0];
				}
				else
					switch(defaultValue)
					{
						case "boolean":
							defaultValue = "false";
							break;
						case "int":
						case "integer":
						case "long":
						case "double":
							defaultValue = "-1";
							break;
						case "object":		
						case "string":	
							defaultValue = "";	
							break;
						case "object[]":		
						case "string[]":	
						case "integer[]":
						case "long[]":
							defaultValue = "[]";
							break;						
						case "file":	
							defaultValue = "";	
							break;	
						default:
							defaultValue = "[UnMappedTye]: {0}".format(defaultValue);
							break;
					}
				defaultValues[i] = defaultValue;
			}
			return defaultValues;
		}
		/*public static string[] emptyParameters(this DWR_Function dwrFunction)
		{		
			if (dwrFunction.isNull() || dwrFunction.Parameters.size() < 2)
				return new string[0] ;
			return new string[dwrFunction.Parameters.size()-1] ;			
		}*/
	}
	public static class DWR_GUIs
	{
	
		public static TreeView createTestGuiFromJavascriptInterfaces(this Panel topPanel, string baseUrl, List<string> javascriptFiles)
		{
			var treeView = topPanel.createTestGuiFromJavascriptInterfaces(baseUrl);
			foreach(var file in javascriptFiles)	
				if (file.valid())
					treeView.add_Node(file.uri().Segments.Last(),file);//rootUrl + file);
			treeView.selectFirst();	
			return treeView;
		}
			
	
		public static TreeView createTestGuiFromJavascriptInterfaces(this Panel panel, string baseUrl)
		{
			var topPanel = panel.add_Panel();
			var dwr = new API_DWR(baseUrl);
		//			.attributes("href")
//			.values() ;

			
			//var rootUrl = "http://127.0.0.1:8080/dwr_war_3.0/dwr/interface/";
			//var files = new string[] {"Demo.js", "Intro.js", "Corporations.js", "People.js", "CallCenter.js" ,"UploadDownload.js"} .toList();
			//var file = rootUrl + files[5];
			
			//var sourceCodeViewer = topPanel.add_SourceCodeViewer(); 			
			var files_treeView = topPanel.insert_Left<Panel>(200).add_TreeView().sort();
			var functions_treeView = files_treeView.insert_Below<Panel>().add_TreeView().sort();
			var ActionPanel = topPanel.insert_Above<GroupBox>(60).add_Panel();
			var selectedFunction = ActionPanel.add_Label("Selected Function").append_Label("...").font_bold().autoSize() ;
			var functionParametersRaw = ActionPanel.add_Label("Functions Parameters (Raw)",20).append_TextBox("").width(200);
			var functionParameters = topPanel.add_GroupBox("Function Parameters").add_DataGridView();
			functionParameters.add_Columns("name","value");
			var invocationResponse = functionParameters.parent().insert_Right<Panel>().add_GroupBox("Invocation Response").add_TextArea();  
			invocationResponse.splitContainer().splitterDistance(invocationResponse.parent().width()/2); 
			
			functionParametersRaw.append_Link("Invoke Method", 
				()=>{
						invocationResponse.set_Text("");
						invocationResponse.backColor("Salmon");
						var dwrMethod = (DWR_Function)functions_treeView.selected().get_Tag();
						if (dwrMethod.notNull())
						{ 
							var parameters = new List<String>();
							foreach(var row in functionParameters.rows())
								parameters.add(row[1].str());
							var response = dwr.invoke(dwrMethod.ClassName, dwrMethod.FunctionName, parameters.ToArray());
							invocationResponse.set_Text(response.ResponseData);
							invocationResponse.backColor("White");
						} 							
					});
			
			functionParametersRaw.onTextChange(
				(text)=>{
							var splittedText = text.split(",");
							for(int i=0; i < splittedText.size(); i++)
								functionParameters.get_Row(i).Cells[1].Value = splittedText[i];					
						});
			
			Func<string, DWR_Class> loadJavascriptFile = 
				(url)=>{
							functions_treeView.clear();
							var dwrClass= url.dwr_mapJavascriptIntoDwrClass();   				
							return dwrClass; 
						};
						   
			files_treeView.afterSelect<string>(
				(file)=>{				
							var dwrClass = loadJavascriptFile(file);
							foreach(var function in dwrClass.Functions)				
								functions_treeView.add_Node(function.FunctionName, function);
							functions_treeView.selectFirst();					
						}); 
						
			functions_treeView.afterSelect<DWR_Function>(
				(dwrFunction)=> {
									selectedFunction.set_Text("{0}.{1}".format(dwrFunction.ClassName, dwrFunction.FunctionName));
									functionParameters.remove_Rows();
									foreach(var parameter in dwrFunction.Parameters)
										if (parameter.neq("callback"))
											functionParameters.add_Row(parameter);
									functionParametersRaw.enabled(dwrFunction.Parameters.size()>1);  												
								});
			return files_treeView;
			
		}
	}
}
