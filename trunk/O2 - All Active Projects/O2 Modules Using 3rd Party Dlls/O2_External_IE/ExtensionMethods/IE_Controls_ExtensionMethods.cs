using System;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.IE.Interfaces;
using O2.External.IE.WebObjects;
using O2.External.IE.Wrapper;
using O2.Kernel.ExtensionMethods;

namespace O2.External.IE.ExtensionMethods
{
    public static class IE_Controls_ExtensionMethods
    {
        public static IO2Browser add_WebBrowser(this Control control)
        {
            return (IO2Browser) control.invokeOnThread(
                                    () =>
                                        {
                                            var o2BrowserIE = new O2BrowserIE {Dock = DockStyle.Fill};
                                            control.Controls.Add(o2BrowserIE);                        
                                            return o2BrowserIE;
                                        });
        }
        
        public static IO2Browser add_WebBrowserWithLocationBar(this Control control)
        {
            return control.add_WebBrowserWithLocationBar("");
        }

        public static IO2Browser add_WebBrowserWithLocationBar(this Control control, string startUrl)
        {
            return control.add_WebBrowserWithLocationBar(startUrl,(webBrowser, url) => webBrowser.open(url));
        }

        public static IO2Browser add_WebBrowserWithLocationBar(this Control control, string startUrl, Action<IO2Browser,string> onEnter)
        {
            return control.add_WebBrowserWithLocationBar(startUrl,
                                                         (keys, webBrowser, url) =>
                                                             {
                                                                 if (keys == Keys.Enter)
                                                                     onEnter(webBrowser, url);
                                                             });
        }

        public static IO2Browser add_WebBrowserWithLocationBar(this Control control, string startUrl, Action<Keys, IO2Browser, string> onKeyUp)
        {
            return (IO2Browser)control.invokeOnThread(
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
                                           //textBox.Multiline = false;
                                           textBox.Dock = DockStyle.Fill;
                                           var webBrowser = splitControl.Panel2.add_WebBrowser();
                                           
                                           webBrowser.onDocumentCompleted +=
                                               htmlPage => textBox.set_Text(htmlPage.PageUrl.ToString());
                                           //textBox.TextChanged += (sender, e) => webBrowser.open(textBox.Text);
                                           textBox.KeyUp += (sender, e) => onKeyUp(e.KeyCode, webBrowser, textBox.Text);
                                           textBox.Text = startUrl;
                                           if (startUrl != "")
                                               webBrowser.open(startUrl);
                                           return webBrowser;
                                       });
        }

        public static IO2HtmlFormField formField(this IO2HtmlForm form, object data)
        {
            object name = data.prop("name");
            object type = data.prop("type");
            object value = data.prop("value");
            object enabled = data.prop("enabled");
            return new IE_Form_Field
                       {
                           Form = form,
                           Name = (name != null) ? name.ToString() : "",
                           Type = (type != null) ? type.ToString() : "",
                           Value = (value != null) ? value.ToString() : "",
                           Enabled = (enabled != null) ? bool.Parse(enabled.ToString()) : false
                       };
        }

        public static IO2HtmlFormField formField(this IO2HtmlForm form, string name, string type, string value, bool enabled)
        {
            return new IE_Form_Field
                       {
                           Form = form,
                           Name = name,
                           Type = type,
                           Value = value,
                           Enabled = enabled
                       };
        }

        public static string Html(this O2BrowserIE o2BrowserIE)
        {
            return o2BrowserIE.DocumentText;
        }

        public static void onEditedHtmlChange(this O2BrowserIE o2BrowserIE, Action<string> onHtmlChange)
        {
            if (o2BrowserIE.Document != null)
            {
                var markupContainer2 = (mshtml.IMarkupContainer2) o2BrowserIE.Document.DomDocument;

                uint pdwCookie;
                markupContainer2.RegisterForDirtyRange(
                    new IEChangeSink(() => onHtmlChange(o2BrowserIE.Html())), out pdwCookie);
            }
        }
    }
}