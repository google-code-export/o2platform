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

using TweetSharp.Model;

namespace TweetSharp.Fluent
{
    public static partial class Extensions
    {
        public static IYammerMessagePost InReplyTo(this IYammerMessagePost instance, YammerMessage message)
        {
            return InReplyTo(instance, message.Id);
        }

        public static IYammerMessagePost ToGroup(this IYammerMessagePost instance, YammerGroup group)
        {
            return InReplyTo(instance, group.Id);
        }

        public static IYammerMessagePost DirectToUser(this IYammerMessagePost instance, YammerUser user)
        {
            return InReplyTo(instance, user.Id);
        }

        public static IYammerMessagePost InReplyTo(this IYammerMessagePost instance, long messageId)
        {
            instance.Root.Parameters.InReplyTo = messageId;
            return instance;
        }

        public static IYammerMessagePost ToGroup(this IYammerMessagePost instance, long groupId)
        {
            instance.Root.Parameters.ToGroupID = groupId;
            return instance;
        }

        public static IYammerMessagePost DirectToUser(this IYammerMessagePost instance, long userId)
        {
            instance.Root.Parameters.DirectToUser = userId;
            return instance;
        }

        public static IYammerMessagePost WithAttachment(this IYammerMessagePost instance, string attachmentPath)
        {
            instance.Root.Parameters.Attachments.Add(attachmentPath);
            return instance;
        }
    }
}