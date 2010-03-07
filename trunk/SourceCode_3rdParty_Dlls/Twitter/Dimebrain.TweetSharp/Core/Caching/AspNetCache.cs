#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace TweetSharp.Core.Caching
{
    /// <summary>
    /// The ASP.NET Cache system implemented behind <see cref="IWebCache" />
    /// </summary>
    internal class AspNetCache : IWebCache
    {
        #region IWebCache Members

        public int Count
        {
            get { return HttpRuntime.Cache.Count; }
        }

        public void Add(string key, object value, CacheDependency dependency, DateTime absoluteExpiration,
                        TimeSpan slidingExpiration, CacheItemPriority priority,
                        CacheItemRemovedCallback onRemoveCallback)
        {
            HttpRuntime.Cache.Add(key, value, dependency, absoluteExpiration, Cache.NoSlidingExpiration, priority,
                                  onRemoveCallback);
        }

        public void Insert(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public void Insert(string key, object value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public void Insert(string key, object value, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public void Insert(string key, object value, CacheDependency dependencies)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public void Insert(string key, object value, CacheDependency dependencies, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

#if !Mono
        public void Insert(string key, object value, CacheDependency dependencies, TimeSpan slidingExpiration,
                           CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, slidingExpiration, priority,
                                     onRemoveCallback);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                           CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, absoluteExpiration, Cache.NoSlidingExpiration, priority,
                                     onRemoveCallback);
        }

        public void Insert(string key, object value, CacheDependency dependencies, TimeSpan slidingExpiration,
                           CacheItemUpdateCallback onUpdateCallback)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, Cache.NoAbsoluteExpiration, slidingExpiration,
                                     onUpdateCallback);
        }

        public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                           CacheItemUpdateCallback onUpdateCallback)
        {
            HttpRuntime.Cache.Insert(key, value, dependencies, absoluteExpiration, Cache.NoSlidingExpiration,
                                     onUpdateCallback);
        }
#endif

        public void Clear()
        {
            var keys = new List<string>();
            var enumerator = HttpRuntime.Cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var key = enumerator.Key.ToString();
                keys.Add(key);
            }

            for (var i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
        }

        public T Get<T>(string key)
        {
            return (T) HttpRuntime.Cache.Get(key);
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public IEnumerable<string> Keys
        {
            get
            {
                var enumerator = HttpRuntime.Cache.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Key.ToString();
                }
            }
        }

        #endregion
    }
}