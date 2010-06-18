using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using TheWorldsWorst.ApiWrapper.Helpers;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TheWorldsWorst.ApiWrapper.Model
{
    [DataContract]
    public class Reputation : HasWrapperClass
    {
        [DataContract]
        class ReputationWrapper
        {
            [DataMember]
            IEnumerable<Reputation> rep_changes;
        }

        internal override Type WrapperClass
        {
            get { return typeof(ReputationWrapper); }
        }

        [DataMember(Name="post_id")]
        public int PostId { get; internal set; }

        [DataMember(Name = "post_type")]
        internal string _PostType { get; set; }
        public PostType PostType { get { return (PostType)Enum.Parse(typeof(PostType), _PostType, true); } }

        [DataMember(Name = "title")]
        public string Title { get; internal set; }
        [DataMember(Name = "positive_rep")]
        public int Gain { get; internal set; }
        [DataMember(Name = "negative_rep")]
        public int Loss { get; internal set; }

        [DataMember(Name = "on_date")]
        internal long _OnDate { get; set; }
        public DateTime OnDate { get { return _OnDate.FromUnixTime(); } }

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }
    }
}
