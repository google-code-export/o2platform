using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheWorldsWorst.ApiWrapper.Helpers;
using System.Globalization;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Different PostEvent types
    /// </summary>
    public enum PostEventType
    {
        Question,
        Answer,
        Comment,
        Revision,
        Votes
    }

    /// <summary>
    /// Represents an event that can occur to a Question or an Answer.
    /// </summary>
    [DataContract]
    public class PostEvent : ATimelineEvent
    {
        [DataContract]
        class PostEventWrapper
        {
            [DataMember]
            IEnumerable<PostEvent> post_timelines;
        }

        internal override Type WrapperClass
        {
            get { return typeof(PostEventWrapper); }
        }

        [DataMember(Name="timeline_type")]
        internal string _PostTimelineType { get; set; }
        public PostEventType PostTimelineType { get { return (PostEventType)Enum.Parse(typeof(PostEventType), _PostTimelineType, true); } }
        
        [DataMember(Name="post_id")]
        public int PostId { get; internal set; }

        [DataMember(Name = "revision_guid")]
        public string RevisionGuid { get; internal set; }

        [DataMember(Name = "comment_id")]
        public int? CommentId { get; internal set; }

        [DataMember(Name="user_id")]
        public int? UserId { get; internal set; }
        [DataMember(Name="display_name")]
        public string DisplayName { get; internal set; }
        [DataMember(Name = "email_hash")]
        public string EmailHash { get; internal set; }

        [DataMember(Name="owner_user_id")]
        public int? OwnerUserId { get; internal set; }
        [DataMember(Name = "owner_display_name")]
        public string OwnerDisplayName { get; internal set; }
        [DataMember(Name = "owner_email_hash")]
        public string OwnerEmailHash { get; internal set; }

        [DataMember(Name="action")]
        public string Action { get; internal set; }

        [DataMember(Name = "post_revision_url")]
        internal string _PostRevisionUrl { get; set; }
        public Revision PostRevision { get { return ApiProxy.GetObject<Revision>(_PostRevisionUrl); } }

        [DataMember(Name = "post_url")]
        internal string _PostUrl { get; set; }
        public Post Post
        {
            get
            {
                switch (PostTimelineType)
                {
                    case PostEventType.Answer: return ApiProxy.GetObject<Answer>(_PostUrl);
                    case PostEventType.Question: return ApiProxy.GetObject<Question>(_PostUrl);
                }

                throw new NotImplementedException("Not Implemented for this PostTimelineType");
            }
        }

        [DataMember(Name = "post_comment_url")]
        internal string _CommentUrl { get; set; }
        public Comment Comment { get { return ApiProxy.GetObject<Comment>(_CommentUrl); } }
    }
}
