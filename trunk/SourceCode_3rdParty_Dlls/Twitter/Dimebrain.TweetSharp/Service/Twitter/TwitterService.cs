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
using TweetSharp.Core.Caching;
using TweetSharp.Core.Configuration;
using TweetSharp.Core.Web;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Service.Base;

namespace TweetSharp.Service
{
#if !SILVERLIGHT
    /// <summary>
    /// This service provides a simple API layer for TweetSharp that hides
    /// serialization details and uses the most efficient API configuration.
    /// </summary>
    [Serializable]
#endif
    public partial class TwitterService : ServiceBase<IFluentTwitter, TwitterResult, ITwitterLeafNode>
    {
        private AuthenticationMode _authenticationMode;
        private IAuthenticator _authenticator;
        private IClientCache _cache;

        private readonly ServiceCacheOptions _cacheOptions;
        private ServiceRetryOptions _retryOptions;
        private TimeSpan _timeout;

        private readonly Func<IFluentTwitter> _noAuthQuery
            = () =>
                  {
                      var query = FluentTwitter.CreateRequest();
                      return query;
                  };

        private readonly Func<string, string, IFluentTwitter> _basicAuthQuery
            = (username, password) =>
                  {
                      var query = FluentTwitter.CreateRequest();
                      query.AuthenticateAs(username, password);
                      return query;
                  };

        private readonly Func<string, string, IFluentTwitter> _oAuthQuery
            = (token, tokenSecret) =>
                  {
                      var query = FluentTwitter.CreateRequest();
                      query.AuthenticateWith(token, tokenSecret);
                      return query;
                  };

        /// <summary>
        /// Occurs when a streaming API call received statuses for processing.
        /// </summary>
        public virtual event EventHandler<TwitterStreamResultEventArgs> StreamResult;

        /// <summary>
        /// Raises the <see cref="StreamResult"/> event.
        /// </summary>
        /// <param name="args">The <see cref="TwitterStreamResultEventArgs"/> instance containing the event data.</param>
        protected virtual void OnStreamResult(TwitterStreamResultEventArgs args)
        {
            if (StreamResult != null)
            {
                StreamResult(this, args);
            }
        }

        static TwitterService()
        {
            Bootstrapper.Run();
        }

        /// <summary>
        /// Gets the <see cref="TwitterRateLimitStatus"/> of the last call made to the Twitter API.
        /// If the rate limit status was indeterminate on the last call, this is null.
        /// </summary>
        /// <value>The rate limit status based on the last successful API call.</value>
        public TwitterRateLimitStatus RateLimitStatus { get; private set; }

        /// <summary>
        /// Gets the <see cref="TwitterResult"/> of the last call made to the Twitter API.
        /// </summary>
        /// <value>The result based on the last API call.</value>
        public TwitterResult Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterService"/> class.
        /// </summary>
        /// <param name="info">The client info.</param>
        public TwitterService(IClientInfo info) : this()
        {
            FluentBase<TwitterResult>.SetClientInfo(info);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterService"/> class.
        /// </summary>
        public TwitterService()
        {
#if !Smartphone
            _cache = CacheFactory.AspNetCache;
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
            _cache = cache;
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
            _retryOptions = new ServiceRetryOptions
                                {
                                    RetryConditions = conditions,
                                    MaxRetries = maxRetries
                                };
        }

        /// <summary>
        /// Sets a duration before timing out an API request
        /// or Stream API read operation.
        /// </summary>
        /// <param name="duration">The duration before timing out.</param>
        public void TimeoutAfter(TimeSpan duration)
        {
            _timeout = duration;
        }

        /// <summary>
        /// Authenticates the service using a username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void AuthenticateAs(string username, string password)
        {
            _authenticationMode = AuthenticationMode.Basic;
            _authenticator = new FluentTwitterBasicAuth(username, password);
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

        protected override IFluentTwitter GetQuery()
        {
            switch (_authenticationMode)
            {
                case AuthenticationMode.Basic:
                    return GetBasicAuthQuery();
                case AuthenticationMode.OAuth:
                    return GetOAuthQuery();
                case AuthenticationMode.None:
                    return GetNoAuthQuery();
                default:
                    throw new NotSupportedException("Unrecognized authentication mode.");
            }
        }

        private IFluentTwitter GetOAuthQuery()
        {
            var authenticator = _authenticator as FluentBaseOAuth;
            if (authenticator == null)
            {
                throw new TweetSharpException("Could not find OAuth authentication data.");
            }

            var query = _oAuthQuery.Invoke(authenticator.Token, authenticator.TokenSecret);
            return query;
        }

        private IFluentTwitter GetBasicAuthQuery()
        {
            var authenticator = _authenticator as FluentTwitterBasicAuth;
            if (authenticator == null)
            {
                throw new TweetSharpException("Could not find basic authentication data.");
            }

            var query = _basicAuthQuery.Invoke(authenticator.Username, authenticator.Password);
            return query;
        }

        private IFluentTwitter GetNoAuthQuery()
        {
            var query = _noAuthQuery.Invoke();
            return query;
        }

        private TwitterCursorList<T> WithTweetSharpAndCursors<T>(Func<IFluentTwitter, ITwitterLeafNode> executor)
        {
            long? next = null;
            long? prev = null;
            var results = WithTweetSharp<List<T>>(executor, r =>
                                                                {
                                                                    next = r.AsNextCursor();
                                                                    prev = r.AsPreviousCursor();
                                                                });

            var list = new TwitterCursorList<T>(results) {NextCursor = next, PreviousCursor = prev};
            return list;
        }

        protected override void PrepareQuery<T>(IFluentTwitter query, Func<IFluentTwitter, ITwitterLeafNode> executor)
        {
            if (query.Method != WebMethod.Post)
            {
                // Results from POSTs are never large enough to justify GZIP
                query.Configuration.UseGzipCompression();
            }

            PrepareCacheOptions<T>(query);
            PrepareRetriesAndTimeout(query);

            executor.Invoke(query);

            // JSON is the lightest over the wire and supported by all methods
            query.Format = WebFormat.Json;
        }

        private void PrepareRetriesAndTimeout(IFluentTwitter query)
        {
            if (_retryOptions != null)
            {
                query.Configuration.UseAutomaticRetries(_retryOptions.RetryConditions, _retryOptions.MaxRetries);
            }

            if (_timeout != TimeSpan.Zero)
            {
                query.Configuration.TimeoutAfter(_timeout);
            }
        }

        private void PrepareCacheOptions<T>(IFluentTwitter query)
        {
            query.Configuration.CacheStrategy = _cache;
            var expectedTypeName = typeof (T).Name;
            if (!_cacheOptions.TimeSpan.MoreThan(TimeSpan.Zero) ||
                query.Method == WebMethod.Post ||
                expectedTypeName.Equals("TwitterRateLimitStatus"))
            {
                return;
            }

            switch (_cacheOptions.CacheMode)
            {
                case ServiceCacheMode.None:
                    break;
                case ServiceCacheMode.AbsoluteExpiration:
                    query.Configuration.CacheUntil(_cacheOptions.TimeSpan.FromNow());
                    break;
                case ServiceCacheMode.SlidingExpiration:
                    query.Configuration.CacheForInactivityOf(_cacheOptions.TimeSpan);
                    break;
                default:
                    throw new NotSupportedException("Unrecognized cache mode.");
            }
        }

        protected override T HandleResponse<T>(IFluentTwitter query, TwitterResult response)
        {
            if (response.IsTwitterError)
            {
                var error = response.AsError();
                if (error != null)
                {
                    Error = error;
                    Result = response;
                    return null;
                }

                throw new TweetSharpException("Twitter returned an API error but TweetSharp was unable to parse it.");
            }

            Result = response;
            RateLimitStatus = response.RateLimitStatus;

            var result = response.As<T>();
            if (result != null)
            {
                return result;
            }

            var message = string.Format("Could not convert the Twitter API response to {0}.", typeof (T).Name);
            throw new TweetSharpException(message);
        }

        protected override void HandleResponse(IFluentTwitter query, TwitterResult response)
        {
            if (response.IsTwitterError)
            {
                var error = response.AsError();
                if (error != null)
                {
                    throw new TwitterException(error);
                }
                throw new TweetSharpException("Twitter returned an API error but TweetSharp was unable to parse it.");
            }

            Result = response;
            RateLimitStatus = response.RateLimitStatus;
        }

        /// <summary>
        /// Gets the <see cref="TwitterError" /> returned from the last service call.
        /// If the last call was successful, this property is null.
        /// </summary>
        /// <value>The error.</value>
        public TwitterError Error { get; private set; }
    }
}