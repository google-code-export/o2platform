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
using O2.External.SharpDevelop.Ascx;
using O2.Views.ASCX.classes;
using HTMLparserLibDotNet20.O2ExtraCode;
using O2.Views.ASCX.CoreControls;
using O2.Core.XRules.Classes;

namespace O2.Core.XRules.Ascx
{	    
  	public class ascx_SvnBrowser : UserControl
	{
		private static IO2Log log = PublicDI.log;
		
		public SplitContainer splitControl;		
		public GroupBox leftGroupBox;
		public GroupBox rightGroupBox;
		public TreeView tvDirectoriesAndFiles;
		public ascx_SourceCodeEditor sourceCodeEditor;

        static string svnBaseUrl = @"http://o2platform.googlecode.com/svn/trunk/";

		public ascx_SvnBrowser()
        {
        	log.info("in ascx_SvnBrowser constructor");
            InitializeComponent();
            openSvnUrl(svnBaseUrl);
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
            tvDirectoriesAndFiles.NodeMouseDoubleClick += tvCurrentFilters_DoubleClick;
            sourceCodeEditor = rightGroupBox.addSourceCodeEditor();                                    
        }

        private void tvCurrentFilters_DoubleClick(object sender, EventArgs e)
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
}
