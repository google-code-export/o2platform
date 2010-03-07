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

using System.Collections.Generic;
using System.Linq;
using TweetSharp.Extensions;
using TweetSharp.Model;
using TweetSharp.Model.Twitter.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TweetSharp.Core.Extensions
{
    public static class JsonExtensions
    {
        public static JProperty FindSingleChildProperty(this JToken startToken, string propertyName)
        {
            JProperty ret = null;
            var props = from JProperty p
                            in startToken.Children().OfType<JProperty>()
                        where p.Name.ToLower() == propertyName
                        select p;

            if (!props.Any())
            {
                foreach (var token in startToken.Children())
                {
                    ret = FindSingleChildProperty(token, propertyName);
                    if (ret != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                ret = props.ToArray()[0];
            }
            return ret;
        }

        public static JObject FindSingleChildObject(this JToken startToken, string objectName)
        {
            JObject ret = null;
            var props = from JObject o
                            in startToken.Children().OfType<JObject>()
                        where o["Name"].Value<string>() == objectName
                        select o;

            if (!props.Any())
            {
                foreach (var token in startToken.Children())
                {
                    ret = FindSingleChildObject(token, objectName);
                    if (ret != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                ret = props.ToArray()[0];
            }
            return ret;
        }

        public static string ToJson(this IModel instance)
        {
            var json = JsonConvert.SerializeObject(instance,
                                                   new TwitterDateTimeConverter(),
                                                   new TwitterWonkyBooleanConverter());

            return json;
        }

        public static string ToJson(this IEnumerable<IModel> collection)
        {
            var json = JsonConvert.SerializeObject(collection,
                                                   new TwitterDateTimeConverter(),
                                                   new TwitterWonkyBooleanConverter());

            return json;
        }

#if !SILVERLIGHT
        public static string ToJson(this TwitterResult result)
        {
            return TwitterExtensions.PreProcessXml(result.Response);
        }
#endif
    }
}