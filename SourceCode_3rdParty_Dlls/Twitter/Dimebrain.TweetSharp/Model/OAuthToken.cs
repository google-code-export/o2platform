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

namespace TweetSharp.Model
{
    /// <summary>
    /// A data class representing either a request or an access token returned during an OAuth session.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif

    public class OAuthToken
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// Gets or sets the token secret.
        /// </summary>
        public virtual string TokenSecret { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the callback was confirmed.
        /// This value is only populated if request token authorization was requested with a callback.
        /// </summary>
        /// <value><c>true</c> if the callback was confirmed; otherwise, <c>false</c>.</value>
        public virtual bool CallbackConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// This value is only populated if client authentication was used.
        /// </summary>
        /// <value>The user ID.</value>
        public virtual string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user screen name.
        /// This value is only populated if client authentication was used.
        /// </summary>
        /// <value>The user screen name.</value>
        public virtual string ScreenName { get; set; }
    }
}