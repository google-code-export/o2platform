﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.Threading;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.XRules.Database.APIs;
using O2.XRules.Database.Utils;
//O2File:HttpData.cs

//O2File:_Extra_methods_Web.cs
//O2File:_Extra_methods_Items.cs


namespace HTTPProxyServer
{	
	public class ProxyCache
	{				
		public static List<RequestResponseData> Requests { get; set; }				
		public static Dictionary<string, RequestResponseData> RequestCache { get; set; }
		
		public ProxyCache_Brain_DefaultMode ProxyCache_Brain 	{ get; set; }
		public bool 						ProxyEnabled 	 	{ get; set; }			
		
		static ProxyCache()
		{
			Requests = new List<RequestResponseData>();
			RequestCache = new Dictionary<string, RequestResponseData>();			
		}		
		
		public ProxyCache()
		{
			ProxyCache_Brain = new ProxyCache_Brain_DefaultMode(this);
			ProxyEnabled = false;
		}
	}
	
	public class ProxyCache_Brain_DefaultMode
	{
		public ProxyCache proxyCache;
		
		public ProxyCache_Brain_DefaultMode(ProxyCache _proxyCache)
		{
			this.proxyCache = _proxyCache;
		}
		
		public virtual string cacheKey(RequestResponseData requestRessponseData)
		{
			return requestRessponseData.Request_Uri.pathNoQuery();
		}
	}
	
	
	public static class ProxyCache_ExtensionMethods
	{
		public static bool enabled(this ProxyCache proxyCache)
		{
			return proxyCache.ProxyEnabled;
		}
		
		public static Dictionary<string, RequestResponseData> requestMappings(this ProxyCache proxyCache)
		{
			return ProxyCache.RequestCache;
		}
		
		public static bool hasMapping(this ProxyCache proxyCache, string key)
		{
			return proxyCache.requestMappings().hasKey(key);
		}
		
		public static byte[] response_Bytes(this ProxyCache proxyCache, string key)
		{
			if (proxyCache.hasMapping(key))
				return proxyCache.requestMappings().value(key).Response_Bytes;
			return null;
		}
		
		public static string response_String(this ProxyCache proxyCache, string key)
		{
			if (proxyCache.hasMapping(key))
				return proxyCache.requestMappings().value(key).Response_String;
			return null;
		}
		
		public static ProxyCache add_ToCache(this ProxyCache proxyCache, RequestResponseData requestResponseData)
		{
			var cacheKey = proxyCache.cacheKey(requestResponseData);
			ProxyCache.Requests.Add(requestResponseData);
			ProxyCache.RequestCache.add(cacheKey,requestResponseData);
			return proxyCache;
		}
		
		
		public static string cacheKey(this ProxyCache proxyCache, RequestResponseData requestResponseData)
		{
			return proxyCache.ProxyCache_Brain.cacheKey(requestResponseData);
		}
		
	}
	
	public static class RequestResponseData_ExtensionMethods
	{
		public static RequestResponseData add(	this  ProxyCache proxyCache, 
												HttpWebRequest webRequest,
												HttpWebResponse webResponse,
												byte[] responseBytes,
												string responseString )
{			
			var requestResponseData = new RequestResponseData()
            									{
            										WebRequest = webRequest, 
													WebResponse = webResponse, 
													Response_Bytes = responseBytes,
													Response_String = responseString 
            									};	
			
			proxyCache.add_ToCache(requestResponseData);
			return requestResponseData;
		}
	}
	
}



/*


//based on the code from http://www.codeproject.com/KB/IP/HTTPSDebuggingProxy.aspx
//originally coded by @matt_mcknight  

// see O2_Web_Proxy.cs API for a way to consume this Proxy from O2

    public static class ProxyCache
    {
        public static Hashtable _cache  	{ get; set;} 
        public static Int32 _hits			{ get; set;} 
        
        private static Object _cacheLockObj = new object();
        private static Object _statsLockObj = new object();
        
        
        static ProxyCache()
        {
        	_cache = new Hashtable();
        }

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
*/    

