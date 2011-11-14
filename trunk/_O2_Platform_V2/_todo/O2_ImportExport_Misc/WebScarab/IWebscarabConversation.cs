// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;

namespace O2.ImportExport.Misc.WebScarab
{
    public interface IWebscarabConversation
    {
        string id { get; set; }
        string request { get; set; }
        string response { get; set; }
        string RESPONSE_SIZE { get; set; }
        string WHEN { get; set; }
        string METHOD { get; set; }
        string COOKIE { get; set; }
        string STATUS { get; set; }
        string URL { get; set; }
        string TAG { get; set; }
        string ORIGIN { get; set; }
        List<string> XSS_GET { get; set; }
        List<string> CRLF_GET { get; set; }
        List<string> SET_COOKIE { get; set; }
        List<string> XSS_POST { get; set; }
    }
}
