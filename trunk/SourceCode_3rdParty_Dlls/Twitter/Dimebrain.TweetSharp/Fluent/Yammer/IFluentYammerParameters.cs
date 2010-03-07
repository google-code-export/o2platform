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
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public enum SortGroupsBy
    {
        Messages,
        Members,
        Privacy,
        Created_At,
        Creator
    }

    public enum SortUsersBy
    {
        Messages,
        Followers
    }

    public interface IFluentYammerParameters
    {
        string Activity { get; set; }
        string Action { get; set; }
        long? OlderThan { get; set; }
        long? NewerThan { get; set; }
        bool Threaded { get; set; }
        long? ThreadID { get; set; }
        int? Count { get; set; }
        int? Page { get; set; }
        int? ReturnPerPage { get; set; }
        long? Id { get; set; }
        string ScreenName { get; set; }
        string Body { get; set; }
        string Email { get; set; }
        string UserScreenName { get; set; }
        long? UserId { get; set; }
        bool UseCurrentAsUserId { get; set; }
        long? BotID { get; set; }
        long? GroupID { get; set; }
        bool? Follow { get; set; }
        string Tag { get; set; }
        long? DirectToUser { get; set; }
        long? ToGroupID { get; set; }
        long? InReplyTo { get; set; }
        long? MessageId { get; set; }
        char? StartingWith { get; set; }
        string GroupName { get; set; }
        SortGroupsBy? SortGroupsBy { get; set; }
        SortUsersBy? SortUsersBy { get; set; }
        bool? Reverse { get; set; }
        bool? Private { get; set; }
        IList<string> Attachments { get; }
        YammerUser UserData { get; set; }
        string Colleague { get; set; }
        string Superior { get; set; }
        string Subordinate { get; set; }
        long? TargetId { get; set; }
        string TargetType { get; set; }
        string Prefix { get; set; }
        string Search { get; set; }
        OrgChartRelationshipType? RelationshipType { get; set; }
    }
}