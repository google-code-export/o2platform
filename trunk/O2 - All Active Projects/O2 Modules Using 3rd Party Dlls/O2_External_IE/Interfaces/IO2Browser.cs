using System;
using System.Linq;
using System.Text;
using O2.External.IE.WebObjects;
using O2.Kernel.CodeUtils;

namespace O2.External.IE.Interfaces
{        

    public interface IO2Browser
    {
        event Action<IO2HtmlPage> onDocumentCompleted;
        bool HtmlEditMode { get; set; }
        void open(string url);
    }
}
