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

using TweetSharp.Core.Web;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    internal abstract class YammerLeafNodeBase : YammerNodeBase, IYammerLeafNode
    {
        protected YammerLeafNodeBase(IFluentYammer root) : base(root)
        {
        }

        #region IYammerLeafNode Members

        public string AsUrl()
        {
            return Root.AsUrl();
        }

        public IYammerLeafNode CallbackTo(YammerWebCallback callback)
        {
            Root.CallbackTo(callback);
            return this;
        }

        public YammerResult Request()
        {
            return Root.Request();
        }

        public void RequestAsync()
        {
            Root.RequestAsync();
        }

        #endregion

        // TODO: ensure formats are only exposed by interface

        public IYammerLeafNode AsXml()
        {
            Root.Format = WebFormat.Xml;
            return this;
        }

        public IYammerLeafNode AsJson()
        {
            Root.Format = WebFormat.Json;
            return this;
        }

        public IYammerLeafNode AsAtom()
        {
            Root.Format = WebFormat.Atom;
            return this;
        }

        public IYammerLeafNode AsRss()
        {
            Root.Format = WebFormat.Rss;
            return this;
        }
    }
}