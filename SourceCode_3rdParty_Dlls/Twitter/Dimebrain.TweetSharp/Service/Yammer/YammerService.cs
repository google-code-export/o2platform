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
using TweetSharp.Core.Caching;
using TweetSharp.Core.Configuration;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Service.Base;

namespace TweetSharp.Service
{
#if !SILVERLIGHT
    /// <summary>
    /// This service provides a simple API layer for Yammer that hides
    /// serialization details and uses the most efficient API configuration.
    /// </summary>
    [Serializable]
#endif
    public partial class YammerService : ServiceBase<IFluentYammer, YammerResult, IYammerLeafNode>
    {
        private AuthenticationMode _authenticationMode;
        private IAuthenticator _authenticator;
        private readonly ServiceCacheOptions _cacheOptions;

        private readonly Func<IFluentYammer> _noAuthQuery
            = () =>
                  {
                      var query = FluentYammer.CreateRequest();
                      return query;
                  };

        private readonly Func<string, string, IFluentYammer> _oAuthQuery
            = (token, tokenSecret) =>
                  {
                      var query = FluentYammer.CreateRequest();
                      query.AuthenticateWith(token, tokenSecret);
                      return query;
                  };

        static YammerService()
        {
            Bootstrapper.Run();
        }

        /// <summary>
        /// Gets the <see cref="YammerResult"/> of the last call made to the Yammer API.
        /// </summary>
        /// <value>The result based on the last API call.</value>
        public YammerResult Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterService"/> class.
        /// </summary>
        /// <param name="info">The client info.</param>
        public YammerService(YammerClientInfo info) : this()
        {
            FluentYammer.SetClientInfo(info);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterService"/> class.
        /// </summary>
        public YammerService()
        {
#if !Smartphone
#else
            _cache = CacheFactory.InMemoryCache;
#endif
            _cacheOptions = new ServiceCacheOptions();
        }

        /// <summary>
        /// Delegates caching to the provided <see cref="IClientCache" /> instance.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public void CacheWith(IClientCache cache)
        {
        }

        /// <summary>
        /// Caches all following API calls using the current caching scheme
        /// for a specified length of time. 
        /// Cache keys are scoped to the authenticating user and the API URI.
        /// </summary>
        /// <param name="length">The length of time to cache individual queries for until they are invalid.</param>
        public void CacheFor(TimeSpan length)
        {
            _cacheOptions.CacheMode = ServiceCacheMode.AbsoluteExpiration;
            _cacheOptions.TimeSpan = length;
        }

        /// <summary>
        /// Caches all following API calls using the current caching scheme
        /// for a specified length of inactive time. Inactive time corresponds
        /// to the amount of time passed without attempts to access or update
        /// the cache key.
        /// Cache keys are scoped to the authenticating user and the API URI.
        /// </summary>
        /// <param name="length">The length of inactivity required to invalid individual cached queries.</param>
        public void CacheForInactivityOf(TimeSpan length)
        {
            _cacheOptions.CacheMode = ServiceCacheMode.SlidingExpiration;
            _cacheOptions.TimeSpan = length;
        }

        /// <summary>
        /// Sets the retry policy for API requests based on 
        /// <see cref="RetryOn"/> condition flags and a maximum number
        /// of retry attempts.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="maxRetries">The max retries.</param>
        public void RetryOn(RetryOn conditions, int maxRetries)
        {
        }

        /// <summary>
        /// Sets a duration before timing out an API request
        /// or Stream API read operation.
        /// </summary>
        /// <param name="duration">The duration before timing out.</param>
        public void TimeoutAfter(TimeSpan duration)
        {
        }

        /// <summary>
        /// Authenticates the service using a username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void AuthenticateAs(string username, string password)
        {
            _authenticationMode = AuthenticationMode.Basic;
            _authenticator = new FluentBaseBasicAuth(username, password);
        }

        /// <summary>
        /// Authenticates the service using an OAuth token and secret.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="tokenSecret">The token secret.</param>
        public void AuthenticateWith(string token, string tokenSecret)
        {
            var oAuth = new FluentBaseOAuth {Token = token, TokenSecret = tokenSecret};
            _authenticationMode = AuthenticationMode.OAuth;
            _authenticator = oAuth;
        }

        protected override IFluentYammer GetQuery()
        {
            switch (_authenticationMode)
            {
                case AuthenticationMode.OAuth:
                    return GetOAuthQuery();
                case AuthenticationMode.None:
                    return GetNoAuthQuery();
                default:
                    throw new NotSupportedException("Unrecognized authentication mode.");
            }
        }

        protected override void PrepareQuery<T>(IFluentYammer query, Func<IFluentYammer, IYammerLeafNode> executor)
        {
            throw new NotImplementedException();
        }

        protected override T HandleResponse<T>(IFluentYammer query, YammerResult response)
        {
            throw new NotImplementedException();
        }

        protected override void HandleResponse(IFluentYammer query, YammerResult response)
        {
            throw new NotImplementedException();
        }

        private IFluentYammer GetOAuthQuery()
        {
            var authenticator = _authenticator as FluentBaseOAuth;
            if (authenticator == null)
            {
                throw new TweetSharpException("Could not find OAuth authentication data.");
            }

            var query = _oAuthQuery.Invoke(authenticator.Token, authenticator.TokenSecret);
            return query;
        }

        private IFluentYammer GetNoAuthQuery()
        {
            var query = _noAuthQuery.Invoke();
            return query;
        }
    }
}