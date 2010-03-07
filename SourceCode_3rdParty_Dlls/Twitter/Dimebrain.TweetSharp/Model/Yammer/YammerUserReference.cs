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
    /// Represents a yammer user (summary details)
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerUserReference : YammerObjectBase, IYammerModel
    {
        private string _fullName;
        private string _jobTitle;
        private string _mugshotUrl;
        private string _name;
        private YammerUserStats _userStats;

        /// <summary>
        /// the screen name of the user
        /// </summary>
        [JsonProperty("name")]
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

        /// <summary>
        /// the full name of the user
        /// </summary>
        [JsonProperty("full_name")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName == value)
                {
                    return;
                }
                _fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        /// <summary>
        /// url to the user's photo
        /// </summary>
        [JsonProperty("mugshot_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string MugshotUrl
        {
            get { return _mugshotUrl; }
            set
            {
                if (_mugshotUrl == value)
                {
                    return;
                }
                _mugshotUrl = value;
                OnPropertyChanged("MugshotUrl");
            }
        }

        /// <summary>
        /// The user's job title
        /// </summary>
        [JsonProperty("job_title")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string JobTitle
        {
            get { return _jobTitle; }
            set
            {
                if (_jobTitle == value)
                {
                    return;
                }
                _jobTitle = value;
                OnPropertyChanged("JobTitle");
            }
        }

        /// <summary>
        /// Counters for the user (updates, following, followers...)
        /// </summary>
        [JsonProperty("stats")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerUserStats UserStats
        {
            get { return _userStats; }
            set
            {
                if (_userStats == value)
                {
                    return;
                }
                _userStats = value;
                OnPropertyChanged("Stats");
            }
        }
    }
}