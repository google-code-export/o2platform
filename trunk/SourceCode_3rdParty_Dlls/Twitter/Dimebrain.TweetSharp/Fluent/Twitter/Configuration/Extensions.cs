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
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Tasks;
using TweetSharp.Model;

#if SILVERLIGHT

#endif

namespace TweetSharp.Fluent
{
    // TODO: make configuration come from an interface you can inject into the request
    public static partial class Extensions
    {
        /// <summary>
        /// When this configuration option is called, any status updates that are longer than the
        /// 140 character option are truncated prior to sending. By default, updates longer than
        /// 140 characters will throw a <see cref="TweetSharpException" />
        /// </summary>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseUpdateTruncation(this IFluentTwitterConfiguration instance)
        {
            instance.TruncateUpdates = true;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any valid URLs found in user status text
        /// are sent out of band to the default URL shortening service provider, with the shortened
        /// URL replacing the longer one.
        /// </summary>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseUrlShortening(this IFluentTwitterConfiguration instance)
        {
            instance.ShortenUrls = true;
            instance.ShortenUrlService = ShortenUrlServiceProvider.Tomato;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any valid URLs found in user status text
        /// are sent out of band to the specificed URL shortening service provider, with the shortened
        /// URL replacing the longer one.
        /// </summary>
        /// <param name="type">The URL service provider to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseUrlShortening(this IFluentTwitterConfiguration instance,
                                                      ShortenUrlServiceProvider type)
        {
            instance.ShortenUrls = true;
            instance.ShortenUrlService = type;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any valid URLs found in user status text
        /// are sent out of band to the specificed URL shortening service provider, with the shortened
        /// URL replacing the longer one.</summary>
        /// <param name="type">The URL service provider to use</param>
        /// <param name="username">The username to pass to the URL service provider</param>
        /// <param name="password">The password to pass to the URL service provider</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseUrlShortening(this IFluentTwitterConfiguration instance,
                                                      ShortenUrlServiceProvider type,
                                                      string username,
                                                      string password)
        {
            instance.ShortenUrls = true;
            instance.ShortenUrlService = type;
            instance.ShortenUrlUsername = username;
            instance.ShortenUrlPassword = password;
            return instance.Root;
        }

        /// When this configuration option is called, any valid URLs found in user status text
        /// are sent out of band to the specificed URL shortening service provider, with the shortened
        /// URL replacing the longer one.
        /// <param name="type">The URL service provider to use</param>
        /// <param name="apiKey">The API key to pass to the URL service provider</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseUrlShortening(this IFluentTwitterConfiguration instance,
                                                      ShortenUrlServiceProvider type,
                                                      string apiKey)
        {
            instance.ShortenUrls = true;
            instance.ShortenUrlService = type;
            instance.ShortenUrlApiKey = apiKey;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, the specified cache provider is used for any subsequent
        /// caching on the request. The default caching strategy is this method is not used, is <see cref="AspNetCache" />.
        /// </summary>
        /// <param name="cache">The caching strategy to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter CacheWith(this IFluentTwitterConfiguration instance, IClientCache cache)
        {
            instance.CacheStrategy = cache;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any request made inside the specified absolute expiration date,
        /// is served from the client cache rather than from a request made to Twitter.
        /// </summary>
        /// <param name="absoluteExpiration">The specified local time that the cache for the request as defined will expire</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter CacheUntil(this IFluentTwitterConfiguration instance, DateTime absoluteExpiration)
        {
            instance.CacheAbsoluteExpiration = absoluteExpiration;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any request made inside the specified sliding expiratino date,
        /// is served from the client cache rather than from a request made to Twitter. Sliding expiration countdown begins
        /// from the last time a request for the same URL was executed.
        /// </summary>
        /// <param name="slidingExpiration">The specified amount of inactivity that may elapse before expiring the cache</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter CacheForInactivityOf(this IFluentTwitterConfiguration instance,
                                                          TimeSpan slidingExpiration)
        {
            instance.CacheSlidingExpiration = slidingExpiration;
            return instance.Root;
        }

        /// <summary>
        /// Throttles recurring task using a calculation, using the return value from the predicate to determine if the task should run
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="predicate">Predicate - will not run query if false is returned.</param>
        /// <param name="getRateLimitFunction">User provided function to get the RateLimit status</param>
        /// <returns></returns>
        public static IFluentTwitter UseRateLimiting(this IFluentTwitterConfiguration instance,
                                                     Predicate<TwitterRateLimitStatus> predicate,
                                                     Func<TwitterRateLimitStatus> getRateLimitFunction)
        {
            instance.LimitRate = true;
            instance.Root.RateLimitingRule = new RateLimitingRule(predicate) {GetRateLimitStatus = getRateLimitFunction};
            return instance.Root;
        }

        /// <summary>
        /// Throttles recurring task using a calculation, limiting it to a percentage of the periodic total rate limit
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="percentOfTotalLimit">Percentage of the user's total rate limit to allocate for this recurring request</param>
        /// <returns></returns>
        public static IFluentTwitter UseRateLimiting(this IFluentTwitterConfiguration instance,
                                                     double percentOfTotalLimit)
        {
            instance.LimitRate = true;
            instance.Root.RateLimitingRule = new RateLimitingRule(percentOfTotalLimit);
            return instance.Root;
        }

        /// <summary>
        /// Throttles recurring task using a calculation, limiting it to a percentage of the periodic total rate limit
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="percentOfTotalLimit">Percentage of the user's total rate limit to allocate for this recurring request</param>
        /// <param name="getRateLimitFunction">User provided function to get the rate limit status</param>
        /// <returns></returns>
        public static IFluentTwitter UseRateLimiting(this IFluentTwitterConfiguration instance,
                                                     double percentOfTotalLimit,
                                                     Func<TwitterRateLimitStatus> getRateLimitFunction)
        {
            instance.LimitRate = true;
            instance.Root.RateLimitingRule = new RateLimitingRule(percentOfTotalLimit)
                                                 {GetRateLimitStatus = getRateLimitFunction};
            return instance.Root;
        }

        // TODO: Need more proxy options, like port, etc. Also, intercept the default proxy.
        /// <summary>
        /// When this configuration option is called, the query request is sent via the specified proxy URL,
        /// rather than directly to Twitter.
        /// <remarks>
        /// Currently, the .NET default of using the Internet Explorer defined proxy as a default for all
        /// outgoing requests is in place, but may change before the version 1.0 release.
        /// </remarks>
        /// </summary>
        /// <param name="url">The URL of a proxy to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseProxy(this IFluentTwitterConfiguration instance, string url)
        {
            instance.Proxy = url;
            return instance.Root;
        }

        /// <summary>
        /// Uses the transparent proxy instead of calling twitter directly
        /// </summary>
        /// <param name="instance">The FluentTwitter instance.</param>
        /// <param name="url">The transparent proxy URL.</param>
        /// <returns>The FluentTwitter instance</returns>
        public static IFluentTwitter UseTransparentProxy(this IFluentTwitterConfiguration instance, string url)
        {
            if (!url.EndsWith("/"))
            {
                url = url.Then("/");
            }
            instance.TransparentProxy = url;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, the query request is sent as GZIP encoded content,
        /// and automatically decompressed when received. This is useful for requests that retrieve a large
        /// number of results, but will increase bandwidth on smaller requests.
        /// </summary>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentTwitter UseGzipCompression(this IFluentTwitterConfiguration instance)
        {
            instance.CompressHttpRequests = true;
            return instance.Root;
        }

        /// <summary>
        /// Disables the mocking.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IFluentTwitter DisableMocking(this IFluentTwitterConfiguration instance)
        {
            // todo test this properly, likely this needs more work
            instance.MockWebRequests = false;
            return instance.Root;
        }

        /// <summary>
        /// Sets up automatic retries for the error conditions indicated in 'retryPolicies'
        /// </summary>
        /// <param name="instance">The intance</param>
        /// <param name="retryOn">The error condition(s) that trigger a retry</param>
        /// <param name="maximumRetries">Max number of times to retry.  If exhausted, the last error will be returned</param>
        /// <returns></returns>
        public static IFluentTwitter UseAutomaticRetries(this IFluentTwitterConfiguration instance, RetryOn retryOn,
                                                         int maximumRetries)
        {
            instance.RetryConditions = retryOn;
            instance.MaxRetries = maximumRetries;
            return instance.Root;
        }

        public static IFluentTwitter TimeoutAfter(this IFluentTwitterConfiguration instance, TimeSpan timeout)
        {
            instance.RequestTimeout = timeout;
            return instance.Root;
        }
    }
}