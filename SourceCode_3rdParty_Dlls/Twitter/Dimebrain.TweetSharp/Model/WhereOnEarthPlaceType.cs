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
#if !SILVERLIGHT
    /// <summary>
    /// Represents a place type for a <see cref="WhereOnEarthLocation" /> in the Yahoo! WOE specification.
    /// </summary>
    /// <seealso cref="http://developer.yahoo.com/geo/geoplanet/"/>
    [Serializable]
#endif

    [JsonObject(MemberSerialization.OptIn)]
    public class WhereOnEarthPlaceType : PropertyChangedBase
    {
        private int _code;
        private string _name;

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("code")]
        public virtual int Code
        {
            get { return _code; }
            set
            {
                if (_code == value)
                {
                    return;
                }

                _code = value;
                OnPropertyChanged("Code");
            }
        }

#if !Smartphone
        [DataMember]
#endif
        [JsonProperty("name")]
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
    }
}