// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.XRules.Database.Utils;
using O2.DotNetWrappers.Windows;
using O2.XRules.Database.Languages_and_Frameworks.DotNet;

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs
//O2File:Scripts_ExtensionMethods.cs
//O2File:DotNet_SDK_WSDL.cs
//O2File:DynamicTypes.cs

//O2Ref:O2_API_AST.dll



namespace O2.XRules.Database.Languages_and_Frameworks.AspNet
{
	public class test_ascx_WSDL_Creation_and_Execution
	{		
		public void launchGui()
		{
			"step -1".info();
			O2Thread.mtaThread(
				()=>{
						"step 0".info();
						var wsdlCreation = "ascx_WSDL_Creation_and_Execution".showAsForm<ascx_WSDL_Creation_and_Execution>(1000,500 );			
						wsdlCreation.TestWebServices.select_Item(0);
					});
		}
	}

	public class ascx_WSDL_Creation_and_Execution : Control
	{	
		public TextBox WsdlLocation { get; set; }
		public TextBox ExtraWsdlParameres { get; set; }
		public ComboBox TestWebServices { get; set; }
		public TreeView Methods_TreeView { get; set; } 	
		public TextBox ExecutionResult { get; set; } 	
		public TreeView ExecutionResult_TreeView { get; set; } 	
		public PropertyGrid ExecutionResult_Properties { get; set; } 	
		public Button ExecuteSoapRequest_Button { get; set; }
		public bool ShowFullMethodSignatures { get; set; }
		public bool Cache_WSDL_Dll { get; set; }
		
		public string CSharpFile { get; set; }
		public Assembly WsdlAssembly { get; set; }
		public MethodInfo CurrentMethod { get; set; }
		
		public object SoapParametersObject { get; set; }
		public object ResultObject { get; set; }
		
		public ascx_WSDL_Creation_and_Execution()
		{
			this.Width = 700;
			this.Height = 500; 
			buildGui();
		}
		
		public ascx_WSDL_Creation_and_Execution add_TestWebServices()
		{			
			TestWebServices.add_Item("http://www.webservicex.net/globalweather.asmx?WSDL")
						   .add_Item("http://api.search.live.net/search.wsdl")
						   .add_Item("http://www.webservicex.net/whois.asmx?WSDL" )
						   .add_Item("http://www.webservicex.net/genericbarcode.asmx?WSDL")
						   .add_Item("http://blogs.msdn.com/blogservice.asmx?WSDL")
						   .add_Item("http://services.msdn.microsoft.com/ContentServices/ContentService.asmx?wsdl")			   			   
						   .add_Item("http://webservices.amazon.com/AWSECommerceService/AWSECommerceService.wsdl");			   
			TestWebServices.onSelection<string>((value)=> open(value) );  
			 
			TestWebServices.insert_Left(100).add_Label("Test WebServices").top(2);
			return this;
		}
		
		public ascx_WSDL_Creation_and_Execution buildGui() 
		{				
			var topPanel = this.add_Panel();
			topPanel.insert_Below(100).add_LogViewer();			
			//var assemblyInvoke = topPanel.add_Control<O2.External.SharpDevelop.Ascx.ascx_AssemblyInvoke>();
			//assemblyInvoke.loadAssembly("OnlineStorage.dll".assembly());
			//var methodProperties = topPanel.add_DataGridView();			
			var methodProperties = topPanel.add_GroupBox("Request Data").add_Control<ascx_ObjectViewer>();			
			methodProperties.showSerializedString().createObjectWhenNull().simpleView();			
			
			Methods_TreeView = methodProperties.parent().insert_Left("Soap Methods").add_TreeView().sort();
			ExecutionResult = methodProperties.parent().insert_Below(150).add_GroupBox("Web Service Invocation Response Data").add_TextArea();
			ExecutionResult_Properties = ExecutionResult.insert_Right().add_PropertyGrid();
			ExecutionResult_TreeView = ExecutionResult_Properties.insert_Below().add_TreeView();
			ExecuteSoapRequest_Button = methodProperties.insert_Below(40)
														.add_Button("Execute Soap request")
														.font_bold()														
														.fill();
			
			Methods_TreeView.afterSelect<MethodInfo>(
				(methodInfo) => {								
									CurrentMethod = methodInfo;	
									"Current Method: {0}".info(CurrentMethod.Name);
									"Current Method: Signature = {0}".info(CurrentMethod.str());
									"Current Method: DeclaringType = {0}".info(CurrentMethod.DeclaringType.FullName);
									SoapParametersObject = methodInfo.create_LiveObject_From_MethodInfo_Parameters("Soap_Invocation_Parameters");
									methodProperties.show(SoapParametersObject);
									//new O2FormsReflectionASCX().loadMethodInfoParametersInDataGridView(methodInfo, methodProperties);
								});
								
			ExecuteSoapRequest_Button.onClick(
				()=>{
						//var parameters = new O2FormsReflectionASCX().getParameterObjectsFromDataGridColumn(methodProperties, "Value");				
						var invocationParameters = new List<object>();
						foreach(var property in SoapParametersObject.type().properties())  
							invocationParameters.add(SoapParametersObject.property(property.Name));  
				
						ExecutionResult.set_Text("[{0}] Executing method : {1}".line().format(DateTime.Now,CurrentMethod.Name)); 
						
						var liveObject = CurrentMethod.DeclaringType.ctor();
						ExecutionResult.append_Line("Created dynamic proxy object of type: {0}".format(liveObject));						
						
						try
						{
							var o2Timer = new O2Timer("Method execution time: ").start();
							ResultObject = CurrentMethod.Invoke(liveObject, invocationParameters.ToArray());
							ExecutionResult.append_Line("Method execution time: {0}".format(o2Timer.stop()));
							ExecutionResult.append_Line("Method Executed OK, here is the return value:");
							ExecutionResult.append_Line(ResultObject.str().lineBeforeAndAfter());
							ExecutionResult_Properties.show(ResultObject);	
							ExecutionResult_TreeView.xmlShow(ResultObject.serialize());
						}
						//catch(System.Web.Services.Protocols.SoapException ex)
						//{
							
						//}
						catch (Exception ex)
						{	
							ExecutionResult.append_Line((ex.InnerException.property("Detail") as System.Xml.XmlElement).OuterXml);
							ExecutionResult.append_Line(ex.InnerException.Message);
							ExecutionResult.append_Line(ex.InnerException.StackTrace);
							//show.info(ex);
							//ex.details();
						}
						
						//currentMethod.invoke(parameters); 
						
						ExecutionResult.append_Line("Execution complete");
					});
			
			
			Methods_TreeView.insert_Below(20)
							.add_CheckBox("Show Full Method Signatures",0,0,(value)=> ShowFullMethodSignatures = value)
							.autoSize()							
							.parent<Panel>() 							
							.add_CheckBox("Cache WSDL Dll",0,200,(value)=> Cache_WSDL_Dll = value)
							.autoSize()
							.check();
							
			 				
							//.append_Link("Delete cached compiled dll", deleteCachedFile);
			
			ExecutionResult.insert_Below(20)
						   .add_Link("View serialized string of response object", 0,0,
						      	()=>{						      	
						      			var serializedResponse = ResultObject.serialize(false);
						      			if (serializedResponse.notNull())
						      				"".showInCodeViewer().set_Text(serializedResponse,"a.xml");
						      	  	});
				
			TestWebServices = topPanel.insert_Above(22).add_ComboBox();
			
			WsdlLocation = topPanel.insert_Above(22)
								   .add_LabelAndTextAndButton("Load Custom WSDL from location (file or url)","","Load",(value) => open(value))
								   .control<TextBox>();
			ExtraWsdlParameres = topPanel.insert_Above(22).add_Label("Extra wsdl.exe parameters").left(WsdlLocation.Left).top(2)
											  .append_TextBox("").align_Right(topPanel);
			addScriptingSupport();
			
			
			add_TestWebServices();
			
			return this;
		} 
		
		public void load_Wsdl_From_Assembly(Assembly wsdlAssembly)
		{
			if (wsdlAssembly.notNull())
			{
				Methods_TreeView.clear();
				"Loading Wsdl from Assembly: {0} at {1}".info(wsdlAssembly.str() , wsdlAssembly.Location);
				WsdlAssembly = wsdlAssembly;
				var soapMethods = WsdlAssembly.webService_SoapMethods();
				"Found {0} Web Service methods:{0}".info(soapMethods.size());					
				Methods_TreeView.add_Nodes(soapMethods, (method)=> ((this.ShowFullMethodSignatures) 
																		? method.str()
																		: method.Name));
				Methods_TreeView.selectFirst();
			}
			else
				"Provided Assembly was null".error();
			markGuiAs_OK();
		}	
		
		public string load_Wsdl_From_CSharpFile(string csharpFile)
		{
			markGuiAs_Busy();
			"Loading Wsdl from CSharpFile: {0}".info(csharpFile);
			if (csharpFile.valid())
			{			
				var extraLineToAdd = "//O2Ref:System.Web.Services.dll".line();
				if (csharpFile.fileContains(extraLineToAdd).isFalse())
					csharpFile.fileInsertAt(0, extraLineToAdd); 
			
				var assembly = csharpFile.compile(); 
				if (assembly.notNull())
				{ 
					CSharpFile = csharpFile; 
					"Created Assembly:{0}".info(assembly.Location);
					load_Wsdl_From_Assembly(assembly);	
					return assembly.Location;
				}
				else
					"Error creating assembly from wsdl file: {0}".error(csharpFile);
			}
			return ""; 
		}
		
		public void load_Wsdl_From_WSDL_File(string wsdlToLoad)
		{
			markGuiAs_Busy();			
			O2Thread.mtaThread(
				()=> {						
						var cacheLocation = getCacheFileName(wsdlToLoad);
						
						var sdl_Wsdl = new DotNet_SDK_WSDL(); 			
						if (wsdlToLoad.fileExists().isFalse() && wsdlToLoad.isUri() && wsdlToLoad.ends("wsdl").isFalse())
							wsdlToLoad += "?wsdl";						
						var extraWsdlParameres = ExtraWsdlParameres.get_Text();	
						"extraWsdlParameres: {0}".debug(extraWsdlParameres ?? "");
						var cSharpFile = sdl_Wsdl.wsdl_CreateAssembly(wsdlToLoad,null,extraWsdlParameres); 
						//var cSharpFile = sdl_Wsdl.wsdl_CreateCSharp(wsdlToLoad); 
						var compiledAssemblyPath = load_Wsdl_From_CSharpFile(cSharpFile);	
						if (Cache_WSDL_Dll)
						{
							//create a cache copy of the dll	
							"Saving {0} dll cache to {1}".info(compiledAssemblyPath,cacheLocation);
							Files.Copy(compiledAssemblyPath, cacheLocation);
						}
					  });
		}
		
		public ascx_WSDL_Creation_and_Execution open(string wsdlToLoad)
		{
			this.WsdlLocation.set_Text(wsdlToLoad);
			
			var cacheLocation = getCacheFileName(wsdlToLoad);
			if (Cache_WSDL_Dll && cacheLocation.fileExists())
				wsdlToLoad = cacheLocation;

			if (wsdlToLoad.isUri())					
				load_Wsdl_From_WSDL_File(wsdlToLoad);														
			else if (wsdlToLoad.fileExists())
			{
				if (wsdlToLoad.extension(".cs"))
					load_Wsdl_From_CSharpFile(wsdlToLoad);
				else
				{
					if (wsdlToLoad.extension(".dll"))
						load_Wsdl_From_Assembly(wsdlToLoad.assembly());
					else
						load_Wsdl_From_WSDL_File(wsdlToLoad);
				}					
			}
			return this;
		}
		
		public ascx_WSDL_Creation_and_Execution open(string wsdlToLoad, string cachedFile)
		{
			if (cachedFile.fileExists())
				return open(cachedFile);
			else
				return open(wsdlToLoad);			
		}
		
		public void markGuiAs_Busy() 
		{
			Methods_TreeView.backColor(Color.LightPink);
		}
		 
		public void markGuiAs_OK()
		{
			Methods_TreeView.backColor(Color.White);
		}
		
		public string getCacheFileName(string wsdlLocation)
		{	
			return "".tempDir(false).pathCombine(wsdlLocation.safeFileName() +".dll");
		}
		
		//doesn't work because dll will be in use
		/*public void deleteCachedFile()
		{
			var loadedWsdl = WsdlLocation.get_Text();
			if (loadedWsdl.valid())
			{
				var cachedFile = getCacheFileName(loadedWsdl);
				if(cachedFile.fileExists())
				{
					"Deleting File: {0}".info(cachedFile);
					Files.deleteFile(cachedFile);
				}
			}
		}*/
		
		public void addScriptingSupport()
		{
			Func<string> getDefaultScript = 
			()=>{
					var cSharpFile = new DotNet_SDK_WSDL().wsdl_CreateCSharp(this.WsdlLocation.get_Text()); 					
					
					var parametersCreation = "".line();
					var invocationParameters = "";
					
					foreach(var parameter in this.CurrentMethod.parameters())
					{
						var parameterName = parameter.Name.lowerCaseFirstLetter();
						var parameterDefaultValue = "";
						switch (parameter.ParameterType.FullName)
						{
							case "System.String":
								parameterDefaultValue = "\"\"";
								break;
							case "System.Int32":
								parameterDefaultValue = "0";
								break;	
							default:
								parameterDefaultValue = "new {0}()".format(parameter.ParameterType.FullName);
								break;
						}
						parametersCreation += "var {0} = {1};".line().format(parameterName, parameterDefaultValue);
						invocationParameters += "{0} ,".format(parameterName);
					}
										
					parametersCreation = (parametersCreation.valid()) ? parametersCreation.lineBeforeAndAfter() : "";
					invocationParameters = invocationParameters.removeLastChar();
					
					var varName = this.CurrentMethod.DeclaringType.Name.lowerCaseFirstLetter();					
					var scriptText = "var {0} = new {1}();".line().format(varName, this.CurrentMethod.DeclaringType.FullName) + 
									 parametersCreation + 									 
									 "var response = {0}.{1}({2});".line().format(varName, this.CurrentMethod.Name,  invocationParameters) + 
									 "return response; // response type is: {0}".line().format(this.CurrentMethod.ReturnType.FullName) + 
									 "".line() + 
									 "//O2File:{0}".line().format(cSharpFile) +
									 "//O2Ref:System.Web.Services.dll";										
					return scriptText;
				}; 
		
		Action openScriptEditorForCurrentMethod = 
			()=>{
					//O2.Kernel.O2LiveObjects.set("SoapParametersObject", wsdlCreation.SoapParametersObject);
					//O2.Kernel.O2LiveObjects.set("CurrentMethod", wsdlCreation.CurrentMethod);
		
		 
					var script = "WebService Script Execution".showAsForm(700,400).add_Script(false);   
					
					script.InvocationParameters.Add("assembly", this.WsdlAssembly); 
					script.InvocationParameters.Add("currentType", this.CurrentMethod.DeclaringType.Name); 
					script.InvocationParameters.Add("currentMethod", this.CurrentMethod.Name); 
					script.Code = "....Creating WebService Method invocation script ...";
					script.Code = getDefaultScript();
				};
					
		//return wsdlCreation.CurrentMethod.typeName();   
		
		 
		 this.Methods_TreeView.insert_Above(20)
							  .add_Link("Open Script Editor for selected Method",0,0, ()=>openScriptEditorForCurrentMethod());
		 							  
		}
		
		public static ascx_WSDL_Creation_and_Execution createAndOpen(string wsdlToOpen)
		{
			var wsdlGui = "WSDL Execution for: {0}".format(wsdlToOpen)
										    	   .showAsForm<ascx_WSDL_Creation_and_Execution>(1000,400);
													    	   
			O2Thread.mtaThread(()=> wsdlGui.open(wsdlToOpen));;
			return wsdlGui;
		}
	}
}