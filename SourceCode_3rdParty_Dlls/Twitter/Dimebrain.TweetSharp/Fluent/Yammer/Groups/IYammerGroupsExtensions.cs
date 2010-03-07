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
    public static class IYammerGroupsExtensions
    {
        /// <summary>
        /// Gets all groups in the network
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns></returns>
        public static IYammerGroups All(this IYammerGroups instance)
        {
            return new YammerGroups(instance.Root);
        }

        /// <summary>
        /// Gets groups that begin with the specified letter
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="startingWith">The letter to search for</param>
        /// <returns></returns>
        public static IYammerGroups StartingWith(this IYammerGroups instance, char startingWith)
        {
            instance.Root.Parameters.StartingWith = startingWith;
            return new YammerGroups(instance.Root);
        }

        /// <summary>
        /// Gets the specified page of groups (20 per page)
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="page">The requested page</param>
        /// <returns></returns>
        public static IYammerGroups Page(this IYammerGroups instance, int page)
        {
            instance.Root.Parameters.Page = page;
            return new YammerGroups(instance.Root);
        }

        /// <summary>
        /// Gets groups from the network sorted using the provided criterion
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="sortGroupsBy">The desired sort</param>
        /// <returns></returns>
        public static IYammerGroupsSorted SortedBy(this IYammerGroups instance, SortGroupsBy sortGroupsBy)
        {
            instance.Root.Parameters.SortGroupsBy = sortGroupsBy;
            return new YammerGroupsSorted(instance.Root);
        }

        /// <summary>
        /// Creates a new list in the network
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="name">The desired name for the list</param>
        /// <returns></returns>
        public static IYammerGroupsCreate Create(this IYammerGroups instance, string name)
        {
            instance.Root.Method = WebMethod.Post;
            instance.Root.Parameters.GroupName = name;
            return new YammerGroupsCreate(instance.Root);
        }

        /// <summary>
        /// Creates a new group in the network
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="name">the desired name for the list</param>
        /// <param name="privacy">the desired privacy setting for the list</param>
        /// <returns></returns>
        public static IYammerGroupsCreate Create(this IYammerGroups instance, string name, YammerGroupPrivacy privacy)
        {
            instance.Root.Parameters.GroupName = name;
            instance.Root.Parameters.Private = privacy == YammerGroupPrivacy.Private;
            return new YammerGroupsCreate(instance.Root);
        }

        /// <summary>
        /// Updates an existing group
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="group">The group to update</param>
        /// <returns></returns>
        public static IYammerGroupsUpdate Update(this IYammerGroups instance, YammerGroup group)
        {
            instance.Root.Method = WebMethod.Put;
            instance.Root.Parameters.Id = group.Id;
            return new YammerGroupsUpdate(instance.Root);
        }

        /// <summary>
        /// Updates an existing group
        /// </summary>
        /// <param name="instance">the instance</param>
        /// <param name="groupId">The id of the group to update</param>
        /// <returns></returns>
        public static IYammerGroupsUpdate Update(this IYammerGroups instance, long groupId)
        {
            instance.Root.Method = WebMethod.Put;
            instance.Root.Parameters.Id = groupId;
            return new YammerGroupsUpdate(instance.Root);
        }
    }
}