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
    public abstract class Post : HasWrapperClass
    {
        [DataMember(Name = "question_id")]
        public int QuestionId { get; internal set; }

        [DataMember(Name = "owner")]
        public ShallowUser Owner { get; internal set; }

        [DataMember(Name = "creation_date")]
        internal long _CreationDate { get; set; }
        public DateTime CreationDate { get { return _CreationDate.FromUnixTime(); } }

        [DataMember(Name = "last_edit_date")]
        internal long? _LastEditDate { get; set; }
        public DateTime? LastEditDate { get { if (!_LastEditDate.HasValue) return null; return _LastEditDate.Value.FromUnixTime(); } }

        [DataMember(Name = "last_activity_date")]
        internal long? _LastActivityDate { get; set; }
        public DateTime? LastActivityDate { get { if (!_LastActivityDate.HasValue) return null; return _LastActivityDate.Value.FromUnixTime(); } }

        [DataMember(Name = "up_vote_count")]
        public int UpVoteCount { get; internal set; }
        [DataMember(Name = "down_vote_count")]
        public int DownVoteCount { get; internal set; }
        [DataMember(Name="view_count")]
        public int ViewCount { get; internal set; }
        [DataMember(Name = "score")]
        public int Score { get; internal set; }
        [DataMember(Name = "community_owned")]
        public bool IsCommunityOwned { get; internal set; }
        [DataMember(Name = "title")]
        public string Title { get; internal set; }
        [DataMember(Name = "body")]
        public string Body { get; internal set; }
        [DataMember(Name = "comments")]
        internal IEnumerable<Comment> m_comments { get; set; }

        internal IEnumerable<PostEvent> m_timeline { get; set; }

        public virtual IEnumerable<Comment> Comments { get { return m_comments; } internal set { m_comments = value; } }
        public virtual IEnumerable<PostEvent> Events { get { return m_timeline; } internal set { m_timeline = value; } }
    }
}
