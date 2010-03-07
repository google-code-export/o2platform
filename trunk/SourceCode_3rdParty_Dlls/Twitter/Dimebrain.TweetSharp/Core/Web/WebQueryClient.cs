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
using System.Linq;
using System.Net;
using TweetSharp.Core.Extensions;
#if Smartphone
#endif

namespace TweetSharp.Core.Web
{
    /// <summary>
    /// A web query instance that tracks its <see cref="WebRequest"/>
    /// and <see cref="WebResponse" /> pair in order to inform a consumer.
    /// </summary>
    public class WebQueryClient :
        WebClient,
        IWebQueryClient
    {
        public WebResponse Response { get; private set; }
        public WebRequest Request { get; private set; }
        public WebCredentials WebCredentials { get; set; }

        public bool UseCompression { get; set; }
        public TimeSpan? RequestTimeout { get; set; }
        public string ProxyValue { get; set; }
        public string SourceUrl { get; set; }

        private readonly IDictionary<string, string> _headers;
        public WebParameterCollection Parameters { get; private set; }
        public string UserAgent { get; private set; }
        public string Method { get; private set; }
        public WebException Exception { get; set; }

        public WebQueryClient(IDictionary<string, string> headers, WebParameterCollection parameters, string userAgent,
                              string method)
        {
            _headers = headers;
            Parameters = parameters;
            UserAgent = userAgent;
            Method = method;
        }

        // todo find a way to merge this with other implementations to avoid duplication
        public void SetWebProxy(WebRequest request)
        {
#if !Smartphone
            var proxyUriBuilder = new UriBuilder(ProxyValue);
            request.Proxy = new WebProxy(proxyUriBuilder.Host,
                                         proxyUriBuilder.Port);

            if (!proxyUriBuilder.UserName.IsNullOrBlank())
            {
                request.Headers["Proxy-Authorization"] = WebExtensions.ToAuthorizationHeader(proxyUriBuilder.UserName,
                                                                                             proxyUriBuilder.Password);
            }
#else
            var uri = new Uri(ProxyValue);
            request.Proxy = new WebProxy(uri.Host, uri.Port);
            var userParts = uri.UserInfo.Split(new[] {':'}).Where(ui => !ui.IsNullOrBlank()).ToArray();
            if (userParts.Length == 2)
            {
                request.Proxy.Credentials = new NetworkCredential(userParts[0], userParts[1]);
            }
#endif
        }

        /// <summary>
        /// Gets the web request shim.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public WebRequest GetWebRequestShim(Uri address)
        {
            return GetWebRequest(address);
        }

#if !SILVERLIGHT
        /// <summary>
        /// Gets the web response shim.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public WebResponse GetWebResponseShim(WebRequest request)
        {
            return GetWebResponse(request);
        }

        public bool KeepAlive { get; set; }
#endif

        /// <summary>
        /// Gets the web response shim.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public WebResponse GetWebResponseShim(WebRequest request, IAsyncResult result)
        {
            return GetWebResponse(request, result);
        }

        /// <summary>
        /// Returns a <see cref="T:System.Net.WebRequest"/> object for the specified resource.
        /// </summary>
        /// <param name="address">A <see cref="T:System.Uri"/> that identifies the resource to request.</param>
        /// <returns>
        /// A new <see cref="T:System.Net.WebRequest"/> object for the specified resource.
        /// </returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
#if !Smartphone
            var request = (HttpWebRequest) base.GetWebRequest(address);
#else
            var request = (HttpWebRequest) WebRequest.Create(address);
#endif
            if (_headers != null)
            {
                foreach (var header in _headers)
                {
#if !SILVERLIGHT
                    request.Headers.Add(header.Key, header.Value);
#else
                    request.Headers[header.Key] = header.Value;
#endif
                }
            }

            if (UseCompression)
            {
#if !SILVERLIGHT
                request.AutomaticDecompression = DecompressionMethods.GZip;
#else
                request.Accept = "gzip,deflate";
#endif
            }

            if (WebCredentials != null)
            {
                // NetworkCredentials always makes two trips, even if with PreAuthenticate,
                // it is also unsafe for many partial trust scenarios
                // request.Credentials = Credentials;
                var credentials = WebCredentials;
                request.Headers["Authorization"] = WebExtensions.ToAuthorizationHeader(credentials.Username,
                                                                                       credentials.Password);
#if !SILVERLIGHT
                // todo consider removing if we're already setting auth explicitly
                request.PreAuthenticate = true;
#endif
            }

            if (Parameters != null)
            {
                var hasParameters = address.Query.Contains("?");
                foreach (var parameter in Parameters)
                {
                    address.Query.Then(hasParameters ? "&" : "?");
                    address.Query.Then("{0}={1}".FormatWith(parameter.Name, parameter.Value));
                    hasParameters = true;
                }
            }

#if !SILVERLIGHT
            if (!ProxyValue.IsNullOrBlank())
            {
                SetWebProxy(request);
            }
#endif

            if (!UserAgent.IsNullOrBlank())
            {
#if !SILVERLIGHT
                request.UserAgent = UserAgent;
#else
                request.Headers["User-Agent"] = UserAgent;
#endif
            }
            request.Method = Method;
            Request = request;
            if (RequestTimeout.HasValue)
            {
                request.Timeout = (int) RequestTimeout.Value.TotalMilliseconds;
            }

            if (KeepAlive)
            {
                request.KeepAlive = KeepAlive;
                if (RequestTimeout.HasValue)
                {
                    request.ReadWriteTimeout = (int) RequestTimeout.Value.TotalMilliseconds;
                }
            }
            return request;
        }

#if !SILVERLIGHT
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            try
            {
#if !Smartphone
                var response = base.GetWebResponse(request);
#else
                var response = request.GetResponse();
#endif
                Response = response;
                return response;
            }
            catch (WebException ex)
            {
                Exception = ex;
                return HandleWebException(ex);
            }
        }
#endif

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            try
            {
#if !Smartphone
                var response = base.GetWebResponse(request, result);
#else
                var response = request.GetResponse();
#endif
                Response = response;
                return response;
            }
            catch (WebException ex)
            {
                Exception = ex;
                return HandleWebException(ex);
            }
        }

        private WebResponse HandleWebException(WebException ex)
        {
            Response = ex.Response;
            return ex.Response;
        }
    }
}