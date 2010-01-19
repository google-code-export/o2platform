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
//O2Ref:Cecil.Decompiler.dll
using O2.External.O2Mono.CecilDecompiler;
using O2.External.O2Mono.MonoCecil;
//O2Ref:nunit.framework.dll
using NUnit.Framework;
using O2.External.SharpDevelop.Ascx;
using O2.DotNetWrappers.Filters;
namespace O2.Script
{
	[TestFixture]
    public class test_ascx_MonoControlFlowGraph
    {        	

		//public string testExe = @"C:\O2\_tempDir\tmp37E2.tmp.dll";
		public string testExe = PublicDI.config.ExecutingAssembly;
		
		
		
		[Test]
        public string openAscxInNewWindow()
    	{    	    	    					
			var monoDecompiler = (ascx_MonoDecompiler)O2AscxGUI.openAscx(typeof(ascx_MonoDecompiler), O2DockState.Float,"Mono Decompiler");
			monoDecompiler.loadAssembly(testExe);		
			return "click on method to view it";			
    	}    	    	    	    	    
	
	}
	
	public class ascx_MonoControlFlow : UserControl
	{ 
		private static IO2Log log = PublicDI.log;
	
		public TreeView tvDirectoriesAndFiles;
		public ascx_SourceCodeEditor sourceCodeEditor;

        public ascx_MonoControlFlow()
        {
            log.info("in ascx_MonoControlFlow constructor");
            InitializeComponent();	            
        }
    
        public void InitializeComponent()
        {
        	var splitControl = this.add_SplitContainer(
        						false, 		//setOrientationToHorizontal
        						true,		// setDockStyleoFill
        						true);		// setBorderStyleTo3D)
        	var leftGroupBox = splitControl.Panel1.add_GroupBox("Methods");
            var rightGroupBox = splitControl.Panel2.add_GroupBox("SourceCode");
            tvDirectoriesAndFiles = leftGroupBox.add_TreeView();
            tvDirectoriesAndFiles.AfterSelect += tvDirectoriesAndFiles_AfterSelect;
            tvDirectoriesAndFiles.AllowDrop = true;
            tvDirectoriesAndFiles.DragEnter += tvDirectoriesAndFiles_DragEnter;
            tvDirectoriesAndFiles.DragDrop += tvDirectoriesAndFiles_DragDrop;
            
            //tvControlFlowGraph = 
            sourceCodeEditor = rightGroupBox.add_SourceCodeEditor(); 
            
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
            loadAssembly(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

    	private void tvDirectoriesAndFiles_AfterSelect(object sender, EventArgs e)
    	{
    	 	if (tvDirectoriesAndFiles.SelectedNode != null && tvDirectoriesAndFiles.SelectedNode.Tag is MethodDefinition)
    		{
    			var methodDefinition = (MethodDefinition)tvDirectoriesAndFiles.SelectedNode.Tag;
    			var sourceCode = new CecilDecompiler().getSourceCode(methodDefinition);
    			sourceCodeEditor.setDocumentContents(sourceCode,"aaa.cs");        		
    	 	}
    	}
    
    	public void loadAssembly(string assemblyToLoad)
    	{
    		log.debug("Loading assembly {0}", assemblyToLoad);
    		var assembly = CecilUtils.getAssembly(assemblyToLoad);
    		Assert.That(assembly != null, "could not load into an assembly the file: " + assemblyToLoad);
    		tvDirectoriesAndFiles.invokeOnThread(
    			()=> {
    					tvDirectoriesAndFiles.Nodes.Clear();
			    		foreach(var method in CecilUtils.getMethods(assembly))
			    		{			
			    			if (method.HasBody)
			    			{
			    				var filteredSignature = new FilteredSignature(method.ToString());					    			
			    				O2Forms.newTreeNode(tvDirectoriesAndFiles.Nodes, filteredSignature.sSignature, 0, method);	    			
			    			}
			    		}
    				 });
    		
    	}	    
    }
}