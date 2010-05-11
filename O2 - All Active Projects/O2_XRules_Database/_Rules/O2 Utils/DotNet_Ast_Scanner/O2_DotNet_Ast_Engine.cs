// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI; 
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.SharpDevelop.Dom;
//O2Ref:QuickGraph.dll
//O2File:C:\O2\_XRules_Local\Extra_methods.cs

namespace O2.Script
{
    public class O2_DotNet_Ast_Engine : Control
	{	 	
		
	 	public Panel HostPanel { get; set; }
	 	public TreeView Step_1_TreeView_SourceFiles { get; set; }	 	
	 	
	 	public string step_1_WikiHelpPage = "O2 DotNet Ast Engine - Step 1: load source code files";
	 	public string step_2_WikiHelpPage = "O2 DotNet Ast Engine - Step 2: view method streams";
	 	
	 	public O2MappedAstData AstData { get; set; }

		public O2_DotNet_Ast_Engine()
		{
			AstData = new O2MappedAstData(); 	
		}
		
	 	public void start()
	 	{
	 		var astEngine = O2Gui.open<O2_DotNet_Ast_Engine>("O2 .NET Ast Engine", 600,400);
	 		astEngine.buildGui();		 			 		
	 	}
	 	
		public void buildGui() 
		{		 
			var controls = this.add_1x1("actions", "", false, 40);		
			controls[0].add_Link("Step 1: load source code files", 15, 2, step1)
			           .append_Link("Step 2: view method streams", step2)
			           .append_Link("Step 3: ...", null).enabled(false)
			           .append_Link("Step 4: ...", null).enabled(false)
			           .append_Link("Step 5: ...", null).enabled(false);
			
			HostPanel = controls[1].add_Panel();
			
			HostPanel.parent<SplitContainer>().BorderStyle = BorderStyle.None;  			
			HostPanel.parent<SplitContainer>().FixedPanel = FixedPanel.Panel1;
		}
		
		
		public void step1()
		{			
			HostPanel.clear();
			HostPanel.backColor("Control");  
			Step_1_TreeView_SourceFiles =  HostPanel.add_TreeView(); 
			var Step_1_Browser = Step_1_TreeView_SourceFiles.insert_Above<Panel>(100);
			Step_1_Browser.add_WikiHelpPage(step_1_WikiHelpPage);	    						
			Step_1_TreeView_SourceFiles.insert_Below<Panel>(20)
			       	                   .add_Link("clear",0,0,()=> Step_1_TreeView_SourceFiles.clear());

			Step_1_TreeView_SourceFiles.invokeOnThread(
			()=> 
				Step_1_TreeView_SourceFiles.onDrop((fileOrFolder)=> step1_loadSourceFiles(fileOrFolder))				
				);
		}
		 
		public void step1_loadSourceFiles(string fileOrFolder)
		{
			if (fileOrFolder.fileExists())			
				AstData.loadFile(fileOrFolder);				
			else if (fileOrFolder.dirExists())
				AstData.loadFiles(fileOrFolder.files("*.cs",true));		
				
			Step_1_TreeView_SourceFiles.clear();
			Step_1_TreeView_SourceFiles.add_Nodes(AstData.files());
		}
		
		public void step2()
		{
			HostPanel.clear();
			
			var topControls = HostPanel.add_1x1("Source Files","Method Streams",false);
			var files_treeView = topControls[0].add_TreeView();
			files_treeView.add_Nodes(AstData.files());
			var childControls = topControls[1].add_1x1("Methods","Stream",true,300); 
			var methods_TreeView = childControls[0].add_TreeView(); 
			var sourceCode = childControls[1].add_SourceCodeViewer();
			
			files_treeView.parent<SplitContainer>().FixedPanel = FixedPanel.Panel1;
			methods_TreeView.parent<SplitContainer>().FixedPanel = FixedPanel.Panel1;
			
			files_treeView.invokeOnThread(
			()=> 
				files_treeView.onDrop(
					(fileOrFolder)=>{
										step1_loadSourceFiles(fileOrFolder);
										files_treeView.clear();
										files_treeView.add_Nodes(AstData.files());
									})
				);
			files_treeView.afterSelect<string>(
				(file)=>{
							methods_TreeView.clear(); 
							foreach(var iNode in AstData.iNodes<MethodDeclaration>(file)) 
							{
								var iMethod = AstData.iMethod(iNode);
								methods_TreeView.add_Node(iMethod.name(),iMethod); 
							}
							//sourceCode.open(file);
						});
			
			methods_TreeView.afterSelect<IMethod>(
				(iMethod)=>{
							var methodStream = AstData.createO2MethodStream(iMethod);
							sourceCode.set_Text(methodStream.csharpCode());
							//"method stream:{0}".info(methodStream);
						});
						
			//HostPanel.backColor(Color.White);  
			//Step__TreeView_SourceFiles =  HostPanel.add_TreeView(); 
			//var Step_1_Browser = Step_1_TreeView_SourceFiles.insert_Above<Panel>(100);
			//Step_1_Browser.add_WikiHelpPage(step_2_WikiHelpPage);	    						
		}

	/*
			var dir = "C:\\O2\\DemoData\\HacmeBank_v2.0 (Dinis version - 7 Dec 08)\\HacmeBank_v2_WS\\WebServices";
			var targetFolder = PublicDI.config.getTempFolderInTempDirectory("MethodStreams");
			
			var o2MappedAstData = new O2MappedAstData();
			o2MappedAstData.O2AstResolver.addReference("System.Data");
	
			var targetSourceFiles = (dir + "\\..").fullPath().files("*.cs", true);
	 
			o2MappedAstData.loadFiles(targetSourceFiles);		
	
			var originalFile = this.add_SourceCodeViewer();
	
			var treeView = originalFile.insert_Left<Panel>(300).add_Directory().open(dir).afterFileSelect(file =>
			{
				originalFile.set_Text(file.fileContents());
				o2MappedAstData.createO2MethodStreamFiles(dir.files()[1], targetFolder);
			}).getTreeView();
	
			treeView.SelectedNode = treeView.Nodes[2];
	
			var methodStreamViewer = originalFile.insert_Below<ascx_SourceCodeViewer>();
	
			var createdFiles = methodStreamViewer.insert_Left<Panel>(300).add_Directory(targetFolder).afterFileSelect(file => methodStreamViewer.open(file));
	*/
		
	
	}
}
