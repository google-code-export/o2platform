using System.Collections.Generic;
using Skybound.Gecko;

namespace O2.External.Firefox.WebAutomation.WebObjects
{
    public class O2HtmlPage
    {
        public List<O2Form> lForms = new List<O2Form>();
        public List<O2Link> lLinks = new List<O2Link>();

        // only call this after the page is loaded
        public static O2HtmlPage getHtmlPageObjectFromWebBrowserObject(GeckoWebBrowser wbWebBrowser)
        {
            var hpHtmlPage = new O2HtmlPage();
            if (wbWebBrowser.Document != null)
            {
                foreach (GeckoElement link in wbWebBrowser.Document.Links)
                    hpHtmlPage.lLinks.Add(new O2Link(link));

                foreach (GeckoElement form in wbWebBrowser.Document.GetElementsByTagName("Form"))
                    hpHtmlPage.lForms.Add(new O2Form(form));

                //    lbLinks.Items.Add(hecLink.InnerHtml);

                // add O2JavaScript
            }
            return hpHtmlPage;
        }
    }
}