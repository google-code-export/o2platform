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
using System.Net;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Web.Query;
using TweetSharp.Extensions;

namespace TweetSharp.Model
{
#if !SILVERLIGHT
    /// <summary>
    /// Represents a result returning from the Twitter API.
    /// </summary>
    [Serializable]
#endif
    public sealed class YammerResult : TweetSharpResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YammerResult"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="exception">The WebException that was caught during the query</param>
        public YammerResult(WebQueryResult result, WebException exception)
        {
            Response = result.Response;
            RequestUri = result.RequestUri;
            ResponseDate = result.ResponseDate;
            RequestDate = result.RequestDate;
            Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YammerResult"/> class.
        /// </summary>
        public YammerResult()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this result returned a Twitter error.
        /// </summary>
        /// <value><c>true</c> if this result is in error; otherwise, <c>false</c>.</value>
        public bool IsYammerError
        {
            get { return IsServiceError; }
        }

        public override bool IsServiceError
        {
            get { return !Response.IsNullOrBlank() && this.AsError() != null; }
        }
    }
}