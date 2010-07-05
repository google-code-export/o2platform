// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
using O2.Views.ASCX.classes.MainGUI;
using O2.External.IE.ExtensionMethods;
using O2.External.IE.Wrapper;
using System.Windows;
using System.Windows.Controls;
using O2.API.Visualization.ExtensionMethods;
using Odyssey.Controls;

//O2File:WPF_Controls_ExtensionMethods.cs
//O2File:WPF_WinFormIntegration_ExtensionMethods.cs
//O2File:ElementHost_ExtensionMethods.cs
//O2File:XamlCode_ExtensionMethods.cs
//O2File:O2PlatformWikiAPI.cs

//O2Ref:Odyssey.dll
//O2Ref:O2_API_AST.dll
//O2Ref:O2_API_Visualization.dll
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
//O2Ref:WindowsBase.dll   
//O2Ref:System.Core.dll
//O2Ref:WindowsFormsIntegration.dll
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll 
//O2Ref:GraphSharp.Controls.dll
//O2Ref:ICSharpCode.AvalonEdit.dll
//O2Ref:O2_External_IE.dll

namespace O2.XRules.Database.APIs
{
    public class WPF_GUI : WinForms.Control
    {        	
    	public OutlookBar GUI_OutlookBar { get; set; }
		public WinForms.Panel WinFormPanel { get; set; }
		public O2BrowserIE O2Browser { get; set; }
		//public List<WPF_GUI_Section> Sections { get; set;}
		public O2PlatformWikiAPI Wiki_O2 { get; set; }
		
    	public static void testGui()
    	{    	 
    		var wpfGui = O2Gui.open<WPF_GUI>("Test - O2 WPF Gui");			
			wpfGui.buildGui();			
			wpfGui.add_Section("Main", "This is the intro text. Put here an explanation of what this module is all about");				
			wpfGui.add_Section("Section 1", "Text that describes Section 1");
			wpfGui.add_Section("Section 2");
			wpfGui.add_Section("Section 3");
			wpfGui.add_Section("Section 4");
			wpfGui.add_Section("Section 5");
			wpfGui.add_Section("Section 6");
			wpfGui.add_Section("Section 7");			
			
			
			

			
    	}
    
    	public WPF_GUI()
    	{
    		this.Width = 640;    		
    		this.Height = 320;    		
    		//buildGui();
    		//Sections= new List<WPF_GUI_Section>();
    		Wiki_O2 = new O2PlatformWikiAPI();
    	}
    	
    	/*public WPF_GUI buildGui(List<WPF_GUI_Section> sections)
    	{
    		Sections = sections;
    		return buildGui();
    	}*/
    	public WPF_GUI buildGui()    	
    	{
    		return (WPF_GUI)this.invokeOnThread(
    			()=>{
			    		
			    		this.backColor(Color.White);
			    		var wpfHost = this.add_WpfHost();
			    		
			    		var dockPanel = wpfHost.add_Control_Wpf<DockPanel>();
						GUI_OutlookBar =  dockPanel.add_Control_Wpf<OutlookBar>(); 
						GUI_OutlookBar.IsButtonSplitterVisible=false; 
						GUI_OutlookBar.IsOverflowVisible=true;
						GUI_OutlookBar.IsPopupVisible=true;
						GUI_OutlookBar.ShowSideButtons=true;
						GUI_OutlookBar.ShowButtons=true;			
						GUI_OutlookBar.Width = 300; 
						GUI_OutlookBar.MaxWidth = 300; 
						GUI_OutlookBar.ButtonHeight = 21;
						
						var userControl = dockPanel.add_WinForms_Panel()
												   .add_Control<WinForms.UserControl>();
						var statusLabel = userControl.add_StatusStrip(Color.White);									   
						WinFormPanel = userControl.add_Panel();						
						
						O2Browser  = WinFormPanel.add_Browser();
    					O2Browser.silent(true);
    					
						//WinFormPanel.backColor(Color.White);			
						/**add_Sections();
						if (Sections.size() > 0 && Sections[0].WinFormsControl.notNull())
							WinFormPanel.add_Control(Sections[0].WinFormsControl);*/
						return this;
					});
    	}
				
    	
    	public WPF_GUI_Section add_Section(string name)
    	{
    		return add_Section(name, "");
    	}
    	public WPF_GUI_Section add_Section(string name, string introText)
    	{
    		return add_Section(new WPF_GUI_Section(name,introText));
    	}
    	
    	public WPF_GUI_Section add_Section(WPF_GUI_Section section)
    	{
    		section.Wpf_Gui = this;
			return (WPF_GUI_Section)this.invokeOnThread(
    			()=>{
    					var outlookSection = new  OutlookSection();
    					section.SectionInGui = outlookSection;
						outlookSection.Header = section.Name;
						var stackPanel = outlookSection.add_StackPanel();						
						if (section.IntroText.valid())
						{
							var textBlock = stackPanel.add_TextBlock();
							textBlock.set_Text_Wpf(section.IntroText);
						}
						section.ContentPanel = stackPanel.add_WrapPanel();						
						
						
						if (section.WinFormsCtor.notNull())
						{
							section.WinFormsControl = section.WinFormsCtor();
						}						
						
						outlookSection.Click+=
							(sender,e)=>{
											if (section.WinFormsControl.notNull())
											{
												WinFormPanel.clear();
												WinFormPanel.add_Control(section.WinFormsControl);
											}
										};
						//wrapPanel.add_Control_Wpf<Button>();
						
						
						
						
						//outlookSection.Visibility = Visibility.Hidden;
						//outlookSection
						
						GUI_OutlookBar.Sections.Add(outlookSection);												
						
						return section;
					});
		}    	    	    	    	
    }
    
    public static class WPF_GUI_ExtensionMethods_Show
    {
    
    	public static WPF_GUI show_Url(this WPF_GUI wpf_Gui, string url)
    	{
    		wpf_Gui.WinFormPanel.clear();    		
			wpf_Gui.WinFormPanel.add_Control(wpf_Gui.O2Browser);
    		wpf_Gui.O2Browser.open(url);
    		return wpf_Gui;
    	}
    	
    	public static WPF_GUI show_YouTubeVideo(this WPF_GUI wpf_Gui,string youTubeVideoId)
    	{
			var code = 	"<objec ><param name=\"movie\" "+
						"value=\"http://www.youtube.com/v/1bbKNvyvLO0&amp;hl=en_GB&amp;fs=1\"></param><param name=\"allowFullScreen\" "+
						"value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed "+
						"src=\"http://www.youtube.com/v/1bbKNvyvLO0&amp;hl=en_GB&amp;fs=1\" type=\"application/x-shockwave-flash\" "+
						"allowscriptaccess=\"always\" allowfullscreen=\"true\" width=\"480\" height=\"385\"></embed></object>";
			wpf_Gui.O2Browser.set_Text(code);
			return wpf_Gui;
    	}    	    	
    	
    	public static WPF_GUI show_O2Wiki(this WPF_GUI wpf_Gui,string wikiPageToShow)
    	{
    		wpf_Gui.O2Browser.set_Text(wpf_Gui.Wiki_O2.html(wikiPageToShow));//"O2_Videos_on_YouTube"));    		
    		return wpf_Gui;
    	}
    }
    
    public class WPF_GUI_Section
    {
    	public WPF_GUI Wpf_Gui { get; set;}
    	public WinForms.Control WinFormsControl { get; set;}
    	public Func<WinForms.Control> WinFormsCtor { get; set;}
    	public string Name { get; set;}
    	public string IntroText { get; set;}
    	public WrapPanel ContentPanel { get; set;}
    	public OutlookSection SectionInGui { get; set;}
    	    	
    	public WPF_GUI_Section (string name, string introText)
    	{
    		Name = name;    	 	
    	 	IntroText = introText;
    	}
    	
    }
    
    
    public static class WPF_GUI_Section_ExtensionMethods
    {
    	public static WPF_GUI_Section add_Section(this List<WPF_GUI_Section> sections, string name)
    	{
    		return sections.add_Section(name,"");
    	}
   
    	public static WPF_GUI_Section add_Section(this List<WPF_GUI_Section> sections, string name, string introText)
    	{
			var newSection = new WPF_GUI_Section(name, introText);
			sections.Add(newSection);
			return newSection;
    	}
		
		public static WPF_GUI_Section add_Link(this WPF_GUI_Section section, string linkText, Action onCLick)
		{			
			return (WPF_GUI_Section)section.SectionInGui.wpfInvoke(
				()=>{
						section.ContentPanel.add_Xaml_Link(linkText,onCLick);
						return section;
					});								
		}
		
		public static WPF_GUI_Section add_Link_Web(this WPF_GUI_Section section, string linkText, string linkUrl)
		{			
			return section.add_Link(linkText, ()=> section.Wpf_Gui.show_Url(linkUrl));						
		}
		
		public static WPF_GUI_Section add_Link_YouTube(this WPF_GUI_Section section, string linkText, string youTubeVideoId)
		{			
			return section.add_Link(linkText, ()=> section.Wpf_Gui.show_YouTubeVideo(youTubeVideoId));						
		}
		
		public static WPF_GUI_Section add_Link_O2Wiki(this WPF_GUI_Section section, string linkText, string wikiPageToShow)
		{			
			return section.add_Link(linkText, ()=> section.Wpf_Gui.show_O2Wiki(wikiPageToShow));						
		}
				
		/*		public static WPF_GUI_Section add_WebLink(this WPF_GUI_Section section, string linkText, string linkUrl)
		{			
			return (WPF_GUI_Section)section.SectionInGui.wpfInvoke(
				()=>{
						section.ContentPanel.add_Xaml_Link(linkText,()=> section.Wpf_Gui.showUrl(linkUrl));
						return section;
					});								
		}
		
		public static 
		

		*/
    }
}
