// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using mshtml;
using O2.External.IE.Interfaces;

namespace O2.External.IE.WebObjects
{
    public class IE_Link : IO2HtmlLink
    {
        public string Href { get; set; }
        public string InnerText { get; set; }
        public string InnerHtml { get; set; }        
        public string OuterHtml { get; set; }
        public string Target { get; set; }

        //public HtmlLinkIE(HTMLLinkElementClass linkElement)
        //public IE_Link(HTMLAnchorElementClass linkElement)
        public IE_Link(DispHTMLAnchorElement linkElement)         
        {
            Href = linkElement.href;
            OuterHtml = linkElement.outerHTML;
            InnerHtml = linkElement.innerHTML;
            InnerText = linkElement.innerText;
            Target = linkElement.target;
        }    
    }
}