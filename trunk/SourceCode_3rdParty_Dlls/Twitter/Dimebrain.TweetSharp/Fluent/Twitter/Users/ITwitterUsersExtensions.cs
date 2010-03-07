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

#if !Smartphone && !SILVERLIGHT
using System.Net.Mail;
#endif
using TweetSharp.Core.Attributes;

namespace TweetSharp.Fluent
{
    public static class ITwitterUsersExtensions
    {
        [RequiresAuthentication]
        public static ITwitterUserFriends GetFriends(this ITwitterUsers instance)
        {
            instance.Root.Parameters.Activity = "statuses";
            instance.Root.Parameters.Action = "friends";
            return new TwitterUserFriends(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterUserFollowers GetFollowers(this ITwitterUsers instance)
        {
            instance.Root.Parameters.Activity = "statuses";
            instance.Root.Parameters.Action = "followers";
            return new TwitterUserFollowers(instance.Root);
        }

        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, int id)
        {
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Action = "show";
            return new TwitterUsersShow(instance.Root);
        }

        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, long id)
        {
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Action = "show";
            return new TwitterUsersShow(instance.Root);
        }

        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, string screenName)
        {
            instance.Root.Parameters.UserScreenName = screenName;
            instance.Root.Parameters.Action = "show";
            return new TwitterUsersShow(instance.Root);
        }

        public static ITwitterUsersSearch SearchFor(this ITwitterUsers instance, string query)
        {
            instance.Root.Parameters.Action = "search";
            instance.Root.Parameters.UserSearch = query;

            return new TwitterUsersSearch(instance.Root);
        }

#if !Smartphone && !SILVERLIGHT
        public static ITwitterUsersShow ShowProfileFor(this ITwitterUsers instance, MailAddress email)
        {
            instance.Root.Parameters.Email = email.Address;
            instance.Root.Parameters.Action = "show";
            return new TwitterUsersShow(instance.Root);
        }
#endif
    }
}