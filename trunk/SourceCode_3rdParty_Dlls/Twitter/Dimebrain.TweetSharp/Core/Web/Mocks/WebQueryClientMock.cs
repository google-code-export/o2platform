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
using System.IO;
using System.Net;
using System.Text;
using TweetSharp.Core.Extensions;
using TweetSharp.Model;

namespace TweetSharp.Core.Web.Mocks
{
    internal class WebQueryClientMock : IWebQueryClient
    {
        private readonly IEnumerable<IModel> _graph;

        public WebQueryClientMock(IEnumerable<IModel> graph)
        {
            _graph = graph;
        }

        public WebFormat WebFormat { get; set; }
        public ICredentials Credentials { get; set; }

        #region IWebQueryClient Members

        public WebResponse Response { get; private set; }

        public WebRequest Request { get; private set; }

        public WebCredentials WebCredentials { get; set; }


        public WebException Exception { get; set; }

        public string SourceUrl { get; set; }

        public bool UseCompression { get; set; }

        public TimeSpan? RequestTimeout { get; set; }

        public string ProxyValue { get; set; }

        public void SetWebProxy(WebRequest request)
        {
            // No-op
        }

        public WebRequest GetWebRequestShim(Uri address)
        {
            var request = new WebRequestMock(address, _graph.ToJson());
            Request = request;

            return request;
        }

        public WebResponse GetWebResponseShim(WebRequest request)
        {
            var response = new WebResponseMock(request.RequestUri, _graph.ToJson());
            Response = response;

            return response;
        }

        public WebResponse GetWebResponseShim(WebRequest request, IAsyncResult result)
        {
            var response = new WebResponseMock(request.RequestUri, _graph.ToJson());
            Response = response;

            return response;
        }

        public event OpenReadCompletedEventHandler OpenReadCompleted;

        public void OpenReadAsync(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void OpenReadAsync(Uri uri, object state)
        {
            throw new NotImplementedException();
        }

        public void CancelAsync()
        {
            throw new NotImplementedException();
        }

        public Stream OpenRead(string url)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(_graph.ToJson()));
        }

        public bool KeepAlive { get; set; }

        #endregion

        protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
        {
            if (OpenReadCompleted != null)
            {
                OpenReadCompleted(this, e);
            }
        }
    }
}