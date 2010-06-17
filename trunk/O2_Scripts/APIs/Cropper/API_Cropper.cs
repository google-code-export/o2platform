// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
using Fusion8.Cropper.Core;

//O2Ref:Cropper.exe

namespace O2.XRules.Database.APIs
{
    public class API_Cropper : Control			// make this a windows form so that we get the STA thread from it 
    {        	
        public ImageCapture CropperImageCapture { get; set;}
		public API_Cropper()
		{
			setup();			
		}  
		
		public API_Cropper setup()
		{
			var configuration = Configuration.Current;								
			CropperImageCapture = new ImageCapture();	
			return this;
		}		    	    	    	    	    
    }
    
    public static class API_Cropper_ExtensionMethods
    {
    	public static API_Cropper cropper<T>(this T control)
    		where T : Control
    	{
    		return (API_Cropper)control.invokeOnThread(()=> new API_Cropper());
    	}
    	
    
    	public static API_Cropper showConfig(this API_Cropper cropper)
    	{
    		cropper.invokeOnThread(()=> show.info(Configuration.Current));
    		return cropper;    		
    	}
    	
    	public static API_Cropper toClipboard(this API_Cropper cropper)
    	{    		
    		cropper.CropperImageCapture.ImageFormat = ImageCapture.ImageOutputs["Clipboard"];
    		return cropper;
    	}
    	
    	public static Bitmap capture(this API_Cropper cropper, int x, int y, int width, int height)
    	{
    		return (Bitmap)cropper.invokeOnThread(
    			()=>{
    					try	
    					{
    						
	    					"before toClipboard".info();
	    					cropper.toClipboard();
	    					"after toClipboard".info();
	    					cropper.CropperImageCapture.Capture(x,  y,  width,  height);			
							return cropper.fromClipboardGetImage();
						}
						catch(Exception ex)
						{
							ex.log("in API_Cropper capture");
							return null;
						}					
					});
    	}
    	
    	public static Bitmap captureDesktop(this API_Cropper cropper)
    	{
    		return (Bitmap)cropper.invokeOnThread(
    			()=>{
			    		cropper.toClipboard();
			    		cropper.CropperImageCapture.CaptureDesktop();
			    		return cropper.fromClipboardGetImage();
			    	});
    	}
    	
    	public static Bitmap capture<T>(this API_Cropper cropper, T control)
    		where T : Control
    	{
    		return (Bitmap)control.invokeOnThread(
    			()=>{
    					var location = control.PointToScreen(Point.Empty); 
    					return cropper.capture(location.X, location.Y, control.Width, control.Height);
    				});
    	}
    	
    	public static Bitmap bitmap<T>(this T control)
    		where T : Control
    	{
    		return control.capture<T>();
    	}
    	
    	public static Bitmap capture<T>(this T control)
    		where T : Control
    	{    		
    		return (Bitmap)control.invokeOnThread(
    			()=>{
    					var cropper = new API_Cropper();        					
    					return 	cropper.capture(control);
    				});
    	}
    	
    	public static Bitmap desktop<T>(this T control)
    		where T : Control
    	{    		
    		return (Bitmap)control.invokeOnThread(
    			()=>{
    					var cropper = new API_Cropper();        					
    					return 	cropper.captureDesktop();
    				});
    	}
    }
}
