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
    /// Represent's a user's email address
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerEmailAddress : PropertyChangedBase, IYammerModel
    {
        private string _address;
        private string _emailType;

        /// <summary>
        /// The type of address (primary, personal, etc...)
        /// </summary>
        [JsonProperty("type")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string EmailType
        {
            get { return _emailType; }
            set
            {
                if (_emailType == value)
                {
                    return;
                }
                _emailType = value;
                OnPropertyChanged("EmailType");
            }
        }

        /// <summary>
        /// The email address
        /// </summary>
        [JsonProperty("address")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Address
        {
            get { return _address; }
            set
            {
                if (_address == value)
                {
                    return;
                }
                _address = value;
                OnPropertyChanged("Address");
            }
        }
    }
}