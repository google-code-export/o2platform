// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
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
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI; 

namespace O2.Script
{
    public class O2_DotNet_Ast_Scanner : Control
	{	 	
	 	
	 	public void start()
	 	{
	 		var astEngine = O2Gui.open<O2_DotNet_Ast_Scanner>("O2 .NET Ast Scanner", 600,400);
	 		astEngine.buildGui();
	 		
	 	}
	 	
		public void buildGui() 
		{		 
			var controls = this.add_1x1("actions", "Data", false, 40);		
			controls[0].add_Link("Step 1: Load source code files", 15, 2, null).append_Link("Step 2: select rules", null).enabled(false).append_Link("Step 3: view results", null).enabled(false).append_Link("Step 2: view report", null).enabled(false).append_Link("Step 2: publish report", null).enabled(false);
			
	
			return;
	
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
	
		}
	
	}
}
