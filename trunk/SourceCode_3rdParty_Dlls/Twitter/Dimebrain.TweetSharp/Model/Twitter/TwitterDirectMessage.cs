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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using TweetSharp.Core.Extensions;
using TweetSharp.Model.Twitter.Converters;
using Newtonsoft.Json;
#if !Smartphone
#endif

namespace TweetSharp.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{SenderScreenName} to {RecipientScreenName}:{Text}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterDirectMessage : PropertyChangedBase,
                                        IComparable<TwitterDirectMessage>,
                                        IEquatable<TwitterDirectMessage>,
                                        ITwitterEntity,
                                        ITweetable
    {
        private long _id;
        private long _recipientId;
        private string _recipientScreenName;
        private TwitterUser _recipient;
        private long _senderId;
        private TwitterUser _sender;
        private string _senderScreenName;
        private string _text;
        private DateTime _createdDate;

        [JsonProperty("id", Required = Required.Always)]
#if !Smartphone
        [DataMember]
#endif
        public virtual long Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                OnPropertyChanged("Id");
            }
        }

        [JsonProperty("recipient_id", Required = Required.Always)]
#if !Smartphone
        [DataMember]
#endif
        public virtual long RecipientId
        {
            get { return _recipientId; }
            set
            {
                if (_recipientId == value)
                {
                    return;
                }

                _recipientId = value;
                OnPropertyChanged("RecipientId");
            }
        }

        [JsonProperty("recipient_screen_name", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string RecipientScreenName
        {
            get { return _recipientScreenName; }
            set
            {
                if (_recipientScreenName == value)
                {
                    return;
                }

                _recipientScreenName = value;
                OnPropertyChanged("RecipientScreenName");
            }
        }

        [JsonProperty("recipient")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterUser Recipient
        {
            get { return _recipient; }
            set
            {
                if (_recipient == value)
                {
                    return;
                }

                _recipient = value;
                OnPropertyChanged("Recipient");
            }
        }

        [JsonProperty("sender_id", Required = Required.Always)]
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

        [JsonProperty("sender")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterUser Sender
        {
            get { return _sender; }
            set
            {
                if (_sender == value)
                {
                    return;
                }

                _sender = value;
                OnPropertyChanged("Sender");
            }
        }

        [JsonProperty("sender_screen_name", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string SenderScreenName
        {
            get { return _senderScreenName; }
            set
            {
                if (_senderScreenName == value)
                {
                    return;
                }

                _senderScreenName = value;
                OnPropertyChanged("SenderScreenName");
            }
        }

        [JsonProperty("text", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Calculates the HTML value of the <see cref="Text" />
        /// by parsing URLs, mentions, and hashtags into anchors.
        /// </summary>
        /// <value>The HTML text.</value>
        public virtual string TextHtml
        {
            get { return Text.ParseTwitterageToHtml(); }
        }

        /// <summary>
        /// Returns the URLs embedded in the <see cref="Text" /> value.
        /// </summary>
        /// <value>The <see cref="Uri" /> values embedded in <see cref="Text" />.</value>
        public virtual IEnumerable<Uri> TextLinks
        {
            get { return Text.ParseTwitterageToUris(); }
        }

        /// <summary>
        /// Returns the <see cref="TwitterUser.ScreenName" /> values mentioned in the <see cref="Text" /> value.
        /// </summary>
        /// <value>The <see cref="TwitterUser.ScreenName" /> values mentioned in <see cref="Text" />.</value>
        public virtual IEnumerable<string> TextMentions
        {
            get { return Text.ParseTwitterageToScreenNames(); }
        }

        /// <summary>
        /// Returns the hashtag values used in the <see cref="Text" /> value.
        /// </summary>
        /// <value>The hashtag values used in <see cref="Text" />.</value>
        public virtual IEnumerable<string> TextHashtags
        {
            get { return Text.ParseTwitterageToHashtags(); }
        }

        [JsonProperty("created_at", Required = Required.Always)]
        [JsonConverter(typeof (TwitterDateTimeConverter))]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            set
            {
                if (_createdDate == value)
                {
                    return;
                }

                _createdDate = value;
                OnPropertyChanged("CreatedDate");
            }
        }

        #region IComparable<TwitterDirectMessage> Members

        public int CompareTo(TwitterDirectMessage other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterDirectMessage> Members

        public bool Equals(TwitterDirectMessage obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.Id == Id;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == typeof (TwitterDirectMessage) && Equals((TwitterDirectMessage) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TwitterDirectMessage left, TwitterDirectMessage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TwitterDirectMessage left, TwitterDirectMessage right)
        {
            return !Equals(left, right);
        }
    }
}