﻿using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;
using Jint.Delegates;

namespace Jint.Native
{
    [Serializable]
    public class JsFunctionConstructor : JsConstructor
    {
        public JsFunctionConstructor(IGlobal global)
            : base(global)
        {
            Prototype = new JsFunctionWrapper(delegate(JsInstance[] arguments) { return JsUndefined.Instance; }) { Prototype = global.ObjectClass.Prototype, Name = "Function" };
            Name = "Function";
        }

        public override void InitPrototype(IGlobal global)
        {
            ((JsFunction)Prototype).Scope = global.ObjectClass.Scope;
            Prototype.DefineOwnProperty("constructor", this, PropertyAttributes.DontEnum);

            Prototype.DefineOwnProperty(CALL.ToString(), new JsCallFunction(this), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty(APPLY.ToString(), new JsApplyFunction(this), PropertyAttributes.DontEnum);

            Prototype.DefineOwnProperty("toString", New<JsDictionaryObject>(ToString2), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("toLocaleString", New<JsDictionaryObject>(ToString2), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("length", new PropertyDescriptor<JsObject>(global, Prototype, "length", GetLengthImpl, SetLengthImpl));
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
                throw new JsException(Global.RangeErrorClass.New("invalid length"));
            }

            JsDictionaryObject obj = (JsDictionaryObject)target;
            obj.Length = length;

            return parameters[0];
        }

        public JsInstance GetLength(JsDictionaryObject target)
        {
            return Global.NumberClass.New(target.Length);
        }

        public JsFunction New()
        {
            JsFunction function = new JsFunction();
            function.Prototype = Global.ObjectClass.New(function);
            function.Scope.Prototype = Prototype;
            return function;
        }

        public JsFunction New<T>(Func<T, JsInstance> impl) where T : JsInstance
        {
            JsFunction function = new ClrImplDefinition<T>(impl);
            function.Prototype = Global.ObjectClass.New(function);
            function.Scope.Prototype = Prototype;
            return function;
        }
        public JsFunction New<T>(Func<T, JsInstance> impl, int length) where T : JsInstance
        {
            JsFunction function = new ClrImplDefinition<T>(impl, length);
            function.Prototype = Global.ObjectClass.New(function);
            function.Scope.Prototype = Prototype;
            return function;
        }

        public JsFunction New<T>(Func<T, JsInstance[], JsInstance> impl) where T : JsInstance
        {
            JsFunction function = new ClrImplDefinition<T>(impl);
            function.Prototype = Global.ObjectClass.New(function);
            function.Scope.Prototype = Prototype;
            return function;
        }
        public JsFunction New<T>(Func<T, JsInstance[], JsInstance> impl, int length) where T : JsInstance
        {
            JsFunction function = new ClrImplDefinition<T>(impl, length);
            function.Prototype = Global.ObjectClass.New(function);
            function.Scope.Prototype = Prototype;
            return function;
        }

        public JsFunction New(Delegate d)
        {
            JsFunction function = new ClrFunction(d);
            function.Prototype = Global.ObjectClass.New(function);
            function.Scope.Prototype = Prototype;
            return function;
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            JsFunction instance = New();

            instance.Arguments = new List<string>();

            for (int i = 0; i < parameters.Length - 1; i++)
            {
                string arg = parameters[i].ToString();

                foreach (string a in arg.Split(','))
                {
                    instance.Arguments.Add(a.Trim());
                }
            }

            if (parameters.Length >= 1)
            {
                Program p = JintEngine.Compile(parameters[parameters.Length - 1].Value.ToString(), visitor.DebugMode);
                instance.Statement = new BlockStatement() { Statements = p.Statements };
            }

            return visitor.Return(instance);
        }

        public JsInstance ToString2(JsDictionaryObject target, JsInstance[] parameters)
        {
            return Global.StringClass.New(target.ToString());
        }
    }
}
