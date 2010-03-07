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
using TweetSharp.Core.Attributes;
using TweetSharp.Core.Extensions;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public static class ITwitterStatusesExtensions
    {
        public static ITwitterPublicTimeline OnPublicTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "public_timeline";
            return new TwitterPublicTimeline(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterFriendsTimeline OnFriendsTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "friends_timeline";
            return new TwitterFriendsTimeline(instance.Root);
        }

        public static ITwitterUserTimeline OnUserTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "user_timeline";
            return new TwitterUserTimeline(instance.Root);
        }

        public static ITwitterHomeTimeline OnHomeTimeline(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "home_timeline";
            return new TwitterHomeTimeline(instance.Root);
        }

        public static ITwitterListTimeline OnListTimeline(this ITwitterStatuses instance, string screenName,
                                                          string listSlug)
        {
            instance.Root.Parameters.Activity = "lists";
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterListTimeline(instance.Root);
        }

        public static ITwitterListTimeline OnListTimeline(this ITwitterStatuses instance, string screenName, int listId)
        {
            instance.Root.Parameters.Activity = "lists";
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterListTimeline(instance.Root);
        }

        public static ITwitterListTimeline OnListTimeline(this ITwitterStatuses instance, string screenName, long listId)
        {
            instance.Root.Parameters.Activity = "lists";
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserScreenName = screenName;
            return new TwitterListTimeline(instance.Root);
        }

        public static ITwitterStatusShow Show(this ITwitterStatuses instance, int id)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.Id = id;
            return new TwitterStatusShow(instance.Root);
        }

        public static ITwitterStatusShow Show(this ITwitterStatuses instance, long id)
        {
            instance.Root.Parameters.Action = "show";
            instance.Root.Parameters.Id = id;
            return new TwitterStatusShow(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusUpdate Update(this ITwitterStatuses instance, string text)
        {
            instance.Root.Parameters.Action = "update";
            instance.Root.Parameters.Text = text;
            return new TwitterStatusUpdate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusUpdate Update(this ITwitterStatuses instance, string text, double latitude,
                                                  double longitude)
        {
            var location = new GeoLocation {Latitude = latitude, Longitude = longitude};
            return instance.Update(text, location);
        }

        public static ITwitterStatusUpdate Update(this ITwitterStatuses instance, string text, GeoLocation location)
        {
            instance.Root.Parameters.Action = "update";
            instance.Root.Parameters.Text = text;
            instance.Root.Parameters.GeoLocation = location;
            return new TwitterStatusUpdate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, TwitterStatus status)
        {
            // todo switch this to native mode when it is released
            return instance.Retweet(status, RetweetMode.Prefix);
        }

        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, long statusId)
        {
            return instance.Retweet(new TwitterStatus {Id = statusId}, RetweetMode.Native);
        }

        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, long statusId, RetweetMode mode)
        {
            return instance.Retweet(new TwitterStatus {Id = statusId}, mode);
        }

        public static ITwitterRetweets RetweetsOf(this ITwitterStatuses instance, long statusId)
        {
            instance.Root.Parameters.Action = "retweets";
            instance.Root.Parameters.Id = statusId;
            return new TwitterRetweets(instance.Root);
        }

        public static ITwitterRetweets RetweetsOf(this ITwitterStatuses instance, TwitterStatus status)
        {
            return RetweetsOf(instance, status.Id);
        }

        [RequiresAuthentication]
        public static ITwitterStatusUpdate Retweet(this ITwitterStatuses instance, TwitterStatus status,
                                                   RetweetMode mode)
        {
            instance.Root.Parameters.Action = "update";
            switch (mode)
            {
                case RetweetMode.Native:
                    instance.Root.Parameters.Action = "retweet";
                    instance.Root.Parameters.Id = status.Id;
                    break;
                case RetweetMode.SymbolPrefix:
                    instance.Root.Parameters.Text =
                        '\u2672'.ToString().Then(" ").Then(status.Text);
                    break;
                case RetweetMode.Prefix:
                    var rt = "@{0} {1}".FormatWith(status.User.ScreenName, status.Text);
                    instance.Root.Parameters.Text = "RT ".Then(rt);
                    break;
                case RetweetMode.Suffix:
                    var via = "{1} (via @{0})".FormatWith(status.User.ScreenName, status.Text);
                    instance.Root.Parameters.Text = via;
                    break;
                default:
                    throw new TweetSharpException("Unknown retweet mode specified.");
            }

            return new TwitterStatusUpdate(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusMentions Mentions(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "mentions";
            return new TwitterStatusReplies(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusDestroy Destroy(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "destroy";
            return new TwitterStatusDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusDestroy Destroy(this ITwitterStatuses instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterStatusDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterStatusDestroy Destroy(this ITwitterStatuses instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterStatusDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterRetweetedByMe RetweetedByMe(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "retweeted_by_me";
            return new TwitterRetweetedByMe(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterRetweetedToMe RetweetedToMe(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "retweeted_to_me";
            return new TwitterRetweetedToMe(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterRetweetsOfMe RetweetsOfMe(this ITwitterStatuses instance)
        {
            instance.Root.Parameters.Action = "retweets_of_me";
            return new TwitterRetweetsOfMe(instance.Root);
        }
    }
}