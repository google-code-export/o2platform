using System;
using System.Windows.Forms;
using O2.Kernel;

using O2.DotNetWrappers.DotNet;

namespace O2.Views.ASCX.classes.MainGUI
{
    public static class WinForms_ExtensionMethods
    {
		public static MenuStrip add_Menu(this Form form)
		{
			var menuStrip = new MenuStrip();
			form.Controls.Add(menuStrip);            
            form.MainMenuStrip  = menuStrip;  
            return menuStrip;
		}
		
		public static ToolStripMenuItem add_MenuItem(this MenuStrip menuStrip, string text)
		{
			var fileMenuItem = new ToolStripMenuItem {Text = text};
		    menuStrip.Items.Add(fileMenuItem);
            return fileMenuItem;            
		}
		
		public static ToolStripMenuItem add_MenuItem(this ToolStripMenuItem menuItem,  string text)
		{
		    var clildMenuItem = new ToolStripMenuItem {Text = text};
		    menuItem.DropDownItems.Add(clildMenuItem);
            return clildMenuItem;
		}
		
		public static Control add_Ascx(this O2Gui o2Gui, Type controlType)
		{
			return (Control)(o2Gui.invokeOnThread(
				()=> {
						var control = (Control)PublicDI.reflection.createObjectUsingDefaultConstructor(controlType);
						if (control != null)
						{
							control.Dock = DockStyle.Fill;
							var hostForm = new Form();
							hostForm.Controls.Add(control);
							hostForm.MdiParent = o2Gui;
							hostForm.WindowState=FormWindowState.Maximized;
							hostForm.Show();							
							return control;
						}
						return null;
					 }));
		}
		
		public static void add_Ascx(this O2Gui o2Gui, Control control)
		{
			o2Gui.Controls.Add(control);
		}
    }
}
