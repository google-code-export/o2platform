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

namespace TweetSharp.Fluent
{
    public static partial class Extensions
    {
        public static ITwitterUserFriends For(this ITwitterUserFriends instance, string screenName)
        {
            instance.Root.Parameters.ScreenName = screenName;
            return instance;
        }

        public static ITwitterUserFriends For(this ITwitterUserFriends instance, int id)
        {
            instance.Root.Parameters.Id = id;
            return instance;
        }

        public static ITwitterUserFriends For(this ITwitterUserFriends instance, long id)
        {
            instance.Root.Parameters.Id = id;
            return instance;
        }

        /// <summary>
        /// Gets the default cursor used to return the first page of friends
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ITwitterUserFriends CreateCursor(this ITwitterUserFriends instance)
        {
            return instance.GetCursor(-1);
        }

        /// <summary>
        /// Gets the cursor used to return corresponding the page of friends 
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="cursor">The cursor of the page of friend/follower ids to get.</param>
        /// <returns></returns>
        public static ITwitterUserFriends GetCursor(this ITwitterUserFriends instance, long cursor)
        {
            instance.Root.Parameters.Cursor = cursor;
            return instance;
        }
    }
}