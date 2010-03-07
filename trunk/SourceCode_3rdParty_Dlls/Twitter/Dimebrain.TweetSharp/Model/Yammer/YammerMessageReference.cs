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
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
    /// <summary>
    /// Summary reference to a Yammer message
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerMessageReference : YammerObjectBase, IYammerModel
    {
        private YammerMessageBody _body;
        private long? _directToId;
        private YammerMessageType _messageType;
        private long? _repliedToId;
        private long _senderId;
        private YammerSenderType _senderType;

        /// <summary>
        /// When a message is a private 1-to-1 (or "direct") message, this will indicate the intended recipient.
        /// </summary>
        [JsonProperty("direct_to_id")]
        //[JsonConverter(typeof(YammerNullableTypeConverter<long?>))]
#if !Smartphone
        [DataMember]
#endif
        public virtual long? DirectToId
        {
            get { return _directToId; }
            set
            {
                if (_directToId == value)
                {
                    return;
                }
                _directToId = value;
                OnPropertyChanged("DirectToId");
            }
        }
        
        /// <summary>
        /// The ID of the message this message is in reply to, if applicable.
        /// </summary>
        [JsonProperty("replied_to_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long? RepliedToId
        {
            get { return _repliedToId; }
            set
            {
                if (_repliedToId == value)
                {
                    return;
                }
                _repliedToId = value;
                OnPropertyChanged("RepliedToId");
            }
        }

        /// <summary>
        /// the body of the message
        /// </summary>
        [JsonProperty("body")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerMessageBody Body
        {
            get { return _body; }
            set
            {
                if (_body == value)
                {
                    return;
                }
                _body = value;
                OnPropertyChanged("Body");
            }
        }

        /// <summary>
        /// The ID of the message's sender
        /// </summary>
        [JsonProperty("sender_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long SenderId
        {
            get { return _senderId; }
            set
            {
                if (_senderId == value)
                {
                    return;
                }
                _senderId = value;
                OnPropertyChanged("SenderId");
            }
        }

        /// <summary>
        /// One of the supported message types
        /// </summary>
        [JsonProperty("type")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerMessageType MessageType
        {
            get { return _messageType; }
            set
            {
                if (_messageType == value)
                {
                    return;
                }
                _messageType = value;
                OnPropertyChanged("MessageType");
            }
        }

        /// <summary>
        /// the type of the sender (user,bot,guide...)
        /// </summary>
        [JsonProperty("sender_type")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerSenderType SenderType
        {
            get { return _senderType; }
            set
            {
                if (_senderType == value)
                {
                    return;
                }
                _senderType = value;
                OnPropertyChanged("SenderType");
            }
        }
    }
}