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
using TweetSharp.Core.Web;

namespace TweetSharp.Fluent
{
    partial class Extensions
    {
        public static ITwitterSearchTrends Current(this ITwitterSearchTrends instance)
        {
            instance.Root.Parameters.Action = "trends/current";
            instance.Root.SearchParameters.SearchCurrentTrends = true;
            instance.Root.Format = WebFormat.Json;
            return instance;
        }

        public static ITwitterSearchTrends Daily(this ITwitterSearchTrends instance)
        {
            instance.Root.Parameters.Action = "trends/daily";
            instance.Root.SearchParameters.SearchDailyTrends = true;
            instance.Root.Format = WebFormat.Json;
            return instance;
        }

        public static ITwitterSearchTrends Weekly(this ITwitterSearchTrends instance)
        {
            instance.Root.Parameters.Action = "trends/weekly";
            instance.Root.SearchParameters.SearchWeeklyTrends = true;
            instance.Root.Format = WebFormat.Json;
            return instance;
        }

        public static ITwitterSearchTrends ExcludeHashtags(this ITwitterSearchTrends instance)
        {
            instance.Root.SearchParameters.SearchExcludesHashtags = true;
            instance.Root.Format = WebFormat.Json;
            return instance;
        }

        public static ITwitterSearchTrends On(this ITwitterSearchTrends instance, DateTime startDate)
        {
            instance.Root.SearchParameters.SearchDate = startDate;
            instance.Root.Format = WebFormat.Json;
            return instance;
        }

        public static ITwitterSearchTrends For(this ITwitterSearchTrends instance, DateTime startDate)
        {
            instance.Root.SearchParameters.SearchDate = startDate;
            instance.Root.Format = WebFormat.Json;
            return instance;
        }
    }
}