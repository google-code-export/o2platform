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
using System.ComponentModel;
using System.Linq;
using System.Net;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public abstract class TwitterNodeBase : ITwitterNode
    {
        protected TwitterNodeBase(IFluentTwitter root)
        {
            Root = root;
        }

        #region ITwitterNode Members

        public IFluentTwitter Root { get; private set; }

        public virtual IFluentTwitterConfiguration Configuration
        {
            get { return Root.Configuration; }
        }

        public WebResponse Response
        {
            get { return Root.Response; }
        }

        public bool HasError
        {
            get { return Root.HasError; }
        }

        public IFluentTwitter Expect(IEnumerable<IModel> graph)
        {
            return Root.Expect(graph);
        }

        public IFluentTwitter Expect(params IModel[] graph)
        {
            return Root.Expect(graph.AsEnumerable());
        }

        #endregion

        public override string ToString()
        {
            return Root.ToString();
        }
    }
}