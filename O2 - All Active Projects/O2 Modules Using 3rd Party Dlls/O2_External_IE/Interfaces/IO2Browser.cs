using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.External.IE.WebObjects;

namespace O2.External.IE.Interfaces
{
    public interface IO2Browser
    {
        event Action<IHtmlPage> onDocumentCompleted;
        bool HtmlEditMode { get; set; }
        void open(string url);
    }

    public interface IHtmlPage
    {
        string PageSource { get; set; }
        List<IE_Anchor> Anchors { get; set; }       // todo: replace these IE_* class with reneric IHTML* equivalent
        List<IE_Form> Forms { get; set; }
        List<IE_Img> Images { get; set; }
        List<IE_Link> Links { get; set; }
        List<IE_Script> Scripts { get; set; }

    }
}
