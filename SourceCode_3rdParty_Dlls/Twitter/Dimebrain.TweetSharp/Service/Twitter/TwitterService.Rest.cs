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

using System.Collections.Generic;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Service
{
    partial class TwitterService
    {
        #region Timeline Methods

        #region Public Timeline

        /// <summary>
        /// Returns the latest 20 tweets from Twitter's public timeline.
        /// This method is cached by Twitter for 60 seconds.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-public_timeline" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnPublicTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnPublicTimeline());
        }

        #endregion

        #region Home Timeline

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-home_timeline" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline());
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimeline(int count)
        {
            return ListTweetsOnHomeTimeline(1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimeline(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnHomeTimeline().Since(sinceId).Skip(page).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnHomeTimeline().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnHomeTimelineBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnHomeTimeline().Before(maxId).Skip(page).
                                                                  Take(count));
        }

        #endregion

        #region Friends Timeline

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-friends_timeline" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline());
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimeline(int count)
        {
            return ListTweetsOnFriendsTimeline(1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimeline(int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnFriendsTimeline().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnFriendsTimeline().Since(sinceId).Skip(page)
                                                                  .Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnFriendsTimeline().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnFriendsTimeline().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnFriendsTimelineBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnFriendsTimeline().Before(maxId).Skip(page).
                                                                  Take(count));
        }

        #endregion

        #region User Timeline

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-user_timeline" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnUserTimeline()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline());
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimeline(int count)
        {
            return ListTweetsOnUserTimeline(1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimeline(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().Since(sinceId).Skip(page).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnUserTimelineBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().Before(maxId).Skip(page).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(int userId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(int userId, int count)
        {
            return ListTweetsOnSpecifiedUserTimeline(1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(int userId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Skip(page).Take(
                                                                                                                           count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(int userId, long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId).Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(int userId, long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Since(sinceId).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(int userId, long sinceId, int page,
                                                                                 int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Since(sinceId).
                                                                  Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(int userId, long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId).Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(int userId, long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Before(maxId).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(int userId, long maxId, int page,
                                                                                  int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Before(maxId).
                                                                  Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(long userId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(long userId, int count)
        {
            return ListTweetsOnSpecifiedUserTimeline(userId, 1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(long userId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Skip(page).Take(
                                                                                                                           count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(long userId, long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId).Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(long userId, long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Since(sinceId).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(long userId, long sinceId, int page,
                                                                                 int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Since(sinceId).
                                                                  Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(long userId, long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userId).Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(long userId, long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Before(maxId).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(long userId, long maxId, int page,
                                                                                  int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userId).Before(maxId).
                                                                  Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(string userScreenName)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnUserTimeline().For(userScreenName));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(string userScreenName, int count)
        {
            return ListTweetsOnSpecifiedUserTimeline(userScreenName, 1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimeline(string userScreenName, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Skip(
                                                                                                                        page)
                                                                  .Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(string userScreenName, long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Since(
                                                                                                                         sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(string userScreenName, long sinceId,
                                                                                 int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Since(
                                                                                                                         sinceId)
                                                                  .Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineSince(string userScreenName, long sinceId,
                                                                                 int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Since(
                                                                                                                         sinceId)
                                                                  .Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(string userScreenName, long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Before(
                                                                                                                          maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(string userScreenName, long maxId,
                                                                                  int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Before(
                                                                                                                          maxId)
                                                                  .Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnSpecifiedUserTimelineBefore(string userScreenName, long maxId,
                                                                                  int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnUserTimeline().For(userScreenName).Before(
                                                                                                                          maxId)
                                                                  .Skip(page).Take(count));
        }

        #endregion

        #region Mentions

        /// <summary>
        /// Lists the first 20 tweets mentioning the authenticated user.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMe()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions());
        }

        /// <summary>
        /// Lists the first page of tweets mentioning the authenticated user.
        /// Each page has 20 tweets.
        /// </summary>
        /// <param name="count">The number of tweets to return, up to 200.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMe(int count)
        {
            return ListTweetsMentioningMe(1, count);
        }

        /// <summary>
        /// Lists the specified page of tweets mentioning the authenticated user.
        /// Each page has the specified number of tweets, up to 200.
        /// </summary>
        /// <param name="page">The page of tweets to return, relative to the tweet count.</param>
        /// <param name="count">The number of tweets to return, up to 200.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-mentions"/>
        public IEnumerable<TwitterStatus> ListTweetsMentioningMe(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsMentioningMeSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsMentioningMeSince(long sinceId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsMentioningMeSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().Mentions().Since(sinceId).Skip(page).Take(
                                                                                                                        count));
        }

        public IEnumerable<TwitterStatus> ListTweetsMentioningMeBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsMentioningMeBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().Mentions().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsMentioningMeBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().Mentions().Before(maxId).Skip(page).Take(
                                                                                                                       count));
        }

        #endregion

        #region Retweeted by Me

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_by_me" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsByMe()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe());
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMe(int count)
        {
            return ListRetweetsByMe(1, count);
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMe(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMe(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMeSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMeSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetedByMe().Since(sinceId).Skip(page).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMeBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMeBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedByMe().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsByMeBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetedByMe().Before(maxId).Skip(page).Take
                                                                  (count));
        }

        #endregion

        #region Retweeted To Me

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweeted_to_me" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsToMe()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe());
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMe(int count)
        {
            return ListRetweetsToMe(1, count);
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMe(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMe(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMeSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMeSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetedToMe().Since(sinceId).Skip(page).
                                                                  Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMeSince(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMeBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetedToMe().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsToMeBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetedToMe().Before(maxId).Skip(page).Take
                                                                  (count));
        }

        #endregion

        #region Retweets Of Me

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweets_of_me" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe());
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets(int count)
        {
            return ListRetweetsOfMyTweets(1, count);
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweets(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOfMe().Since(sinceId).Skip(page).Take
                                                                  (count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsSince(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsBefore(long maxId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOfMe().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsOfMyTweetsBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOfMe().Before(maxId).Skip(page).Take(
                                                                                                                           count));
        }

        #endregion

        #endregion

        #region Status Methods

        #region Show

        /// <summary>
        /// Gets the tweet with the specified ID.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0show"></seealso>
        public TwitterStatus GetTweet(long id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Show(id));
        }

        /// <summary>
        /// Gets the tweet with the specified ID.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0show"></seealso>
        public TwitterStatus GetTweet(int id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Show(id));
        }

        #endregion

        #region Update

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// Includes provided geo-tagging data.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, double latitude, double longitude)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text, latitude, longitude));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// Includes the provided <see cref="GeoLocation" /> data.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, GeoLocation location)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text, location));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// You must mention a user using @username in your message
        /// if you intend your tweet to include a reference to <see cref="TwitterStatus.InReplyToStatusId" />.
        /// A tweet with text identical to the authenticating user's 
        /// current status will be ignored.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="inReplyToStatusId"></param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, long inReplyToStatusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Update(text).InReplyToStatus(inReplyToStatusId));
        }

        /// <summary>
        /// Tweets the specified text from the authenticated user.
        /// Includes the provided <see cref="GeoLocation"/> data.
        /// You must mention a user using @username in your message
        /// if you intend your tweet to include a reference to <see cref="TwitterStatus.InReplyToStatusId"/>.
        /// A tweet with text identical to the authenticating user's
        /// current status will be ignored.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="inReplyToStatusId">The ID of the tweet this tweet is replying to.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public TwitterStatus SendTweet(string text, long inReplyToStatusId, GeoLocation location)
        {
            return
                WithTweetSharp<TwitterStatus>(
                                                 q =>
                                                 q.Statuses().Update(text, location).InReplyToStatus(inReplyToStatusId));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Deletes a tweet. The tweet must be authored by the authenticated user.
        /// </summary>
        /// <param name="status">The tweet to delete.</param>
        /// <returns></returns>
        public TwitterStatus DeleteTweet(TwitterStatus status)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Destroy(status.Id));
        }

        /// <summary>
        /// Deletes a tweet. The tweet must be authored by the authenticated user.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        public TwitterStatus DeleteTweet(long id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Destroy(id));
        }

        /// <summary>
        /// Deletes a tweet. The tweet must be authored by the authenticated user.
        /// </summary>
        /// <param name="id">The tweet ID.</param>
        /// <returns></returns>
        public TwitterStatus DeleteTweet(int id)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Destroy(id));
        }

        #endregion

        #region Retweet

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses-retweet" />
        /// <returns></returns>
        public TwitterStatus SendRetweet(long statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(statusId));
        }

        public TwitterStatus SendRetweet(TwitterStatus status)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(status));
        }

        public TwitterStatus SendRetweet(long statusId, RetweetMode mode)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(statusId, mode));
        }

        public TwitterStatus SendRetweet(TwitterStatus status, RetweetMode mode)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Statuses().Retweet(status, mode));
        }

        #endregion

        #region Retweets

        public IEnumerable<TwitterStatus> ListRetweets(long statusId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId));
        }

        public IEnumerable<TwitterStatus> ListRetweets(long statusId, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweets(long statusId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsSince(long statusId, long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsSince(long statusId, long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOf(statusId).Since(sinceId).Take(
                                                                                                                       count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsSince(long statusId, long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOf(statusId).Since(sinceId).Skip(page)
                                                                  .Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsBefore(long statusId, long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().RetweetsOf(statusId).Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListRetweetsBefore(long statusId, long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOf(statusId).Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListRetweetsBefore(long statusId, long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().RetweetsOf(statusId).Before(maxId).Skip(page)
                                                                  .Take(count));
        }

        #endregion

        #endregion

        #region User Methods

        /// <summary>
        /// Gets up to the first 100 friends for the authenticating user.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0friends" />
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFriends()
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFriends().CreateCursor());
        }

        /// <summary>
        /// Lists the friends of the authenticated user by cursor value.
        /// This is useful for paging through large numbers of friends.
        /// </summary>
        /// <param name="cursor">The cursor.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0friends"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFriends(long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFriends().GetCursor(cursor));
        }

        /// <summary>
        /// Gets up to the first 100 friends for the authenticating user.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0followers"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFollowers()
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFollowers().CreateCursor());
        }

        /// <summary>
        /// Lists the friends of the authenticated user by cursor value.
        /// This is useful for paging through large numbers of friends.
        /// </summary>
        /// <param name="cursor">The cursor value for paging.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-statuses%C2%A0followers"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListFollowers(long cursor)
        {
            return WithTweetSharpAndCursors<TwitterUser>(q => q.Users().GetFollowers().GetCursor(cursor));
        }

        /// <summary>
        /// Gets the authenticated user's profile.
        /// This is achieved with a call to verify credentials.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0verify_credentials"/>
        /// <returns></returns>
        public TwitterUser GetUserProfile()
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().VerifyCredentials());
        }

        /// <summary>
        /// Gets the specified user screen name's profile.
        /// </summary>
        /// <param name="screenName">The user's screen name.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users%C2%A0show" />
        /// <returns></returns>
        public TwitterUser GetUserProfileFor(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Users().ShowProfileFor(screenName));
        }

        /// <summary>
        /// Gets the specified user ID's profile.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users%C2%A0show" />
        /// <returns></returns>
        public TwitterUser GetUserProfileFor(long id)
        {
            return WithTweetSharp<TwitterUser>(q => q.Users().ShowProfileFor(id));
        }

        /// <summary>
        /// Searches for a Twitter user given a query.
        /// This search is the same as Twitter's web-based People Search.
        /// This search yields a maximum of 1000 results in total.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users-search" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> SearchForUser(string query)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().SearchFor(query));
        }

        /// <summary>
        /// Searches for a Twitter user given a query.
        /// This search is the same as Twitter's web-based People Search.
        /// This search yields a maximum of 1000 results in total.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="page">The page of user results to get for this query.</param>
        /// <returns></returns>
        public IEnumerable<TwitterUser> SearchForUser(string query, int page)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().SearchFor(query).Page(page));
        }

        /// <summary>
        /// Searches for a Twitter user given a query.
        /// This search is the same as Twitter's web-based People Search.
        /// This search yields a maximum of 1000 results in total.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="page">The page of user results to get for this query.</param>
        /// <param name="count">The number of results to return on this page.</param>
        /// <returns></returns>
        public IEnumerable<TwitterUser> SearchForUser(string query, int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Users().SearchFor(query).Page(page).Count(count));
        }

        /// <summary>
        /// Gets the specified user ID's profile.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-users%C2%A0show" />
        /// <returns></returns>
        public TwitterUser GetUserProfileFor(int id)
        {
            return WithTweetSharp<TwitterUser>(q => q.Users().ShowProfileFor(id));
        }

        #endregion

        #region List Methods

        #region POST lists

        /// <summary>
        /// Creates a new list for the authenticated user. Accounts are limited to 20 lists. 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists" />
        /// <returns></returns>
        public TwitterList CreatePublicList(string listOwnerScreenName, string listName)
        {
            return CreatePublicList(listOwnerScreenName, listName, null);
        }

        public TwitterList CreatePublicList(string listOwnerScreenName, string listName, string listDescription)
        {
            return
                WithTweetSharp<TwitterList>(
                                               q =>
                                               q.Lists().CreatePublicList(listOwnerScreenName, listName, listDescription));
        }

        public TwitterList CreatePrivateList(string listOwnerScreenName, string listName)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().CreatePrivateList(listOwnerScreenName, listName, null));
        }

        public TwitterList CreatePrivateList(string listOwnerScreenName, string listName, string listDescription)
        {
            return
                WithTweetSharp<TwitterList>(
                                               q =>
                                               q.Lists().CreatePrivateList(listOwnerScreenName, listName,
                                                                           listDescription));
        }

        #endregion

        #region POST lists id

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-lists-id" />
        /// <returns></returns>
        public TwitterList UpdateList(TwitterList list)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().UpdateList(list));
        }

        /// <summary>
        /// List the lists of the specified user. Private lists will be included if the authenticated users is the same as the user whose lists are being returned.
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-lists"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterList> ListListsFor(string listOwnerScreenName)
        {
            return WithTweetSharpAndCursors<TwitterList>(q => q.Lists().GetListsBy(listOwnerScreenName).CreateCursor());
        }

        public TwitterCursorList<TwitterList> ListListsFor(string listOwnerScreenName, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterList>(q => q.Lists().GetListsBy(listOwnerScreenName).GetCursor(cursor));
        }

        #endregion

        #region GET list id

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-id"/>
        /// <returns></returns>
        public TwitterList GetList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().GetListBy(listOwnerScreenName, listId));
        }

        public TwitterList GetList(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().GetListBy(listOwnerScreenName, listId));
        }

        public TwitterList GetList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().GetListBy(listOwnerScreenName, listSlug));
        }

        #endregion

        #region DELETE list id

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-id"/>
        /// <returns></returns>
        public TwitterList DeleteList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().DeleteList(listOwnerScreenName, listId));
        }

        public TwitterList DeleteList(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().DeleteList(listOwnerScreenName, listId));
        }

        public TwitterList DeleteList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().DeleteList(listOwnerScreenName, listSlug));
        }

        #endregion

        #region GET list statuses

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-statuses"/>
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, int listId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, int listId, int count)
        {
            return ListTweetsOnListTimeline(listOwnerScreenName, listId, 1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, int listId, int page,
                                                                   int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, int listId,
                                                                        long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, int listId,
                                                                        long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, int listId,
                                                                        long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Since(sinceId).Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, int listId,
                                                                         long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, int listId,
                                                                         long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, int listId,
                                                                         long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Before(maxId).Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, long listId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, long listId, int count)
        {
            return ListTweetsOnListTimeline(listOwnerScreenName, listId, 1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, long listId, int page,
                                                                   int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, long listId,
                                                                        long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, long listId,
                                                                        long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, long listId,
                                                                        long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Since(sinceId).Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, long listId,
                                                                         long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, long listId,
                                                                         long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, long listId,
                                                                         long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Statuses().OnListTimeline(listOwnerScreenName, listId)
                                                                    .Before(maxId).Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, string listSlug)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, string listSlug,
                                                                   int count)
        {
            return ListTweetsOnListTimeline(listOwnerScreenName, listSlug, 1, count);
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimeline(string listOwnerScreenName, string listSlug, int page,
                                                                   int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, string listSlug,
                                                                        long sinceId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Since(sinceId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, string listSlug,
                                                                        long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineSince(string listOwnerScreenName, string listSlug,
                                                                        long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Since(sinceId).Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, string listSlug,
                                                                         long maxId)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Before(maxId));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, string listSlug,
                                                                         long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Before(maxId).Take(count));
        }

        public IEnumerable<TwitterStatus> ListTweetsOnListTimelineBefore(string listOwnerScreenName, string listSlug,
                                                                         long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Statuses().OnListTimeline(listOwnerScreenName, listSlug)
                                                                  .Before(maxId).Skip(page).Take(count));
        }

        #endregion

        #region GET list memberships

        /// <summary>
        /// List the lists the specified user has been added to.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-memberships"/>
        public TwitterCursorList<TwitterList> ListListMembershipsFor(string listOwnerScreenName)
        {
            return
                WithTweetSharpAndCursors<TwitterList>(q => q.Lists().GetMemberships(listOwnerScreenName).CreateCursor());
        }

        /// <summary>
        /// List the lists the specified user follows.
        /// </summary>
        /// <param name="listSubscriberScreenName"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscriptions"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterList> ListListSubscriptionsFor(string listSubscriberScreenName)
        {
            return
                WithTweetSharpAndCursors<TwitterList>(
                                                         q =>
                                                         q.Lists().GetSubscriptions(listSubscriberScreenName).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterList> ListListSubscriptionsFor(string listSubscriberScreenName, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterList>(
                                                         q =>
                                                         q.Lists().GetSubscriptions(listSubscriberScreenName).GetCursor(
                                                                                                                           cursor));
        }

        #endregion

        #endregion

        #region GET list members

        /// <summary>
        /// Returns the members of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-members"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, int listId)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetMembersOf(listOwnerScreenName, listId).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, long listId)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetMembersOf(listOwnerScreenName, listId).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, string listSlug)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetMembersOf(listOwnerScreenName, listSlug).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, int listId, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetMembersOf(listOwnerScreenName, listId).GetCursor(
                                                                                                                          cursor));
        }

        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, long listId, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetMembersOf(listOwnerScreenName, listId).GetCursor(
                                                                                                                          cursor));
        }

        public TwitterCursorList<TwitterUser> ListListMembers(string listOwnerScreenName, string listSlug, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetMembersOf(listOwnerScreenName, listSlug).GetCursor
                                                             (cursor));
        }

        #endregion

        #region POST list members

        /// <summary>
        /// Add a member to a list. The authenticated user must own the list to add members to it. 
        /// Lists are limited to 500 members.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-members"/>
        /// <returns></returns>
        public TwitterList AddMemberToList(string listOwnerScreenName, long listId, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().AddMemberTo(listOwnerScreenName, listId, userId));
        }

        public TwitterList AddMemberToList(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().AddMemberTo(listOwnerScreenName, listId, userId));
        }

        public TwitterList AddMemberToList(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().AddMemberTo(listOwnerScreenName, listSlug, userId));
        }

        #endregion

        #region DELETE list members

        /// <summary>
        /// Removes the specified member from the list. 
        /// The authenticated user must be the list's owner to remove members from the list.
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-members"/>
        /// <returns></returns>
        public TwitterList RemoveMemberFromList(string listOwnerScreenName, long listId, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().RemoveMemberFrom(listOwnerScreenName, listId, userId));
        }

        public TwitterList RemoveMemberFromList(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().RemoveMemberFrom(listOwnerScreenName, listId, userId));
        }

        public TwitterList RemoveMemberFromList(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().RemoveMemberFrom(listOwnerScreenName, listSlug, userId));
        }

        #endregion

        #region GET list members id

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers-id"/>
        /// <returns></returns>
        public TwitterUser VerifyListMember(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserMemberOf(listOwnerScreenName, listId, userId));
        }

        public TwitterUser VerifyListMember(string listOwnerScreenName, long listId, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserMemberOf(listOwnerScreenName, listId, userId));
        }

        public TwitterUser VerifyListMember(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserMemberOf(listOwnerScreenName, listSlug, userId));
        }

        #endregion

        #region List Subscribers Methods

        #region GET list susbcribers

        /// <summary>
        /// Returns the subscribers of the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers"/>
        /// <returns></returns>
        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, int listId)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetSubscribersOf(listOwnerScreenName, listId).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, long listId)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetSubscribersOf(listOwnerScreenName, listId).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, string listSlug)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetSubscribersOf(listOwnerScreenName, listSlug).
                                                             CreateCursor());
        }

        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, int listId, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetSubscribersOf(listOwnerScreenName, listId).
                                                             GetCursor(cursor));
        }

        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, long listId, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetSubscribersOf(listOwnerScreenName, listId).
                                                             GetCursor(cursor));
        }

        public TwitterCursorList<TwitterUser> ListListFollowers(string listOwnerScreenName, string listSlug, long cursor)
        {
            return
                WithTweetSharpAndCursors<TwitterUser>(
                                                         q =>
                                                         q.Lists().GetSubscribersOf(listOwnerScreenName, listSlug).
                                                             GetCursor(cursor));
        }

        #endregion

        #region POST list subscribers

        /// <summary>
        /// Subscribes the authenticated user to the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The screen name of the list owner.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-POST-list-subscribers"/>
        public TwitterList FollowList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Follow(listOwnerScreenName, listId));
        }

        public TwitterList FollowList(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Follow(listOwnerScreenName, listId));
        }

        public TwitterList FollowList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Follow(listOwnerScreenName, listSlug));
        }

        #endregion

        #region DELETE list subscribers

        /// <summary>
        /// Unsubscribes the authenticated user from the specified list.
        /// </summary>
        /// <param name="listOwnerScreenName">The screen name of the list owner.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-DELETE-list-subscribers"/>
        public TwitterList UnfollowList(string listOwnerScreenName, long listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Unfollow(listOwnerScreenName, listId));
        }

        public TwitterList UnfollowList(string listOwnerScreenName, int listId)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Unfollow(listOwnerScreenName, listId));
        }

        public TwitterList UnfollowList(string listOwnerScreenName, string listSlug)
        {
            return WithTweetSharp<TwitterList>(q => q.Lists().Unfollow(listOwnerScreenName, listSlug));
        }

        #endregion

        #region GET list subscribers id

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listOwnerScreenName"></param>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-GET-list-subscribers-id"/>
        /// <returns></returns>
        public TwitterUser VerifyListFollower(string listOwnerScreenName, int listId, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserFollowerOf(listOwnerScreenName, listId, userId));
        }

        public TwitterUser VerifyListFollower(string listOwnerScreenName, long listId, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserFollowerOf(listOwnerScreenName, listId, userId));
        }

        public TwitterUser VerifyListFollower(string listOwnerScreenName, string listSlug, int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Lists().IsUserFollowerOf(listOwnerScreenName, listSlug, userId));
        }

        #endregion

        #endregion

        #region Direct Message Methods

        #region / (Received)

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-direct_messages"/>
        /// <returns></returns>
        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceived()
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received());
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceived(int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Take(10));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceived(int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Skip(page).Take(10));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Since(sinceId));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Received().Since(sinceId).Take(
                                                                                                                          count));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Received().Since(sinceId).Skip(
                                                                                                                          page)
                                                                         .Take(count));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Received().Before(maxId));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Received().Before(maxId).Take(
                                                                                                                         count));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesReceivedBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Received().Before(maxId).Skip(
                                                                                                                         page)
                                                                         .Take(count));
        }

        #endregion

        #region Sent

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSent()
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent());
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSent(int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Take(10));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSent(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Skip(page).Take(10));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Since(sinceId));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentSince(long sinceId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Sent().Since(sinceId).Take(count));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentSince(long sinceId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Sent().Since(sinceId).Skip(page)
                                                                         .Take(count));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentBefore(long maxId)
        {
            return WithTweetSharp<IEnumerable<TwitterDirectMessage>>(q => q.DirectMessages().Sent().Before(maxId));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentBefore(long maxId, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Sent().Before(maxId).Take(count));
        }

        public IEnumerable<TwitterDirectMessage> ListDirectMessagesSentBefore(long maxId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterDirectMessage>>(
                                                                     q =>
                                                                     q.DirectMessages().Sent().Before(maxId).Skip(page).
                                                                         Take(count));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// Deletes the direct message. The direct message must be authored by the authenticated user.
        /// </summary>
        /// <param name="messageId">The message ID.</param>
        /// <returns></returns>
        public TwitterDirectMessage DeleteDirectMessage(long messageId)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Destroy(messageId));
        }

        /// <summary>
        /// Deletes the direct message. The direct message must be authored by the authenticated user.
        /// </summary>
        /// <param name="messageId">The message ID.</param>
        /// <returns></returns>
        public TwitterDirectMessage DeleteDirectMessage(int messageId)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Destroy(messageId));
        }

        #endregion

        #region Create

        /// <summary>
        /// Sends a direct message from the authenticated user.
        /// </summary>
        /// <param name="recipientUserId">The recipient user ID.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public TwitterDirectMessage SendDirectMessage(int recipientUserId, string text)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Send(recipientUserId, text));
        }

        /// <summary>
        /// Sends a direct message from the authenticated user.
        /// </summary>
        /// <param name="recipientScreenName">Name of the recipient screen.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public TwitterDirectMessage SendDirectMessage(string recipientScreenName, string text)
        {
            return WithTweetSharp<TwitterDirectMessage>(q => q.DirectMessages().Send(recipientScreenName, text));
        }

        #endregion

        #endregion

        #region Friendship Methods

        #region Create

        /// <summary>
        /// Follows the specified user by ID.
        /// </summary>
        /// <param name="userId">The user's ID.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships%C2%A0create"/>
        /// <returns></returns>
        public TwitterUser FollowUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Befriend(userId));
        }

        public TwitterUser FollowUser(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Befriend(screenName));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenName"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships%C2%A0destroy"/>
        /// <returns></returns>
        public TwitterUser UnfollowUser(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Destroy(screenName));
        }

        public TwitterUser UnfollowUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Friendships().Destroy(userId));
        }

        #endregion

        #region Exists

        /// <summary>
        /// Returns an indication whether the authenticating user follows the specified user.
        /// </summary>
        /// <param name="followedUserId">The user ID.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-exists"/>
        public bool VerifyFriendship(int followedUserId)
        {
            var exists = false;

            WithTweetSharp(
                              q => q.Friendships().Verify(followedUserId),
                              r => exists = r.Response.Equals("true")
                );

            return exists;
        }

        /// <summary>
        /// Returns an indication whether the first specified user follows the other specified user.
        /// </summary>
        /// <param name="followingUserId">The following user ID.</param>
        /// <param name="followedUserId">The followed user ID.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-exists"/>
        public bool VerifyFriendship(int followingUserId, int followedUserId)
        {
            var exists = false;

            WithTweetSharp(
                              q => q.Friendships().Verify(followingUserId).IsFriendsWith(followedUserId),
                              r => exists = r.Response.Equals("true")
                );

            return exists;
        }

        #endregion

        #region Show

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-show"/>
        /// <returns></returns>
        public TwitterFriendship GetFriendshipInfo(string sourceUserScreenName, string targetUserScreenName)
        {
            return
                WithTweetSharp<TwitterFriendship>(q => q.Friendships().Show(sourceUserScreenName, targetUserScreenName));
        }

        /// <summary>
        /// Returns detailed information about the relationship between two users.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friendships-show"/>
        /// <returns></returns>
        public TwitterFriendship GetFriendshipInfo(int sourceUserId, int targetUserId)
        {
            return WithTweetSharp<TwitterFriendship>(q => q.Friendships().Show(sourceUserId, targetUserId));
        }

        #endregion

        #endregion

        #region Social Graph Methods

        #region Friends

        /// <summary>
        /// Lists the authenticating user's friends' IDs.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        /// <returns></returns>
        public TwitterCursorList<int> ListFriendIds()
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriends().CreateCursor());
        }

        public TwitterCursorList<int> ListFriendIds(long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriends().GetCursor(cursor));
        }

        /// <summary>
        /// Lists the specified user's friends' IDs.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-friends%C2%A0ids"/>
        /// <returns></returns>
        public TwitterCursorList<int> ListFriendIdsFor(int userId)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriendsOf(userId).CreateCursor());
        }

        public TwitterCursorList<int> ListFriendIdsFor(string userScreenName)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriendsOf(userScreenName).CreateCursor());
        }

        public TwitterCursorList<int> ListFriendIdsFor(int userId, long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriendsOf(userId).GetCursor(cursor));
        }

        public TwitterCursorList<int> ListFriendIdsFor(string userScreenName, long cursor)
        {
            return
                WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFriendsOf(userScreenName).GetCursor(cursor));
        }

        #endregion

        #region Followers

        /// <summary>
        /// Lists the authenticating user's followers' IDs.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        /// <returns></returns>
        public TwitterCursorList<int> ListFollowerIds()
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowers().CreateCursor());
        }

        public TwitterCursorList<int> ListFollowerIds(long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowers().GetCursor(cursor));
        }

        /// <summary>
        /// Lists the specified user's followers' IDs.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-followers%C2%A0ids"/>
        /// <returns></returns>
        public TwitterCursorList<int> ListFollowerIdsFor(int userId)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowersOf(userId).CreateCursor());
        }

        public TwitterCursorList<int> ListFollowerIdsFor(string userScreenName)
        {
            return
                WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowersOf(userScreenName).CreateCursor());
        }

        public TwitterCursorList<int> ListFollowerIdsFor(int userId, long cursor)
        {
            return WithTweetSharpAndCursors<int>(q => q.SocialGraph().Ids().ForFollowersOf(userId).GetCursor(cursor));
        }

        public TwitterCursorList<int> ListFollowerIdsFor(string userScreenName, long cursor)
        {
            return
                WithTweetSharpAndCursors<int>(
                                                 q =>
                                                 q.SocialGraph().Ids().ForFollowersOf(userScreenName).GetCursor(cursor));
        }

        #endregion

        #endregion

        #region Account Methods

        /// <summary>
        /// Verifies the credentials provided with the service call to ensure a user 
        /// will authenticate against the API.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0verify_credentials"></seealso>
        /// <returns></returns>
        public TwitterUser VerifyCredentials()
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().VerifyCredentials());
        }

        /// <summary>
        /// Gets the <see cref="TwitterRateLimitStatus" /> for the authenticated user.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0rate_limit_status"/>
        /// <returns></returns>
        public TwitterRateLimitStatus GetRateLimitStatus()
        {
            return WithTweetSharp<TwitterRateLimitStatus>(q => q.Account().GetRateLimitStatus());
        }

        /// <summary>
        /// Ends the session of the authenticating user, returning a null cookie.  
        /// Use this method to sign users out of client-facing applications like widgets.
        /// If this method returns a null <see cref="TwitterError"/> instance, 
        /// the session was located and ended successfully.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0end_session"/>
        /// <returns></returns>
        public TwitterError EndSession()
        {
            return WithTweetSharp<TwitterError>(q => q.Account().EndSession());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_delivery_device"/>
        /// <returns></returns>
        public TwitterUser UpdateDeliveryDevice(TwitterDeliveryDevice device)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateDeliveryDeviceTo(device));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_colors"/>
        /// <returns></returns>
        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor, string linkColor,
                                               string sidebarFillColor, string sidebarBorderColor)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfileColors()
                                                        .UpdateProfileBackgroundColor(backgroundColor)
                                                        .UpdateProfileTextColor(textColor)
                                                        .UpdateProfileLinkColor(linkColor)
                                                        .UpdateProfileSidebarFillColor(sidebarFillColor)
                                                        .UpdateProfileSidebarBorderColor(sidebarBorderColor)
                );
        }

        public TwitterUser UpdateProfileColors(string backgroundColor)
        {
            return UpdateProfileColors(backgroundColor, null, null, null, null);
        }

        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor)
        {
            return UpdateProfileColors(backgroundColor, textColor, null, null, null);
        }

        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor, string linkColor)
        {
            return UpdateProfileColors(backgroundColor, textColor, linkColor, null, null);
        }

        public TwitterUser UpdateProfileColors(string backgroundColor, string textColor, string linkColor,
                                               string sidebarFillColor)
        {
            return UpdateProfileColors(backgroundColor, textColor, linkColor, sidebarFillColor, null);
        }

        /// <summary>
        /// Updates the authenticating user's profile image. 
        /// This method expects raw multipart data, not a URL to an image.
        /// Must be a valid GIF, JPG, or PNG image of less than 700 kilobytes in size.  
        /// Images with width larger than 500 pixels will be scaled down.
        /// </summary>
        /// <param name="path"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_image"/>
        /// <returns></returns>
        public TwitterUser UpdateProfileImage(string path)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfileImage(path));
        }

        /// <summary>
        /// Updates the authenticating user's profile background image.  
        /// This method expects raw multipart data, not a URL to an image.
        /// Must be a valid GIF, JPG, or PNG image of less than 800 kilobytes in size.  
        /// Images with width larger than 2048 pixels will be scaled down.
        /// </summary>
        /// <param name="path"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile_background_image"/>
        /// <returns></returns>
        public TwitterUser UpdateProfileBackgroundImage(string path)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfileBackgroundImage(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="email"></param>
        /// <param name="url"></param>
        /// <param name="location"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-account%C2%A0update_profile"/>
        /// <returns></returns>
        public TwitterUser UpdateProfile(string name, string description, string email, string url, string location)
        {
            return WithTweetSharp<TwitterUser>(q => q.Account().UpdateProfile()
                                                        .UpdateName(name)
                                                        .UpdateDescription(name)
                                                        .UpdateEmail(email)
                                                        .UpdateUrl(url)
                                                        .UpdateLocation(location)
                );
        }

        #endregion

        #region Favorite Methods

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites" />
        /// <returns></returns>
        public IEnumerable<TwitterStatus> ListFavoriteTweets()
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavorites());
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweets(int page)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavorites().Skip(page));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweets(int page, int count)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavorites().Skip(page).Take(count));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(int userId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userId));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(int userId, int page)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userId).Skip(page));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(int userId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Favorites().GetFavoritesFor(userId).Skip(page).Take(
                                                                                                                       count));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(long userId)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userId));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(long userId, int page)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userId).Skip(page));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(long userId, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Favorites().GetFavoritesFor(userId).Skip(page).Take(
                                                                                                                       count));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(string userScreenName)
        {
            return WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userScreenName));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(string userScreenName, int page)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(q => q.Favorites().GetFavoritesFor(userScreenName).Skip(page));
        }

        public IEnumerable<TwitterStatus> ListFavoriteTweetsFor(string userScreenName, int page, int count)
        {
            return
                WithTweetSharp<IEnumerable<TwitterStatus>>(
                                                              q =>
                                                              q.Favorites().GetFavoritesFor(userScreenName).Skip(page).
                                                                  Take(count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites%C2%A0create" />
        /// <returns></returns>
        public TwitterStatus FavoriteTweet(long statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Favorite(statusId));
        }

        public TwitterStatus FavoriteTweet(int statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Favorite(statusId));
        }

        public TwitterStatus FavoriteTweet(TwitterStatus tweet)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Favorite(tweet));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-favorites%C2%A0destroy" />
        /// <returns></returns>
        public TwitterStatus UnfavoriteTweet(long statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Unfavorite(statusId));
        }

        public TwitterStatus UnfavoriteTweet(int statusId)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Unfavorite(statusId));
        }

        public TwitterStatus UnfavoriteTweet(TwitterStatus tweet)
        {
            return WithTweetSharp<TwitterStatus>(q => q.Favorites().Unfavorite(tweet));
        }

        #endregion

        #region Notification Methods

        #region Follow

        /// <summary>
        /// Follows the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0follow" />
        /// <returns></returns>
        public TwitterUser FollowUserNotifications(TwitterUser user)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Follow(user.Id));
        }

        /// <summary>
        /// Follows the specified user by screen name.
        /// </summary>
        /// <param name="screenName">The user's screen name.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0follow" />
        /// <returns></returns>
        public TwitterUser FollowUserNotifications(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Follow(screenName));
        }

        public TwitterUser FollowUserNotifications(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Follow(userId));
        }

        #endregion

        #region Leave

        /// <summary>
        /// Disables notifications for updates from the specified user to the authenticating user.  
        /// Returns the specified user when successful.
        /// </summary>
        /// <param name="userId">The user ID to stop following notifications for.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-notifications%C2%A0leave" />
        /// <returns></returns>
        public TwitterUser UnfollowUserNotifications(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Leave(userId));
        }

        public TwitterUser UnfollowUserNotifications(string screenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Notifications().Leave(screenName));
        }

        #endregion

        #endregion

        #region Block Methods

        #region Create

        public TwitterUser BlockUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Block(userId));
        }

        public TwitterUser BlockUser(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Block(userScreenName));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-blocks%C2%A0destroy" />
        /// <returns></returns>
        public TwitterUser UnblockUser(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Unblock(userId));
        }

        public TwitterUser UnblockUser(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Unblock(userScreenName));
        }

        #endregion

        #region Exists

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter+REST+API+Method%3A-blocks-exists" /> 
        /// <returns></returns>
        public TwitterUser VerifyBlocking(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Exists(userId));
        }

        public TwitterUser VerifyBlocking(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.Blocking().Exists(userScreenName));
        }

        #endregion

        #region Blocking

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter+REST+API+Method%3A-blocks-blocking" />
        /// <returns></returns>
        public IEnumerable<TwitterUser> ListBlockedUsers()
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Blocking().ListUsers());
        }

        public IEnumerable<TwitterUser> ListBlockedUsers(int page)
        {
            return WithTweetSharp<IEnumerable<TwitterUser>>(q => q.Blocking().ListUsers().Skip(page));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-blocks-blocking-ids" />
        /// <returns></returns>
        public IEnumerable<int> ListBlockedUserIds()
        {
            return WithTweetSharp<IEnumerable<int>>(q => q.Blocking().ListIds());
        }

        #endregion

        #endregion

        #region Spam Reporting Methods

        #region Report Spam

        /// <summary>
        /// Reports the screen name as a spammer.
        /// </summary>
        /// <param name="userScreenName">The spammer's screen name.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-report_spam" />
        /// <returns></returns>
        public TwitterUser ReportSpamFrom(string userScreenName)
        {
            return WithTweetSharp<TwitterUser>(q => q.ReportSpam().ReportSpammer(userScreenName));
        }

        /// <summary>
        /// Reports the user ID as a spammer.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-report_spam"/>
        public TwitterUser ReportSpamFrom(int userId)
        {
            return WithTweetSharp<TwitterUser>(q => q.ReportSpam().ReportSpammer(userId));
        }

        #endregion

        #endregion

        #region Saved Searches Methods

        /// <summary>
        /// Lists the saved searches for the authenticating user.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches" />
        /// <returns></returns>
        public IEnumerable<TwitterSavedSearch> ListSavedSearches()
        {
            return WithTweetSharp<IEnumerable<TwitterSavedSearch>>(q => q.SavedSearches().List());
        }

        #region Show

        /// <summary>
        /// Gets the saved search.
        /// </summary> 
        /// <param name="searchId">The search id.</param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches-show" />
        /// <returns></returns>
        public TwitterSavedSearch GetSavedSearch(int searchId)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Show(searchId));
        }

        public TwitterSavedSearch GetSavedSearch(long searchId)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Show(searchId));
        }

        #endregion

        #region Create

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches-create" /> 
        /// <returns></returns>
        public TwitterSavedSearch CreateSavedSearch(string query)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Create(query));
        }

        #endregion

        #region Destroy

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchId"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-saved_searches-destroy"/>
        /// <returns></returns>
        public TwitterSavedSearch DeleteSavedSearch(int searchId)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Delete(searchId));
        }

        public TwitterSavedSearch DeleteSavedSearch(long searchId)
        {
            return WithTweetSharp<TwitterSavedSearch>(q => q.SavedSearches().Delete(searchId));
        }

        #endregion

        #endregion

        #region OAuth Methods

        #region Request Token

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-request_token" />
        /// <returns></returns>
        public OAuthToken GetRequestToken()
        {
            return WithTweetSharp<OAuthToken>(q => q.Authentication.GetRequestToken());
        }

        public OAuthToken GetRequestToken(string consumerKey, string consumerSecret)
        {
            return WithTweetSharp<OAuthToken>(q => q.Authentication.GetRequestToken(consumerKey, consumerSecret));
        }

        #endregion

        #region Authorize

        public string GetAuthorizationUrl(OAuthToken requestToken)
        {
            return GetQuery().Authentication.GetAuthorizationUrl(requestToken.Token);
        }

        public string GetAuthorizationUrl(OAuthToken requestToken, string callback)
        {
            return GetQuery().Authentication.GetAuthorizationUrl(requestToken.Token, callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestToken"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-authorize"/>
        public void AuthorizeDesktop(OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthorizeDesktop(requestToken.Token));
        }

        public void AuthorizeDesktop(OAuthToken requestToken, string callback)
        {
            WithTweetSharp(q => q.Authentication.AuthorizeDesktop(requestToken.Token, callback));
        }

        public void AuthorizeDesktop(string consumerKey, string consumerSecret, OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthorizeDesktop(consumerKey, consumerSecret, requestToken.Token));
        }

        public void AuthorizeDesktop(string consumerKey, string consumerSecret, OAuthToken requestToken, string callback)
        {
            WithTweetSharp(
                              q =>
                              q.Authentication.AuthorizeDesktop(consumerKey, consumerSecret, requestToken.Token,
                                                                callback));
        }

        #endregion

        #region Authenticate

        public string GetAuthenticationUrl(OAuthToken requestToken)
        {
            return GetQuery().Authentication.GetAuthenticationUrl(requestToken.Token);
        }

        public string GetAuthenticationUrl(OAuthToken requestToken, string callback)
        {
            return GetQuery().Authentication.GetAuthenticationUrl(requestToken.Token, callback);
        }

        public void AuthenticateDesktop(OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthenticateDesktop(requestToken.Token));
        }

        public void AuthenticateDesktop(OAuthToken requestToken, string callback)
        {
            WithTweetSharp(q => q.Authentication.AuthenticateDesktop(requestToken.Token, callback));
        }

        public void AuthenticateDesktop(string consumerKey, string consumerSecret, OAuthToken requestToken)
        {
            WithTweetSharp(q => q.Authentication.AuthenticateDesktop(consumerKey, consumerSecret, requestToken.Token));
        }

        public void AuthenticateDesktop(string consumerKey, string consumerSecret, OAuthToken requestToken,
                                        string callback)
        {
            WithTweetSharp(
                              q =>
                              q.Authentication.AuthenticateDesktop(consumerKey, consumerSecret, requestToken.Token,
                                                                   callback));
        }

        #endregion

        #region Access Token

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-oauth-access_token"/>
        /// <returns></returns>
        public OAuthToken GetAccessToken(OAuthToken requestToken)
        {
            return WithTweetSharp<OAuthToken>(q => q.Authentication.GetAccessToken(requestToken.Token));
        }

        public OAuthToken GetAccessToken(string consumerKey, string consumerSecret, OAuthToken requestToken)
        {
            return
                WithTweetSharp<OAuthToken>(
                                              q =>
                                              q.Authentication.GetAccessToken(consumerKey, consumerSecret,
                                                                              requestToken.Token));
        }

        public OAuthToken GetAccessToken(OAuthToken requestToken, string pin)
        {
            return WithTweetSharp<OAuthToken>(q => q.Authentication.GetAccessToken(requestToken.Token, pin));
        }

        public OAuthToken GetAccessToken(string consumerKey, string consumerSecret, OAuthToken requestToken, string pin)
        {
            return
                WithTweetSharp<OAuthToken>(
                                              q =>
                                              q.Authentication.GetAccessToken(consumerKey, consumerSecret,
                                                                              requestToken.Token, pin));
        }

        #endregion

        #endregion

        #region Local Trends Methods

        public IEnumerable<WhereOnEarthLocation> ListLocalTrendLocations()
        {
            return WithTweetSharp<IEnumerable<WhereOnEarthLocation>>(
                                                                        q => q.Trends().GetAvailable()
                );
        }

        public IEnumerable<WhereOnEarthLocation> ListLocalTrendLocations(GeoLocation orderByLocation)
        {
            return WithTweetSharp<IEnumerable<WhereOnEarthLocation>>(
                                                                        q =>
                                                                        q.Trends().GetAvailable().OrderBy(
                                                                                                             orderByLocation)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="woeId"></param>
        /// <seealso cref="http://twitterapi.pbworks.com/Twitter-REST-API-Method%3A-trends-location"/>
        /// <returns></returns>
        public TwitterLocalTrends SearchLocalTrends(long woeId)
        {
            return WithTweetSharp<TwitterLocalTrends>(
                                                         q => q.Trends().ByLocation(woeId)
                );
        }

        #endregion

        #region Help Methods

        #region Test

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-help%C2%A0test" />
        /// <returns></returns>
        public bool IsTwitterDown()
        {
            var test = WithTweetSharp<string>(q => q.Help().TestService().AsXml());
            return !test.Contains("true");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-REST-API-Method%3A-help%C2%A0test" />
        /// <returns></returns>
        public bool IsTwitterUp()
        {
            var test = WithTweetSharp<string>(q => q.Help().TestService().AsXml());
            return test.Contains("true");
        }

        #endregion

        #endregion
    }
}