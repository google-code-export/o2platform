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

    public sealed class TwitterResult : TweetSharpResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterResult"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="exception">The WebException that was caught during the query</param>
        public TwitterResult(WebQueryResult result, WebException exception)
        {
            Response = result.Response;
            RequestUri = result.RequestUri;
            ResponseDate = result.ResponseDate;
            RequestDate = result.RequestDate;
            Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterResult"/> class.
        /// </summary>
        public TwitterResult()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this result returned a Twitter error.
        /// </summary>
        /// <value><c>true</c> if this result is in error; otherwise, <c>false</c>.</value>
        public bool IsTwitterError
        {
            get { return IsServiceError; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsServiceError
        {
            get { return IsFailWhale || Exception != null || (!Response.IsNullOrBlank() && this.AsError() != null); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a fail whale.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a fail whale; otherwise, <c>false</c>.
        /// </value>
        public bool IsFailWhale
        {
            get
            {
                // text/html; charset=UTF-8
                return (ResponseHttpStatusCode == 502 || ResponseHttpStatusCode == 503) &&
                       (!ResponseType.IsNullOrBlank() &&
                        ResponseType.ToLower().Contains("text/html"));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this result was served from a continuous stream.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this result is from a stream; otherwise, <c>false</c>.
        /// </value>
        public bool IsFromStream
        {
            get { return StreamedResponses != null; }
        }

        /// <summary>
        /// Gets or sets the streamed responses requested from the streaming API.
        /// </summary>
        /// <value>The streamed responses.</value>
        public IEnumerable<string> StreamedResponses { get; set; }

#if !SILVERLIGHT
        public bool ConnectionClosed
        {
            get
            {
                return ExceptionStatus == WebExceptionStatus.ConnectionClosed ||
                       ExceptionStatus == WebExceptionStatus.KeepAliveFailure;
            }
        }
#endif


        /// <summary>
        /// Gets or sets the rate limit status, if it was available at the time of the response.
        /// </summary>
        /// <value>The rate limit status.</value>
        public TwitterRateLimitStatus RateLimitStatus { get; set; }


        /// <summary>
        /// Gets or sets a flag indicating if the request was skipped for rate limiting rules. 
        /// </summary>
        public bool SkippedDueToRateLimiting { get; set; }
    }
}