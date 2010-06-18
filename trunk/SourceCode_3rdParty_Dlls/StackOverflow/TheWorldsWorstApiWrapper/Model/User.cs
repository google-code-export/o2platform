using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using TheWorldsWorst.ApiWrapper.Helpers;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    public enum UserType
    {
        Anonymous,
        Unregistered,
        Registered,
        Moderator
    }

    /// <summary>
    /// Represents a User
    /// </summary>
    [DataContract]
    public class User : HasWrapperClass
    {
        [DataContract]
        class UserWrapper
        {
            [DataMember]
            IEnumerable<User> users;
        }

        internal override Type WrapperClass
        {
            get { return typeof(UserWrapper); }
        }

        [DataMember(Name="user_id")]
        public int UserId { get; internal set; }

        [DataMember(Name = "user_type")]
        internal string _UserType { get; set; }
        public UserType UserType { get{ return (UserType)Enum.Parse(typeof(UserType), _UserType, true);}}

        [DataMember(Name = "display_name")]
        public string Name { get; internal set; }

        [DataMember(Name = "creation_date")]
        internal long _CreationDate { get; set; }
        public DateTime CreationDate { get { return _CreationDate.FromUnixTime(); } }

        [DataMember(Name = "reputation")]
        public int Reputation { get; internal set; }

        [DataMember(Name = "email_hash")]
        internal string _EmailHash { get; set; }
        public string Gravatar { get { return String.Format("http://www.gravatar.com/avatar/{0}?d=identicon&r=PG", _EmailHash); } }
        
        [DataMember(Name="age")]
        public int? Age { get; internal set; }

        [DataMember(Name="last_access_date")]
        internal long _LastAccessDate { get; set; }
        public DateTime LastAccessDate { get { return _LastAccessDate.FromUnixTime(); } }

        [DataMember(Name="website_url")]
        public string Website { get; internal set; }
        [DataMember(Name = "location")]
        public string Location { get; internal set; }
        [DataMember(Name = "about_me")]
        public string AboutMe { get; internal set; }
        [DataMember(Name = "question_count")]
        public int QuestionCount { get; internal set; }
        [DataMember(Name = "answer_count")]
        public int AnswerCount { get; internal set; }
        [DataMember(Name = "view_count")]
        public int ViewCount { get; internal set; }
        [DataMember(Name = "up_vote_count")]
        public int UpVotes { get; internal set; }
        [DataMember(Name = "down_vote_count")]
        public int DownVotes { get; internal set; }
        [DataMember(Name = "accept_rate")]
        public int? AcceptRate { get; internal set; }

        [DataMember(Name = "badge_counts")]
        public BadgeCounts BadgeCounts { get; internal set; }

        //Artificial properties
        public int GoldBadgeCount { get { return BadgeCounts != null ? BadgeCounts.gold ?? 0 : 0; } }
        public int SilverBadgeCount { get { return BadgeCounts != null ? BadgeCounts.silver ?? 0 : 0; } }
        public int BronzeBadgeCount { get { return BadgeCounts != null ? BadgeCounts.bronze ?? 0 : 0; } }

        public int TotalBadges { get { return GoldBadgeCount + SilverBadgeCount + BronzeBadgeCount; } }

        [DataMember(Name = "user_questions_url")]
        internal string QuestionsUrl { get; set; }
        private IEnumerable<Question> m_questions = null;
        public IEnumerable<Question> Questions {
            get
            {
                if (m_questions == null)
                    m_questions = ApiProxy.GetObjects<Question>(QuestionsUrl);

                return m_questions;
            }
        }

        [DataMember(Name = "user_answers_url")]
        internal string AnswersUrl { get; set; }
        private IEnumerable<Answer> m_answers = null;
        public IEnumerable<Answer> Answers
        {
            get
            {
                if (m_answers == null)
                    m_answers = ApiProxy.GetObjects<Answer>(AnswersUrl);

                return m_answers;
            }
        }

        [DataMember(Name = "user_badges_url")]
        internal string BadgesUrl { get; set; }
        private IEnumerable<Badge> m_badges = null;
        public IEnumerable<Badge> Badges
        {
            get
            {
                if (m_badges == null)
                    m_badges = ApiProxy.GetObjects<Badge>(BadgesUrl);

                return m_badges;
            }
        }

        [DataMember(Name = "user_tags_url")]
        internal string TagsUrl { get; set; }
        private IEnumerable<Tag> m_tags = null;
        public IEnumerable<Tag> Tags
        {
            get
            {
                if (m_tags == null)
                    m_tags = ApiProxy.GetObjects<Tag>(TagsUrl);

                return m_tags;
            }
        }


        
        
        [DataMember(Name = "user_favorites_url")]
        internal string FavoritesUrl { get; set; }
        private IEnumerable<Question> m_favorites = null;
        public IEnumerable<Question> Favorites
        {
            get
            {
                if (m_favorites == null)
                    m_favorites = ApiProxy.GetObjects<Question>(FavoritesUrl);

                return m_favorites;
            }
        }

        [DataMember(Name = "user_timeline_url")]
        internal string TimelineUrl { get; set; }
        private IEnumerable<UserEvent> m_timeline;
        public IEnumerable<UserEvent> Events
        {
            get
            {
                if (m_timeline == null)
                    m_timeline = ApiProxy.GetObjects<UserEvent>(TimelineUrl);

                return m_timeline;
            }
        }


        [DataMember(Name = "user_mentioned_url")]
        internal string MentionedUrl { get; set; }
        private IEnumerable<Comment> m_mentioned;
        public IEnumerable<Comment> MentionedIn
        {
            get
            {
                if (m_mentioned == null)
                    m_mentioned = ApiProxy.GetObjects<Comment>(MentionedUrl);

                return m_mentioned;
            }
        }

        [DataMember(Name = "user_comments_url")]
        internal string CommentsUrl { get; set; }
        private IEnumerable<Comment> m_comments;
        public IEnumerable<Comment> Comments
        {
            get
            {
                if (m_comments == null)
                    m_comments = ApiProxy.GetObjects<Comment>(CommentsUrl);

                return m_comments;
            }
        }

        // This really needs to be indexed, which is a bit out of scope at the moment
        [DataMember(Name = "user_reputation_url")]
        internal string ReputationUrl { get; set; }
    }
}
