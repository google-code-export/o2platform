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
using System.Net;
using System.Text;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.OAuth;
using TweetSharp.Core.Web.OAuth;
#if !SILVERLIGHT
using System.Web;
#endif

namespace TweetSharp.Core.Web.Query.OAuth
{
    /// <summary>
    /// A web query engine for OAuth requests.
    /// </summary>
    public class OAuthWebQuery : WebQueryBase
    {
        /// <summary>
        /// Gets or sets the HTTP Realm.
        /// </summary>
        /// <value>The realm.</value>
        public string Realm { get; set; }

        /// <summary>
        /// Gets or sets the parameter handling.
        /// </summary>
        /// <value>The parameter handling.</value>
        public OAuthParameterHandling ParameterHandling { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthWebQuery"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        public OAuthWebQuery(OAuthWebQueryInfo info)
            : base(info)
        {
            Method = info.WebMethod;
            ParameterHandling = info.ParameterHandling;
        }

        // todo duplicated in OAuthWebQueryClient, consolidate
        protected override WebRequest BuildPostOrPutWebRequest(PostOrPut method, string url, out byte[] content)
        {
            // remove POST parameters from query
            var uri = url.AsUri();
            url = uri.Scheme.Then("://")
#if !SILVERLIGHT
                .Then(uri.Authority)
#else
                .Then(uri.Host)
#endif
                ;

            if (uri.Port != 80)
            {
                url = url.Then(":" + uri.Port);
            }
            url = url.Then(uri.AbsolutePath);

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = method == PostOrPut.Post ? "POST" : "PUT";
            request.ContentType = "application/x-www-form-urlencoded";

#if !SILVERLIGHT
            if (!Proxy.IsNullOrBlank())
            {
                SetWebProxy(request);
            }
#endif
            foreach (var header in Headers)
            {
#if !SILVERLIGHT
                request.Headers.Add(header.Key, header.Value);
#else
                request.Headers[header.Key] = header.Value;
#endif
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
            if (RequestTimeout.HasValue)
            {
                request.Timeout = (int)RequestTimeout.Value.TotalMilliseconds;
            }

            if (KeepAlive)
            {
                request.KeepAlive = true;
            }
#endif
            var body = "";
            switch (ParameterHandling)
            {
                case OAuthParameterHandling.HttpAuthorizationHeader:
                    SetAuthorizationHeader(request, "Authorization");
                    break;
                case OAuthParameterHandling.UrlOrPostParameters:
                    body = GetPostParametersValue(Parameters, false);
                    break;
            }

            // Only use the POST parameters that exist in the body
#if SILVERLIGHT
            var postParameters = new WebParameterCollection(uri.Query.ParseQueryString());
#else
            var postParameters = new WebParameterCollection(HttpUtility.ParseQueryString(uri.Query));
#endif
            // Append any leftover values to the POST body
            var nonAuthParameters = GetPostParametersValue(postParameters, true);
            if (body.IsNullOrBlank())
            {
                body = nonAuthParameters;
            }
            else
            {
                if (!nonAuthParameters.IsNullOrBlank())
                {
                    body += "&".Then(nonAuthParameters);
                }
            }

            content = Encoding.UTF8.GetBytes(body);
#if !SILVERLIGHT
            request.ContentLength = content.Length;
            // Silverlight sets this dynamically
#endif
            return request;
        }

        private static string GetPostParametersValue(ICollection<WebParameter> postParameters, bool escapeParameters)
        {
            var body = "";
            var parameters = 0;
            foreach (var postParameter in postParameters)
            {
                // client_auth method does not function when these are escaped
                var name = escapeParameters
                               ? OAuthTools.UrlEncode(postParameter.Name)
                               : postParameter.Name;
                var value = escapeParameters
                                ? OAuthTools.UrlEncode(postParameter.Value)
                                : postParameter.Value;

                body = body.Then("{0}={1}".FormatWith(name, value));

                if (parameters < postParameters.Count - 1)
                {
                    body = body.Then("&");
                }

                parameters++;
            }
            return body;
        }

        protected override WebRequest BuildDeleteWebRequest(string url)
        {
            url = AppendParameters(url);

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "DELETE";

#if !SILVERLIGHT
            if (!Proxy.IsNullOrBlank())
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

            SetAuthorizationHeader(request, "Authorization");
            AppendHeaders(request);
            return request;
        }

        protected override void SetAuthorizationHeader(WebRequest request, string header)
        {
            var authorization = GetAuthorizationHeader();
            AuthorizationHeader = authorization;

#if !SILVERLIGHT
            request.Headers["Authorization"] = AuthorizationHeader;
#else
            request.Headers["X-Twitter-Auth"] = AuthorizationHeader;
#endif
        }

        private string GetAuthorizationHeader()
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
            return authorization;
        }

        protected override HttpWebRequest BuildMultiPartFormRequest(PostOrPut method, string url,
                                                                    IEnumerable<HttpPostParameter> parameters,
                                                                    out byte[] bytes)
        {
            var boundary = Guid.NewGuid().ToString();
            var request = (HttpWebRequest) WebRequest.Create(url);

#if !SILVERLIGHT
    // todo we can probably remove these anyway
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
#endif
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            request.Method = method == PostOrPut.Post ? "POST" : "PUT";

            SetAuthorizationHeader(request, "Authorization");

            var contents = BuildMultiPartFormRequestParameters(boundary, parameters);
            var payload = contents.ToString();

#if !Smartphone
            bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(payload);
#else
            bytes = Encoding.GetEncoding(1252).GetBytes(payload);
#endif

#if !SILVERLIGHT
            request.ContentLength = bytes.Length;
#endif
            return request;
        }

#if !SILVERLIGHT
        public override string Request(string url, IEnumerable<HttpPostParameter> parameters)
        {
            RecalculateSignature(url);
            return base.Request(url, parameters);
        }

        public override string Request(string url, out WebException exception)
        {
            RecalculateSignature(url); 
            return base.Request(url, out exception);
        }

#endif

        private void RecalculateSignature(string url)
        {
            var info = (OAuthWebQueryInfo) Info;
            if (!string.IsNullOrEmpty(info.Token) && !string.IsNullOrEmpty(info.TokenSecret))
            {
                var oauth = new OAuthWorkflow
                                {
                                    ConsumerKey = info.ConsumerKey,
                                    ConsumerSecret = info.ConsumerSecret,
                                    Token = info.Token,
                                    TokenSecret = info.TokenSecret,
                                    ClientUsername = info.ClientUsername,
                                    ClientPassword = info.ClientPassword,
                                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                                    CallbackUrl = info.Callback,
                                    Verifier = info.Verifier
                                };

                var parameters = new WebParameterCollection();
                Info = oauth.BuildProtectedResourceInfo(Method, parameters, url);
                Parameters = BuildRequestParameters();
            }
        }
    }
}