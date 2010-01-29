using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;

namespace O2.Views.ASCX.classes.MainGUI
{
    public class O2Gui : Form
    {
		public AutoResetEvent formClosed = new AutoResetEvent(false);
        public AutoResetEvent formLoaded = new AutoResetEvent(false);
        
        public O2Gui() : this(-1,-1, false)
        {            	
        }

        public O2Gui(bool isMdiContainer) : this(-1,-1, isMdiContainer)
        {            
        }


        public O2Gui(int width, int height) : this(width, height, false)
        {
        }

        public O2Gui(int width, int height, bool isMdiContainer)
        {
            if (width > -1)
        	    Width = width;
            if (height > -1)
        	    Height = height;

            IsMdiContainer = isMdiContainer;

            Closed += (sender, e) => formClosed.Set();
            Load += (sender, e) => formLoaded.Set();
            
        }
        		
        public void showDialog()
        {
            showDialog(true);
        }
        public void showDialog(bool useNewStaThread)
        {
            if (useNewStaThread)
        	    O2Thread.staThread(()=>ShowDialog());
            else
                ShowDialog();
            formLoaded.WaitOne();
        }

        public void show()
        {           
            O2Thread.staThread(Show);
            formLoaded.WaitOne();
        }

        public void waitForFormClose()
        {
            formClosed.WaitOne();
        }        
        
        public void waitForFormLoad()
        {
        	formLoaded.WaitOne();
        }

    }
}
