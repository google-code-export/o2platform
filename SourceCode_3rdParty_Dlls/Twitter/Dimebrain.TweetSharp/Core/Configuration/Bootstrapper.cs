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

using System.Net;
#if SILVERLIGHT3 || SILVERLIGHT4
using System.Net.Browser;
#endif

namespace TweetSharp.Core.Configuration
{
    internal static class Bootstrapper
    {
        public static void Run()
        {
#if !SILVERLIGHT
    // http://groups.google.com/group/twitter-development-talk/browse_thread/thread/7c67ff1a2407dee7
            ServicePointManager.Expect100Continue = false;
#endif
#if !SILVERLIGHT && !Smartphone
            ServicePointManager.UseNagleAlgorithm = false;
#endif

#if SILVERLIGHT3 || SILVERLIGHT4
            // [DC]: Opt-in to the networking stack so we can get headers for proxies
            WebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            WebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);
#endif
        }
    }
}