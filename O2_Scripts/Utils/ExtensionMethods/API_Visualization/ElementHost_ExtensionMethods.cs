using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
//O2Ref:WindowsFormsIntegration.dll
//O2File:WPF_ExtensionMethods.cs

namespace O2.API.Visualization.ExtensionMethods
{
    public static class ElementHost_ExtensioMethods
    {
        /*public static Control getHost(this string xamlFileName)
        {
            if (xamlFileName.fileExists())
            { }
            return null;
        }*/
        #region ElementHost add		
        
		public static T add_Control_Wpf<T>(this ElementHost elementHost) where T : UIElement
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
				
		    	

		#region ElementHost - WPF Controls
		
        public static ListView add_ListView_Wpf(this ElementHost elementHost)
        {
            return elementHost.add_Control_Wpf<ListView>();
        }
        
        public static Label add_Label_Wpf(this ElementHost elementHost, string text)
        {
            return elementHost.add_Control_Wpf<Label>().set_Text_Wpf(text);
        }
        #endregion

    }
}
