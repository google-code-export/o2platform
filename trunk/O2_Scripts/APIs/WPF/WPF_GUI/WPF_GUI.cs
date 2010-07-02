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
using System.Windows.Controls;
using O2.API.Visualization.ExtensionMethods;
using Odyssey.Controls;


//O2Ref:C:\_Downloads\odyssey-24878\Odyssey\Demos\bin\Debug\Odyssey.dll
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

namespace O2.XRules.Database.APIs
{
    public class WPF_GUI : WinForms.Control
    {        	
    	public OutlookBar GUI_OutlookBar { get; set; }
		public WinForms.Panel WinFormPanel { get; set; }
		
    	public static void launchGui()
    	{
    	
    		var wpfGui = O2Gui.open<WPF_GUI>("O2 WPF Gui");
    		
    		wpfGui.add_OutlookSection("Main", "This is an intro text");
    		wpfGui.add_OutlookSection("BlackBox Security Tools");
    		wpfGui.add_OutlookSection("WhiteBox Security Tools");
    		wpfGui.add_OutlookSection("APIs");
    		wpfGui.add_OutlookSection("OWASP Projects");
    		wpfGui.add_OutlookSection("Help");
    	}
    
    	public WPF_GUI()
    	{
    		this.Width = 600;
    		this.Height = 320;
    		buildGui();
    	}
    	
    	public WPF_GUI buildGui()
    	{
    		//var topPanel = this.add_1x1("","",true,300);
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
			
			var userControl = dockPanel.add_WinForms_Panel()
									   .add_Control<WinForms.UserControl>();
			var statusLabel = userControl.add_StatusStrip(Color.White);									   
			WinFormPanel = userControl.add_Panel();
			WinFormPanel.backColor(Color.White);			
			return this;
    	}
    	    	
    	
    	public OutlookSection add_OutlookSection(string name)
    	{
    		return add_OutlookSection(name,"");
    	}
    	public OutlookSection add_OutlookSection(string name, string introText)
    	{
    		return (OutlookSection)this.invokeOnThread(
    			()=>{
    					var outlookSection = new  OutlookSection();
						outlookSection.Header = name;
						var stackPanel = outlookSection.add_StackPanel();
						stackPanel.add_Control_Wpf<Button>();
						//if (introText.valid());
						
						
						
						GUI_OutlookBar.Sections.Add(outlookSection);
						return outlookSection;
					});
    	}
    	    	    	    	    
    }
}
