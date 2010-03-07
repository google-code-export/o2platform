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
    public interface IFluentTwitterSearchParameters
    {
        string SearchPhrase { get; set; }
        string SearchFromUser { get; set; }
        string SearchToUser { get; set; }
        string SearchHashTag { get; set; }
        string SearchReferences { get; set; }
        string SearchNear { get; set; }
        double? SearchMiles { get; set; }
        DateTime? SearchSince { get; set; }
        DateTime? SearchSinceUntil { get; set; }
        DateTime? SearchDate { get; set; }

        bool? SearchNegativity { get; set; }
        bool? SearchPositivity { get; set; }
        bool? SearchContainingLinks { get; set; }
        bool? SearchShowUser { get; set; }
        bool? SearchQuestion { get; set; }

        bool? SearchCurrentTrends { get; set; }
        bool? SearchExcludesHashtags { get; set; }
        bool? SearchDailyTrends { get; set; }
        bool? SearchWeeklyTrends { get; set; }

        string SearchLanguage { get; set; }
        string SearchLocale { get; set; }
        double? SearchGeoLatitude { get; set; }
        double? SearchGeoLongitude { get; set; }
        string SearchWithoutPhrase { get; set; }
    }
}