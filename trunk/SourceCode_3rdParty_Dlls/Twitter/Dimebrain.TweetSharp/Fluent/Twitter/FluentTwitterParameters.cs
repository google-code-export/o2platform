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
using TweetSharp.Model;

namespace TweetSharp.Fluent
{
#if !SILVERLIGHT
    [Serializable]
#endif

    public class FluentTwitterParameters : IFluentTwitterParameters
    {
        #region IFluentTwitterParameters Members

        public string Activity { get; set; }
        public string Action { get; set; }
        public DateTime? SinceDate { get; set; }
        public long? SinceId { get; set; }
        public long? MaxId { get; set; }
        public int? Count { get; set; }
        public int? Page { get; set; }
        public int? ReturnPerPage { get; set; }
        public long? Id { get; set; }
        public string ScreenName { get; set; }
        public string Text { get; set; }
        public long? InReplyToStatusId { get; set; }
        public string Email { get; set; }
        public string UserScreenName { get; set; }
        public long? UserId { get; set; }
        public bool? Follow { get; set; }
        public long? VerifyId { get; set; }
        public string VerifyScreenName { get; set; }

        public string PostImagePath { get; set; }
        public SendPhotoServiceProvider? PostImageProvider { get; set; }
        public string DirectPath { get; set; }
        public GeoLocation? GeoLocation { get; set; }
        public long? Cursor { get; set; }
        public long? TargetId { get; set; }
        public long? SourceId { get; set; }
        public string SourceScreenName { get; set; }
        public string TargetScreenName { get; set; }

        public string ListSlug { get; set; }
        public long? ListId { get; set; }
        public string ListMode { get; set; }
        public string ListName { get; set; }
        public long? ListMemberId { get; set; }
        public string ListDescription { get; set; }
        public string UserSearch { get; set; }

        #endregion
    }
}