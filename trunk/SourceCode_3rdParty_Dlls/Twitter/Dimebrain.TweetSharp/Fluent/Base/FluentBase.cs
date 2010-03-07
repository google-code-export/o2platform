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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TweetSharp.Core;
using TweetSharp.Core.Caching;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.OAuth;
using TweetSharp.Core.Tasks;
using TweetSharp.Core.Web;
using TweetSharp.Core.Web.OAuth;
using TweetSharp.Core.Web.Query;
using TweetSharp.Core.Web.Query.Basic;
using TweetSharp.Core.Web.Query.OAuth;
using TweetSharp.Extensions;
using TweetSharp.Fluent.Base;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public abstract class FluentBase<T> : IFluentBase<T>
        where T : TweetSharpResult
    {
        protected ITimedTask _recurringTask;

        //todo: encapsulate retry state in its own object
        //retry state
        protected bool _firstTry = true;
        protected int _remainingRetries;
        protected T _previousResult;
        //end of retry state 

        protected static IClientInfo _clientInfo;
        protected static object _clientInfoLock = new object();

        protected abstract T BuildResult(WebQueryBase query, WebException exception);

        public abstract string AsUrl(bool ignoreTransparentProxy);
        public abstract string AsUrl();

#if !SILVERLIGHT
        public abstract T Request();
#endif
        protected abstract string UrlOAuthAuthority { get; }
        public abstract void RequestAsync();
        protected abstract string BuildQuery(bool hasAction, string format, string activity, string action);
        protected abstract Action<object, T> InternalCallback { get; }
        protected abstract void QueryQueryResponse(object sender, WebQueryResponseEventArgs args);

        /// <summary>
        /// Returns the key prepended to the URL when caching queries.
        /// If using basic authentication, this will be the authenticated user's username.
        /// If using OAuth, this will be the authenticated token.
        /// </summary>
        public string CacheKey
        {
            get { return AuthenticationPair != null ? AuthenticationPair.First : string.Empty; }
        }

        public abstract Pair<string, string> AuthenticationPair { get; }

        /// <summary>
        /// Gets a value indicating whether the last request resulted in an error.
        /// </summary>
        /// <value><c>true</c> if the last request resulted in an error; otherwise, <c>false</c>.</value>
        public bool HasError
        {
            get
            {
                return Response != null && Response is HttpWebResponse &&
                       ((Response as HttpWebResponse).StatusCode != HttpStatusCode.OK);
            }
        }

        /// <summary>
        /// Returns the human-readable query to Yammer representing the current expression.
        /// If you are storing URLs for sending later, you can use <code>AsUrl()</code> to return
        /// a URL-encoded string instead.
        /// </summary>
        /// <returns>A URL-decoded string representing this expression's query to Yammer</returns>
        public override string ToString()
        {
            // human-readable; for storing urls, use AsUrl()
            return AsUrl().UrlDecode();
        }

        /// <summary>
        /// Gets or sets the authentication.
        /// </summary>
        /// <value>The authentication.</value>
        public IFluentAuthentication Authentication { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public WebResponse Response { get; protected set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public WebMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        public WebFormat Format { get; set; }

        protected virtual string BuildOAuthQuery()
        {
            var oAuthBase = UrlOAuthAuthority;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                oAuthBase = oAuthBase.Replace(UrlOAuthAuthority, authority);
            }

            var oauth = (FluentBaseOAuth) Authentication.Authenticator;
            var url = oAuthBase.FormatWith(oauth.Action);

            if (oauth.Action.Equals("access_token") && !oauth.Verifier.IsNullOrBlank())
            {
                var delim = url.Contains("?") ? '&' : '?';
                url += delim;
                url += "oauth_verifier={0}".FormatWith(oauth.Verifier);
            }

            url = url.Replace("client_auth", "access_token");

            return oauth.Action == "authorize" ? BuildOAuthParameters(oauth, url) : url;
        }

        protected virtual string BuildOAuthParameters(IFluentBaseOAuth oauth, string url)
        {
            var parameters = new List<string>(0);
            if (!oauth.Token.IsNullOrBlank())
            {
                parameters.Add("oauth_token={0}".FormatWith(oauth.Token));
            }

            if (!oauth.Callback.IsNullOrBlank())
            {
                parameters.Add("oauth_callback={0}".FormatWith(oauth.Callback));
            }

            var sb = new StringBuilder(url);
            for (var i = 0; i < parameters.Count(); i++)
            {
                sb.Append(i > 0 ? "&" : "?");
                sb.Append(parameters[i]);
            }

            return sb.ToString();
        }

        protected virtual WebQueryBase CreateWebQuery()
        {
            WebQueryBase query;

            switch (Authentication.Mode)
            {
                case AuthenticationMode.OAuth:
                    {
                        var oAuthQuery = CreateOAuthQuery();
                        query = oAuthQuery;
                        break;
                    }
                case AuthenticationMode.Basic:
                    {
                        if (HasAuth)
                        {
                            var authToken = AuthenticationPair.First;
                            var authSecret = AuthenticationPair.Second;

                            var basicAuthQuery = new BasicAuthWebQuery(ClientInfo, authToken, authSecret);

                            if (ClientInfo != null && !ClientInfo.ClientName.IsNullOrBlank() && Method == WebMethod.Post)
                            {
                                var postParameter = new HttpPostParameter("source", ClientInfo.ClientName);
                                basicAuthQuery.Parameters.Add(postParameter);
                            }

                            query = basicAuthQuery;
                        }
                        else
                        {
                            query = new BasicAuthWebQuery(ClientInfo);
                        }
                        break;
                    }
                case AuthenticationMode.None:
                    query = new BasicAuthWebQuery(ClientInfo);
                    break;
                default:
                    throw new NotSupportedException("Only Basic Auth and OAuth authentication schemes are supported");
            }

            query.UseTransparentProxy = !string.IsNullOrEmpty(Configuration.TransparentProxy);
            query.SourceUrl = query.UseTransparentProxy ? AsUrl(true) : AsUrl();

            query.UseCompression = Configuration.CompressHttpRequests;
            query.MockWebQueryClient = Configuration.MockWebRequests;
            query.MockGraph = Configuration.MockGraph;
            query.Method = Method;
            query.RequestTimeout = Configuration.RequestTimeout;

            var proxy = Configuration.Proxy;
            if (!proxy.IsNullOrBlank())
            {
                if (Uri.IsWellFormedUriString(proxy, UriKind.RelativeOrAbsolute))
                {
                    query.Proxy = proxy;
                }
                else
                {
                    throw new TweetSharpException("A proxy '{0}' was specified but was an invalid URI".FormatWith(proxy));
                }
            }

            return query;
        }

        private OAuthWebQuery CreateOAuthQuery()
        {
            var oauth = (FluentBaseOAuth) Authentication.Authenticator;

            var workflow = new OAuthWorkflow
                               {
                                   ConsumerKey = oauth.ConsumerKey,
                                   ConsumerSecret = oauth.ConsumerSecret,
                                   Token = oauth.Token,
                                   TokenSecret = oauth.TokenSecret,
                                   SignatureMethod = OAuthSignatureMethod.HmacSha1,
                                   CallbackUrl = oauth.Callback,
                                   Verifier = oauth.Verifier
                               };

            var parameters = new WebParameterCollection();

            OAuthWebQueryInfo info;
            switch (oauth.Action)
            {
                case "resource":
                    info = workflow.BuildProtectedResourceInfo(Method, parameters, AsUrl(true));
                    break;
                case "request_token":
                    workflow.RequestTokenUrl = AsUrl(true);
                    info = workflow.BuildRequestTokenInfo(Method, parameters);
                    break;
                case "access_token":
                    oauth.Action = "access_token";
                    workflow.AccessTokenUrl = AsUrl(true);
                    info = workflow.BuildAccessTokenInfo(Method, parameters);
                    break;
                case "authorize":
                    // convert to a token request
                    oauth.Action = "access_token";
                    workflow.AccessTokenUrl = AsUrl(true);
                    info = workflow.BuildAccessTokenInfo(Method, parameters);
                    break;
                default:
                    throw new NotSupportedException("Unknown or unsupported OAuth action");
            }

            return new OAuthWebQuery(info);
        }

        public IFluentConfiguration Configuration { get; set; }

        protected void EnsureDefaultCache()
        {
            if (Configuration.CacheStrategy == null &&
                (Configuration.CacheAbsoluteExpiration.HasValue ||
                 Configuration.CacheSlidingExpiration.HasValue))
            {
#if !Smartphone && !SILVERLIGHT
                Configuration.CacheStrategy = CacheFactory.AspNetCache;
#else
                Configuration.CacheStrategy = CacheFactory.InMemoryCache;
#endif
            }
        }

        public TimeSpan RepeatInterval { get; set; }
        public int RepeatTimes { get; set; }
        public IRateLimitingRule RateLimitingRule { get; set; }

        public void Cancel()
        {
            if (RecurringTask == null)
            {
                return;
            }

            RecurringTask.Stop();
            RecurringTask.Dispose();
            RecurringTask = null;
        }

        public ITimedTask RecurringTask
        {
            get { return _recurringTask; }
            set
            {
                if (_recurringTask == null || value == null)
                {
                    _recurringTask = value;
                }
                else
                {
                    throw new InvalidOperationException("Recurring task already set");
                }
            }
        }

#if !SILVERLIGHT
        protected T RequestPostOrPut(PostOrPut method, WebQueryBase query, IEnumerable<string> attachPaths)
        {
            string queryResult = null;
            var uri = AsUrl();
            WebException exception = null;

            // skip caching if we're posting multi-part form data
            if (Configuration.CacheStrategy != null && !attachPaths.Any())
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
                if (attachPaths.Any())
                {
                    var files = new List<HttpPostParameter>();
                    var i = 0;
                    foreach (var path in attachPaths)
                    {
                        i++;
                        files.Add(HttpPostParameter.CreateFile("attachment" + i, Path.GetFileName(path), path,
                                                               "application/octet-stream"));
                    }
                    queryResult = query.Request(uri, files);
                }
                else
                {
                    // normal flow
                    queryResult = query.Request(uri, out exception);
                }
            }

            return BuildResultFromRequest(query, uri, queryResult, exception);
        }

        protected T BuildResultFromRequest(WebQueryBase query, string uri, string queryResponse,
                                           WebException exception)
        {
            var queryResult = query.Result;
            if (queryResponse == null)
            {
                queryResponse = query.Request(uri, out exception);
            }

            var twitterResponse = queryResponse.ToTwitterResponseString();
            queryResult.Response = twitterResponse;

            Response = query.WebResponse ?? (exception != null ? exception.Response : null);

            var result = BuildResult(query, exception);
            return result;
        }
#endif

        protected void RequestPostAsync(WebQueryBase query)
        {
            // skip caching if we're posting multi-part form data
            if (Configuration.CacheStrategy != null)
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

            // normal flow
            query.RequestAsync(AsUrl());
            return;
        }

#if !SILVERLIGHT
        protected T RequestGet(WebQueryBase query)
        {
            string queryResult = null;
            var uri = AsUrl();
            WebException exception = null;
            if (Configuration.CacheStrategy != null)
            {
                if (Configuration.CacheAbsoluteExpiration.HasValue && Configuration.CacheSlidingExpiration.HasValue)
                {
                    throw new ArgumentException("You may only specify one cache expiration on a query");
                }

                if (Configuration.CacheAbsoluteExpiration.HasValue)
                {
                    queryResult = query.Request(uri, CacheKey, Configuration.CacheStrategy,
                                                Configuration.CacheAbsoluteExpiration.Value, out exception);
                }
                else
                {
                    queryResult = Configuration.CacheSlidingExpiration.HasValue
                                      ? query.Request(uri, CacheKey, Configuration.CacheStrategy,
                                                      Configuration.CacheSlidingExpiration.Value, out exception)
                                      : query.Request(uri, CacheKey, Configuration.CacheStrategy, out exception);
                }
            }

            return BuildResultFromRequest(query, uri, queryResult, exception);
        }
#endif

        protected void RequestGetAsync(WebQueryBase query)
        {
            var url = AsUrl();

            if (Configuration.CacheStrategy != null)
            {
                if (Configuration.CacheAbsoluteExpiration.HasValue && Configuration.CacheSlidingExpiration.HasValue)
                {
                    throw new ArgumentException("You may only specify one cache expiration on a query");
                }

                if (Configuration.CacheAbsoluteExpiration.HasValue)
                {
                    query.RequestAsync(url, CacheKey, Configuration.CacheStrategy,
                                       Configuration.CacheAbsoluteExpiration.Value);

                    return;
                }

                if (Configuration.CacheSlidingExpiration.HasValue)
                {
                    query.RequestAsync(url, CacheKey, Configuration.CacheStrategy,
                                       Configuration.CacheSlidingExpiration.Value);
                    return;
                }

                query.RequestAsync(url, CacheKey, Configuration.CacheStrategy);
            }

            query.RequestAsync(url);
        }

#if !SILVERLIGHT
        protected T RequestDelete(WebQueryBase query)
        {
            var uri = AsUrl();

            // no sense caching deletes
            WebException exception = null;
            var queryResult = query.Request(uri, out exception);

            return BuildResultFromRequest(query, uri, queryResult, exception);
        }
#endif

        /// <summary>
        /// Gets a value indicating whether this instance has authentication data present.
        /// </summary>
        /// <value><c>true</c> if this instance has authentication data; otherwise, <c>false</c>.</value>
        public bool HasAuth
        {
            get
            {
                if (Authentication == null)
                {
                    return false;
                }

                var authenticator = Authentication.Authenticator;
                if (authenticator == null)
                {
                    return false;
                }

                if (!(authenticator is FluentBaseBasicAuth))
                {
                    return false;
                }

                return !((FluentBaseBasicAuth) authenticator).Username.IsNullOrBlank() &&
                       !((FluentBaseBasicAuth) authenticator).Password.IsNullOrBlank();
            }
        }

        /// <summary>
        /// Sets the client info.
        /// </summary>
        /// <param name="clientInfo">The client info.</param>
        public static void SetClientInfo(IClientInfo clientInfo)
        {
            lock (_clientInfoLock)
            {
                _clientInfo = clientInfo;
            }
        }

        public IClientInfo ClientInfo
        {
            get
            {
                lock (_clientInfoLock)
                {
                    return _clientInfo;
                }
            }
            set
            {
                lock (_clientInfoLock)
                {
                    _clientInfo = value;
                }
            }
        }

        protected void RequestDeleteAsync(WebQueryBase query)
        {
            var url = AsUrl();
            //don't cache deletes
            query.RequestAsync(url);
        }

        public abstract void ValidateUpdateText();

        protected bool IsOAuthProcessCall
        {
            get
            {
                if (Authentication.Mode != AuthenticationMode.OAuth)
                {
                    return false;
                }

                if (Authentication == null)
                {
                    return false;
                }

                if (!(Authentication.Authenticator is FluentBaseOAuth))
                {
                    return false;
                }

                var oauth = (FluentBaseOAuth) Authentication.Authenticator;
                return !oauth.Action.Equals("resource");
            }
        }
    }
}