using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    [Serializable]
    public sealed class JsBoolean : JsObject
    {
        private bool value;

        public override object Value
        {
            get { return value; }
        }

        public JsBoolean()
        {
            value = false;
        }

        public JsBoolean(bool boolean)
        {
            value = boolean;
        }

        public static JsBoolean False = new JsBoolean(false);
        public static JsBoolean True = new JsBoolean(true);
        
        public const string TYPEOF = "boolean";

        public override string Class
        {
            get { return TYPEOF; }
        }

        public override bool ToBoolean()
        {
            return value;
        }

        public override string ToString()
        {
            return value ? "true" : "false";
        }

        public override double ToNumber()
        {
            return value ? 1 : 0;
        }
    }
}
