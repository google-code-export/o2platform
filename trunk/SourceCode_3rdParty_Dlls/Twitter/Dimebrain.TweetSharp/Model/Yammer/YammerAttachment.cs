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
    /// Class representing an attachment to a yammer message
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class YammerAttachment : PropertyChangedBase, IYammerModel
    {
        private YammerAttachmentType _attachmentType;
        private long _id;
        private string _name;
        private string _webUrl;

        /// <summary>
        /// Gets or sets the type of attachment
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
#if !Smartphone
        [DataMember]
#endif
        public virtual YammerAttachmentType AttachmentType
        {
            get { return _attachmentType; }
            set
            {
                if (_attachmentType == value)
                {
                    return;
                }
                _attachmentType = value;
                OnPropertyChanged("AttachmentType");
            }
        }


        /// <summary>
        /// Gets or sets the id of the attachment.
        /// </summary>
        /// <value>The id.</value>
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
        /// Gets or sets the name of the attachment
        /// </summary>
        /// <value>The name of the attachment.</value>
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

        /// <summary>
        /// Gets or sets the web URL for the attachment
        /// </summary>
        /// <value>The web URL.</value>
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
    }
}