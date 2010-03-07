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
using TweetSharp.Core.Attributes;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Web.Query;
using Newtonsoft.Json;

namespace TweetSharp.Fluent.Twitter.Services
{
#if !SILVERLIGHT
    [Serializable]
    internal static class ShortenUrlService
    {
        public static string ShortenUrl(ShortenUrlServiceProvider provider, IEnumerable<string> words, string text)
        {
            return ShortenUrl(provider, words, text, null, null, null);
        }

        public static string ShortenUrl(ShortenUrlServiceProvider provider, IEnumerable<string> words, string text, string username, string password, string apiKey)
        {
            if(provider.RequiresAuthentication(true) && (username.IsNullOrBlank() || password.IsNullOrBlank()))
            {
                throw new TweetSharpException("Expected credentials for the shortening service {0}".FormatWith(provider));
            }

            var hasAuth = provider.RequiresAuthentication(false) && (!username.IsNullOrBlank() && !password.IsNullOrBlank());
            var hasApiKey = provider.RequiresApiKey() && (!apiKey.IsNullOrBlank());
            var scheme = provider.GetScheme();

            switch (provider)
            {
                case ShortenUrlServiceProvider.Tomato:
                    text = ExecuteGet(text, words, "http://to.m8.to/api/shortenLink?url={0}");
                    break;
                case ShortenUrlServiceProvider.Trim:
                {
                    var result = text;
                    var function = new Func<string, string, string>((response, word) =>
                                                                    {
                                                                        var data = new {url = String.Empty};
                                                                        data = JsonConvert.DeserializeAnonymousType(response, data);
                                                                        
                                                                        if (!data.url.IsNullOrBlank())
                                                                        {
                                                                            result = result.Replace(word, data.url);
                                                                        }
                                                                        return result;
                                                                    });
                    text = hasApiKey
                               ? ExecuteGet(text, words, "http://tr.im/api/trim_url.json?api_key={0}&url={{0}}".FormatWith(apiKey), function)
                               : hasAuth
                                     ? ExecuteGet(text, words, "http://tr.im/api/trim_url.json?url={0}", function, username, password, scheme)
                                     : ExecuteGet(text, words, "http://tr.im/api/trim_url.json?url={0}", function);
                    break;
                }
                case ShortenUrlServiceProvider.Bitly:
               { 
                    /* {
                                "errorCode": 0, 
                                "errorMessage": "", 
                                "results": {
                                    "http://www.dimebrain.com": {
                                        "hash": "wzJq", 
                                        "shortKeywordUrl": "", 
                                        "shortUrl": "http://bit.ly/5Dch", 
                                        "userHash": "5Dch"
                                    }
                                }, 
                                "statusCode": "OK"
                           }*/

                    var result = text;
                    var function = new Func<string, string, string>((response, word) =>
                                                                    {
                                                                        response = response.Replace("\"", "");
                                                                        response = response.Replace(word, "");
                                                                        response = response.Replace(",", "");

                                                                        var url = word;
                                                                        foreach (var token in response.Split(' '))
                                                                        {
                                                                            if (!token.IsValidUrl())
                                                                            {
                                                                                continue;
                                                                            }

                                                                            url = token;
                                                                            break;
                                                                        }

                                                                        if (!url.IsNullOrBlank())
                                                                        {
                                                                            result = result.Replace(word, url);
                                                                        }

                                                                        return result;
                                                                    });

                    text = ExecuteGet(text, words,
                                      "http://api.bit.ly/shorten?version=2.0.1&longUrl={0}&login={1}&apiKey={2}",
                                      function, username, password, scheme);
                    break;
                }
                case ShortenUrlServiceProvider.IsGd:
                {
                    text = ExecuteGet(text, words, "http://is.gd/api.php?longurl={0}");
                    break;
                }
                case ShortenUrlServiceProvider.TinyUrl:
                {
                    text = ExecuteGet(text, words, "http://tinyurl.com/api-create.php?url={0}");
                    break;
                }
                default:
                    throw new TweetSharpException("Unknown service provider for URL shortening");
            }

            return text;
        }

        private static string ExecuteGet(string text, IEnumerable<string> words, string query, Func<string, string, string> function)
        {
            return ExecuteGet(text, words, query, function, null, null, AuthenticationScheme.None);
        }

        private static string ExecuteGet(string text, IEnumerable<string> words, string query)
        {
            return ExecuteGet(text, words, query, null);
        }

        private static string ExecuteGet(string text, IEnumerable<string> words, string query, Func<string, string, string> function, string username, string password, AuthenticationScheme scheme)
        {
            foreach (var word in words)
            {
                if (!word.IsValidUrl())
                {
                    // not valid
                    continue;
                }

                if (word.IsShortenedUrl())
                {
                    // already shortened
                    continue;
                }

                var hasAuth = !username.IsNullOrBlank() && !password.IsNullOrBlank();

                string url;
                string response;

                if(hasAuth)
                {
                    switch(scheme)
                    {
                        case AuthenticationScheme.Http:
                            url = query.FormatWith(word.UrlEncode());
                            response = WebQueryBase.QuickGet(url, username, password);
                            break;
                        case AuthenticationScheme.Parameters:
                            url = query.FormatWith(word.UrlEncode(), username, password);
                            response = WebQueryBase.QuickGet(url);
                            break;
                        default:
                            throw new ArgumentException(
                                "Authentication was provided to shortening service call with no valid scheme.");
                    }
                }
                else
                {
                    url = query.FormatWith(word.UrlEncode());
                    response = WebQueryBase.QuickGet(url);
                }
                
                if(function != null)
                {
                    // post-process the http response
                    return function.Invoke(response, word);
                }

                if (response != null)
                {
                    // the response is the shortened url
                    text = text.Replace(word, response);
                }
            }
            return text;
        }
    }
#endif
}