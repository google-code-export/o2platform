// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;

namespace O2.Legacy.CoreLib.Ascx.DataViewers
{
    public partial class ascx_WebBrowser : UserControl
    {
        public ascx_WebBrowser()
        {
            InitializeComponent();
        }

        public ascx_WebBrowser(String sUrl)
        {
            InitializeComponent();
            wbWebBrowser.Navigate(sUrl);
            makeWebBrowserOnlyControlVisible();
        }

        private void btNavigate_Click(object sender, EventArgs e)
        {
            wbWebBrowser.Navigate(tbUrl.Text);
        }

        public void makeWebBrowserOnlyControlVisible()
        {
            tbUrl.Visible = false;
            btNavigate.Visible = false;
            ascx_DropObject1.Visible = false;
            wbWebBrowser.Dock = DockStyle.Fill;
        }

        public void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oVar)
        {
            wbWebBrowser.Navigate("about:black");
            String sDataReceived = oVar.ToString();
            if (File.Exists(sDataReceived))
                if (Path.GetExtension(sDataReceived) == ".xml")
                    wbWebBrowser.Navigate(sDataReceived);
                else
                    wbWebBrowser.DocumentText = Files.getFileContents(sDataReceived);
        }

        public void navigate(String sPageToOpen)
        {
            wbWebBrowser.Navigate(sPageToOpen);
        }
    }
}
