using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Represents a tag
    /// </summary>
    [DataContract]
    public class Tag : HasWrapperClass
    {
        [DataContract]
        class TagWrapper
        {
            [DataMember]
            IEnumerable<Tag> tags;
        }

        internal override Type WrapperClass
        {
            get { return typeof(TagWrapper); }
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; internal set; }
        [DataMember(Name = "user_id")]
        public int? UserId { get; internal set; }
    }
}
