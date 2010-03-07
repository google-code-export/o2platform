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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using TweetSharp.Core;
using TweetSharp.Core.Configuration;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.OAuth;
using TweetSharp.Core.Tasks;
using TweetSharp.Core.Web;
using TweetSharp.Core.Web.OAuth;
using TweetSharp.Core.Web.Query;
using TweetSharp.Core.Web.Query.OAuth;
using TweetSharp.Extensions;
using TweetSharp.Fluent.Base;
using TweetSharp.Fluent.Twitter.Services;
using TweetSharp.Model;
#if !SILVERLIGHT
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Security;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
#else
using HttpUtility = System.Windows.Browser.HttpUtility;
#endif
#if !SILVERLIGHT

#endif

namespace TweetSharp.Fluent
{
    /// <summary>
    /// This is the main fluent class for building expressions
    /// bound for the Twitter API.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class FluentTwitter : FluentBase<TwitterResult>, IFluentTwitter
    {
        // OAuth Authority
        protected override string UrlOAuthAuthority
        {
            get { return "http://api.twitter.com/oauth/{0}"; }
        }

        // Authority
        private const string UrlAuthority = "http://api.twitter.com/1/";
        private const string UrlSearchAuthority = "http://search.twitter.com/";
        private const string UrlStreamingAuthority = "http://stream.twitter.com/1/";

        // Base
        private const string UrlActionBase = UrlAuthority + "{0}/{1}.{2}";
        private const string UrlActionIdBase = UrlAuthority + "{0}/{1}/{2}.{3}";
        private const string UrlBase = UrlAuthority + "{0}.{1}";
        private const string UrlSearchBase = UrlSearchAuthority + "{0}.{1}";
        private const string UrlStreamingBase = UrlStreamingAuthority + "statuses/{0}.{1}";

        // List Base
        private const string UrlListsBase = UrlAuthority + "{0}/lists.{1}";
        private const string UrlListsIdBase = UrlAuthority + "{0}/lists/{1}.{2}";
        private const string UrlListsActionBase = UrlAuthority + "{0}/lists/{1}/{2}.{3}";

        // ClientInfo Defaults
        private const string TwitterClientDefaultName = "tweetsharp";
        private const string TwitterClientDefaultUrl = "http://tweetsharp.com";
        private const string TwitterClientDefaultVersion = "1.0.0.0";
        private const int TwitterMaxUpdateLength = 140;

        // Proxy Response Headers
        private const string ProxyStatusCode = "X-Twitter-StatusCode";
        private const string ProxyStatusDescription = "X-Twitter-StatusDescription";

        static FluentTwitter()
        {
            Bootstrapper.Run();
        }

        private FluentTwitter(IClientInfo clientInfo)
        {
#if !SILVERLIGHT && !Smartphone
            SetLibraryWebPermissions();
#endif
            ClientInfo = clientInfo;

            Profile = new FluentTwitterProfile();
            Authentication = new FluentTwitterAuthentication(this);
            SecondaryAuthentication = new FluentTwitterAuthentication(this);
            Configuration = new FluentTwitterConfiguration(this);
            SearchParameters = new FluentTwitterSearchParameters();
            StreamingParameters = new FluentTwitterStreamingParameters();
            TrendsParameters = new FluentTwitterTrendsParameters();
            Parameters = new FluentTwitterParameters();

            // Set JSON as the default
            Format = WebFormat.Json;
        }

        /// <summary>
        /// Gets the authentication pair used to authenticate to twitter.
        /// </summary>
        /// <value>The authentication pair, typically a username and password or a oauth token and tokensecret.</value>
        public override Pair<string, string> AuthenticationPair
        {
            get
            {
                if (Authentication == null || Authentication.Authenticator == null)
                {
                    return null;
                }

                return GetAuthPairFromAuthenticator(Authentication);
            }
        }

        /// <summary>
        /// Gets the authentication pair used to authenticate to 3rd party services such as image hosts
        /// </summary>
        /// <value>The authentication pair, typically a username and password or a oauth token and tokensecret.</value>
        public Pair<string, string> SecondaryAuthenticationPair
        {
            get
            {
                if (SecondaryAuthentication == null || SecondaryAuthentication.Authenticator == null)
                {
                    return null;
                }

                return GetAuthPairFromAuthenticator(SecondaryAuthentication);
            }
        }

        private static Pair<string, string> GetAuthPairFromAuthenticator(IFluentAuthentication authentication)
        {
            if (authentication == null)
            {
                return null;
            }

            switch (authentication.Mode)
            {
                case AuthenticationMode.Basic:
                    return new Pair<string, string>
                               {
                                   First = ((FluentTwitterBasicAuth) authentication.Authenticator).Username,
                                   Second = ((FluentTwitterBasicAuth) authentication.Authenticator).Password
                               };
                case AuthenticationMode.OAuth:
                    return new Pair<string, string>
                               {
                                   First = ((FluentBaseOAuth) authentication.Authenticator).Token,
                                   Second = ((FluentBaseOAuth) authentication.Authenticator).TokenSecret
                               };
                default:
                    throw new NotSupportedException("Unknown authentication mode");
            }
        }

        /// <summary>
        /// Gets or sets the secondary authentication.
        /// </summary>
        /// <value>The secondary authentication.</value>
        public IFluentTwitterAuthentication SecondaryAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>The profile.</value>
        public IFluentTwitterProfile Profile { get; set; }

        /// <summary>
        /// Gets or sets the search parameters.
        /// </summary>
        /// <value>The search parameters.</value>
        public IFluentTwitterSearchParameters SearchParameters { get; private set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IFluentTwitterParameters Parameters { get; private set; }

        public IFluentTwitterStreamingParameters StreamingParameters { get; private set; }

        public IFluentTwitterTrendsParameters TrendsParameters { get; private set; }

        protected override Action<object, TwitterResult> InternalCallback
        {
            get { return InternalCallbackImpl; }
        }

        private void InternalCallbackImpl(object sender, TweetSharpResult result)
        {
            if (Callback != null)
            {
                Callback(sender, result as TwitterResult);
            }
        }

        /// <summary>
        /// Gets or sets the client info.
        /// </summary>
        /// <value>The client info.</value>
        TwitterClientInfo IFluentTwitter.ClientInfo
        {
            get { return base.ClientInfo as TwitterClientInfo; }
            set { base.ClientInfo = value; }
        }

        IFluentTwitterAuthentication IFluentTwitter.Authentication
        {
            get { return (IFluentTwitterAuthentication) Authentication; }
            set { Authentication = value; }
        }

        IFluentTwitterConfiguration IFluentTwitter.Configuration
        {
            get { return (FluentTwitterConfiguration) Configuration; }
        }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>The callback.</value>
        public TwitterWebCallback Callback { get; set; }

#if !SILVERLIGHT
        TwitterResult IFluentBase<TwitterResult>.Request()
        {
            return  Request();
        }

        /// <summary>
        /// Makes a sequential call to Twitter to get the results of this query.
        /// </summary>
        /// <returns></returns>
        public override TwitterResult Request()
        {
            ValidateUpdateText();

            if (Parameters.Activity != null && Parameters.Activity.Equals("stream"))
            {
                throw new TweetSharpException("You must use RequestAsync() when using the Streaming API.");
            }

            var query = CreateWebQuery();

            // Default cache
            EnsureDefaultCache();

            // Multi-part form post for updating images
            var path = ValidatePostFilePath();

            if (_firstTry)
            {
                _remainingRetries = Configuration.MaxRetries + 1;
                _firstTry = false;
            }

            TwitterResult result = null;
            TwitterResult previousResult = null;
            while (_remainingRetries > 0)
            {
                switch (Method)
                {
                    case WebMethod.Get:
                        result = RequestGet(query);
                        break;
                    case WebMethod.Post:
                        result = RequestPostOrPut(PostOrPut.Post, query, path);
                        break;
                    case WebMethod.Put:
                        result = RequestPostOrPut(PostOrPut.Put, query, path);
                        break;
                    case WebMethod.Delete:
                        result = RequestDelete(query);
                        break;
                    default:
                        throw new NotSupportedException("Unknown or unsupported web method");
                }
                result.PreviousResult = previousResult;
                //handle retries
                if (Configuration.RetryConditions == RetryOn.Never)
                {
                    //retry policy is to never retry, so whatever we got is the result
                    _remainingRetries = 0;
                }
                else
                {
                    //retry based on policy and response result 
                    if (ShouldRetry(result))
                    {
                        previousResult = result;
                        _remainingRetries--;
                    }
                    else
                    {
                        //none of the retry conditions were met
                        _remainingRetries = 0;
                    }
                }
            }
            _firstTry = _remainingRetries == 0;
            return result;
        }
#endif

        private TwitterRateLimitStatus GetRateLimitStatus()
        {
            if (Response == null)
            {
                return null;
            }

            // [DC]: When using a SL proxy, the internal implementation
            // [DC]: is BrowserHttpWebResponse which does not implement
            // [DC]: the Headers collection; ergo, not supported in SL
            // [DC]: (This would work with SL3 + and ClientHttp)
#if SILVERLIGHT
            return null;
#endif
            var headers = Response.Headers;

            // X-RateLimit-Limit, X-RateLimit-Remaining, X-RateLimit-Reset
            var limit = headers["X-RateLimit-Limit"];
            var remaining = headers["X-RateLimit-Remaining"];
            var reset = headers["X-RateLimit-Reset"];

            return !(new[] {limit, remaining, reset}).AreNullOrBlank()
                       ? new TwitterRateLimitStatus
                             {
                                 HourlyLimit = Convert.ToInt32(limit, CultureInfo.InvariantCulture),
                                 RemainingHits = Convert.ToInt32(remaining, CultureInfo.InvariantCulture),
                                 ResetTimeInSeconds = Convert.ToInt64(reset, CultureInfo.InvariantCulture),
                                 ResetTime = Convert.ToInt64(reset, CultureInfo.InvariantCulture).FromUnixTime()
                             }
                       : null;
        }

        private string ValidatePostFilePath()
        {
            var hasProfileImage = !Profile.ProfileImagePath.IsNullOrBlank();
            var hasProfileBackgroundImage = !Profile.ProfileBackgroundImagePath.IsNullOrBlank();
            var isPhotoPost = !Parameters.PostImagePath.IsNullOrBlank();

            return hasProfileImage
                       ? Profile.ProfileImagePath
                       : hasProfileBackgroundImage
                             ? Profile.ProfileBackgroundImagePath
                             : isPhotoPost ? Parameters.PostImagePath : null;
        }


        /// <summary>
        /// Sends the query asynchronously.
        /// </summary>
        public override void RequestAsync()
        {
            // todo detect the [RequiresAuthentication] attribute and throw if credentials aren't provided

            ValidateUpdateText();
            if (_firstTry)
            {
                _remainingRetries = Configuration.MaxRetries;
                _firstTry = false;
            }

            var query = CreateWebQuery();

            if (Callback != null || Configuration.RetryConditions != RetryOn.Never)
            {
                query.QueryResponse += QueryQueryResponse;
            }

            if (Parameters.Activity != null && Parameters.Activity.Equals("stream"))
            {
                if (Callback == null)
                {
                    throw new TweetSharpException("You must declare a callback when using the Streaming API.");
                }

                query.KeepAlive = true;
                RequestAsyncStreamAction(query);
                return;
            }

            if (RepeatInterval > TimeSpan.Zero)
            {
                // Continuous async operation
                RecurringTask = new TimedTask(TimeSpan.Zero, RepeatInterval, true, RepeatTimes,
                                              RateLimitingRule, skip => RequestAsyncAction(skip, query));
            }
            else
            {
                // Normal async operation
                RequestAsyncAction(query);
            }
        }

        private void RequestAsyncStreamAction(WebQueryBase query)
        {
            var url = AsUrl();

            var duration = StreamingParameters.Duration.HasValue
                               ? StreamingParameters.Duration.Value
                               : TimeSpan.Zero;

            var resultCount = StreamingParameters.ResultsPerCallback.HasValue
                                  ? StreamingParameters.ResultsPerCallback.Value
                                  : 10;

            switch (Method)
            {
                case WebMethod.Get:
#if !SILVERLIGHT
                    query.ExecuteStreamGet(url, duration, resultCount);
#else
                    query.ExecuteStreamGetAsync(url, duration, resultCount);
#endif
                    return;
                case WebMethod.Post:
#if !SILVERLIGHT
                    // Convert GET URL to POST with parameters
                    var uri = new Uri(url);
                    url = uri.Scheme + "://" + uri.Authority + uri.AbsolutePath;
                    var parameters = HttpUtility.ParseQueryString(uri.Query);
                    foreach (
                        var postParameter in
                            parameters.AllKeys.Select(key => new HttpPostParameter(key, parameters[key])))
                    {
                        query.Parameters.Add(postParameter);
                    }
                    query.ExecuteStreamPost(PostOrPut.Post, url, duration, resultCount);
#else
    // [DC]: This will limit Silverlight streaming by URL length
                    query.ExecuteStreamGetAsync(url, duration, resultCount);
#endif
                    return;
                default:
                    throw new NotSupportedException("Unknown or unsupported streaming web method");
            }
        }

        private void RequestAsyncAction(WebQueryBase query)
        {
            RequestAsyncAction(false, query);
        }

        private void RequestAsyncAction(bool skip, WebQueryBase query)
        {
            if (skip)
            {
                //skipped due to rate limiting
                Callback(this, new TwitterResult {SkippedDueToRateLimiting = true});
                return;
            }

            // Default cache
            EnsureDefaultCache();

            // Multi-part form post for updating images
            var path = ValidatePostFilePath();

            switch (Method)
            {
                case WebMethod.Get:
                    RequestGetAsync(query);
                    return;
                case WebMethod.Post:
                    RequestPostAsync(query, path);
                    return;
                case WebMethod.Delete:
                    RequestDeleteAsync(query);
                    return;
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

#if !SILVERLIGHT
        private TwitterResult RequestPostOrPut(PostOrPut method, WebQueryBase query, string path)
        {
            var format = ValidatePhotoQuery(path);
            string queryResult = null;
            var uri = AsUrl();
            WebException exception = null;

            // skip caching if we're posting multi-part form data
            if (Configuration.CacheStrategy != null && path.IsNullOrBlank())
            {
                if (Configuration.CacheAbsoluteExpiration.HasValue && Configuration.CacheSlidingExpiration.HasValue)
                {
                    throw new ArgumentException("You may only specify one cache expiration on a query");
                }

                if (Configuration.CacheAbsoluteExpiration.HasValue)
                {
                    queryResult = query.ExecutePostOrPut(method, uri, CacheKey, Configuration.CacheStrategy,
                                                         Configuration.CacheAbsoluteExpiration.Value, out exception);
                }
                else
                {
                    queryResult = Configuration.CacheSlidingExpiration.HasValue
                                      ? query.ExecutePostOrPut(method, uri, CacheKey, Configuration.CacheStrategy,
                                                               Configuration.CacheSlidingExpiration.Value, out exception)
                                      : query.ExecutePostOrPut(method, uri, CacheKey, Configuration.CacheStrategy,
                                                               out exception);
                }
            }

            if (queryResult == null)
            {
                var isPhotoPost = !Parameters.PostImagePath.IsNullOrBlank() &&
                                  Parameters.PostImageProvider.HasValue;

                if (isPhotoPost)
                {
                    // external
                    SendPhotoAndAppendStatus(query, Parameters.PostImageProvider.Value, format, path);
                    WebException thirdPartyException;
                    // post with new media url
                    uri = AsUrl();
                    queryResult = query.Request(uri, out thirdPartyException);
                    //todo - do something meaningful with exceptions caught when posting to 3rd party servers - logging?
                }
                else
                {
                    if (path != null)
                    {
                        // internal
                        var file = HttpPostParameter.CreateFile("image", "twitterProfilePhoto.jpg", path, "image/jpeg");
                        queryResult = query.Request(uri, new[] {file});
                    }
                    else
                    {
                        // normal flow
                        queryResult = query.Request(uri, out exception);
                    }
                }
            }

            return (TwitterResult) BuildResultFromRequest(query, uri, queryResult, exception);
        }


#endif

        private void RequestPostAsync(WebQueryBase query, string path)
        {
#if !SILVERLIGHT
            var format = ValidatePhotoQuery(path);
#endif
            // skip caching if we're posting multi-part form data
            if (Configuration.CacheStrategy != null && path.IsNullOrBlank())
            {
                if (Configuration.CacheAbsoluteExpiration.HasValue && Configuration.CacheSlidingExpiration.HasValue)
                {
                    throw new ArgumentException("You may only specify one cache expiration on a query");
                }

                if (Configuration.CacheAbsoluteExpiration.HasValue)
                {
                    query.RequestAsync(AsUrl(), CacheKey, Configuration.CacheStrategy,
                                       Configuration.CacheAbsoluteExpiration.Value);

                    return;
                }

                if (Configuration.CacheSlidingExpiration.HasValue)
                {
                    query.RequestAsync(AsUrl(), CacheKey, Configuration.CacheStrategy,
                                       Configuration.CacheSlidingExpiration.Value);
                    return;
                }

                query.RequestAsync(AsUrl(), CacheKey, Configuration.CacheStrategy);
            }

#if !SILVERLIGHT
            var isPhotoPost = !Parameters.PostImagePath.IsNullOrBlank() && Parameters.PostImageProvider.HasValue;
            if (isPhotoPost)
            {
                // external async
                ThreadPool.QueueUserWorkItem(callback =>
                                                 {
                                                     if (Parameters.PostImageProvider != null)
                                                     {
                                                         SendPhotoAndAppendStatus(query,
                                                                                  Parameters.PostImageProvider.Value,
                                                                                  format, path);
                                                     }
                                                     query.RequestAsync(AsUrl());
                                                 });
                return;
            }

            if (path != null)
            {
                // internal async
                // todo preserve user's image name from path rather than invent one
                var file = HttpPostParameter.CreateFile("image", "twitterProfilePhoto.jpg", path, format.ToContentType());
                query.RequestAsync(AsUrl(), new[] {file});
                return;
            }
#endif
            // normal flow
            query.RequestAsync(AsUrl());
            return;
        }

#if !SILVERLIGHT
        private void SendPhotoAndAppendStatus(
            WebQueryBase query,
            SendPhotoServiceProvider provider,
            ImageFormat format,
            string path)
        {
            // todo when third parties move to oauth, will have to go through that whole process for these as well
            var authPair = SecondaryAuthenticationPair ?? AuthenticationPair;
            var authToken = authPair.First;
            var authSecret = authPair.Second;

            var mediaUrl = SendPhotoService.SendPhoto(query, provider, format, path, authToken, authSecret);

            Parameters.Text = !Parameters.Text.IsNullOrBlank()
                                  ? "{0} - {1}".FormatWith(Parameters.Text, mediaUrl)
                                  : mediaUrl;
        }

        private ImageFormat ValidatePhotoQuery(string path)
        {
            var isPhotoPost = !Parameters.PostImagePath.IsNullOrBlank() && Parameters.PostImageProvider.HasValue;
            if (!path.IsNullOrBlank())
            {
                switch (isPhotoPost)
                {
                    case true:
                        // external
                        return ValidateProviderPostImage(path, Parameters.PostImageProvider.Value);
                    case false:
                        // internal
                        ValidateTwitterPostImage(path);
                        break;
                    default:
                        throw new NotSupportedException("No fuzzy logic allowed");
                }
            }

            return null;
        }

        private static ImageFormat ValidateProviderPostImage(string path, SendPhotoServiceProvider provider)
        {
            switch (provider)
            {
                    // todo refactor to generic size / formats validation
                case SendPhotoServiceProvider.TwitPic:
                    return ValidateProviderPostImage(path, 4.Megabytes());
                case SendPhotoServiceProvider.YFrog:
                    // todo unclear on YFrog size limitations
                    return ValidateProviderPostImage(path, 4.Megabytes());
                case SendPhotoServiceProvider.TwitGoo:
                    // todo unclear on TwitGoo size limitations
                    return ValidateProviderPostImage(path, 16.Megabytes());
                default:
                    throw new NotSupportedException("Not a recognized photo posting service");
            }
        }

        private static ImageFormat ValidateProviderPostImage(string path, long size)
        {
            try
            {
                // valid image
                var info = new FileInfo(path);
                if (info.Length > size)
                {
                    throw new TweetSharpException(
                        string.Format("The image provided was larger than 4Mb, actual size was {0}", info.Length));
                }
#if !Smartphone
                var image = Image.FromFile(path);
                return image.RawFormat;
#else
                return ImageFormat.Bmp;
#endif
            }
            catch (IOException iex)
            {
                throw new TweetSharpException("The path to the image provided was invalid", iex);
            }
            catch (Exception ex)
            {
                throw new TweetSharpException("Unable to process the provided path as an image", ex);
            }
        }

        private static void ValidateTwitterPostImage(string path)
        {
            try
            {
#if !Smartphone
                var image = Image.FromFile(path);
                if (ImageFormat.Jpeg.Equals(image.RawFormat))
                {
                    // valid
                }
                else if (ImageFormat.Gif.Equals(image.RawFormat))
                {
                    // valid
                }
                else if (ImageFormat.Png.Equals(image.RawFormat))
                {
                    // valid
                }
                else
                {
                    throw new TweetSharpException("The image provided was not a supported type");
                }
#endif

                var info = new FileInfo(path);
                if (info.Length > 700.Kilobytes())
                {
                    throw new TweetSharpException(
                        string.Format("The image provided was larger than 700kb, actual size was {0}", info.Length));
                }

                return;
            }
            catch (IOException iex)
            {
                throw new TweetSharpException("The path to the image provided was invalid", iex);
            }
            catch (Exception ex)
            {
                throw new TweetSharpException("Unable to process the provided path as an image", ex);
            }
        }


#endif

        /// <summary>
        /// Creates a new composable query, using a specified client and a default platform.
        /// </summary>
        /// <param name="clientInfo">The client making the request</param>
        public static IFluentTwitter CreateRequest(TwitterClientInfo clientInfo)
        {
            return new FluentTwitter(clientInfo);
        }

        /// <summary>
        /// Creates a new composable query, using the default client and platform.
        /// </summary>
        public static IFluentTwitter CreateRequest()
        {
            if (_clientInfo == null)
            {
                lock (_clientInfoLock)
                {
                    _clientInfo = new TwitterClientInfo
                                      {
                                          ClientName = TwitterClientDefaultName,
                                          ClientUrl = TwitterClientDefaultUrl,
                                          ClientVersion = TwitterClientDefaultVersion
                                      };
                }
            }

            return new FluentTwitter(_clientInfo);
        }

        public override void ValidateUpdateText()
        {
            if (Parameters.Text == null)
            {
                // non-participant
                return;
            }

            if (Parameters.Text.IsNullOrBlank())
            {
                if (!Parameters.PostImagePath.IsNullOrBlank() &&
                    Parameters.PostImageProvider.HasValue)
                {
                    // photo statuses have no text initially
                    return;
                }

                throw new ArgumentException("Status text must contain at least one character");
            }

            var words = Parameters.Text.Split(' ').ToList();

#if !SILVERLIGHT
            ShortenUrls(words);
#endif

            if (Parameters.Text.Length <= TwitterMaxUpdateLength)
            {
                // valid
                return;
            }

            switch (((IFluentTwitterConfiguration) Configuration).TruncateUpdates)
            {
                case true:
                    while (Parameters.Text.Length > TwitterMaxUpdateLength)
                    {
                        if (words.Count > 1)
                        {
                            var last = words.Last();
                            Parameters.Text = Parameters.Text.RemoveRange(Parameters.Text.LastIndexOf(last),
                                                                          Parameters.Text.Length);
                            words.Remove(last);
                        }
                        else
                        {
                            if (Parameters.Text.Length == 1)
                            {
                                throw new TweetSharpException("This shouldn't have happened");
                            }

                            Parameters.Text = Parameters.Text.Substring(0, Parameters.Text.Length - 1);
                        }
                    }

                    ValidateUpdateText();
                    break;
                default:
                    throw new TweetSharpException(
                        "Status length of {0} exceeds the maximum length of {1}".FormatWith(Parameters.Text.Length,
                                                                                            TwitterMaxUpdateLength));
            }
        }

#if !SILVERLIGHT
        private void ShortenUrls(IEnumerable<string> words)
        {
            var configuration = (IFluentTwitterConfiguration) Configuration;
            if (!configuration.ShortenUrls)
            {
                return;
            }

            if (configuration.ShortenUrlService == null)
            {
                return;
            }

            var provider = configuration.ShortenUrlService.Value;
            var username = configuration.ShortenUrlUsername;
            var password = configuration.ShortenUrlPassword;
            var apiKey = configuration.ShortenUrlApiKey;

            Parameters.Text = ShortenUrlService.ShortenUrl(provider, words, Parameters.Text, username, password, apiKey);
        }
#endif

        protected override void QueryQueryResponse(object sender, WebQueryResponseEventArgs args)
        {
            if (Callback == null && Configuration.RetryConditions == RetryOn.Never)
            {
                return;
            }

            var query = sender as WebQueryBase;
            if (query == null)
            {
                return;
            }

            Response = query.WebResponse;

            args.Response = args.Response.ToTwitterResponseString();

            var result = BuildResult(query, args.Exception);

            // [DC] Accommodate a proxy by setting X-based statuses if found           
            if (Response is HttpWebResponse)
            {
                if (Response.Headers == null)
                {
                    // [DC]: Silverlight is using BrowserHttp, not ClientHttp
                }
                else
                {
                    if (!Response.Headers[ProxyStatusCode].IsNullOrBlank())
                    {
                        result.ResponseHttpStatusCode = Convert.ToInt32(Response.Headers[ProxyStatusCode]);
                    }

                    if (!Response.Headers[ProxyStatusDescription].IsNullOrBlank())
                    {
                        result.ResponseHttpStatusDescription = Response.Headers[ProxyStatusDescription];
                    }
                }
            }

            result.PreviousResult = _previousResult;
            if (RateLimitingRule != null)
            {
                var rateLimitStatus = RateLimitingRule.GetRateLimitStatus != null
                                          ? RateLimitingRule.GetRateLimitStatus()
                                          : result.RateLimitStatus;

                AdjustRepeatRateForThrottledTask(rateLimitStatus);
            }

            if (ShouldRetry(result) && _remainingRetries > 0)
            {
                _remainingRetries--;
                _previousResult = result;
                RequestAsync();
            }
            else
            {
                if (Callback != null)
                {
                    Callback(this, result);
                }
                _firstTry = true;
                _previousResult = null;
            }
        }

        private void AdjustRepeatRateForThrottledTask(TwitterRateLimitStatus currentRateLimitStatus)
        {
            //only "by percent" needs adjustment 
            //and then only if we have a current rate limit object to work with
            if (RateLimitingRule == null
                || currentRateLimitStatus == null
                || RateLimitingRule.RateLimitingType != RateLimitingType.ByPercent)
            {
                return;
            }

            //stop the task while we calculate - further iterations would complicate matters
            _recurringTask.Stop();
            var desiredRemaining = currentRateLimitStatus.RemainingHits*RateLimitingRule.LimitToPercentOfTotal;
            var newRate = ((int) (currentRateLimitStatus.ResetTimeInSeconds/desiredRemaining)).Seconds();
            if (newRate > 0.Seconds())
            {
                //adjust rate from now on
                _recurringTask.Start(newRate, newRate);
            }
            else
            {
                //delay until next reset and calc rate to use desired percent of total
                if (RateLimitingRule.LimitToPercentOfTotal != null)
                {
                    var desiredPerHour =
                        Math.Floor(currentRateLimitStatus.HourlyLimit*RateLimitingRule.LimitToPercentOfTotal.Value);
                    var intervalInSeconds = 1.Hour().TotalSeconds/desiredPerHour;
                    var interval = ((int) intervalInSeconds).Seconds();
                    var resetTime = ((int) currentRateLimitStatus.ResetTimeInSeconds).Seconds();
                    _recurringTask.Start(resetTime, interval);
                }
            }
            RepeatInterval = _recurringTask.Interval;
        }

        protected override TwitterResult BuildResult(WebQueryBase query, WebException exception)
        {
#if !Smartphone
            var response = Response;
            var webResponse = query.WebResponse;
#else
            var response = Response ?? (exception != null ? exception.Response : null); 
            var webResponse = query.WebResponse ?? response;

#endif
            var statusDescription = response != null && response is HttpWebResponse
                                        ? ((response as HttpWebResponse).StatusDescription)
                                        : default(string);

            var statusCode = response != null && response is HttpWebResponse
                                 ? Convert.ToInt32((response as HttpWebResponse).StatusCode,
                                                   CultureInfo.InvariantCulture)
                                 : default(int);

            var result = new TwitterResult(query.Result, exception)
                             {
                                 // WebResponse is null in the event of an error or a cached result
                                 ResponseType = webResponse != null ? webResponse.ContentType : "",
                                 ResponseLength = webResponse != null ? webResponse.ContentLength : 0,
                                 ResponseUri = webResponse != null ? webResponse.ResponseUri : null,
                                 ResponseHttpStatusCode = statusCode,
                                 ResponseHttpStatusDescription = statusDescription,
                                 RateLimitStatus = GetRateLimitStatus(),
                                 RequestHttpMethod = query.Method.ToUpper()
                             };

            if (query.Result != null && !query.Result.Response.IsNullOrBlank() && query.KeepAlive)
            {
                // StringSplitOptions.RemoveEmptyEntries not supported on CE
                var lines = query.Result.Response.Split(new[] {'\r'}).Where(v => !v.IsNullOrBlank()).ToArray();
                if (lines.Length > 1)
                {
                    result.StreamedResponses = lines;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the human-readable query to Twitter representing the current expression.
        /// If you are storing URLs for sending later, you can use <code>AsUrl()</code> to return
        /// a URL-encoded string instead.
        /// </summary>
        /// <returns>A URL-decoded string representing this expression's query to Twitter</returns>
        public override string ToString()
        {
            // human-readable; for storing urls, use AsUrl()
            return AsUrl().UrlDecode();
        }

#if !SILVERLIGHT && !Smartphone
        private static void SetLibraryWebPermissions()
        {
            var permissions = new WebPermission();
            var baseUrl = new Regex(@"http://twitter\.com/.*");
            var apiUrl = new Regex(@"http://api.twitter\.com/.*");
            var searchUrl = new Regex(@"http://search.twitter\.com/.*");

            permissions.AddPermission(NetworkAccess.Connect, baseUrl);
            permissions.AddPermission(NetworkAccess.Connect, apiUrl);
            permissions.AddPermission(NetworkAccess.Connect, searchUrl);

            try
            {
                permissions.Demand();
            }
            catch (SecurityException)
            {
                var message =
                    "You cannot use TweetSharp in partial trust without a policy that allows connecting to API endpoints." +
                    Environment.NewLine +
                    "The following policy information (or equivalent) must be added to your trust policy:" +
                    Environment.NewLine +
                    permissions.ToXml();

                throw new SecurityException(message);
            }
        }
#endif


        private static void ArrangeClientAuthForAuthorizationHeader(OAuthWorkflow workflow,
                                                                    WebParameterCollection parameters,
                                                                    OAuthWebQueryInfo info)
        {
            if (workflow.ParameterHandling != OAuthParameterHandling.HttpAuthorizationHeader)
            {
                return;
            }

            var mode = parameters["x_auth_mode"];
            var username = parameters["x_auth_username"];
            var password = parameters["x_auth_password"];

            parameters.Remove(mode);
            parameters.Remove(username);
            parameters.Remove(password);

            info.ClientMode = null;
            info.ClientUsername = null;
            info.ClientPassword = null;

            // These values are already encoded
            workflow.AccessTokenUrl += string.Format("?{0}={1}&{2}={3}&{4}={5}", mode.Name, mode.Value,
                                                     username.Name, username.Value, password.Name,
                                                     password.Value);
        }

        private bool ShouldRetry(TwitterResult result)
        {
            return (result.IsFailWhale && (Configuration.RetryConditions & RetryOn.FailWhale) == RetryOn.FailWhale)
                   ||
                   (result.IsTwitterError &&
                    (Configuration.RetryConditions & RetryOn.ServiceError) == RetryOn.ServiceError)
#if !SILVERLIGHT
                   || (result.TimedOut) && (Configuration.RetryConditions & RetryOn.Timeout) == RetryOn.Timeout
                   ||
                   (result.ConnectionClosed) &&
                   (Configuration.RetryConditions & RetryOn.ConnectionClosed) == RetryOn.ConnectionClosed;
#else
                   ;
#endif
        }

        public override string AsUrl(bool ignoreTransparentProxy)
        {
            var pre = Configuration.TransparentProxy;
            if (ignoreTransparentProxy)
            {
                Configuration.TransparentProxy = null;
            }

            var hasAuthAction = IsOAuthProcessCall;
            var hasAction = !Parameters.Action.IsNullOrBlank();
            var format = Format.ToLower();
            var activity = Parameters.Activity.IsNullOrBlank() ? "?" : Parameters.Activity;
            var action = hasAction ? Parameters.Action : "?";

            string url;

            // this is a direct call
            if (!Parameters.DirectPath.IsNullOrBlank())
            {
                url = BuildDirectQuery();
            }
            else
                // this is an oauth call
                if (hasAuthAction)
                {
                    url = BuildOAuthQuery();
                }
                else // this is a streaming api call
                    if (hasAction && activity.Equals("stream"))
                    {
                        url = BuildStreamingQuery(action, format);
                    }
                    else // this is a trends query
                        if (hasAction && activity.Equals("trends"))
                        {
                            url = BuildTrendsQuery(activity, action, format);
                        }
                        else // this is a search api call
                            if (hasAction && !activity.Equals("users") &&
                                (Equals(action, "search")
                                 || Equals(action, "trends")
                                 || Equals(action, "trends/current")
                                 || Equals(action, "trends/daily")
                                 || Equals(action, "trends/weekly")))
                            {
                                url = BuildSearchQuery(format);
                            }
                            else // this is a lists query
                                if (Equals(Parameters.Activity, "lists"))
                                {
                                    url = BuildListsQuery(format, action);
                                }
                                else // this is a rest api call
                                {
                                    url = BuildQuery(hasAction, format, activity, action);
                                }

            // [DC] Twitter has recently had issues with passing screen names in URLs with uppercase;
            // So we should lowercase all non-query elements of the URL automatically
            // todo determine if Uri does this on its own; if so, just wrap and fire
            var uri = new Uri(url);
            url = string.Format("{0}://{1}{2}{3}{4}",
                                uri.Scheme,
                                uri.Host.ToLower(),
                                (uri.Scheme == "http" && uri.Port != 80 ||
                                 uri.Scheme == "https" && uri.Port != 443)
                                    ? ":" + uri.Port
                                    : "",
                                uri.AbsolutePath.ToLower(),
                                uri.Query); // Don't lowercase the query; otherwise this would be one ToLower() call

            Configuration.TransparentProxy = pre;
            return url;
        }

        /// <summary>
        /// Builds a URL from the specified fluent expression instance.
        /// </summary>
        /// <returns></returns>
        public override string AsUrl()
        {
            return AsUrl(false /* ignoreTransparentProxy */);
        }

        protected override string BuildQuery(bool hasAction, string format, string activity, string action)
        {
            var id = !Parameters.ScreenName.IsNullOrBlank()
                         ? Parameters.ScreenName
                         : !Parameters.Email.IsNullOrBlank()
                               ? Parameters.Email
                               : Parameters.Id.HasValue
                                     ? Parameters.Id.Value.ToString()
                                     : String.Empty;

            var isDisambiguated =
                Parameters.Activity.Equals("users") &&
                (!Parameters.Action.IsNullOrBlank() &&
                 Parameters.Action.Equals("show"));

            var hasId = !id.IsNullOrBlank() && !isDisambiguated;

            // Swap the authority if a transparent proxy is used
            var urlActionIdBase = UrlActionIdBase;
            var urlActionBase = UrlActionBase;
            var urlBase = UrlBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlActionIdBase = urlActionIdBase.Replace(UrlAuthority, authority);
                urlActionBase = urlActionBase.Replace(UrlAuthority, authority);
                urlBase = urlBase.Replace(UrlAuthority, authority);
            }

            var url = hasAction ? hasId ? urlActionIdBase : urlActionBase : urlBase;
            url = String.Format(url, hasAction
                                         ? hasId
                                               ? new object[] {activity, action, id, format}
                                               : new object[] {activity, action, format}
                                         : new object[] {activity, format});

            var resultUrl = BuildRestParameters(url);
            return resultUrl;
        }

        private string BuildRestParameters(string url)
        {
            var parameters = new List<string>(BuildRestParameters());

            return ConcatentateParameters(parameters, url);
        }

        private IEnumerable<string> BuildRestParameters()
        {
            foreach (var pagingParameter in BuildPagingParameters())
            {
                yield return pagingParameter;
            }

            if (Parameters.Cursor.HasValue)
            {
                yield return "cursor={0}".FormatWith(Parameters.Cursor.Value);
            }

            if (Parameters.ReturnPerPage.HasValue)
            {
                yield return string.Format("rpp={0}", Parameters.ReturnPerPage.Value);
            }

            if (!Parameters.Text.IsNullOrBlank())
            {
                var format = Parameters.Activity == "direct_messages"
                                 ? "text={0}"
                                 : "status={0}";

                var content = Parameters.Text.UrlEncode();
                yield return string.Format(format, content);
            }

            if (Parameters.InReplyToStatusId.HasValue)
            {
                yield return string.Format("in_reply_to_status_id={0}", Parameters.InReplyToStatusId.Value);
            }

            if (Parameters.UserId.HasValue)
            {
                string format;
                switch (Parameters.Activity)
                {
                    case "friendships":
                        format = "user_a={0}";
                        break;
                    case "users":
                    case "report_spam":
                        format = "user_id={0}";
                        break;
                    default:
                        format = "user={0}";
                        break;
                }

                yield return string.Format(format, Parameters.UserId.Value);
            }

            if (!Parameters.UserScreenName.IsNullOrBlank())
            {
                string format;
                switch (Parameters.Activity)
                {
                    case "friendships":
                        format = "user_a={0}";
                        break;
                    case "users":
                    case "report_spam":
                        format = "screen_name={0}";
                        break;
                    default:
                        format = "user={0}";
                        break;
                }

                yield return string.Format(format, Parameters.UserScreenName.UrlEncode());
            }

            if (Parameters.Follow.HasValue)
            {
                yield return string.Format("follow={0}", Parameters.Follow.ToString().ToLower());
            }

            if (Parameters.VerifyId.HasValue)
            {
                yield return string.Format("user_b={0}", Parameters.VerifyId.ToString().ToLower());
            }

            if (!Parameters.VerifyScreenName.IsNullOrBlank())
            {
                var format = Parameters.Activity == "friendships"
                                 ? "user_b={0}"
                                 : "user={0}";

                yield return string.Format(format, Parameters.VerifyScreenName.UrlEncode());
            }

            if (!Profile.ProfileName.IsNullOrBlank())
            {
                yield return string.Format("name={0}", Profile.ProfileName.UrlEncode());
            }

            if (!Profile.ProfileLocation.IsNullOrBlank())
            {
                yield return string.Format("location={0}", Profile.ProfileLocation.UrlEncode());
            }

            if (!Profile.ProfileUrl.IsNullOrBlank())
            {
                yield return string.Format("url={0}", Profile.ProfileUrl.UrlEncode());
            }

            if (!Profile.ProfileDescription.IsNullOrBlank())
            {
                yield return string.Format("description={0}", Profile.ProfileDescription.UrlEncode());
            }

            if (Profile.ProfileDeliveryDevice.HasValue)
            {
                yield return string.Format("device={0}", Profile.ProfileDeliveryDevice.ToString().ToLower());
            }

            if (!Profile.ProfileBackgroundColor.IsNullOrBlank())
            {
                yield return string.Format("profile_background_color={0}", Profile.ProfileBackgroundColor.UrlEncode());
            }

            if (!Profile.ProfileTextColor.IsNullOrBlank())
            {
                yield return string.Format("profile_text_color={0}", Profile.ProfileTextColor.UrlEncode());
            }

            if (!Profile.ProfileLinkColor.IsNullOrBlank())
            {
                yield return string.Format("profile_link_color={0}", Profile.ProfileLinkColor.UrlEncode());
            }

            if (!Profile.ProfileSidebarFillColor.IsNullOrBlank())
            {
                yield return
                    string.Format("profile_sidebar_fill_color={0}", Profile.ProfileSidebarFillColor.UrlEncode());
            }

            if (!Profile.ProfileSidebarBorderColor.IsNullOrBlank())
            {
                yield return string.Format("profile_sidebar_border_color={0}", Profile.ProfileLinkColor.UrlEncode());
            }

            if (Parameters.GeoLocation != null)
            {
                yield return "lat={0}&long={1}".FormatWithInvariantCulture(
                                                                              Parameters.GeoLocation.Value.Latitude,
                                                                              Parameters.GeoLocation.Value.Longitude);
            }

            if (Parameters.SourceId.HasValue)
            {
                yield return string.Format("source_id={0}", Parameters.SourceId.Value);
            }

            if (Parameters.TargetId.HasValue)
            {
                yield return string.Format("target_id={0}", Parameters.TargetId.Value);
            }

            if (!Parameters.SourceScreenName.IsNullOrBlank())
            {
                yield return string.Format("source_screen_name={0}", Parameters.SourceScreenName);
            }

            if (!Parameters.TargetScreenName.IsNullOrBlank())
            {
                yield return string.Format("target_screen_name={0}", Parameters.TargetScreenName);
            }

            if (!Parameters.UserSearch.IsNullOrBlank())
            {
                yield return string.Format("q={0}", Parameters.UserSearch.UrlEncode());
            }

            // Saved searches borrows the search builder
            if (Parameters.Activity.Equals("saved_searches") &&
                (Parameters.Action != null && Parameters.Action.Equals("create")))
            {
                const string format = "query={0}";
                var sb = new StringBuilder();

                var searchOperators = BuildSearchOperators();
                var searchParameters = BuildSearchParameters();

                var total = searchOperators.Count();
                var count = 0;
                foreach (var searchOperator in searchOperators)
                {
                    sb.Append(searchOperator);
                    count++;

                    if (total < count)
                    {
                        sb.Append(" ");
                    }
                }

                total = searchParameters.Count();
                count = 0;
                foreach (var searchParameter in searchParameters)
                {
                    sb.Append(searchParameter);
                    count++;

                    if (total < count)
                    {
                        sb.Append(" ");
                    }
                }

                yield return format.FormatWith(sb);
            }
        }

        private string BuildDirectQuery()
        {
            var path = Parameters.DirectPath;
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            // Swap the authority if a transparent proxy is used
            var urlBase = UrlAuthority;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlBase = urlBase.Replace(UrlAuthority, authority);
            }

            var url = String.Concat(urlBase, path);
            var uri = url.AsUri();

            var query = uri.Query;
            if (query.IsNullOrBlank())
            {
                path = url;
            }
            else
            {
                // remove POST parameters 
                // NOTE: Duplicate from Core.Web.Query.OAuth.OAuthWebQuery.BuildPostOrPutWebRequest()
                path = uri.Scheme.Then("://")
#if !SILVERLIGHT
                    .Then(uri.Authority)
#else
                    .Then(uri.Host)
#endif
                    ;
                if (uri.Port != 80)
                    path = path.Then(":" + uri.Port);
                path = path.Then(uri.AbsolutePath);

#if !SILVERLIGHT
                var parameters = HttpUtility.ParseQueryString(query);
#else
                var parameters = StringExtensions.ParseQueryString(query);
#endif

                var encodedQuery = "?" + string.Join("&", parameters
#if !SILVERLIGHT
                                                              .AllKeys
#else
                    .Keys
#endif
                                                              .Select(key => "{0}={1}".FormatWith(
                                                                                                     HttpUtility.
                                                                                                         UrlEncode(key),
                                                                                                     HttpUtility.
                                                                                                         UrlEncode(
                                                                                                                      parameters
                                                                                                                          [
                                                                                                                              key
                                                                                                                          ])))
                                                              .ToArray());

                path = path.Then(encodedQuery);
            }

            return path;
        }

        private string BuildStreamingQuery(string action, string format)
        {
            var streamingBase = UrlStreamingBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                streamingBase = streamingBase.Replace(UrlStreamingAuthority, authority);
            }

            var url = String.Format(streamingBase, action, format);

            return BuildStreamingParameters(url);
        }

        private string BuildStreamingParameters(string url)
        {
            var parameters = new List<string>(0);
            // todo do filter methods need to be in the POST body?
            if (StreamingParameters.Count.HasValue && StreamingParameters.Count.Value != 0)
            {
                parameters.Add("count={0}".FormatWith(StreamingParameters.Count.Value));
            }

            if (StreamingParameters.Length.HasValue)
            {
                parameters.Add("delimited={0}".FormatWith(StreamingParameters.Length.Value));
            }

            if (StreamingParameters.UserIds != null && StreamingParameters.UserIds.Count() > 0)
            {
                var sb = new StringBuilder();
                var array = StreamingParameters.UserIds.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    sb.Append(array[i]);
                    if (i < array.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                parameters.Add("follow={0}".FormatWith(sb.ToString().UrlEncode()));
            }

            if (StreamingParameters.Locations != null && StreamingParameters.Locations.Count() > 0)
            {
                var sb = new StringBuilder();
                var array = StreamingParameters.Locations.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    var box = array[i];
                    var pair = "{0},{1}".FormatWith(box.Longitude, box.Latitude);
                    sb.Append(pair);
                    if (i < array.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                parameters.Add("locations={0}".FormatWith(sb.ToString().UrlEncode()));
            }

            if (StreamingParameters.Keywords != null && StreamingParameters.Keywords.Count() > 0)
            {
                var sb = new StringBuilder();
                var array = StreamingParameters.Keywords.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    sb.Append(array[i]);
                    if (i < array.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                parameters.Add("track={0}".FormatWith(sb.ToString().UrlEncode()));
            }

            return ConcatentateParameters(parameters, url);
        }

        private static string ConcatentateParameters(IList<string> parameters, string url)
        {
            var sb = new StringBuilder(url);
            for (var i = 0; i < parameters.Count(); i++)
            {
                sb.Append(i > 0 ? "&" : "?");
                sb.Append(parameters[i]);
            }

            return sb.ToString();
        }

        private string BuildTrendsQuery(string activity, string action, string format)
        {
            var trendsBase = UrlActionBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                trendsBase = trendsBase.Replace(UrlStreamingAuthority, authority);
            }

            var hasId = TrendsParameters.WoeId.HasValue;

            var url = hasId
                          ? String.Format(trendsBase, activity, TrendsParameters.WoeId, format)
                          : String.Format(trendsBase, activity, action, format);

            return BuildTrendsParameters(url);
        }

        private string BuildTrendsParameters(string url)
        {
            var parameters = new List<string>(0);

            if (TrendsParameters.OrderLocation.HasValue)
            {
                parameters.Add("lat={0}".FormatWith(TrendsParameters.OrderLocation.Value.Latitude.ToString().UrlEncode()));
                parameters.Add(
                                  "long={0}".FormatWith(
                                                           TrendsParameters.OrderLocation.Value.Longitude.ToString().
                                                               UrlEncode()));
            }

            return ConcatentateParameters(parameters, url);
        }

        private string BuildListsQuery(string format, string action)
        {
            // todo refactor this spaghetti when the Twitter Lists API stabilizes/improves

            var id = Parameters.ListSlug.IsNullOrBlank()
                         ? Parameters.ListId.HasValue
                               ? Parameters.ListId.Value.ToString()
                               : null
                         : Parameters.ListSlug;

            var hasAction = !Parameters.Action.IsNullOrBlank();
            var urlBase = hasAction
                              ? UrlListsActionBase
                              : id != null ? UrlListsIdBase : UrlListsBase;

            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlBase = urlBase.Replace(UrlAuthority, authority);
            }

            var user = (Parameters.UserScreenName ?? "").UrlEncode();
            var url = hasAction
                          ? urlBase.FormatWith(user, id, action, format)
                          : id != null
                                ? urlBase.FormatWith(user, id, format)
                                : urlBase.FormatWith(user, format);

            if (!Parameters.Action.IsNullOrBlank() &&
                (Parameters.Action.Equals("members") || Parameters.Action.Equals("subscribers")))
            {
                url = url.Replace("lists/", "");
            }

            if (!Parameters.Action.IsNullOrBlank() &&
                (Parameters.Action.Equals("members_id") || Parameters.Action.Equals("subscribers_id")))
            {
                // http://api.twitter.com/1/user/list_id/members/id.format
                // http://api.twitter.com/1/user/list_id/subscribers/id.format
                url = url.Replace("lists/", "");
                url = url.Replace("members_id", string.Format("members/{0}", Parameters.UserId.Value));
                url = url.Replace("subscribers_id", string.Format("subscribers/{0}", Parameters.UserId.Value));
            }

            url = url.Replace("//", "/").Replace("http:/", "http://").Replace("https:/", "https://");

            var resultUrl = BuildListsParameters(url);
            return resultUrl;
        }

        private string BuildListsParameters(string url)
        {
            var parameters = new List<string>(BuildListsParameters());

            return ConcatentateParameters(parameters, url);
        }

        private string BuildSearchQuery(string format)
        {
            // Swap the authority if a transparent proxy is used
            var urlSearchBase = UrlSearchBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlSearchBase = urlSearchBase.Replace(UrlSearchAuthority, authority);
            }

            var searchUrl = urlSearchBase.FormatWith(Parameters.Action, format);
            var searchBuilder = new StringBuilder(searchUrl);

            // [Issue 2] Distinguish between parameters and operators
            var searchOperators = new List<string>(BuildSearchOperators());
            for (var i = 0; i < searchOperators.Count(); i++)
            {
                searchBuilder.Append(i > 0 ? "+" : "?q=");
                searchBuilder.Append(searchOperators[i]);
            }

            var hasOperators = searchOperators.Count > 0;
            var searchParameters = new List<string>(BuildSearchParameters());
            for (var i = 0; i < searchParameters.Count(); i++)
            {
                searchBuilder.Append(i > 0 || hasOperators ? "&" : "?");
                searchBuilder.Append(searchParameters[i]);
            }

            var result = searchBuilder.ToString();
            return result;
        }

        private IEnumerable<string> BuildSearchOperators()
        {
            if (!SearchParameters.SearchPhrase.IsNullOrBlank())
            {
                yield return SearchParameters.SearchPhrase.UrlEncode();
            }

            if (!SearchParameters.SearchWithoutPhrase.IsNullOrBlank())
            {
                yield return "-{0}".FormatWith(SearchParameters.SearchWithoutPhrase).UrlEncode();
            }

            // operators below phrase

            if (SearchParameters.SearchSince.HasValue)
            {
                var date = SearchParameters.SearchSince.Value;
                yield return
                    string.Format("since:{0}-{1}-{2}", date.Year,
                                  date.Month.ToString("00"),
                                  date.Day.ToString("00")).UrlEncode();
            }

            if (SearchParameters.SearchSinceUntil.HasValue)
            {
                var date = SearchParameters.SearchSinceUntil.Value;
                yield return
                    string.Format("until:{0}-{1}-{2}",
                                  date.Year,
                                  date.Month.ToString("00"),
                                  date.Day.ToString("00")).UrlEncode();
            }

            if (!SearchParameters.SearchFromUser.IsNullOrBlank())
            {
                yield return "from:{0}".FormatWith(SearchParameters.SearchFromUser).UrlEncode();
            }

            if (!SearchParameters.SearchToUser.IsNullOrBlank())
            {
                yield return string.Format("to:{0}", SearchParameters.SearchToUser).UrlEncode();
            }

            if (!SearchParameters.SearchHashTag.IsNullOrBlank())
            {
                yield return string.Format("#{0}", SearchParameters.SearchHashTag.Replace("#", "")).UrlEncode();
            }

            if (!SearchParameters.SearchReferences.IsNullOrBlank())
            {
                yield return string.Format("@{0}", SearchParameters.SearchReferences).UrlEncode();
            }

            if (!SearchParameters.SearchNear.IsNullOrBlank())
            {
                yield return string.Format("near:{0}", SearchParameters.SearchNear).UrlEncode();
            }

            if (SearchParameters.SearchNegativity.HasValue &&
                SearchParameters.SearchNegativity.Value)
            {
                yield return ":(".UrlEncode();
            }

            if (SearchParameters.SearchPositivity.HasValue &&
                SearchParameters.SearchPositivity.Value)
            {
                yield return ":)".UrlEncode();
            }

            if (SearchParameters.SearchQuestion.HasValue &&
                SearchParameters.SearchQuestion.Value)
            {
                yield return "?".UrlEncode();
            }

            if (SearchParameters.SearchContainingLinks.HasValue &&
                SearchParameters.SearchContainingLinks.Value)
            {
                yield return "filter:links".UrlEncode();
            }
        }

        private IEnumerable<string> BuildSearchParameters()
        {
            if (Parameters.SinceId.HasValue)
            {
                yield return string.Format("since_id={0}", Parameters.SinceId.Value);
            }

            if (Parameters.MaxId.HasValue)
            {
                yield return string.Format("max_id={0}", Parameters.MaxId.Value);
                    // Note: Although it isn't documented, it is set in the result's next_page
            }

            if (Parameters.ReturnPerPage.HasValue)
            {
                yield return "rpp={0}".FormatWith(Parameters.ReturnPerPage.Value);
            }

            if (Parameters.Page.HasValue)
            {
                yield return "page={0}".FormatWith(Parameters.Page.Value);
            }

            // root parameters above

            if (!SearchParameters.SearchLanguage.IsNullOrBlank())
            {
                yield return "lang={0}".FormatWith(SearchParameters.SearchLanguage.Substring(0, 2)).ToLower();
            }

            if (!SearchParameters.SearchLocale.IsNullOrBlank())
            {
                yield return "locale={0}".FormatWith(SearchParameters.SearchLocale.Substring(0, 2)).ToLower();
            }

            // within can be "within:" or "geocode?"
            if (SearchParameters.SearchMiles.HasValue && SearchParameters.SearchMiles.Value != 0)
            {
                if (SearchParameters.SearchGeoLatitude.HasValue && SearchParameters.SearchGeoLongitude.HasValue)
                {
                    var lat = SearchParameters.SearchGeoLatitude.Value;
                    var lon = SearchParameters.SearchGeoLongitude.Value;
                    var mi = SearchParameters.SearchMiles.Value;

                    // todo confirm precision of units
                    yield return "geocode={0}"
                        .FormatWithInvariantCulture("{0},{1},{2}mi".FormatWithInvariantCulture(lat, lon, mi)
                                                        .UrlEncode());
                }
                else
                {
                    var miles = Convert.ToInt32(SearchParameters.SearchMiles.Value);
                    yield return string.Format("within:{0}mi", miles).UrlEncode();

                    // [Issue 1] Can't use the "near:" + "within:" operator with arbitrary locations 
                    throw new NotSupportedException(
                        "You must specify a geo location with Of(latitude, longitude) when using Within(double miles)");
                }
            }

            // [Issue 2]: Missing "show_user" parameter
            if (SearchParameters.SearchShowUser.HasValue)
            {
                yield return "show_user={0}".FormatWith(SearchParameters.SearchShowUser.Value.ToString().ToLower());
            }

            // trend parameters below 

            if (SearchParameters.SearchExcludesHashtags.HasValue)
            {
                yield return "exclude=hashtags";
            }
            if (SearchParameters.SearchDate.HasValue)
            {
                yield return string.Format("date={0}", SearchParameters.SearchDate.Value.ToString("yyyy-MM-dd"));
            }
        }

        private IEnumerable<string> BuildPagingParameters()
        {
            if (Parameters.SinceId.HasValue)
            {
                yield return string.Format("since_id={0}", Parameters.SinceId.Value);
            }

            if (Parameters.MaxId.HasValue)
            {
                yield return string.Format("max_id={0}", Parameters.MaxId.Value);
            }

            if (Parameters.Count.HasValue)
            {
                // Newer API methods use per_page, not count, when they don't use cursors
                var token = Parameters.Activity.Equals("lists")
                            || (Parameters.Activity.Equals("users") &&
                                !Parameters.Action.IsNullOrBlank() &&
                                Parameters.Action.Equals("search"))
                                ? "per_page"
                                : "count";

                yield return string.Format("{0}={1}", token, Parameters.Count.Value);
            }

            if (Parameters.Page.HasValue)
            {
                yield return string.Format("page={0}", Parameters.Page.Value);
            }
        }

        private IEnumerable<string> BuildListsParameters()
        {
            foreach (var pagingParameter in BuildPagingParameters())
            {
                yield return pagingParameter;
            }

            if (Parameters.ListMemberId.HasValue)
            {
                yield return "id={0}".FormatWith(Parameters.ListMemberId.Value);
            }

            if (Parameters.Cursor.HasValue)
            {
                yield return "cursor={0}".FormatWith(Parameters.Cursor.Value);
            }

            if (!Parameters.ListName.IsNullOrBlank())
            {
                yield return "name={0}".FormatWith(Parameters.ListName.UrlEncode());
            }

            if (!Parameters.ListMode.IsNullOrBlank())
            {
                yield return "mode={0}".FormatWith(Parameters.ListMode.UrlEncode());
            }

            if (!Parameters.ListDescription.IsNullOrBlank())
            {
                yield return "description={0}".FormatWith(Parameters.ListDescription.UrlEncode());
            }
        }
    }
}