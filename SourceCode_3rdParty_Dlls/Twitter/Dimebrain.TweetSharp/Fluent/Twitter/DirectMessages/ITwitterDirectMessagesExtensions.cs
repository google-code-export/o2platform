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

using TweetSharp.Core.Attributes;

namespace TweetSharp.Fluent
{
    public static class ITwitterDirectMessagesExtensions
    {
        [RequiresAuthentication]
        public static ITwitterDirectMessagesReceived Received(this ITwitterDirectMessages instance)
        {
            return new TwitterDirectMessagesReceived(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterDirectMessagesSent Sent(this ITwitterDirectMessages instance)
        {
            instance.Root.Parameters.Action = "sent";
            return new TwitterDirectMessagesSent(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterDirectMessagesNew Send(this ITwitterDirectMessages instance, int id, string text)
        {
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Text = text;
            return new TwitterDirectMessagesNew(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterDirectMessagesNew Send(this ITwitterDirectMessages instance, long id, string text)
        {
            instance.Root.Parameters.Action = "new";
            instance.Root.Parameters.UserId = id;
            instance.Root.Parameters.Text = text;
            return new TwitterDirectMessagesNew(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterDirectMessagesNew Send(this ITwitterDirectMessages instance, string screenName,
                                                     string text)
        {
            instance.Root.Parameters.Action = "new";
            instance.Root.Parameters.UserScreenName = screenName;
            instance.Root.Parameters.Text = text;
            return new TwitterDirectMessagesNew(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterDirectMessagesDestroy Destroy(this ITwitterDirectMessages instance, int id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterDirectMessagesDestroy(instance.Root);
        }

        [RequiresAuthentication]
        public static ITwitterDirectMessagesDestroy Destroy(this ITwitterDirectMessages instance, long id)
        {
            instance.Root.Parameters.Action = "destroy";
            instance.Root.Parameters.Id = id;
            return new TwitterDirectMessagesDestroy(instance.Root);
        }
    }
}