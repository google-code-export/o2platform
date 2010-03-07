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
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterFriend : PropertyChangedBase,
                                 ITwitterModel
    {
        private long _id;
        private string _screenName;
        private bool _following;
        private bool _followedBy;
        private bool? _notificationsEnabled;

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("id", Required = Required.Always)]
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

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("screen_name", Required = Required.AllowNull)]
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

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("following", Required = Required.Always)]
        public virtual bool Following
        {
            get { return _following; }
            set
            {
                if (_following == value)
                {
                    return;
                }

                _following = value;
                OnPropertyChanged("Following");
            }
        }

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("followed_by", Required = Required.Always)]
        public virtual bool FollowedBy
        {
            get { return _followedBy; }
            set
            {
                if (_followedBy == value)
                {
                    return;
                }

                _followedBy = value;
                OnPropertyChanged("FollowedBy");
            }
        }

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("notifications_enabled")]
        public virtual bool? NotificationsEnabled
        {
            get { return _notificationsEnabled; }
            set
            {
                if (_notificationsEnabled == value)
                {
                    return;
                }

                _notificationsEnabled = value;
                OnPropertyChanged("NotificationsEnabled");
            }
        }
    }
}