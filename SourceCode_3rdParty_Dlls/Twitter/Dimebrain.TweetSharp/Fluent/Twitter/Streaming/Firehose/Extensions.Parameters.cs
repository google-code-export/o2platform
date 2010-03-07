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

namespace TweetSharp.Fluent
{
    public partial class Extensions
    {
        /// <summary>
        /// Requests a backlog of tweets before streaming live.
        /// Use a value between -150,000 and 150,000.
        /// If a negative value is passed, the stream will close after the backlog is sent.
        /// Note: You must have elevated access (greater than "default") to use this parameter.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Streaming-API-Documentation#count" />
        /// <param name="instance">The query chain.</param>
        /// <param name="count">The number of previous statuses to backlog.</param>
        /// <returns>The query chain.</returns>
        public static ITwitterStreamingFirehose WithBacklog(this ITwitterStreamingFirehose instance, int count)
        {
            if (count < -150000) count = -150000;
            if (count > 150000) count = 150000;

            instance.Root.StreamingParameters.Count = count;
            return instance;
        }

        public static ITwitterStreamingFirehose DelimitedBy(this ITwitterStreamingFirehose instance, int length)
        {
            instance.Root.StreamingParameters.Length = length;
            return instance;
        }
    }
}