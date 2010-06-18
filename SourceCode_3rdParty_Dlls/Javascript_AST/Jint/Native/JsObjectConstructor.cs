using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class JsObjectConstructor : JsConstructor
    {
        public JsObjectConstructor(IGlobal global)
            : base(global)
        {
            Name = "Object";
            Prototype = new JsObject();
        }

        public override void InitPrototype(IGlobal global)
        {
            Prototype.DefineOwnProperty("constructor", this, PropertyAttributes.DontEnum);
            //Prototype.DefineOwnProperty("length", new PropertyDescriptor<JsObject>(global, GetLengthImpl, SetLengthImpl));

            Prototype.DefineOwnProperty("toString", global.FunctionClass.New<JsDictionaryObject>(ToStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("toLocaleString", global.FunctionClass.New<JsDictionaryObject>(ToStringImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("valueOf", global.FunctionClass.New<JsDictionaryObject>(ValueOfImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("hasOwnProperty", global.FunctionClass.New<JsDictionaryObject>(HasOwnPropertyImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("isPrototypeOf", global.FunctionClass.New<JsDictionaryObject>(IsPrototypeOfImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("propertyIsEnumerable", global.FunctionClass.New<JsDictionaryObject>(PropertyIsEnumerableImpl), PropertyAttributes.DontEnum);
            Prototype.DefineOwnProperty("getPrototypeOf", new JsFunctionWrapper(GetPrototypeOfImpl), PropertyAttributes.DontEnum);
            if (global.HasOption(Options.Ecmascript5))
            {
                Prototype.DefineOwnProperty("defineProperty", new JsFunctionWrapper(DefineProperty), PropertyAttributes.DontEnum);
                Prototype.DefineOwnProperty("__lookupGetter__", global.FunctionClass.New<JsDictionaryObject>(GetGetFunction), PropertyAttributes.DontEnum);
                Prototype.DefineOwnProperty("__lookupSetter__", global.FunctionClass.New<JsDictionaryObject>(GetSetFunction), PropertyAttributes.DontEnum);
            }
        }

        public JsObject New(JsFunction constructor)
        {
            JsObject obj = new JsObject() { Prototype = this.Prototype };
            obj.DefineOwnProperty(CONSTRUCTOR, new ValueDescriptor(obj, CONSTRUCTOR, constructor) { Enumerable = false });
            return obj;
        }

        public JsObject New(object value)
        {
            return new JsObject(value) { Prototype = this.Prototype };
        }

        public JsObject New()
        {
            return New((object)null);
        }

        /// <summary>
        /// 15.2.2.1
        /// </summary>
        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            if (parameters.Length > 0)
            {
                switch (parameters[0].Class)
                {
                    case JsString.TYPEOF: return Global.StringClass.New(parameters[0].ToString());
                    case JsNumber.TYPEOF: return new JsNumber(parameters[0].ToNumber());
                    case JsBoolean.TYPEOF: return new JsBoolean(parameters[0].ToBoolean());
                    default:
                        return parameters[0];
                }
            }

            return New(this);
        }

        // 15.2.4.3 and 15.2.4.4
        public JsInstance ToStringImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            JsFunction constructor = target.Prototype["constructor"] as JsFunction;

            if (target.Class == JsFunction.TYPEOF)
                return Global.StringClass.New(String.Concat("[object Function]"));

            if (constructor == null)
            {
                return Global.StringClass.New(String.Concat("[object Object]"));
            }
            else
            {
                return Global.StringClass.New(String.Concat("[object ", constructor.Name, "]"));
            }


        }

        // 15.2.4.4
        public JsInstance ValueOfImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            return target;
        }

        // 15.2.4.5
        public JsInstance HasOwnPropertyImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            return Global.BooleanClass.New(target.HasOwnProperty(parameters[0]));
        }

        // 15.2.4.6
        public JsInstance IsPrototypeOfImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            if (target.Class != JsObject.TYPEOF)
            {
                return JsBoolean.False;
            }

            while (true)
            {
                target = target.Prototype;
                if (target == null)
                {
                    return JsBoolean.True;
                }

                if (target == this)
                {
                    return JsBoolean.True;
                }
            }
        }

        // 15.2.4.7
        public JsInstance PropertyIsEnumerableImpl(JsDictionaryObject target, JsInstance[] parameters)
        {
            if (!HasOwnProperty(parameters[0]))
            {
                return JsBoolean.False;
            }

            var v = target[parameters[0]];

            return new JsBoolean((v.Attributes & PropertyAttributes.DontEnum) == PropertyAttributes.None);
        }

        /// <summary>
        /// 15.2.3.2
        /// </summary>
        /// <returns></returns>
        public JsInstance GetPrototypeOfImpl(JsInstance[] parameters)
        {
            if (parameters[0].Class != JsObject.TYPEOF)
                throw new JsException(Global.TypeErrorClass.New());
            return ((JsObject)parameters[0]).Prototype;
        }

        public override string ToString()
        {
            return JsObject.TYPEOF;
        }

        /// <summary>
        /// 15.2.3.6
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="p"></param>
        /// <param name="currentDescriptor"></param>
        public JsInstance DefineProperty(JsInstance[] parameters)
        {
            JsInstance instance = parameters[0];

            if (!(instance is JsDictionaryObject))
                throw new JsException(Global.TypeErrorClass.New());

            string name = parameters[1].ToString();
            Descriptor desc = Descriptor.ToPropertyDesciptor(Global, (JsDictionaryObject)instance, name, parameters[2]);
            if (desc.DescriptorType == DescriptorType.Accessor)
            {
                if (((PropertyDescriptor)desc).GetFunction != null)
                    ((PropertyDescriptor)desc).GetFunction.Scope[JsInstance.THIS] = instance;
                if (((PropertyDescriptor)desc).SetFunction != null)
                    ((PropertyDescriptor)desc).SetFunction.Scope[JsInstance.THIS] = instance;
            }
            ((JsDictionaryObject)instance).DefineOwnProperty(name, desc);

            return instance;
        }
    }
}