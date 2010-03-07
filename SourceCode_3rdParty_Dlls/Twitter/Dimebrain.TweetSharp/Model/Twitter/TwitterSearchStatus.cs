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
    /// <summary>
    /// This data class represents a <see cref="TwitterStatus" /> originating from a Search API result. 
    /// </summary>
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{FromUserScreenName}: {Text}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterSearchStatus : PropertyChangedBase,
                                       IComparable<TwitterSearchStatus>,
                                       IEquatable<TwitterSearchStatus>,
                                       ITwitterModel,
                                       ITweetable
    {
        private DateTime _createdAt;
        private int _fromUserId;
        private string _fromUserScreenName;
        private long _id;
        private string _isoLanguageCode;
        private string _profileImageUrl;
        private long _sinceId;
        private string _source;
        private string _text;
        private int? _toUserId;
        private string _toUserScreenName;
        private string _location;
        private GeoLocation? _geoLocation;

        /// <summary>
        /// The unique identifier for this status.
        /// </summary>
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

        /// <summary>
        /// The date the status was created.
        /// </summary>
        [JsonProperty("created_at", Required = Required.Always)]
        [JsonConverter(typeof (TwitterDateTimeConverter))]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime CreatedDate
        {
            get { return _createdAt; }
            set
            {
                if (_createdAt == value)
                {
                    return;
                }

                _createdAt = value;
                OnPropertyChanged("CreatedDate");
            }
        }

        /// <summary>
        /// This represents the internal ID for the user who received a status in a search
        /// result. Keep in mind that this ID is currently meaningless to the Twitter API
        /// as it is not the same ID as the user's ID. It will eventually provide the
        /// correct ID for the target user.
        /// </summary>
        [JsonProperty("to_user_id")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("This property is currently erroneous as it contains an internal ID")]
        public virtual int? ToUserId
        {
            get { return _toUserId; }
            set
            {
                if (_toUserId == value)
                {
                    return;
                }

                _toUserId = value;
                OnPropertyChanged("ToUserId");
            }
        }

        /// <summary>
        /// This represents the internal ID for the user who wrote a status in a search
        /// result. Keep in mind that this ID is currently meaningless to the Twitter API
        /// as it is not the same ID as the user's ID. It will eventually provide the
        /// correct ID for the originating user.
        /// </summary>
        [JsonProperty("from_user_id")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("This property is currently erroneous as it contains an internal ID")]
        public int? FromUserId
        {
            get { return _fromUserId; }
            set
            {
                if (_fromUserId == value)
                {
                    return;
                }

                _fromUserId = (int) value;
                OnPropertyChanged("FromUserId");
            }
        }

        [JsonProperty("from_user")]
#if !Smartphone
        [DataMember]
#endif
            public string FromUserScreenName
        {
            get { return _fromUserScreenName; }
            set
            {
                if (_fromUserScreenName == value)
                {
                    return;
                }

                _fromUserScreenName = value;
                OnPropertyChanged("FromUserScreenName");
            }
        }

        [JsonProperty("to_user")]
#if !Smartphone
        [DataMember]
#endif
            public string ToUserScreenName
        {
            get { return _toUserScreenName; }
            set
            {
                if (_toUserScreenName == value)
                {
                    return;
                }

                _toUserScreenName = value;
                OnPropertyChanged("ToUserScreenName");
            }
        }

        [JsonProperty("iso_language_code")]
#if !Smartphone
        [DataMember]
#endif
            public string IsoLanguageCode
        {
            get { return _isoLanguageCode; }
            set
            {
                if (_isoLanguageCode == value)
                {
                    return;
                }

                _isoLanguageCode = value;
                OnPropertyChanged("IsoLanguageCode");
            }
        }

        [JsonProperty("profile_image_url")]
#if !Smartphone
        [DataMember]
#endif
            public string ProfileImageUrl
        {
            get { return _profileImageUrl; }
            set
            {
                if (_profileImageUrl == value)
                {
                    return;
                }

                _profileImageUrl = value;
                OnPropertyChanged("ProfileImageUrl");
            }
        }

        [JsonProperty("since_id")]
#if !Smartphone
        [DataMember]
#endif
            public long SinceId
        {
            get { return _sinceId; }
            set
            {
                if (_sinceId == value)
                {
                    return;
                }

                _sinceId = value;
                OnPropertyChanged("SinceId");
            }
        }

        [JsonProperty("location")]
#if !Smartphone
        [DataMember]
#endif
            public string Location
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

        [JsonProperty("geo")]
        [JsonConverter(typeof (TwitterGeoLocationConverter))]
#if !Smartphone
        [DataMember]
#endif
            public GeoLocation? GeoLocation
        {
            get { return _geoLocation; }
            set
            {
                if (_geoLocation == value)
                {
                    return;
                }

                _geoLocation = value;
                OnPropertyChanged("GeoLocation");
            }
        }

        #region IComparable<TwitterSearchStatus> Members

        public int CompareTo(TwitterSearchStatus other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterSearchStatus> Members

        public bool Equals(TwitterSearchStatus status)
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
            return status.GetType() == typeof (TwitterSearchStatus) && Equals((TwitterSearchStatus) status);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TwitterSearchStatus left, TwitterSearchStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TwitterSearchStatus left, TwitterSearchStatus right)
        {
            return !Equals(left, right);
        }

        ///<summary>
        /// This implicit conversion supports treating a search status as if it were a 
        /// regular <see cref="TwitterStatus" />. This is useful if you need to combine search
        /// results and regular results in the same UI context.
        ///</summary>
        ///<param name="searchStatus"></param>
        ///<returns></returns>
        public static implicit operator TwitterStatus(TwitterSearchStatus searchStatus)
        {
            var user = new TwitterUser
                           {
                               ProfileImageUrl = searchStatus.ProfileImageUrl,
                               ScreenName = searchStatus.FromUserScreenName
                           };

            var status = new TwitterStatus
                             {
                                 CreatedDate = searchStatus.CreatedDate,
                                 Id = searchStatus.Id,
                                 Source = searchStatus.Source,
                                 Text = searchStatus.Text,
                                 User = user
                             };

            return status;
        }
    }
}