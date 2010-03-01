// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
//O2Tag_OnlyAddReferencedAssemblies
//O2Ref:System.dll
//O2Ref:System.Core.dll
//O2Ref:System.Windows.Forms.dll
//O2Ref:O2_Kernel.dll
//O2Ref:O2_Interfaces.dll
//O2Ref:O2_DotNetWrappers.dll
//O2Ref:O2_Views_Ascx.dll
//O2Ref:O2_External_IE.dll
//O2Ref:O2_External_SharpDevelop.dll
//O2Ref:O2SharpDevelop.dll
//O2Ref:System.Drawing.dll
//O2Ref:itextsharp.dll
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using iTextSharp;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.IE.ExtensionMethods;
using O2.External.IE.Interfaces;
using O2.External.IE.Wrapper;
using O2.DotNetWrappers.Windows;

namespace O2.Script
{
    public class ascx_Create_Links_From_Clipboard : Control
    {    
    	private static IO2Log log = PublicDI.log;
		public static RichTextBox textBox;
		public static IO2Browser webBrowser;
		
		public static void startControl()
		{
			O2Gui.open<ascx_Create_Links_From_Clipboard>("Create Links from Clipboard", 400,400);
		}
        public ascx_Create_Links_From_Clipboard()
        {
        	this.Width = 400;
        	this.Height = 400;
        	buildGui();	
        }
        
        public void buildGui()
        {
        	var controls = this.add_1x1("Text to convert","converted links",true,200);
        	textBox = controls[0].add_RichTextBox();
        	webBrowser = controls[1].add_WebBrowser();        	
        	// events
        	textBox.onDrop(convertFile);
        	textBox.TextChanged += (sender,e) => convertText(textBox.Text);
        }
        
        public void convertFile(string fileToConvert)
        {        	
        	O2Thread.mtaThread(
        		()=>{
			        	//fileToConvert.error();
			        	if (fileToConvert.fileExists())            
			                if (fileToConvert.extension(".pdf"))
			                {
			                    textBox.set_Text("...processing pdf file: " + fileToConvert);
			                    var pdfParser = new PDFParser();
			                    var tempFile = PublicDI.config.getTempFileInTempDirectory(".txt");                    
			                    pdfParser.ExtractText(fileToConvert, tempFile);
			                    textBox.set_Text(tempFile.contents());
			                    Files.deleteFile(tempFile);
			                }
			                else
			                    textBox.set_Text(fileToConvert.contents());        	
			        });
        }
        
        public void convertText(string textToConvert)
        {
	        O2Thread.mtaThread(
        		()=>{
			        	textToConvert.size().str().info();
			        	textToConvert = textToConvert.replace(" ","\"","'",Environment.NewLine); 
			        	var itemsToConvert = new List<String>();        	
			        	foreach(var textSnippet in textToConvert.split_onSpace())
			        		itemsToConvert.Add(textSnippet);
			        	
			        	var htmlCode = "";
			        	foreach(var item in itemsToConvert)
			        		if (item.validUri() && item.ToLower().starts("http"))
				        		htmlCode += "<a href=\"{0}\">{0}</a><br/>".format(item).line();
			        		
			        	((O2BrowserIE)webBrowser).DocumentText = htmlCode;
			        });
        }
    	    	    	    	    
    }
}
