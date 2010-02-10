using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//O2Ref:C:\Program Files\Microsoft.NET\Primary Interop Assemblies\Microsoft.mshtml.dll
using System.Windows.Forms;
using mshtml;
using O2.External.IE.Interfaces;
using O2.Kernel;
using O2.Kernel.CodeUtils;

namespace O2.External.IE.WebObjects
{
    public class HtmlPageIE : IHtmlPage
    {        
        public Uri PageUri { get; set; }
        public string PageSource { get; set; }

        public List<IE_Anchor> Anchors { get; set; }
        public List<IE_Form> Forms { get; set; }
        public List<IE_Img> Images { get; set; }
        public List<IE_Link> Links { get; set; }
        public List<IE_Script> Scripts { get; set; }

        public HtmlPageIE()
        {
            //PageUri = new Uri("");
            PageSource = "";
            //Scripts = new List<string>();
        }

        public HtmlPageIE(string pageSource, string rawUrl) : this()
        {
            PageSource = pageSource;
            PageUri = new Uri(rawUrl);
        }

        public HtmlPageIE(HTMLDocumentClass documentClass)
            : this()
        {
            populateData(documentClass);
        }

        private List<T> populateVar<T>(IHTMLElementCollection elementCollection) //, List<T> targetList) 
        {
            var targetList = new List<T>();
            //PublicDI.log.debug("### Mapping  {0}", targetList);
            foreach (IHTMLElement element in elementCollection)
            //    if (element is HTMLAnchorElementClass)
                {
            
                    var t = (T)typeof(T).ctor(element);                    
                    targetList.Add(t);
                        //PublicDI.log.debug("t not null: {0}", t.prop("href"));
                    //targetList.Add(element);
                    //var t1 = new T1(elementCollection);
//                    targetList.Add(new T((T1)elementCollection));
                    //PublicDI.log.debug(" {0} {1} ", element.tagName, element.GetType().FullName, element.outerHTML);
                }
            //PublicDI.log.info("-------- there are {0} elements in the current List of Type: {1}", targetList.Count, typeof(T).Name);
            return targetList;
        }

        private void populateData(HTMLDocumentClass documentClass)
        {
            // PageUri
            PageUri = new Uri(documentClass.url);
            // get PageSource
            var documentElement = (HTMLHtmlElementClass)documentClass.documentElement;
            PageSource = documentElement.outerHTML;

            Anchors = populateVar<IE_Anchor>(documentClass.anchors);//, new List<IE_Link>());// "Anchors");
            //populateVar<HTMLAnchorElementClass>(documentClass.applets, "Applets");
            //populateVar<HTMLAnchorElementClass>(documentClass.embeds, "Embeds");
            Forms = populateVar<IE_Form>(documentClass.forms);//, "Forms");            
            Images = populateVar<IE_Img>(documentClass.images);//, "Images");
            Links = populateVar<IE_Link>(documentClass.links); //, new List<IE_Link>()); //"Links");            
            //populateVar<HTMLAnchorElementClass>(documentClass.plugins, "Plugins");                        
            Scripts = populateVar<IE_Script>(documentClass.scripts);//, "Scripts");

            //populateVar<HTMLAnchorElementClass>(documentClass.frames, "Frames");
            //populateVar<HTMLAnchorElementClass>(documentClass.namespaces, "Namespaces");
            //populateVar<HTMLAnchorElementClass>(documentClass.styleSheets, "StyleSheets");

            //documentClass.cookie
            /*documentClass.dir;
            documentClass.defaultCharset;
            documentClass.activeElement;
            documentClass.attributes;
            documentClass.baseUrl;
            documentClass.compatMode;
            documentClass.doctype;
            documentClass.domain;
            documentClass.enableDownload;
            documentClass.fileCreatedDate;
            documentClass.fileModifiedDate;
            documentClass.fileSize;
            documentClass.fileUpdatedDate;
            documentClass.location;
            documentClass.media;
            documentClass.mimeType;
            documentClass.protocol;
            documentClass.referrer;
            documentClass.title;
            documentClass.uniqueID;
            documentClass.url;
            documentClass.URLUnencoded;
            */

            
            
            /*
            PublicDI.log.debug("### Links");
            foreach (var link in documentClass.anchors)                
            {
                PublicDI.log.debug(link.type().Name);
            }
            //scripts
            PublicDI.log.debug("### SCRIPTS");
            foreach (HTMLScriptElementClass script in documentClass.scripts)
            {
                PublicDI.log.debug(script.outerHTML);
            }*/


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
