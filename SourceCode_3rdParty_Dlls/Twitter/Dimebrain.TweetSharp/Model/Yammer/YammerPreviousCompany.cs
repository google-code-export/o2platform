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
    public class YammerPreviousCompany : PropertyChangedBase, IYammerModel
    {
        private string _description;
        private string _employer;
        private int? _endYear;
        private string _position;
        private int? _startYear;

        [JsonProperty("employer")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Employer
        {
            get { return _employer; }
            set
            {
                if (_employer == value)
                {
                    return;
                }
                _employer = value;
                OnPropertyChanged("Employer");
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

        [JsonProperty("start_year")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int? StartYear
        {
            get { return _startYear; }
            set
            {
                if (_startYear == value)
                {
                    return;
                }
                _startYear = value;
                OnPropertyChanged("StartYear");
            }
        }

        [JsonProperty("end_year")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int? EndYear
        {
            get { return _endYear; }
            set
            {
                if (_endYear == value)
                {
                    return;
                }
                _endYear = value;
                OnPropertyChanged("EndYear");
            }
        }
    }
}