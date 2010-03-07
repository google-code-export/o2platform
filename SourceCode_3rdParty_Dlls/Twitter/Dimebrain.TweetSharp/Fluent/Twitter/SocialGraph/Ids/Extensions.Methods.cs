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

using TweetSharp.Core.Attributes;

namespace TweetSharp.Fluent
{
    partial class Extensions
    {
        /// <summary>
        /// Gets IDs of the friends of the authenticated user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFriends(this ITwitterSocialGraphIds instance)
        {
            instance.Root.Parameters.Activity = "friends";
            return instance;
        }

        /// <summary>
        /// Gets IDs of the friends of the specified user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the user to get the friends of.</param>
        /// <returns>The Instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFriendsOf(this ITwitterSocialGraphIds instance, int id)
        {
            instance.Root.Parameters.Activity = "friends";
            instance.Root.Parameters.UserId = id;
            return instance;
        }

        /// <summary>
        /// Gets IDs of the friends of the specified user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the user to get the friends of.</param>
        /// <returns>The Instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFriendsOf(this ITwitterSocialGraphIds instance, long id)
        {
            instance.Root.Parameters.Activity = "friends";
            instance.Root.Parameters.UserId = id;
            return instance;
        }

        /// <summary>
        /// Gets IDs of the friends of the specified user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenName">Screen name of user to get the friends of.</param>
        /// <returns>the Instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFriendsOf(this ITwitterSocialGraphIds instance, string screenName)
        {
            instance.Root.Parameters.Activity = "friends";
            instance.Root.Parameters.UserScreenName = screenName;
            return instance;
        }

        /// <summary>
        /// Gets the IDs of the followers of the authenticated user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFollowers(this ITwitterSocialGraphIds instance)
        {
            instance.Root.Parameters.Activity = "followers";
            return instance;
        }

        /// <summary>
        /// Gets the IDs of the followers of the specified user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the user whos followers to get.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFollowersOf(this ITwitterSocialGraphIds instance, int id)
        {
            instance.Root.Parameters.Activity = "followers";
            instance.Root.Parameters.UserId = id;
            return instance;
        }

        /// <summary>
        /// Gets the IDs of the followers of the specified user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the user whos followers to get.</param>
        /// <returns>the instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFollowersOf(this ITwitterSocialGraphIds instance, long id)
        {
            instance.Root.Parameters.Activity = "followers";
            instance.Root.Parameters.UserId = id;
            return instance;
        }

        /// <summary>
        /// Gets the IDs of the followers of the specified user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="screenName">The screen name of the user whos followers to get</param>
        /// <returns>the instance</returns>
        [RequiresAuthentication]
        public static ITwitterSocialGraphIds ForFollowersOf(this ITwitterSocialGraphIds instance, string screenName)
        {
            instance.Root.Parameters.Activity = "followers";
            instance.Root.Parameters.UserScreenName = screenName;
            return instance;
        }
    }
}