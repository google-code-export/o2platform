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
using TweetSharp.Fluent;

namespace TweetSharp.Fluent
{
    // TODO: make configuration come from an interface you can inject into the request
    public static class FluentYammerConfigurationExtensions
    {
        /// <summary>
        /// When this configuration option is called, the specified cache provider is used for any subsequent
        /// caching on the request. The default caching strategy is this method is not used, is <see cref="AspNetCache" />.
        /// </summary>
        /// <param name="cache">The caching strategy to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentYammer CacheWith(this IFluentYammerConfiguration instance, IClientCache cache)
        {
            instance.CacheStrategy = cache;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any request made inside the specified absolute expiration date,
        /// is served from the client cache rather than from a request made to Yammer.
        /// </summary>
        /// <param name="absoluteExpiration">The specified local time that the cache for the request as defined will expire</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentYammer CacheUntil(this IFluentYammerConfiguration instance, DateTime absoluteExpiration)
        {
            instance.CacheAbsoluteExpiration = absoluteExpiration;
            return instance.Root;
        }

        /// <summary>
        /// When this configuration option is called, any request made inside the specified sliding expiratino date,
        /// is served from the client cache rather than from a request made to Yammer. Sliding expiration countdown begins
        /// from the last time a request for the same URL was executed.
        /// </summary>
        /// <param name="slidingExpiration">The specified amount of inactivity that may elapse before expiring the cache</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentYammer CacheForInactivityOf(this IFluentYammerConfiguration instance,
                                                         TimeSpan slidingExpiration)
        {
            instance.CacheSlidingExpiration = slidingExpiration;
            return instance.Root;
        }

        // TODO: Need more proxy options, like port, etc. Also, intercept the default proxy.
        /// <summary>
        /// When this configuration option is called, the query request is sent via the specified proxy URL,
        /// rather than directly to Yammer.
        /// <remarks>
        /// Currently, the .NET default of using the Internet Explorer defined proxy as a default for all
        /// outgoing requests is in place, but may change before the version 1.0 release.
        /// </remarks>
        /// </summary>
        /// <param name="url">The URL of a proxy to use</param>
        /// <param name="instance">The current position in the fluent expression</param>
        /// <returns>The current position in the fluent expression</returns>
        public static IFluentYammer UseProxy(this IFluentYammerConfiguration instance, string url)
        {
            instance.Proxy = url;
            return instance.Root;
        }

        /// <summary>
        /// Uses the transparent proxy instead of calling Yammer directly
        /// </summary>
        /// <param name="instance">The FluentYammer instance.</param>
        /// <param name="url">The transparent proxy URL.</param>
        /// <returns>The FluentYammer instance</returns>
        public static IFluentYammer UseTransparentProxy(this IFluentYammerConfiguration instance, string url)
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
        public static IFluentYammer UseGzipCompression(this IFluentYammerConfiguration instance)
        {
            instance.CompressHttpRequests = true;
            return instance.Root;
        }

        /// <summary>
        /// Disables the mocking.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IFluentYammer DisableMocking(this IFluentYammerConfiguration instance)
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
        public static IFluentYammer UseAutomaticRetries(this IFluentYammerConfiguration instance, RetryOn retryOn,
                                                        int maximumRetries)
        {
            instance.RetryConditions = retryOn;
            instance.MaxRetries = maximumRetries;
            return instance.Root;
        }

        public static IFluentYammer TimeoutAfter(this IFluentYammerConfiguration instance, TimeSpan timeout)
        {
            instance.RequestTimeout = timeout;
            return instance.Root;
        }
    }
}