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
    /// <summary>
    /// Paramters used in Twitter queries
    /// </summary>
    public interface IFluentTwitterParameters
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        string Activity { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        string Action { get; set; }

        /// <summary>
        /// Gets or sets the since date.
        /// </summary>
        /// <value>The since date.</value>
        DateTime? SinceDate { get; set; }

        /// <summary>
        /// Gets or sets the since id.
        /// </summary>
        /// <value>The since id.</value>
        long? SinceId { get; set; }

        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        long? MaxId { get; set; }

        /// <summary>
        /// Gets or sets the count of objects to return.
        /// </summary>
        /// <value>The count.</value>
        int? Count { get; set; }

        /// <summary>
        /// Gets or sets the page of objects to request.
        /// </summary>
        /// <value>The page.</value>
        int? Page { get; set; }

        /// <summary>
        /// Gets or sets the number if items per page to return
        /// </summary>
        /// <value>The return per page.</value>
        int? ReturnPerPage { get; set; }

        /// <summary>
        /// Gets or sets the requested id.
        /// </summary>
        /// <value>The id.</value>
        long? Id { get; set; }

        /// <summary>
        /// Gets or sets the requested screenname
        /// </summary>
        /// <value>The screenname</value>
        string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the "in reply to status id" parameter
        /// </summary>
        /// <value>The in reply to status id.</value>
        long? InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets the email address parameter.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the user screen name parameter
        /// </summary>
        /// <value>The user screen name parameter.</value>
        string UserScreenName { get; set; }

        /// <summary>
        /// Gets or sets the user id parameter.
        /// </summary>
        /// <value>The user id parameter.</value>
        long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the follow parameter.
        /// </summary>
        /// <value>The follow parameter.</value>
        bool? Follow { get; set; }

        /// <summary>
        /// Gets or sets the verify id parameter.
        /// </summary>
        /// <value>The verify id.</value>
        long? VerifyId { get; set; }

        /// <summary>
        /// Gets or sets the verify screen name parameter.
        /// </summary>
        /// <value>The verify screen name parameter.</value>
        string VerifyScreenName { get; set; }

        // Extras

        /// <summary>
        /// Gets or sets the post image path.
        /// </summary>
        /// <value>The post image path.</value>
        string PostImagePath { get; set; }

        /// <summary>
        /// Gets or sets the post image provider.
        /// </summary>
        /// <value>The post image provider.</value>
        SendPhotoServiceProvider? PostImageProvider { get; set; }

        /// <summary>
        /// Gets or sets a direct URL path to use in place of a fluent query.
        /// </summary>
        string DirectPath { get; set; }

        /// <summary>
        /// Gets or sets the geospatial location of the query, if applicable.
        /// </summary>
        GeoLocation? GeoLocation { get; set; }

        /// <summary>
        /// Gets or sets the Cursor ID for the query
        /// </summary>
        long? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the target id (for friendships/show).
        /// </summary>
        /// <value>The target id.</value>
        long? TargetId { get; set; }

        /// <summary>
        /// Gets or sets the source id (for friendships/show).
        /// </summary>
        /// <value>The source id.</value>
        long? SourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the source screen (for friendships/show).
        /// </summary>
        /// <value>The name of the source screen.</value>
        string SourceScreenName { get; set; }

        /// <summary>
        /// Gets or sets the name of the target screen (for friendships/show).
        /// </summary>
        /// <value>The name of the target screen.</value>
        string TargetScreenName { get; set; }

        /// <summary>
        /// Gets or sets the list slug.
        /// </summary>
        /// <value>The list slug.</value>
        string ListSlug { get; set; }

        /// <summary>
        /// Gets or sets the list id.
        /// </summary>
        /// <value>The list id.</value>
        long? ListId { get; set; }

        /// <summary>
        /// Gets or sets the list mode.
        /// </summary>
        /// <value>The list mode.</value>
        string ListMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the list (for creating lists).
        /// </summary>
        /// <value>The name of the list.</value>
        string ListName { get; set; }

        /// <summary>
        /// Gets or sets the list member id.
        /// </summary>
        /// <value>The list member id.</value>
        long? ListMemberId { get; set; }

        /// <summary>
        /// Gets or sets the list description.
        /// </summary>
        /// <value>The list description.</value>
        string ListDescription { get; set; }

        /// <summary>
        /// Gets or sets the user search query.
        /// </summary>
        /// <value>The user search query.</value>
        string UserSearch { get; set; }
    }
}