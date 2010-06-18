using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class JsErrorConstructor : JsConstructor
    {
        private string errorType;

        public JsErrorConstructor(IGlobal global, string errorType)
            : base(global)
        {
            this.errorType = errorType;
            Name = errorType;
        }

        public override void InitPrototype(IGlobal global)
        {
            //Prototype = global.FunctionClass;
            Prototype.DefineOwnProperty("constructor", this, PropertyAttributes.DontEnum);

            Prototype.DefineOwnProperty("name", global.StringClass.New(errorType), PropertyAttributes.DontEnum | PropertyAttributes.DontDelete | PropertyAttributes.ReadOnly);
            Prototype.DefineOwnProperty("toString", global.FunctionClass.New<JsDictionaryObject>(ToStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("toLocaleString", global.FunctionClass.New<JsDictionaryObject>(ToStringImpl), PropertyAttributes.DontEnum);
        }

        public JsError New(string message)
        {
            var error = new JsError(Global) { Prototype = this.Prototype };
            error["message"] = Global.StringClass.New(message);
            return error;
        }

        public JsError New()
        {
            return New(String.Empty);
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            if (that == null)
            {
                visitor.Return(parameters.Length > 0 ? New(parameters[0].ToString()) : New());
            }
            else
            {
                if (parameters.Length > 0)
                {
                    that.Value = parameters[0].ToString();
                }
                else
                {
                    that.Value = String.Empty;
                }

                visitor.Return(that);
            }

            return that;
        }

        public JsInstance ToStringImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            return Global.StringClass.New(target["name"] + ": " + target["message"]);
        }
    }
}
