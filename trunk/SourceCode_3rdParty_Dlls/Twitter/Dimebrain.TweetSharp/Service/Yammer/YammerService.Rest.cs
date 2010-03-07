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

using System.Collections.Generic;
using TweetSharp.Fluent;
using TweetSharp.Model;

namespace TweetSharp.Service
{
    partial class YammerService
    {
        #region Messages (Viewing)

        #region All

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="https://www.yammer.com/api_doc.html"/>
        /// <returns></returns>
        public IEnumerable<YammerMessage> ListMessages()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All()
                );
        }

        public IEnumerable<YammerMessage> ListMessagesSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().NewerThan(sinceId)
                );
        }

        public IEnumerable<YammerMessage> ListMessagesBefore(long afterId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().OlderThan(afterId)
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessages()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().Threaded()
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesSince(long sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().NewerThan(sinceId).Threaded()
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesBefore(long afterId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().All().OlderThan(afterId).Threaded()
                );
        }

        #endregion

        #region Sent

        public IEnumerable<YammerMessage> ListMessagesSent()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Sent()
                );
        }

        public IEnumerable<YammerMessage> ListMessagesSentSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Sent().NewerThan(sinceId)
                );
        }

        public IEnumerable<YammerMessage> ListMessagesSentBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Sent().OlderThan(beforeId)
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesSent()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q => q.Messages().Sent().Threaded()
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesSentSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q => q.Messages().Sent().NewerThan(sinceId).Threaded()
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesSentBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q => q.Messages().Sent().OlderThan(beforeId).Threaded()
                );
        }

        #endregion

        #region Received

        public IEnumerable<YammerMessage> ListMessagesReceived()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received()
                );
        }

        public IEnumerable<YammerMessage> ListMessagesReceivedSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received().NewerThan(sinceId)
                );
        }

        public IEnumerable<YammerMessage> ListMessagesReceivedBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received().OlderThan(beforeId)
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesReceived()
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(q =>
                                                              q.Messages().Received().Threaded()
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesReceivedSince(int sinceId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q =>
                                                                 q.Messages().Received().NewerThan(sinceId).Threaded()
                );
        }

        public IEnumerable<YammerMessage> ListThreadedMessagesReceivedBefore(int beforeId)
        {
            return WithTweetSharp<IEnumerable<YammerMessage>>(
                                                                 q =>
                                                                 q.Messages().Received().OlderThan(beforeId).Threaded()
                );
        }

        #endregion

        #endregion
    }
}