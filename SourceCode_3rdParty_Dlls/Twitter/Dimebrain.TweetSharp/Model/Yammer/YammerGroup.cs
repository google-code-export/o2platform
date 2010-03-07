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
    public enum YammerGroupPrivacy
    {
        Public,
        Private
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerGroup : YammerObjectBase, IYammerModel
    {
        private string _fullName;
        private string _name;
        private YammerGroupPrivacy _privacy;
        private YammerGroupStats _stats;

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

        [JsonProperty("privacy")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerGroupPrivacy Privacy
        {
            get { return _privacy; }
            set
            {
                if (_privacy == value)
                {
                    return;
                }
                _privacy = value;
                OnPropertyChanged("Privacy");
            }
        }

        [JsonProperty("stats")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerGroupStats Stats
        {
            get { return _stats; }
            set
            {
                if (_stats == value)
                {
                    return;
                }
                _stats = value;
                OnPropertyChanged("Stats");
            }
        }
    }
}