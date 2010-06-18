using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;

namespace Jint.Native
{
    [Serializable]
    public class JsError : JsObject
    {
        private string message { get { return this["message"].ToString(); } set { this["message"] = global.StringClass.New(value); } }

        public override object Value
        {
            get
            {
                return message;
            }
        }

        private IGlobal global;

        public JsError(IGlobal global)
            : this(global, string.Empty)
        {
        }

        public JsError(IGlobal global, string message)
        {
            this.global = global;
            this.message = message;
        }

        public const string TYPEOF = "object";

        public override string Class
        {
            get { return TYPEOF; }
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
