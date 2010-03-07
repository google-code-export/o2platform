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
using System.Reflection;
using TweetSharp.Core.Attributes;
using TweetSharp.Core.Web;
using TweetSharp.Core.Web.Query;

namespace TweetSharp.Core.Extensions
{
    internal static class WebExtensions
    {
        public static WebParameterCollection ParseParameters(this IWebQueryInfo info)
        {
            var parameters = new Dictionary<string, string>();
            var properties = info.GetType().GetProperties();

            info.ParseAttributes<ParameterAttribute>(properties, parameters);

            var collection = new WebParameterCollection();
            parameters.ForEach(p => collection.Add(new WebParameter(p.Key, p.Value)));

            return collection;
        }

        public static void ParseAttributes<T>(this IWebQueryInfo info, IEnumerable<PropertyInfo> properties,
                                              IDictionary<string, string> collection)
            where T : Attribute, INamedAttribute
        {
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes<T>(true);
                foreach (var attribute in attributes)
                {
                    var value = property.GetValue(info, null);
                    if (value == null)
                    {
                        continue;
                    }

                    var header = value.ToString();
                    if (!header.IsNullOrBlank())
                    {
                        collection.Add(attribute.Name, header);
                    }
                }
            }
        }

        public static string ToAuthorizationHeader(string username, string password)
        {
            var token = "{0}:{1}".FormatWith(username, password).GetBytes().ToBase64String();
            return "Basic {0}".FormatWith(token);
        }
    }
}