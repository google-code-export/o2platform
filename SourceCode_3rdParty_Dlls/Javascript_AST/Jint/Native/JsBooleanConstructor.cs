using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class JsBooleanConstructor : JsConstructor
    {
        public JsBooleanConstructor(IGlobal global)
            : base(global)
        {
            Name = "Boolean";
        }

        public override void InitPrototype(IGlobal global)
        {
            Prototype = new JsObject() { Prototype = global.FunctionClass.Prototype };

            Prototype.DefineOwnProperty("constructor", this, PropertyAttributes.DontEnum);

            Prototype.DefineOwnProperty("toString", global.FunctionClass.New<JsDictionaryObject>(ToString2), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("toLocaleString", global.FunctionClass.New<JsDictionaryObject>(ToString2), PropertyAttributes.DontEnum);
        }

        public JsBoolean New()
        {
            return New(false);
        }

        public JsBoolean New(bool value)
        {
            return new JsBoolean(value) { Prototype = this.Prototype };
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            // e.g., var foo = Boolean(true);
            if (that == null)
            {
                visitor.Return(parameters.Length > 0 ? new JsBoolean(parameters[0].ToBoolean()) : new JsBoolean());
            }
            else // e.g., var foo = new Boolean(true);
            {
                if (parameters.Length > 0)
                {
                    that.Value = parameters[0].ToBoolean();
                }
                else
                {
                    that.Value = false;
                }

                visitor.Return(that);
            }

            return that;
        }


        public JsInstance ToString2(JsDictionaryObject target, JsInstance[] parameters)
        {
            return Global.StringClass.New(target.ToString());
        }
    }
}
