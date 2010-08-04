// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
 
namespace O2.XRules.Database.Utils
{
    public class test_ascx_SimpleFileSearch : ContainerControl
    {       		
		public static void launchGui()
		{
			var simpleSearch = O2Gui.open<ascx_SimpleFileSearch>("Util - Simple File Search", 500,400);			
			var localScriptsFolder = PublicDI.config.LocalScriptsFolder;
			var filesToShow = localScriptsFolder.files("*.cs",true);
			simpleSearch.loadFiles(localScriptsFolder, filesToShow); 
		}
	}

    public class ascx_SimpleFileSearch : ContainerControl
    {       		
		public TextBox Path { get;set; }

		public Panel leftPanel;
		public ascx_SourceCodeEditor sourceCode;
		
        public ascx_SimpleFileSearch()
    	{
    		this.Width = 300;
    		this.Height = 300;
    		buildGui();
    	}
 
    	public void buildGui()
    	{
    		var topPanel = this.add_Panel();
    		Path = topPanel.insert_Above<TextBox>(20);
			sourceCode = topPanel.add_SourceCodeEditor();
			
			leftPanel = topPanel.insert_Left<Panel>(300);									
			
			Path.onEnter(loadFiles);
			Path.onDrop(
				(fileOrFolder)=>{
									Path.set_Text(fileOrFolder);
									loadFiles(fileOrFolder);
								}); 	   	   	   	   
		}
		
		public void loadFiles(string filesPath)
		{			
			if (filesPath.dirExists())
				loadFiles(filesPath, filesPath.files(true));
		}
		
		public void loadFiles(string filesPath, List<string> filesToLoad)
		{
			Path.set_Text(filesPath);
			var filesContent = new Dictionary<string,string>();
			foreach(var file in filesToLoad) 
				if (file.isBinaryFormat().isFalse()) 
					filesContent.add(file.remove(filesPath),file.contents());
			leftPanel.clear();
			var treeView = leftPanel.add_TreeViewWithFilter(filesContent); 
			treeView.afterSelect<string>(
				(fileContents)=>{
									sourceCode.open(filesPath + treeView.selected().get_Text());
								});						
			sourceCode.colorCodeForExtension(treeView.selected().str());
		}		
	}
 
}
