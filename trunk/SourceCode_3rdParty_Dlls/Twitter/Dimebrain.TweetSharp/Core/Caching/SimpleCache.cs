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

namespace TweetSharp.Core.Caching
{
    /// <summary>
    /// A basic in-memory cache.
    /// </summary>
    internal class SimpleCache : IClientCache
    {
        private const string NOT_SUPPORTED_MESSAGE = "This simple cache does not support expiration.";

        private static readonly IDictionary<string, object> _cache = new Dictionary<string, object>(0);

        public int Count
        {
            get { return _cache.Count; }
        }

        public IEnumerable<string> Keys
        {
            get { return _cache.Keys; }
        }

        #region IClientCache Members

        public void Insert(string key, object value)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, value);
            }
            else
            {
                _cache[key] = value;
            }
        }

        public void Insert(string key, object value, DateTime absoluteExpiration)
        {
            throw new NotSupportedException(NOT_SUPPORTED_MESSAGE);
        }

        public void Insert(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotSupportedException(NOT_SUPPORTED_MESSAGE);
        }

        public T Get<T>(string key)
        {
            if (_cache.ContainsKey(key))
            {
                return (T) _cache[key];
            }
            return default(T);
        }

        public void Remove(string key)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
            }
        }

        #endregion
    }
}