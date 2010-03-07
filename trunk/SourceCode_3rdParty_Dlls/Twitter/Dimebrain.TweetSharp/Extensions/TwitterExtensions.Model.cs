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
using System.IO;
using System.Linq;
using System.Text;
using TweetSharp.Core.Extensions;
using TweetSharp.Model;
using TweetSharp.Model.Twitter;
using TweetSharp.Model.Twitter.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if !Smartphone && !SILVERLIGHT

#endif

namespace TweetSharp.Extensions
{
    public static partial class TwitterExtensions
    {
        private static readonly JsonSerializerSettings _settings =
            new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    Converters = (new JsonConverter[]
                                      {
                                          new TwitterDateTimeConverter(),
                                          new TwitterWonkyBooleanConverter(),
                                          new TwitterGeoLocationConverter()
                                      })
                };

        internal static string ToTwitterResponseString(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return string.Empty;
            }

            if (value.First() == '"')
            {
                value = value.Substring(1);
            }

            if (value.Last() == '"')
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }

        internal static string ToTwitterDateString(this DateTime date)
        {
            // Tue, 27 Mar 2007 22:55:48 GMT
            var result = date.ToString("ddd, dd MMM yyyy H':'mm':'ss 'GMT'".UrlEncode(), CultureInfo.InvariantCulture);
            return result;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterUser" /> collection.
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>An user collection instance, or null if the input cannot cast to a user collection</returns>
        public static IEnumerable<TwitterUser> AsUsers(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            JContainer collection = null;
            if (result.Response.StartsWith("["))
            {
                collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
            }
            else
            {
                var jobj = JObject.Parse(result.Response);
                var usersJson = jobj.FindSingleChildProperty("users");
                if (usersJson != null)
                {
                    collection =
                        (JContainer) JsonConvert.DeserializeObject(usersJson.Children().ToArray()[0].ToString());
                }
            }
            if (collection == null)
            {
                return null;
            }

            var users = new List<TwitterUser>(0);
            foreach (var item in collection.Children())
            {
                try
                {
                    var user = item.ToString().Deserialize<TwitterUser>();
                    if (user == null)
                    {
                        return null;
                    }
                    users.Add(user);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return users;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterUser" />
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A user instance, or null if the input cannot cast to a user</returns>
        public static TwitterUser AsUser(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var user = result.Response.Deserialize<TwitterUser>();
            return user;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterList" />
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A user instance, or null if the input cannot cast to a list</returns>
        public static TwitterList AsList(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var list = result.Response.Deserialize<TwitterList>();
            return list;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterList" /> collection
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A status collection instance, or null if the input cannot cast to a list collection</returns>
        public static IEnumerable<TwitterList> AsLists(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var jObject = JObject.Parse(result.Response);
                var listsJson = jObject.FindSingleChildProperty("lists");
                if (listsJson != null)
                {
                    collection =
                        (JContainer) JsonConvert.DeserializeObject(listsJson.Children().ToArray()[0].ToString());
                }

                var lists = new List<TwitterList>();
                foreach (var item in collection.Children())
                {
                    try
                    {
                        var list = item.ToString().Deserialize<TwitterList>();
                        if (list == null)
                        {
                            return null;
                        }
                        lists.Add(list);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return lists;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterStatus" /> collection
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A status collection instance, or null if the input cannot cast to a status collection</returns>
        public static IEnumerable<TwitterStatus> AsStatuses(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

            // Streaming collector
            if (result.IsFromStream)
            {
                var responses = result.StreamedResponses;

                return new List<TwitterStatus>(
                    responses.Select(
                                        response => new TwitterResult {Response = response}.AsStatus()).Where(
                                                                                                                 status
                                                                                                                 =>
                                                                                                                 status !=
                                                                                                                 null)
                    );
            }

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var statuses = new List<TwitterStatus>();
                var children = collection.Children();
                foreach (var child in children)
                {
                    try
                    {
                        var status = child.ToString().Deserialize<TwitterStatus>();
                        if (status == null)
                        {
                            return null;
                        }
                        statuses.Add(status);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return statuses;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        public static IEnumerable<WhereOnEarthLocation> AsWhereOnEarthLocations(this TwitterResult result)
        {
            if (result.IsFailWhale)
            {
                return null;
            }

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var locations = new List<WhereOnEarthLocation>();
                var children = collection.Children();
                foreach (var child in children)
                {
                    try
                    {
                        var location = child.ToString().Deserialize<WhereOnEarthLocation>();
                        if (location == null)
                        {
                            return null;
                        }
                        locations.Add(location);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return locations;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }


        public static TwitterStatus AsStatus(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var status = result.Response.Deserialize<TwitterStatus>();
            return status;
        }

        public static TwitterSavedSearch AsSavedSearch(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var savedSearch = result.Response.Deserialize<TwitterSavedSearch>();
            return savedSearch;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterSavedSearch" /> collection
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A saved search collection instance, or null if the input cannot cast to a saved search collection</returns>
        public static IEnumerable<TwitterSavedSearch> AsSavedSearches(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var savedSearches = new List<TwitterSavedSearch>();
                var children = collection.Children();
                foreach (var child in children)
                {
                    try
                    {
                        var savedSearch = child.ToString().Deserialize<TwitterSavedSearch>();
                        if (savedSearch == null)
                        {
                            return null;
                        }
                        savedSearches.Add(savedSearch);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return savedSearches;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        public static TwitterFriendship AsFriendship(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var friendship = result.Response.Deserialize<TwitterFriendship>();
            return friendship;
        }

        public static IEnumerable<TwitterDirectMessage> AsDirectMessages(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
            if (collection == null)
            {
                return null;
            }

            var dms = new List<TwitterDirectMessage>();
            foreach (var item in collection.Children())
            {
                try
                {
                    var dm = item.ToString().Deserialize<TwitterDirectMessage>();
                    if (dm == null)
                    {
                        return null;
                    }
                    dms.Add(dm);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return dms;
        }

        public static TwitterDirectMessage AsDirectMessage(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var dm = result.Response.Deserialize<TwitterDirectMessage>();
            return dm;
        }

        public static TwitterRateLimitStatus AsRateLimitStatus(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            string response;
            if (result.Response.IsXml(out response))
            {
                return DeserializeRateLimitStatusXml(response);
            }
#endif

            var limit = result.Response.Deserialize<TwitterRateLimitStatus>();
            return limit;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterError" />
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>An error instance, or null if the input cannot cast to an error</returns>
        public static TwitterError AsError(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            string response;
            if (result.Response.IsXml(out response))
            {
                return DeserializeErrorXml(response);
            }
#endif
            var error = result.Response.Deserialize<TwitterError>();
            if (error == null || (error.ErrorMessage == null && error.Request == null))
            {
                return null;
            }
            return error;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterSearchResult" />
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A search result instance, or null if the input cannot cast to a search result</returns>
        public static TwitterSearchResult AsSearchResult(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var searchResult = result.Response.Deserialize<TwitterSearchResult>();
            return searchResult;
        }

#if !Mono
        // [DC] This method causes the Mono compiler to throw a stack overflow exception

        /// <summary>
        /// This method attempts to cast JSON string into a <see cref="TwitterSearchTrends" />
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The JSON input to convert</param>
        /// <returns>A search result instance, or null if the input cannot cast to a search result</returns>
        public static TwitterSearchTrends AsSearchTrends(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            //FIX: This isn't pretty, but it gets this stuff working
            //The problem is that the json returned from the search api has a 
            //dynamically named property (a date/time) that we can't know ahead
            //of time. That property is also the parent of the trend objects, 
            //so we can't ignore it either.  This removes that property from the
            //json and reparents the trend objects to the "trends" property 
            //before passing it off to the deserializer to get turned into 
            //objects in our model. 
            var fullResponseObject = JObject.Parse(result.Response);

            JProperty trendsProp;

            // get the outer scoping trends object
            var trendsProperties = from JProperty p
                                       in fullResponseObject.Properties()
                                   where p.Name.ToLower() == "trends"
                                   select p;

            var coalescedArray = new JArray();

            if (trendsProperties.Count() > 0)
            {
                trendsProp = trendsProperties.ToArray()[0];
                if (trendsProp.Value.Type != JTokenType.Array)
                {
                    var trendsObject = (JObject) trendsProp.Value;
                    foreach (var trendsArray in trendsObject.Properties())
                    {
                        DateTime trendingDate;
#if Smartphone 
                        bool parsedDate = true; 
                        try
                        {
                            trendingDate = DateTime.Parse(trendsArray.Name);
                        }
                        catch( FormatException /*ex*/ )
                        {
                            parsedDate = false; 
                            trendingDate = DateTime.MinValue;
                        }
#else
                        var parsedDate = DateTime.TryParse(trendsArray.Name, out trendingDate);
#endif
                        if (parsedDate)
                        {
                            foreach (JObject child in trendsArray.Value)
                            {
                                child.Add("trending_as_of",
                                          new JValue(TwitterDateTime.ConvertFromDateTime(trendingDate,
                                                                                         TwitterDateFormat.TrendsCurrent)));
                                coalescedArray.Add(child);
                            }
                        }
                    }
                    trendsProp.Value = coalescedArray;
                }
            }

            //convert the as_of date
            var asOf = fullResponseObject.FindSingleChildProperty("as_of");
            if (asOf != null)
            {
                var val = asOf.Value;
                if (val.Type == JTokenType.Integer)
                {
                    var date = new DateTime(1970, 1, 1).AddSeconds((int) val);
                    asOf.Value = new JValue(TwitterDateTime.ConvertFromDateTime(date, TwitterDateFormat.TrendsCurrent));
                }
            }

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            fullResponseObject.WriteTo(writer);
            writer.Close();
            stringWriter.Dispose();
            result.Response = sb.ToString();
            var trends = result.Response.Deserialize<TwitterSearchTrends>();
            return trends;
        }
#endif

        public static TwitterLocalTrends AsLocalTrends(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            // [DC]: Always in array form for some reason (global trends does not return multiple items)
            var trends = JArray.Parse(result.Response).FirstOrDefault();
            return trends == null
                       ? null
                       : trends.ToString().Deserialize<TwitterLocalTrends>();
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="List" />
        /// instance. If this method is not successful, an empty list is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>An Id list instance, or an emppty list if the input cannot cast to a list of Ids</returns>
        public static List<long> AsIds(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var ids = new List<long>();
            var jobj = JObject.Parse(result.Response);

            var prop = jobj.FindSingleChildProperty("ids");
            if (prop != null)
            {
                ids = prop.Value.ToString().Deserialize<List<long>>();
            }
            return ids;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into an arbitrary class instance. 
        /// If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>An T instance, or null if the input cannot cast to T</returns>
        public static T As<T>(this TwitterResult result)
        {
            if (result.IsFailWhale) return default(T);

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var instance = JsonConvert.DeserializeObject<T>(result.Response);
            return instance;
        }

        internal static T Deserialize<T>(this string json) where T : class
        {
            try
            {
                var deserialized = JsonConvert.DeserializeObject<T>(json, _settings);
                return deserialized;
            }
            catch (JsonSerializationException /*jsEx*/)
            {
                return null;
            }
            catch (JsonReaderException /*jrEx*/)
            {
                // Issue 12: Twitter might return a plain string.
                return null;
            }
            catch (Exception /*ex*/)
            {
                // Collections have issues converting to singles
                return null;
            }
        }

        /// <summary>
        /// This method attempts to cast a string response into an <see cref="OAuthToken" />.
        /// If unsuccessful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON result to convert</param>
        /// <returns>A token instance, or null if the result cannot cast to a token pair.</returns>
        public static OAuthToken AsToken(this TwitterResult result)
        {
            return result.IsFailWhale ? null : StringExtensions.AsToken(result);
        }

        /// This method attempts to cast an XML or JSON string into an <see cref="long"/> to be used with the paging of friend/follower ids. 
        /// If this method is not successful, 0 is returned.
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static long? AsNextCursor(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

            try
            {
#if !SILVERLIGHT
                result.Response = PreProcessXml(result.Response);
#endif
                var jobj = JObject.Parse(result.Response);
                const string cursor = "next_cursor";
                return FetchCursor(jobj, cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                return null;
            }
        }

        /// This method attempts to cast an XML or JSON string into an <see cref="long"/> to be used with the paging of friend/follower ids. 
        /// If this method is not successful, 0 is returned.
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static long? AsPreviousCursor(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

            try
            {
#if !SILVERLIGHT
                result.Response = PreProcessXml(result.Response);
#endif
                var jobj = JObject.Parse(result.Response);
                var cursor = "previous_cursor";
                return FetchCursor(jobj, cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                return null;
            }
        }

        private static long? FetchCursor(JToken token, string cursor)
        {
            long? result = null;

            var property = token.FindSingleChildProperty(cursor);
            long val;

#if !Smartphone
            if (long.TryParse(property.Value.ToString().Replace("\"", ""), out val))
            {
                result = val;
            }
#else
            try
            {
                var propertyValue = property.Value.ToString().Replace("\"", "").ToString();
                val = Convert.ToInt64(propertyValue, CultureInfo.InvariantCulture);
                result = val; 
            }
            catch (Exception)
            {
                throw;
            }
#endif
            return result;
        }
    }
}