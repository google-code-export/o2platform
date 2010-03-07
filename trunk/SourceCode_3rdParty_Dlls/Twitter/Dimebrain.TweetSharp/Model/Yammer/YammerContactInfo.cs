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
    public class YammerContactInfo : PropertyChangedBase, IYammerModel
    {
        private List<YammerEmailAddress> _emailAddresses;
        private YammerImInfo _imInfo;
        private List<YammerPhoneNumber> _phoneNumbers;

        /// <summary>
        /// List of email addresses for this user
        /// </summary>
        [JsonProperty("email_addresses")]
#if !Smartphone
        [DataMember]
#endif
        public virtual List<YammerEmailAddress> EmailAddresses
        {
            get { return _emailAddresses; }
            set
            {
                if (_emailAddresses == value)
                {
                    return;
                }
                _emailAddresses = value;
                OnPropertyChanged("EmailAddresses");
            }
        }

        /// <summary>
        /// List of phone numbers for this user
        /// </summary>
        [JsonProperty("phone_numbers")]
#if !Smartphone
        [DataMember]
#endif
        public virtual List<YammerPhoneNumber> PhoneNumbers
        {
            get { return _phoneNumbers; }
            set
            {
                if (_phoneNumbers == value)
                {
                    return;
                }
                _phoneNumbers = value;
                OnPropertyChanged("PhoneNumbers");
            }
        }

        /// <summary>
        /// Intstant-messaging contact info for this user
        /// </summary>
        [JsonProperty("im")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerImInfo Im
        {
            get { return _imInfo; }
            set
            {
                if (_imInfo == value)
                {
                    return;
                }
                _imInfo = value;
                OnPropertyChanged("Im");
            }
        }
    }
}