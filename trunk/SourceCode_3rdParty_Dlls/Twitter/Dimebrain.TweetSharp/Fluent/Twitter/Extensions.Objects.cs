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
    /// <summary>
    /// Extension methods for the IFluentTwitter Interface
    /// </summary>
    public static partial class IFluentTwitterExtensions
    {
        /// <summary>
        /// Accesses the "Statuses" (aka. Tweets) subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterStatuses instance with methods for dealing with Statuses</returns>
        public static ITwitterStatuses Statuses(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "statuses";
            return new TwitterStatuses(instance);
        }

        /// <summary>
        /// Accesses the "Users" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterUsers instance with methods for dealing with Users</returns>
        public static ITwitterUsers Users(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "users";
            return new TwitterUsers(instance);
        }

        /// <summary>
        /// Accesses the "Spam" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterSpam instance with methods for dealing with Spam</returns>
        public static ITwitterReportSpam ReportSpam(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "report_spam";
            return new TwitterReportSpam(instance);
        }

        /// <summary>
        /// Accesses the "Direct Messages" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterDirectMessages instance with methods for dealing with Direct Messages</returns>
        public static ITwitterDirectMessages DirectMessages(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "direct_messages";
            return new TwitterDirectMessages(instance);
        }

        /// <summary>
        /// Accesses the "Friendships" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterFriendships instance with methods for dealing with Friendships</returns>
        public static ITwitterFriendships Friendships(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "friendships";
            return new TwitterFriendships(instance);
        }

        /// <summary>
        /// Accesses the "Social Graph" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterSocialGraph instance with methods for dealing with the Social Graph</returns>
        public static ITwitterSocialGraph SocialGraph(this IFluentTwitter instance)
        {
            return new TwitterSocialGraph(instance);
        }

        /// <summary>
        /// Accesses the "Account" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterAccount instance with methods for dealing with Accounts</returns>
        public static ITwitterAccount Account(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "account";
            return new TwitterAccount(instance);
        }

        /// <summary>
        /// Accesses the "Favorites" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterFavoites instance with methods for dealing with Favorites</returns>
        public static ITwitterFavorites Favorites(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "favorites";
            return new TwitterFavorites(instance);
        }

        /// <summary>
        /// Accesses the "Notifications" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterNotifications instance with methods for dealing with Notifications</returns>
        public static ITwitterNotifications Notifications(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "notifications";
            return new TwitterNotifications(instance);
        }

        /// <summary>
        /// Accesses the "Blocking" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterBlocks instance with methods for adding/removing Blocks</returns>
        public static ITwitterBlocks Blocking(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "blocks";
            return new TwitterBlocks(instance);
        }

        /// <summary>
        /// Accesses the "Saved Searches" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterSavedSearches instance with methods for querying the twitter service status</returns>
        public static ITwitterSavedSearches SavedSearches(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "saved_searches";
            return new TwitterSavedSearches(instance);
        }

        /// <summary>
        /// Accesses the "Help" subset of the REST API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>An ITwitterHelp instance with methods for querying the twitter service status</returns>
        public static ITwitterHelp Help(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "help";
            return new TwitterHelp(instance);
        }

        /// <summary>
        /// Accesses the Search API
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A ITwitterSearch instance with methods for performing searches</returns>
        public static ITwitterSearch Search(this IFluentTwitter instance)
        {
            instance.Parameters.Action = "search";
            return new TwitterSearch(instance);
        }

        /// <summary>
        /// Accesses the Photo Posting features
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A <see cref="ITwitterPhotos" /> instance with methods for performing photo posting</returns>
        public static ITwitterPhotos Photos(this IFluentTwitter instance)
        {
            return new TwitterPhotos(instance);
        }

        /// <summary>
        /// Accesses the raw URL path provided, rather than using a fluent interface.
        /// This method is meant as a safety mechanism when the Twitter API changes, but
        /// TweetSharp doesn't currently reflect the change.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="urlPath">The raw URL path, i.e. "/users/show/bob.xml"</param>
        /// <returns>A IFluentTwitterDirect instance with methods for performing searches</returns>
        public static IFluentTwitterDirect Direct(this IFluentTwitter instance, string urlPath)
        {
            instance.Parameters.DirectPath = urlPath;
            return new FluentTwitterDirect(instance);
        }

        /// <summary>
        /// Accesses the Twitter Lists features
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A <see cref="ITwitterLists" /> instance with methods for performing list handling</returns>
        public static ITwitterLists Lists(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "lists";
            return new TwitterLists(instance);
        }

        public static ITwitterStream Stream(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "stream";
            return new TwitterStream(instance);
        }

        public static ITwitterTrends Trends(this IFluentTwitter instance)
        {
            instance.Parameters.Activity = "trends";
            return new TwitterTrends(instance);
        }
    }
}