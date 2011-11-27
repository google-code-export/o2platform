//based on the code from http://www.codeproject.com/KB/IP/HTTPSDebuggingProxy.aspx
//originally coded by @matt_mcknight  

// see O2_Web_Proxy.cs API for a way to consume this Proxy from O2

//O2File:ProxyServer.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HTTPProxyServer
{
    public class CacheEntry
    {
        public CacheKey Key { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime DateStored { get; set; }
        public Byte[] ResponseBytes { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public String StatusDescription { get; set; }
        public List<Tuple<String,String>> Headers { get; set; }
        public Boolean FlagRemove { get; set; }
    }
}
