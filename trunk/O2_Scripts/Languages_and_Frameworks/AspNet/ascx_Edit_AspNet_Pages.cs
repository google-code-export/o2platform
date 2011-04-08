// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.XRules.Database.Utils;
using O2.XRules.Database.APIs;

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs
//O2File:WatiN_IE_ExtensionMethods.cs

//O2Ref:WatiN.Core.1x.dll
//O2Ref:Interop.SHDocVw.dll

namespace O2.XRules.Database.Languages_and_Frameworks.AspNet
{

	public class ascx_Edit_AspNet_Pages_Test
	{
		public void test()
		{
			"test ascx_Edit_AspNet_Pages".showAsForm<ascx_Edit_AspNet_Pages>(1000,600)
										 .buildGui();
		}
	}
	
    public class ascx_Edit_AspNet_Pages : Control
    {   
    	public string File_Path { get; set;}
		public string Virtual_Path { get; set; }				
								
		//public string Server { get; set; }		
		public string Web_Address { get; set; }
			
		public Panel ActionsPanel { get; set; }
		public TreeView AspNetFiles { get; set; }
		public ascx_SourceCodeViewer Aspx_CodeViewer { get; set; }
		public ascx_SourceCodeEditor CSharp_CodeViewer { get; set; }
		
		public WatiN_IE ie { get; set; }
		
    	public ascx_Edit_AspNet_Pages()
    	{
    		this.Virtual_Path = "/";
    		/*this.width(1000);
    		this.height(600);
    		buildGui();*/
    	}
    	
    	public ascx_Edit_AspNet_Pages buildGui()
    	{
    		"step 1".info();
    		var topPanel = this.add_Panel();    		

			ActionsPanel = topPanel.insert_Above<Panel>(40).add_GroupBox("Actions").add_Panel() ;
			AspNetFiles = topPanel.insert_Left<Panel>().add_GroupBox("ASP.NET Files").add_TreeView();
			"step 2".info();
			Aspx_CodeViewer = topPanel.add_GroupBox("Aspx page").add_SourceCodeViewer();
			CSharp_CodeViewer = Aspx_CodeViewer.parent().insert_Below().add_GroupBox("CSharp page").add_SourceCodeEditor();
			"step 3".info();
			this.ie = topPanel.add_IE_SideView();
			"step 4".info();
			setPageViewers();
			return this;
    	}
    	
    	public ascx_Edit_AspNet_Pages setPageViewers()
    	{
    		AspNetFiles.afterSelect<string>(
				(file)=>{				
					Aspx_CodeViewer.open(file);
					var csharpFile = "{0}.cs".format(file);
					if (csharpFile.fileExists())
						CSharp_CodeViewer.open(csharpFile);
					else
					{					
						var folder = file.directoryName();
						var fileName = System.IO.Path.GetFileNameWithoutExtension(file);
						csharpFile = folder.pathCombine("App_Code").pathCombine("{0}.cs".format(fileName));
						if (csharpFile.fileExists())
							CSharp_CodeViewer.open(csharpFile); 
					}
					var pageUrl = new Uri(this.Web_Address.uri(), file.remove(this.File_Path));					
					this.ie.open_ASync(pageUrl.str()); 
					//var codeBehind = 
				});	
			return this;
    	}
	}
	
	public static class ascx_Edit_AspNet_Pages_ExtensionMethods
	{
		public static ascx_Edit_AspNet_Pages loadFiles_FromLocalFolder(this ascx_Edit_AspNet_Pages editAspNet, string folder)
		{
			editAspNet.File_Path = folder;
			
			var files = folder.files(true, "*.aspx", "*.asmx","*.ashx", "*.asax");			
			editAspNet.AspNetFiles.add_Nodes(files, (file)=> file.remove(folder));  
			return editAspNet;
		}
	}
	public static class IE_ExtensionMethods
	{
		public static WatiN_IE add_IE_SideView<T>(this T control)
			where T : System.Windows.Forms.Control
		{
			
			var internetView = control.insert_Right().add_GroupBox("Internet Explorer View");
			var addressBar = internetView.parent().insert_Above(20).add_TextBox("Current page:","");
			var htmlCode = internetView.parent().insert_Below(200).add_GroupBox("Html Code").add_SourceCodeEditor(); 			
			var ie = internetView.add_IE();			
			addressBar.onEnter((text)=> ie.open_ASync(text));
			ie.onNavigate(
				(url)=> {
							addressBar.set_Text(url);				
							htmlCode.set_Text(ie.html(), "a.html");
						});			
			return ie;
		}
		
	}
	
	
}
