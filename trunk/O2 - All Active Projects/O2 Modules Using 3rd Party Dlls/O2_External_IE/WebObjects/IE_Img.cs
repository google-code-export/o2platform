using mshtml;
using O2.External.IE.Interfaces;

namespace O2.External.IE.WebObjects
{
    public class IE_Img : IO2HtmlImg
    {
        public string OuterHtml { get; set; }
        public IE_Img(HTMLImgClass image)
        {
            OuterHtml = image.outerHTML;
        }
    }
}