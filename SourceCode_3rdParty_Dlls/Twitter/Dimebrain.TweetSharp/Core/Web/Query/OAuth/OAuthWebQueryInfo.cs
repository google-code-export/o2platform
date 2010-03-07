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

using TweetSharp.Core.Attributes;
using TweetSharp.Core.OAuth;

namespace TweetSharp.Core.Web.Query.OAuth
{
    /// <summary>
    /// Mandatory and some optional oAuth parameters travelling with the request.
    /// <seealso cref="http://www.hueniverse.com/hueniverse/2008/10/beginners-gui-1.html"/>
    /// /// <seealso cref="http://tools.ietf.org/html/draft-dehora-farrell-oauth-accesstoken-creds-00#section-4"/>
    /// </summary>
    public class OAuthWebQueryInfo : IWebQueryInfo
    {
        [Parameter("oauth_consumer_key")]
        public string ConsumerKey { get; set; }

        [Parameter("oauth_token")]
        public string Token { get; set; }

        [Parameter("oauth_nonce")]
        public string Nonce { get; set; }

        [Parameter("oauth_timestamp")]
        public string Timestamp { get; set; }

        [Parameter("oauth_signature_method")]
        public string SignatureMethod { get; set; }

        [Parameter("oauth_signature")]
        public string Signature { get; set; }

        [Parameter("oauth_version")]
        public string Version { get; set; }

        // Optional parameters 

        [Parameter("oauth_callback")]
        public string Callback { get; set; }

        [Parameter("oauth_verifier")]
        public string Verifier { get; set; }

        [Parameter("x_auth_mode")]
        public string ClientMode { get; set; }

        [Parameter("x_auth_username")]
        public string ClientUsername { get; set; }

        [Parameter("x_auth_password")]
        public string ClientPassword { get; set; }

        [UserAgent]
        public string UserAgent { get; set; }

        public WebMethod WebMethod { get; set; }
        public OAuthParameterHandling ParameterHandling { get; set; }

        internal string ConsumerSecret { get; set; }
        internal string TokenSecret { get; set; }
    }
}