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
using TweetSharp.Core.Extensions;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// Methods for the Twitter Lists API
    /// </summary>
    public static class ITwitterListsExtensions
    {
        /// <summary>
        /// Gets lists belonging to the specified owner.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsGet GetListsBy(this ITwitterLists instance,
                                                  string listOwnerScreenName)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            return new TwitterListsGet(instance.Root);
        }

        /// <summary>
        /// Gets a single list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsShow GetListBy(this ITwitterLists instance,
                                                  string listOwnerScreenName,
                                                  string listSlug)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsShow(instance.Root);
        }

        /// <summary>
        /// Gets a single list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsShow GetListBy(this ITwitterLists instance,
                                                  string listOwnerScreenName,
                                                  long listId)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsShow(instance.Root);
        }

        /// <summary>
        /// Gets a single list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsShow GetListBy(this ITwitterLists instance,
                                                  string listOwnerScreenName,
                                                  int listId)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsShow(instance.Root);
        }

        /// <summary>
        /// Deletes a list.
        /// The authenticating user must be the owner of the list to delete it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list ID.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsDelete DeleteList(this ITwitterLists instance,
                                                     string listOwnerScreenName,
                                                     long listId)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsDelete(instance.Root);
        }

        /// <summary>
        /// Deletes a list.
        /// The authenticating user must be the owner of the list to delete it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsDelete DeleteList(this ITwitterLists instance,
                                                     string listOwnerScreenName,
                                                     string listSlug)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsDelete(instance.Root);
        }

        /// <summary>
        /// Deletes a list.
        /// The authenticating user must be the owner of the list to delete it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsDelete DeleteList(this ITwitterLists instance,
                                                     TwitterList list)
        {
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            return new TwitterListsDelete(instance.Root);
        }

        /// <summary>
        /// Creates a public list.
        /// The authenticating user must be the owner of the list to create it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listName">Name of the list.</param>
        /// <param name="listDescription">The list description.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsCreate CreatePublicList(this ITwitterLists instance,
                                                           string listOwnerScreenName,
                                                           string listName,
                                                           string listDescription)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListName = listName;
            instance.Root.Parameters.ListMode = "public";
            instance.Root.Parameters.ListDescription = listDescription;
            return new TwitterListsCreate(instance.Root);
        }

        /// <summary>
        /// Creates a public list.
        /// The authenticating user must be the owner of the list to create it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsCreate CreatePublicList(this ITwitterLists instance,
                                                           TwitterList list)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListName = list.Name;
            instance.Root.Parameters.ListMode = "public";
            instance.Root.Parameters.ListDescription = list.Description;
            return new TwitterListsCreate(instance.Root);
        }

        /// <summary>
        /// Creates a private list.
        /// The authenticating user must be the owner of the list to create it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listName">The name of the list.</param>
        /// <param name="listDescription">The list description.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsCreate CreatePrivateList(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            string listName,
                                                            string listDescription)
        {
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListName = listName;
            instance.Root.Parameters.ListMode = "private";
            instance.Root.Parameters.ListDescription = listDescription;
            return new TwitterListsCreate(instance.Root);
        }

        /// <summary>
        /// Creates a private list.
        /// The authenticating user must be the owner of the list to create it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsCreate CreatePrivateList(this ITwitterLists instance,
                                                            TwitterList list)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListName = list.Name;
            instance.Root.Parameters.ListDescription = list.Description;
            instance.Root.Parameters.ListMode = "private";
            return new TwitterListsCreate(instance.Root);
        }

        /// <summary>
        /// Updates the given list. 
        /// The authenticating user must be the owner of the list to update it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsUpdate UpdateList(this ITwitterLists instance,
                                                     TwitterList list)
        {
            instance.Root.Parameters.ListId = list.Id;
            instance.Root.Parameters.ListName = list.Name;
            instance.Root.Parameters.ListMode = list.Mode;
            instance.Root.Parameters.ListDescription = list.Description;
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            return new TwitterListsUpdate(instance.Root);
        }

        /// <summary>
        /// Gets the status timeline of a specified list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsStatuses GetStatuses(this ITwitterLists instance,
                                                        string listOwnerScreenName,
                                                        int listId)
        {
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsStatuses(instance.Root);
        }

        /// <summary>
        /// Gets the status timeline of a specified list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsStatuses GetStatuses(this ITwitterLists instance,
                                                        string listOwnerScreenName,
                                                        long listId)
        {
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsStatuses(instance.Root);
        }

        /// <summary>
        /// Gets the status timeline of a specified list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        public static ITwitterListsStatuses GetStatuses(this ITwitterLists instance,
                                                        string listOwnerScreenName,
                                                        string listSlug)
        {
            instance.Root.Parameters.Action = "statuses";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsStatuses(instance.Root);
        }

        /// <summary>
        /// Gets the lists the list owner is listed on.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMemberships GetMemberships(this ITwitterLists instance,
                                                              string listOwnerScreenName)
        {
            instance.Root.Parameters.Action = "memberships";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the lists the list owner follows.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMemberships GetSubscriptions(this ITwitterLists instance,
                                                                string listOwnerScreenName)
        {
            instance.Root.Parameters.Action = "subscriptions";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the users that are members of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMemberships GetMembersOf(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            int listId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the users that are members of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMemberships GetMembersOf(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            string listSlug)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the users that are members of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner screen name.</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMemberships GetMembersOf(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            long listId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the users that are members of a given list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMemberships GetMembersOf(this ITwitterLists instance,
                                                            TwitterList list)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the followers of a list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name.</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribers GetSubscribersOf(this ITwitterLists instance,
                                                                string listOwnerScreenName,
                                                                long listId)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsSubscribers(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterListsSubscribers GetSubscribersOf(this ITwitterLists instance,
                                                                string listOwnerScreenName,
                                                                int listId)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsSubscribers(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterListsMemberships GetSubscribersOf(this ITwitterLists instance,
                                                                string listOwnerScreenName,
                                                                string listSlug)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsMemberships(instance.Root);
        }

        /// <summary>
        /// Gets the followers of a list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribers GetSubscribersOf(this ITwitterLists instance,
                                                                TwitterList list)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            return new TwitterListsSubscribers(instance.Root);
        }

        /// <summary>
        /// Determines whether a list contains a user.
        /// </summary>
        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserMemberOf(this ITwitterLists instance,
                                                           TwitterList list,
                                                           TwitterUser user)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.Action = "members_id";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            instance.Root.Parameters.UserId = user.Id;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Determines whether a list contains a user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The ID of the list to test for membership.</param>
        /// <param name="userId">The ID of the user to check for within the list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserMemberOf(this ITwitterLists instance,
                                                           string listOwnerScreenName,
                                                           int listId,
                                                           int userId)
        {
            instance.Root.Parameters.Action = "members_id";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserId = userId;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Determines whether a list contains a user.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The ID of the list to test for membership.</param>
        /// <param name="userId">The ID of the user to check for within the list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserMemberOf(this ITwitterLists instance,
                                                           string listOwnerScreenName,
                                                           long listId,
                                                           long userId)
        {
            instance.Root.Parameters.Action = "members_id";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserId = userId;
            return new TwitterListsIsMember(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserMemberOf(this ITwitterLists instance,
                                                           string listOwnerScreenName,
                                                           string listSlug,
                                                           long userId)
        {
            instance.Root.Parameters.Action = "members_id";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.UserId = userId;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Determines whether a user follows a list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserFollowerOf(this ITwitterLists instance,
                                                             TwitterList list,
                                                             TwitterUser user)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.Action = "subscribers_id";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            instance.Root.Parameters.UserId = user.Id;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Determines whether a user follows a list.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The ID of the list to test for a subscription.</param>
        /// <param name="userId">The ID of the user to check against  the list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserFollowerOf(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             int listId,
                                                             int userId)
        {
            instance.Root.Parameters.Action = "subscribers_id";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserId = userId;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Determines whether the given user is a subscriber of the the specified list
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The ID of the list to test for a subscription.</param>
        /// <param name="userId">The ID of the user to check against the list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsIsMember IsUserFollowerOf(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             long listId,
                                                             long userId)
        {
            instance.Root.Parameters.Action = "subscribers_id";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.UserId = userId;
            return new TwitterListsIsMember(instance.Root);
        }

        public static ITwitterListsIsMember IsUserFollowerOf(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             string listSlug,
                                                             long userId)
        {
            instance.Root.Parameters.Action = "subscribers_id";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.UserId = userId;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Adds a new list member.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsIsMember AddMemberTo(this ITwitterLists instance,
                                                        TwitterList list,
                                                        TwitterUser user)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            instance.Root.Parameters.UserId = user.Id;
            return new TwitterListsIsMember(instance.Root);
        }

        /// <summary>
        /// Adds a new list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersCreate AddMemberTo(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             long listId,
                                                             long userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersCreate(instance.Root);
        }

        /// <summary>
        /// Adds a new list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersCreate AddMemberTo(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             string listSlug,
                                                             long userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersCreate(instance.Root);
        }

        /// <summary>
        /// Adds a new list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersCreate AddMemberTo(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             string listSlug,
                                                             int userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersCreate(instance.Root);
        }

        /// <summary>
        /// Adds a new list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersCreate AddMemberTo(this ITwitterLists instance,
                                                             string listOwnerScreenName,
                                                             int listId,
                                                             int userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersCreate(instance.Root);
        }

        /// <summary>
        /// Removes a list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersDelete RemoveMemberFrom(this ITwitterLists instance,
                                                                  TwitterList list,
                                                                  TwitterUser user)
        {
            ValidateListInputs(list);

            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            instance.Root.Parameters.UserId = user.Id;
            return new TwitterListsMembersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersDelete RemoveMemberFrom(this ITwitterLists instance,
                                                                  string listOwnerScreenName,
                                                                  long listId,
                                                                  long userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersDelete RemoveMemberFrom(this ITwitterLists instance,
                                                                  string listOwnerScreenName,
                                                                  string listSlug,
                                                                  long userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listSlug">The list slug.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersDelete RemoveMemberFrom(this ITwitterLists instance,
                                                                  string listOwnerScreenName,
                                                                  string listSlug,
                                                                  int userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list member.
        /// The authenticating user must be the owner of the list to change it.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsMembersDelete RemoveMemberFrom(this ITwitterLists instance,
                                                                  string listOwnerScreenName,
                                                                  int listId,
                                                                  int userId)
        {
            instance.Root.Parameters.Action = "members";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            instance.Root.Parameters.ListMemberId = userId;
            return new TwitterListsMembersDelete(instance.Root);
        }


        /// <summary>
        /// Adds a list to the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersCreate Follow(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            long listId)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsSubscribersCreate(instance.Root);
        }

        /// <summary>
        /// Adds a list to the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersCreate Follow(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            int listId)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsSubscribersCreate(instance.Root);
        }

        /// <summary>
        /// Adds a list to the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersCreate Follow(this ITwitterLists instance,
                                                            string listOwnerScreenName,
                                                            string listSlug)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsSubscribersCreate(instance.Root);
        }

        /// <summary>
        /// Adds a list to the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersCreate Follow(this ITwitterLists instance,
                                                            TwitterList list)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListId = list.Id;
            return new TwitterListsSubscribersCreate(instance.Root);
        }

        /// <summary>
        /// Removes a list from the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersDelete Unfollow(this ITwitterLists instance,
                                                              string listOwnerScreenName,
                                                              long listId)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsSubscribersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list from the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listId">The list id.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersDelete Unfollow(this ITwitterLists instance,
                                                              string listOwnerScreenName,
                                                              int listId)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListId = listId;
            return new TwitterListsSubscribersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list from the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="listOwnerScreenName">The list owner's screen name</param>
        /// <param name="listSlug">The list slug.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersDelete Unfollow(this ITwitterLists instance,
                                                              string listOwnerScreenName,
                                                              string listSlug)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = listOwnerScreenName;
            instance.Root.Parameters.ListSlug = listSlug;
            return new TwitterListsSubscribersDelete(instance.Root);
        }

        /// <summary>
        /// Removes a list from the authenticating user's followed lists.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        [RequiresAuthentication]
        public static ITwitterListsSubscribersDelete Unfollow(this ITwitterLists instance,
                                                              TwitterList list)
        {
            instance.Root.Parameters.Action = "subscribers";
            instance.Root.Parameters.UserScreenName = list.User.ScreenName.ToLower();
            instance.Root.Parameters.ListSlug = list.Slug;
            return new TwitterListsSubscribersDelete(instance.Root);
        }

        private static void ValidateListInputs(TwitterList list)
        {
            if (list.User == null || list.User.ScreenName.IsNullOrBlank())
            {
                throw new TweetSharpException("You must provide the authenticating user to create a list.");
            }
        }
    }
}