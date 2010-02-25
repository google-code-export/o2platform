using System;
using System.Collections.Generic;
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
        IO2HtmlPage openSync(string url);
        void submitRequest_POST(string url, string targetFrame, Dictionary<string, string> parameters);
        void submitRequest_GET(string url, string targetFrame, Dictionary<string, string> parameters);

        IO2HtmlPage submitRequest_POST_Sync(string url, string targetFrame, Dictionary<string, string> parameters);
        IO2HtmlPage submitRequest_GET_Sync(string url, string targetFrame, Dictionary<string, string> parameters);
    }
}
