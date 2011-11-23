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
