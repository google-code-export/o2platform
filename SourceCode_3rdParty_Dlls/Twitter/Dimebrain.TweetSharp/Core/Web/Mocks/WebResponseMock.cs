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

namespace TweetSharp.Core.Web.Mocks
{
    public class WebResponseMock : WebResponse
    {
        private readonly Uri _origin;
        public string Content { get; private set; }

        public override Uri ResponseUri
        {
            get { return _origin; }
        }

        public WebResponseMock(Uri origin, string content)
        {
            _origin = origin;
            Content = content;
        }

#if SILVERLIGHT
        public override string ContentType { get; private set; }

        public override Stream GetResponseStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(Content));
        }

        public override void Close()
        {
            // todo
        }

        // todo implement Silverlight's length setting
        private long _contentLength;
        public override long ContentLength
        {
            get
            {
                return _contentLength;
            }
        }
#endif
    }
}