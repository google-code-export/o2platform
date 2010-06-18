using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    [Serializable]
    public sealed class JsArray : JsObject
    {
        public JsArray()
            : base()
        {
        }

        public override bool ToBoolean()
        {
            return Length > 0;
        }

        public override double ToNumber()
        {
            return Length;
        }

        public override bool Equals(object obj)
        {
            if (obj is JsArray)
            {
                return this.Value.Equals(((JsArray)obj).Value);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            var list = new List<JsInstance>(GetValues());
            string[] values = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                    values[i] = list[i].ToString();
            }

            return String.Join(",", values);
        }

        public const string TYPEOF = "object";

        public override string Class
        {
            get { return TYPEOF; }
        }

    }
}
