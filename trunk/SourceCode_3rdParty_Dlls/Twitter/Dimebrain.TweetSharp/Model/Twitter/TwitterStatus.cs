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

namespace TweetSharp.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{User.ScreenName}: {Text}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterStatus : PropertyChangedBase,
                                 IComparable<TwitterStatus>,
                                 IEquatable<TwitterStatus>,
                                 ITwitterEntity,
                                 ITweetable
    {
        private DateTime _createdDate;
        private long _id;
        private string _inReplyToScreenName;
        private long? _inReplyToStatusId;
        private int? _inReplyToUserId;
        private bool _isFavorited;
        private bool _isTruncated;
        private string _source;
        private string _text;
        private TwitterUser _user;
        private TwitterStatus _retweetedStatus;
        private GeoLocation? _location;

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

        [JsonProperty("in_reply_to_user_id", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual int? InReplyToUserId
        {
            get { return _inReplyToUserId; }
            set
            {
                if (_inReplyToUserId == value)
                {
                    return;
                }

                _inReplyToUserId = value;
                OnPropertyChanged("InReplyToUserId");
            }
        }

        [JsonProperty("in_reply_to_status_id", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual long? InReplyToStatusId
        {
            get { return _inReplyToStatusId; }
            set
            {
                if (_inReplyToStatusId == value)
                {
                    return;
                }

                _inReplyToStatusId = value;
                OnPropertyChanged("InReplyToStatusId");
            }
        }

        [JsonProperty("in_reply_to_screen_name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string InReplyToScreenName
        {
            get { return _inReplyToScreenName; }
            set
            {
                if (_inReplyToScreenName == value)
                {
                    return;
                }

                _inReplyToScreenName = value;
                OnPropertyChanged("InReplyToScreenName");
            }
        }

        [JsonProperty("truncated")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool IsTruncated
        {
            get { return _isTruncated; }
            set
            {
                if (_isTruncated == value)
                {
                    return;
                }

                _isTruncated = value;
                OnPropertyChanged("IsTruncated");
            }
        }

        [JsonProperty("favorited")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool IsFavorited
        {
            get { return _isFavorited; }
            set
            {
                if (_isFavorited == value)
                {
                    return;
                }

                _isFavorited = value;
                OnPropertyChanged("IsFavorited");
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

        [JsonProperty("source", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Source
        {
            get { return _source; }
            set
            {
                if (_source == value)
                {
                    return;
                }

                _source = value;
                OnPropertyChanged("Source");
            }
        }

        [JsonProperty("user")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterUser User
        {
            get { return _user; }
            set
            {
                if (_user == value)
                {
                    return;
                }

                _user = value;
                OnPropertyChanged("TwitterUser");
            }
        }

        [JsonProperty("retweeted_status")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterStatus RetweetedStatus
        {
            get { return _retweetedStatus; }
            set
            {
                if (_retweetedStatus == value)
                {
                    return;
                }

                _retweetedStatus = value;
                OnPropertyChanged("RetweetedStatus");
            }
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

        [JsonProperty("geo")]
        [JsonConverter(typeof (TwitterGeoLocationConverter))]
#if !Smartphone
        [DataMember]
#endif
        public virtual GeoLocation? Location
        {
            get { return _location; }
            set
            {
                if (_location == value)
                {
                    return;
                }

                _location = value;
                OnPropertyChanged("Location");
            }
        }

        #region IComparable<TwitterStatus> Members

        public int CompareTo(TwitterStatus other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterStatus> Members

        public bool Equals(TwitterStatus status)
        {
            if (ReferenceEquals(null, status))
            {
                return false;
            }
            if (ReferenceEquals(this, status))
            {
                return true;
            }
            return status.Id == Id;
        }

        #endregion

        public override bool Equals(object status)
        {
            if (ReferenceEquals(null, status))
            {
                return false;
            }
            if (ReferenceEquals(this, status))
            {
                return true;
            }
            return status.GetType() == typeof (TwitterStatus) && Equals((TwitterStatus) status);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TwitterStatus left, TwitterStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TwitterStatus left, TwitterStatus right)
        {
            return !Equals(left, right);
        }
    }
}