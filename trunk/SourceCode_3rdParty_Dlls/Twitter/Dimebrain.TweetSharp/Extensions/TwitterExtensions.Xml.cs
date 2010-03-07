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
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using TweetSharp.Core.Extensions;
using TweetSharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Formatting = Newtonsoft.Json.Formatting;

namespace TweetSharp.Extensions
{
    partial class TwitterExtensions
    {
        private static readonly Regex _isXml =
            new Regex("<\\?xml version=\"1\\.0\" encoding=\"UTF-8\"\\?>"
                      , RegexOptions.Compiled);

        private static readonly Regex _geoXml =
            new Regex(
                @"\{\s*""@xmlns:georss""\s*:\s*""http://www\.georss\.org/georss""\s*,\s*""georss:point""\s*:\s*""\s*([0123456789.-]+)\s+([0123456789.-]+)\s*""\s*\}\s*"
                , RegexOptions.Compiled);

        private static readonly Regex _geoJson =
            new Regex(
                @"\{\s*""type""\s*:\s*""Point""\s*,\s*""coordinates""\s*:\s*\[\s*([0123456789.-]+)\s*,\s*([0123456789.-]+)\s*\]\s*\}\s*"
                , RegexOptions.Compiled);

        internal static string PreProcessXml(string json)
        {
            string xml;
            if (json.IsXml(out xml))
            {
#if Mono
    // [DC] Mono doesn't like this specific extension method
				json = FromXml(xml);
#else
                json = xml.FromXml();
#endif
            }

            //FIX: Twitter seems to be transitioning between 'verified_profile' and 'verified'
            //standardize on 'verified' for our purposes
            json = json.Replace("\"verified_profile\":", "\"verified\":");

            json = PreProcessGeoData(json);

            return json;
        }

        private static string PreProcessGeoData(string json)
        {
            if (_geoXml.IsMatch(json))
            {
                var matches = _geoXml.Matches(json);
                foreach (Match match in matches)
                {
                    json = CleanGeo(json, match);
                }
            }

            if (_geoJson.IsMatch(json))
            {
                var matches = _geoJson.Matches(json);
                foreach (Match match in matches)
                {
                    json = CleanGeo(json, match);
                }
            }

            return json;
        }

        private static string CleanGeo(string json, Match geoBlock)
        {
            if (geoBlock.Groups.Count != 3)
            {
                return json;
            }

            var latitude = geoBlock.Groups[1].Value;
            var longitude = geoBlock.Groups[2].Value;

            const string geoFormat = "\"{0}, {1}\"";
            var formatted = geoFormat.FormatWith(latitude, longitude)
                .Replace("{{", "{").Replace("}}", "}");

            json = json.Replace(geoBlock.Value, formatted);
            return json;
        }

        internal static bool IsXml(this string json, out string xml)
        {
            // Since we only want a string, I don't want to incur exceptions thrown
            // each and every time in order to dictate branching; I also don't
            // want to validate each and every line of XML; the quickest way
            // for now is to just run a regex looking for the head tag
            const string head = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            var isXml = _isXml.IsMatch(json);
            xml = isXml ? json.Replace(head, String.Empty) : json;
            return isXml;
        }

        private static string FromXml(this string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new XmlNodeConverter());
            string json;
            using (var sw = new StringWriter())
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.None;
                    serializer.Serialize(jw, document);
                }

                json = sw.ToString();
            }

            return CleanXml(json);
        }

        private static TwitterRateLimitStatus DeserializeRateLimitStatusXml(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            // easier to use linq for rate limit status
            var hourlylimitNode = document.SelectSingleNode("/hash/hourly-limit");
            var hourly = hourlylimitNode != null ? hourlylimitNode.FirstChild : null;
            var remaininghitsNode = document.SelectSingleNode("/hash/remaining-hits");
            var hits = remaininghitsNode != null ? remaininghitsNode.FirstChild : null;
            var resettimeinsecondsNode = document.SelectSingleNode("/hash/reset-time-in-seconds");
            var seconds = resettimeinsecondsNode != null ? resettimeinsecondsNode.FirstChild : null;
            // var time = document.SelectSingleNode("/hash/reset-time").FirstChild;

            var hourlyValue = hourly != null && hourly.Value != null
                                  ? Convert.ToInt32(hourly.Value, CultureInfo.InvariantCulture)
                                  : 0;
            var hitsValue = hits != null && hits.Value != null
                                ? Convert.ToInt32(hits.Value, CultureInfo.InvariantCulture)
                                : 0;
            var secondsValue = seconds != null && seconds.Value != null
                                   ? Convert.ToInt64(seconds.Value, CultureInfo.InvariantCulture)
                                   : 0;

            var resetTime = secondsValue.FromUnixTime();
            var limit = new TwitterRateLimitStatus
                            {
                                HourlyLimit = hourlyValue,
                                RemainingHits = hitsValue,
                                ResetTimeInSeconds = secondsValue,
                                ResetTime = resetTime
                            };

            return limit;
        }

        private static TwitterError DeserializeErrorXml(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            // easier to use linq for rate limit status
            var requestNode = document.SelectSingleNode("/hash/request");
            var errorNode = document.SelectSingleNode("/hash/error");

            if (requestNode == null && errorNode == null)
            {
                return null;
            }

            var request = requestNode != null ? requestNode.FirstChild : null;
            var errorMessage = errorNode != null ? errorNode.FirstChild : null;

            var requestValue = request != null && request.Value != null ? request.Value : "";
            var errorValue = errorMessage != null && errorMessage.Value != null ? errorMessage.Value : "";

            var error = new TwitterError
                            {
                                Request = requestValue,
                                ErrorMessage = errorValue
                            };

            return error;
        }

        private static string CleanXml(string json)
        {
            const string users = "{\"users\":{\"@type\":\"array\",\"user\":";
            const string users_list = "{\"users_list\":" + users;
            const string statuses = "{\"statuses\":{\"@type\":\"array\",\"status\":";
            const string dms = "{\"direct-messages\":{\"@type\":\"array\",\"direct_message\":";
            const string user = "{\"user\":";
            const string status = "{\"status\":";
            const string dm = "{\"direct_message\":";
            const string emptyStatuses = "{\"statuses\":{\"@type\":\"array\"}}";
            const string emptyUsers = "{\"users\":{\"@type\":\"array\"}}";
            const string emptyDms = "{\"direct-messages\":{\"@type\":\"array\"}}";
            const string ids = "{\"ids\":{\"id\":";
            const string lists = "{\"lists_list\":";
            const string listsArray = "\"@type\":\"array\",";
            const string listsInner = "{\"list\":";
            const string listsWithCursorsEnd = "},\"next_cursor\"";
            const string listsWithCursorsEndCorrected = ",\"next_cursor\"";
            const string nullResult = "{\"nilclasses\":{\"@type\":\"array\"}}";

            const string emptyLocalTrendsAvailable = "{\"locations\":{\"@type\":\"array\"}}";
            const string localTrendsAvailable = "{\"locations\":{\"@type\":\"array\",\"location\":";
            const string localTrendsLocation = "{\"matching_trends\":{\"@type\":\"array\",\"trends\":{\"";

            if (json.TryReplace(users_list, "{\"users\":", out json))
            {
                var arrayEnd = json.LastIndexOf(']');
                var tmp = (json.Substring(0, arrayEnd + 1) + (json.Substring(arrayEnd + 2)));
                json = tmp;
                return json.Substring(0, json.Length - 1); // "}"
            }

            if (json.TryReplace(users, "", out json))
            {
                json = json.Substring(0, json.Length - 2); // "}}"
                json = string.Format("[{0}]", json);
                return json.Replace("[[", "[").Replace("]]", "]");
            }

            if (json.TryReplace(user, "", out json))
            {
                return json.Substring(0, json.Length - 1); // "}"
            }

            if (json.TryReplace(emptyUsers, "", out json))
            {
                return string.Format("[{0}]", json);
            }

            if (json.TryReplace(statuses, "", out json))
            {
                json = json.Substring(0, json.Length - 2); // "}}"
                json = string.Format("[{0}]", json);
                return json.Replace("[[", "[").Replace("]]", "]");
            }

            if (json.TryReplace(localTrendsLocation, "", out json))
            {
                json = json.Substring(0, json.Length - 2); // "}}"
                json = string.Format("[{0}]", json);
                json = json
                    .Replace("[@as_of", "[{\"as_of")
                    .Replace("@url", "url").Replace("@query", "query").Replace("#text", "text")
                    .Replace("\"trend\"", "\"trends\"")
                    .Replace("[[", "[").Replace("]]", "]");

                //"locations":[{"woeid":2487956,"name":"San Francisco"}],
                //"locations":{"location":{"woeid":"2487956","name":"San Francisco"}}

                var matches = Regex.Matches(json,
                                            @"""locations"":\{""location"":\{""woeid"":""([A-Za-z0-9-\s]*)"",""name"":""([A-Za-z-\s]*)""\}\}",
                                            RegexOptions.IgnoreCase |
                                            RegexOptions.IgnorePatternWhitespace |
                                            RegexOptions.Compiled);

                foreach (Match match in matches)
                {
                    if (match.Groups.Count != 3)
                    {
                        continue;
                    }

                    var locations = "\"locations\":[{{\"woeid\":{0},\"name\":\"{1}\"}}]"
                        .FormatWith(match.Groups[1].Value, match.Groups[2].Value);

                    locations = locations.Replace("{{", "{").Replace("}}", "}");
                    json = json.Replace(match.Value, locations);
                }

                return json;
            }

            if (json.TryReplace(localTrendsAvailable, "", out json))
            {
                /*
                 * {
                 * "woeid":"23424900",
                 * "name":"Mexico",
                 * "placeTypeName":{"code":"12","name":"Country"},
                 * "country":{"@type":"Country","code":"MX","name":"Mexico"},
                 * "url":"http://where.yahooapis.com/v1/place/23424900"}                  * 
                 */
                json = json.Substring(0, json.Length - 2); // "}}"
                json = string.Format("[{0}]", json);
                json = json
                    .Replace("@code", "code").Replace("#text", "name")
                    .Replace("placeTypeName", "placeType")
                    .Replace("[[", "[")
                    .Replace("]]", "]");

                // Regex to strip out {"@type":"Country","code":"MX","name": from country notifier
                var matches = Regex.Matches(json,
                                            @"\{""@type"":""Country"",""code"":""([A-Za-z-\s]*)"",""name"":""([A-Za-z-\s]*)""\}",
                                            RegexOptions.IgnoreCase |
                                            RegexOptions.IgnorePatternWhitespace |
                                            RegexOptions.Compiled);

                foreach (Match match in matches)
                {
                    if (match.Groups.Count != 3)
                    {
                        continue;
                    }

                    json = json.Replace(match.Value, "\"" + match.Groups[2].Value + "\"");
                }

                return json;
            }

            if (json.TryReplace(ids, "{\"ids\":", out json))
            {
                return json.Substring(0, json.Length - 1); // "}"
            }

            if (json.TryReplace(status, "", out json))
            {
                return json.Substring(0, json.Length - 1); // "}"
            }

            if (json.TryReplace(emptyStatuses, "", out json))
            {
                return string.Format("[{0}]", json);
            }

            if (json.TryReplace(nullResult, "", out json))
            {
                return string.Format("[{0}]", json);
            }

            if (json.TryReplace(dms, "", out json))
            {
                json = json.Substring(0, json.Length - 2); // "}}"
                json = string.Format("[{0}]", json);
                return json.Replace("[[", "[").Replace("]]", "]");
            }

            if (json.TryReplace(dm, "", out json))
            {
                return json.Substring(0, json.Length - 1); // "}"
            }

            if (json.TryReplace(emptyDms, "", out json))
            {
                return string.Format("[{0}]", json);
            }

            if (json.TryReplace(emptyLocalTrendsAvailable, "", out json))
            {
                return string.Format("[{0}]", json);
            }

            if (json.TryReplace(lists, "", out json))
            {
                json.TryReplace(listsArray, "", out json);
                json.TryReplace(listsInner, "", out json);
                json.TryReplace(listsWithCursorsEnd, listsWithCursorsEndCorrected, out json);

                return json.Substring(0, json.Length - 1);
            }
            if (json.TryReplace(listsInner, "", out json))
            {
                return json.Substring(0, json.Length - 1);
            }
            return json;
        }
    }
}