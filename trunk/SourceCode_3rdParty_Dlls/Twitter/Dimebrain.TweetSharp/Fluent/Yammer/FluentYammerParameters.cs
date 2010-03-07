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
using System.Collections.Generic;
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    [Serializable]
    public class FluentYammerParameters : IFluentYammerParameters
    {
        private readonly List<string> _attachments = new List<string>(20);

        #region IFluentYammerParameters Members

        public string Activity { get; set; }
        public string Action { get; set; }
        public long? OlderThan { get; set; }
        public long? NewerThan { get; set; }
        public bool Threaded { get; set; }
        public long? ThreadID { get; set; }
        public int? Count { get; set; }
        public int? Page { get; set; }
        public int? ReturnPerPage { get; set; }
        public long? Id { get; set; }
        public bool UseCurrentAsUserId { get; set; }
        public string ScreenName { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string UserScreenName { get; set; }
        public long? UserId { get; set; }
        public long? BotID { get; set; }
        public long? GroupID { get; set; }
        public bool? Follow { get; set; }
        public string Tag { get; set; }
        public long? DirectToUser { get; set; }
        public long? ToGroupID { get; set; }
        public long? InReplyTo { get; set; }
        public long? MessageId { get; set; }
        public char? StartingWith { get; set; }
        public string GroupName { get; set; }
        public SortGroupsBy? SortGroupsBy { get; set; }
        public SortUsersBy? SortUsersBy { get; set; }
        public bool? Reverse { get; set; }
        public bool? Private { get; set; }
        public YammerUser UserData { get; set; }
        public string Colleague { get; set; }
        public string Superior { get; set; }
        public string Subordinate { get; set; }
        public long? TargetId { get; set; }
        public string TargetType { get; set; }
        public string Prefix { get; set; }
        public string Search { get; set; }
        public OrgChartRelationshipType? RelationshipType { get; set; }

        public IList<string> Attachments
        {
            get { return _attachments; }
        }

        #endregion
    }
}