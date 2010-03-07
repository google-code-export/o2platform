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

using System.Net;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Web.Query;

namespace TweetSharp.Model
{
    public abstract class TweetSharpResult : WebQueryResult
    {
        public abstract bool IsServiceError { get; }

        /// <summary>
        /// Gets a value indicating whether this result was served from a cache.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this result is from a cache; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsFromCache
        {
            get
            {
                return !Response.IsNullOrBlank() &&
                       ResponseUri == null &&
                       !IsServiceError;
            }
        }

#if !SILVERLIGHT
        public virtual bool IsNetworkError
        {
            get
            {
                return ExceptionStatus != WebExceptionStatus.Success
                       && ExceptionStatus != WebExceptionStatus.ProtocolError
                       && ExceptionStatus != WebExceptionStatus.Pending;
            }
        }

        public virtual bool TimedOut
        {
            get
            {
                return ExceptionStatus == WebExceptionStatus.Timeout
                       || ExceptionStatus == WebExceptionStatus.RequestCanceled;
            }
        }


#else
        public virtual bool IsNetworkError
        {
            get
            {
                return ExceptionStatus != WebExceptionStatus.Success
                       && ExceptionStatus != WebExceptionStatus.Pending;
            }
        }

        public virtual bool IsTimeout
        {
            get { return ExceptionStatus == WebExceptionStatus.RequestCanceled; }
        }
#endif

        public virtual WebException Exception { get; protected set; }

        public virtual WebExceptionStatus ExceptionStatus
        {
            get { return Exception == null ? WebExceptionStatus.Success : Exception.Status; }
        }

        public virtual TweetSharpResult PreviousResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how many retries occured to get the current result
        /// </summary>
        public virtual int Retries
        {
            get
            {
                var ret = 0;
                var previous = PreviousResult;
                while (previous != null)
                {
                    ret++;
                    previous = previous.PreviousResult;
                }
                return ret;
            }
        }
    }
}