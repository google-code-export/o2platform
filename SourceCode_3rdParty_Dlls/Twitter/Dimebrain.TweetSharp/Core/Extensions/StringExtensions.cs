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
using System.Text;
using System.Text.RegularExpressions;
using TweetSharp.Model;
#if SILVERLIGHT
using System.Windows.Browser;
#else
using System.Text.RegularExpressions;
using System.Web;
#endif

namespace TweetSharp.Core.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrBlank(this string value)
        {
            return String.IsNullOrEmpty(value) ||
                   (!String.IsNullOrEmpty(value) && value.Trim() == String.Empty);
        }

        public static bool AreNullOrBlank(this IEnumerable<string> values)
        {
            if (values.Count() == 0 || values == null)
            {
                return false;
            }

            var result = true;

            foreach (var value in values)
            {
                result &= value.IsNullOrBlank();
            }

            return result;
        }

        public static bool EqualsIgnoreCase(this string left, string right)
        {
            return String.Compare(left, right, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public static bool EqualsAny(this string input, params string[] args)
        {
            var result = false;
            foreach (var arg in args)
            {
                result |= input.Equals(arg);
            }
            return result;
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return String.Format(format, args);
        }

        public static string FormatWithInvariantCulture(this string format, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture, format, args);
        }

        public static string Then(this string input, string value)
        {
            return String.Concat(input, value);
        }

        public static string UrlEncode(this string value)
        {
            // This is more correct than HttpUtility; 
            // it escapes spaces as %20, not +
            return Uri.EscapeDataString(value);
        }

        public static string UrlDecode(this string value)
        {
#if !SILVERLIGHT
            return HttpUtility.UrlDecode(value);
#else
            // todo look into why HttpUtility is a better choice here
            return Uri.UnescapeDataString(value);
#endif
        }

        public static Uri AsUri(this string value)
        {
            return new Uri(value);
        }

        public static bool IsValidUrl(this string value)
        {
            const string pattern =
                "(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?";
            return value.Matches(pattern) && value.IsPrefixedByOneOf("http://", "https://", "ftp://");
        }

        public static bool IsShortenedUrl(this string value)
        {
            // todo: find or build a service that lets me check against known valid shortening services
            return value.IsValidUrl() &&
                   value.IsPrefixedByOneOf("http://to.m8.to", "http://tinyurl", "http://cli.gs", "http://zi.ma",
                                           "http://bit.ly", "http://is.gd", "http://snipurl", "http://poprl",
                                           "http://ad.vu", "http://tr.im", "http://budurl");
        }

        internal static bool IsPrefixedByOneOf(this string value, params string[] prefixes)
        {
            return value.IsPrefixedByOneOf(prefixes.ToList());
        }

        internal static bool IsPrefixedByOneOf(this string value, IEnumerable<string> prefixes)
        {
            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;

            foreach (var prefix in prefixes)
            {
                if (compareInfo.IsPrefix(value, prefix, CompareOptions.IgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        internal static string EnsurePrefixIsOneOf(this string value, params string[] prefixes)
        {
            return value.EnsurePrefixIsOneOf(prefixes.ToList());
        }

        internal static string EnsurePrefixIsOneOf(this string value, IEnumerable<string> prefixes)
        {
            var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
            var prefixed = false;
            foreach (var prefix in prefixes)
            {
                if (compareInfo.IsPrefix(value, prefix, CompareOptions.IgnoreCase))
                {
                    prefixed = true;
                }
            }

            if (!prefixed)
            {
                value = String.Concat(prefixes.First(), value);
            }
            return value;
        }

        public static string RemoveRange(this string input, int startIndex, int endIndex)
        {
            return input.Remove(startIndex, endIndex - startIndex);
        }

        public static bool TryReplace(this string input, string oldValue, string newValue, out string output)
        {
            var value = input.Replace(oldValue, newValue);
            output = value;

            return !output.Equals(input);
        }

        public static Guid AsGuid(this string input)
        {
            return new Guid(input);
        }

        public static OAuthToken AsToken(this TwitterResult result)
        {
            return result.Response.AsToken();
        }

        private static OAuthToken AsToken(this string response)
        {
#if !SILVERLIGHT
            var values = HttpUtility.ParseQueryString(response, Encoding.UTF8);

            var token = values["oauth_token"];
            var tokenSecret = values["oauth_token_secret"];
            var userId = values["user_id"];
            var screenName = values["screen_name"];
            var callbackConfirmed = values["oauth_callback_confirmed"];
#else
            var values = ParseQueryString(response, Encoding.UTF8);

            // [DC]: SL throws when a value in a NameValueCollection is null
            var keys = values.Keys;
            var token = keys.Contains("oauth_token") ? values["oauth_token"] : null;
            var tokenSecret = keys.Contains("oauth_token_secret") ? values["oauth_token_secret"] : null;
            var userId = keys.Contains("user_id") ? values["user_id"] : null;
            var screenName = keys.Contains("screen_name") ? values["screen_name"] : null;
            var callbackConfirmed = keys.Contains("oauth_callback_confirmed")
                                        ? values["oauth_callback_confirmed"]
                                        : null;
#endif

#if !Smartphone
            bool callbackConfirmedValue;
            var hasCallbackConfirmedValue = bool.TryParse(callbackConfirmed, out callbackConfirmedValue);
#else
            bool callbackConfirmedValue;
            bool hasCallbackConfirmedValue;
            try
            {
                callbackConfirmedValue = bool.Parse(callbackConfirmed);
                hasCallbackConfirmedValue = true;
            }
            catch (Exception)
            {
                callbackConfirmedValue = false;
                hasCallbackConfirmedValue = false;
            }
#endif
            // [DC]: Account for non-token results
            if (token.IsNullOrBlank() && tokenSecret.IsNullOrBlank())
            {
                // Not a token
                return null;
            }

            return new OAuthToken
                       {
                           Token = token,
                           TokenSecret = tokenSecret,
                           UserId = userId,
                           ScreenName = screenName,
                           CallbackConfirmed = hasCallbackConfirmedValue
                                                   ? callbackConfirmedValue
                                                   : false
                       };
        }

#if !Smartphone && !SILVERLIGHT
        public static OAuthToken AsToken(this YammerResult result)
        {
            return result.Response.AsToken(); 
        }
#endif

        public static string ToBase64String(this byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        public static byte[] GetBytes(this string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public static string PercentEncode(this string s)
        {
            var bytes = s.GetBytes();
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(string.Format("%{0:X}", b));
            }
            return sb.ToString();
        }

        public static T TryConvert<T>(this object instance)
        {
            var converted = default(T);
            try
            {
                if (instance != null)
                {
                    converted = (T) Convert.ChangeType(instance, typeof (T), CultureInfo.InvariantCulture);
                }
            }
            catch (InvalidCastException)
            {
                // Bad cast
            }
            catch (FormatException)
            {
                // Illegal value for the type i.e. "13" != bool
            }
            return converted;
        }

        public static IDictionary<string, string> ParseQueryString(this string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }

        public static IDictionary<string, string> ParseQueryString(this string query, Encoding encoding)
            // Note: From http://anonsvn.mono-project.com/viewvc/trunk/mcs/class/System.Web/System.Web/HttpUtility.cs
        {
            var result = new Dictionary<string, string>();
            if (query.Length == 0)
                return result;

            var decoded = HttpUtility.HtmlDecode(query);
            var decodedLength = decoded.Length;
            var namePos = 0;
            var first = true;
            while (namePos <= decodedLength)
            {
                int valuePos = -1, valueEnd = -1;
                for (var q = namePos; q < decodedLength; q++)
                {
                    if (valuePos == -1 && decoded[q] == '=')
                    {
                        valuePos = q + 1;
                    }
                    else if (decoded[q] == '&')
                    {
                        valueEnd = q;
                        break;
                    }
                }

                if (first)
                {
                    first = false;
                    if (decoded[namePos] == '?')
                        namePos++;
                }

                string name, value;
                if (valuePos == -1)
                {
                    name = null;
                    valuePos = namePos;
                }
                else
                {
                    name = UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1));
                }
                if (valueEnd < 0)
                {
                    namePos = -1;
                    valueEnd = decoded.Length;
                }
                else
                {
                    namePos = valueEnd + 1;
                }
                value = UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos));

                if (name != null)
                    result.Add(name, value);
                if (namePos == -1)
                    break;
            }
            return result;
        }

        private const RegexOptions Options =
#if !SILVERLIGHT
            RegexOptions.Compiled | RegexOptions.IgnoreCase;
#else
            RegexOptions.IgnoreCase;
#endif

        // Jon Gruber's URL Regex: http://daringfireball.net/2009/11/liberal_regex_for_matching_urls
        private static readonly Regex _parseUrls =
            new Regex(@"\b(([\w-]+://?|www[.])[^\s()<>]+(?:\([\w\d]+\)|([^\p{P}\s]|/)))", Options);

        // Diego Sevilla's @ Regex: http://stackoverflow.com/questions/529965/how-could-i-combine-these-regex-rules
        private static readonly Regex _parseMentions = new Regex(@"(^|\W)@([A-Za-z0-9_]+)", Options);

        // Simon Whatley's # Regex: http://www.simonwhatley.co.uk/parsing-twitter-usernames-hashtags-and-urls-with-javascript
        private static readonly Regex _parseHashtags = new Regex("[#]+[A-Za-z0-9-_]+", Options);

        public static string ParseTwitterageToHtml(this string input)
        {
            if (input.IsNullOrBlank())
            {
                return input;
            }

            foreach (Match match in _parseUrls.Matches(input))
            {
                input = input.Replace(match.Value,
                                      "<a href=\"{0}\" target=\"_blank\">{0}</a>".FormatWithInvariantCulture(match.Value));
            }

            foreach (Match match in _parseMentions.Matches(input))
            {
                if (match.Groups.Count != 3)
                {
                    continue;
                }

                var screenName = match.Groups[2].Value;
                var mention = "@" + screenName;

                input = input.Replace(mention,
                                      "<a href=\"http://twitter.com/{0}\" target=\"_blank\">{1}</a>".
                                          FormatWithInvariantCulture(screenName, mention));
            }

            foreach (Match match in _parseHashtags.Matches(input))
            {
                var hashtag = match.Value.UrlEncode();
                input = input.Replace(match.Value,
                                      "<a href=\"http://search.twitter.com/search?q={0}\" target=\"_blank\">{1}</a>".
                                          FormatWithInvariantCulture(hashtag, match.Value));
            }

            return input;
        }

        public static IEnumerable<Uri> ParseTwitterageToUris(this string input)
        {
            if (input.IsNullOrBlank())
            {
                yield break;
            }

            foreach (Match match in _parseUrls.Matches(input))
            {
                var url = match.Value;
                yield return new Uri(url);
            }
        }

        public static IEnumerable<string> ParseTwitterageToScreenNames(this string input)
        {
            if (input.IsNullOrBlank())
            {
                yield break;
            }

            foreach (Match match in _parseMentions.Matches(input))
            {
                if (match.Groups.Count != 3)
                {
                    continue;
                }

                var screenName = match.Groups[2].Value;
                yield return screenName;
            }
        }

        public static IEnumerable<string> ParseTwitterageToHashtags(this string input)
        {
            if (input.IsNullOrBlank())
            {
                yield break;
            }

            foreach (Match match in _parseHashtags.Matches(input))
            {
                yield return match.Value;
            }
        }
    }
}