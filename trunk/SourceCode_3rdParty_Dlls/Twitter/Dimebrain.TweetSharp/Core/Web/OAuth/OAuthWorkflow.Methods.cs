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
using TweetSharp.Core.Extensions;
using TweetSharp.Core.OAuth;
using TweetSharp.Core.Web.Query.OAuth;

#if(!SILVERLIGHT)
using System.Web;
#else

#endif

namespace TweetSharp.Core.Web.OAuth
{
    public partial class OAuthWorkflow
    {
        public string OAUTH_VERSION = "1.0";

        /// <summary>
        /// Generates a <see cref="OAuthWebQueryInfo"/> instance to pass to an
        /// <see cref="OAuthWebQuery" /> for the purpose of requesting an
        /// unauthorized request token.
        /// </summary>
        /// <param name="method">The HTTP method for the intended request</param>
        /// <seealso cref="http://oauth.net/core/1.0#anchor9"/>
        /// <returns></returns>
        public OAuthWebQueryInfo BuildRequestTokenInfo(WebMethod method)
        {
            return BuildRequestTokenInfo(method, null);
        }

        /// <summary>
        /// Generates a <see cref="OAuthWebQueryInfo"/> instance to pass to an
        /// <see cref="OAuthWebQuery" /> for the purpose of requesting an
        /// unauthorized request token.
        /// </summary>
        /// <param name="method">The HTTP method for the intended request</param>
        /// <param name="parameters">Any existing, non-OAuth query parameters desired in the request</param>
        /// <seealso cref="http://oauth.net/core/1.0#anchor9"/>
        /// <returns></returns>
        public OAuthWebQueryInfo BuildRequestTokenInfo(WebMethod method, WebParameterCollection parameters)
        {
            ValidateTokenRequestState();

            if (parameters == null)
            {
                parameters = new WebParameterCollection();
            }

            var timestamp = OAuthTools.GetTimestamp();
            var nonce = OAuthTools.GetNonce();

            AddAuthParameters(parameters, timestamp, nonce);

            var signatureBase = OAuthTools.ConcatenateRequestElements(method, RequestTokenUrl, parameters);
            var signature = OAuthTools.GetSignature(SignatureMethod, signatureBase, ConsumerSecret);

            var info = new OAuthWebQueryInfo
                           {
                               WebMethod = method,
                               ParameterHandling = ParameterHandling,
                               ConsumerKey = ConsumerKey,
                               SignatureMethod = SignatureMethod.ToRequestValue(),
                               Signature = signature,
                               Timestamp = timestamp,
                               Nonce = nonce,
                               Version = OAUTH_VERSION,
                               Callback = OAuthTools.UrlEncode(CallbackUrl ?? ""),
                               UserAgent = "TweetSharp",
                               TokenSecret = TokenSecret,
                               ConsumerSecret = ConsumerSecret
                           };

            return info;
        }

        /// <summary>
        /// Generates a <see cref="OAuthWebQueryInfo"/> instance to pass to an
        /// <see cref="OAuthWebQuery" /> for the purpose of exchanging a request token
        /// for an access token authorized by the user at the Service Provider site.
        /// </summary>
        /// <param name="method">The HTTP method for the intended request</param>
        /// <seealso cref="http://oauth.net/core/1.0#anchor9"/>
        public OAuthWebQueryInfo BuildAccessTokenInfo(WebMethod method)
        {
            return BuildAccessTokenInfo(method, null);
        }

        /// <summary>
        /// Generates a <see cref="OAuthWebQueryInfo"/> instance to pass to an
        /// <see cref="OAuthWebQuery" /> for the purpose of exchanging a request token
        /// for an access token authorized by the user at the Service Provider site.
        /// </summary>
        /// <param name="method">The HTTP method for the intended request</param>
        /// <seealso cref="http://oauth.net/core/1.0#anchor9"/>
        /// <param name="parameters">Any existing, non-OAuth query parameters desired in the request</param>
        public OAuthWebQueryInfo BuildAccessTokenInfo(WebMethod method, WebParameterCollection parameters)
        {
            ValidateAccessRequestState();

            if (parameters == null)
            {
                parameters = new WebParameterCollection();
            }

            var uri = new Uri(AccessTokenUrl);
            var timestamp = OAuthTools.GetTimestamp();
            var nonce = OAuthTools.GetNonce();

            AddAuthParameters(parameters, timestamp, nonce);

            var signatureBase = OAuthTools.ConcatenateRequestElements(method, uri.ToString(), parameters);
            var signature = OAuthTools.GetSignature(SignatureMethod, signatureBase, ConsumerSecret);

            var info = new OAuthWebQueryInfo
                           {
                               WebMethod = method,
                               ParameterHandling = ParameterHandling,
                               ConsumerKey = ConsumerKey,
                               Token = Token,
                               SignatureMethod = SignatureMethod.ToRequestValue(),
                               Signature = signature,
                               Timestamp = timestamp,
                               Nonce = nonce,
                               Version = OAUTH_VERSION,
                               Callback = CallbackUrl,
                               UserAgent = "TweetSharp",
                               TokenSecret = TokenSecret,
                               ConsumerSecret = ConsumerSecret
                           };

            return info;
        }

        /// <summary>
        /// Generates a <see cref="OAuthWebQueryInfo"/> instance to pass to an
        /// <see cref="OAuthWebQuery" /> for the purpose of exchanging user credentials
        /// for an access token authorized by the user at the Service Provider site.
        /// </summary>
        /// <param name="method">The HTTP method for the intended request</param>
        /// <seealso cref="http://tools.ietf.org/html/draft-dehora-farrell-oauth-accesstoken-creds-00#section-4"/>
        /// <param name="parameters">Any existing, non-OAuth query parameters desired in the request</param>
        public OAuthWebQueryInfo BuildClientAuthAccessTokenInfo(WebMethod method, WebParameterCollection parameters)
        {
            ValidateClientAuthAccessRequestState();

            if (parameters == null)
            {
                parameters = new WebParameterCollection();
            }

            var uri = new Uri(AccessTokenUrl);
            var timestamp = OAuthTools.GetTimestamp();
            var nonce = OAuthTools.GetNonce();

            AddClientAuthParameters(parameters, timestamp, nonce);

            var signatureBase = OAuthTools.ConcatenateRequestElements(method, uri.ToString(), parameters);
            var signature = OAuthTools.GetSignature(SignatureMethod, signatureBase, ConsumerSecret);

            var info = new OAuthWebQueryInfo
                           {
                               WebMethod = method,
                               ParameterHandling = ParameterHandling,
                               ClientMode = "client_auth",
                               ClientUsername = ClientUsername,
                               ClientPassword = ClientPassword,
                               ConsumerKey = ConsumerKey,
                               SignatureMethod = SignatureMethod.ToRequestValue(),
                               Signature = signature,
                               Timestamp = timestamp,
                               Nonce = nonce,
                               Version = OAUTH_VERSION,
                               UserAgent = "TweetSharp",
                               TokenSecret = TokenSecret,
                               ConsumerSecret = ConsumerSecret
                           };

            return info;
        }

        public OAuthWebQueryInfo BuildProtectedResourceInfo(WebMethod method, string url)
        {
            return BuildProtectedResourceInfo(method, null, url);
        }

        public OAuthWebQueryInfo BuildProtectedResourceInfo(WebMethod method, WebParameterCollection parameters,
                                                            string url)
        {
            ValidateProtectedResourceState();

            if (parameters == null)
            {
                parameters = new WebParameterCollection();
            }

            // Include url parameters in query pool
            var uri = new Uri(url);
#if !SILVERLIGHT
            var urlParameters = HttpUtility.ParseQueryString(uri.Query);
#else
            var urlParameters = uri.Query.ParseQueryString();
#endif

#if !SILVERLIGHT
            foreach (var parameter in urlParameters.AllKeys)
#else
            // todo can we just use keys instead of allkeys?
            foreach (var parameter in urlParameters.Keys)
#endif
            {
                switch (method)
                {
                    case WebMethod.Post:
                        parameters.Add(new HttpPostParameter(parameter, urlParameters[parameter]));
                        break;
                    default:
                        parameters.Add(parameter, urlParameters[parameter]);
                        break;
                }
            }

            var timestamp = OAuthTools.GetTimestamp();
            var nonce = OAuthTools.GetNonce();

            AddAuthParameters(parameters, timestamp, nonce);

            var signatureBase = OAuthTools.ConcatenateRequestElements(method, url, parameters);
            var signature = OAuthTools.GetSignature(SignatureMethod, signatureBase, ConsumerSecret, TokenSecret);

            var info = new OAuthWebQueryInfo
                           {
                               WebMethod = method,
                               ParameterHandling = ParameterHandling,
                               ConsumerKey = ConsumerKey,
                               Token = Token,
                               SignatureMethod = SignatureMethod.ToRequestValue(),
                               Signature = signature,
                               Timestamp = timestamp,
                               Nonce = nonce,
                               Version = OAUTH_VERSION,
                               UserAgent = "TweetSharp",
                               ConsumerSecret = ConsumerSecret,
                               TokenSecret = TokenSecret
                           };

            return info;
        }

        private void ValidateTokenRequestState()
        {
            if (RequestTokenUrl.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a request token URL");
            }

            if (ConsumerKey.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer key");
            }

            if (ConsumerSecret.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer secret");
            }
        }

        private void ValidateAccessRequestState()
        {
            if (AccessTokenUrl.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify an access token URL");
            }

            if (ConsumerKey.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer key");
            }

            if (ConsumerSecret.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer secret");
            }

            if (Token.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a token");
            }
        }

        private void ValidateClientAuthAccessRequestState()
        {
            if (AccessTokenUrl.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify an access token URL");
            }

            if (ConsumerKey.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer key");
            }

            if (ConsumerSecret.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer secret");
            }

            if (ClientUsername.IsNullOrBlank() || ClientPassword.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify user credentials");
            }
        }

        private void ValidateProtectedResourceState()
        {
            if (ConsumerKey.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer key");
            }

            if (ConsumerSecret.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a consumer secret");
            }

            if (Token.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a token");
            }

            if (TokenSecret.IsNullOrBlank())
            {
                throw new ArgumentException("You must specify a token secret");
            }
        }

        private void AddAuthParameters(ICollection<WebParameter> parameters, string timestamp, string nonce)
        {
            var authParameters = new WebParameterCollection
                                     {
                                         new WebParameter("oauth_consumer_key", ConsumerKey),
                                         new WebParameter("oauth_nonce", nonce),
                                         new WebParameter("oauth_signature_method", SignatureMethod.ToRequestValue()),
                                         new WebParameter("oauth_timestamp", timestamp),
                                         new WebParameter("oauth_version", OAUTH_VERSION)
                                     };

            if (!Token.IsNullOrBlank())
            {
                authParameters.Add(new WebParameter("oauth_token", Token));
            }

            if (!CallbackUrl.IsNullOrBlank())
            {
                authParameters.Add(new WebParameter("oauth_callback", CallbackUrl));
            }

            if (!Verifier.IsNullOrBlank())
            {
                authParameters.Add(new WebParameter("oauth_verifier", Verifier));
            }

            foreach (var authParameter in authParameters)
            {
                parameters.Add(authParameter);
            }
        }

        private void AddClientAuthParameters(ICollection<WebParameter> parameters, string timestamp, string nonce)
        {
            var authParameters = new WebParameterCollection
                                     {
                                         new WebParameter("x_auth_username", ClientUsername),
                                         new WebParameter("x_auth_password", ClientPassword),
                                         new WebParameter("x_auth_mode", "client_auth"),
                                         new WebParameter("oauth_consumer_key", ConsumerKey),
                                         new WebParameter("oauth_signature_method", SignatureMethod.ToRequestValue()),
                                         new WebParameter("oauth_timestamp", timestamp),
                                         new WebParameter("oauth_nonce", nonce),
                                         new WebParameter("oauth_version", OAUTH_VERSION)
                                     };

            foreach (var authParameter in authParameters)
            {
                parameters.Add(authParameter);
            }
        }
    }
}