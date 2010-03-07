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
    /// <summary>
    /// 
    /// </summary>
    public static class YammerUsersExtensions
    {
        /// <summary>
        /// Gets all users in the network
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns></returns>
        public static IYammerUsers All(this IYammerUsers instance)
        {
            return instance;
        }

        /// <summary>
        /// Gets users that begin with the specified letter
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="startingWith">The letter to search for</param>
        /// <returns></returns>
        public static IYammerUsers StartingWith(this IYammerUsers instance, char startingWith)
        {
            instance.Root.Parameters.StartingWith = startingWith;
            return instance;
        }

        /// <summary>
        /// Gets the specified page of users (50 per page)
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="page">The requested page</param>
        /// <returns></returns>
        public static IYammerUsers Page(this IYammerUsers instance, int page)
        {
            instance.Root.Parameters.Page = page;
            return instance;
        }

        /// <summary>
        /// Gets users from the network sorted using the provided criterion
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="sortUsersBy">The desired sort</param>
        /// <returns></returns>
        public static IYammerUsersSorted SortedBy(this IYammerUsers instance, SortUsersBy sortUsersBy)
        {
            instance.Root.Parameters.SortUsersBy = sortUsersBy;
            return new YammerUsersSorted(instance.Root);
        }

        /// <summary>
        /// Gets the currently logged in user
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns></returns>
        public static IYammerUsersGet Current(this IYammerUsers instance)
        {
            instance.Root.Parameters.UseCurrentAsUserId = true;
            return new YammerUsersGet(instance.Root);
        }

        /// <summary>
        /// Gets a specific user by Id
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="userId">the id of the user to get</param>
        /// <returns></returns>
        public static IYammerUsersGet Get(this IYammerUsers instance, long userId)
        {
            instance.Root.Parameters.Id = userId;
            return new YammerUsersGet(instance.Root);
        }

        /// <summary>
        /// Gets a specific user by email address
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="emailAddress">the email address of the user to get</param>
        /// <returns></returns>
        public static IYammerUsersByEmail GetByEmail(this IYammerUsers instance, string emailAddress)
        {
            instance.Root.Parameters.Email = emailAddress;
            return new YammerUsersByEmail(instance.Root);
        }

        /// <summary>
        /// Creates a new user in the network (Requires admin status)
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="email">The email address of the user for the user</param>
        /// <returns></returns>
        public static IYammerUsersCreate Create(this IYammerUsers instance, string email)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.Email = email;
            return new YammerUsersCreate(instance.Root);
        }

        /// <summary>
        /// Creates a new user in the network (Requires admin status)
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="userData">a YammerUser object representing the user data</param>
        /// <returns></returns>
        public static IYammerUsersCreate Create(this IYammerUsers instance, YammerUser userData)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.UserData = userData;
            return new YammerUsersCreate(instance.Root);
        }


        /// <summary>
        /// Updates the currently active user's profile
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userData">a YammerUser object representing the user data to change</param>
        /// <returns></returns>
        public static IYammerUsersCreate UpdateCurrent(this IYammerUsers instance, YammerUser userData)
        {
            instance.Root.Method = WebMethod.Put;
            instance.Root.Parameters.UseCurrentAsUserId = true;
            instance.Root.Parameters.UserData = userData;
            return new YammerUsersCreate(instance.Root);
        }


        /// <summary>
        /// Updates the specified user's profile data (requires admin status).
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="id">The id of the user to update</param>
        /// <param name="userData">a YammerUser object representing the user data to change</param>
        /// <returns></returns>
        public static IYammerUsersCreate Update(this IYammerUsers instance, long id, YammerUser userData)
        {
            instance.Root.Method = WebMethod.Put;
            instance.Root.Parameters.UserData = userData;
            instance.Root.Parameters.Id = id;
            return new YammerUsersCreate(instance.Root);
        }
    }
}