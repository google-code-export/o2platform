// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods; 

using O2.XRules.Database.Utils;

//O2File:ascx_Simple_Script_Editor.cs.o2
//O2File:Scripts_ExtensionMethods.cs
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.Utils
{
    public class ascx_IE_ScriptExecution : Control
    {   
        public Panel topPanel { get; set; }
        public ascx_Simple_Script_Editor script {get; set;}        
		public bool EnableCodeComplete { get; set;}
		
		
		public static ascx_IE_ScriptExecution launchGui()
		{
			return O2Gui.open<ascx_IE_ScriptExecution>("IE Script Execution", 600,400)
						.buildGui();
		}
		
		public static ascx_IE_ScriptExecution launchGui_NoCodeComplete()
		{
			var host = O2Gui.open<Panel>("IE Script Execution (no code complete)", 600,400);
			return (ascx_IE_ScriptExecution)host.invokeOnThread(
				()=>{
						var ieExecution = new ascx_IE_ScriptExecution(false).fill();						
						ieExecution.buildGui("");
						host.add_Control(ieExecution);
						return ieExecution;
					});
			
						
		}
		
    	public ascx_IE_ScriptExecution() : this (true)
    	{    		
    	}
    	
    	public ascx_IE_ScriptExecution(bool enableCodeComplete)
    	{
    		this.Width = 600;
    		this.Height = 400;
    		EnableCodeComplete = enableCodeComplete;
    	}
    	
    	public ascx_IE_ScriptExecution buildGui()
    	{
    		return buildGui("ie.open(\"http://www.google.com\");");
    	}
		public ascx_IE_ScriptExecution buildGui(string customScript)
		{
			topPanel = this.add_Panel();			

			script = topPanel.insert_Below<Panel>().add_Script(EnableCodeComplete);
			script.InvocationParameters.Add("panel",topPanel); 
			script.onCompileExecuteOnce();
			if (customScript.valid())
				script.set_Command(getScript(customScript));
			return this;
		}
		
		public string getScript(string customScript)
		{						
			return getScriptWrapper().format(customScript);
		}
		
		public string getScriptWrapper()
		{
			var scriptWrapper = "var topPanel = panel.clear().add_Panel();".line() + 
								"var ie = topPanel.add_IE().silent(true);".line().line() +
								"{0}".line().line() +
								"//O2File:WatiN_IE_ExtensionMethods.cs".line() + 
								"//using O2.XRules.Database.Utils.O2".line() + 
								"//O2Ref:WatiN.Core.1x.dll";
			return scriptWrapper;								
		}
		
		public ascx_IE_ScriptExecution setScript(string scriptCode)
		{
			script.Code = scriptCode;
			return this;
			
		}
		
		public ascx_IE_ScriptExecution add_SourceCodeEditor(string sourceFile)		
 		{
 			var sourceCodeEditor = topPanel.insert_Right().add_SourceCodeEditor();				
 			sourceCodeEditor.open(sourceFile);
 			return this;
 		}
 		
 		public ascx_IE_ScriptExecution enableCodeComplete(bool value)
 		{
 			this.EnableCodeComplete = value;
 			return this;
 		}
 		
 		
 		public ascx_IE_ScriptExecution createSpecialTestGui(Action<Panel> buildControl)
 		{
 			
 			var treeViewPanel = topPanel.insert_Left(200,"Gui Actions");
 			Action buildMethodsInvocationList = null;
 			
 			buildControl(treeViewPanel);
 			
			//buildMethodsInvocationList = 
			//	()=>{
			//			buildControl(treeViewPanel);
			return this;
		}		
					
 		/*public ascx_IE_ScriptExecution createSpecialTestGui(string file, string baseTypeName, WatiN_IE ie)
 		{
 			
 			var treeViewPanel = topPanel.insert_Left(200,"Gui Actions");
 			Action buildMethodsInvocationList = null;
 			
			buildMethodsInvocationList = 
				()=>{
						buildControl(treeViewPanel);
						var assembly = file.compile();
						var methodsToInvoke = file.compile().withAttribute("ShowInGui");		
						var typeObject = assembly.type(baseTypeName).ctor(ie);
						treeViewPanel.clear()
									 .add_TreeView()
									 .sort()
									 .add_Nodes(methodsToInvoke, (method)=>method.Name)
									 
					.afterSelect<MethodInfo>(	
					   		  (methodToInvoke)=>
					   	  		O2Thread.mtaThread(
					   	  			()=> methodToInvoke.DeclaringType.invokeStatic(methodToInvoke.Name, typeObject) ))
					   	  	;
					   	 //.add_ContextMenu().add_MenuItem("Reload", ()=> buildMethodsInvocationList());
									 
					};
					
			buildMethodsInvocationList();		
 			return this;
 		}*/
 		
 		
		
    }
}
