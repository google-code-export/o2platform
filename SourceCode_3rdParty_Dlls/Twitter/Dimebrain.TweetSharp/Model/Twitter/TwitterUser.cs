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
using System.Diagnostics;
using System.Runtime.Serialization;
using TweetSharp.Model.Twitter.Converters;
using Newtonsoft.Json;
#if !Smartphone
#endif

namespace TweetSharp.Model
{
    /// <summary>
    /// This data class provides more information than the basic data provided by
    /// <see cref="TwitterUser" /> returned in other calls for friends and followers.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{ScreenName}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterUser : PropertyChangedBase,
                               IComparable<TwitterUser>,
                               IEquatable<TwitterUser>,
                               ITwitterModel
    {
        private string _description;
        private int _followersCount;
        private int _id;
        private bool? _isProtected;
        private string _location;
        private string _name;
        private string _profileImageUrl;
        private string _screenName;
        private TwitterStatus _status;
        private string _url;
        private DateTime _createdDate;
        private int _favouritesCount;
        private int _friendsCount;
        private bool? _hasNotifications;
        private bool? _isFollowing;
        private bool? _isVerified;
        private bool? _isGeoEnabled;
        private bool _isProfileBackgroundTiled;
        private string _profileBackgroundColor;
        private string _profileBackgroundImageUrl;
        private string _profileLinkColor;
        private string _profileSidebarBorderColor;
        private string _profileSidebarFillColor;
        private string _profileTextColor;
        private int _statusesCount;
        private string _timeZone;
        private string _utcOffset;
        private string _language;

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("id", Required = Required.Always)]
        public virtual int Id
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

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("followers_count")]
        public virtual int FollowersCount
        {
            get { return _followersCount; }
            set
            {
                if (_followersCount == value)
                {
                    return;
                }

                _followersCount = value;
                OnPropertyChanged("FollowersCount");
            }
        }

        [JsonProperty("name", Required = Required.Always)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        [JsonProperty("description")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
                OnPropertyChanged("Description");
            }
        }

        [JsonProperty("profile_image_url", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileImageUrl
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

        [JsonProperty("url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Url
        {
            get { return _url; }
            set
            {
                if (_url == value)
                {
                    return;
                }

                _url = value;
                OnPropertyChanged("Url");
            }
        }

        [JsonProperty("protected", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool? IsProtected
        {
            get { return _isProtected; }
            set
            {
                if (_isProtected == value)
                {
                    return;
                }

                _isProtected = value;
                OnPropertyChanged("IsProtected");
            }
        }

        [JsonProperty("screen_name", Required = Required.AllowNull)]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ScreenName
        {
            get { return _screenName; }
            set
            {
                if (_screenName == value)
                {
                    return;
                }

                _screenName = value;
                OnPropertyChanged("ScreenName");
            }
        }

        [JsonProperty("location")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Location
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

        [JsonProperty("status")]
#if !Smartphone
        [DataMember]
#endif
        public virtual TwitterStatus Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                {
                    return;
                }

                _status = value;
                OnPropertyChanged("Status");
            }
        }


        [JsonProperty("friends_count")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int FriendsCount
        {
            get { return _friendsCount; }
            set
            {
                if (_friendsCount == value)
                {
                    return;
                }

                _friendsCount = value;
                OnPropertyChanged("FriendsCount");
            }
        }

        [JsonProperty("profile_background_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileBackgroundColor
        {
            get { return _profileBackgroundColor; }
            set
            {
                if (_profileBackgroundColor == value)
                {
                    return;
                }

                _profileBackgroundColor = value;
                OnPropertyChanged("ProfileBackgroundColor");
            }
        }

        [JsonProperty("utc_offset")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string UtcOffset
        {
            get { return _utcOffset; }
            set
            {
                if (_utcOffset == value)
                {
                    return;
                }

                _utcOffset = value;
                OnPropertyChanged("UtcOffset");
            }
        }

        [JsonProperty("profile_text_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileTextColor
        {
            get { return _profileTextColor; }
            set
            {
                if (_profileTextColor == value)
                {
                    return;
                }

                _profileTextColor = value;
                OnPropertyChanged("ProfileTextColor");
            }
        }

        [JsonProperty("profile_background_image_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileBackgroundImageUrl
        {
            get { return _profileBackgroundImageUrl; }
            set
            {
                if (_profileBackgroundImageUrl == value)
                {
                    return;
                }

                _profileBackgroundImageUrl = value;
                OnPropertyChanged("ProfileBackgroundImageUrl");
            }
        }

        [JsonProperty("time_zone")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string TimeZone
        {
            get { return _timeZone; }
            set
            {
                if (_timeZone == value)
                {
                    return;
                }

                _timeZone = value;
                OnPropertyChanged("TimeZone");
            }
        }

        // note: spelling is UK English here but not elsewhere
#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("favourites_count")]
        public virtual int FavouritesCount
        {
            get { return _favouritesCount; }
            set
            {
                if (_favouritesCount == value)
                {
                    return;
                }

                _favouritesCount = value;
                OnPropertyChanged("FavouritesCount");
            }
        }

        [JsonProperty("profile_link_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileLinkColor
        {
            get { return _profileLinkColor; }
            set
            {
                if (_profileLinkColor == value)
                {
                    return;
                }

                _profileLinkColor = value;
                OnPropertyChanged("ProfileLinkColor");
            }
        }

        [JsonProperty("statuses_count")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int StatusesCount
        {
            get { return _statusesCount; }
            set
            {
                if (_statusesCount == value)
                {
                    return;
                }

                _statusesCount = value;
                OnPropertyChanged("StatusesCount");
            }
        }

        [JsonProperty("profile_sidebar_fill_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileSidebarFillColor
        {
            get { return _profileSidebarFillColor; }
            set
            {
                if (_profileSidebarFillColor == value)
                {
                    return;
                }

                _profileSidebarFillColor = value;
                OnPropertyChanged("ProfileSidebarFillColor");
            }
        }

        [JsonProperty("profile_sidebar_border_color")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ProfileSidebarBorderColor
        {
            get { return _profileSidebarBorderColor; }
            set
            {
                if (_profileSidebarBorderColor == value)
                {
                    return;
                }

                _profileSidebarBorderColor = value;
                OnPropertyChanged("ProfileSidebarBorderColor");
            }
        }

        [JsonProperty("profile_background_tile")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool IsProfileBackgroundTiled
        {
            get { return _isProfileBackgroundTiled; }
            set
            {
                if (_isProfileBackgroundTiled == value)
                {
                    return;
                }

                _isProfileBackgroundTiled = value;
                OnPropertyChanged("IsProfileBackgroundTiled");
            }
        }

        // http://twitter.com/account/verify_credentials
        [JsonProperty("notifications")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("Twitter will obsolete this element and it is unreliable- http://is.gd/7LRZs")]
        public virtual bool? HasNotifications
        {
            get { return _hasNotifications; }
            set
            {
                if (_hasNotifications == value)
                {
                    return;
                }

                _hasNotifications = value;
                OnPropertyChanged("HasNotifications");
            }
        }

        // http://twitter.com/account/verify_credentials
        [JsonProperty("following")]
#if !Smartphone
        [DataMember]
#endif
        [Obsolete("Twitter will obsolete this element and it is unreliable- http://is.gd/7LRZs")]
        public virtual bool? IsFollowing
        {
            get { return _isFollowing; }
            set
            {
                if (_isFollowing == value)
                {
                    return;
                }

                _isFollowing = value;
                OnPropertyChanged("IsFollowing");
            }
        }

        [JsonProperty("verified")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool? IsVerified
        {
            get { return _isVerified; }
            set
            {
                if (_isVerified == value)
                {
                    return;
                }

                _isVerified = value;
                OnPropertyChanged("IsVerified");
            }
        }

        [JsonProperty("geo_enabled")]
#if !Smartphone
        [DataMember]
#endif
        public virtual bool? IsGeoEnabled
        {
            get { return _isGeoEnabled; }
            set
            {
                if (_isGeoEnabled == value)
                {
                    return;
                }

                _isGeoEnabled = value;
                OnPropertyChanged("IsGeoEnabled");
            }
        }

        [JsonProperty("lang")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Language
        {
            get { return _language; }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
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

        #region IComparable<TwitterUser> Members

        public int CompareTo(TwitterUser user)
        {
            return user.Id == Id ? 0 : user.Id > Id ? -1 : 1;
        }

        #endregion

        #region IEquatable<TwitterUser> Members

        public bool Equals(TwitterUser user)
        {
            if (ReferenceEquals(null, user))
            {
                return false;
            }
            if (ReferenceEquals(this, user))
            {
                return true;
            }
            return user.Id == Id;
        }

        #endregion

        public override bool Equals(object user)
        {
            if (ReferenceEquals(null, user))
            {
                return false;
            }
            if (ReferenceEquals(this, user))
            {
                return true;
            }
            return user.GetType() == typeof (TwitterUser) && Equals((TwitterUser) user);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TwitterUser left, TwitterUser right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterUser left, TwitterUser right)
        {
            return !Equals(left, right);
        }
    }
}