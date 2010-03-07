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
using System.Threading;
using TweetSharp.Core.Attributes;
using TweetSharp.Core.Caching;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.OAuth;
using TweetSharp.Core.Web.Query.OAuth;
using TweetSharp.Model;
#if !SILVERLIGHT
using TweetSharp.Core.Web.Mocks;
#endif

namespace TweetSharp.Core.Web.Query
{
    public enum PostOrPut
    {
        Post,
        Put
    }

    /// <summary>
    /// A general-purpose web query engine. Supports asynchronous calls, caching,
    /// and dynamic header / parameter generation.
    /// </summary>
    public abstract class WebQueryBase
    {
        private static readonly object _sync = new object();
        private WebResponse _webResponse;

        public IWebQueryInfo Info { get; protected set; }
        public string UserAgent { get; private set; }

        public IDictionary<string, string> Headers { get; protected set; }
        public WebParameterCollection Parameters { get; protected set; }

        public WebResponse WebResponse
        {
            get
            {
                lock (_sync)
                {
                    return _webResponse;
                }
            }
            set
            {
                lock (_sync)
                {
                    _webResponse = value;
                }
            }
        }

        public WebMethod Method { get; set; }
        public string Proxy { get; set; }
        public bool MockWebQueryClient { get; set; }
        public string AuthorizationHeader { get; protected set; }

        internal IEnumerable<IModel> MockGraph { get; set; }
        internal bool UseCompression { get; set; }
        internal bool UseTransparentProxy { get; set; }
        internal TimeSpan? RequestTimeout { get; set; }

        // todo push WebRequest/WebResponse objects into this result class
        /// <summary>
        /// Gets or sets the last query result.
        /// </summary>
        /// <value>The result of the query.</value>
        public WebQueryResult Result { get; private set; }

        public bool KeepAlive { get; set; }
        public string SourceUrl { get; set; }

        protected WebQueryBase(IWebQueryInfo info)
        {
            SetQueryMetadata(info);
            InitializeResult();
        }

        private void SetQueryMetadata(IWebQueryInfo info)
        {
            Info = info;
            Headers = BuildRequestHeaders();
            Parameters = BuildRequestParameters();
            ParseUserAgent();
        }

        private void InitializeResult()
        {
            Result = new WebQueryResult();
            QueryRequest += (s, e) =>
                                {
                                    Result.RequestDate = DateTime.UtcNow;
                                    Result.RequestUri = new Uri(e.Request);
                                };
            QueryResponse += (s, e) =>
                                 {
                                     Result.ResponseDate = DateTime.UtcNow;
                                     Result.Response = e.Response;
                                 };
        }

#if !SILVERLIGHT
        protected void SetWebProxy(WebRequest request)
        {
#if !Smartphone
            var proxyUriBuilder = new UriBuilder(Proxy);
            request.Proxy = new WebProxy(proxyUriBuilder.Host,
                                         proxyUriBuilder.Port);

            if (!proxyUriBuilder.UserName.IsNullOrBlank())
            {
                request.Headers["Proxy-Authorization"] = WebExtensions.ToAuthorizationHeader(proxyUriBuilder.UserName,
                                                                                             proxyUriBuilder.Password);
            }
#else
            var uri = new Uri(Proxy);
            request.Proxy = new WebProxy(uri.Host, uri.Port);
            var userParts = uri.UserInfo.Split(new[] { ':' }).Where(ui => !ui.IsNullOrBlank()).ToArray();
            if (userParts.Length == 2)
            {
                request.Proxy.Credentials = new NetworkCredential(userParts[0], userParts[1]);
            }
#endif
        }
#endif

        protected virtual IWebQueryClient CreateWebQueryClientForDelete(string url)
        {
            IWebQueryClient client;

            if (this is OAuthWebQuery)
            {
                var info = (OAuthWebQueryInfo) Info;
                client = CreateOAuthWebQueryClient(Headers, Parameters, info.ParameterHandling, UserAgent, "DELETE");
            }
            else
            {
                client = CreateWebQueryClient(Headers, Parameters, UserAgent, MockWebQueryClient, MockGraph,
                                              UseTransparentProxy, "DELETE");
            }

            client.ProxyValue = Proxy;
            client.UseCompression = UseCompression;
            client.RequestTimeout = RequestTimeout;
            client.KeepAlive = KeepAlive;
            return client;
        }

        protected virtual WebRequest BuildDeleteWebRequest(string url)
        {
            url = AppendParameters(url);

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "DELETE";

#if !SILVERLIGHT
            if (!Proxy.IsNullOrBlank())
            {
                SetWebProxy(request);
            }
#endif
            if (!UserAgent.IsNullOrBlank())
            {
#if !SILVERLIGHT
                request.UserAgent = UserAgent;
#else
                request.Headers["User-Agent"] = UserAgent;
#endif
            }

#if !SILVERLIGHT
            if (RequestTimeout.HasValue)
            {
                request.Timeout = (int)RequestTimeout.Value.TotalMilliseconds;
            }
#endif

            AppendHeaders(request);
            return request;
        }

        protected virtual WebRequest BuildPostOrPutWebRequest(PostOrPut method, string url, out byte[] content)
        {
            url = AppendParameters(url);

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = method == PostOrPut.Post ? "POST" : "PUT";
            request.ContentType = "application/x-www-form-urlencoded";

#if !SILVERLIGHT
            if (!Proxy.IsNullOrBlank())
            {
                SetWebProxy(request);
            }
#endif
            if (!UserAgent.IsNullOrBlank())
            {
#if !SILVERLIGHT
                request.UserAgent = UserAgent;
#else
                request.Headers["User-Agent"] = UserAgent;
#endif
            }

            if (UseCompression)
            {
#if !SILVERLIGHT
                request.AutomaticDecompression = DecompressionMethods.GZip;
#else
                // todo will need decompression on response
                request.Accept = "gzip,deflate";
#endif
            }
#if !SILVERLIGHT
            if (RequestTimeout.HasValue)
            {
                request.Timeout = (int)RequestTimeout.Value.TotalMilliseconds;
            }
#endif
            AppendHeaders(request);

#if !SILVERLIGHT
            content = Encoding.ASCII.GetBytes(url);
            request.ContentLength = content.Length;

            if (KeepAlive)
            {
                request.KeepAlive = true;
            }
#else
            // todo check implications
            content = Encoding.UTF8.GetBytes(url);
#endif
            return request;
        }

        protected void AppendHeaders(WebRequest request)
        {
            foreach (var header in Headers)
            {
#if !SILVERLIGHT
                request.Headers.Add(header.Key, header.Value);
#else
                request.Headers[header.Key] = header.Value;
#endif
            }
        }

        protected virtual string AppendParameters(string url)
        {
            var parameters = 0;
            foreach (var parameter in Parameters)
            {
                if (parameter is HttpPostParameter && Method != WebMethod.Post)
                {
                    continue;
                }

                // GET parameters in URL
                url = url.Then(parameters > 0 || url.Contains("?") ? "&" : "?");
                url = url.Then("{0}={1}".FormatWith(parameter.Name, parameter.Value.UrlEncode()));
                parameters++;
            }

            return url;
        }

        private IDictionary<string, string> BuildRequestHeaders()
        {
            var headers = new Dictionary<string, string>();
            var properties = Info.GetType().GetProperties();

            Info.ParseAttributes<HeaderAttribute>(properties, headers);
            return headers;
        }

        protected WebParameterCollection BuildRequestParameters()
        {
            var parameters = new Dictionary<string, string>();
            var properties = Info.GetType().GetProperties();

            Info.ParseAttributes<ParameterAttribute>(properties, parameters);

            var collection = new WebParameterCollection();
            parameters.ForEach(p => collection.Add(new WebParameter(p.Key, p.Value)));

            return collection;
        }

        private void ParseUserAgent()
        {
            var properties = Info.GetType().GetProperties();
            var count = 0;
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes<UserAgentAttribute>(true);
                count += attributes.Count();
                if (count > 1)
                {
                    throw new ArgumentException("Cannot declare more than one user agent per query");
                }

                if (count < 1)
                {
                    continue;
                }

                if (!UserAgent.IsNullOrBlank())
                {
                    continue;
                }

                var value = property.GetValue(Info, null);
                UserAgent = value != null ? value.ToString() : null;
            }
        }

        protected string HandleWebException(WebException ex)
        {
            if (ex.Response is HttpWebResponse && ex.Response != null)
            {
                WebResponse = ex.Response;

                using (var reader = new StreamReader(WebResponse.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    var args = new WebQueryResponseEventArgs(result, ex);

                    OnQueryResponse(args);
                    return result;
                }
            }

            return string.Empty;
        }

        protected IWebQueryClient CreateWebQueryClient()
        {
            IWebQueryClient client;

            // todo: LSP violation
            if (this is OAuthWebQuery)
            {
                var info = (OAuthWebQueryInfo) Info;
                client = CreateOAuthWebQueryClient(Headers, Parameters, info.ParameterHandling, UserAgent);
            }
            else
            {
                client = CreateWebQueryClient(Headers, Parameters, UserAgent, MockWebQueryClient, MockGraph,
                                              UseTransparentProxy);
            }

            client.ProxyValue = Proxy;
            client.SourceUrl = SourceUrl;

            client.RequestTimeout = RequestTimeout;
            client.UseCompression = UseCompression;
            client.KeepAlive = KeepAlive;

            return client;
        }

        private static IWebQueryClient CreateOAuthWebQueryClient(IDictionary<string, string> headers,
                                                                 WebParameterCollection parameters,
                                                                 OAuthParameterHandling parameterHandling,
                                                                 string userAgent)
        {
            return CreateOAuthWebQueryClient(headers, parameters, parameterHandling, userAgent, "GET");
        }

        private static IWebQueryClient CreateWebQueryClient(IDictionary<string, string> headers,
                                                            WebParameterCollection parameters, string userAgent,
                                                            bool mockClient, IEnumerable<IModel> graph,
                                                            bool useTransparentProxy)
        {
            return CreateWebQueryClient(headers, parameters, userAgent, mockClient, graph, useTransparentProxy, "GET");
        }

        private static IWebQueryClient CreateOAuthWebQueryClient(IDictionary<string, string> headers,
                                                                 WebParameterCollection parameters,
                                                                 OAuthParameterHandling parameterHandling,
                                                                 string userAgent, string method)
        {
            return new OAuthWebQueryClient(headers, parameters, parameterHandling, userAgent, method);
        }

        private static IWebQueryClient CreateWebQueryClient(IDictionary<string, string> headers,
                                                            WebParameterCollection parameters, string userAgent,
                                                            bool mockClient, IEnumerable<IModel> graph,
                                                            bool useTransparentProxy, string method)
        {
            return
#if !SILVERLIGHT
                mockClient
                ? (IWebQueryClient) new WebQueryClientMock(graph) :
                new WebQueryClient(headers, parameters, userAgent, method);
#else
                new WebQueryClient(headers, parameters, userAgent, useTransparentProxy);
#endif
        }

        protected abstract void SetAuthorizationHeader(WebRequest request, string header);

        protected virtual void ExecuteGetAsync(string url)
        {
            WebResponse = null;

            var client = CreateWebQueryClient();

            client.OpenReadCompleted += client_OpenReadCompleted;
            try
            {
                var args = new WebQueryRequestEventArgs(url);
                OnQueryRequest(args);

                client.OpenReadAsync(new Uri(url));
            }
            catch (WebException ex)
            {
                client.Exception = ex;
                HandleWebException(ex);
            }
            catch (NullReferenceException)
            {
                // Can happen on DNS failures following a WebException.
                // Client should already have the exception info for the WebException
            }
        }

        protected virtual void PostAsyncRequestCallback(IAsyncResult asyncResult)
        {
            WebRequest request;
            byte[] content;
            Triplet<IClientCache, object, string> store;

            var state = asyncResult.AsyncState as Pair<WebRequest, byte[]>;
            if (state == null)
            {
                // no expiration specified
                if (asyncResult is Pair<WebRequest, Triplet<byte[], IClientCache, string>>)
                {
                    var cacheScheme = (Pair<WebRequest, Triplet<byte[], IClientCache, string>>) asyncResult;
                    var cache = cacheScheme.Second.Second;

                    var url = cacheScheme.First.RequestUri.ToString();
                    var prefix = cacheScheme.Second.Third;
                    var key = CreateCacheKey(prefix, url);

                    var fetch = cache.Get<string>(key);
                    if (fetch != null)
                    {
                        var args = new WebQueryResponseEventArgs(fetch);
                        OnQueryResponse(args);
                        return;
                    }

                    request = cacheScheme.First;
                    content = cacheScheme.Second.First;
                    store = new Triplet<IClientCache, object, string> {First = cache, Second = null, Third = prefix};
                }
                else
                    // absolute expiration specified
                    if (asyncResult is Pair<WebRequest, Pair<byte[], Triplet<IClientCache, DateTime, string>>>)
                    {
                        var cacheScheme =
                            (Pair<WebRequest, Pair<byte[], Triplet<IClientCache, DateTime, string>>>) asyncResult;
                        var url = cacheScheme.First.RequestUri.ToString();
                        var cache = cacheScheme.Second.Second.First;
                        var expiry = cacheScheme.Second.Second.Second;

                        var prefix = cacheScheme.Second.Second.Third;
                        var key = CreateCacheKey(prefix, url);

                        var fetch = cache.Get<string>(key);
                        if (fetch != null)
                        {
                            var args = new WebQueryResponseEventArgs(fetch);
                            OnQueryResponse(args);
                            return;
                        }

                        request = cacheScheme.First;
                        content = cacheScheme.Second.First;
                        store = new Triplet<IClientCache, object, string>
                                    {First = cache, Second = expiry, Third = prefix};
                    }
                    else
                        // sliding expiration specified
                        if (asyncResult is Pair<WebRequest, Pair<byte[], Triplet<IClientCache, TimeSpan, string>>>)
                        {
                            var cacheScheme =
                                (Pair<WebRequest, Pair<byte[], Triplet<IClientCache, TimeSpan, string>>>) asyncResult;
                            var url = cacheScheme.First.RequestUri.ToString();
                            var cache = cacheScheme.Second.Second.First;
                            var expiry = cacheScheme.Second.Second.Second;

                            var prefix = cacheScheme.Second.Second.Third;
                            var key = CreateCacheKey(prefix, url);

                            var fetch = cache.Get<string>(key);
                            if (fetch != null)
                            {
                                var args = new WebQueryResponseEventArgs(fetch);
                                OnQueryResponse(args);
                                return;
                            }

                            request = cacheScheme.First;
                            content = cacheScheme.Second.First;
                            store = new Triplet<IClientCache, object, string>
                                        {First = cache, Second = expiry, Third = prefix};
                        }
                        else
                        {
                            // unrecognized state signature
                            throw new ArgumentNullException("asyncResult",
                                                            "The asynchronous post failed to return its state");
                        }
            }
            else
            {
                request = state.First;
                content = state.Second;
                store = null;
            }

            // no cached response
            using (var stream = request.EndGetRequestStream(asyncResult))
            {
                if (content != null)
                {
                    stream.Write(content, 0, content.Length);
                }
                stream.Close();

                request.BeginGetResponse(PostAsyncResponseCallback,
                                         new Pair<WebRequest, Triplet<IClientCache, object, string>>
                                             {First = request, Second = store});
            }
        }

        protected virtual void PostAsyncResponseCallback(IAsyncResult asyncResult)
        {
            var state = asyncResult.AsyncState as Pair<WebRequest, Triplet<IClientCache, object, string>>;
            if (state == null)
            {
                throw new ArgumentNullException("asyncResult", "The asynchronous post failed to return its state");
            }

            var request = state.First;
            if (request == null)
            {
                throw new ArgumentNullException("asyncResult", "The asynchronous post failed to return a request");
            }

            try
            {
                // Avoid disposing until no longer needed to build results
                var response = request.EndGetResponse(asyncResult);
                WebResponse = response;

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    if (state.Second != null)
                    {
                        var cache = state.Second.First;
                        var expiry = state.Second.Second;
                        var url = request.RequestUri.ToString();

                        var prefix = state.Second.Third;
                        var key = CreateCacheKey(prefix, url);

                        if (expiry is DateTime)
                        {
                            // absolute
                            cache.Insert(key, result, (DateTime) expiry);
                        }

                        if (expiry is TimeSpan)
                        {
                            // sliding
                            cache.Insert(key, result, (TimeSpan) expiry);
                        }
                    }

                    var args = new WebQueryResponseEventArgs(result);
                    OnQueryResponse(args);
                }
            }
            catch (WebException ex)
            {
                HandleWebException(ex);
            }
        }

        protected virtual IAsyncResult ExecutePostOrPutAsync(PostOrPut method, string url)
        {
            WebResponse = null;

            byte[] content;
            var request = BuildPostOrPutWebRequest(method, url, out content);

            var state = new Pair<WebRequest, byte[]> {First = request, Second = content};

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        protected virtual void ExecutePostOrPutAsync(PostOrPut method, string url,
                                                     IEnumerable<HttpPostParameter> parameters)
        {
            WebResponse = null;

            // Credit to Sean Erickson for providing a clean 
            // implementation of multi-part data posting
            byte[] content;
            var request = BuildMultiPartFormRequest(method, url, parameters, out content);

            var state = new Pair<WebRequest, byte[]> {First = request, Second = content};

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        // expects real cache key, not prefix
        private void ExecuteGetAsyncAndCache(IClientCache cache, string key, string url, IWebQueryClient client)
        {
            var fetch = cache.Get<string>(key);

            if (fetch != null)
            {
                var args = new WebQueryResponseEventArgs(fetch);
                OnQueryResponse(args);
            }
            else
            {
                var state = new Pair<IClientCache, string>
                                {
                                    First = cache,
                                    Second = key
                                };

                var args = new WebQueryRequestEventArgs(url);
                OnQueryRequest(args);

                client.OpenReadCompleted += client_OpenReadCompleted;
                client.OpenReadAsync(new Uri(url), state);
            }
        }

        // expects real cache key, not prefix
        private void ExecuteGetAsyncAndCacheWithExpiry(IClientCache cache, string key, string url,
                                                       DateTime absoluteExpiration, IWebQueryClient client)
        {
            var fetch = cache.Get<string>(key);

            if (fetch != null)
            {
                var args = new WebQueryResponseEventArgs(fetch);
                OnQueryResponse(args);
            }
            else
            {
                var state = new Pair<IClientCache, Pair<string, DateTime>>
                                {
                                    First = cache,
                                    Second = new Pair<string, DateTime> {First = key, Second = absoluteExpiration}
                                };

                var args = new WebQueryRequestEventArgs(url);
                OnQueryRequest(args);

                client.OpenReadCompleted += client_OpenReadCompleted;
                client.OpenReadAsync(new Uri(url), state);
            }
        }

        // expects real cache key, not prefix
        private void ExecuteGetAsyncAndCacheWithExpiry(IClientCache cache, string key, string url,
                                                       TimeSpan slidingExpiration, IWebQueryClient client)
        {
            var fetch = cache.Get<string>(key);

            if (fetch != null)
            {
                var args = new WebQueryResponseEventArgs(fetch);
                OnQueryResponse(args);
            }
            else
            {
                var state = new Pair<IClientCache, Pair<string, TimeSpan>>
                                {
                                    First = cache,
                                    Second = new Pair<string, TimeSpan> {First = key, Second = slidingExpiration}
                                };

                var args = new WebQueryRequestEventArgs(url);
                OnQueryRequest(args);

                client.OpenReadCompleted += client_OpenReadCompleted;
                client.OpenReadAsync(new Uri(url), state);
            }
        }

        protected virtual void ExecuteGetAsync(string url, string prefixKey, IClientCache cache)
        {
            WebResponse = null;

            var client = CreateWebQueryClient();
            var key = CreateCacheKey(prefixKey, url);

            ThreadPool.QueueUserWorkItem(work =>
                                         ExecuteGetAsyncAndCache(cache, key, url, client)
                );
        }

        protected virtual void ExecuteGetAsync(string url, string prefixKey, IClientCache cache,
                                               DateTime absoluteExpiration)
        {
            WebResponse = null;

            var client = CreateWebQueryClient();
            var key = CreateCacheKey(prefixKey, url);

            ThreadPool.QueueUserWorkItem(
                                            work =>
                                            ExecuteGetAsyncAndCacheWithExpiry(cache, key, url, absoluteExpiration,
                                                                              client)
                );
        }

        protected virtual void ExecuteGetAsync(string url, string prefixKey, IClientCache cache,
                                               TimeSpan slidingExpiration)
        {
            WebResponse = null;

            var client = CreateWebQueryClient();
            var key = CreateCacheKey(prefixKey, url);

            ThreadPool.QueueUserWorkItem(
                                            work =>
                                            ExecuteGetAsyncAndCacheWithExpiry(cache, key, url, slidingExpiration, client)
                );
        }

        /// <summary>
        /// Handles the OpenReadCompleted event of the client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        protected virtual void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            // TODO: AsyncCompletedEventArgs?
#if !SILVERLIGHT
            if (e.Error != null)
            {
                var args = new WebQueryResponseEventArgs(e.Error.Message, e.Error as WebException);
                OnQueryResponse(args);
            }
#endif
            try
            {
                using (var stream = e.Result)
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var result = reader.ReadToEnd();

                            WebResponse = ((IWebQueryClient) sender).Response;

                            if (e.UserState != null)
                            {
                                // no expiration specified
                                if (e.UserState is Pair<IClientCache, string>)
                                {
                                    var state = (e.UserState as Pair<IClientCache, string>);
                                    state.First.Insert(state.Second, result);
                                }

                                // absolute expiration specified
                                if (e.UserState is Pair<IClientCache, Pair<string, DateTime>>)
                                {
                                    var state = e.UserState as Pair<IClientCache, Pair<string, DateTime>>;
                                    state.First.Insert(state.Second.First, result, state.Second.Second);
                                }

                                // sliding expiration specified
                                if (e.UserState is Pair<IClientCache, Pair<string, TimeSpan>>)
                                {
                                    var state = e.UserState as Pair<IClientCache, Pair<string, TimeSpan>>;
                                    state.First.Insert(state.Second.First, result, state.Second.Second);
                                }
                            }

                            // only send query when caching is complete
                            var args = new WebQueryResponseEventArgs(result);
                            OnQueryResponse(args);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var message = HandleWebException(ex);
                var args = new WebQueryResponseEventArgs(message, ex);

                OnQueryResponse(args);
            }
        }

        protected virtual IAsyncResult ExecutePostOrPutAsync(PostOrPut method, string url, string key,
                                                             IClientCache cache)
        {
            WebResponse = null;

            byte[] content;
            var request = BuildPostOrPutWebRequest(method, url, out content);

            var state = new Pair<WebRequest, Triplet<byte[], IClientCache, string>>
                            {
                                First = request,
                                Second = new Triplet<byte[], IClientCache, string>
                                             {
                                                 First = content,
                                                 Second = cache,
                                                 Third = key
                                             }
                            };

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        protected virtual IAsyncResult ExecutePostOrPutAsync(PostOrPut method, string url, string prefixKey,
                                                             IClientCache cache, DateTime absoluteExpiration)
        {
            WebResponse = null;

            byte[] content;
            var request = BuildPostOrPutWebRequest(method, url, out content);

            var state = new Pair<WebRequest, Pair<byte[], Triplet<IClientCache, DateTime, string>>>
                            {
                                First = request,
                                Second = new Pair<byte[], Triplet<IClientCache, DateTime, string>>
                                             {
                                                 First = content,
                                                 Second = new Triplet<IClientCache, DateTime, string>
                                                              {
                                                                  First = cache,
                                                                  Second = absoluteExpiration,
                                                                  Third = prefixKey
                                                              }
                                             }
                            };

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        protected virtual IAsyncResult ExecutePostOrPutAsync(PostOrPut method, string url, string prefixKey,
                                                             IClientCache cache, TimeSpan slidingExpiration)
        {
            WebResponse = null;

            byte[] content;
            var request = BuildPostOrPutWebRequest(method, url, out content);

            var state = new Pair<WebRequest, Pair<byte[], Triplet<IClientCache, TimeSpan, string>>>
                            {
                                First = request,
                                Second = new Pair<byte[], Triplet<IClientCache, TimeSpan, string>>
                                             {
                                                 First = content,
                                                 Second = new Triplet<IClientCache, TimeSpan, string>
                                                              {
                                                                  First = cache,
                                                                  Second = slidingExpiration,
                                                                  Third = prefixKey
                                                              }
                                             }
                            };

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        private static string CreateCacheKey(string prefix, string url)
        {
            return !prefix.IsNullOrBlank() ? "{0}_{1}".FormatWith(prefix, url) : url;
        }

        protected virtual string ExecuteWithCache(IClientCache cache,
                                                  string url,
                                                  string key,
                                                  Func<IClientCache, string, string> cacheScheme)
        {
            var fetch = cache.Get<string>(CreateCacheKey(key, url));
            if (fetch != null)
            {
                return fetch;
            }

            var result = cacheScheme.Invoke(cache, url);
            return result;
        }

        protected virtual string ExecuteWithCacheAndAbsoluteExpiration(IClientCache cache,
                                                                       string url,
                                                                       string key,
                                                                       DateTime expiry,
                                                                       Func<IClientCache, string, DateTime, string>
                                                                           cacheScheme)
        {
            var fetch = cache.Get<string>(CreateCacheKey(key, url));
            if (fetch != null)
            {
                return fetch;
            }

            var result = cacheScheme.Invoke(cache, url, expiry);
            return result;
        }

        protected virtual string ExecuteWithCacheAndSlidingExpiration(IClientCache cache,
                                                                      string url,
                                                                      string key,
                                                                      TimeSpan expiry,
                                                                      Func<IClientCache, string, TimeSpan, string>
                                                                          cacheScheme)
        {
            var fetch = cache.Get<string>(CreateCacheKey(key, url));
            if (fetch != null)
            {
                return fetch;
            }

            var result = cacheScheme.Invoke(cache, url, expiry);
            return result;
        }

#if !SILVERLIGHT
        protected virtual string ExecuteGet(string url, string key, IClientCache cache, out WebException exception)
        {
            WebException ex = null; 
            var ret = ExecuteWithCache(cache, url, key, (c, u) => ExecuteGetAndCache(cache, url, key, out ex));
            exception = ex;
            return ret; 

        }

        protected virtual string ExecuteGet(string url, string key, IClientCache cache, DateTime absoluteExpiration, out WebException exception )
        {
            WebException ex = null; 
            var ret = ExecuteWithCacheAndAbsoluteExpiration(cache, url, key, absoluteExpiration,
                                                         (c, u, e) =>
                                                         ExecuteGetAndCacheWithExpiry(cache, url, key,
                                                                                      absoluteExpiration, out ex));
            exception = ex;
            return ret; 
        }

        protected virtual string ExecuteGet(string url, string key, IClientCache cache, TimeSpan slidingExpiration, out WebException exception )
        {
            WebException ex = null; 
            var ret = ExecuteWithCacheAndSlidingExpiration(cache, url, key, slidingExpiration,
                                                        (c, u, e) =>
                                                        ExecuteGetAndCacheWithExpiry(cache, url, key, slidingExpiration, out ex));
            exception = ex;
            return ret; 
        }

        private string ExecuteGetAndCache(IClientCache cache, string url, string key, out WebException exception )
        {
            
            var result = ExecuteGet(url, out exception);
            if (exception == null)
            {
                cache.Insert(CreateCacheKey(key, url), result);
            }
            return result;
        }

        private string ExecuteGetAndCacheWithExpiry(IClientCache cache, string url, string key,
                                                    DateTime absoluteExpiration, out WebException exception)
        {
            var result = ExecuteGet(url, out exception );
            if (exception == null)
            {
                cache.Insert(CreateCacheKey(key, url), result, absoluteExpiration);
            }
            return result;
        }

        private string ExecuteGetAndCacheWithExpiry(IClientCache cache, string url, string key,
                                                    TimeSpan slidingExpiration, out WebException exception)
        {
            
            var result = ExecuteGet(url, out exception);
            if (exception == null)
            {
                cache.Insert(CreateCacheKey(key, url), result, slidingExpiration);
            }
            return result;
        }

        // todo rather than always override this, use SetAuthorizationHeader with a virtual no-op
        protected virtual string ExecuteDelete(string url, out WebException exception )
        {
            WebResponse = null;
            exception = null; 
            var client = CreateWebQueryClientForDelete(url);
            var requestArgs = new WebQueryRequestEventArgs(url);
            OnQueryRequest(requestArgs);

            try
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();

                        var responseArgs = new WebQueryResponseEventArgs(result);
                        OnQueryResponse(responseArgs);

                        return result;
                    }
                }
            }
            catch (WebException ex)
            {
                exception = ex; 
                return HandleWebException(ex);
            }
            finally
            {
                WebResponse = client.Response;
            }
        }
#endif

        // todo rather than always override this, use SetAuthorizationHeader with a virtual no-op
        protected virtual IAsyncResult ExecuteDeleteAsync(string url)
        {
            WebResponse = null;

            var request = BuildDeleteWebRequest(url);

            var state = new Pair<WebRequest, byte[]> {First = request, Second = null};
            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        protected virtual IAsyncResult ExecuteDeleteAsync(string url, string key, IClientCache cache)
        {
            WebResponse = null;

            var request = BuildDeleteWebRequest(url);
            var state = new Pair<WebRequest, Triplet<byte[], IClientCache, string>>
                            {
                                First = request,
                                Second = new Triplet<byte[], IClientCache, string>
                                             {
                                                 First = null,
                                                 Second = cache,
                                                 Third = key
                                             }
                            };

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        protected virtual IAsyncResult ExecuteDeleteAsync(string url, string prefixKey, IClientCache cache,
                                                          DateTime absoluteExpiration)
        {
            WebResponse = null;

            var request = BuildDeleteWebRequest(url);

            var state = new Pair<WebRequest, Pair<byte[], Triplet<IClientCache, DateTime, string>>>
                            {
                                First = request,
                                Second = new Pair<byte[], Triplet<IClientCache, DateTime, string>>
                                             {
                                                 First = null,
                                                 Second = new Triplet<IClientCache, DateTime, string>
                                                              {
                                                                  First = cache,
                                                                  Second = absoluteExpiration,
                                                                  Third = prefixKey
                                                              }
                                             }
                            };

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        protected virtual IAsyncResult ExecuteDeleteAsync(string url, string prefixKey, IClientCache cache,
                                                          TimeSpan slidingExpiration)
        {
            WebResponse = null;

            var request = BuildDeleteWebRequest(url);

            var state = new Pair<WebRequest, Pair<byte[], Triplet<IClientCache, TimeSpan, string>>>
                            {
                                First = request,
                                Second = new Pair<byte[], Triplet<IClientCache, TimeSpan, string>>
                                             {
                                                 First = null,
                                                 Second = new Triplet<IClientCache, TimeSpan, string>
                                                              {
                                                                  First = cache,
                                                                  Second = slidingExpiration,
                                                                  Third = prefixKey
                                                              }
                                             }
                            };

            var args = new WebQueryRequestEventArgs(url);
            OnQueryRequest(args);

            return request.BeginGetRequestStream(PostAsyncRequestCallback, state);
        }

        /// <summary>
        /// Raises the <see cref="QueryResponse"/> event.
        /// </summary>
        /// <param name="args">The <see cref="TweetSharp.Core.Web.WebQueryResponseEventArgs"/> instance containing the event data.</param>
        public virtual void OnQueryResponse(WebQueryResponseEventArgs args)
        {
            if (QueryResponse != null)
            {
                QueryResponse(this, args);
            }
        }

        /// <summary>
        /// Occurs when a web query request is sent.
        /// </summary>
        public virtual event EventHandler<WebQueryRequestEventArgs> QueryRequest;

        /// <summary>
        /// Raises the <see cref="QueryRequest"/> event.
        /// </summary>
        /// <param name="args">The <see cref="TweetSharp.Core.Web.WebQueryRequestEventArgs"/> instance containing the event data.</param>
        public virtual void OnQueryRequest(WebQueryRequestEventArgs args)
        {
            if (QueryRequest != null)
            {
                QueryRequest(this, args);
            }
        }

        /// <summary>
        /// Occurs when a web query response is received.
        /// </summary>
        public virtual event EventHandler<WebQueryResponseEventArgs> QueryResponse;

#if !SILVERLIGHT
        public virtual void ExecuteStreamGet(string url, TimeSpan duration, int resultCount)
        {
            WebResponse = null;
            var client = CreateWebQueryClient();

            var requestArgs = new WebQueryRequestEventArgs(url);
            OnQueryRequest(requestArgs);

            Stream stream = null;

            try
            {
                using (stream = client.OpenRead(url))
                {
                    // [DC]: cannot refactor this block to common method; will cause wc/hwr to hang
                    var count = 0;
                    var results = new List<string>();
                    var start = DateTime.UtcNow;

                    using (var reader = new StreamReader(stream))
                    {
                        var line = "";

                        while ((line = reader.ReadLine()).Length > 0)
                        {
                            if (line.Equals(Environment.NewLine))
                            {
                                // Keep-Alive
                                continue;
                            }

                            if (line.Equals("<html>"))
                            {
                                // We're looking at a 401 or similar; construct error result?
                                return;
                            }

                            results.Add(line);
                            count++;

                            if (count < resultCount)
                            {
                                // Result buffer
                                continue;
                            }

                            var sb = new StringBuilder();
                            foreach (var result in results)
                            {
                                sb.AppendLine(result);
                            }

                            var responseArgs = new WebQueryResponseEventArgs(sb.ToString());
                            OnQueryResponse(responseArgs);

                            count = 0;

                            var now = DateTime.UtcNow;
                            if (duration == TimeSpan.Zero || now.Subtract(start) < duration)
                            {
                                continue;
                            }

                            // Time elapsed
                            client.CancelAsync();
                            return;
                        }

                        // Stream dried up
                    }
                    client.CancelAsync();
                }
            }
            catch (WebException ex)
            {
                client.Exception = client.Exception ?? ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }

                WebResponse = client.Response;
            }
        }

        public virtual void ExecuteStreamPost(PostOrPut method, string url, TimeSpan duration, int resultCount)
        {
            WebResponse = null;
            byte[] content;
            var request = BuildPostOrPutWebRequest(method, url, out content);

            var requestArgs = new WebQueryRequestEventArgs(url);
            OnQueryRequest(requestArgs);

            Stream stream = null;
            try
            {
                using (stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                    stream.Close();

                    var response = request.GetResponse();
                    WebResponse = response;
                    
                    using (var responseStream = response.GetResponseStream())
                    {
                        // [DC]: cannot refactor this block to common method; will cause hwr to hang
                        var count = 0;
                        var results = new List<string>();
                        var start = DateTime.UtcNow;

                        using (var reader = new StreamReader(responseStream))
                        {
                            var line = "";

                            while ((line = reader.ReadLine()).Length > 0)
                            {
                                if (line.Equals(Environment.NewLine))
                                {
                                    // Keep-Alive
                                    continue;
                                }

                                if (line.Equals("<html>"))
                                {
                                    // We're looking at a 401 or similar; construct error result?
                                    return;
                                }

                                results.Add(line);
                                count++;

                                if (count < resultCount)
                                {
                                    // Result buffer
                                    continue;
                                }

                                var sb = new StringBuilder();
                                foreach (var result in results)
                                {
                                    sb.AppendLine(result);
                                }

                                var responseArgs = new WebQueryResponseEventArgs(sb.ToString());
                                OnQueryResponse(responseArgs);

                                count = 0;

                                var now = DateTime.UtcNow;
                                if (now.Subtract(start) >= duration)
                                {
                                    // Time elapsed
                                    request.Abort();
                                    return;
                                }
                            }

                            // Stream dried up
                        }
                    }
                }
            }
            catch (WebException)
            {
                // 
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
#endif

        public virtual void ExecuteStreamGetAsync(string url, TimeSpan duration, int resultCount)
        {
            WebResponse = null;

            var client = CreateWebQueryClient();
            client.OpenReadCompleted += client_OpenReadStreamCompleted;

            var state = new Pair<TimeSpan, int>();
            state.First = duration;
            state.Second = resultCount;

            try
            {
                client.OpenReadAsync(new Uri(url), state);
            }
            catch (WebException ex)
            {
                client.Exception = ex;
                HandleWebException(ex);
            }
            catch (NullReferenceException)
            {
                // Can happen on DNS failures following a WebException.
                // Client should already have the exception info for the WebException
            }
        }

        protected virtual void client_OpenReadStreamCompleted(object sender, OpenReadCompletedEventArgs e)
        {
#if !SILVERLIGHT
            if (e.Error != null)
            {
                var args = new WebQueryResponseEventArgs(e.Error.Message, e.Error as WebException);
                OnQueryResponse(args);
            }
#endif
            var stream = e.Result;
            if (stream == null)
            {
                return;
            }

            var client = ((IWebQueryClient) sender);
            var state = (Pair<TimeSpan, int>) e.UserState;
            var duration = state.First;
            var resultCount = state.Second;

            try
            {
                using (stream = e.Result)
                {
                    if (stream != null)
                    {
                        var count = 0;
                        var results = new List<string>();
                        var start = DateTime.UtcNow;

                        using (var reader = new StreamReader(stream))
                        {
                            var line = "";

                            while ((line = reader.ReadLine()).Length > 0)
                            {
                                if (line.Equals(Environment.NewLine))
                                {
                                    // Keep-Alive
                                    continue;
                                }

                                if (line.Equals("<html>"))
                                {
                                    // We're looking at a 401 or similar; construct error result?
                                    return;
                                }

                                results.Add(line);
                                count++;

                                if (count < resultCount)
                                {
                                    // Result buffer
                                    continue;
                                }

                                var sb = new StringBuilder();
                                foreach (var result in results)
                                {
                                    sb.AppendLine(result);
                                }

                                var responseArgs = new WebQueryResponseEventArgs(sb.ToString());
                                OnQueryResponse(responseArgs);

                                count = 0;

                                var now = DateTime.UtcNow;
                                if (now.Subtract(start) >= duration)
                                {
                                    // Time elapsed
                                    client.CancelAsync();
                                    return;
                                }
                            }

                            // Stream dried up
                        }
                        client.CancelAsync();
                    }
                }
            }
            catch (WebException)
            {
                //    
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }

                WebResponse = client.Response;
            }
        }

#if !SILVERLIGHT
        protected virtual string ExecuteGet(string url, out WebException exception)
        {
            WebResponse = null;
            var client = CreateWebQueryClient();

            var requestArgs = new WebQueryRequestEventArgs(url);
            OnQueryRequest(requestArgs);

            try
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();

                        var responseArgs = new WebQueryResponseEventArgs(result);
                        OnQueryResponse(responseArgs);

                        return result;
                    }
                }
            }
            catch (WebException ex)
            {
                client.Exception = client.Exception ?? ex; 
                return HandleWebException(ex);
            }
            finally
            {
                exception = client.Exception;
                WebResponse = client.Response;
            }
        }

        public static string QuickGet(string url)
        {
            return QuickGet(url, null, null, null);
        }

        public static string QuickGet(string url, string username, string password)
        {
            return QuickGet(url, null, username, password);
        }

        public static string QuickGet(string url, IDictionary<string, string> headers, string username, string password)
        {
            var client = CreateWebQueryClient(headers, null, null, false, null, true);

            if (!username.IsNullOrBlank() && !password.IsNullOrBlank())
            {
                client.WebCredentials = new WebCredentials(username, password);
            }

            try
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                return null;
            }
        }
#endif

        protected virtual HttpWebRequest BuildMultiPartFormRequest(PostOrPut method, string url,
                                                                   IEnumerable<HttpPostParameter> parameters,
                                                                   out byte[] bytes)
        {
            var boundary = Guid.NewGuid().ToString();
            var request = (HttpWebRequest) WebRequest.Create(url);

#if !SILVERLIGHT
    // todo should be able to remove these anyway
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
#endif
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            request.Method = method == PostOrPut.Post ? "POST" : "PUT";

            var contents = BuildMultiPartFormRequestParameters(boundary, parameters);
            var payload = contents.ToString();

#if !Smartphone
            bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(payload);
#else
            bytes = Encoding.GetEncoding(1252).GetBytes(payload);
#endif

#if !SILVERLIGHT
            request.ContentLength = bytes.Length;
#endif
            return request;
        }

        protected static StringBuilder BuildMultiPartFormRequestParameters(string boundary,
                                                                           IEnumerable<HttpPostParameter> parameters)
        {
            var header = string.Format("--{0}", boundary);
            var footer = string.Format("--{0}--", boundary);
            var contents = new StringBuilder();

            foreach (var parameter in parameters)
            {
                contents.AppendLine(header);
                switch (parameter.Type)
                {
                    case HttpPostParameterType.File:
                        {
#if !Smartphone && !SILVERLIGHT
                            var fileBytes = File.ReadAllBytes(parameter.FilePath);
#else
                            byte[] fileBytes;
                            var info = new FileInfo(parameter.FilePath);
                            using (var fs = new FileStream(parameter.FilePath, FileMode.Open, FileAccess.Read))
                            {
                                using (var br = new BinaryReader(fs))
                                {
                                    fileBytes = br.ReadBytes((int) info.Length);
                                }
                            }
#endif
                            const string fileMask = "Content-Disposition: file; name=\"{0}\"; filename=\"{1}\"";
                            var fileHeader = fileMask.FormatWith(parameter.Name, parameter.FileName);
#if !Smartphone
                            var fileData = Encoding.GetEncoding("iso-8859-1").GetString(fileBytes, 0, fileBytes.Length);
#else
                            var fileData = Encoding.GetEncoding(1252).GetString(fileBytes, 0, fileBytes.Length);
#endif
                            contents.AppendLine(fileHeader);
                            contents.AppendLine("Content-Type: {0}".FormatWith(parameter.ContentType.ToLower()));
                            contents.AppendLine();
                            contents.AppendLine(fileData);

                            break;
                        }
                    case HttpPostParameterType.Field:
                        {
                            contents.AppendLine("Content-Disposition: form-data; name=\"{0}\"".FormatWith(parameter.Name));
                            contents.AppendLine();
                            contents.AppendLine(parameter.Value);
                            break;
                        }
                }
            }

            contents.AppendLine(footer);
            return contents;
        }

#if !SILVERLIGHT
        protected virtual string ExecutePostOrPut(PostOrPut method, string url, out WebException exception)
        {
            WebResponse = null;
            exception = null;
            byte[] content;
            var request = BuildPostOrPutWebRequest(method, url, out content);

            var requestArgs = new WebQueryRequestEventArgs(url);
            OnQueryRequest(requestArgs);

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                    stream.Close();

                    // Avoid disposing until no longer needed to build results
                    var response = request.GetResponse();
                    WebResponse = response;

                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = reader.ReadToEnd();

                        var responseArgs = new WebQueryResponseEventArgs(result);
                        OnQueryResponse(responseArgs);

                        return result;
                    }
                }
            }
            catch (WebException ex)
            {
                exception = ex; 
                return HandleWebException(ex);
            }
        }

        protected virtual string ExecutePostOrPut(PostOrPut method, string url, IEnumerable<HttpPostParameter> parameters)
        {
            WebResponse = null;
            byte[] bytes;
            var request = BuildMultiPartFormRequest(method, url, parameters, out bytes);

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Flush();
                requestStream.Close();

                // Avoid disposing until no longer needed to build results
                var response = request.GetResponse();
                WebResponse = response;

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();

                    var responseArgs = new WebQueryResponseEventArgs(result);
                    OnQueryResponse(responseArgs);

                    WebResponse = response;
                    return result;
                }
            }
        }
#endif

#if !SILVERLIGHT
        public virtual string Request(string url, out WebException exception)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    return ExecuteGet(url, out exception);
                case WebMethod.Put:
                    return ExecutePostOrPut(PostOrPut.Put, url, out exception);
                case WebMethod.Post:
                    return ExecutePostOrPut(PostOrPut.Post, url, out exception);
                case WebMethod.Delete:
                    return ExecuteDelete(url, out exception);
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

        public virtual string Request(string url, string key, IClientCache cache, out WebException exception)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    return ExecuteGet(url, key, cache, out exception);
                case WebMethod.Put:
                    return ExecutePostOrPut(PostOrPut.Put, url, key, cache, out exception);
                case WebMethod.Post: 
                    return ExecutePostOrPut(PostOrPut.Post, url, key, cache, out exception);
                case WebMethod.Delete:
                    // todo implement delete with cache (for mocking and completeness)
                    throw new NotImplementedException("HTTP DELETE not supported yet; use HTTP POST instead");
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

        public virtual string Request(string url, string key, IClientCache cache, DateTime absoluteExpiration, out WebException exception)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    return ExecuteGet(url, key, cache, absoluteExpiration, out exception);
                case WebMethod.Put:
                    return ExecutePostOrPut(PostOrPut.Put, url, key, cache, absoluteExpiration, out exception);
                case WebMethod.Post:
                    return ExecutePostOrPut(PostOrPut.Post, url, key, cache, absoluteExpiration, out exception);
                case WebMethod.Delete:
                    // todo implement delete with cache (for mocking and completeness)
                    throw new NotImplementedException("HTTP DELETE not supported yet; use HTTP POST instead");
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

        public virtual string Request(string url, string key, IClientCache cache, TimeSpan slidingExpiration, out WebException exception)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    return ExecuteGet(url, key, cache, slidingExpiration, out exception);
                case WebMethod.Put:
                    return ExecutePostOrPut(PostOrPut.Put, url, key, cache, slidingExpiration, out exception);
                case WebMethod.Post:
                    return ExecutePostOrPut(PostOrPut.Post, url, key, cache, slidingExpiration, out exception);
                case WebMethod.Delete:
                    // todo implement delete with cache (for mocking and completeness)
                    throw new NotImplementedException("HTTP DELETE not supported yet; use HTTP POST instead");
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

        public virtual string Request(string url, IEnumerable<HttpPostParameter> parameters)
        {
            switch (Method)
            {
                case WebMethod.Put:
                    return ExecutePostOrPut(PostOrPut.Put, url, parameters);
                case WebMethod.Post:
                    return ExecutePostOrPut(PostOrPut.Post, url, parameters);
                default:
                    throw new NotSupportedException("Only HTTP POSTs and PUTs can use post parameters");
            }
        }
#endif

        public virtual void RequestAsync(string url)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    ExecuteGetAsync(url);
                    break;
                case WebMethod.Put:
                    ExecutePostOrPutAsync(PostOrPut.Put, url);
                    break;
                case WebMethod.Post:
                    ExecutePostOrPutAsync(PostOrPut.Post, url);
                    break;
                case WebMethod.Delete:
                    ExecuteDeleteAsync(url);
                    break;
                default:
                    throw new NotSupportedException("Unknown web method");
            }
        }

        public virtual void RequestAsync(string url, string key, IClientCache cache)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    ExecuteGetAsync(url, key, cache);
                    break;
                case WebMethod.Put:
                    ExecutePostOrPutAsync(PostOrPut.Put, url, key, cache);
                    break;
                case WebMethod.Post:
                    ExecutePostOrPutAsync(PostOrPut.Post, url, key, cache);
                    break;
                case WebMethod.Delete:
                    ExecuteDeleteAsync(url, key, cache);
                    break;
                default:
                    throw new NotSupportedException(
                        "Unsupported web method: {0}".FormatWith(Method.ToUpper())
                        );
            }
        }

        public virtual void RequestAsync(string url, string key, IClientCache cache, DateTime absoluteExpiration)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    ExecuteGetAsync(url, key, cache, absoluteExpiration);
                    break;
                case WebMethod.Put:
                    ExecutePostOrPutAsync(PostOrPut.Put, url, key, cache, absoluteExpiration);
                    break;
                case WebMethod.Post:
                    ExecutePostOrPutAsync(PostOrPut.Post, url, key, cache, absoluteExpiration);
                    break;
                case WebMethod.Delete:
                    ExecuteDeleteAsync(url, key, cache, absoluteExpiration);
                    break;
                default:
                    throw new NotSupportedException(
                        "Unsupported web method: {0}".FormatWith(Method.ToUpper())
                        );
            }
        }

        public virtual void RequestAsync(string url, string key, IClientCache cache, TimeSpan slidingExpiration)
        {
            switch (Method)
            {
                case WebMethod.Get:
                    ExecuteGetAsync(url, key, cache, slidingExpiration);
                    break;
                case WebMethod.Post:
                    ExecutePostOrPutAsync(PostOrPut.Post, url, key, cache, slidingExpiration);
                    break;
                case WebMethod.Put:
                    ExecutePostOrPutAsync(PostOrPut.Put, url, key, cache, slidingExpiration);
                    break;
                case WebMethod.Delete:
                    ExecuteDeleteAsync(url, key, cache, slidingExpiration);
                    break;
                default:
                    throw new NotSupportedException(
                        "Unsupported web method: {0}".FormatWith(Method.ToUpper())
                        );
            }
        }

        public virtual void RequestAsync(string url, IEnumerable<HttpPostParameter> parameters)
        {
            switch (Method)
            {
                case WebMethod.Put:
                    ExecutePostOrPutAsync(PostOrPut.Put, url, parameters);
                    break;
                case WebMethod.Post:
                    ExecutePostOrPutAsync(PostOrPut.Post, url, parameters);
                    break;
                default:
                    throw new NotSupportedException("Only HTTP POSTS can use multi-part forms");
            }
        }

#if !SILVERLIGHT
        public virtual string ExecutePostOrPut( PostOrPut method, string url, string key, IClientCache cache, out WebException exception)
        {
            WebException ex = null; 
            var ret = ExecuteWithCache(cache, url, key, (c, u) => ExecutePostOrPutAndCache(method, cache, url, key, out ex));
            exception = ex;
            return ret; 
        }

        public virtual string ExecutePostOrPut(PostOrPut method, string url, string key, IClientCache cache, DateTime absoluteExpiration, out WebException exception)
        {
            WebException ex = null; 
            var ret = ExecuteWithCacheAndAbsoluteExpiration(cache, url, key, absoluteExpiration,
                                                         (c, u, e) =>
                                                         ExecutePostOrPutAndCacheWithExpiry(method, cache, url, key,
                                                                                       absoluteExpiration, out ex));
            exception = ex;
            return ret; 

        }

        public virtual string ExecutePostOrPut(PostOrPut method, string url, string key, IClientCache cache, TimeSpan slidingExpiration, out WebException exception )
        {
            WebException ex = null; 
            var ret = ExecuteWithCacheAndSlidingExpiration(cache, url, key, slidingExpiration,
                                                        (c, u, e) =>
                                                        ExecutePostOrPutAndCacheWithExpiry(method, cache, url, key, slidingExpiration, out ex));
            exception = ex; 
            return ret; 
        }

        private string ExecutePostOrPutAndCache(PostOrPut method, IClientCache cache, string url, string key, out WebException exception)
        {
            var result = ExecutePostOrPut(method, url, out exception);
            if (exception == null)
            {
                cache.Insert(CreateCacheKey(key, url), result);
            }
            return result;
        }

        private string ExecutePostOrPutAndCacheWithExpiry(PostOrPut method, IClientCache cache, string url, string key,
                                                     DateTime absoluteExpiration, out WebException exception)
        {
            var result = ExecutePostOrPut(method, url, out exception);
            if (exception == null)
            {
                cache.Insert(CreateCacheKey(key, url), result, absoluteExpiration);
            }
            return result;
        }

        private string ExecutePostOrPutAndCacheWithExpiry(PostOrPut method, IClientCache cache, string url, string key,
                                                     TimeSpan slidingExpiration, out WebException exception)
        {
            var result = ExecutePostOrPut(method, url, out exception);
            if (exception == null)
            {
                cache.Insert(CreateCacheKey(key, url), result, slidingExpiration);
            }
            return result;
        }
#endif
    }
}