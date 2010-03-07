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

namespace TweetSharp.Service
{
#if !SILVERLIGHT
    /// <summary>
    /// Options for filtering the Twitter API Filter stream.
    /// </summary>
    /// <seealso cref="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
    [Serializable]
#endif
    public class TwitterSearchOptions
    {
        /// <summary>
        /// Returns tweets by users located within a given radius of the given latitude/longitude.  
        /// The location is preferentially taking from the Geotagging API, 
        /// but will fall back to their Twitter profile. 
        /// 
        /// You cannot use the near phrase operator via the API to geocode arbitrary locations 
        /// but you can use this geocode parameter to search near geocodes directly. 
        /// </summary>
        public GeoLocation Location { get; set; }

        /// <summary>
        /// Returns tweets by users located within a given radius of the given latitude/longitude.  
        /// The location is preferentially taking from the Geotagging API, 
        /// but will fall back to their Twitter profile. 
        /// 
        /// You cannot use the near phrase operator via the API to geocode arbitrary locations 
        /// but you can use this geocode parameter to search near geocodes directly. 
        /// </summary>
        public int LocationRadiusMiles { get; set; }

        /// <summary>
        /// Specify the language of the query you are sending (only ja is currently effective). 
        /// This is intended for language-specific clients and the default should work
        /// in the majority of cases. 
        /// </summary>
        /// <seealso cref="http://en.wikipedia.org/wiki/ISO_639-1" />
        public string LocaleIso { get; set; }

        /// <summary>
        /// Restricts tweets to the given language, given by an ISO 639-1 code. 
        /// </summary>
        /// <seealso cref="http://en.wikipedia.org/wiki/ISO_639-1"/>
        public string LanguageIso { get; set; }
    }
}