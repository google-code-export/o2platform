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

namespace TweetSharp.Fluent
{
#if(!SILVERLIGHT)
    [Serializable]
#endif

    public class FluentTwitterSearchParameters : IFluentTwitterSearchParameters
    {
        #region IFluentTwitterSearchParameters Members

        public string SearchPhrase { get; set; }
        public string SearchWithoutPhrase { get; set; }
        public string SearchFromUser { get; set; }
        public string SearchToUser { get; set; }
        public string SearchHashTag { get; set; }
        public string SearchReferences { get; set; }
        public string SearchNear { get; set; }
        public double? SearchMiles { get; set; }
        public DateTime? SearchSince { get; set; }
        public DateTime? SearchSinceUntil { get; set; }
        public DateTime? SearchDate { get; set; }
        public bool? SearchNegativity { get; set; }
        public bool? SearchPositivity { get; set; }
        public bool? SearchContainingLinks { get; set; }
        public bool? SearchCurrentTrends { get; set; }
        public bool? SearchExcludesHashtags { get; set; }
        public bool? SearchDailyTrends { get; set; }
        public bool? SearchWeeklyTrends { get; set; }
        public bool? SearchShowUser { get; set; }
        public string SearchLanguage { get; set; }
        public string SearchLocale { get; set; }

        public double? SearchGeoLatitude { get; set; }
        public double? SearchGeoLongitude { get; set; }
        public bool? SearchQuestion { get; set; }

        #endregion
    }
}