using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Runtime.Serialization;

using TheWorldsWorst.ApiWrapper.Helpers;

namespace TheWorldsWorst.ApiWrapper.Model
{
    /// <summary>
    /// Root class for timeline events.
    /// </summary>
    [DataContract]
    public abstract class ATimelineEvent : HasWrapperClass
    {
        [DataMember(Name = "creation_date")]
        internal long _CreationDate { get; set; }
        public DateTime CreationDate { get { return _CreationDate.FromUnixTime(); } }

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
