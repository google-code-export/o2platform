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
using TweetSharp.Model.Yammer.Converters;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
    /// <summary>
    /// Represents a Yammer User (full details)
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerUser : YammerUserReference
    {
        private string _birthDate;
        private YammerContactInfo _contactInfo;
        private string _expertise;
        private IList<string> _externalUrls;
        private DateTime? _hireDate;
        private string _interests;
        private string _kidsNames;
        private string _location;
        private IList<string> _networkDomains;
        private long _networkId;
        private IList<YammerPreviousCompany> _previousCompanies;
        private IList<YammerSchool> _schools;
        private string _significantOther;
        private string _state;
        private string _summary;
        private string _timeZone;

        /// <summary>
        /// The user's location
        /// </summary>
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

        /// <summary>
        /// The user's birth date
        /// </summary>
        [JsonProperty("birth_date")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate == value)
                {
                    return;
                }
                _birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        /// <summary>
        /// The date the user was hired
        /// </summary>
        [JsonProperty("hire_date")]
        [JsonConverter(typeof (YammerDateConverter))]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime? HireDate
        {
            get { return _hireDate; }
            set
            {
                if (_hireDate == value)
                {
                    return;
                }
                _hireDate = value;
                OnPropertyChanged("HireDate");
            }
        }

        /// <summary>
        /// The user's interests
        /// </summary>
        [JsonProperty("interests")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Interests
        {
            get { return _interests; }
            set
            {
                if (_interests == value)
                {
                    return;
                }
                _interests = value;
                OnPropertyChanged("Interests");
            }
        }


        /// <summary>
        /// The user's expertise
        /// </summary>
        [JsonProperty("Expertise")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Expertise
        {
            get { return _expertise; }
            set
            {
                if (_expertise == value)
                {
                    return;
                }
                _expertise = value;
                OnPropertyChanged("Expertise");
            }
        }

        /// <summary>
        /// Summary text from the user's profile
        /// </summary>
        [JsonProperty("summary")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Summary
        {
            get { return _summary; }
            set
            {
                if (_summary == value)
                {
                    return;
                }
                _summary = value;
                OnPropertyChanged("Summary");
            }
        }


        /// <summary>
        /// the current timezone of the user
        /// </summary>
        [JsonProperty("timezone")]
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

        /// <summary>
        /// value indicating the user's active state
        /// </summary>
        [JsonProperty("state")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string State
        {
            get { return _state; }
            set
            {
                if (_state == value)
                {
                    return;
                }
                _state = value;
                OnPropertyChanged("State");
            }
        }

        /// <summary>
        /// the id of the network that this user belongs to
        /// </summary>
        [JsonProperty("network_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long NetworkId
        {
            get { return _networkId; }
            set
            {
                if (_networkId == value)
                {
                    return;
                }
                _networkId = value;
                OnPropertyChanged("NetworkId");
            }
        }

        /// <summary>
        /// List of applicable network domains
        /// </summary>
        [JsonProperty("network_domains")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IList<string> NetworkDomains
        {
            get { return _networkDomains; }
            set
            {
                if (_networkDomains == value)
                {
                    return;
                }
                _networkDomains = value;
                OnPropertyChanged("NetworkDomains");
            }
        }
        
        /// <summary>
        /// list of schools attended by this user
        /// </summary>
        [JsonProperty("schools")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IList<YammerSchool> Schools
        {
            get { return _schools; }
            set
            {
                if (_schools == value)
                {
                    return;
                }
                _schools = value;
                OnPropertyChanged("Schools");
            }
        }
        
        /// <summary>
        /// list of previous companies that employed this user
        /// </summary>
        [JsonProperty("previous_companies")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IList<YammerPreviousCompany> PreviousCompanies
        {
            get { return _previousCompanies; }
            set
            {
                if (_previousCompanies == value)
                {
                    return;
                }
                _previousCompanies = value;
                OnPropertyChanged("PreviousCompanies");
            }
        }

        /// <summary>
        /// List of external web addresses for the user (homepage, blog, etc)
        /// </summary>
        [JsonProperty("external_urls")]
#if !Smartphone
        [DataMember]
#endif
        public virtual IList<string> ExternalUrls
        {
            get { return _externalUrls; }
            set
            {
                if (_externalUrls == value)
                {
                    return;
                }
                _externalUrls = value;
                OnPropertyChanged("ExternalUrls");
            }
        }

        [JsonProperty("contact")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerContactInfo ContactInfo
        {
            get { return _contactInfo; }
            set
            {
                if (_contactInfo == value)
                {
                    return;
                }
                _contactInfo = value;
                OnPropertyChanged("ContactInfo");
            }
        }

        [JsonProperty("kids_names")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string KidsNames
        {
            get { return _kidsNames; }
            set
            {
                if (_kidsNames == value)
                {
                    return;
                }
                _kidsNames = value;
                OnPropertyChanged("PropertyName");
            }
        }

        [JsonProperty("significant_other")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string SignificantOther
        {
            get { return _significantOther; }
            set
            {
                if (_significantOther == value)
                {
                    return;
                }
                _significantOther = value;
                OnPropertyChanged("SignificantOther");
            }
        }
    }
}