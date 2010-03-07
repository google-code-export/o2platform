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

namespace TweetSharp.Core.Caching
{
    /// <summary>
    /// A simple caching interface that supports absolute and sliding expiration.
    /// </summary>
    public interface IClientCache
    {
        /// <summary>
        /// Caches a new value under the specified key. 
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        void Insert(string key, object value);

        /// <summary>
        /// Caches a new value under the specified key, with an absolute expiration.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="absoluteExpiration">The absolute expiration of the cached value</param>
        void Insert(string key, object value, DateTime absoluteExpiration);

        /// <summary>
        /// Caches a new value under the specified key, with an absolute expiration.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="slidingExpiration">The sliding inactivity expiration of the cached value</param>
        void Insert(string key, object value, TimeSpan slidingExpiration);

        /// <summary>
        /// Retrieves a value from the cache, strongly typed to the value's type.
        /// </summary>
        /// <typeparam name="T">The expected type of the cached value</typeparam>
        /// <param name="key">The key</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Removes a value from the cache by its key.
        /// </summary>
        /// <param name="key">The key</param>
        void Remove(string key);
    }
}