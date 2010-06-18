using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWorldsWorst.ApiWrapper.Helpers;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Represents a Question
    /// </summary>
    [DataContract]
    public class Question : Post
    {
        [DataContract]
        class QuestionWrapper
        {
            [DataMember]
            IEnumerable<Question> questions;
        }

        internal override Type WrapperClass
        {
            get { return typeof(QuestionWrapper); }
        }

        [DataMember(Name="tags")]
        public IEnumerable<string> Tags { get; internal set; }
        [DataMember(Name="answer_count")]
        public int TotalAnswers { get; internal set; }
        [DataMember(Name = "answers")]
        internal IEnumerable<Answer> m_answers;

        public IEnumerable<Answer> Answers
        {
            get
            {
                if (m_answers == null)
                    m_answers = ApiProxy.GetObjects<Answer>(AnswersUrl);

                return m_answers;
            }
        }
        [DataMember(Name="accepted_answer_id")]
        public int? AcceptedAnswer { get; internal set; }
        [DataMember(Name = "favorite_count")]
        public int FavoriteCount { get; internal set; }

        [DataMember(Name = "bounty_closes_date")]
        internal long? _BountyClosesDate { get; set; }
        public DateTime? BountyClosesDate { get { if (!_BountyClosesDate.HasValue) return null; return _BountyClosesDate.Value.FromUnixTime(); } }

        [DataMember(Name = "bounty_amount")]
        public int? BountyAmount { get; internal set; }

        [DataMember(Name="question_timeline_url")]
        internal string TimelineUrl { get; set; }
        [DataMember(Name = "question_comments_url")]
        internal string CommentsUrl { get; set; }
        [DataMember(Name = "question_answers_url")]
        internal string AnswersUrl { get; set; }

        public override IEnumerable<Comment> Comments
        {
            get
            {
                if (base.Comments != null)
                {
                    return base.Comments; 
                }

                base.Comments = ApiProxy.GetObjects<Comment>(CommentsUrl);

                return base.Comments;
            }
            internal set
            {
                base.Comments = value;
            }
        }

        public override IEnumerable<PostEvent> Events
        {
            get
            {
                if(base.Events != null) return base.Events;

                base.Events = ApiProxy.GetObjects<PostEvent>(TimelineUrl);

                return base.Events;
            }
            internal set
            {
                base.Events = value;
            }
        }
    }
}
