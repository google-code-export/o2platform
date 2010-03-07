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
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// The base implementation for fluent query nodes on an <see cref="IFluentYammer" /> instance.
    /// </summary>
    internal abstract class YammerNodeBase : IYammerNode
    {
        protected YammerNodeBase(IFluentYammer root)
        {
            Root = root;
        }

        #region IYammerNode Members

        public IFluentYammer Root { get; private set; }

        public virtual IFluentYammerConfiguration Configuration
        {
            get { return Root.Configuration; }
        }


        public IFluentYammer Expect(IEnumerable<IModel> graph)
        {
            return Root.Expect(graph);
        }

        public IFluentYammer Expect(params IModel[] graph)
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