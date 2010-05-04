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
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.IE.ExtensionMethods;
namespace O2.Script
{
    public class MediaWikiEditor
    {    
    	private static IO2Log log = PublicDI.log;

		public static void run()
		{
			
			var wikiApi = new O2PlatformWikiAPI(); 
			new MediaWikiEditor().buildGui(wikiApi, @"C:\O2\_USERDATA\O2PlatformWiki.xml");
		}		        

        public void buildGui(O2MediaWikiAPI wikiApi, string fileWithloginDetails)
        {
        	var panel = O2Gui.open<Panel>("MediaWiki Editor", 600,400);
			var loginDetails = fileWithloginDetails.xRoot();			
			var username = loginDetails.element("Credentials").element("UserName").value();
			var password = loginDetails.element("Credentials").element("Password").value();
			wikiApi.login(username,password); 
			 
			var controls = panel.add_1x2("WikiText","Preview","Current Content",true,500,100); 
			var wikiTextEditor = controls[0].add_RichTextBox();
			var browserPreview = controls[1].add_Browser();
			var browserCurrent = controls[2].add_Browser();  
			
			wikiTextEditor.onKeyPress((key)=> 
				{
					if (key == (Keys.Control | Keys.V))		
						if (Clipboard.ContainsImage())
						{ 
							"Image in Clipboard".debug();
							O2Thread.mtaThread( 
								()=>{
										var imageUrl = wikiApi.uploadImage_FromClipboard();
										var wikiTextForImage = wikiApi.getWikiTextForImage(imageUrl);					
										wikiTextEditor.insertText(wikiTextForImage); 
									});
							return true;
						}		 			 
					return false;
				}); 
				
			var contextMenu = wikiTextEditor.add_ContextMenu();
			
			contextMenu.add_MenuItem("Past Image from Clipboard", 
				()=>{
						var imageUrl = wikiApi.uploadImage_FromClipboard();
						var wikiTextForImage = wikiApi.getWikiTextForImage(imageUrl);					
						wikiTextEditor.insertText("[[Image:{0}]]".format(wikiTextForImage)); 
						//RichTextBox aaa = wikiTextEditor.insertText("[[image:asdas]]"); 			   
						//var currentRange = wikiTextEditor.SelectedText
						//wikiTextEditor.insert_Above "selected item".info();
					});
			browserPreview.silent(true);
			browserCurrent.silent(true);
			
			var textBox = wikiTextEditor.insert_Above<TextBox>(50);
			var bottomPanel = textBox.insert_Below<Panel>(30);  
			 
			//default values
			textBox.set_Text("Test");
			
			// events
			 
			var image = panel.fromClipboardGetImage();
			var button = bottomPanel.add_Button("Paste Image",0, 350);
			
			button.BackgroundImage = image;
			button.BackgroundImageLayout = ImageLayout.Stretch;
			
			bottomPanel.add_Button("Load",0).onClick(
				()=>{
						O2Thread.mtaThread(()=>
						{			
							var pageToEdit =  textBox.get_Text();	
							var wikiText = wikiApi.wikiText(pageToEdit);
							//wikiTextEditor.set_Text(wikiApi.wikiText(pageToEdit));
							wikiTextEditor.set_Text(wikiText);
							browserPreview.set_Text(wikiApi.parseText(wikiText,true));
							browserCurrent.set_Text(wikiApi.html(pageToEdit));	  
						});
					}).click();
			
			
			bottomPanel.add_Button("Preview",0,100).onClick(
				()=>{
						O2Thread.mtaThread( 
								()=>{
										browserPreview.set_Text(wikiApi.parseText(wikiTextEditor.get_Text(),true));
									});
					});
					
			bottomPanel.add_Button("Save",0,200).onClick(
				()=>{
						var currentPage =  textBox.get_Text();
						// save content
						wikiApi.save(currentPage, wikiTextEditor.get_Text()); 
						 
						// reload content and show it
						var wikiText = wikiApi.wikiText(currentPage);					
						browserPreview.set_Text(wikiApi.parseText(wikiText,true));
						browserCurrent.set_Text(wikiApi.html(currentPage));	  			
					}); 
			
			wikiTextEditor.onKeyPress(Keys.Enter, 
				(code)=>{
							O2Thread.mtaThread(()=>
							{			
								browserPreview.set_Text(wikiApi.parseText(code,true));
							});
						});
			        }
        
        
    	    	    	    	    
    }
}
