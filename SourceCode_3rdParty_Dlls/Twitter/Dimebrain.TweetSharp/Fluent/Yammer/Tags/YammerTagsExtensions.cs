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
    public static class YammerTagsExtensions
    {
        /// <summary>
        /// Gets the details of a specific tag
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="tagId">The id of the tag to get the details for</param>
        /// <returns></returns>
        public static IYammerTagsGet Get(this IYammerTags instance, long tagId)
        {
            instance.Root.Parameters.Id = tagId;
            return new YammerTagsGet(instance.Root);
        }

        /// <summary>
        /// Gets all tags in the network.
        ///  (Listed as 'not implemented' in the Yammer API spec, but appears to work - may not give correct results)
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IYammerTagsAll All(this IYammerTags instance)
        {
            return new YammerTagsAll(instance.Root);
        }
    }
}