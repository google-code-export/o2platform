// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX;
using WindowsInput.Native;
using WindowsInput;
//O2Ref:O2_Misc_Microsoft_MPL_Libs.dll
//O2Ref:O2_API_Visualization.dll

namespace O2.XRules.Database.APIs
{
    public class API_InputSimulator
    {    
    	public InputSimulator Input_Simulator { get; set; }
    	public double ScreenWidth  { get; set;}
    	public double ScreenHeight  { get; set;}
    	public double XDelta { get; set; }
    	public double YDelta { get; set; }
    	public bool DebugMode { get; set; }
    	public int Move_SleepValue { get; set; }    
    	public int Move_SkipValue { get; set; }    
    	
    	public API_InputSimulator()
    	{
    		Input_Simulator = new InputSimulator(); 
    		ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
			ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
			
			XDelta =  (double)65535 / (double)ScreenWidth;  
			YDelta =  (double)65535 / (double)ScreenHeight;// -3;   	
			Move_SleepValue = 1;
			Move_SkipValue = 3;
			DebugMode = false;
    	}
    		 	
    	    	    	    	    
    }
    
	public static class API_InputSimulator_ExtensionMethods
	{
		public static API_InputSimulator wait(this API_InputSimulator inputSimulator, int sleepValue)
  		{
  			inputSimulator.sleep(sleepValue);
  			return inputSimulator;
  		}
	}
	
  	public static class API_InputSimulator_ExtensionMethods_Keyboard
  	{
  		public static API_InputSimulator text_Send(this API_InputSimulator inputSimulator, string text)
  		{
  			inputSimulator.Input_Simulator.Keyboard.TextEntry(text);
  			return inputSimulator;
  		}
  	}

    public static class API_InputSimulator_ExtensionMethods_Mouse
    {    	
    	public static Point mouse_CurrentPosition(this API_InputSimulator inputSimulator)
    	{
    		return Cursor.Position;
    	}    	
    
    	public static API_InputSimulator mouse_SetPosition(this API_InputSimulator inputSimulator, double x, double y)
    	{
			return inputSimulator.mouse_SetPosition(x,y, true);
    	}
    	
    	public static API_InputSimulator mouse_SetPosition(this API_InputSimulator inputSimulator, double x, double y, bool sleepAfterSetPosition)
    	{
    		var xPos = x * inputSimulator.XDelta; 
			var yPos = y * inputSimulator.YDelta;
			inputSimulator.DebugMode.ifInfo("Setting mouse location to: {0}/{1} :: {2}/{3}".format(x,y, xPos,yPos));
			inputSimulator.Input_Simulator.Mouse.MoveMouseToPositionOnVirtualDesktop(xPos, yPos);
			if (sleepAfterSetPosition)
				inputSimulator.sleep(inputSimulator.Move_SleepValue, false);
			Cursor.Show();
			Application.DoEvents();
			return inputSimulator;
    	}
    	
    	public static API_InputSimulator mouse_MoveBy(this API_InputSimulator inputSimulator, double x, double y)
    	{
    		return inputSimulator.mouse_MoveBy(x,y,true);
    	}
    	
    	public static API_InputSimulator mouse_MoveBy(this API_InputSimulator inputSimulator, double x, double y, bool animate)
    	{
    		if (x != 0 || y != 0)
    		{
    			"moving mouse by:{0} {1}".info(x,y);
    			double currentX = inputSimulator.mouse_CurrentPosition().X;
    			double currentY = inputSimulator.mouse_CurrentPosition().Y;    		
    			double numberOfSteps = (Math.Abs(x) > Math.Abs(y)) ? Math.Abs(x) : Math.Abs(y);
    			double xStep = ((x != 0) ? x / numberOfSteps : 0) * inputSimulator.Move_SkipValue;
    			double yStep = ((y != 0) ? y / numberOfSteps : 0) * inputSimulator.Move_SkipValue;
    			for(int i =0 ; i < numberOfSteps ; i+=inputSimulator.Move_SkipValue)  
    			{
    				currentX += xStep; //(x >0) ? -xStep : -xStep;
    				currentY += yStep; //(y >0) ? -yStep : -yStep;
    				inputSimulator.mouse_SetPosition(currentX, currentY,animate);
    			}
    		}
    		return inputSimulator;    		
    	}
    	
    	public static API_InputSimulator mouse_MoveTo<T>(this API_InputSimulator inputSimulator, T control)
    		where T : Control
    	{
    		return inputSimulator.mouse_MoveTo(control, true);
    	}
    	
    	public static API_InputSimulator mouse_MoveTo<T>(this API_InputSimulator inputSimulator, T control, bool animate)
    		where T : Control
    	{
    		return (API_InputSimulator)control.invokeOnThread(
    			()=>{
	    				var location1 = control.PointToScreen(Point.Empty); 
						var xPos = (double)location1.X + control.width()/2;
						var yPos = (double)location1.Y  + control.height()/2;
						//return inputSimulator;
						return inputSimulator.mouse_MoveTo(xPos, yPos, animate);
    				});    		
    	
    	}
    	
    	public static API_InputSimulator mouse_MoveTo_Wpf<T>(this API_InputSimulator inputSimulator, T uiElement)
    		where T : System.Windows.UIElement
    	{
    		return (API_InputSimulator)O2.API.Visualization.ExtensionMethods.WPF_Threading_ExtensionMethods.wpfInvoke(
    			uiElement, 
    				()=>{
    						var point = uiElement.PointToScreen(new System.Windows.Point(0, 0)); 
    						return inputSimulator.mouse_MoveTo_Wpf(point);
    					});
    	}
    	
    	public static API_InputSimulator mouse_MoveTo_Wpf(this API_InputSimulator inputSimulator, System.Windows.Point point)
    	{
    		return inputSimulator.mouse_MoveTo(point.X+10, point.Y+2);    		
    	}			
			
			
    	public static API_InputSimulator mouse_MoveTo(this API_InputSimulator inputSimulator, Point point)
    	{
    		return inputSimulator.mouse_MoveTo(point, true);
    	}
    	
    	public static API_InputSimulator mouse_MoveTo(this API_InputSimulator inputSimulator, Point point, bool animate)
    	{
    		return inputSimulator.mouse_MoveTo(point.X, point.Y, animate);
    	}
    	
    	public static API_InputSimulator mouse_MoveTo(this API_InputSimulator inputSimulator, double x, double y)
    	{
    		return inputSimulator.mouse_MoveTo(x,y, true);
    	}
    	
    	public static API_InputSimulator mouse_MoveTo(this API_InputSimulator inputSimulator, double x, double y, bool animate)
    	{
    		var currentPosition = inputSimulator.mouse_CurrentPosition();
    		inputSimulator.mouse_MoveBy(x - currentPosition.X, y - currentPosition.Y, animate);    		
			return inputSimulator;
    	}
    	
    	public static API_InputSimulator click(this API_InputSimulator inputSimulator)
    	{
    		return inputSimulator.mouse_Click();
    	}
    	
    	public static API_InputSimulator mouse_LeftDown(this API_InputSimulator inputSimulator)
    	{
    		inputSimulator.Input_Simulator.Mouse.LeftButtonDown();			
    		return inputSimulator;
    	}
    	
    	public static API_InputSimulator mouse_LeftUp(this API_InputSimulator inputSimulator)
    	{
    		inputSimulator.Input_Simulator.Mouse.LeftButtonUp();			
    		return inputSimulator;
    	}
    	
    	public static API_InputSimulator mouse_Click(this API_InputSimulator inputSimulator)
    	{    		
    		inputSimulator.Input_Simulator.Mouse.LeftButtonDown();			
    		inputSimulator.sleep(300);
			inputSimulator.Input_Simulator.Mouse.LeftButtonUp();
//			inputSimulator.sleep(500);	
			return inputSimulator;    	
    	}
    	
    	public static API_InputSimulator mouse_RightClick(this API_InputSimulator inputSimulator)
    	{
    		inputSimulator.Input_Simulator.Mouse.RightButtonUp();			
    		//inputSimulator.Input_Simulator.Mouse.LeftButtonDown();			
    		//inputSimulator.sleep(300);
			//inputSimulator.Input_Simulator.Mouse.LeftButtonUp();
//			inputSimulator.sleep(500);	
			return inputSimulator;
    	}    	
    	
    
    }
        
    
    
    	
    	/*
    	var x = (double)0;
var y = (double)0;
var interval = 50;
for(int i=0 ; i < interval ; i ++) 
{	
	x += xDelta * (screenWidth / (double)interval); 
	y += yDelta * (screenHeight / (double)interval);
	"X: {0}   Y: {1}".debug(x,y);
	sim.Mouse.MoveMouseToPositionOnVirtualDesktop(x, y);
	this.sleep(50);	
}
	
return xDelta;
*/    
}
