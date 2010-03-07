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
using TweetSharp.Model.Yammer.Converters;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
    public abstract class YammerObjectBase : PropertyChangedBase
    {
        private DateTime? _createdAt;
        private long _id;
        private string _url;
        private string _webUrl;

        /// <summary>
        /// the id of the object
        /// </summary>
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

        /// <summary>
        /// The API resource for fetching this object.
        /// </summary>
        [JsonProperty("url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Url
        {
            get { return _url; }
            set
            {
                if (_url == value)
                {
                    return;
                }
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// The URL for viewing this object on the main Yammer website.
        /// </summary>
        [JsonProperty("web_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string WebUrl
        {
            get { return _webUrl; }
            set
            {
                if (_webUrl == value)
                {
                    return;
                }
                _webUrl = value;
                OnPropertyChanged("WebUrl");
            }
        }

        /// <summary>
        /// The time and date this message was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof (YammerDateConverter))]
#if !Smartphone
        [DataMember]
#endif
        public virtual DateTime? CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (_createdAt == value)
                {
                    return;
                }
                _createdAt = value;
                OnPropertyChanged("CreatedAt");
            }
        }
    }
}