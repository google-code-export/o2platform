using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//O2Ref:C:\Program Files\Microsoft.NET\Primary Interop Assemblies\Microsoft.mshtml.dll
using mshtml;
using O2.External.IE.Interfaces;
using O2.Kernel;

namespace O2.External.IE.WebObjects
{
    public class HtmlPageIE_ : IHtmlPage
    {
        public string PageSource { get; set; }
        public Uri PageUri { get; set; }

        public HtmlPageIE_(string pageSource, string rawUrl)
        {
            PageSource = pageSource;
            PageUri = new Uri(rawUrl);
        }

        /*public HtmlPageIE(O2BrowserIE o2BrowserIE, Uri uri)
        {
            PageUri = uri;            
            if (o2BrowserIE.Document != null && o2BrowserIE.Document.Body != null)
            {
                PageSource = o2BrowserIE.Document.DomDocument.ToString(); // o2BrowserIE.Document.Body.OuterHtml;                
            }
        }*/

        //PageSource = o2BrowserIE.DocumentText;
        //PublicDI.log.info("in HtmlPageIE");
        //var doc = (HTMLDocumentClass)o2BrowserIE.Document;

        //PublicDI.log.info("LocationURL: {0}", o2BrowserIE.Url.ToString());
        //PublicDI.log.info("LocationName: {0}", o2BrowserIE.Name);
        //PublicDI.log.info("Document obj: {0}", (o2BrowserIE.Document.GetType().FullName));


        /*PublicDI.log.info("doc.title:{0}", o2BrowserIE.Document.Title);
        PublicDI.log.info("doc.url:{0}", o2BrowserIE.Document.Url);
        PublicDI.log.debug("doc.outherHtml:{0}", o2BrowserIE.Document.Body.OuterHtml);*/

        public override string ToString()
        {
            return PageUri.ToString();
        }
    }
}
