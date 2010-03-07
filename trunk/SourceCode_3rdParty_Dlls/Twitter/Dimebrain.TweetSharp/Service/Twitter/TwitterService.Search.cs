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
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Service
{
    partial class TwitterService
    {
        #region Search

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phrase"></param>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-search"/>
        /// <returns></returns>
        public TwitterSearchResult SearchForTweets(string phrase)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                );
        }

        public TwitterSearchResult SearchForTweets(string phrase, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweets(string phrase, int page)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase).Skip(page)
                );
        }

        public TwitterSearchResult SearchForTweets(string phrase, int page, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Skip(page)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweets(string phrase, int page, int count)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q =>
                                                          q.Search().Query().Containing(phrase).Skip(page).Take(count)
                );
        }

        public TwitterSearchResult SearchForTweets(string phrase, int page, int count, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Skip(page).Take(count)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase).Since(sinceId)
                );
        }

        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Since(sinceId)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Since(sinceId).Skip(page)
                );
        }

        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page,
                                                        TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Since(sinceId).Skip(page)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page, int count)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Since(sinceId).Skip(page).Take(count)
                );
        }

        public TwitterSearchResult SearchForTweetsSince(long sinceId, string phrase, int page, int count,
                                                        TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Since(sinceId).Skip(page).Take(count)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase).Before(maxId)
                );
        }

        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Before(maxId)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q =>
                                                          q.Search().Query().Containing(phrase).Before(maxId).Skip(page)
                );
        }

        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page,
                                                         TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Before(maxId).Skip(page)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page, int count)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q =>
                                                          q.Search().Query().Containing(phrase).Before(maxId).Skip(page)
                                                              .Take(count)
                );
        }

        public TwitterSearchResult SearchForTweetsBefore(long maxId, string phrase, int page, int count,
                                                         TwitterSearchOptions options)
        {
            return WithTweetSharp<TwitterSearchResult>(
                                                          q => q.Search().Query().Containing(phrase)
                                                                   .Before(maxId).Skip(page).Take(count)
                                                                   .InLanguage(options.LanguageIso)
                                                                   .InLocale(options.LocaleIso)
                                                                   .Within(options.LocationRadiusMiles)
                                                                   .Of(options.Location)
                );
        }

        #endregion

        #region Trends

        /// <summary>
        /// Returns the current top 10 trending topics on Twitter.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends"/>
        /// <returns></returns>
        public ITwitterSearchTrends SearchCurrentTrends()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Current()
                );
        }

        public ITwitterSearchTrends SearchCurrentTrendsWithoutHashtags()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Current().ExcludeHashtags()
                );
        }

        /// <summary>
        /// Returns the top 20 trending topics for each hour in a given day.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-daily"/>
        /// <returns></returns>
        public ITwitterSearchTrends SearchDailyTrends()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Daily()
                );
        }

        public ITwitterSearchTrends SearchDailyTrends(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Daily().On(startDate)
                );
        }

        public ITwitterSearchTrends SearchDailyTrendsWithoutHashtags()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Daily().ExcludeHashtags()
                );
        }

        public ITwitterSearchTrends SearchDailyTrendsWithoutHashtags(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q =>
                                                          q.Search().Trends().Daily().On(startDate).ExcludeHashtags()
                );
        }

        /// <summary>
        /// Returns the top 30 trending topics for each day in a given week.
        /// </summary>
        /// <seealso cref="http://apiwiki.twitter.com/Twitter-Search-API-Method%3A-trends-weekly"/>
        /// <returns></returns>
        public ITwitterSearchTrends SearchWeeklyTrends()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Weekly()
                );
        }

        public ITwitterSearchTrends SearchWeeklyTrends(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Weekly().On(startDate)
                );
        }

        public ITwitterSearchTrends SearchWeeklyTrendsWithoutHashtags()
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q => q.Search().Trends().Weekly()
                );
        }

        public ITwitterSearchTrends SearchWeeklyTrendsWithoutHashtags(DateTime startDate)
        {
            return WithTweetSharp<TwitterSearchTrends>(
                                                          q =>
                                                          q.Search().Trends().Weekly().On(startDate).ExcludeHashtags()
                );
        }

        #endregion
    }
}