using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TheWorldsWorst.ApiWrapper.Model
{
    [DataContract]
    public class ApiVersion
    {
        [DataMember(Name="version")]
        public string Version { get; internal set; }
        [DataMember(Name = "revision")]
        public string Revision { get; internal set; }
    }

    [DataContract]
    public class Stats : HasWrapperClass
    {
        [DataContract]
        class StatsWrapper
        {
            [DataMember]
            IEnumerable<Stats> statistics;
        }

        internal override Type WrapperClass
        {
            get { return typeof(StatsWrapper); }
        }

        [DataMember(Name = "total_question")]
        public int TotalQuestions { get; internal set; }
        [DataMember(Name = "total_unanswered")]
        public int TotalUnanswered { get; internal set; }
        [DataMember(Name = "total_answers")]
        public int TotalAnswers { get; internal set; }
        [DataMember(Name = "total_comments")]
        public int TotalComments { get; internal set; }
        [DataMember(Name = "total_votes")]
        public int TotalVotes { get; internal set; }
        [DataMember(Name = "total_badges")]
        public int TotalBadges { get; internal set; }
        [DataMember(Name = "total_users")]
        public int TotalUsers { get; internal set; }

        [DataMember(Name = "questions_per_minute")]
        public decimal QuestionsPerMinute { get; internal set; }
        [DataMember(Name = "answers_per_minute")]
        public decimal AnswersPerMinute { get; internal set; }
        [DataMember(Name = "badges_per_minute")]
        public decimal BadgesPerMinute { get; internal set; }

        [DataMember(Name = "api_version")]
        public ApiVersion ApiVersion { get; internal set; }
    }
}
