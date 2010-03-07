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
    /// Details about a school attended by a Yammer user. 
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerSchool : PropertyChangedBase, IYammerModel
    {
        private string _degree;
        private string _description;
        private int? _endYear;
        private string _school;
        private int? _startYear;

        /// <summary>
        /// the name of the school
        /// </summary>
        [JsonProperty("school")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string School
        {
            get { return _school; }
            set
            {
                if (_school == value)
                {
                    return;
                }
                _school = value;
                OnPropertyChanged("School");
            }
        }

        /// <summary>
        /// Degree attained at this school
        /// </summary>
        [JsonProperty("degree")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Degree
        {
            get { return _degree; }
            set
            {
                if (_degree == value)
                {
                    return;
                }
                _degree = value;
                OnPropertyChanged("Degree");
            }
        }

        /// <summary>
        /// Description of the program attended
        /// </summary>
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

        /// <summary>
        /// The first year the user attended this school
        /// </summary>
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

        /// <summary>
        /// The last year the user attended this school
        /// </summary>
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