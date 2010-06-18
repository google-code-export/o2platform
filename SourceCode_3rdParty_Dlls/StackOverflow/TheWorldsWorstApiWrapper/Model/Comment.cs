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
    /// Represents a Comment.
    /// </summary>
    [DataContract]
    public class Comment : HasWrapperClass
    {
        [DataContract]
        class CommentWrapper
        {
            [DataMember]
            IEnumerable<Comment> comments;
        }

        internal override Type WrapperClass
        {
            get { return typeof(CommentWrapper); }
        }

        [DataMember(Name = "owner")]
        public ShallowUser Owner { get; set; }

        [DataMember(Name="reply_to_user")]
        public ShallowUser ReplyToUser { get; internal set;}
        [DataMember(Name="comment_id")]
        public int CommentId { get; internal set; }
        [DataMember(Name="score")]
        public int VoteCount { get; internal set; }
        [DataMember(Name="post_id")]
        public int PostId { get; internal set; }

        [DataMember(Name="post_type")]
        internal string _PostType { get; set; }
        public PostType PostType { get { return (PostType)Enum.Parse(typeof(PostType), _PostType, true); } }

        [DataMember(Name="creation_date")]
        internal long _CreationDate { get; set; }
        public DateTime CreationDate { get { return _CreationDate.FromUnixTime(); } }

        [DataMember(Name="edit_count")]
        public int? EditCount { get; internal set; }

        [DataMember(Name="body")]
        public string Body { get; internal set; }

        public string Ago
        {
            get
            {
                var ago = DateTime.Now - CreationDate;

                if (ago.Days < 1)
                {
                    return ago.ToString();
                }
                else
                {
                    return CreationDate.ToString("MMM dd", CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
