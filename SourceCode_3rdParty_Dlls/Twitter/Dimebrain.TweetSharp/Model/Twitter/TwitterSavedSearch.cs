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

namespace TweetSharp.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{Name}:'{Query}'")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterSavedSearch : PropertyChangedBase,
                                      IComparable<TwitterSavedSearch>,
                                      IEquatable<TwitterSavedSearch>,
                                      ITwitterEntity
    {
        private long _id;
        private string _name;
        private string _query;
        private string _position;
        private DateTime _createdDate;

        [JsonProperty("id")]
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

        [JsonProperty("query")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Query
        {
            get { return _query; }
            set
            {
                if (_query == value)
                {
                    return;
                }

                _query = value;
                OnPropertyChanged("Query");
            }
        }

        [JsonProperty("position")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Position
        {
            get { return _position; }
            set
            {
                if (_position == value)
                {
                    return;
                }

                _position = value;
                OnPropertyChanged("Position");
            }
        }

        [JsonProperty("created_at")]
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

        public int CompareTo(TwitterSavedSearch other)
        {
            return other.Id == Id ? 0 : other.Id > Id ? -1 : 1;
        }

        public bool Equals(TwitterSavedSearch savedSearch)
        {
            if (ReferenceEquals(null, savedSearch))
            {
                return false;
            }
            if (ReferenceEquals(this, savedSearch))
            {
                return true;
            }
            return savedSearch.Id == Id;
        }

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
            return status.GetType() == typeof (TwitterSavedSearch) &&
                   Equals((TwitterSavedSearch) status);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TwitterSavedSearch left, TwitterSavedSearch right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TwitterSavedSearch left, TwitterSavedSearch right)
        {
            return !Equals(left, right);
        }
    }
}