using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.External.IE.Interfaces;
using O2.DotNetWrappers.Windows;

namespace O2.External.IE
{
    public static class IE_ExtensionMethods
    {
        public static IO2Browser_ add_WebBrowser_(this Control control)
        {
            return (IO2Browser_) control.invokeOnThread(
                () =>
                    {
                        var o2BrowserIE = new O2BrowserIE_ {Dock = DockStyle.Fill};
                        control.Controls.Add(o2BrowserIE);                        
                        return o2BrowserIE;
                    });
        }
        
        public static IO2Browser_ add_WebBrowserWithLocationBar_(this Control control, string startUrl)
        {
            return (IO2Browser_) control.invokeOnThread(
                () =>
                    {
                    	var splitControl = control.add_SplitContainer(
        						true, 		//setOrientationToHorizontal
        						true,		// setDockStyleoFill
        						false);		// setBorderStyleTo3D                        
                        splitControl.FixedPanel = FixedPanel.Panel1;
                        splitControl.Panel1MinSize = 20;
        				splitControl.SplitterDistance = 20; 
        				control.Controls.Add(splitControl);	
						var textBox = splitControl.Panel1.add_TextBox();
                        textBox.Multiline = false;
                        var webBrowser = splitControl.Panel2.add_WebBrowser_();

                        //textBox.TextChanged += (sender, e) => webBrowser.open(textBox.Text);
                        textBox.KeyUp += 
                            (sender, e) =>
                                {
                                    if (e.KeyCode == Keys.Enter)
                                        webBrowser.open(textBox.Text);
                                };
                        textBox.Text = startUrl;
                        webBrowser.open(startUrl);
                        return webBrowser;
                    });
        }
    }
}
