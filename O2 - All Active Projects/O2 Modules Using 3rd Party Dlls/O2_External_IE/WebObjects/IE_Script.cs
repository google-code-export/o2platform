using mshtml;
using O2.External.IE.Interfaces;

namespace O2.External.IE.WebObjects
{
    public class IE_Script : IO2HtmlScript
    {
        public string CharSet { get; set; }
        public string Event { get; set; }
        public bool Defer { get; set; }
        public string HtmlFor { get; set; }
        public string Src { get; set; }
        public string Text { get; set; }
        public string OuterHtml { get; set; }

        public IE_Script(HTMLScriptElementClass script)
        {
            OuterHtml = script.outerHTML;
            CharSet = ((IHTMLScriptElement2)script).charset;
            Event = ((IHTMLScriptElement) script).@event;
            Defer = ((IHTMLScriptElement)script).defer;
            HtmlFor = ((IHTMLScriptElement)script).htmlFor;
            Src = ((IHTMLScriptElement)script).src;
            Text = ((IHTMLScriptElement)script).text;            
            //Charset = script.charset;            
        }
    }
}