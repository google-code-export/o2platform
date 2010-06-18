using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;
using TheWorldsWorst.ApiWrapper.Helpers;

namespace TheWorldsWorst.ApiWrapper.Model
{
    [DataContract]
    public class Revision : HasWrapperClass
    {
        [DataContract]
        class RevisionWrapper
        {
            [DataMember]
            IEnumerable<Revision> revisions;
        }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "creation_date")]
        internal long _CreationDate { get; set; }
        public DateTime CreationDate { get { return _CreationDate.FromUnixTime(); } }

        [DataMember(Name = "is_question")]
        public bool IsQuestion { get; set; }

        [DataMember(Name = "is_rollback")]
        public bool IsRollback { get; set; }

        [DataMember(Name = "last_body")]
        public string LastBody { get; set; }

        [DataMember(Name = "last_title")]
        public string LastTitle { get; set; }

        [DataMember(Name = "last_tags")]
        public IEnumerable<string> LastTags { get; internal set; }

        [DataMember(Name = "tags")]
        public IEnumerable<string> Tags { get; internal set; }

        [DataMember(Name = "revision_guid")]
        public Guid RevisionGuid { get; set; }

        [DataMember(Name = "revision_number")]
        public int RevisionNumber { get; set; }

        [DataMember(Name = "revision_type")]
        public string RevisionType { get; set; }

        [DataMember(Name = "set_community_wiki")]
        public bool SetCommunityWiki { get; set; }

        [DataMember(Name = "user")]
        public ShallowUser User { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "post_id")]
        public int PostId { get; set; }

        internal override Type WrapperClass
        {
            get { return typeof(RevisionWrapper); }
        }

    }
}
