using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Jint.Native
{
    [Serializable]
    public sealed class JsNumber : JsObject
    {
        private double value;

        public override object Value
        {
            get
            {
                return value;
            }
        }

        public JsNumber() : this(0d)
        {
        }

        public JsNumber(double num)
        {
            value = num;
        }

        public JsNumber(int num)
        {
            value = num;
        }

        public override bool ToBoolean()
        {
            return value > 0;
        }

        public override double ToNumber()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString(CultureInfo.InvariantCulture).ToLower();
        }

        public override object ToObject()
        {
            return value;
        }

        public const string TYPEOF = "number";

        public override string Class
        {
            get { return TYPEOF; }
        }
    }
}
