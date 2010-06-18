using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;
using Jint.Expressions;
using System.Globalization;
using System.Web;

namespace Jint.Native
{
    [Serializable]
    public class JsGlobal : JsObject, IGlobal
    {
        /// <summary>
        /// Useful for eval()
        /// </summary>
        public IJintVisitor Visitor { get; set; }

        public Options Options { get; set; }

        public JsGlobal(ExecutionVisitor visitor, Options options)
        {
            this.Options = options;
            this.Visitor = visitor;

            this["null"] = JsNull.Instance;

            #region Global Classes
            this["Object"] = ObjectClass = new JsObjectConstructor(this);
            this["Function"] = FunctionClass = new JsFunctionConstructor(this);
            this["Array"] = ArrayClass = new JsArrayConstructor(this);
            this["Boolean"] = BooleanClass = new JsBooleanConstructor(this);
            this["Date"] = DateClass = new JsDateConstructor(this);

            this["Error"] = ErrorClass = new JsErrorConstructor(this, "Error");
            this["EvalError"] = EvalErrorClass = new JsErrorConstructor(this, "EvalError");
            this["RangeError"] = RangeErrorClass = new JsErrorConstructor(this, "RangeError");
            this["ReferenceError"] = ReferenceErrorClass = new JsErrorConstructor(this, "ReferenceError");
            this["SyntaxError"] = SyntaxErrorClass = new JsErrorConstructor(this, "SyntaxError");
            this["TypeError"] = TypeErrorClass = new JsErrorConstructor(this, "TypeError");
            this["URIError"] = URIErrorClass = new JsErrorConstructor(this, "URIError");

            this["Number"] = NumberClass = new JsNumberConstructor(this);
            this["RegExp"] = RegExpClass = new JsRegExpConstructor(this);
            this["String"] = StringClass = new JsStringConstructor(this);
            this["Math"] = MathClass = new JsMathConstructor(this);
            this.Prototype = ObjectClass.Prototype;
            #endregion


            MathClass.Prototype = ObjectClass.Prototype;

            foreach (JsInstance c in this.GetValues())
            {
                if (c is JsConstructor)
                {
                    ((JsConstructor)c).InitPrototype(this);
                }
            }

            #region Global Properties
            this["NaN"] = NumberClass["NaN"];  // 15.1.1.1
            this["Infinity"] = NumberClass["POSITIVE_INFINITY"]; // // 15.1.1.2
            this["undefined"] = JsUndefined.Instance; // 15.1.1.3
            this[JsInstance.THIS] = this;
            #endregion

            #region Global Functions
            this["eval"] = new JsFunctionWrapper(Eval); // 15.1.2.1
            this["parseInt"] = new JsFunctionWrapper(ParseInt); // 15.1.2.2
            this["parseFloat"] = new JsFunctionWrapper(ParseFloat); // 15.1.2.3
            this["isNaN"] = new JsFunctionWrapper(IsNaN);
            this["isFinite"] = new JsFunctionWrapper(isFinite);
            this["decodeURI"] = new JsFunctionWrapper(DecodeURI);
            this["encodeURI"] = new JsFunctionWrapper(EncodeURI);
            this["decodeURIComponent"] = new JsFunctionWrapper(DecodeURIComponent);
            this["encodeURIComponent"] = new JsFunctionWrapper(EncodeURIComponent);
            #endregion

        }

        #region Global Functions

        public JsObjectConstructor ObjectClass { get; private set; }
        public JsFunctionConstructor FunctionClass { get; private set; }
        public JsArrayConstructor ArrayClass { get; private set; }
        public JsBooleanConstructor BooleanClass { get; private set; }
        public JsDateConstructor DateClass { get; private set; }
        public JsErrorConstructor ErrorClass { get; private set; }
        public JsErrorConstructor EvalErrorClass { get; private set; }
        public JsErrorConstructor RangeErrorClass { get; private set; }
        public JsErrorConstructor ReferenceErrorClass { get; private set; }
        public JsErrorConstructor SyntaxErrorClass { get; private set; }
        public JsErrorConstructor TypeErrorClass { get; private set; }
        public JsErrorConstructor URIErrorClass { get; private set; }

        public JsMathConstructor MathClass { get; private set; }
        public JsNumberConstructor NumberClass { get; private set; }
        public JsRegExpConstructor RegExpClass { get; private set; }
        public JsStringConstructor StringClass { get; private set; }

        /// <summary>
        /// 15.1.2.1
        /// </summary>
        public JsInstance Eval(JsInstance[] arguments)
        {
            if (JsString.TYPEOF != arguments[0].Class)
            {
                return arguments[0];
            }

            Program p;

            try
            {
                p = JintEngine.Compile(arguments[0].ToString(), Visitor.DebugMode);
            }
            catch (Exception e)
            {
                throw new JsException(this.SyntaxErrorClass.New(e.Message));
            }

            try
            {
                p.Accept((IStatementVisitor)Visitor);
            }
            catch (Exception e)
            {
                throw new JsException(this.EvalErrorClass.New(e.Message));
            }

            return Visitor.Result;
        }

        /// <summary>
        /// 15.1.2.2
        /// </summary>
        public JsInstance ParseInt(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return JsUndefined.Instance;
            }

            //in case of an enum, just cast it to an integer
            if (arguments[0].IsClr && arguments[0].Value.GetType().IsEnum)
                return NumberClass.New((int)arguments[0].Value);

            string number = arguments[0].ToString().Trim();
            int sign = 1;
            int radix = 10;

            if (number == String.Empty)
            {
                return this["NaN"];
            }

            if (number.StartsWith("-"))
            {
                number = number.Substring(1);
                sign = -1;
            }
            else if (number.StartsWith("+"))
            {
                number = number.Substring(1);
            }

            if (arguments.Length >= 2)
            {
                if (arguments[1] != JsUndefined.Instance && !0.Equals(arguments[1]))
                {
                    radix = Convert.ToInt32(arguments[1].Value);
                }
            }

            if (radix == 0)
            {
                radix = 10;
            }
            else if (radix < 2 || radix > 36)
            {
                return this["NaN"];
            }

            if (number.ToLower().StartsWith("0x"))
            {
                radix = 16;
            }

            try
            {
                return NumberClass.New(sign * Convert.ToInt32(number, radix));
            }
            catch
            {
                return this["NaN"];
            }
        }

        /// <summary>
        /// 15.1.2.3
        /// </summary>
        public JsInstance ParseFloat(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return JsUndefined.Instance;
            }

            string number = arguments[0].ToString().Trim();

            float result;
            if (float.TryParse(number, NumberStyles.Float, new CultureInfo("en-US"), out result))
            {
                return new JsNumber(result);
            }
            else
            {
                return this["NaN"];
            }
        }

        /// <summary>
        /// 15.1.2.4
        /// </summary>
        public JsInstance IsNaN(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return JsBoolean.False;
            }

            return new JsBoolean(double.NaN.Equals(arguments[0].ToNumber()));
        }

        /// <summary>
        /// 15.1.2.5
        /// </summary>
        protected JsInstance isFinite(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return JsBoolean.False;
            }

            var value = arguments[0];
            return new JsBoolean(value != NumberClass["NaN"]
                && value != NumberClass["POSITIVE_INFINITY"]
                && value != NumberClass["NEGATIVE_INFINITY"]);
        }

        protected JsInstance DecodeURI(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return StringClass.New();
            }

            return this.StringClass.New(HttpUtility.UrlDecode(arguments[0].ToString()));
        }

        private static char[] reservedEncoded = new char[] { ';', ',', '/', '?', ':', '@', '&', '=', '+', '$', '#' };
        private static char[] reservedEncodedComponent = new char[] { '-', '_', '.', '!', '~', '*', '\'', '(', ')', '[', ']' };

        protected JsInstance EncodeURI(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return this.StringClass.New();
            }

            string encoded = HttpUtility.UrlEncode(arguments[0].ToString());

            foreach (char c in reservedEncoded)
            {
                encoded = encoded.Replace(HttpUtility.UrlEncode(c.ToString()), c.ToString());
            }

            foreach (char c in reservedEncodedComponent)
            {
                encoded = encoded.Replace(HttpUtility.UrlEncode(c.ToString()), c.ToString());
            }

            return this.StringClass.New(encoded.ToUpper());
        }

        protected JsInstance DecodeURIComponent(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return this.StringClass.New();
            }

            return this.StringClass.New(HttpUtility.UrlDecode(arguments[0].ToString()));
        }

        protected JsInstance EncodeURIComponent(JsInstance[] arguments)
        {
            if (arguments.Length < 1 || arguments[0] == JsUndefined.Instance)
            {
                return this.StringClass.New();
            }

            string encoded = HttpUtility.UrlEncode(arguments[0].ToString());

            foreach (char c in reservedEncodedComponent)
            {
                encoded = encoded.Replace(HttpUtility.UrlEncode(c.ToString()), c.ToString());
            }

            return this.StringClass.New(encoded.ToUpper());
        }

        #endregion

        public JsObject Wrap(object value)
        {
            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.Boolean:
                    return BooleanClass.New((bool)value);
                case TypeCode.Char:
                case TypeCode.String:
                    return StringClass.New(Convert.ToString(value));
                case TypeCode.DateTime:
                    return DateClass.New((DateTime)value);
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return NumberClass.New(Convert.ToDouble(value));
                case TypeCode.Object:
                    return ObjectClass.New(value);
                case TypeCode.DBNull:
                case TypeCode.Empty:
                default:
                    throw new ArgumentNullException("value");
            }
        }

        public JsClr WrapClr(object value)
        {
            if (value == null)
            {
                return new JsClr(Visitor, null);
            }

            JsClr clr = new JsClr(Visitor, value);

            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.Boolean:
                    clr.Prototype = BooleanClass.Prototype;
                    break;
                case TypeCode.Char:
                case TypeCode.String:
                    clr.Prototype = StringClass.Prototype;
                    break;
                case TypeCode.DateTime:
                    clr.Prototype = DateClass.Prototype;
                    break;
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    clr.Prototype = NumberClass.Prototype;
                    break;
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.Empty:
                default:
                    if (value is System.Collections.IEnumerable)
                        clr.Prototype = ArrayClass.Prototype;
                    else
                        clr.Prototype = ObjectClass.Prototype;
                    break;
            }

            return clr;
        }

        public bool HasOption(Options options)
        {
            return (Options & options) == options;
        }

        #region IGlobal Members


        public JsInstance NaN
        {
            get { return this["NaN"]; }
        }

        #endregion
    }
}
