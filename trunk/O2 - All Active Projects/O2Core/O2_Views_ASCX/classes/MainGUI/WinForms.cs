using System;
using System.Threading;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;

namespace O2.Views.ASCX.classes.MainGUI
{
    public class WinForms
    {
        public static Control showAscxInForm(Type controlType)
    	{
    		return showAscxInForm(controlType, controlType.Name);
    	}

        public static Control showAscxInForm(Type controlType, string formTitle)
    	{
    		return showAscxInForm(controlType, formTitle, -1, -1);
    	}
    	
        public static Control showAscxInForm(Type controlType, string formTitle, int width, int height)
        {
        	var controlCreation = new AutoResetEvent(false);
        	Control control = null;
            O2Thread.staThread(
                ()=> {
                         control = (Control)PublicDI.reflection.createObjectUsingDefaultConstructor(controlType);
                         if (control != null)
                         {
                             control.Dock = DockStyle.Fill;
                             var o2Gui = new O2Gui(width, height,false)
                                             {
                                                 Text = formTitle
                                             };
                             o2Gui.Controls.Add(control);
                             o2Gui.Load += (sender,e) => controlCreation.Set();	
                             o2Gui.showDialog(false);
                         }
                         else
                         	controlCreation.Set();
                });
            controlCreation.WaitOne();
            return control;
        }
    }
}