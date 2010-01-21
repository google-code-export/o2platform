using System.Windows.Forms;

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
    }
}
