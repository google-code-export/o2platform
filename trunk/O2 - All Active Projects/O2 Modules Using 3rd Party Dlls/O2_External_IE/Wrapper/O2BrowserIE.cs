using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Network;
using O2.External.IE.Interfaces;
using O2.External.IE.WebObjects;
using O2.Kernel;

namespace O2.External.IE.Wrapper
{
    public class O2BrowserIE : ExtendedWebBrowser, IO2Browser        
    {
        public event Action<IO2HtmlPage> onDocumentCompleted;
        public bool DebugMode;                      // false by default
        public IO2HtmlPage HtmlPage;
        public AutoResetEvent documentCompleted;

        public O2BrowserIE()
        {
            //  Navigated += O2BrowserIE_Navigated;
            //  Navigating += O2BrowserIE_Navigating;

            //this.Navigated += O2BrowserIE_Navigated;

            DocumentComplete += O2BrowserIE_DocumentComplete;
            AllowWebBrowserDrop = false;
            documentCompleted = new AutoResetEvent(false);
            

            /*DocumentCompleted +=
                (sender, e)
                =>
                    {
                        PublicDI.log.debug("Document Complete:{0}" , e.Url);
                        if (onDocumentCompleted != null)
                            onDocumentCompleted(new IE_HtmlPage((O2BrowserIE)sender, e.Url));
                    };*/
            
        }

       

        /* void O2BrowserIE_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
         //   PublicDI.log.info("Navigating: {0} ({1}", e.Url, e.TargetFrameName);
            //throw new NotImplementedException();
        }

        void O2BrowserIE_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
         //   PublicDI.log.info("Navigated: {0}", e.Url);
        }*/

        void O2BrowserIE_DocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            documentCompleted.Set();
            if (DebugMode)
                PublicDI.log.debug("in O2BrowserIE_DocumentComplete for:{0}", e.Url); 
            ////var uri = (Uri) e.Url;            
            try
            {
                HtmlPage = new IE_HtmlPage(e.DocumentClass);
                if (onDocumentCompleted != null)
                    onDocumentCompleted(HtmlPage);
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex,"O2BrowserIE_DocumentComplete:",  true);
            }
                        
        }
                 
        public void open(string url)
        {
            if (DebugMode)
                PublicDI.log.debug("[O2BrowserIE] opening: {0}", url);
            Navigate(url);
        }
        
        /// <summary>
        /// _note: this only works well for pages with no frames (since on those cases the returned IO2HtmlPage will be the first one loaded)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IO2HtmlPage openSync(string url)
        {
            documentCompleted.Reset();
            O2Thread.mtaThread(() => open(url));
            documentCompleted.WaitOne();
            return HtmlPage;
        }

        
        public bool HtmlEditMode
        {
            get
            {
                if (Document != null)
                {
                    var doc = (mshtml.IHTMLDocument2) Document.DomDocument;
                    return (doc.designMode == "On");
                }
                PublicDI.log.error("in DesignMode.get Document == null");
                return false;
            }
            set
            {
                if (Document != null)
                {
                    var doc = (mshtml.IHTMLDocument2)Document.DomDocument;
                    doc.designMode = value ? "On" : "Off";
                }
                else
                    PublicDI.log.error("in DesignMode.get Document == null");
            }
        }

        public void submitRequest_POST(string url, string targetFrame, Dictionary<string, string> parameters)
        {
            var parametersText = "";
            if (parameters != null)
                foreach (var parameter in parameters.Keys)
                    parametersText += string.Format("{0}={1}&", parameter, WebEncoding.urlEncode(parameters[parameter]));
            byte[] postData = Encoding.ASCII.GetBytes(parametersText);
            const string additionalHeaders = "Content-Type: application/x-www-form-urlencoded";
            Navigate(url, targetFrame, postData, additionalHeaders);
        }

        public void submitRequest_GET(string url, string targetFrame, Dictionary<string, string> parameters)
        {
            var parametersText = "";
            if (parameters != null)
            {
                foreach (var parameter in parameters.Keys)
                    parametersText += string.Format("{0}={1}&", parameter, WebEncoding.urlEncode(parameters[parameter]));
                url += "?" + parametersText;
            }
            Navigate(url, targetFrame);
        }

        public IO2HtmlPage submitRequest_POST_Sync(string url, string targetFrame, Dictionary<string, string> parameters)
        {
            documentCompleted.Reset();
            O2Thread.mtaThread(() => submitRequest_POST(url, targetFrame, parameters));
            documentCompleted.WaitOne();
            return HtmlPage;
        }

        public IO2HtmlPage submitRequest_GET_Sync(string url, string targetFrame, Dictionary<string, string> parameters)
        {
            documentCompleted.Reset();
            O2Thread.mtaThread(() => submitRequest_GET(url, targetFrame, parameters));
            documentCompleted.WaitOne();
            return HtmlPage;
        }


    }
}