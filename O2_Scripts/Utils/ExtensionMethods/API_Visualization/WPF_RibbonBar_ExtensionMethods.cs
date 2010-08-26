// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Windows;
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
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Kernel.ExtensionMethods;
using O2.API.Visualization.ExtensionMethods;
using Odyssey.Controls;

//O2Ref:Odyssey.dll

//O2File:WPF_Controls_ExtensionMethods.cs
//O2File:WPF_WinFormIntegration_ExtensionMethods.cs
//O2File:ElementHost_ExtensionMethods.cs
//O2File:XamlCode_ExtensionMethods.cs

//STILL POC
namespace O2.XRules.Database.Utils
{

	public class WPF_Ribbon_Test
	{
		public void launchGui()
		{
			var wpfRibbon = O2Gui.open<WPF_Ribbon>("Test - O2 WPF Ribbon",600,400);			
			wpfRibbon.buildGui();					
		}
	}
	
	
	public class WPF_Ribbon : System.Windows.Forms.Control
	{
		public WPF_Ribbon buildGui() 
		{
			return (WPF_Ribbon)this.invokeOnThread(
    			()=>{
    					var wpfHost = this.add_WpfHost();
    					var grid = wpfHost.add_Grid_Wpf();
		    			grid.backColor(System.Windows.Media.Brushes.White);				
						var rowDefinition = new RowDefinition(); 
		    			rowDefinition.Height = GridLength.Auto;
		    			grid.RowDefinitions.Add(rowDefinition);
		    			
    					//var dockPanel = wpfHost.add_Control_Wpf<Grid>(); ;//wpfHost.add_Control_Wpf<DockPanel>();
						//var tabItem1 = dockPanel.add_Control_Wpf<RibbonTabItem>();  
						
						//var ribonGroup1 = dockPanel.add_Control_Wpf<RibbonGroup>();
						//return null;
						 
						//var tabItem2 = grid.add_Control_Wpf<RibbonTabItem>();  
						//show.info(tabItem);
						//tabItem2.Title = "2nd tab";
						//tabItem2.backColor(System.Windows.Media.Brushes.White);				
						var tabItem1 = grid.add_Control_Wpf<RibbonTabItem>();  
						//show.info(tabItem);
						//tabItem1.height_Wpf(200);
						tabItem1.Title = "1st tab";
						//tabItem1.height_Wpf(
						var ribonGroup1 = tabItem1.add_Control_Wpf<RibbonGroup>();
	    		//ribonGroup1.width_Wpf(100);
	    		ribonGroup1.Title =  "ribbon Toggle Button";  
	    		var ribbonButton = new RibbonToggleButton(); 
	    		ribbonButton.Content = "Search Button"; 
	    		ribbonButton.LargeImage = "search32.png".local().image_Wpf().Source;
	    		//ribbonButton.width_Wpf(150);
	    		ribonGroup1.Controls.Add(ribbonButton);  
	    		
	    		ribonGroup1.Controls.Add(new RibbonSeparator());  
						//wpfHost.add_Control_Wpf<RibbonBar>(); 
    					return this;
    				});
		}
	}
	
    public static class WPF_RibbonBar_ExtensionMethods
    {
    	public static RibbonBar add_RibbonBar<T>(this T control)
    		where T : System.Windows.Forms.Control
    	{    	        		
    	
    		var wpfHost = control.add_WpfHost();
    		//return (RibbonBar)
    		//var grid = wpfHost.add_Grid_Wpf();
    		//grid.wpfInvoke(    		
    		//wpfHost.invokeOnThread(
    		O2Thread.staThread(
			()=>{   			
					
					var form = new System.Windows.Forms.Form();
					
					var grid = new Grid();
					grid.backColor(System.Windows.Media.Brushes.White);				
					var rowDefinition = new RowDefinition(); 
		    		rowDefinition.Height = GridLength.Auto;
		    		grid.RowDefinitions.Add(rowDefinition);
		    		var ribbonBar = new RibbonBar();
		    		grid.Children.Add(ribbonBar);
		    		var aa = form.add_WpfHost();
		    		aa.Child = grid;
		    		"Here 9 ....".error();
		    		form.ShowDialog();
					
					//wpfHost.Child = grid;
					//wpfHost.add_Control_Wpf(grid);
					//return null;
		    		//var grid = wpfHost.add_Grid_Wpf();
		    		/*grid.backColor(System.Windows.Media.Brushes.White);				
					var rowDefinition = new RowDefinition(); 
		    		rowDefinition.Height = GridLength.Auto;
		    		grid.RowDefinitions.Add(rowDefinition);
		    		"here 7".info();
		    		var hashTable = (Hashtable)"PresentationFramework".assembly().type("SystemResources").fieldValue("_resourceCache");  
					hashTable.Clear();

		    		var ribbonBar = grid.add_Control_Wpf<RibbonBar>();*/
		    		//return ribbonBar;		    		
		    		//return null;
		    	});
		    return null;
    	}
    }
}	
    	