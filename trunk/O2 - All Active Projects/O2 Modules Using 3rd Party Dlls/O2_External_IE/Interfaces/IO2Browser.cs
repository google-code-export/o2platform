using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.External.IE.Interfaces
{
    public interface IO2Browser_
    {
        event Action<IHtmlPage> onDocumentCompleted;
        bool HtmlEditMode { get; set; }
        void open(string url);
    }

    public interface IHtmlPage
    {
        string PageSource { get; set; }
    }
}
