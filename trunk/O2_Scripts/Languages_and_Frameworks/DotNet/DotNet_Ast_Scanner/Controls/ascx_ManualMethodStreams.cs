// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX.Ascx.MainGUI;
using O2.Views.ASCX.classes.MainGUI; 
using O2.Views.ASCX.O2Findings;
using O2.Views.ASCX.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.AST;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.API.AST.CSharp;
using O2.API.AST.ExtensionMethods;
using O2.API.AST.ExtensionMethods.CSharp;
using O2.XRules.Database.Utils.ExtensionMethods;
using O2.XRules.Database.Utils;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;

//O2File:Ast_Engine_ExtensionMethods.cs
//O2File:SharpDevelop_O2MappedAstData_ExtensionMethods.cs
//O2File:TextEditor_O2CodeStream_ExtensionMethods.cs
//O2File:HtmlAgilityPack_ExtensionMethods.cs 

//O2File:Scripts_ExtensionMethods.cs   


namespace O2.XRules.Database.Languages_and_Frameworks.DotNet
{
	public class test_ascx_ManualMethodStreams
	{
		public void launchGui()
		{
			//var astData = new O2MappedAstData();
			//astData.loadFiles(@"C:\O2\DemoData\HacmeBank_v2.0 (Dinis version - 7 Dec 08)\HacmeBank_v2_WS\classes".files());
			
			var control = O2Gui.open<Panel>("test ascx_ManualMethodStream",1000,600);
			var manualMethodStreams = control.add_Control<ascx_ManualMethodStreams>();
			
			manualMethodStreams.buildGui(); 			
			var testFile = @"C:\O2\O2 Demos\HacmeBank_WebSite_FLAT_VIEW\2nd Batch\HacmeBank_v2_Website.Main.Page_Load.cs";
			manualMethodStreams.loadFile(testFile);
			
		}
	}

	public class ascx_ManualMethodStreams : Control
	{
		//public O2MappedAstData AstData {get;set;}
	
		public O2MappedAstData AstData_MethodStream { get; set; }
		//public O2MethodStream MethodStream { get; set; }
		//public O2CodeStream CodeStream { get; set; }			
		//public O2CodeStreamTaintRules TaintRules { get; set; }
		public String MethodStreamFile { get; set; }
		public INode CurrentINode { get; set; }
		
		//public TreeView MethodsTreeView  { get; set; }
		public TreeView ParametersTreeView  { get; set; }
		public TreeView MethodsCalledTreeView { get; set; }
		public ascx_SourceCodeViewer CodeViewer { get; set; }			
//		public GraphLayout CodeStreamGraph { get; set; }	
		public TreeView CodeStreamTreeView { get; set; }	
		public TabPage CodeStreamGraphTab { get; set; }
		public TabPage CodeStreamTreeViewTab { get; set; }
		public Label CurrentINodeLabel { get; set; }
		
		public LinkLabel SaveCodeLink { get; set; }
		public LinkLabel showINodeLink { get; set; }
		//public String MethodsFilter { get; set; }
		public ascx_SourceCodeViewer CodeStreamCodeViewer  { get; set; }	
		
		public ascx_ManualMethodStreams()//astData astEngine)
		{
		//	AstData = astData
			//buildGui();
			//loadDataInGui();
			//TaintRules = new O2CodeStreamTaintRules(); 
			//TaintRules.add_TaintPropagator("System.String.Concat");
		}	
		
		public void buildGui()
		{
			//AstEngine.HostPanel.clear();												
			var topPanel = this.add_1x1("Methods & Parameters", "Source Code", true);
			var tabControl = topPanel[1].add_TabControl(); 
			//CodeViewer = tabControl.add_Tab("Source Code").add_SourceCodeViewer();				
			CodeViewer = topPanel[0].add_SourceCodeViewer();
			CodeViewer.editor().colorCodeForExtension(".cs");
			CodeStreamTreeViewTab = tabControl.add_Tab("CodeStream TreeView");
			//CodeStreamGraphTab = tabControl.add_Tab("CodeStream Graph");
			//CodeStreamGraphTab.backColor(Color.White);				
			//CodeStreamCodeViewer = CodeStreamTreeViewTab.add_SourceCodeViewer();
			//CodeStreamTreeView = CodeStreamCodeViewer.insert_Left<TreeView>(200);
			CodeStreamTreeView = CodeStreamTreeViewTab.add_TreeView();
			//CodeStreamGraph = CodeStreamGraphTab.add_Panel().add_Graph(); 
			
			ParametersTreeView =  CodeViewer.insert_Below<TreeView>(100)
											.showSelection()
											.sort();
			MethodsCalledTreeView =  ParametersTreeView.insert_Right<TreeView>(250)
													   .showSelection()
													   .sort();
			var commandsPanel = CodeViewer.insert_Below<Panel>(20);
			SaveCodeLink = commandsPanel.add_Link("save code", 0,0, ()=> saveEditorContents());										
			showINodeLink = commandsPanel.add_Link("show INode CodeStream", 0,60, ()=> showCurrentINodeCodeStream());
			CurrentINodeLabel = commandsPanel.add_Label("current INode: ", 0,200); 
			 
			// context menu
			 
			/*MethodsCalledTreeView.add_ContextMenu()
					  		     .add_MenuItem("Show Method Stream Details", ()=> showMethodStreamDetails(CodeViewer.get_Text()));
			*/	
			//events
			
			CodeStreamTreeView.afterSelect<O2CodeStreamNode>
			 	((streamNode)=> CodeViewer.editor().setSelectionText(streamNode.INode.StartLocation, streamNode.INode.EndLocation));				
			
			MethodsCalledTreeView.afterSelect<INode>((iNode)=>createAndShowCodeStream(iNode));
			ParametersTreeView.afterSelect<INode>((iNode)=>createAndShowCodeStream(iNode));
			
			CodeViewer.onCaretMove((caret)=> findINodeAtCaretLocation(caret));
			//CodeViewer.editor().eDocumentDataChanged += (text)=> saveEditorContents();
			this.insert_Below<Panel>(100).add_LogViewer();				
			
			commandsPanel.onDrop(
				(file)=>{
							if (file.fileExists())
								loadFile(file);
						});									
		}
		
		public void loadDataInGui()
		{
		
		}
		
		public void loadFile(string fileToLoad)
		{
			CodeViewer.load(fileToLoad);
			MethodStreamFile = fileToLoad;
			processCodeViewerContents();
		}
		
		public void saveEditorContents()
		{
			
			if (MethodStreamFile.fileExists())
				Files.deleteFile(MethodStreamFile);			// delete previous file since we don't need it anymore
			
			"saving editor contents: {0}".info(MethodStreamFile);	
			var code = CodeViewer.get_Text();	
			MethodStreamFile = code.saveWithExtension(".cs");
			processCodeViewerContents();				
		}
		
		public void processCodeViewerContents()
		{
			"Processing source code: {0}".info(MethodStreamFile);									
			
			//O2AstResolver cachedO2AstResolver = null;
			//if (AstData_MethodStream != null)
			//	cachedO2AstResolver = AstData_MethodStream.O2AstResolver;
			
			AstData_MethodStream = new O2MappedAstData(); 														
										
			AstData_MethodStream.loadFile(MethodStreamFile);	
			AstData_MethodStream.showMethodStreamDetailsInTreeViews(ParametersTreeView, MethodsCalledTreeView);
		}
		
		public void findINodeAtCaretLocation(ICSharpCode.TextEditor.Caret caret)
		{	
			
			if (AstData_MethodStream!=null)
			{
				var iNode = AstData_MethodStream.iNode(MethodStreamFile, caret);
				if (iNode != null)	
				{
					//CodeViewer.editor().selectTextWithColor(iNode);	
					if (iNode is TypeReference)
						iNode = iNode.Parent;
						
					CurrentINode = iNode;
					CurrentINodeLabel.set_Text("current iNode:{0}".format(iNode.typeName()));						
					
					//"current iNode:{0} : {1}".debug(iNode.typeName(), iNode);	
					
					//createAndShowCodeStream(iNode);
				}
			}				
		}
		
		/*public void showMethodStreamDetails(string code)
		{
			code.info();				
							
			
			AstData_MethodStream = new O2MappedAstData(); 							
			AstData_MethodStream.loadFile(MethodStreamFile);	
			AstData_MethodStream.showMethodStreamDetailsInTreeViews(ParametersTreeView, MethodsCalledTreeView);
		}*/
		
		public void showCurrentINodeCodeStream()
		{
			createAndShowCodeStream(CurrentINode);
		}
		
		public void createAndShowCodeStream(INode iNode)
		{
			if (iNode != null)
			{
				CodeViewer.editor().selectTextWithColor(iNode);	
				
				var taintRules = new O2CodeStreamTaintRules();
				//taintRules.add_TaintPropagator("System.Int32.Parse");					
				var codeStream = AstData_MethodStream.createO2CodeStream(taintRules, MethodStreamFile,iNode);
				//var codeStream = AstData_MethodStream.createO2CodeStream(MethodStreamFile,iNode);
				if (codeStream.hasPaths())
					showCodeStream(codeStream);
			}
		}
		
		public void showCodeStream(O2CodeStream codeStream)
		{							
			codeStream.show(CodeViewer.editor());
			codeStream.show(CodeStreamTreeView);
			CodeStreamTreeView.expand();
		}
		
	}
}
