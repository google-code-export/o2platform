/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * Author: Andrew Smith <andy@snae.net> 22/11/08
 * 
*/

using System;
using System.Text;

namespace Google.GData.Client
{
    /// <summary>
    /// Provides a means to generate an OAuth signature suitable for use
    /// with Google two-legged OAuth requests.
    /// </summary>
    public class OAuthUtil : OAuthBase
    {
        /// <summary>
        /// Generate the timestamp for the signature        
        /// </summary>
        /// <returns></returns>
        public override string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string timeStamp = ts.TotalSeconds.ToString();
            // remove any fractions of seconds
            int pointIndex = timeStamp.IndexOf(".");
            if (pointIndex != -1)
                timeStamp = timeStamp.Substring(0, pointIndex);
            return timeStamp;
        }

        /// <summary>
        /// Generate a nonce
        /// </summary>
        /// <returns>A nonce suitable for Google's two-legged OAuth implementation</returns>
        public override string GenerateNonce()
        {
            // changed from the original oauth code to use Guid
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }

        /// <summary>
        /// Generates an OAuth header.
        /// </summary>
        /// <param name="uri">The URI of the request</param>
        /// <param name="consumerKey">The consumer key</param>
        /// <param name="consumerSecret">The consumer secret</param>
        /// <param name="httpMethod">The http method</param>
        /// <returns>The OAuth authorization header</returns>
        public static string GenerateHeader(Uri uri, String consumerKey, String consumerSecret, String httpMethod)
        {
            return GenerateHeader(uri, consumerKey, consumerSecret, string.Empty, string.Empty, httpMethod);
        }

        public static string GenerateHeader(Uri uri, String consumerKey, String consumerSecret, String token, String tokenSecret, String httpMethod)
        {
            OAuthUtil oauthUtil = new OAuthUtil();
            string timeStamp = oauthUtil.GenerateTimeStamp();
            string nonce = oauthUtil.GenerateNonce();
            string normalizedUrl; string normalizedRequestParameters;

  
            string signature = oauthUtil.GenerateSignature(uri, consumerKey, consumerSecret, token, tokenSecret,
                httpMethod.ToUpper(), timeStamp, nonce, out normalizedUrl, out normalizedRequestParameters);
           
            signature = System.Web.HttpUtility.UrlEncode(signature);
            
            StringBuilder sb = new StringBuilder();
            sb.Append("Authorization: OAuth realm=\"\",oauth_version=\"1.0\",");
            sb.AppendFormat("oauth_nonce=\"{0}\",", nonce);
            sb.AppendFormat("oauth_timestamp=\"{0}\",", timeStamp);
            sb.AppendFormat("oauth_consumer_key=\"{0}\",", consumerKey);
            if (!String.IsNullOrEmpty(token))
            {
                token = System.Web.HttpUtility.UrlEncode(token);
                sb.AppendFormat("oauth_token=\"{0}\",", token);
            }
            sb.Append("oauth_signature_method=\"HMAC-SHA1\",");
            sb.AppendFormat("oauth_signature=\"{0}\"", signature);

            return sb.ToString();
        }

    }
}
