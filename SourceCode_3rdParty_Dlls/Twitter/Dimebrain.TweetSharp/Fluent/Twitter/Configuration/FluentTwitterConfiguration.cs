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
using System.ComponentModel;
using TweetSharp.Core.Caching;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
#if !SILVERLIGHT
    [Serializable]
#endif

    public class FluentTwitterConfiguration : IFluentTwitterConfiguration
    {
        public FluentTwitterConfiguration(IFluentTwitter root)
        {
            Root = root;
        }

        #region IFluentTwitterConfiguration Members

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IFluentTwitter Root { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool LimitRate { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool TruncateUpdates { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShortenUrls { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool CompressHttpRequests { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool MockWebRequests { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<IModel> MockGraph { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ShortenUrlServiceProvider? ShortenUrlService { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ShortenUrlPassword { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ShortenUrlUsername { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ShortenUrlApiKey { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IClientCache CacheStrategy { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime? CacheAbsoluteExpiration { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TimeSpan? CacheSlidingExpiration { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Proxy { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string TransparentProxy { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public RetryOn RetryConditions { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int MaxRetries { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TimeSpan? RequestTimeout { get; set; }

        #endregion
    }
}