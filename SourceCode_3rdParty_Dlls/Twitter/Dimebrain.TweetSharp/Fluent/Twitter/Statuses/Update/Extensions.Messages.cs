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

using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// Extention Methods for Messages
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// Sets the ID of the tweet that this status is in reply to
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the tweet that this status is in reply to.</param>
        /// <returns>the instance</returns>
        public static ITwitterStatusUpdate InReplyToStatus(this ITwitterStatusUpdate instance, int id)
        {
            instance.Root.Parameters.InReplyToStatusId = id;
            return instance;
        }

        /// <summary>
        /// Sets the ID of the tweet that this status is in reply to
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the tweet that this status is in reply to.</param>
        /// <returns>the instance</returns>
        public static ITwitterStatusUpdate InReplyToStatus(this ITwitterStatusUpdate instance, long id)
        {
            instance.Root.Parameters.InReplyToStatusId = id;
            return instance;
        }

        /// <summary>
        /// Sets the ID of the tweet that this status is in reply to
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="status">The tweet that this status is in reply to.</param>
        /// <returns>the instance</returns>
        public static ITwitterStatusUpdate InReplyToStatus(this ITwitterStatusUpdate instance, TwitterStatus status)
        {
            instance.Root.Parameters.InReplyToStatusId = status.Id;
            return instance;
        }

        public static ITwitterStatusUpdate From(this ITwitterStatusUpdate instance, double latitude, double longitude)
        {
            instance.Root.Parameters.GeoLocation = new GeoLocation(latitude, longitude);
            return instance;
        }

        public static ITwitterStatusUpdate From(this ITwitterStatusUpdate instance, GeoLocation location)
        {
            instance.Root.Parameters.GeoLocation = location;
            return instance;
        }
    }
}