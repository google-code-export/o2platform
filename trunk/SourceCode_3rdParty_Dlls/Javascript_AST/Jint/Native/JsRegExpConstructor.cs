using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class JsRegExpConstructor : JsConstructor
    {
        public JsRegExpConstructor(IGlobal global)
            : base(global)
        {
            Name = "RegExp";
        }

        public override void InitPrototype(IGlobal global)
        {
            //Prototype = global.ObjectClass.New(this);
            Prototype.DefineOwnProperty("constructor", this, PropertyAttributes.DontEnum);

            Prototype.DefineOwnProperty("toString", global.FunctionClass.New<JsDictionaryObject>(ToStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("toLocaleString", global.FunctionClass.New<JsDictionaryObject>(ToStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("lastIndex", global.FunctionClass.New<JsRegExp>(GetLastIndex), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("exec", global.FunctionClass.New<JsRegExp>(ExecImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("test", global.FunctionClass.New<JsRegExp>(TestImpl), PropertyAttributes.DontEnum);
        }

        public JsInstance GetLastIndex(JsRegExp regex, JsInstance[] parameters)
        {
            return regex["lastIndex"];
        }

        public JsRegExp New()
        {
            return New(String.Empty, false, false, false);
        }

        public JsRegExp New(string pattern, bool g, bool i, bool m)
        {
            var ret = new JsRegExp(pattern, g, i, m) { Prototype = this.Prototype };
            ret["source"] = Global.StringClass.New(pattern);
            ret["lastIndex"] = Global.NumberClass.New(0);
            ret["global"] = Global.BooleanClass.New(g);

            return ret;
        }

        public JsInstance ExecImpl(JsRegExp regexp, JsInstance[] parameters)
        {
            string S = parameters[0].ToString();
            int length = S.Length;
            int lastIndex = (int)regexp["lastIndex"].ToNumber();
            int i = lastIndex;
            if (regexp["global"].ToBoolean())
                i = 0;
            if (i < 0 || i > length)
            {
                lastIndex = 0;
                return JsNull.Instance;
            }
            Match r = ((Regex)regexp.Value).Match(S, i);
            if (!r.Success)
            {
                lastIndex = 0;
                return JsNull.Instance;
            }
            int e = r.Index + r.Length;
            if (regexp["global"].ToBoolean())
                lastIndex = e;
            int n = r.Groups.Count;
            JsArray result = Global.ArrayClass.New();
            result["index"] = Global.NumberClass.New(r.Index);
            result["input"] = Global.StringClass.New(S);
            result["length"] = Global.NumberClass.New(n + 1);
            result[Global.NumberClass.New(0)] = Global.StringClass.New(r.Value);
            for (i = 1; i > 0 && i < n; i++)
            {
                result[Global.NumberClass.New(i)] = Global.StringClass.New(r.Groups[i].Value);
            }
            return result;
        }

        public JsInstance TestImpl(JsRegExp regex, JsInstance[] parameters)
        {
            return new JsBoolean(ExecImpl(regex, parameters) != JsNull.Instance);
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            if (parameters.Length == 0)
            {
                return visitor.Return(New());
                //throw new ArgumentNullException("pattern");
            }

            return visitor.Return(New(parameters[0].ToString(), false, false, false));
        }

        public JsInstance ToStringImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            return Global.StringClass.New(target.ToString());
        }
    }
}
