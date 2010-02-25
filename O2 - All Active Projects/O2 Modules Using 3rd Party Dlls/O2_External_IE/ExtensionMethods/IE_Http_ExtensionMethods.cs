using System.Collections.Generic;
using O2.External.IE.Interfaces;

namespace O2.External.IE.ExtensionMethods
{
    public static class IE_Http_ExtensionMethods
    {
        public static IO2HtmlPage GET(this IO2Browser webBrowser, string url)
        {
            return webBrowser.submitRequest_GET_Sync(url, "", null);
        }

        public static IO2HtmlPage GET(this IO2Browser webBrowser, string url, Dictionary<string, string> parameters)
        {
            return webBrowser.submitRequest_GET_Sync(url, "", parameters);
        }

        public static IO2HtmlPage GET(this IO2Browser webBrowser, string url, string targetFrame, Dictionary<string, string> parameters)
        {
            return webBrowser.submitRequest_GET_Sync(url, targetFrame, parameters);
        }

        public static IO2HtmlPage POST(this IO2Browser webBrowser, string url)
        {
            return webBrowser.submitRequest_POST_Sync(url, "", null);
        }

        public static IO2HtmlPage POST(this IO2Browser webBrowser, string url, Dictionary<string, string> parameters)
        {
            return webBrowser.submitRequest_POST_Sync(url, "", parameters);
        }

        public static IO2HtmlPage POST(this IO2Browser webBrowser, string url, string targetFrame, Dictionary<string, string> parameters)
        {
            return webBrowser.submitRequest_POST_Sync(url, targetFrame, parameters);
        }
    }
}
