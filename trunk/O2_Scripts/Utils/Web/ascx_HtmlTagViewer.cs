// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Linq;
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
using O2.External.SharpDevelop.Ascx;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.XRules.Database.Utils.ExtensionMethods;

//O2File:HtmlAgilityPack_ExtensionMethods.cs


namespace O2.XRules.Database.Utils
{
    public class ascx_HtmlTagViewer : Control
    {    
    
    	public TreeView HtmlTags_TreeView; 
    	public TextBox HtmlNodeFilter; 
    	public ascx_SourceCodeViewer HtmlCodeViewer;
    	public TextBox PageToOpen;
    	
    	public string HtmlCode { get; set; }
    	public bool ViewAsXml { get; set; }
    	public static string defaultPage = "http://www.google.com";
	
		public static List<String> sampleXPathQueries 
						= new List<string> {
												"//a",  
												"//img",
												"//a[contains(@href,'news')]",
												"//a[contains(text(),'S')]",
												"//a[text()='Blogs']"
											};
    	public static void launchGui()
    	{
    		O2Gui.open<ascx_HtmlTagViewer>("Control - Html Tag Viewer", 1000,400)
    			 .buildGui(true, true)
    			 .show(defaultPage.uri());
    	}
    	
		public ascx_HtmlTagViewer()
		{
			HtmlCode = "";			
		}
		public ascx_HtmlTagViewer buildGui()
		{
			return buildGui(false,false);
		}
		
		public ascx_HtmlTagViewer buildGui(bool addLoadUrlTextBox, bool addHtmlCodeViewer)
		{
			//return (ascx_HtmlTagViewer)this.invokeOnThread(
			//	()=>{
			var TopPanel = this.add_Panel();
			HtmlTags_TreeView =  TopPanel.add_TreeView()
									 .showSelection();			
			
			if (addHtmlCodeViewer)
			{
				HtmlCodeViewer = HtmlTags_TreeView.insert_Left<Panel>(TopPanel.width()/2).add_SourceCodeViewer(); 
				HtmlTags_TreeView.afterSelect<HtmlAgilityPack.HtmlNode>(
				  	(htmlNode)=>{ 
				  					HtmlCodeViewer.showHtmlNodeLocation(htmlNode);
				  					HtmlCodeViewer.editor().caret(htmlNode.Line, htmlNode.LinePosition);
				  				});
				var optionsPanel = HtmlCodeViewer.insert_Below<Panel>(30);
				optionsPanel.add_CheckBox("View as Xml",0,5, 
					(value)=>{
								ViewAsXml = value;
								reloadPage();
							 });
			}
			
			if (addLoadUrlTextBox)	
			{
				PageToOpen = TopPanel.insert_Above<Panel>(20).add_TextBox().fill();
				var propertyGrid = HtmlTags_TreeView.insert_Right<Panel>(150).add_PropertyGrid();
				
				
				HtmlTags_TreeView.afterSelect<HtmlAgilityPack.HtmlNode>(
				  (htmlNode)=> propertyGrid.show(htmlNode));
				
				PageToOpen.onEnter((text)=> show(text.uri()));								
			}
			
			HtmlNodeFilter = HtmlTags_TreeView.insert_Below<TextBox>(25).fill();
			var sampleQueries_MenuItem = HtmlNodeFilter.add_ContextMenu().add_MenuItem("Sample queries");
			
			var treeView_ContextMenu = HtmlTags_TreeView.add_ContextMenu();
			
			treeView_ContextMenu.add_MenuItem("Sort Nodes", ()=> HtmlTags_TreeView.sort());
			treeView_ContextMenu.add_MenuItem("Don't Sort Nodes", ()=> HtmlTags_TreeView.sort(false));
			treeView_ContextMenu.add_MenuItem("Show all nodes",()=> HtmlNodeFilter.sendKeys("//*".line()));   
			foreach(var xPathQuery in sampleXPathQueries)
				sampleQueries_MenuItem.add_MenuItem(xPathQuery , (text) => HtmlNodeFilter.set_Text(text.str()));
			
			HtmlTags_TreeView.beforeExpand<HtmlAgilityPack.HtmlNode>(
					(treeNode, htmlNode)=>{																
											  if (htmlNode.Attributes != null)
											  	  foreach(var attribute in htmlNode.Attributes)
											 	  	  treeNode.add_Node("a: {0}={1}".format(attribute.Name, attribute.Value)); 
											  treeNode.add_Node("v: {0}".format(htmlNode.InnerHtml));  	
											  if (htmlNode.ChildNodes != null)
											  	  foreach(var childNode in htmlNode.ChildNodes)
												  	  if (childNode.html().valid()) 
													  	  treeNode.add_Node("n: {0}".format(childNode.Name), childNode, true);  
										  });			
			HtmlNodeFilter.onEnter(
					(text)=>{
								show(HtmlCode, text);
							});										
			
			
			return this;
		//});
		}
		
		public ascx_HtmlTagViewer reloadPage()
		{
			var url = PageToOpen.get_Text();
			if (url.isUri())
				show(url.uri().getHtml());
			return this;
		}
		public ascx_HtmlTagViewer show(Uri uri)
		{	
			if (PageToOpen.notNull())
				PageToOpen.set_Text(uri.str());
			return show(uri.getHtml());
		}
		
		public ascx_HtmlTagViewer show(string htmlCode)
		{			
			HtmlCode = htmlCode;
			if (ViewAsXml)
				HtmlCode = htmlCode.htmlToXml();
				
			if (HtmlCodeViewer.notNull())
			{
				if (ViewAsXml)
					HtmlCodeViewer.set_Text(HtmlCode,".xml");									
				else
					HtmlCodeViewer.set_Text(HtmlCode,".xml"); 
			}
			return show(HtmlCode,HtmlNodeFilter.get_Text());
		}
		
		public ascx_HtmlTagViewer show(string htmlCode, string filter)
		{		
			HtmlTags_TreeView.clear();
			try
			{
				">showing htmlcode with size: {0}".info(htmlCode.size());
				HtmlNodeFilter.backColor(Color.White);
				var htmlDocument = htmlCode.htmlDocument();  	
				if (filter.valid())
					HtmlTags_TreeView.add_Nodes(htmlDocument.select(filter));
				else
				{
					HtmlTags_TreeView.add_Node(htmlDocument);
					HtmlTags_TreeView.expand();
				}
				"HtmlTags_TreeView nodes: {0}".info(HtmlTags_TreeView.nodes().size());
				
			}
			catch(System.Exception ex)
			{
				ex.log("in htmlNodeFilter.onEnter");
				HtmlNodeFilter.backColor(Color.Red);
			}			
			return this;
		}
		
		
    }
}
