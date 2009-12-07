// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.Views.ASCX;
using O2.Kernel.Interfaces.Views;
using O2.External.WinFormsUI.Forms;
using O2.External.SharpDevelop.Ascx;
//O2Tag_AddReferenceFile:nunit.framework.dll
using NUnit.Framework; 
using O2.Views.ASCX.classes;
//O2Tag_AddReferenceFile:C:\O2\O2 - All Active Projects\O2 - All Active Projects\_3rdPartyDlls\HTMLparserLibDotNet20.dll

// temp extra files to compile locally
//O2Tag_AddSourceFile:C:\O2\O2 - All Active Projects\O2 - All Active Projects\O2Core\O2_DotNetWrappers\Windows\Ascx_ExtensionMethods_2.cs
//O2Tag_AddSourceFile:_C:\O2\O2 - All Active Projects\SourceCode_3rdParty_Dlls\HtmlParser\HTMLparser\O2ExtraCode\Magestic12ToXml.cs
using HTMLparserLibDotNet20.O2ExtraCode;

using O2.Views.ASCX.CoreControls;
using O2.DotNetWrappers.Network;

namespace O2.Script
{	
	[TestFixture]
	public class testAscx
	{
		private static IO2Log log = PublicDI.log;
		static string svnBaseUrl = @"http://o2platform.googlecode.com/svn/trunk/SourceCode_3rdParty_Dlls/";
						
		[Test]
		public static void parseSvnCode()
		{			
			var svnMappedUrls = SvnApi.getSvnMappedUrls(svnBaseUrl);
			Assert.That(svnMappedUrls.Count() > 0, "svnMappedUrls.Count() was 0");
			log.info("There are {0} SvnMappedUrls", svnMappedUrls.Count());
			foreach(var svnMappedUrl in svnMappedUrls)
				log.debug(" {0}: {1}", (svnMappedUrl.IsFile) ? "File" : "Dir", svnMappedUrl.FullPath);
			//foreach (XElement anchorTag in nodes.OfType<XElement>().DescendantsAndSelf("a")) {
            //if (anchorTag.Attribute("href") == null)
            //    continue;

            //Console.WriteLine(anchorTag.Attribute("href").Value);
        //}
			
			//var parser = new Majestic12.HTMLparser(codeToParse);

			//return true;
		}
		
		[Test] 
    	public bool openAscxAsFloat()
    	{    		
    		var svnBrowser = (ascx_SvnBrowser)O2AscxGUI.openAscx(typeof(ascx_SvnBrowser),  O2DockState.Float, "Svn Browser");
    		Assert.That(svnBrowser!= null,"ascx_SvnBrowser was null");
    		svnBrowser.openSvnUrl(svnBaseUrl);
    		return true;
    	}
    	
    }
    
  	public class ascx_SvnBrowser : UserControl
	{
		private static IO2Log log = PublicDI.log;
		
		public SplitContainer splitControl;		
		public GroupBox leftGroupBox;
		public GroupBox rightGroupBox;
		public TreeView tvDirectoriesAndFiles;
		public ascx_SourceCodeEditor sourceCodeEditor;		
	
		public ascx_SvnBrowser()
        {
        	log.info("in ascx_SvnBrowser constructor");
            InitializeComponent();            
        }
        
        public void InitializeComponent()
        {
        	splitControl = this.addSplitContainer(
            						false, 		//setOrientationToHorizontal
            						true,		// setDockStyleoFill
            						true);		// setBorderStyleTo3D)
            leftGroupBox = splitControl.Panel1.addGroupBox("Directories and Files");
            rightGroupBox = splitControl.Panel2.addGroupBox("FileContents");
            this.Width = 500;
            this.Height = 500;
            tvDirectoriesAndFiles = leftGroupBox.addTreeView();
            tvDirectoriesAndFiles.ImageList = ImagesLists.withFolderAndFile();
            tvDirectoriesAndFiles.AfterSelect += tvCurrentFilters_AfterSelect;
            sourceCodeEditor = rightGroupBox.addSourceCodeEditor();                                    
        }
        
        private void tvCurrentFilters_AfterSelect(object sender, TreeViewEventArgs e)
        {
        	
        	if (tvDirectoriesAndFiles.SelectedNode != null)
        	{
        		var svnMappedUrl = (SvnMappedUrl)tvDirectoriesAndFiles.SelectedNode.Tag;
        		log.info("on after select for: {0}", svnMappedUrl.Text);
        		
        		if (svnMappedUrl.IsFile)
	        		sourceCodeEditor.setDocumentContents(svnMappedUrl.getFileContents());
    			else    		        		
        			openSvnUrl(svnMappedUrl.FullPath);
        		//log.info(svnMappedUrl.Text);
        		//log.debug();
        	}
        }
        
        public void openSvnUrl(string urlToOpen)
        {        	
        	tvDirectoriesAndFiles.clear();
        	var svnMappedUrls = SvnApi.getSvnMappedUrls(urlToOpen);
        	foreach(var svnMappedUrl in svnMappedUrls)
        	{
        		var newTreeNode = 
        			tvDirectoriesAndFiles.addNode(
        				svnMappedUrl.Text, 
        				(svnMappedUrl.IsFile) ? 1 : 0,
        				(svnMappedUrl.IsFile) ? Color.Blue : Color.Black,        				
        				(object)svnMappedUrl);
        		
        		/*if (svnMappedUrl.IsFile)   
        		
        			tvDirectoriesAndFiles.setTextColor(newTreeNode,Color.Blue);        			     			        		}
        		else        	
        			tvDirectoriesAndFiles.setTextColor(newTreeNode,Color.DarkOrange);        			     			
        			*/
        	}
        	//tvDirectoriesAndFiles.addNode(O2SvnApi.getHtmlCode(urlToOpen));
        	//sourceCodeEditor.setDocumentContents(SvnApi.getHtmlCode(urlToOpen));
			//tvDirectoriesAndFiles.Nodes.Add(urlToOpen);
        }                
	}

    public class SvnApi
    {
        public static string getHtmlCode(string urlToFetch)
        {
            var urlContents = Web.getUrlContents(urlToFetch);
            return urlContents;
        }

        public static List<SvnMappedUrl> getSvnMappedUrls(string urlToFetch)
        {
            var svnMappedUrls = new List<SvnMappedUrl>();
            var codeToParse = Web.getUrlContents(urlToFetch);
            Assert.That(codeToParse != "", "codeToParse was empty");

            //			var link = Majestic12ToXml.ConvertNodesToXml(new byte[]{});
            var nodes = Majestic12ToXml.ConvertNodesToXml(codeToParse);
            Assert.That(nodes.Count() > 0, " There were no nodes");
            foreach (var element in nodes.OfType<XElement>().Descendants("li").Descendants("a"))
            {
                //				log.info("element: {0}",element);
                //				log.debug("    href = {0} , name = {1}", element.Attribute("href"), element.Value);
                svnMappedUrls.Add(new SvnMappedUrl(urlToFetch, element.Attribute("href").Value, element.Value));
            }
            return svnMappedUrls;
        }
    }

    public class SvnMappedUrl
    {
        public string BasePath { get; set; }
        public string VirtualPath { get; set; }
        public string Text { get; set; }
        public string FullPath { get; set; }
        public bool IsFile { get; set; }

        public SvnMappedUrl(string basePath, string virtualPath, string text)
        {
            BasePath = basePath;
            VirtualPath = virtualPath;
            Text = text;
            if (basePath.Length > 0 && basePath[basePath.Length - 1] != '/' &&
                virtualPath.Length > 0 && virtualPath[0] != '/')
                FullPath = basePath + '/' + virtualPath;
            else
                FullPath = basePath + virtualPath;

            if (virtualPath.Length > 0)
                IsFile = virtualPath[virtualPath.Length - 1] != '/';
        }

        public string getFileContents()
        {
            if (IsFile)
                return Web.getUrlContents(FullPath);
            return "";
        }
    }
}
