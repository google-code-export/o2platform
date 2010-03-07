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
using System.Linq;
using TweetSharp.Core.Extensions;
using TweetSharp.Core.Web.Query;
using TweetSharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TweetSharp.Extensions
{
    public static partial class YammerExtensions
    {
        public static YammerError AsError(this YammerResult result)
        {
            if (result.Exception != null)
            {
                return new YammerError {ErrorMessage = result.Exception.Message};
            }
            return null;
        }

        /// <summary>
        /// This method attempts to cast a string response into an <see cref="OAuthToken" />.
        /// If unsuccessful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON result to convert</param>
        /// <returns>A token instance, or null if the result cannot cast to a token pair.</returns>
        public static OAuthToken AsToken(this YammerResult result)
        {
            return StringExtensions.AsToken(result);
        }

        public static YammerMessage AsMessage(this YammerResult result)
        {
            return result.AsMessages().FirstOrDefault();
        }

        public static IEnumerable<YammerGroup> AsGroups(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);

            return DeserializeGroups(json);
        }

        private static IEnumerable<YammerGroup> DeserializeGroups(string json)
        {
            try
            {
                var collection = (JArray) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }


                var groups = new List<YammerGroup>();
                foreach (var item in collection.Children())
                {
                    try
                    {
                        var group = item.ToString().Deserialize<YammerGroup>();
                        if (group == null)
                        {
                            return null;
                        }
                        groups.Add(group);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return groups;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static YammerUser AsUser(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                return json.Deserialize<YammerUser>();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static YammerAutoCompleteSuggestions AsAutoCompleteSuggestions(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                return json.Deserialize<YammerAutoCompleteSuggestions>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<YammerNetwork> AsNetworks(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                var nets = new List<YammerNetwork>();

                var jobject = JsonConvert.DeserializeObject(json);
                if ((jobject is JArray))
                {
                    var collection = (JArray) jobject;
                    foreach (var item in collection.Children())
                    {
                        try
                        {
                            var net = item.ToString().Deserialize<YammerNetwork>();
                            if (net == null)
                            {
                                return null;
                            }
                            nets.Add(net);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                            return null;
                        }
                    }
                }
                else
                {
                    var net = json.Deserialize<YammerNetwork>();
                    nets.Add(net);
                }
                return nets;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static YammerSearchResult AsSearchResult(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                var collection = (JObject) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }

                var messagesNode = collection["messages"];
                var messages = DeserializeMessages(messagesNode.ToString());
                var groupsNode = collection["groups"];
                var groups = DeserializeGroups(groupsNode.ToString());
                var tagsNode = collection["tags"];
                var tags = DeserializeTags(tagsNode.ToString());
                var usersNode = collection["users"];
                var users = DeserializeUsers(usersNode.ToString());

                return new YammerSearchResult
                           {
                               Users = users,
                               Messages = messages,
                               Groups = groups,
                               Tags = tags
                           };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static YammerRelationships AsRelationships(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                return json.Deserialize<YammerRelationships>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<YammerUser> AsUsers(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            return DeserializeUsers(json);
        }

        private static IEnumerable<YammerUser> DeserializeUsers(string json)
        {
            try
            {
                var collection = (JArray) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }
                var users = new List<YammerUser>();
                foreach (var item in collection.Children())
                {
                    try
                    {
                        var user = item.ToString().Deserialize<YammerUser>();
                        if (user == null)
                        {
                            return null;
                        }
                        users.Add(user);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static YammerTag AsTag(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                return json.Deserialize<YammerTag>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static YammerSubscription AsSubscription(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            try
            {
                return json.Deserialize<YammerSubscription>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<YammerTag> AsTags(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);
            return DeserializeTags(json);
        }

        private static IEnumerable<YammerTag> DeserializeTags(string json)
        {
            try
            {
                var collection = (JArray) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }


                var tags = new List<YammerTag>();
                foreach (var item in collection.Children())
                {
                    try
                    {
                        var tag = item.ToString().Deserialize<YammerTag>();
                        if (tag == null)
                        {
                            return null;
                        }
                        tags.Add(tag);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                        return null;
                    }
                }

                return tags;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<YammerMessage> AsMessages(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);

            return DeserializeMessages(json);
        }

        private static IEnumerable<YammerMessage> DeserializeMessages(string json)
        {
            try
            {
                var collection = (JObject) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }

                var messagesNode = collection["messages"];

                var messages = new List<YammerMessage>();
                //Single objects aren't returned in an array
                if (messagesNode.Type == JTokenType.Object)
                {
                    var message = messagesNode.ToString().Deserialize<YammerMessage>();
                    messages.Add(message);
                }
                else
                {
                    foreach (var item in messagesNode.Children())
                    {
                        try
                        {
                            var message = item.ToString().Deserialize<YammerMessage>();
                            if (message == null)
                            {
                                return null;
                            }
                            messages.Add(message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                            return null;
                        }
                    }
                }
                return messages;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static YammerResponseMetadata AsResponseMetadata(this YammerResult result)
        {
            var json = result.Response;
            json = PreProcessXml(json);

            try
            {
                var collection = (JObject) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }

                var metaNode = collection["meta"] ?? collection.First["meta"];
                var metaData = metaNode.ToString().Deserialize<YammerResponseMetadata>();
                metaData.TagReferences = result.AsTagReferences();
                metaData.UserReferences = result.AsUserReferences();
                metaData.ThreadReferences = result.AsThreadReferences();
                metaData.GuideReferences = result.AsGuideReferences();
                return metaData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static IEnumerable<T> GetReferences<T>(WebQueryResult result, string referenceTypeName) where T : class
        {
            try
            {
                var json = result.Response;
                json = PreProcessXml(json);
                var collection = (JObject) JsonConvert.DeserializeObject(json);
                if (collection == null)
                {
                    return null;
                }

                //will be nested one level deeper when converted from xml
                var messagesNode = collection["references"] ?? collection.First["references"];

                var references = new List<T>();
                //Single objects aren't returned in an array
                if (messagesNode.Type == JTokenType.Object)
                {
                    if (messagesNode["type"] != null
                        && messagesNode["type"] is JProperty
                        && ((JProperty) messagesNode["type"]).Value.ToString() == referenceTypeName)
                    {
                        var reference = messagesNode.ToString().Deserialize<T>();
                        references.Add(reference);
                    }
                }
                else
                {
                    foreach (var item in messagesNode.Children())
                    {
                        try
                        {
                            if (item["type"].Value<string>() == referenceTypeName)
                            {
                                var reference = item.ToString().Deserialize<T>();
                                if (reference != null)
                                {
                                    references.Add(reference);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                            return null;
                        }
                    }
                }
                return references;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<YammerTagReference> AsTagReferences(this YammerResult result)
        {
            return GetReferences<YammerTagReference>(result, "tag");
        }

        public static IEnumerable<YammerThreadReference> AsThreadReferences(this YammerResult result)
        {
            return GetReferences<YammerThreadReference>(result, "thread");
        }

        public static IEnumerable<YammerUserReference> AsUserReferences(this YammerResult result)
        {
            return GetReferences<YammerUserReference>(result, "user");
        }

        public static IEnumerable<YammerGuideReference> AsGuideReferences(this YammerResult result)
        {
            return GetReferences<YammerGuideReference>(result, "guide");
        }
    }
}