using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;
using System.Globalization;

namespace Jint.Native
{
    [Serializable]
    public sealed class JsDate : JsObject
    {
        static internal long OFFSET_1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        static internal int TICKSFACTOR = 10000;

        private double value;

        public override object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = (double)value;
            }
        }

        public JsDate()
        {
            value = 0;
        }

        public JsDate(DateTime date) : this((date.ToUniversalTime().Ticks - OFFSET_1970) / TICKSFACTOR)
        {
        }

        public JsDate(double value)
        {
            this.value = value;
        }

        public override double ToNumber()
        {
            return value;
        }

        public static string FORMAT = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'zzz";
        public static string FORMATUTC = "ddd, dd MMM yyyy HH':'mm':'ss 'UTC'";
        public static string DATEFORMAT = "ddd, dd MMM yyyy";
        public static string TIMEFORMAT = "HH':'mm':'ss 'GMT'zzz";

        public override string ToString()
        {
            return JsDateConstructor.CreateDateTime(value).ToLocalTime().ToString(FORMAT, CultureInfo.InvariantCulture);
        }

        public override object ToObject()
        {
            return JsDateConstructor.CreateDateTime(value);
        }
        public const string TYPEOF = "object";

        public override string Class
        {
            get { return TYPEOF; }
        }
    }
}
