using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using TheWorldsWorst.ApiWrapper.Helpers;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Represents an Answer
    /// </summary>
    [DataContract]
    public class Answer : Post
    {
        [DataContract]
        class AnswerWrapper
        {
            [DataMember]
            IEnumerable<Answer> answers;
        }

        internal override Type WrapperClass
        {
            get { return typeof(AnswerWrapper); }
        }

        [DataMember(Name="answer_id")]
        public int AnswerId { get; internal set; }       
        
        [DataMember(Name = "accepted")]
        public bool IsAccepted { get; internal set; }
        
        [DataMember(Name="answer_comments_url")]
        protected string CommentLink { get; set; }

        public int TotalVoteCount
        {
            get
            {
                return UpVoteCount - DownVoteCount;
            }
        }
    }
}
