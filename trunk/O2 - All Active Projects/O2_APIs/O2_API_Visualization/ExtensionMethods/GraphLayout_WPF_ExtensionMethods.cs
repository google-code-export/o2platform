// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
//O2Ref:GraphSharp.Controls.dll 
using GraphSharp.Controls;
//O2Ref:GraphSharp.dll
//O2Ref:QuickGraph.dll
using QuickGraph;
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

//O2File:WPF_Controls_ExtensionMethods.cs
//O2File:GraphSharp_ExtensionMethods.cs
//O2File:WPF_Threading_ExtensionMethods.cs
//O2File:GraphFactory.cs

namespace O2.Script
{
    public static class GraphLayout_WPF_ExtensionMethods
    {   
    
    	// generic one
    	public static T add<T>(this GraphLayout graphLayout) where T : UIElement
		{
			var uiElement = graphLayout.newInThread<T>();			
			graphLayout.add(uiElement);
			return uiElement;
		}
    	
    	#region TextBox
    	
    	public static TextBox add_TextBox(this GraphLayout graphLayout)
    	{
    		return graphLayout.add_TextBox("");
    	}
		public static TextBox add_TextBox(this GraphLayout graphLayout, string textValue)
		{
			var textBox = graphLayout.newInThread<TextBox>();
			textBox.set_Text(textValue);
			graphLayout.add(textBox);
			return textBox;
		}				
		
		#endregion
		
		#region Button
		
		public static Button add_Button(this GraphLayout graphLayout, string text, int width, int height)
		{
			var button  = graphLayout.add<Button>();
			button.set(text);
			button.width(width);
			button.height(height);
			return button;
		}
		
		#endregion
		
		#region Image
		
		public static Image add_Image(this GraphLayout graphLayout, string uri)
		{
			return graphLayout.add_Image(new Uri(uri) , -1 , -1);
		}
		
		public static Image add_Image(this GraphLayout graphLayout, string uri, int width, int height)
		{
			return graphLayout.add_Image(new Uri(uri),width, height);
		}
		
		public static Image add_Image(this GraphLayout graphLayout, Uri uri, int width, int height)
		{
			return (Image)graphLayout.wpfInvoke(
				()=>{
						var image = graphLayout.add<Image>();
						var bitmap = new BitmapImage(uri);
						image.Source = bitmap; 
						if (width > -1)
							image.width(width);
						if (height > -1)
							image.height(height);
						return image;
					});
		}
		
		#endregion
	
		
    	    	 
    }
}
