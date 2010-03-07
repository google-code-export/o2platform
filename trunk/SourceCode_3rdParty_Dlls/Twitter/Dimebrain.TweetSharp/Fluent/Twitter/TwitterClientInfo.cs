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
using TweetSharp.Core.Attributes;

namespace TweetSharp
{
#if(!SILVERLIGHT)
    ///<summary>
    /// This class provides meta-data for your specific Twitter application, that is
    /// used to identify your client to Twitter, store OAuth credentials for all future
    /// request, and in some cases define a transparent proxy to redirect API calls to.
    ///</summary>
    [Serializable]
#endif

    public class TwitterClientInfo : IClientInfo
    {
        /// <summary>
        /// This is the name of your client application. It is used to
        /// identify your client when a user updates their status, or when
        /// your application makes a Twitter Search API request.
        /// </summary>
        [UserAgent, Header("X-Twitter-Client")]
        public string ClientName { get; set; }

        /// <summary>
        /// This is the version of your application. This is meta-data only,
        /// and not used by Twitter for client processing.
        /// </summary>
        [Header("X-Twitter-Version")]
        public string ClientVersion { get; set; }

        /// <summary>
        /// This is the URL of your application. This is meta-data only,
        /// and not used by Twitter for client processing. Your application's URL
        /// is stored by Twitter when you apply for a 'Source' attribute or register
        /// your application for OAuth.
        /// </summary>
        [Header("X-Twitter-URL")]
        public string ClientUrl { get; set; }

        /// <summary>
        /// If your client is using OAuth authentication, this value should be set
        /// to the value of your consumer key. This avoids having to provide the key
        /// in every query.
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// If your client is using OAuth authentication, this value should be set
        /// to the value of your consumer secret. This avoids having to provide the secret
        /// in every query.
        /// </summary>
        public string ConsumerSecret { get; set; }

#if SILVERLIGHT
        /// <summary>
        /// Since you are communicating from the client-side, this value should point to a 
        /// proxy that is configured to work transparently (API methods are identical other
        /// than the domain), allow cross-domain access, and understand TweetSharp custom 
        /// headers.
        /// </summary>
        public string TransparentProxy { get; set; }
#endif
    }
}