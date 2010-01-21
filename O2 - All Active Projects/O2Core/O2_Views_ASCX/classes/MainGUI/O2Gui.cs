using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace O2.Views.ASCX.classes.MainGUI
{
    public class O2Gui : Form
    {

        public O2Gui()
        {
            IsMdiContainer = true;
        }
        
        public O2Gui(int width, int height) :this()
        {
        	this.Width = width;
        	this.Height = height;
        }

        public void show()
        {
            ShowDialog();
        }

    }
}
