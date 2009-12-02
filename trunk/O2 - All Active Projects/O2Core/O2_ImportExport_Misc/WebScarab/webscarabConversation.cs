// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.ImportExport.Misc.WebScarab
{
    public class WebscarabConversation : IWebscarabConversation
    {
        public string id { get; set; }
        public string request { get; set; }
        public string response { get; set; }
        public string RESPONSE_SIZE { get; set; }
        public string WHEN { get; set; }
        public string METHOD { get; set; }
        public string COOKIE { get; set; }
        public string STATUS { get; set; }
        public string URL { get; set; }
        public string TAG { get; set; }
        public string ORIGIN { get; set; }
        public List<string> XSS_GET { get; set; }
        public List<string> CRLF_GET { get; set; }
        public List<string> SET_COOKIE { get; set; }
        public List<string> XSS_POST { get; set; }

        public WebscarabConversation()
        {
            XSS_GET = new List<string>();
            CRLF_GET = new List<string>();
            SET_COOKIE = new List<string>();
            XSS_POST = new List<string>();
        }
    }
}
