//based on the code from http://www.codeproject.com/KB/IP/HTTPSDebuggingProxy.aspx
//originally coded by @matt_mcknight  

// see O2_Web_Proxy.cs API for a way to consume this Proxy from O2

//O2File:ProxyServer.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTPProxyServer
{
    public class CacheKey
    {
        public String AbsoluteUri { get; set; }
        public String UserAgent { get; set; }

        public CacheKey(String requestUri, String userAgent)
        {
            AbsoluteUri = requestUri;
            UserAgent = userAgent;
        }

        public override bool Equals(object obj)
        {
            CacheKey key = obj as CacheKey;
            if (key != null)
                return (key.AbsoluteUri == AbsoluteUri && key.UserAgent == UserAgent);
            return false;
        }

        public override int GetHashCode()
        {
            String s = AbsoluteUri + UserAgent;
            return s.GetHashCode();
        }
    }
}
