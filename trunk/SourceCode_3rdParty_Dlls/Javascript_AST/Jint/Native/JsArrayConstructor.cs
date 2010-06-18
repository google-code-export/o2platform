﻿using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class JsArrayConstructor : JsConstructor
    {
        public JsArrayConstructor(IGlobal global)
            : base(global)
        {
            Name = "Array";
        }

        public override void InitPrototype(IGlobal global)
        {
            Prototype = new JsObject() { Prototype = global.FunctionClass.Prototype };
            Prototype.DefineOwnProperty("constructor", this, PropertyAttributes.DontEnum);

            #region Methods
            Prototype.DefineOwnProperty("toString", global.FunctionClass.New<JsArray>(ToStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("toLocaleString", global.FunctionClass.New<JsArray>(ToLocaleStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("concat", global.FunctionClass.New<JsObject>(Concat), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("join", global.FunctionClass.New<JsObject>(Join, 1), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("pop", global.FunctionClass.New<JsObject>(Pop), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("push", global.FunctionClass.New<JsObject>(Push, 1), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("reverse", global.FunctionClass.New<JsObject>(Reverse), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("shift", global.FunctionClass.New<JsObject>(Shift), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("slice", global.FunctionClass.New<JsObject>(Slice, 2), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("sort", global.FunctionClass.New<JsObject>(Sort), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("splice", global.FunctionClass.New<JsObject>(Splice, 2), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("unshift", global.FunctionClass.New<JsObject>(UnShift, 1), PropertyAttributes.DontEnum);

            #region ES5
            Prototype.DefineOwnProperty("indexOf", global.FunctionClass.New<JsObject>(IndexOfImpl, 1), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("lastIndexOf", global.FunctionClass.New<JsObject>(LastIndexOfImpl, 1), PropertyAttributes.DontEnum);
            #endregion

            #endregion

            #region Properties
            Prototype.DefineOwnProperty("length", new PropertyDescriptor<JsObject>(global, Prototype, "length", GetLengthImpl, SetLengthImpl));
            #endregion
        }

        public JsArray New()
        {
            JsArray array = new JsArray() { Prototype = this.Prototype };
            //array.DefineOwnProperty("constructor", new ValueDescriptor(this) { Enumerable = false });
            return array;
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            if (that == null)
            {
                JsArray array = Global.ArrayClass.New();

                for (int i = 0; i < parameters.Length; i++)
                {
                    array[i.ToString()] = parameters[i];
                }

                visitor.Return(array);
            }
            else
            {
                // When called as part of a new expression, it is a constructor: it initialises the newly created object.
                for (int i = 0; i < parameters.Length; i++)
                {
                    that[i.ToString()] = parameters[i];
                }

                visitor.Return(that);
            }

            return that;
        }

        public JsInstance GetLengthImpl(JsDictionaryObject target)
        {
            return Global.NumberClass.New(target.Length);
        }

        public JsInstance SetLengthImpl(JsInstance target, JsInstance[] parameters)
        {
            int length = (int)parameters[0].ToNumber();

            if (length < 0 || double.IsNaN(length) || double.IsInfinity(length))
            {
                throw new JsException(Global.RangeErrorClass.New("invalid array length"));
            }

            JsDictionaryObject array = (JsDictionaryObject)target;
            if (length < array.Length)
            {
                int oldLength = array.Length;
                for (int i = length; i < oldLength; i++)
                {
                    array.Delete(i.ToString());
                }
            }
            else
            {
                for (int i = array.Length; i < length; i++)
                {
                    array[i.ToString()] = JsUndefined.Instance;
                }
            }

            return parameters[0];
        }

        /// <summary>
        /// 15.4.4.2
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance ToStringImpl(JsArray target, JsInstance[] parameters)
        {
            JsArray result = Global.ArrayClass.New();

            for (int i = 0; i < target.Length; i++)
            {
                var obj = (JsDictionaryObject)target[i.ToString()];
                JsFunction function = obj["toString"] as JsFunction;
                if (function != null)
                {
                    Global.Visitor.ExecuteFunction(function, obj, parameters);
                    result[i.ToString()] = Global.Visitor.Returned;
                }
                else
                    result[i.ToString()] = null;
            }

            return Global.StringClass.New(result.ToString());

        }

        /// <summary>
        /// 15.4.4.3
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance ToLocaleStringImpl(JsArray target, JsInstance[] parameters)
        {
            JsArray result = Global.ArrayClass.New();

            for (int i = 0; i < target.Length; i++)
            {
                var obj = (JsDictionaryObject)target[i.ToString()];
                Global.Visitor.ExecuteFunction((JsFunction)obj["toLocaleString"], obj, parameters);
                result[i.ToString()] = Global.Visitor.Returned;
            }

            return Global.StringClass.New(result.ToString());
        }

        /// <summary>
        /// 15.4.4.4
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Concat(JsObject target, JsInstance[] parameters)
        {
            JsArray array = Global.ArrayClass.New();
            List<JsInstance> items = new List<JsInstance>();
            items.Add(target);
            items.AddRange(parameters);
            int n = 0;
            while (items.Count > 0)
            {
                JsInstance e = items[0];
                items.RemoveAt(0);
                if (e.Class == JsArray.TYPEOF)
                {
                    for (int k = 0; k < ((JsArray)e).Length; k++)
                    {
                        string p = k.ToString();
                        JsInstance result = null;
                        if (((JsDictionaryObject)e).TryGetProperty(p, out result))
                        {
                            array.DefineOwnProperty(n.ToString(), new ValueDescriptor(this, n.ToString(), result));
                            n++;
                        }
                    }
                }
                else
                {
                    array.DefineOwnProperty(n.ToString(), new ValueDescriptor(this, n.ToString(), e));
                    n++;
                }
            }
            return array;
        }

        /// <summary>
        /// 15.4.4.5
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Join(JsObject target, JsInstance[] parameters)
        {
            string separator = (parameters.Length == 0 || parameters[0] == JsUndefined.Instance)
                ? ","
                : parameters[0].ToString();

            if (target.Length == 0)
            {
                return Global.StringClass.New();
            }

            JsInstance element0 = target[0.ToString()];

            StringBuilder r;
            if (element0 == JsUndefined.Instance || element0 == JsNull.Instance)
            {
                r = new StringBuilder(string.Empty);
            }
            else
            {
                r = new StringBuilder(element0.Call(Global.Visitor, "toString").ToString());
            }

            var length = target["length"].ToNumber();

            for (int k = 1; k < length; k++)
            {
                r.Append(separator);
                JsInstance element = target[k.ToString()];
                if (element != JsUndefined.Instance && element != JsNull.Instance)
                    r.Append(element.Call(Global.Visitor, "toString").ToString());
            }
            return Global.StringClass.New(r.ToString());
        }

        /// <summary>
        /// 15.4.4.6
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Pop(JsObject target, JsInstance[] parameters)
        {
            var length = Convert.ToUInt32(target.Length);
            if (length == 0)
                return JsUndefined.Instance;
            var key = (length - 1).ToString();
            var result = target[key];
            target.Delete(key);
            return result;
        }

        /// <summary>
        /// 15.4.4.7
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Push(JsDictionaryObject target, JsInstance[] parameters)
        {
            int length = (int)target["length"].ToNumber();
            foreach (var arg in parameters)
            {
                target[length.ToString()] = arg;
                length++;
            }

            return target["length"] = Global.NumberClass.New(length);
        }

        /// <summary>
        /// 15.4.4.8
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Reverse(JsObject target, JsInstance[] parameters)
        {
            int len = target.Length;
            int middle = len / 2;

            for (int lower = 0; lower != middle; lower++)
            {
                int upper = len - lower - 1;
                string upperP = upper.ToString();
                string lowerP = lower.ToString();

                JsInstance lowerValue = null;
                JsInstance upperValue = null;
                bool lowerExists = target.TryGetProperty(lowerP, out lowerValue);
                bool upperExists = target.TryGetProperty(upperP, out upperValue);

                if (lowerExists)
                {
                    target[upperP] = lowerValue;
                }
                else
                {
                    target.Delete(upperP);
                }

                if (upperExists)
                {
                    target[lowerP] = upperValue;
                }
                else
                {
                    target.Delete(lowerP);
                }
            }
            return target;
        }

        /// <summary>
        /// 15.4.4.9
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Shift(JsDictionaryObject target, JsInstance[] parameters)
        {
            if (target.Length == 0)
            {
                return JsUndefined.Instance;
            }

            JsInstance first = target[0.ToString()];
            for (int k = 1; k < target.Length; k++)
            {
                JsInstance result = null;

                string from = k.ToString();
                string to = (k - 1).ToString();
                if (target.TryGetProperty(from, out result))
                {
                    target[to] = result;
                }
                else
                {
                    target.Delete(to);
                }
            }
            target.Delete((target.Length - 1).ToString());
            if (length != int.MinValue)
                length -= parameters.Length;
            return first;
        }

        /// <summary>
        /// 15.4.4.10
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Slice(JsObject target, JsInstance[] parameters)
        {
            int start = (int)parameters[0].ToNumber();
            int end = target.Length;
            if (parameters.Length > 1)
                end = (int)parameters[1].ToNumber();
            if (start < 0)
                start += target.Length;
            if (end < 0)
                end += target.Length;
            if (start > target.Length)
                start = target.Length;
            if (end > target.Length)
                end = target.Length;
            JsArray result = Global.ArrayClass.New();
            for (int i = start; i < end; i++)
                Push(result, new JsInstance[] { target[Global.NumberClass.New(i)] });

            return result;
        }

        /// <summary>
        /// 15.4.4.11
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Sort(JsObject target, JsInstance[] parameters)
        {
            if (target.Length <= 1)
            {
                return target;
            }

            JsFunction compare = null;

            // Compare function defined
            if (parameters.Length > 0)
            {
                compare = parameters[0] as JsFunction;
            }

            var values = new List<JsInstance>();
            var length = (int)target["length"].ToNumber();

            for (int i = 0; i < length; i++)
            {
                values.Add(target[i.ToString()]);
            }

            if (compare != null)
            {
                try
                {
                    values.Sort(new JsComparer(Global.Visitor, compare));
                }
                catch (Exception e)
                {
                    if (e.InnerException is JsException)
                    {
                        throw e.InnerException;
                    }

                    throw;
                }
            }
            else
            {
                values.Sort();
            }

            for (int i = 0; i < length; i++)
            {
                target[i.ToString()] = values[i];
            }

            return target;
        }

        /// <summary>
        /// 15.4.4.12
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance Splice(JsObject target, JsInstance[] parameters)
        {
            JsArray array = Global.ArrayClass.New();
            int relativeStart = Convert.ToInt32(parameters[0].ToNumber());
            int actualStart = relativeStart < 0 ? Math.Max(target.Length + relativeStart, 0) : Math.Min(relativeStart, target.Length);
            int actualDeleteCount = Math.Min(Math.Max(Convert.ToInt32(parameters[1].ToNumber()), 0), target.Length - actualStart);
            int len = target.Length;

            for (int k = 0; k < actualDeleteCount; k++)
            {
                string from = (relativeStart + k).ToString();
                JsInstance result = null;
                if (target.TryGetProperty(from, out result))
                {
                    array.DefineOwnProperty(k.ToString(), new ValueDescriptor(target, k.ToString(), result));
                }
            }

            List<JsInstance> items = new List<JsInstance>();
            items.AddRange(parameters);
            items.RemoveAt(0);
            items.RemoveAt(0);
            if (items.Count < actualDeleteCount)
            {
                for (int k = actualStart; k < len - actualDeleteCount; k++)
                {
                    JsInstance result = null;
                    string from = (k + actualDeleteCount).ToString();
                    string to = (k + items.Count).ToString();
                    if (target.TryGetProperty(from, out result))
                    {
                        target[to] = result;
                    }
                    else
                    {
                        target.Delete(to);
                    }
                }
                for (int k = target.Length; k > len - actualDeleteCount + items.Count; k--)
                {
                    target.Delete((k - 1).ToString());
                }
            }
            else
            {
                for (int k = len - actualDeleteCount; k > actualStart; k--)
                {
                    JsInstance result = null;
                    string from = (k + actualDeleteCount - 1).ToString();
                    string to = (k + items.Count - 1).ToString();
                    if (target.TryGetProperty(from, out result))
                    {
                        target[to] = result;
                    }
                    else
                    {
                        target.Delete(to);
                    }
                }
                if (target.length != int.MinValue && target.Prototype != Prototype)
                    target.length += len - actualDeleteCount + items.Count;

                for (int k = 0; items.Count > 0; k++)
                {
                    JsInstance e = items[0];
                    items.RemoveAt(0);
                    target[k.ToString()] = e;
                }
            }
            return array;
        }

        /// <summary>
        /// 15.4.4.13
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance UnShift(JsObject target, JsInstance[] parameters)
        {
            for (int k = target.Length; k > 0; k--)
            {
                JsInstance result = null;
                string from = (k - 1).ToString();
                string to = (k + parameters.Length - 1).ToString();
                if (target.TryGetProperty(from, out result))
                {
                    target[to] = result;
                }
                else
                {
                    target.Delete(to);
                }
            }
            List<JsInstance> items = new List<JsInstance>(parameters);
            for (int j = 0; items.Count > 0; j++)
            {
                JsInstance e = items[0];
                items.RemoveAt(0);
                target[j.ToString()] = e;
            }
            return Global.NumberClass.New(target.Length);
        }

        /// <summary>
        /// 15.4.4.15
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance LastIndexOfImpl(JsObject target, JsInstance[] parameters)
        {
            if (parameters.Length == 0)
            {
                return Global.NumberClass.New(-1);
            }

            int len = target.Length;
            if (len == 0)
                return Global.NumberClass.New(-1);
            int n = len;
            if (parameters.Length > 1)
                n = Convert.ToInt32(parameters[1].ToNumber());
            int k;
            JsInstance searchParameter = parameters[0];
            if (n >= 0)
                k = Math.Min(n, len - 1);
            else
                k = len - Math.Abs(n - 1);
            while (k >= 0)
            {
                JsInstance result = null;
                if (target.TryGetProperty(k.ToString(), out result))
                {
                    if (result == searchParameter)
                    {
                        return Global.NumberClass.New(k);
                    }
                }
                k--;
            }
            return Global.NumberClass.New(-1);
        }

        /// <summary>
        /// 15.4.4.15
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JsInstance IndexOfImpl(JsObject target, JsInstance[] parameters)
        {
            if (parameters.Length == 0)
            {
                return Global.NumberClass.New(-1);
            }

            int len = (int)target["length"].ToNumber();
            if (len == 0)
                return Global.NumberClass.New(-1);
            int n = 0;
            if (parameters.Length > 1)
                n = Convert.ToInt32(parameters[1].ToNumber());
            int k;
            if (n >= len)
                return Global.NumberClass.New(-1);

            JsInstance searchParameter = parameters[0];
            if (n >= 0)
                k = n;
            else
                k = len - Math.Abs(n);
            while (k < len)
            {
                JsInstance result = null;
                if (target.TryGetProperty(k.ToString(), out result))
                {
                    if (result == searchParameter)
                    {
                        return Global.NumberClass.New(k);
                    }
                }
                k++;
            }
            return Global.NumberClass.New(-1);
        }
    }
}
