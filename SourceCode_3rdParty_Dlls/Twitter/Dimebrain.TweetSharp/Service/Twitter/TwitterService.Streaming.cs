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
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Service
{
    partial class TwitterService
    {
        #region Filter

        public void StreamFilter()
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter()
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(TwitterFilterStreamOptions options)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter()
                                            .Following(options.FollowingUserIds)
                                            .WithBacklog(options.Backlog)
                                            .Tracking(options.TrackingKeywords)
                                            .Within(options.BoundingGeoLocations)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(TwitterFilterStreamOptions options, TimeSpan duration)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter()
                                            .Following(options.FollowingUserIds)
                                            .For(duration)
                                            .WithBacklog(options.Backlog)
                                            .Tracking(options.TrackingKeywords)
                                            .Within(options.BoundingGeoLocations)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(TwitterFilterStreamOptions options, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter()
                                            .Following(options.FollowingUserIds)
                                            .Take(resultsPerCallback)
                                            .WithBacklog(options.Backlog)
                                            .Tracking(options.TrackingKeywords)
                                            .Within(options.BoundingGeoLocations)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(TwitterFilterStreamOptions options, TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter()
                                            .Following(options.FollowingUserIds)
                                            .For(duration).Take(resultsPerCallback)
                                            .WithBacklog(options.Backlog)
                                            .Tracking(options.TrackingKeywords)
                                            .Within(options.BoundingGeoLocations)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(TimeSpan duration)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter().For(duration)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter().For(duration).Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFilter(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromFilter().Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        #endregion

        #region Firehose

        public void StreamFirehose()
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample()
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFirehose(TimeSpan duration)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFirehose(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamFirehose(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        #endregion

        #region Retweets

        public void StreamRetweets()
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample()
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamRetweets(TimeSpan duration)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamRetweets(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamRetweets(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        #endregion

        #region Sample

        public void StreamSample()
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample()
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamSample(TimeSpan duration)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamSample(TimeSpan duration, int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().For(duration).Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        public void StreamSample(int resultsPerCallback)
        {
            WithTweetSharpAsync(
                                   q => q.Stream().FromSample().Take(resultsPerCallback)
                                            .CallbackTo((s, r) => RaiseStreamResults(r))
                );
        }

        #endregion

        private void RaiseStreamResults(TwitterResult result)
        {
            var statuses = result.AsStatuses();
            var args = new TwitterStreamResultEventArgs(statuses);
            OnStreamResult(args);
        }
    }
}