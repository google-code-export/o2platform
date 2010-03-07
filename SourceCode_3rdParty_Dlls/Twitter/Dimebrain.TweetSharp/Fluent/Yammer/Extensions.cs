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

#if !Smartphone && !SILVERLIGHT
using System.Diagnostics;
using TweetSharp.Core.Web;
using TweetSharp.Fluent.Yammer;
#endif

namespace TweetSharp.Fluent
{
    public static partial class Extensions
    {
        public static IFluentYammer GetRequestToken(this IFluentYammerAuthentication instance, string consumerKey,
                                                    string consumerSecret)
        {
            return BuildRequestToken(instance, consumerKey, consumerSecret);
        }

        public static IFluentYammer GetRequestToken(this IFluentYammerAuthentication instance)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            return BuildRequestToken(instance, consumerKey, consumerSecret);
        }

        private static IFluentYammer BuildRequestToken(IFluentYammerAuthentication instance, string consumerKey,
                                                       string consumerSecret)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Mode = AuthenticationMode.OAuth;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "request_token",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret
                                         };
            return instance.Root;
        }

#if !Smartphone && !SILVERLIGHT
        public static IFluentYammer AuthorizeDesktop(this IFluentYammerAuthentication instance, string consumerKey,
                                                     string consumerSecret, string token, string tokenSecret)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Root.Method = WebMethod.Post;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "authorize_desktop",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret,
                                             Token = token,
                                             TokenSecret = tokenSecret
                                         };

            // start browser out of band
            ((IFluentBaseOAuth) instance.Authenticator).Action = "authorize";
            Process.Start(instance.Root.AsUrl());

            return instance.Root;
        }

        /// <summary>
        /// Sets the PIN provided by the service to the user
        /// </summary>
        /// <param name="instance">The fluent twitter instance.</param>
        /// <param name="pin">The pin obtained from the user who authorized the application.</param>
        /// <returns></returns>
        public static IFluentYammer SetVerifier(this IFluentYammerAuthentication instance, string pin)
        {
            var oauth = instance.Authenticator as FluentBaseOAuth;
            if (oauth != null)
            {
                oauth.Verifier = pin;
            }
            return instance.Root;
        }

        public static IFluentYammer AuthorizeDesktop(this IFluentYammerAuthentication instance, string consumerKey,
                                                     string consumerSecret, string token, string tokenSecret,
                                                     string callback)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Root.Method = WebMethod.Post;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "authorize_desktop",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret,
                                             Token = token,
                                             TokenSecret = tokenSecret,
                                             Callback = callback
                                         };

            // start browser out of band
            ((FluentBaseOAuth) instance.Authenticator).Action = "authorize";
            Process.Start(instance.Root.AsUrl());

            return instance.Root;
        }
#endif

        public static IFluentYammer GetAccessToken(this IFluentYammerAuthentication instance, string consumerKey,
                                                   string consumerSecret, string token)
        {
            return BuildAccessToken(instance, consumerKey, consumerSecret, token);
        }

        public static IFluentYammer GetAccessToken(this IFluentYammerAuthentication instance, string token)
        {
            instance.Root.ValidateConsumerCredentials();

            var consumerKey = instance.Root.ClientInfo.ConsumerKey;
            var consumerSecret = instance.Root.ClientInfo.ConsumerSecret;

            return BuildAccessToken(instance, consumerKey, consumerSecret, token);
        }

        public static string GetAuthorizationUrl(this IFluentYammerAuthentication instance, string token)
        {
            return BuildAuthorizationUrl(instance, token, null);
        }

        public static string GetAuthorizationUrl(this IFluentYammerAuthentication instance, string token,
                                                 string callback)
        {
            return BuildAuthorizationUrl(instance, token, callback);
        }

        private static string BuildAuthorizationUrl(IFluentYammerAuthentication instance, string token, string callback)
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

        private static IFluentYammer BuildAccessToken(IFluentYammerAuthentication instance, string consumerKey,
                                                      string consumerSecret, string token)
        {
            instance.Mode = AuthenticationMode.OAuth;
            instance.Root.Method = WebMethod.Post;
            instance.Authenticator = new FluentBaseOAuth
                                         {
                                             Action = "access_token",
                                             ConsumerKey = consumerKey,
                                             ConsumerSecret = consumerSecret,
                                             Token = token
                                         };

            return instance.Root;
        }
    }
}