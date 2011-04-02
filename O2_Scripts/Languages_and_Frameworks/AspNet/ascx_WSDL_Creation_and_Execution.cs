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
//O2File:DotNet_SDK_WSDL.cs

//O2Ref:O2_API_AST.dll


namespace O2.XRules.Database.Languages_and_Frameworks.AspNet
{
	public class test_ascx_WSDL_Creation_and_Execution
	{		
		public void launchGui()
		{
			
			typeof(ascx_WSDL_Creation_and_Execution).showAsForm();			
		}
	}

	public class ascx_WSDL_Creation_and_Execution : Control
	{	
		public TextBox WsdlLocation { get; set; }
		public TreeView Methods_TreeView { get; set; } 	
		public TextBox ExecutionResult { get; set; } 	
		public TreeView ExecutionResult_TreeView { get; set; } 	
		public PropertyGrid ExecutionResult_Properties { get; set; } 	
		
		public string CSharpFile { get; set; }
		public Assembly WsdlAssembly { get; set; }
		public MethodInfo CurrentMethod { get; set; }
		
		public ascx_WSDL_Creation_and_Execution()
		{
			this.Width = 700;
			this.Height = 500;
			buildGui();
		}
		
		public ascx_WSDL_Creation_and_Execution buildGui() 
		{			
			var topPanel = this.add_Panel();
			 
			
			//var assemblyInvoke = topPanel.add_Control<O2.External.SharpDevelop.Ascx.ascx_AssemblyInvoke>();
			//assemblyInvoke.loadAssembly("OnlineStorage.dll".assembly());
			var methodProperties = topPanel.add_DataGridView();
			
			
			Methods_TreeView = methodProperties.insert_Left("Soap Methods").add_TreeView();
			ExecutionResult = methodProperties.insert_Below().add_TextArea();
			ExecutionResult_Properties = ExecutionResult.insert_Right().add_PropertyGrid();
			ExecutionResult_TreeView = ExecutionResult_Properties.insert_Below().add_TreeView();
			var executeButton = methodProperties.insert_Below(30).add_Button("Execute Soap request").fill();
			
			Methods_TreeView.afterSelect<MethodInfo>(
				(methodInfo) => {								
									CurrentMethod = methodInfo;												
									new O2FormsReflectionASCX().loadMethodInfoParametersInDataGridView(methodInfo, methodProperties);
								});
			
			executeButton.onClick(
				()=>{
						/*var parameters = new O2FormsReflectionASCX().getParameterObjectsFromDataGridColumn(methodProperties, "Value");				
						ExecutionResult.set_Text("[{0}] Executing method : {1}".format(DateTime.Now,CurrentMethod.Name)); 
						
						var liveObject = CurrentMethod.DeclaringType.ctor();
						ExecutionResult.append_Line("Created dynamic proxy object of type: {0}".format(liveObject));						
						
						try
						{
							var result = CurrentMethod.Invoke(liveObject, parameters);
							ExecutionResult.append_Line("Method Executed OK, here is the return value:");
							ExecutionResult.append_Line(result.str().lineBeforeAndAfter());
							ExecutionResult_Properties.show(result);	
							ExecutionResult_TreeView.xmlShow(result.serialize());
						}
						catch (Exception ex)
						{				
							ExecutionResult.append_Line(ex.InnerException.Message);
							ExecutionResult.append_Line(ex.InnerException.StackTrace);
						}
						
						//currentMethod.invoke(parameters); 
						
						ExecutionResult.append_Line("Execution complete");*/
					});
						
			
			WsdlLocation = topPanel.insert_Above(20)
								   .add_LabelAndTextAndButton("WSDL location","","Load",open)
								   .control<TextBox>();



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
				Methods_TreeView.add_Nodes(soapMethods); 
			}
			else
				"Provided Assembly was null".error();
			markGuiAs_OK();
		}	
		
		public void load_Wsdl_From_CSharpFile(string csharpFile)
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
				}
				else
					"Error creating assembly from wsdl file: {0}".error(csharpFile);
			}
		}
		
		public void load_Wsdl_From_WSDL_File(string wsdlToLoad)
		{
			markGuiAs_Busy();			
			O2Thread.mtaThread(
				()=> {
						this.WsdlLocation.set_Text(wsdlToLoad);
						
						var sdl_Wsdl = new DotNet_SDK_WSDL(); 			
						if (wsdlToLoad.fileExists().isFalse() && wsdlToLoad.isUri() && wsdlToLoad.ends("wsdl").isFalse())
							wsdlToLoad += "?wsdl";							
						sdl_Wsdl.wsdl_CreateAssembly(wsdlToLoad); 
						var cSharpFile = sdl_Wsdl.wsdl_CreateCSharp(wsdlToLoad); 
						load_Wsdl_From_CSharpFile(cSharpFile);			
					  });
		}
		
		public void open(string wsdlToLoad)
		{
			if (wsdlToLoad.isUri())
				load_Wsdl_From_WSDL_File(wsdlToLoad);
			if (wsdlToLoad.fileExists())
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
		}
		
		public void open(string wsdlToLoad, string cachedFile)
		{
			if (cachedFile.fileExists())
				open(cachedFile);
			else
				open(wsdlToLoad);
		}
		
		public void markGuiAs_Busy()
		{
			Methods_TreeView.backColor(Color.LightPink);
		}
		
		public void markGuiAs_OK()
		{
			Methods_TreeView.backColor(Color.White);
		}
	}
}