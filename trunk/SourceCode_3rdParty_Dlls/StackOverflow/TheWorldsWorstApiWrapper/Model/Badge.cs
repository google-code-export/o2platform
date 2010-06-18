using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Represents a Badge
    /// </summary>
    [DataContract]
    public class Badge : HasWrapperClass, IComparable<Badge>
    {
        [DataContract]
        class BadgeWrapper
        {
            [DataMember]
            IEnumerable<Badge> badges;
        }

        internal override Type WrapperClass
        {
            get { return typeof(BadgeWrapper); }
        }

        public enum BadgeClass
        {
            Gold,
            Silver,
            Bronze
        }

        [DataMember(Name = "badge_id")]
        public int BadgeId { get; internal set; }

        [DataMember(Name = "rank")]
        internal string _Class { get; set; }
        public Badge.BadgeClass Class { get { return (BadgeClass)Enum.Parse(typeof(Badge.BadgeClass), _Class, true); } }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }
        [DataMember(Name = "description")]
        public string Description { get; internal set; }
        [DataMember(Name = "award_count")]
        public int AwardCount { get; internal set; }
        [DataMember(Name = "tag_based")]
        public bool TagBased { get; internal set; }
        [DataMember(Name = "user")]
        public ShallowUser User { get; internal set; }

        [DataMember(Name = "badges_recipients_url")]
        internal string RecipientsUrl { get; set; }
        private IEnumerable<User> m_recipients;
        public IEnumerable<User> Recipients
        {
            get
            {
                if (m_recipients == null)
                    m_recipients = ApiProxy.GetObjects<User>(RecipientsUrl);

                return m_recipients;
            }
        }

        public int CompareTo(Badge other)
        {
            return BadgeId.CompareTo(other.BadgeId);
        }

        public override bool Equals(object obj)
        {
            return (obj is Badge) &&
                BadgeId == ((Badge)obj).BadgeId;
        }

        public override int GetHashCode()
        {
            return BadgeId.GetHashCode();
        }
    }
}
