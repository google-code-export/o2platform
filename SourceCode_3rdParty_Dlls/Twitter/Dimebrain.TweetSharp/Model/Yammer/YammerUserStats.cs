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
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerUserStats : PropertyChangedBase, IYammerModel
    {
        private int _followers;
        private int _following;
        private int _updates;

        /// <summary>
        /// the number of status updates posted by this user
        /// </summary>
        [JsonProperty("updates")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Updates
        {
            get { return _updates; }
            set
            {
                if (_updates == value)
                {
                    return;
                }
                _updates = value;
                OnPropertyChanged("Updates");
            }
        }

        /// <summary>
        /// the number of followers this user has
        /// </summary>
        [JsonProperty("followers")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Followers
        {
            get { return _followers; }
            set
            {
                if (_followers == value)
                {
                    return;
                }
                _followers = value;
                OnPropertyChanged("Followers");
            }
        }

        /// <summary>
        /// the number of other users this user is following
        /// </summary>
        [JsonProperty("following")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Following
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
    }
}