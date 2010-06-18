using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Jint.Delegates;

namespace Jint.Native
{
    [Serializable]
    public sealed class JsString : JsObject
    {
        private string value;

        public override object Value
        {
            get
            {
                return value;
            }
        }
        public JsString()
        {
            value = String.Empty;
        }

        public JsString(string str)
        {
            value = str;
        }

        public override bool ToBoolean()
        {
            if (value == null)
                return false;
            if (value == "true" || value.Length > 0)
            {
                return true;
            }

            return false;
        }

        public override double ToNumber()
        {
            if (value == null)
            {
                return double.NaN;
            }

            double result;

            if (Double.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return double.NaN;
            }
        }

        public override string ToSource()
        {
            /// TODO: subsitute escape sequences
            return "'" + ToString() + "'";
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public const string TYPEOF = "string";

        public override string Class
        {
            get { return TYPEOF; }
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
