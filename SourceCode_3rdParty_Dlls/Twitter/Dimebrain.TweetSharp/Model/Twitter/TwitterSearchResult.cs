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
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
#if !Smartphone
    [DataContract]
    [DebuggerDisplay("{ResultsPerPage} results on page {Page} from {Source}")]
#endif
    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterSearchResult : ITwitterModel
    {
        [JsonProperty("results")]
#if !Smartphone
        [DataMember]
#endif
        public virtual List<TwitterSearchStatus> Statuses { get; set; }

        [JsonProperty("since_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long SinceId { get; set; }

        [JsonProperty("max_id")]
#if !Smartphone
        [DataMember]
#endif
        public virtual long MaxId { get; set; }

        [JsonProperty("refresh_url")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string RefreshUrl { get; set; }

        [JsonProperty("results_per_page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int ResultsPerPage { get; set; }

        [JsonProperty("next_page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string NextPage { get; set; }

        [JsonProperty("previous_page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string PreviousPage { get; set; }

        [JsonProperty("completed_in")]
#if !Smartphone
        [DataMember]
#endif
        public virtual double CompletedIn { get; set; }

        [JsonProperty("page")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Page { get; set; }

        [JsonProperty("query")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Query { get; set; }

        [JsonProperty("warning")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Warning { get; set; }

        [JsonProperty("source")]
#if !Smartphone
        [DataMember]
#endif
        public virtual string Source { get; set; }

        [JsonProperty("total")]
#if !Smartphone
        [DataMember]
#endif
        public virtual int Total { get; set; }
    }
}