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
using System.Text;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.OAuth;

namespace TweetSharp.Core.Web.Query.OAuth
{
    internal class OAuthWebQueryClient : WebClient, IWebQueryClient
    {
        public string AuthorizationHeader { get; private set; }
        public string Realm { get; set; }

        public WebResponse Response { get; private set; }
        public WebRequest Request { get; private set; }
        public WebCredentials WebCredentials { get; set; }
        public WebException Exception { get; set; }
        public string SourceUrl { get; set; }

        public WebParameterCollection Parameters { get; private set; }
        protected OAuthParameterHandling ParameterHandling { get; private set; }

        private readonly IDictionary<string, string> _headers;
        public string UserAgent { get; private set; }
        public string Method { get; private set; }

        public OAuthWebQueryClient(IDictionary<string, string> headers, WebParameterCollection parameters,
                                   OAuthParameterHandling parameterHandling, string userAgent, string method)
        {
            _headers = headers;

            Parameters = parameters;
            ParameterHandling = parameterHandling;
            UserAgent = userAgent;
            Method = method;
        }

        public bool UseCompression { get; set; }
        public string ProxyValue { get; set; }
        public TimeSpan? RequestTimeout { get; set; }

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

        public WebRequest GetWebRequestShim(Uri address)
        {
            return GetWebRequest(address);
        }

#if !SILVERLIGHT
        public WebResponse GetWebResponseShim(WebRequest request)
        {
            return GetWebResponse(request);
        }
#endif

        public WebResponse GetWebResponseShim(WebRequest request, IAsyncResult result)
        {
            return GetWebResponse(request, result);
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            switch (ParameterHandling)
            {
                case OAuthParameterHandling.HttpAuthorizationHeader:
                    break;
                case OAuthParameterHandling.UrlOrPostParameters:
                    address = GetAddressWithOAuthParameters(address);
                    break;
            }

#if !Smartphone
            var request = (HttpWebRequest)base.GetWebRequest(address);
#else
            var request = (HttpWebRequest) WebRequest.Create(address);
#endif
            request.Method = Method;
            request.ContentType = "application/x-www-form-urlencoded";

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

            if (!UserAgent.IsNullOrBlank())
            {
#if !SILVERLIGHT
                request.UserAgent = UserAgent;
#else
                request.Headers["User-Agent"] = UserAgent;
#endif
            }

            if (UseCompression)
            {
#if !SILVERLIGHT
                request.AutomaticDecompression = DecompressionMethods.GZip;
#else
                request.Accept = "gzip,deflate";
#endif
            }

#if !SILVERLIGHT
            if (!ProxyValue.IsNullOrBlank())
            {
                SetWebProxy(request);
            }
#endif

#if !SILVERLIGHT
            if (RequestTimeout.HasValue)
            {
                request.Timeout = (int) RequestTimeout.Value.TotalMilliseconds;
            }
#endif

            switch (ParameterHandling)
            {
                case OAuthParameterHandling.HttpAuthorizationHeader:
                    SetAuthorizationHeader(request);
                    break;
                case OAuthParameterHandling.UrlOrPostParameters:
                    break;
            }

            if (KeepAlive)
            {
                request.KeepAlive = KeepAlive;
                if (RequestTimeout.HasValue)
                {
                    request.ReadWriteTimeout = (int) RequestTimeout.Value.TotalMilliseconds;
                }
            }

            Request = request;
            return request;
        }

        public bool KeepAlive { get; set; }

        private Uri GetAddressWithOAuthParameters(Uri address)
        {
            var sb = new StringBuilder("?");
            var parameters = 0;
            foreach (var parameter in Parameters)
            {
                if (parameter.Name.IsNullOrBlank() || parameter.Value.IsNullOrBlank())
                {
                    continue;
                }

                parameters++;
                var format = parameters < Parameters.Count ? "{0}={1}&" : "{0}={1}";
                sb.Append(format.FormatWith(parameter.Name, parameter.Value));
            }

            return new Uri(address + sb.ToString());
        }

        private void SetAuthorizationHeader(WebRequest request, string header)
        {
            var sb = new StringBuilder("OAuth ");
            if (!Realm.IsNullOrBlank())
            {
                sb.Append("realm=\"{0}\",".FormatWith(OAuthTools.UrlEncode(Realm)));
            }

            var parameters = 0;
            foreach (var parameter in Parameters)
            {
                if (parameter.Name.IsNullOrBlank() || parameter.Value.IsNullOrBlank())
                {
                    continue;
                }

                parameters++;
                var format = parameters < Parameters.Count ? "{0}=\"{1}\"," : "{0}=\"{1}\"";
                sb.Append(format.FormatWith(parameter.Name, parameter.Value));
            }

            var authorization = sb.ToString();
            AuthorizationHeader = authorization;

            request.Headers[header] = AuthorizationHeader;
        }

        // todo duplicated in OAuthWebQuery (for POSTS), find a better way
        private void SetAuthorizationHeader(WebRequest request)
        {
            SetAuthorizationHeader(request, "Authorization");
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
                return HandleWebException(ex);
            }
        }

        private static WebResponse HandleWebException(WebException ex)
        {
            return ex.Response;
        }
    }
}