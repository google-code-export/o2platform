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


#if !SILVERLIGHT
using System;
using System.Diagnostics;
#endif

namespace TweetSharp.Fluent
{
    /// <summary>
    /// Extension methods for Authentication
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the request token.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="callbackUrl">The callback URL, which overrides the URL set via Twitter.</param>
        /// <returns></returns>
        public static IFluentTwitter GetRequestToken(this IFluentTwitterAuthentication instance, string consumerKey,
                                                     string consumerSecret, string callbackUrl)
        {
            return BuildRequestToken(instance, consumerKey, consumerSecret, callbackUrl);
        }

        /// <summary>
        /// Gets the request token.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <returns></returns>
        public static IFluentTwitter GetRequestToken(this IFluentTwitterAuthentication instance, string consumerKey,
                                                     string consumerSecret)
        {
            return BuildRequestToken(instance, consumerKey, consumerSecret, string.Empty);
        }

        /// <summary>
        /// Gets the request token.
        /// </summary>
        /// <param name="instance">The fluent twitter instance.</param>
        /// <returns></returns>
        public static IFluentTwitter GetRequestToken(this IFluentTwitterAuthentication instance)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            return BuildRequestToken(instance, consumerKey, consumerSecret, string.Empty);
        }

        private static IFluentTwitter BuildRequestToken(IFluentTwitterAuthentication instance, string consumerKey,
                                                        string consumerSecret, string callbackUrl)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "request_token",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret,
                                             Callback = callbackUrl
                                         };
            return instance.Root;
        }

#if !SILVERLIGHT
    /// <summary>
    /// Authorizes the desktop by shelling out to the browser
    /// </summary>
    /// <param name="instance">The fluent twitter instance.</param>
    /// <param name="token">The token.</param>
    /// <returns>The fluent twitter instance.</returns>
        public static IFluentTwitter AuthorizeDesktop(this IFluentTwitterAuthentication instance, string token)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authorize_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authorize";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }

        public static IFluentTwitter AuthorizeDesktop(this IFluentTwitterAuthentication instance, string token, string callback)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authorize_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token,
                Callback = callback
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authorize";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }
        
        /// <summary>
        /// Authorizes the desktop by shelling out to the browser
        /// </summary>
        /// <param name="instance">The fluent twitter instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token.</param>
        /// <returns>The fluent twitter instance.</returns>
        public static IFluentTwitter AuthorizeDesktop(this IFluentTwitterAuthentication instance, string consumerKey, string consumerSecret, string token)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authorize_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authorize";
            Process.Start(instance.Root.AsUrl(), "");
            
            return instance.Root;
        }

        /// <summary>
        /// Authorizes the desktop by shelling out to the browser
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The token.</param>
        /// <param name="callback">The callback url.</param>
        /// <returns>The FluentTwitter instance</returns>
        public static IFluentTwitter AuthorizeDesktop(this IFluentTwitterAuthentication instance, string consumerKey, string consumerSecret, string token, string callback)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authorize_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token,
                Callback = callback
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authorize";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }

        public static IFluentTwitter AuthenticateDesktop(this IFluentTwitterAuthentication instance, string token)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authenticate_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authenticate";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }

        public static IFluentTwitter AuthenticateDesktop(this IFluentTwitterAuthentication instance, string token, string callback)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authenticate_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token,
                Callback = callback
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authenticate";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }

        public static IFluentTwitter AuthenticateDesktop(this IFluentTwitterAuthentication instance, string consumerKey, string consumerSecret, string token)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authenticate_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authenticate";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }

        public static IFluentTwitter AuthenticateDesktop(this IFluentTwitterAuthentication instance, string consumerKey, string consumerSecret, string token, string callback)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
            {
                Action = "authenticate_desktop",
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = token,
                Callback = callback
            };

            // start browser out of band
            ((IFluentBaseOAuth)instance.Authenticator).Action = "authenticate";
            Process.Start(instance.Root.AsUrl(), "");

            return instance.Root;
        }
#endif

        /// <summary>
        /// Requests the access token.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The request token.</param>
        /// <returns>The FluentTwitter instance.</returns>
        public static IFluentTwitter GetAccessToken(this IFluentTwitterAuthentication instance, string consumerKey,
                                                    string consumerSecret, string token)
        {
            return BuildAccessToken(instance, consumerKey, consumerSecret, token, "");
        }

        /// <summary>
        /// Requests the access token.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="token">The request token.</param>
        /// <param name="verifier">The PIN.</param>
        /// <returns>The FluentTwitter instance.</returns>
        public static IFluentTwitter GetAccessToken(this IFluentTwitterAuthentication instance, string consumerKey,
                                                    string consumerSecret, string token, string verifier)
        {
            return BuildAccessToken(instance, consumerKey, consumerSecret, token, verifier);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="token">The request token.</param>
        /// <returns>The FluentTwitter instance</returns>
        public static IFluentTwitter GetAccessToken(this IFluentTwitterAuthentication instance, string token)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            return BuildAccessToken(instance, consumerKey, consumerSecret, token, "");
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="token">The request token.</param>
        /// <param name="verifier">The verifier (PIN).</param>
        /// <returns>The FluentTwitter instance</returns>
        public static IFluentTwitter GetAccessToken(this IFluentTwitterAuthentication instance, string token,
                                                    string verifier)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            return BuildAccessToken(instance, consumerKey, consumerSecret, token, verifier);
        }

        /// <summary>
        /// Requests the access token using client authentication, which does not require
        /// a browser.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The FluentTwitter instance.</returns>
        public static IFluentTwitter GetClientAuthAccessToken(this IFluentTwitterAuthentication instance,
                                                              string consumerKey, string consumerSecret, string username,
                                                              string password)
        {
            return BuildClientAuthAccessToken(instance, consumerKey, consumerSecret, username, password);
        }

        /// <summary>
        /// Requests the access token using client authentication, which does not require
        /// a browser.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The FluentTwitter instance.</returns>
        public static IFluentTwitter GetClientAuthAccessToken(this IFluentTwitterAuthentication instance,
                                                              string username, string password)
        {
            instance.Root.ValidateConsumerCredentials();
            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            return BuildClientAuthAccessToken(instance, consumerKey, consumerSecret, username, password);
        }

        /// <summary>
        /// Gets the authorization URL used in the OAuth process.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="token">The request token.</param>
        /// <returns>The authorization url</returns>
        public static string GetAuthorizationUrl(this IFluentTwitterAuthentication instance, string token)
        {
            return BuildAuthorizationUrl(instance, token);
        }

        public static string GetAuthorizationUrl(this IFluentTwitterAuthentication instance, string token,
                                                 string callback)
        {
            return BuildAuthorizationUrl(instance, token, callback);
        }

        /// <summary>
        /// Gets the authorization URL used in the OAuth process.
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="token">The request token.</param>
        /// <returns>the authorization url</returns>
        public static string GetAuthenticationUrl(this IFluentTwitterAuthentication instance, string token)
        {
            return BuildAuthenticationUrl(instance, token);
        }

        public static string GetAuthenticationUrl(this IFluentTwitterAuthentication instance, string token,
                                                  string callback)
        {
            return BuildAuthenticationUrl(instance, token, callback);
        }

        /// <summary>
        /// Sets the OAuth verifier (PIN) provided by the service to the user to enter manually.
        /// This method is used during desktop authentication, to set the verifier after the fact,
        /// since it cannot be guessed and is never provided by Twitter.
        /// </summary>
        /// <param name="instance">The fluent twitter instance.</param>
        /// <param name="verifier">The PIN obtained from the user who authorized the application.</param>
        /// <returns></returns>
        public static IFluentTwitter SetVerifier(this IFluentTwitterAuthentication instance, string verifier)
        {
            var fluentTwitterOAuth = instance.Authenticator as FluentBaseOAuth;
            if (fluentTwitterOAuth != null)
            {
                fluentTwitterOAuth.Verifier = verifier;
            }
            return instance.Root;
        }

        private static string BuildAuthorizationUrl(IFluentTwitterAuthentication instance, string token)
        {
            instance.Root.Authentication.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "authorize",
                                             Token = token,
                                         };

            var url = instance.Root.AsUrl();
            return url;
        }

        private static string BuildAuthorizationUrl(IFluentTwitterAuthentication instance, string token, string callback)
        {
            instance.Root.Authentication.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "authorize",
                                             Token = token,
                                             Callback = callback
                                         };

            var url = instance.Root.AsUrl();
            return url;
        }

        private static string BuildAuthenticationUrl(IFluentTwitterAuthentication instance, string token)
        {
            instance.Root.Authentication.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "authenticate",
                                             Token = token
                                         };

            var url = instance.Root.AsUrl();
            return url;
        }

        private static string BuildAuthenticationUrl(IFluentTwitterAuthentication instance, string token,
                                                     string callback)
        {
            instance.Root.Authentication.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "authenticate",
                                             Token = token,
                                             Callback = callback
                                         };

            var url = instance.Root.AsUrl();
            return url;
        }

        private static IFluentTwitter BuildAccessToken(IFluentTwitterAuthentication instance, string consumerKey,
                                                       string consumerSecret, string token, string verifier)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "access_token",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret,
                                             Token = token,
                                             Verifier = verifier
                                         };
            return instance.Root;
        }

        private static IFluentTwitter BuildClientAuthAccessToken(IFluentTwitterAuthentication instance,
                                                                 string consumerKey, string consumerSecret,
                                                                 string username, string password)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "client_auth",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret,
                                             ClientUsername = username,
                                             ClientPassword = password
                                         };
            return instance.Root;
        }
    }
}