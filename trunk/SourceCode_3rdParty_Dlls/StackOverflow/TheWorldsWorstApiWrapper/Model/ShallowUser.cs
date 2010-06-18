using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    [DataContract]
    public class ShallowUser
    {
        [DataMember(Name="user_id")]
        public int? UserId { get; set; }

        [DataMember(Name="user_type")]
        internal string _UserType { get; set; }
        public UserType UserType { get { return (UserType)Enum.Parse(typeof(UserType), _UserType, true); } }

        [DataMember(Name="display_name")]
        public string DisplayName { get; internal set; }
        
        [DataMember(Name="reputation")]
        public int? Reputation { get; internal set; }

        [DataMember(Name="email_hash")]
        internal string _EmailHash { get; set; }
        public string Gravatar { get { return String.Format("http://www.gravatar.com/avatar/{0}?d=identicon&r=PG", _EmailHash); } }
    }
}
