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
using System.Web.Caching;

namespace TweetSharp.Core.Caching
{
    /// <summary>
    /// A cache that uses cache dependency and priority features.
    /// </summary>
    public interface IWebCache : IClientCache
    {
        void Add(string key, object value, CacheDependency dependency, DateTime absoluteExpiration,
                 TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback removedCallback);

        void Insert(string key, object value, CacheDependency dependencies);
        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration);
        void Insert(string key, object value, CacheDependency dependencies, TimeSpan slidingExpiration);

#if !Mono
        void Insert(string key, object value, CacheDependency dependencies, TimeSpan slidingExpiration,
                    CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);

        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                    CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback);

        void Insert(string key, object value, CacheDependency dependencies, TimeSpan slidingExpiration,
                    CacheItemUpdateCallback onUpdateCallback);

        void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration,
                    CacheItemUpdateCallback onUpdateCallback);
#endif

        void Clear();
    }
}