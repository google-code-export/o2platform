using System;
using System.Windows.Forms;
using mshtml;
using O2.External.IE.Interfaces;
using O2.External.IE.WebObjects;
using O2.Kernel;

//O2Ref:C:\Program Files\Microsoft.NET\Primary Interop Assemblies\Microsoft.mshtml.dll
using O2.External.IE;

namespace O2.External.IE.Wrapper
{
    public class O2BrowserIE : ExtendedWebBrowser, IO2Browser
        //public class O2BrowserIE : WebBrowser, IO2Browser
    {
        public event Action<IO2HtmlPage> onDocumentCompleted;

        public O2BrowserIE()
        {
            //  Navigated += O2BrowserIE_Navigated;
            //  Navigating += O2BrowserIE_Navigating;

            //this.Navigated += O2BrowserIE_Navigated;

            DocumentComplete += O2BrowserIE_DocumentComplete;
            AllowWebBrowserDrop = false;
            

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
            
            PublicDI.log.debug("in O2BrowserIE_DocumentComplete for:{0}", e.Url); 
            //var uri = (Uri) e.Url;            
            try
            {
                if (onDocumentCompleted != null)
                    onDocumentCompleted(new IE_HtmlPage(e.DocumentClass));//.,.PageSource, e.Url));    
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex,"O2BrowserIE_DocumentComplete:",  true);
            }
                        
        }
                 
        public void open(string url)
        {
            PublicDI.log.debug("[O2BrowserIE] opening: {0}", url);
            Navigate(url);
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

    }
}