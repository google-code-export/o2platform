using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Jint.Delegates;
using System.Reflection;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public sealed class JsClr : JsObject
    {
        private IGlobal global;
        private IPropertyGetter propertyGetter;
        private IMethodInvoker methodGetter;
        private IFieldGetter fieldGetter;

        public JsClr(IJintVisitor visitor)
        {
            this.global = visitor.Global;
            this.propertyGetter = visitor.PropertyGetter;
            this.methodGetter = visitor.MethodGetter;
            this.fieldGetter = visitor.FieldGetter;
            value = null;

        }

        public JsClr(IJintVisitor visitor, object clr)
            : this(visitor)
        {
            value = clr;
            if (value != null)
            {
                if (value is System.Collections.IEnumerable)
                    clrCountProperty = propertyGetter.GetValue(value, "Count");
                else
                {
                    //properties = new List<string>();
                    foreach (PropertyInfo pi in value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        DefineOwnProperty(pi.Name, new ClrPropertyDescriptor(propertyGetter, global, this, pi.Name));
                    }
                    foreach (FieldInfo pi in value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                    {
                        DefineOwnProperty(pi.Name, new ClrFieldDescriptor(fieldGetter, global, this, pi.Name));
                    }
                    ClrMethodDescriptor cmd = null;
                    foreach (MethodInfo mi in value.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static))
                    {
                        if (cmd == null || cmd.Name != mi.Name)
                            DefineOwnProperty(mi.Name, cmd = new ClrMethodDescriptor(this, mi.Name));
                    }
                }
            }
        }

        public override bool IsClr
        {
            get { return true; }
        }

        public override bool HasOwnProperty(string key)
        {
            if (base.HasOwnProperty(key))
                return true;
            if (properties == null)
                return false;
            Descriptor d;
            return properties.TryGet(key, out d);
        }

        //List<string> properties = null;

        private PropertyInfo clrCountProperty;

        public override int Length
        {
            get
            {
                if (clrCountProperty != null)
                    return (int)clrCountProperty.GetValue(value, null);
                return base.Length;
            }
            set
            {
                base.Length = value;
            }
        }

        public override bool ToBoolean()
        {
            if (value == null)
                return false;
            if (value is string)
                return !string.IsNullOrEmpty((string)value);
            if (value is IConvertible)
                return Convert.ToBoolean(value);
            return true;
        }

        public override double ToNumber()
        {
            if (value == null)
                return 0;
            if (value is IConvertible)
                return Convert.ToDouble(value);
            return double.NaN;
        }

        public override string ToString()
        {
            if (value == null)
                return null;
            if (value is IConvertible)
                return Convert.ToString(value);
            return value.ToString();
        }

        public const string TYPEOF = "clr";

        public override string Class
        {
            get
            {
                if (Prototype == global.BooleanClass.Prototype)
                    return JsBoolean.TYPEOF;
                if (Prototype == global.DateClass.Prototype)
                    return JsDate.TYPEOF;
                if (Prototype == global.NumberClass.Prototype)
                    return JsNumber.TYPEOF;
                if (Prototype == global.StringClass.Prototype)
                    return JsString.TYPEOF;
                return TYPEOF;
            }
        }

        public override object Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Converts a JsInstance object to its CLR equivalence
        /// </summary>
        /// <param name="parameter">The object to convert</param>
        /// <returns>A CLR object</returns>
        public static object ConvertParameter(JsInstance parameter)
        {
            if (parameter.Class != JsFunction.TYPEOF && parameter.Class != JsArray.TYPEOF)
            {
                return parameter.Value;
            }
            else if (parameter == JsNull.Instance)
            {
                return null;
            }
            else
            {
                if (parameter.IsClr)
                    return parameter.Value;

                JsFunction constructor = ((JsDictionaryObject)parameter)["constructor"] as JsFunction;
                if (constructor == null)
                    return parameter;
                switch (constructor.Name)
                {
                    case "Date":
                        return JsDateConstructor.CreateDateTime(parameter.ToNumber());
                    case "String":
                    case "RegExp":
                    case "Number":
                        return parameter.Value;
                    case "Array":
                    case "Object":
                        if (parameter.Class == JsFunction.TYPEOF)
                            return parameter;
                        object[] array = new object[((JsObject)parameter).Length];
                        foreach (KeyValuePair<string, JsInstance> key in (JsObject)parameter)
                        {
                            int index;
                            if (int.TryParse(key.Key, out index))
                            {
                                array[index] = ConvertParameters(key.Value)[0];
                            }
                        }
                        return new System.Collections.ArrayList(array);
                    default:
                        return parameter;
                }
            }

        }

        /// <summary>
        /// Converts a set of JsInstance objects to their CLR equivalences
        /// </summary>
        /// <param name="parameters">The objects to convert</param>
        /// <returns>An array of CLR object</returns>
        public static object[] ConvertParameters(params JsInstance[] parameters)
        {
            object[] clrParameters = new object[parameters.Length];
            for (int j = 0; j < clrParameters.Length; j++)
            {
                // don't convert JsFunction as they will be translated to Delegates later
                clrParameters[j] = ConvertParameter(parameters[j]);
            }
            return clrParameters;
        }

        public static JsInstance[] ConvertParametersBack(IJintVisitor visitor, object[] args)
        {
            JsInstance[] jsParameters = new JsInstance[args.Length];
            for (int j = 0; j < jsParameters.Length; j++)
            {
                // don't convert JsFunction as they will be translated to Delegates later
                jsParameters[j] = ConvertParameterBack(visitor, args[j]);
            }
            return jsParameters;
        }

        public static JsInstance ConvertParameterBack(IJintVisitor visitor, object parameter)
        {
            //if (parameter.Class != JsFunction.TYPEOF && parameter.Class != JsArray.TYPEOF)
            //{
            //    return parameter.Value;
            //}
            if (parameter == null)
            {
                return JsNull.Instance;
            }
            else
            {
                if (parameter.GetType().IsArray)
                {
                    JsArray jsArray = visitor.Global.ArrayClass.New();
                    int index = -1;

                    foreach (object value in (System.Collections.IEnumerable)parameter)
                    {
                        jsArray[(index++).ToString()] = ConvertParameterBack(visitor, value);
                    }
                    return jsArray;
                }
                else
                    return visitor.Global.WrapClr(parameter);
            }

        }
    }
}
