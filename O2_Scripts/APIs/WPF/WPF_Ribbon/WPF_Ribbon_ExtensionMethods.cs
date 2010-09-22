// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows;
using WinForms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Threading;
using O2.Kernel;
using O2.DotNetWrappers.H2Scripts;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Kernel.ExtensionMethods;
using O2.API.Visualization.ExtensionMethods;
using Microsoft.Windows.Controls.Ribbon;
using O2.XRules.Database.APIs;
using O2.External.SharpDevelop.ExtensionMethods;

//O2File:WPF_Ribbon.cs
//O2File:Scripts_ExtensionMethods.cs

namespace O2.XRules.Database.Utils
{	
	
    public static class WPF_Ribbon_ExtensionMethods
    {
    	public static Ribbon add_Wpf_Ribbon<T>(this T control)
    		where T : System.Windows.Forms.Control
    	{    	        		
    		return (Ribbon)control.invokeOnThread(
    			()=>{    		
			    		var wpfHost = control.add_WPF_Host();
			    		var grid = wpfHost.add_Grid_Wpf();
							
						grid.backColor(System.Windows.Media.Brushes.White);				
						var rowDefinition = new RowDefinition();  
						rowDefinition.Height = GridLength.Auto;
						grid.RowDefinitions.Add(rowDefinition);
									
						var ribbon = grid.add_Control_Wpf<Ribbon>(); 
						ribbon.ShowQuickAccessToolBarOnTop = false;  // this is not being used
						return ribbon;
					});
    	}
    	
    	public static RibbonTab add_RibbonTab(this Ribbon ribbon, string title)
    	{
    		var ribbonTab = ribbon.add_Control_Wpf<RibbonTab>(); 
			ribbonTab.prop("Header", title);
			return ribbonTab;
    	}
    	
    	public static RibbonGroup add_RibbonGroup(this RibbonTab ribbonTab, string title)
    	{
			var ribbonGroup = ribbonTab.add_Control_Wpf<RibbonGroup>(); 
			ribbonGroup.prop("Header", title);
			return ribbonGroup;
    	}
    	
    	
		// this needs the extra dropdown stuff (or is just like the add_RibbonButton)
		/*public static RibbonSplitButton add_RibbonSplitButton(this RibbonGroup ribbonGroup, string imageFile, bool smallImage,  Action onClick)
		{
			return (RibbonSplitButton)ribbonGroup.wpfInvoke(
    			()=>{
						var splitButton = ribbonGroup.add_Control_Wpf<RibbonSplitButton>();												
						splitButton.Label = "Asd";
						if (imageFile.valid())	
						{
							if (imageFile.fileExists().isFalse())
								imageFile = imageFile.local();
							if(smallImage)
								splitButton.SmallImageSource = imageFile.image_Wpf().Source;
							else
								splitButton.LargeImageSource = imageFile.image_Wpf().Source;
						}
						return splitButton;
					});
		}*/
    	
    	public static T add_RibbonButton<T>(this T frameworkElement, string label)
    		where T : FrameworkElement
    	{
    		return frameworkElement.add_RibbonButton(label, null,false, ()=>{});
    	}
    	public static T add_RibbonButton<T>(this T frameworkElement, string label, Action onClick)
    		where T : FrameworkElement
    	{
    		return frameworkElement.add_RibbonButton(label, null,false, onClick);
    	}
    	    
    	public static T add_RibbonButton<T>(this T frameworkElement, string label, string imageFile, bool smallImage,  Action onClick)
    		where T : FrameworkElement
    	{
    		return (T)frameworkElement.wpfInvoke(
    			()=>{
					var ribbonButton = frameworkElement.add_Control_Wpf<RibbonButton>(); 
					ribbonButton.Label = label;
					ribbonButton.onClick_Wpf(onClick);//()=> " I was clicked".info()); 
					if (imageFile.valid())	
					{
						if (imageFile.fileExists().isFalse())
							imageFile = imageFile.local();
						if(smallImage)
							ribbonButton.SmallImageSource =  imageFile.image_Wpf().Source;
						else
							ribbonButton.LargeImageSource =  imageFile.image_Wpf().Source;
					}					
					return frameworkElement;
				});						
    	}
    	
		public static RibbonGroup add_RibbonButton_H2Script(this RibbonGroup ribbonGroup, string label, string h2Script)
		{
			if (h2Script.fileExists().isFalse())
				h2Script = h2Script.local();
			return ribbonGroup.add_RibbonButton(label, ()=>h2Script.executeH2Script());
		}
		
		public static RibbonGroup add_RibbonButton_Script(this RibbonGroup ribbonGroup, string label, string script)
		{
			if (script.fileExists().isFalse())
				script = script.local();
			return ribbonGroup.add_RibbonButton(label, ()=> script.executeFirstMethod());				
		}
		
		public static RibbonGroup add_RibbonButton_H2Script(this RibbonGroup ribbonGroup, WinForms.Control winFormsControl, string label, string h2Script)
		{
			if (h2Script.extension(".h2"))
			{
				if (h2Script.fileExists().isFalse())
					h2Script = h2Script.local();
				var h2 = H2.load(h2Script);
				var scriptText = h2.notNull() ? h2.SourceCode : "";
				return ribbonGroup.add_RibbonButton_Script_GUI(winFormsControl,label, scriptText);
			}
			return ribbonGroup.add_RibbonButton_Script(winFormsControl,label, h2Script);
		}
		
		public static RibbonGroup add_RibbonButton_Script(this RibbonGroup ribbonGroup, WinForms.Control winFormsControl, string label, string script)
		{
			if (script.fileExists().isFalse())
				script = script.local();
			return ribbonGroup.add_RibbonButton_Script_GUI(winFormsControl, label, script.fileContents());
		}
		
		public static RibbonGroup add_RibbonButton_Script_GUI(this RibbonGroup ribbonGroup, WinForms.Control winFormsControl, string label, string sourceCode)
		{
			return ribbonGroup.add_RibbonButton(label,  
				()=>{
						winFormsControl.clear();
						var scriptHost = winFormsControl.add_Script(false ); 						
						
						scriptHost.onCompilationOk = 
							()=>{
									"Compiled OK, moving mouse to click".info(); 
									var inputSimulator = new API_InputSimulator();	
									inputSimulator.Move_SkipValue= 3;			// makes it faster 
									//double currentX = inputSimulator.mouse_CurrentPosition().X;
    								//double currentY = inputSimulator.mouse_CurrentPosition().Y;    		
									inputSimulator.mouse_MoveTo(scriptHost.executeButton).mouse_Click(); 									
									//inputSimulator.mouse_MoveTo(currentX, currentY);
									scriptHost.onCompilationOk = null;
							 	};
												 
						scriptHost.set_Command(sourceCode); 
						
					});
		}
		public static RibbonGroup add_RibbonButton_StartProcess(this RibbonGroup ribbonGroup , string label, string process)
		{
			return ribbonGroup.add_RibbonButton_StartProcess(label,process, "");
		}
		public static RibbonGroup add_RibbonButton_StartProcess(this RibbonGroup ribbonGroup , string label, string process, string arguments)
		{
			return ribbonGroup.add_RibbonButton(label, ()=> Processes.startProcess(process, arguments));
		}
		
		public static RibbonGroup add_RibbonButton_ShowCodeFile(this RibbonGroup ribbonGroup, WinForms.Control winFormsControl, string label, string pathToFile)
		{
			return ribbonGroup.add_RibbonButton(label,  
				()=>{
						if (pathToFile.fileExists().isFalse())
							pathToFile = pathToFile.local();
						winFormsControl.clear();
						winFormsControl.add_SourceCodeViewer().open(pathToFile);
					});
		}
		
		public static RibbonGroup add_RibbonButton_Web(this RibbonGroup ribbonGroup, WinForms.Control winFormsControl, string label, string url)
		{
			return ribbonGroup.add_RibbonButton(label,  
				()=>{						
						winFormsControl.clear();
						"Opening url: {0}".info(url);
						winFormsControl.add_Control<WinForms.WebBrowser>().open(url);;						
					});
		}
		
		

		
//var button1 = ribbonGroup2.add_Control_Wpf<RibbonButton>();
//	button1.Label = "Button1";
	
//	button1.SmallImageSource = "cut16.png".local().image_Wpf().Source;
    	
    }
    
    public static class WPF_Ribbon_ExtensionMethods_API_InputSimulator
    {
    	public static API_InputSimulator click(this API_InputSimulator inputSimulator, RibbonGroup ribbonGroup, int itemToClick)
    	{
    		if (ribbonGroup.notNull() && ribbonGroup.Items.Count > itemToClick)
    		{
				var uiElement = (UIElement)ribbonGroup.Items[itemToClick];
				inputSimulator.mouse_MoveTo_Wpf((uiElement)).click();
			}
			return inputSimulator;
    	}
    }
    
    
}	
    	