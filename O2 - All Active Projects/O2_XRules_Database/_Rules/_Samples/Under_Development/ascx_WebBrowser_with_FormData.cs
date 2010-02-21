// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.Interfaces.O2Core;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.IE.ExtensionMethods;
using O2.External.IE.Interfaces;
//O2File:extra.cs

namespace O2.Script
{
    public class ascx_WebBrowser_with_FormData: UserControl
    {    
    	private static IO2Log log = PublicDI.log;		
		public static IO2Browser webBrowser;
		public static TabControl tabControl;

		public string currentPage;
		
        public static void startControl()
    	{       		
			typeof(ascx_WebBrowser_with_FormData).showAsForm("", 400, 400)			
			.invoke("openWebPage",@"http://localhost.:54275/HacmeBank_v2_Website/aspx/login.aspx");
    	}    	
    	
    	public ascx_WebBrowser_with_FormData()
    	{    		    	
    		buildGui();
        }
    
    	
        private void buildGui()
        {
        	var groupBoxes = this.add_1x1("Web Browser", "Form Data");
        	webBrowser = groupBoxes[0].add_WebBrowser();
        	tabControl = groupBoxes[1].add_TabControl();        	
        	
        	// events
        	webBrowser.onDocumentCompleted += pageLoaded;
     	}   
     	
     	public void openWebPage(string pageToOpen)
     	{
     		currentPage = pageToOpen;
     		openWebPage();
     	}
     	
     	public void openWebPage()
     	{
     		webBrowser.open(currentPage);
     	}
     	
     	public void pageLoaded(IO2HtmlPage htmlPage)
    	{
    		//if (currentPage == htmlPage.PageUrl.ToString())
    		//info_TextBox = tabControl.add_Tab("info").add_TextBox(true);
        	//forms_DataGridView = tabControl.add_Tab("forms").add_DataGridView();
    		tabControl.add_Tab_IfListHasData("aa",htmlPage.Links);
    		//info_TextBox.append_Line(htmlPage.PageUrl.ToString());
    		//forms_DataGridView.show(htmlPage.Links);
    		 //htmlPages_TreeView.add_Node(htmlPage);
    		 //if (htmlPages_TreeView.Nodes.Count == 1)
    		 //	htmlPages_TreeView.SelectedNode = htmlPages_TreeView.Nodes[0];
    	}
    	    	    	    	    
    }
}
