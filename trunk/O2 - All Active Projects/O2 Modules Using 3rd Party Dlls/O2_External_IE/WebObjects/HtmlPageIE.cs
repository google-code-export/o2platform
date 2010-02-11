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
    public class HtmlPageIE : IO2HtmlPage
    {        
        public Uri PageUri { get; set; }
        public string PageSource { get; set; }

        public List<IO2HtmlAnchor> Anchors { get; set; }
        public List<IO2HtmlForm> Forms { get; set; }
        public List<IO2HtmlImg> Images { get; set; }
        public List<IO2HtmlLink> Links { get; set; }
        public List<IO2HtmlScript> Scripts { get; set; }

        public HtmlPageIE()
        {
            //PageUri = new Uri("");
            PageSource = "";
            Anchors = new List<IO2HtmlAnchor>();
            Forms = new List<IO2HtmlForm>();
            Images = new List<IO2HtmlImg>();
            Links = new List<IO2HtmlLink>();
            Scripts = new List<IO2HtmlScript>();
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

        private List<T1> populateVar<T, T1>(IHTMLElementCollection elementCollection) //, List<T> targetList) 
        {
            var targetList = new List<T1>();
            foreach (IHTMLElement element in elementCollection)
            {
                var t = (T1) typeof (T).ctor(element);
                targetList.Add(t);
            }
            return targetList;
        }

        private void populateData(HTMLDocumentClass documentClass)
        {
            // PageUri
            PageUri = new Uri(documentClass.url);
            // get PageSource
            var documentElement = (HTMLHtmlElementClass)documentClass.documentElement;
            PageSource = documentElement.outerHTML;



            Anchors = populateVar<IE_Anchor,IO2HtmlAnchor>(documentClass.anchors);//, new List<IE_Link>());// "Anchors");
            //populateVar<HTMLAnchorElementClass>(documentClass.applets, "Applets");
            //populateVar<HTMLAnchorElementClass>(documentClass.embeds, "Embeds");
            Forms = populateVar<IE_Form,IO2HtmlForm>(documentClass.forms);//, "Forms");                        
            Images = populateVar<IE_Img, IO2HtmlImg>(documentClass.images);//, "Images");
            Links = populateVar<IE_Link, IO2HtmlLink>(documentClass.links); //, new List<IE_Link>()); //"Links");            
            //populateVar<HTMLAnchorElementClass>(documentClass.plugins, "Plugins");                        
            Scripts = populateVar<IE_Script, IO2HtmlScript>(documentClass.scripts);//, "Scripts");

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
     
        public override string ToString()
        {
            return PageUri.ToString();
        }
    }
}
