using System;
using System.Threading;
using System.Windows.Forms;
using FluentSharp.O2.DotNetWrappers.DotNet;
using FluentSharp.O2.Kernel;
using FluentSharp.O2.Kernel.ExtensionMethods;

namespace FluentSharp.O2.Views.ASCX.classes.MainGUI
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
                    O2Gui o2Gui = null;
                    try
                    {
                        control = (Control)PublicDI.reflection.createObjectUsingDefaultConstructor(controlType);
                        if (control != null)
                        {
                            control.Dock = DockStyle.Fill;
                            o2Gui = new O2Gui(width, height, false)         // I might need to adjust these width, height so that the control is the one with this size (and not the hosting form)
                                            {
                                                Text = formTitle
                                            };
                            if (width > -1)
                                control.Width = width;
                            else
                                o2Gui.Width = control.Width + 10;            // if it is not defined resize the form to fit the control

                            if (height > -1)
                                control.Height = height;
                            else
                                o2Gui.Height = control.Height + 20;          // if it is not defined resize the form to fit the control
                            // note: need to double check if the correct values to add to the form (above) are 10 and 20                            
                            o2Gui.Controls.Add(control);
                            o2Gui.Load += (sender, e) => controlCreation.Set();
                            //o2Gui.showDialog(false);
                            o2Gui.showDialog(false);
                        }
                        else
                            controlCreation.Set();
                    }
                    catch (Exception ex)
                    {
                        "in showAscxInForm: {0}".format(ex).error();
                        controlCreation.Set();
                    }
                    finally
                    {
                        if (o2Gui != null)
                            o2Gui.Dispose();
                    }
                });
            controlCreation.WaitOne();
            return control;
        }
    }
}