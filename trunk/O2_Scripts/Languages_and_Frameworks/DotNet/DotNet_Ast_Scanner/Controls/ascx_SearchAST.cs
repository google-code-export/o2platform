// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Text;
using System.Linq;
using System.Drawing;
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
using O2.XRules.Database.Utils;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;

//O2File:Ast_Engine_ExtensionMethods.cs
//O2File:SharpDevelop_O2MappedAstData_ExtensionMethods.cs
//O2File:TextEditor_O2CodeStream_ExtensionMethods.cs


namespace O2.XRules.Database.Languages_and_Frameworks.DotNet
{
	public class test_ascx_SearchAST
	{		
		public void launchGui()
		{
			var astData = new O2MappedAstData();
			
			//astData.loadFile("HacmeBank_v2_Website.ascx.PostMessageForm.btnPostMessage_Click.cs".local());
			
			var control = O2Gui.open<Panel>("test ascx_SearchAST",1000,600);
			var searchAST = control.add_Control<ascx_SearchAST>();
			searchAST.buildGui(astData);
			
			
		}
	}

	public class ascx_SearchAST : Control
	{
		public O2MappedAstData AstData {get;set;}	
		public TreeView AstValueTreeView { get; set; }
		public TreeView AstTypeTreeView { get; set; }
		public ascx_SourceCodeViewer CodeViewer { get; set; }
		public ProgressBar TopProgressBar { get; set; }
		public String INodeTypeFilter { get; set; }
		public String INodeValueFilter { get; set; }
		
		public Dictionary<string,List<INode>> iNodesByType;
		
		public ascx_SearchAST()//astData astEngine)
		{		
			iNodesByType = new  Dictionary<string,List<INode>>();
		}	
		
		public void buildGui(O2MappedAstData astData) 
		{
			AstData = astData;
			INodeTypeFilter = "";
			INodeValueFilter = "";
			buildGui();
			loadDataInGui();
		}
		
		public void buildGui()
			{
				
				//..clear();
				var topPanel = this.add_1x1("AST INode Value", "Source Code", true, 400);
				
				CodeViewer = topPanel[1].add_SourceCodeViewer(); 
				
				AstValueTreeView = topPanel[0].add_TreeView()
											  .sort() 
											  .showSelection()
											  .beforeExpand<List<INode>>(
												  	(selectedNode, nodes)=>{
												  				 selectedNode.add_Nodes(nodes);
												  			 });
				
				 
				AstData.afterSelect_ShowInSourceCodeEditor(AstValueTreeView, CodeViewer.editor());  		   
				AstTypeTreeView  = topPanel[0].insert_Left<GroupBox>(200)
										.set_Text("AST INode Type") 
										.add_TreeView()
										.sort()
										.showSelection()
										.afterSelect<List<INode>>(
											(iNodes)=>{ 	 															
												 		  showINodes(iNodes);
													  });
				
													 		 
				AstTypeTreeView.insert_Above<TextBox>(20)
							   .onTextChange_AlertOnRegExFail()
							   .onEnter((value)=>{
													INodeTypeFilter= value;
													loadDataInGui();
												 });

				//nodeFilterTextBox.onTextChange_AlertOnRegExFail();
				//nodeTypeTextBox.onTextChange_AlertOnRegExFail();
							 
				AstValueTreeView.insert_Above<TextBox>(20)
								.onTextChange_AlertOnRegExFail()
								.onEnter(
								(value)=>{
											INodeValueFilter= value;
											showINodes(null);
										 });
			
			AstTypeTreeView.onDrop(
				(fileOrFolder)=>{
									if(fileOrFolder.fileExists())
										AstData.loadFile(fileOrFolder);
									else
										AstData.loadFiles(fileOrFolder.files(true,"*.cs","*.vb"));
									loadDataInGui();
								});
														   							
			}
			
			public void showINodes(List<INode> iNodes)
			{
				if (iNodes == null)
					if (AstTypeTreeView.selected() ==null)
						return;
					else
						iNodes = (List<INode>)AstTypeTreeView.selected().tag<List<INode>>(); 						
				AstValueTreeView.backColor(Color.Azure);
                //AstValueTreeView.visible(false);
                AstValueTreeView.clear();
				O2Thread.mtaThread(
					()=>{  
							var indexedData = iNodes.indexOnToString(INodeValueFilter);   							
							var typeName = iNodes[0].typeName();			 																																		 	
							var rootNode = AstValueTreeView.add_Node(typeName, null);
							rootNode.add_Nodes(indexedData, 100, TopProgressBar); 
							AstValueTreeView.visible(true);
							if (AstValueTreeView.nodes().size() > 0 && AstValueTreeView.nodes()[0].nodes().size() >0 )
								AstValueTreeView.nodes()[0].nodes()[0].selected(); 		  																		
							rootNode.expand();														
							AstValueTreeView.backColor(Color.White);
						});
			}
			
			public void loadDataInGui()
			{
				AstTypeTreeView.clear();
				AstValueTreeView.clear();
				iNodesByType = AstData.iNodes_By_Type(INodeTypeFilter); 						
				
				foreach(var item in iNodesByType)
				{					
					var nodeText = "{0}   ({1})".format(item.Key, item.Value.size()); 	
					AstTypeTreeView.add_Node(nodeText, item.Value);						
				}
				
				AstTypeTreeView.selectFirst();
			}			
			
	}
}
