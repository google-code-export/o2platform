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
    /// <summary>
    /// 
    /// </summary>
    public static class IYammerMessagesExtensions
    {
        /// <summary>
        /// Gets up to 20 messages from all messages in the network 
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerAllMessages All(this IYammerMessages instance)
        {
            instance.Root.Parameters.Action = "";
            return new YammerAllMessages(instance.Root);
        }

        /// <summary>
        /// Gets up to 20 messages sent by the current user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerSentMessages Sent(this IYammerMessages instance)
        {
            instance.Root.Parameters.Action = "sent";
            return new YammerSentMessages(instance.Root);
        }

        /// <summary>
        /// Gets up to 20 received messages for the logged in user 
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerReceivedMessages Received(this IYammerMessages instance)
        {
            instance.Root.Parameters.Action = "received";
            return new YammerReceivedMessages(instance.Root);
        }

        /// <summary>
        /// Gets up to 20 messages posted by users that the current user is following 
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerFollowingMessages Following(this IYammerMessages instance)
        {
            instance.Root.Parameters.Action = "following";
            return new YammerFollowingMessages(instance.Root);
        }

        /// <summary>
        /// Gets up to 20 messages sent by the specified user
        /// </summary>
        /// <param name="user">The id of the user whose messages to get</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerMessagesFromUser FromUser(this IYammerMessages instance, long user)
        {
            instance.Root.Parameters.Action = "from_user";
            instance.Root.Parameters.Id = user;
            return new YammerMessagesFromUser(instance.Root);
        }


        /// <summary>
        /// Gets up to 20 messages sent by the specified bot
        /// </summary>
        /// <param name="botId">The id of the bot whose messages to get</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerMessagesFromBot FromBot(this IYammerMessages instance, long botId)
        {
            instance.Root.Parameters.Action = "from_bot";
            instance.Root.Parameters.BotID = botId;
            return new YammerMessagesFromBot(instance.Root);
        }

        /// <summary>
        /// Gets up to 20 messages containing the specified tag
        /// </summary>
        /// <param name="tagId">The id of the tag</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerMessagesWithTag WithTag(this IYammerMessages instance, long tagId)
        {
            instance.Root.Parameters.Action = "tagged_with";
            instance.Root.Parameters.Id = tagId;
            return new YammerMessagesWithTag(instance.Root);
        }


        /// <summary>
        /// Gets up to 20 messages posted to the specified group 
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="groupId">The group id.</param>
        /// <returns></returns>
        public static IYammerMessagesInGroup InGroup(this IYammerMessages instance, long groupId)
        {
            instance.Root.Parameters.Action = "in_group";
            instance.Root.Parameters.Id = groupId;
            return new YammerMessagesInGroup(instance.Root);
        }

        /// <summary>
        /// Gets the favorite messages of another user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="userId">The id of the user whose favorites to get</param>
        /// <returns></returns>
        [Obsolete("Appears to always return HTTP 500 when used.  Use is not recommended.")]
        public static IYammerFavoriteMessages FavoritesOf(this IYammerMessages instance, long userId)
        {
            instance.Root.Parameters.Action = "favorites_of";
            instance.Root.Parameters.Id = userId;
            return new YammerFavoriteMessages(instance.Root);
        }

        /// <summary>
        /// Gets up to 20 messages in the specified thread
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="threadId">The thread id.</param>
        /// <returns></returns>
        public static IYammerMessagesInThread InThread(this IYammerMessages instance, long threadId)
        {
            instance.Root.Parameters.Action = "in_thread";
            instance.Root.Parameters.Id = threadId;
            return new YammerMessagesInThread(instance.Root);
        }


        /// <summary>
        /// Gets the favorite messages of the current user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static IYammerFavoriteMessages Favorites(this IYammerMessages instance)
        {
            instance.Root.Parameters.Action = "favorites_of";
            instance.Root.Parameters.UseCurrentAsUserId = true;
            return new YammerFavoriteMessages(instance.Root);
        }


        /// <summary>
        /// Posts a new message from the current user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="messageText">The message text.</param>
        /// <returns></returns>
        public static IYammerMessagePost Post(this IYammerMessages instance, string messageText)
        {
            instance.Root.Parameters.Body = messageText;
            return new YammerMessagePost(instance.Root);
        }


        /// <summary>
        /// Deletes the specified message from the current user
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="messageID">The message ID.</param>
        /// <returns></returns>
        public static IYammerMessageDelete Delete(this IYammerMessages instance, long messageID)
        {
            instance.Root.Parameters.Id = messageID;
            return new YammerMessageDelete(instance.Root);
        }
    }
}