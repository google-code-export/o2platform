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
    public static class ITwitterFriendshipsExtensions
    {
        [RequiresAuthentication]
        public static ITwitterFriendshipsCreate Befriend(this ITwitterFriendships instance, int id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsCreate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsCreate Befriend(this ITwitterFriendships instance, long id)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsCreate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsCreate Befriend(this ITwitterFriendships instance, string screenName)
        {
            instance.Root.Parameters.Action = "create";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterFriendshipsCreate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsDestroy Destroy(this ITwitterFriendships instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsDestroy Destroy(this ITwitterFriendships instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterFriendshipsDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsDestroy Destroy(this ITwitterFriendships instance, string screenName)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.ScreenName = screenName;
            return new TwitterFriendshipsDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsExists Verify(this ITwitterFriendships instance, string screenName)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterFriendshipsExists(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsExists Verify(this ITwitterFriendships instance, int id)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.UserId = id;
            return new TwitterFriendshipsExists(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsExists Verify(this ITwitterFriendships instance, long id)
        {
            instance.Root.Parameters.Action = "exists";
            instance.Root.Parameters.UserId = id;
            return new TwitterFriendshipsExists(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, long targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, int targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string sourceScreenName,
                                                   string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceScreenName = sourceScreenName;
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, int sourceId,
                                                   string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, long sourceId,
                                                   string targetScreenName)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetScreenName = targetScreenName;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, long sourceId, long targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, int sourceId, int targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceId = sourceId;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string sourceScreenName,
                                                   int targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceScreenName = sourceScreenName;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }

        public static ITwitterFriendshipsShow Show(this ITwitterFriendships instance, string sourceScreenName,
                                                   long targetId)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.SourceScreenName = sourceScreenName;
            instance.Root.Parameters.TargetId = targetId;
            return new TwitterFriendshipsShow(instance.Root);
        }
    }
}