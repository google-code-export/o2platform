//based on the code from http://www.codeproject.com/KB/IP/HTTPSDebuggingProxy.aspx
//originally coded by @matt_mcknight  

// see O2_Web_Proxy.cs API for a way to consume this Proxy from O2

//O2File:ProxyServer.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.Threading;
using O2.Kernel.ExtensionMethods;

namespace HTTPProxyServer
{
    public static class ProxyCache
    {
        private static Hashtable _cache = new Hashtable();
        private static Object _cacheLockObj = new object();
        private static Object _statsLockObj = new object();
        private static Int32 _hits;

        public static CacheEntry GetData(HttpWebRequest request)
        {
            CacheKey key = new CacheKey(request.RequestUri.AbsoluteUri, request.UserAgent);
            if (_cache[key] != null)
            {
                CacheEntry entry = (CacheEntry)_cache[key];
                if (entry.FlagRemove || (entry.Expires.HasValue && entry.Expires < DateTime.Now))
                {
                    //don't remove it here, just flag
                    entry.FlagRemove = true;
                    return null;
                }
                Monitor.Enter(_statsLockObj);
                _hits++;
                Monitor.Exit(_statsLockObj);
                return entry;
            }
            return null;
        }

        public static CacheEntry MakeEntry(HttpWebRequest request, HttpWebResponse response,List<Tuple<String,String>> headers, DateTime? expires)
        {
            CacheEntry newEntry = new CacheEntry();
            newEntry.Expires = expires;
            newEntry.DateStored = DateTime.Now;
            newEntry.Headers = headers;
            newEntry.Key = new CacheKey(request.RequestUri.AbsoluteUri, request.UserAgent);
            newEntry.StatusCode = response.StatusCode;
            newEntry.StatusDescription = response.StatusDescription;
            if (response.ContentLength > 0)
                newEntry.ResponseBytes = new Byte[response.ContentLength];
            return newEntry;
        }

        public static void AddData(CacheEntry entry)
        {

            Monitor.Enter(_cacheLockObj);
            if (!_cache.Contains(entry.Key))
                _cache.Add(entry.Key, entry);
            Monitor.Exit(_cacheLockObj);


        }

        public static Boolean CanCache(WebHeaderCollection headers, ref DateTime? expires)
        {

            foreach (String s in headers.AllKeys)
            {
                String value = headers[s].ToLower();
                switch (s.ToLower())
                {
                    case "cache-control":
                        if (value.Contains("max-age"))
                        {
                            int seconds;
                            if (int.TryParse(value, out seconds))
                            {
                                if (seconds == 0)
                                    return false;
                                DateTime d = DateTime.Now.AddSeconds(seconds);
                                if (!expires.HasValue || expires.Value < d)
                                    expires = d;

                            }
                        }

                        if (value.Contains("private") || value.Contains("no-cache"))
                            return false;
                        else if (value.Contains("public") || value.Contains("no-store"))
                            return true;

                        break;

                    case "pragma":

                        if (value == "no-cache")
                            return false;

                        break;
                    case "expires":
                        DateTime dExpire;
                        if (DateTime.TryParse(value, out dExpire))
                        {
                            if (!expires.HasValue || expires.Value < dExpire)
                                expires = dExpire;
                        }
                        break;
                }
            }
            return true;
        }

        public static void CacheMaintenance()
        {        
        	Console.WriteLine("Log is disabled");
        	"Cache is disabled...".debug();
        	return;
            try
            {
                while (true)
                {                	
                    Thread.Sleep(30000);
                    List<CacheKey> keysToRemove = new List<CacheKey>();
                    foreach (CacheKey key in _cache.Keys)
                    {
                        CacheEntry entry = (CacheEntry)_cache[key];
                        if (entry.FlagRemove || entry.Expires < DateTime.Now)
                            keysToRemove.Add(key);
                    }

                    foreach (CacheKey key in keysToRemove)
                        _cache.Remove(key);

                    Console.WriteLine(String.Format("....Cache maintenance complete.  Number of items stored={0} Number of cache hits={1} ", _cache.Count, _hits));
                }
            }
            catch (ThreadAbortException) { }
        }

    }
}
