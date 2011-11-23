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
