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
    /// Different types of User timeline events
    /// </summary>
    public enum UserTimelineType
    {
        Comment,
        AskOrAnswered,
        Badge,
        Revision,
        Accepted
    }

    /// <summary>
    /// Represents an event that a user could have performed, or had happen to them.
    /// </summary>
    [DataContract]
    public class UserEvent : ATimelineEvent
    {
        [DataContract]
        class UserEventWrapper
        {
            [DataMember]
            IEnumerable<UserEvent> user_timelines;
        }

        internal override Type WrapperClass
        {
            get { return typeof(UserEventWrapper); }
        }

        [DataMember(Name = "timeline_type")]
        internal string _UserTimelineType { get; set; }
        public UserTimelineType UserTimelineType { get { return (UserTimelineType)Enum.Parse(typeof(UserTimelineType), _UserTimelineType, true); } }
        
        [DataMember(Name="post_id")]
        public int? PostId { get; internal set; }
        [DataMember(Name = "comment_id")]
        public int? CommentId { get; internal set; }
        [DataMember(Name = "action")]
        public string Action { get; internal set; }
        [DataMember(Name = "description")]
        public string Description { get; internal set; }
        [DataMember(Name = "detail")]
        public string Detail { get; internal set; }
        
        //Artificial properties
        public int UserId { get; internal set; }
        public string UserName { get; internal set; }
    }
}
