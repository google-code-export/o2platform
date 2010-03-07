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
using System.Net;
using System.Text;
using TweetSharp.Core.Extensions;

namespace TweetSharp.Core.Web.Query.Basic
{
    /// <summary>
    /// A web query engine for making requests that use basic HTTP authorization.
    /// </summary>
    internal class BasicAuthWebQuery : WebQueryBase
    {
        private readonly string _password;
        private readonly string _username;

        public BasicAuthWebQuery(IWebQueryInfo info, string username, string password) :
            this(info)
        {
            _username = username;
            _password = password;
        }

        public BasicAuthWebQuery(IWebQueryInfo info) :
            base(info)
        {
        }

        public bool HasAuth
        {
            get
            {
                return
                    (!_username.IsNullOrBlank()
                     && !String.IsNullOrEmpty(_password));
            }
        }

        protected override WebRequest BuildPostOrPutWebRequest(PostOrPut method, string url, out byte[] content)
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
                request.Headers["X-Twitter-Agent"] = UserAgent;
#endif
            }

            if (UseCompression)
            {
#if !SILVERLIGHT
                request.AutomaticDecompression = DecompressionMethods.GZip;
#else
                request.Headers["X-Twitter-Accept"] = "gzip,deflate";
#endif
            }
#if !SILVERLIGHT
            if (RequestTimeout.HasValue)
            {
                request.Timeout = (int) RequestTimeout.Value.TotalMilliseconds;
            }

            if (KeepAlive)
            {
                // Keep-Alive should be the default in Silverlight as-is
                request.KeepAlive = KeepAlive;
                if (RequestTimeout.HasValue)
                {
                    request.ReadWriteTimeout = (int)RequestTimeout.Value.TotalMilliseconds;
                }
            }
#endif
            SetAuthorizationHeader(request, "Authorization");
            AppendHeaders(request);

#if !SILVERLIGHT
            content = Encoding.ASCII.GetBytes(url);
            request.ContentLength = content.Length;
#else
            content = Encoding.UTF8.GetBytes(url);
#endif

            return request;
        }

        protected override void SetAuthorizationHeader(WebRequest request, string header)
        {
            if (!HasAuth)
            {
                return;
            }

            var credentials = WebExtensions.ToAuthorizationHeader(_username, _password);
            AuthorizationHeader = header;

#if !SILVERLIGHT
            request.PreAuthenticate = true;
            request.Headers[header] = credentials;
#else
            request.Headers["X-Twitter-Auth"] = AuthorizationHeader;
#endif
        }

        protected override HttpWebRequest BuildMultiPartFormRequest(PostOrPut method, string url,
                                                                    IEnumerable<HttpPostParameter> parameters,
                                                                    out byte[] bytes)
        {
            var boundary = Guid.NewGuid().ToString();
            var request = (HttpWebRequest) WebRequest.Create(url);

#if !SILVERLIGHT
    // todo we can probably remove these anyway
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
#endif
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            request.Method = method == PostOrPut.Post ? "POST" : "PUT";

            SetAuthorizationHeader(request, "Authorization");

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

#if !SILVERLIGHT
        protected override string ExecuteDelete(string url, out WebException exception)
        {
            WebResponse = null;
            exception = null; 
            var client = CreateWebQueryClientForDelete(url);
            if (HasAuth)
            {
                client.WebCredentials = new WebCredentials(_username, _password);
            }

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
                client.Exception = ex; 
                return HandleWebException(ex);
            }
            finally
            {
                exception = client.Exception;
                if (WebResponse == null || client.Response != null)
                {
                    WebResponse = client.Response;
                }
                if (WebResponse == null)
                {
                    WebResponse = client.GetWebResponseShim(client.Request);
                }
            }
        }
#endif

#if !SILVERLIGHT
        protected override string ExecuteGet(string url, out WebException exception)
        {
            WebResponse = null;
            exception = null;
            
            var client = CreateWebQueryClient();
            if (HasAuth)
            {
                client.WebCredentials = new WebCredentials(_username, _password);
            }

            var requestArgs = new WebQueryRequestEventArgs(url);
            OnQueryRequest(requestArgs);

            // todo too much code duplication with base class
            try
            {
                using (var stream = client.OpenRead(url))
                {
                    exception = client.Exception; 
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
                exception = client.Exception ?? ex;
                return HandleWebException(ex);
            }
            finally
            {
                if (WebResponse == null || client.Response != null)
                {
                    WebResponse = client.Response;
                }
                if ( WebResponse == null && client.Request != null )
                {
                    WebResponse = client.GetWebResponseShim(client.Request);
                }
            }
        }

        public override void ExecuteStreamGet(string url, TimeSpan duration, int resultCount)
        {
            WebResponse = null;
            var client = CreateWebQueryClient();
            if (HasAuth)
            {
                client.WebCredentials = new WebCredentials(_username, _password);
            }

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
#endif

        //#if !Smartphone
        protected override void ExecuteGetAsync(string url)
        {
            WebResponse = null;

            var client = CreateWebQueryClient();

            if (HasAuth)
            {
                client.WebCredentials = new WebCredentials(_username, _password);
            }

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

//#endif

        protected override WebRequest BuildDeleteWebRequest(string url)
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

            SetAuthorizationHeader(request, "Authorization");
            AppendHeaders(request);
            return request;
        }
    }
}