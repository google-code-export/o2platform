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
using System.ComponentModel;
using TweetSharp.Core.Attributes;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// A list of supported URL shortening services.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public enum ShortenUrlServiceProvider
    {
        /// <summary>
        /// http://to.m8.to
        /// </summary>
        [Description("m8.to")] Tomato,
        /// <summary>
        /// http://tr.im
        /// </summary>
        [Description("tr.im")]
        [RequiresApiKey(false)] 
        [RequiresAuthentication(false)] Trim,
        /// <summary>
        /// http://bit.ly
        /// </summary>
        [Description("bit.ly")] 
        [RequiresAuthentication(AuthenticationScheme.Parameters)] 
        Bitly,
        /// <summary>
        /// http://is.gd
        /// </summary>
        [Description("is.gd")] IsGd,
        /// <summary>
        /// http://tinyurl.com
        /// </summary>
        [Description("tinyurl")] TinyUrl
    }
}