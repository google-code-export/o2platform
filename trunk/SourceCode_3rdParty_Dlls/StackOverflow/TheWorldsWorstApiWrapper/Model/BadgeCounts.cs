using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using TheWorldsWorst.ApiWrapper.Helpers;
using System.Runtime.Serialization;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Represents Badge Counts
    /// </summary>
    [DataContract]
    public class BadgeCounts
    {
        [DataMember(Name = "gold")]
        public int? gold { get; set; }

        [DataMember(Name = "silver")]
        public int? silver { get; set; }

        [DataMember(Name = "bronze")]
        public int? bronze { get; set; }

    }
}
