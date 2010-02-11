using mshtml;
using O2.External.IE.Interfaces;

namespace O2.External.IE.WebObjects
{
    public class IE_Anchor : IO2HtmlAnchor
    {
        public string OuterHtml { get; set; }
        public IE_Anchor(HTMLAnchorElementClass anchor)
        {
            OuterHtml = anchor.outerHTML;
        }
    }
}