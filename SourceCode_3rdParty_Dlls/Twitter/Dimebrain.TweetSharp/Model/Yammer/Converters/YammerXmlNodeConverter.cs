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
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TweetSharp.Model.Yammer.Converters
{
    internal class YammerXmlNodeConverter : XmlNodeConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var node = value as XmlNode;
            if (node != null)
            {
                var nodesToRemove = new List<XmlNode>();
                ReplaceDashesWithUnderscores(node, ref nodesToRemove);
                RemoveNilAttribute(node);
                nodesToRemove.ForEach(n => n.ParentNode.RemoveChild(n));
            }

            base.WriteJson(writer, node, serializer);
        }

        private static void ReplaceDashesWithUnderscores(XmlNode node, ref List<XmlNode> nodesToRemove)
        {
            //rename the node, replacing dashes with underscores
            if (node.Name.Contains("-"))
            {
                var newNode = node.OwnerDocument.CreateElement(node.Name.Replace("-", "_"));
                newNode.InnerXml = node.InnerXml;
                node.ParentNode.AppendChild(newNode);
                nodesToRemove.Add(node);
            }
            foreach (var child in node.ChildNodes.OfType<XmlNode>())
            {
                ReplaceDashesWithUnderscores(child, ref nodesToRemove);
            }
        }

        private static void RemoveNilAttribute(XmlNode node)
        {
            //remove the 'nil' attribute 
            if (node.Attributes != null && node.Attributes["nil"] != null)
            {
                node.Attributes.Remove(node.Attributes["nil"]);
            }
            foreach (var child in node.ChildNodes.OfType<XmlNode>())
            {
                RemoveNilAttribute(child);
            }
        }
    }
}