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
using TweetSharp.Core.Web;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public static class YammerRelationshipExtensions
    {
        /// <summary>
        /// Adds a subordinate to the specified user's org chart
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="userId">The id of the user whose org chart to update</param>
        /// <param name="emailOfSubordinate">email address of the user's subordinate to add</param>
        /// <returns></returns>
        public static IYammerRelationshipsCreate AddSubordinate(this IYammerRelationships instance, long userId,
                                                                string emailOfSubordinate)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.UserId = userId;
            instance.Root.Parameters.Subordinate = emailOfSubordinate;
            return new YammerRelationshipsCreate(instance.Root);
        }

        /// <summary>
        /// Adds a colleague to the specified user's org chart
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="userId">The id of the user whose org chart to update</param>
        /// <param name="emailOfColleague">email address of the user's colleague to add</param>
        /// <returns></returns>
        public static IYammerRelationshipsCreate AddColleague(this IYammerRelationships instance, long userId,
                                                              string emailOfColleague)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.UserId = userId;
            instance.Root.Parameters.Colleague = emailOfColleague;
            return new YammerRelationshipsCreate(instance.Root);
        }

        /// <summary>
        /// Adds a subordinate to the specified user's org chart
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="userId">The id of the user whose org chart to update</param>
        /// <param name="emailOfSuperior">email address of the user's superior to add</param>
        /// <returns></returns>
        public static IYammerRelationshipsCreate AddSuperior(this IYammerRelationships instance, long userId,
                                                             string emailOfSuperior)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.UserId = userId;
            instance.Root.Parameters.Superior = emailOfSuperior;
            return new YammerRelationshipsCreate(instance.Root);
        }

        /// <summary>
        /// Creates relationships in specified user's org chart
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="userId">The id of the user whose org chart to update</param>
        /// <param name="emailOfSubordinate">email address of the user's subordinate to add</param>
        /// <param name="emailOfSuperior">email address of the user's superior to add</param>
        /// <param name="emailOfColleague">email address of the user's colleague to add</param>
        /// <returns></returns>
        public static IYammerRelationshipsCreate Create(this IYammerRelationships instance, long userId,
                                                        string emailOfColleague, string emailOfSubordinate,
                                                        string emailOfSuperior)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.UserId = userId;
            instance.Root.Parameters.Colleague = emailOfColleague;
            instance.Root.Parameters.Subordinate = emailOfSubordinate;
            instance.Root.Parameters.Superior = emailOfColleague;
            return new YammerRelationshipsCreate(instance.Root);
        }

        /// <summary>
        /// Destroys a relationship in the current user's org chart
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userId">The id of the user whose org chart to update</param>
        /// <param name="userIdToRemove">The user id to remove from the user's org chart</param>
        /// <param name="relationshipType">Type of the relationship from which to remove the specified user.</param>
        /// <returns></returns>
        [Obsolete("Does not appear to work correctly on Yammer's end. Use is not recommended")]
        public static IYammerRelationshipsDestroy Destroy(this IYammerRelationships instance, long userId,
                                                          long userIdToRemove, OrgChartRelationshipType relationshipType)
        {
            instance.Root.Method = WebMethod.Delete;
            instance.Root.Parameters.UserId = userId;
            instance.Root.Parameters.Id = userIdToRemove;
            instance.Root.Parameters.RelationshipType = relationshipType;
            return new YammerRelationshipsDestroy(instance.Root);
        }

        public static IYammerRelationshipsShow Show(this IYammerRelationships instance, long forUserId)
        {
            instance.Root.Parameters.UserId = forUserId;
            return new YammerRelationshipsShow(instance.Root);
        }
    }
}