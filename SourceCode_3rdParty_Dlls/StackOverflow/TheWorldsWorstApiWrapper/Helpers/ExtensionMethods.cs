using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheWorldsWorst.ApiWrapper.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Convert a long into a DateTime
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(this Int64 self)
        {
            var ret = new DateTime(1970, 1, 1);
            return ret.AddSeconds(self);
        }

        /// <summary>
        /// Convert a DateTime into a long
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Int64 ToUnixTime(this DateTime self)
        {
            var epoc = new DateTime(1970, 1, 1);
            var delta = self - epoc;

            if (delta.TotalSeconds < 0) throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");

            return (long)delta.TotalSeconds;
        }
    }
}
