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
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerAutoCompleteSuggestions : YammerObjectBase, IYammerModel
    {
        private IEnumerable<YammerGroup> _groups = new List<YammerGroup>();
        private IEnumerable<YammerTagReference> _tags = new List<YammerTagReference>();
        private IEnumerable<YammerUserReference> _users = new List<YammerUserReference>();

        [JsonProperty("users")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerUserReference> Users
        {
            get { return _users; }
            set
            {
                if (_users == value)
                {
                    return;
                }
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        [JsonProperty("Tags")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerTagReference> Tags
        {
            get { return _tags; }
            set
            {
                if (_tags == value)
                {
                    return;
                }
                _tags = value;
                OnPropertyChanged("Tags");
            }
        }

        [JsonProperty("Groups")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IEnumerable<YammerGroup> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups == value)
                {
                    return;
                }
                _groups = value;
                OnPropertyChanged("Groups");
            }
        }
    }
}