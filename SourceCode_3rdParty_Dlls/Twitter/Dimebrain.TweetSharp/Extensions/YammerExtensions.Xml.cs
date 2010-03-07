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

using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using TweetSharp.Core.Extensions;
using TweetSharp.Model.Yammer.Converters;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace TweetSharp.Extensions
{
    partial class YammerExtensions
    {
        internal static string PreProcessXml(string json)
        {
            string xml;
            if (json.IsXml(out xml))
            {
#if Mono
    // [DC] Mono doesn't like this extension method
				json = FromXml(xml);
#else
                json = xml.FromXml();
#endif
            }
            return json;
        }

        private static string FromXml(this string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new YammerXmlNodeConverter());
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

        private static string CleanXml(string json)
        {
            const string yammerResponse = "{\"response\":";
            const string nestedSchool = "\"schools\":{\"school\":";
            const string nestedSchoolFixed = "\"schools\":{\"schoolobj\":";
            var nested = new[]
                             {
                                 "{\"schoolobj\":",
                                 "{\"previous_company\":",
                                 "{\"external_url\":",
                                 "{\"phone_number\":",
                                 "{\"email_address\":",
                                 "{\"message\":",
                                 "{\"subordinate\":",
                                 "{\"colleague\":",
                                 "{\"superior\":",
                                 "{\"attachment\":",
                                 "{\"network_domain\":",
                                 "{\"user\":",
                                 "{\"tag\":"
                             };
            var regex = new Regex(yammerResponse);
            var matchCount = regex.Matches(json).Count;

            if (json.TryReplace(yammerResponse, "", out json))
            {
                json = json.Substring(0, json.Length - matchCount); // "}"
            }
            json = json.Replace(nestedSchool, nestedSchoolFixed);
            foreach (var nest in nested)
            {
                json = RemoveNesting(json, nest);
            }
            return json;
        }

        private static string RemoveNesting(string json, string objectString)
        {
            int start;
            do
            {
                start = json.IndexOf(objectString);
                if (start > -1)
                {
                    var braces = 1;
                    var i = start + objectString.Length;
                    while (braces > 0)
                    {
                        if (json[i] == '{')
                        {
                            braces++;
                        }
                        if (json[i] == '}')
                        {
                            braces--;
                        }
                        i++;
                    }
                    if (json[start + objectString.Length] != '[')
                    {
                        json = json.Insert(start + objectString.Length, "[");
                        json = json.Insert(i, "]");
                        i += 2;
                    }
                    json = json.Remove(i - 1, 1);
                    json = json.Remove(start, objectString.Length);
                }
            } while (start > -1);
            return json;
        }
    }
}