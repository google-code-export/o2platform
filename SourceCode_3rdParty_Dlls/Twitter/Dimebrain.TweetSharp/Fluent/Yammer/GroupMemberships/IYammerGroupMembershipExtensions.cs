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

namespace TweetSharp.Fluent
{
    public static class YammerGroupMembershipExtensions
    {
        /// <summary>
        /// Adds the current user to the specified group
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="groupId">Id of the group to which to add the current user</param>
        /// <returns></returns>
        public static IYammerGroupMembershipsJoin JoinGroup(this IYammerGroupMemberships instance, long groupId)
        {
            instance.Root.Parameters.GroupID = groupId;
            instance.Root.Method = WebMethod.Post;
            return new YammerGroupMembershipsJoin(instance.Root);
        }


        /// <summary>
        /// Removes the current user from the specified group.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="groupId">Id of the group from which to remove the current user</param>
        /// <returns></returns>
        public static IYammerGroupMembershipsLeave LeaveGroup(this IYammerGroupMemberships instance, long groupId)
        {
            instance.Root.Parameters.GroupID = groupId;
            instance.Root.Method = WebMethod.Delete;
            return new YammerGroupMembershipsLeave(instance.Root);
        }
    }
}