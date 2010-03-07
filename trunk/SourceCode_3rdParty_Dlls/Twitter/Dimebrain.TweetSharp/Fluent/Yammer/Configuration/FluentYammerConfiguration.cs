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
using TweetSharp.Core.Caching;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    [Serializable]
    internal class FluentYammerConfiguration : IFluentYammerConfiguration
    {
        public FluentYammerConfiguration(IFluentYammer root)
        {
            Root = root;
        }

        #region IFluentYammerConfiguration Members

        public IFluentYammer Root { get; private set; }

        //public bool LimitRate { get; set; }
        public bool MockWebRequests { get; set; }
        public IEnumerable<IModel> MockGraph { get; set; }

        public IClientCache CacheStrategy { get; set; }
        public DateTime? CacheAbsoluteExpiration { get; set; }
        public TimeSpan? CacheSlidingExpiration { get; set; }
        public bool CompressHttpRequests { get; set; }

        public string Proxy { get; set; }
        public string TransparentProxy { get; set; }
        public RetryOn RetryConditions { get; set; }
        public int MaxRetries { get; set; }
        public TimeSpan? RequestTimeout { get; set; }

        #endregion
    }
}