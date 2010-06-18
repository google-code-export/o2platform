using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    [Serializable]
    public class JsObject : JsDictionaryObject
    {
        public JsObject()
        {
            //Prototype = null;
        }

        public JsObject(object value)
        {
            //Prototype = null;
            this.value = value;
        }

        public JsObject(JsFunction constructor)
        {
            Prototype = new JsObject(constructor.Prototype);
        }

        public const string TYPEOF = "Object";

        public override string Class
        {
            get { return TYPEOF; }
        }

        protected object value;

        public override object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override int GetHashCode()
        {
            return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
        }

        public override bool ToBoolean()
        {
            if (Prototype == null || Prototype == JsUndefined.Instance || Prototype == JsNull.Instance)
            {
                return false;
            }

            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.Boolean:
                    return (bool)value;
                case TypeCode.Char:
                case TypeCode.String:
                    return new JsString((string)value).ToBoolean();
                case TypeCode.DateTime:
                    return new JsDate((DateTime)value).ToBoolean();
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
                    return new JsNumber((double)value).ToBoolean();
                case TypeCode.Object:
                    return Convert.ToBoolean(value);
                case TypeCode.DBNull:
                case TypeCode.Empty:
                default:
                    if (value is IConvertible)
                    {
                        return Convert.ToBoolean(value);
                    }
                    else
                    {
                        return true;
                    }
            }
        }

        public override double ToNumber()
        {
            if (value == null)
            {
                return 0;
            }

            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.Boolean:
                    return new JsBoolean((bool)value).ToNumber();
                case TypeCode.Char:
                case TypeCode.String:
                    return new JsString((string)value).ToNumber();
                case TypeCode.DateTime:
                    return new JsDate((DateTime)value).ToNumber();
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
                    return (double)value;
                case TypeCode.Object:
                    return Convert.ToDouble(value);
                case TypeCode.DBNull:
                case TypeCode.Empty:
                default:
                    if (value is IConvertible)
                    {
                        return Convert.ToDouble(value);
                    }
                    else
                    {
                        return double.NaN;
                    }
            }
        }

        public override string ToString()
        {
            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }
    }
}
