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

namespace TweetSharp.Core.Web.Query
{
    /// <summary>
    /// Represents the result from a query execution.
    /// </summary>
    public class WebQueryResult
    {
        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>The request date.</value>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the response date.
        /// </summary>
        /// <value>The response date.</value>
        public DateTime? ResponseDate { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets the request URI.
        /// </summary>
        /// <value>The request URI.</value>
        public Uri RequestUri { get; set; }

        /// Gets or sets the request HTTP method.
        /// </summary>
        /// <value>The request HTTP method.</value>
        public virtual string RequestHttpMethod { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the response.
        /// </summary>
        /// <value>The type of the response.</value>
        public virtual string ResponseType { get; set; }

        /// <summary>
        /// Gets or sets the response HTTP status code.
        /// </summary>
        /// <value>The response HTTP status code.</value>
        public int ResponseHttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response HTTP status description.
        /// </summary>
        /// <value>The response HTTP status description.</value>
        public string ResponseHttpStatusDescription { get; set; }

        /// Gets or sets the length of the response.
        /// This length corresponds to the actual message size in bytes returned from Twitter. 
        /// </summary>
        /// <value>The length of the response.</value>
        public virtual long ResponseLength { get; set; }

        /// <summary>
        /// Gets or sets the response URI.
        /// </summary>
        /// <value>The response URI.</value>
        public virtual Uri ResponseUri { get; set; }
    }
}