// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.Views;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.External.WinFormsUI.Forms;
//O2Ref:Mono.Cecil.dll
using Mono.Cecil;
using Mono.Cecil.Cil;
using Cecil.Decompiler.ControlFlow;
//O2Ref:Cecil.Decompiler.dll
using O2.External.O2Mono.CecilDecompiler;
using O2.External.O2Mono.MonoCecil;
//O2Ref:nunit.framework.dll
using NUnit.Framework;
using O2.External.SharpDevelop.Ascx;
using O2.DotNetWrappers.Filters;
namespace O2.Script
//O2File:C:\O2\O2 - All Active Projects\O2 - All Active Projects\O2Core\O2_DotNetWrappers\Windows\Ascx_ExtensionMethods_2.cs

{

/*	public class debugTest
	{
		public static void test()
		{
			O2AscxGUI.launch();
			new test_ascx_MonoDecompiler().openAscxInNewWindow();
		}
	}*/

	[TestFixture]
    public class test_ascx_MonoDecompiler
    {        	

		//public string testExe = @"C:\O2\_tempDir\tmp37E2.tmp.dll";
		public string testExe = PublicDI.config.ExecutingAssembly;
		
		
		
		[Test]
        public string openAscxInNewWindow()
    	{    	    	    					
			var monoDecompiler = (ascx_MonoDecompiler)O2AscxGUI.openAscx(typeof(ascx_MonoDecompiler), O2DockState.Float,"Mono Decompiler");
			monoDecompiler.loadAssembly(testExe);	
			monoDecompiler.tvDirectoriesAndFiles.selectNode(0);
			return "click on method to view it";			
    	}    	    	    	    	    
	
	}
	
	public class ascx_MonoDecompiler : UserControl
	{ 
		private static IO2Log log = PublicDI.log;
	
		public TreeView tvDirectoriesAndFiles;
		public TreeView tvControlFlowGraph;
		public ascx_SourceCodeEditor sourceCodeEditor;
		
		public ascx_MonoDecompiler()
        {
        	log.info("in ascx_MonoDecompiler constructor");
            InitializeComponent();	            
        }
    
        public void InitializeComponent()
        {
        	var splitControl = this.addSplitContainer(
        						false, 		//setOrientationToHorizontal
        						true,		// setDockStyleoFill
        						true);		// setBorderStyleTo3D)
        	var leftGroupBox = splitControl.Panel1.addGroupBox("Methods");
            var rightGroupBox = splitControl.Panel2.addGroupBox("SourceCode");
            tvDirectoriesAndFiles = leftGroupBox.addTreeView();
            tvDirectoriesAndFiles.AfterSelect += tvDirectoriesAndFiles_AfterSelect;
            tvDirectoriesAndFiles.AllowDrop = true;
            tvDirectoriesAndFiles.DragEnter += tvDirectoriesAndFiles_DragEnter;
            tvDirectoriesAndFiles.DragDrop += tvDirectoriesAndFiles_DragDrop;                        
            
            var rightSplitControl = rightGroupBox.addSplitContainer(
        						true, 		//setOrientationToHorizontal
        						true,		// setDockStyleoFill
        						true);		// setBorderStyleTo3D)
            
            var topGroupBox = rightSplitControl.Panel1.addGroupBox("SourceCode");
            var bottomGroupBox = rightSplitControl.Panel2.addGroupBox("ControlFlowGraph");
            sourceCodeEditor = topGroupBox.addSourceCodeEditor();
            tvControlFlowGraph = bottomGroupBox.addTreeView();
            tvControlFlowGraph.BeforeExpand += tvDirectoriesAndFiles_BeforeExpand;
            // set-up size
            this.Width = 500;
            this.Height = 700;     
            leftGroupBox.Width = 400;
            //splitControl.Panel1.Width=300;
    	}
    	
    	private void tvDirectoriesAndFiles_DragEnter(object sender, DragEventArgs e)
    	{
        	Dnd.setEffect(e);
    	}
    	
    	private void tvDirectoriesAndFiles_DragDrop(object sender, DragEventArgs e)
        {
        	Assembly assembly = (Assembly)Dnd.tryToGetObjectFromDroppedObject(e, typeof(Assembly));
			if (assembly!= null)
        		loadAssembly(assembly);
        	else
            	loadAssembly(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

    	private void tvDirectoriesAndFiles_AfterSelect(object sender, EventArgs e)
    	{
    	 	if (tvDirectoriesAndFiles.SelectedNode != null && tvDirectoriesAndFiles.SelectedNode.Tag is MethodDefinition)
    		{    			
    			var methodDefinition = (MethodDefinition)tvDirectoriesAndFiles.SelectedNode.Tag;
    			
    			// add source code
    			var sourceCode = new CecilDecompiler().getSourceCode(methodDefinition);
    			sourceCodeEditor.setDocumentContents(sourceCode,"aaa.cs");        		
    			
    			// add ControFlowGraph
				tvControlFlowGraph.clear();
				var controlFlowGraph = ControlFlowGraph.Create(methodDefinition);    			                    
				tvControlFlowGraph.addNode(controlFlowGraph.ToString(), controlFlowGraph, true);				    			
    	 	}
    	}
    
    	private void tvDirectoriesAndFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    	{
    		var currentTreeNode = e.Node;    		
    		if (currentTreeNode.Tag != null)
			{
				tvDirectoriesAndFiles.clear(currentTreeNode);    		
				PublicDI.log.info("Expanding: {0}", currentTreeNode.Tag.GetType().Name);
				switch(currentTreeNode.Tag.GetType().Name)
				{
					case "ControlFlowGraph":
						addNodes(currentTreeNode, (ControlFlowGraph)currentTreeNode.Tag);												
						break;						
					case "InstructionBlock":
						addNodes(currentTreeNode, (InstructionBlock)currentTreeNode.Tag);												
						break;					
					case "Instruction":
						addNode(currentTreeNode, (Instruction)currentTreeNode.Tag);												
						break;	
					case "String":
					case "Int32":
					//case "Int16":
						addNode(currentTreeNode, currentTreeNode.Tag.ToString());
						break;	
					default:
						// used for not supported types
						tvDirectoriesAndFiles.addNode(currentTreeNode,"-> Properties in type:  " +  currentTreeNode.Tag.GetType().Name,"",false);						
						foreach(var property in PublicDI.reflection.getProperties(currentTreeNode.Tag))
						{
							var propertyValue = PublicDI.reflection.getProperty(property, currentTreeNode.Tag);
							var nodeText = string.Format("{0}: {1}           ({2})",  property.Name, propertyValue ?? "[null]", property);
							tvDirectoriesAndFiles.addNode(currentTreeNode, nodeText, propertyValue, propertyValue != null);
						}
						break;
				}											
			}    		
    	}
    	
    	public void addNodes(TreeNode treeNode, ControlFlowGraph controlFlowGraph)
    	{
    		foreach(var block in controlFlowGraph.Blocks)
    		{
    			var nodeText = string.Format("Block #{0}", block.Index);
    			tvDirectoriesAndFiles.addNode(treeNode,nodeText,block,true);
    		}
    	}
    	
    	public void addNodes(TreeNode treeNode, InstructionBlock instructionBlock)
    	{    		
    		foreach(var instruction in instructionBlock)
    			addNode(treeNode, instruction);
    	}
    	
		public void addNode(TreeNode treeNode, Instruction instruction)
    	{    		    			
			var nodeText = string.Format("{0} {1}", instruction.OpCode, instruction.Operand);
			var newNode = tvDirectoriesAndFiles.addNode(treeNode, nodeText);
			
//			if (instruction.Operand is Instruction)    		
//    			tvDirectoriesAndFiles.addNode(newNode, instruction.OpCode.ToString(),(Instruction)instruction.Operand,true);    		    		
			
			tvDirectoriesAndFiles.addNode(newNode, "OpCode details:",  instruction.OpCode,true);    			    			
			if (instruction.Operand !=null)
				tvDirectoriesAndFiles.addNode(newNode, "Operand details:",  instruction.Operand,true);    		
    	}
        	
		public void addNode(TreeNode treeNode, string value)
		{
			tvDirectoriesAndFiles.addNode(treeNode, "= " + value);
		}
    
    	public void loadAssembly(Assembly assemblyToLoad)    	
    	{
    		loadAssembly(assemblyToLoad.Location);
    	}
    	
    	public void loadAssembly(string assemblyToLoad)
    	{
    		log.debug("Loading assembly {0}", assemblyToLoad);
    		var assemblyDefinition = CecilUtils.getAssembly(assemblyToLoad);
    		Assert.That(assemblyDefinition != null, "could not load into an assembly the file: " + assemblyToLoad);
    		loadAssembly(assemblyDefinition);    
    	}
    	
    	public void loadAssembly(AssemblyDefinition assemblyDefinition)    	
    	{    	
    		tvDirectoriesAndFiles.invokeOnThread(
    			()=> {
    					tvDirectoriesAndFiles.Nodes.Clear();
			    		foreach(var method in CecilUtils.getMethods(assemblyDefinition))
			    		{			
			    			if (method.HasBody)
			    			{
			    				var filteredSignature = new FilteredSignature(method.ToString());					    			
			    				O2Forms.newTreeNode(tvDirectoriesAndFiles.Nodes, filteredSignature.sSignature, 0, method);	    			
			    			}
			    		}
			    		tvDirectoriesAndFiles.Sort();
    				 });
    		
    	}    	    	
    }
}