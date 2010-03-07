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

namespace TweetSharp.Core.OAuth
{
    /// <summary>
    /// Methods of including OAuth parameters in a signed request.
    /// </summary>
    /// <seealso cref="http://oauth.net/core/1.0#signing_process"/>
#if !SILVERLIGHT
    [Serializable]
#endif

    public enum OAuthParameterHandling
    {
        /// <summary>
        /// OAuth parameters are served in the HTTP Authorization header.
        /// </summary>
        HttpAuthorizationHeader,
        /// <summary>
        /// OAuth parameters are part of the URI, if using GET, or sent
        /// as POST parameters, if using POST.
        /// </summary>
        UrlOrPostParameters
    }
}