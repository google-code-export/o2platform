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
    public class YammerMessage : YammerMessageReference
    {
        private List<YammerAttachment> _attachments;
        private long _groupId;
        private long _threadId;
        private string _yammerClientType;

        /// <summary>
        /// The id of the tread in which this message appears
        /// </summary>
        [JsonProperty("thread_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long ThreadId
        {
            get { return _threadId; }
            set
            {
                if (_threadId == value)
                {
                    return;
                }
                _threadId = value;
                OnPropertyChanged("ThreadId");
            }
        }

        /// <summary>
        /// the (optional) id of the group the message was posted to (if posted to a group)
        /// </summary>
        [JsonProperty("group_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long GroupId
        {
            get { return _groupId; }
            set
            {
                if (_groupId == value)
                {
                    return;
                }
                _groupId = value;
                OnPropertyChanged("GroupId");
            }
        }

        [JsonProperty("attachments")]
#if !Smartphone
        [DataMember]
#endif
        public virtual List<YammerAttachment> Attachments
        {
            get { return _attachments; }
            set
            {
                if (_attachments == value)
                {
                    return;
                }
                _attachments = value;
                OnPropertyChanged("Attachments");
            }
        }

        [JsonProperty("client_type")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string ClientType
        {
            get { return _yammerClientType; }
            set
            {
                if (_yammerClientType == value)
                {
                    return;
                }
                _yammerClientType = value;
                OnPropertyChanged("ClientType");
            }
        }
    }
}