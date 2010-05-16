// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.API.Visualization.Ascx;
//O2Ref:O2_API_Visualization.dll
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
//O2Ref:WindowsBase.dll
//O2Ref:System.Core.dll	
//O2Ref:WindowsFormsIntegration.dll

using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
//O2Ref:GraphSharp.Controls.dll 
using GraphSharp.Controls;
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll
using QuickGraph;
//O2Ref:WPFExtensions.dll
using WPFExtensions.Controls;
using System.Windows.Forms.Integration;

//O2Ref:O2SharpDevelop.dll
using O2.External.SharpDevelop.ExtensionMethods;

//O2Ref:O2_External_IE.dll
using O2.External.IE.ExtensionMethods;
using O2.External.IE.Wrapper;

//O2File:WPF_Threading_ExtensionMethods.cs
//O2File:WPF_Controls_ExtensionMethods.cs
//O2File:GraphSharp_ExtensionMethods.cs
//O2File:GraphLayout_WPF_ExtensionMethods.cs

namespace O2.API.Visualization.ExtensionMethods
{
    public static class WPF_WinFormIntegration_ExtensionMethods
    {   
    	#region generic 
    	
        public static ElementHost add_Wpf(this System.Windows.Forms.Control winFormsControl)
        {
            return winFormsControl.add_WPF_Host();
        }

        public static ElementHost add_WpfHost(this System.Windows.Forms.Control winFormsControl)
        {
            return winFormsControl.add_WPF_Host();
        }

        public static ElementHost add_WPF_Host(this System.Windows.Forms.Control winFormsControl)
        {
            var xamlHost = winFormsControl.add_Control<ascx_Xaml_Host>();
            return xamlHost.element();
        }

    	public static T add_WPF_Control<T>(this System.Windows.Forms.Control winFormsControl) where T : UIElement
    	{
    		var xamlHost = winFormsControl.add_Control<ascx_Xaml_Host>();
    		return (T)xamlHost.add_Control<T>();    		
    	}
    	
    	public static T add_Control<T>(this ascx_Xaml_Host xamlHost) where T : UIElement
    	{    		
    		return (T)xamlHost.invokeOnThread(
    			()=>{
    					try
			            {
	    					var wpfControl = typeof(T).ctor();					    				
	    					if (wpfControl is UIElement)
	    					{
	    						xamlHost.element().Child = (UIElement)wpfControl;
	    						return (T)wpfControl;			    								    					
	    					}
						}			
			            catch (Exception ex)
			            {
			                ex.log("in add_Control");
			            }
			            return null;			    				
					});
    	}
    	
    	public static ElementHost clear(this ElementHost elementHost)
    	{
    		return (ElementHost)elementHost.invokeOnThread(()=> elementHost.Child = null);
    	}
    	    
    	#endregion

		#region ElementHost
		
		public static T add_Control<T>(this ElementHost elementHost) where T : UIElement
    	{
    		return (T)elementHost.invokeOnThread(
    			()=>{
    					try
			            {
	    					var wpfControl = typeof(T).ctor();					    				
	    					if (wpfControl is UIElement)
	    					{
	    						elementHost.Child = (UIElement)wpfControl;
	    						return (T)wpfControl;			    								    					
	    					}
						}			
			            catch (System.Exception ex)
			            {
			                ex.log("in add_Control");
			            }
			            return null;			    				
    				});     		
    	}

		#endregion    	
		
    	#region ElementHost - graph and zoom        	 
    	
    	public static GraphLayout add_GraphWithZoom(this ElementHost elementHost)
		{
			var zoom = elementHost.add_Zoom();
			var graphLayout = zoom.set<GraphLayout>();
			graphLayout.newGraph();
			graphLayout.defaultLayout();
			return graphLayout;
		}
		
		public static GraphLayout add_Graph(this ElementHost elementHost)
    	{
    		var graphLayout = elementHost.add_Control<GraphLayout>();    		
    		graphLayout.background(Brushes.White);
    		graphLayout.newGraph();  			
			return graphLayout;
    	}
		    	
    	public static ZoomControl add_Zoom(this ElementHost elementHost)
    	{
    		return elementHost.add_Control<ZoomControl>();    		    		    					
    	}    	    	    	
    	    	
    	#endregion
    	
    	#region Add WPF Controls to ascx_Xml_Host or Controls

    	public static Label add_WPF_Label(this System.Windows.Forms.Control winFormsControl, string text)
    	{    		
    		var label = winFormsControl.add_WPF_Control<Label>();
    		label.set_Text(text);
    	
    		return label;
    	}    	
    	
    	public static GraphLayout add_Graph(this System.Windows.Forms.Control winFormsControl)
		{
		 	var xamlHost = winFormsControl.add_Control<ascx_Xaml_Host>();
		 	if (xamlHost == null)
		 		"xamlHost was null".error();
			return xamlHost.add_Graph();
		}
		
		public static GraphLayout add_Graph(this ascx_Xaml_Host xamlHost)
		{
			var zoom = xamlHost.element().add_Zoom();
            
			var graphLayout = zoom.set<GraphLayout>();						
			graphLayout.newGraph();
			graphLayout.defaultLayout();			
			return graphLayout;
		}
		
    	public static Label add_Label(this ascx_Xaml_Host xamlHost, string text)
    	{
    		var label = add_Control<Label>(xamlHost);
    		label.set_Text(text);
    		//"in add_Label".debug();
    		return label;
    	} 
    	#endregion
    	    	    	
    	#region WinForms to WPF host - generic

		public static T add_WinForm<T>(this GraphLayout graphLayout, int width, int height) where T : System.Windows.Forms.Control
		{
			return (T)graphLayout.wpfInvoke(
    			()=>{
    					var controlHost = graphLayout.add_UIElement<WindowsFormsHost>(); 
						controlHost.width(width);
						controlHost.height(height);
						var winFormsControl = (System.Windows.Forms.Control)typeof(T).ctor();	
						winFormsControl.Dock = System.Windows.Forms.DockStyle.Fill;
						controlHost.Child = winFormsControl;
						return winFormsControl;
					});
		}
    	
    	#endregion
    	
/*    	#region WinForms to WPF host - SharpDevelop
    	
    	public static ICSharpCode.TextEditor.TextEditorControl add_CodeEditor(this GraphLayout graphLayout, string fileToOpen , int width, int height)
    	{
    		return (ICSharpCode.TextEditor.TextEditorControl)graphLayout.wpfInvoke(
    			()=>{
    					var winControl = graphLayout.add<WindowsFormsHost>(); 
						winControl.width(width);
						winControl.height(height);
						var textEditor = new ICSharpCode.TextEditor.TextEditorControl();						
						textEditor.fill();
						winControl.Child = textEditor;
						textEditor.open(fileToOpen);
						return textEditor;
    				});    		
    	}
    	
    	#endregion
 * */
    	
    	
    	#region WinForms to WPF host - Web Browser
    		
		public static O2BrowserIE add_WebBrowser(this GraphLayout graphLayout)
		{
			return (O2BrowserIE)graphLayout.add_WinForm<O2BrowserIE>(800,400);
		}
		
		#endregion
    }
}
