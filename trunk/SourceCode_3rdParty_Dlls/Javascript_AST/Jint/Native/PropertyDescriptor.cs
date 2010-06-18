﻿using System;
using System.Collections.Generic;
using System.Text;
using Jint.Delegates;

namespace Jint.Native
{
    [Serializable]
    public class PropertyDescriptor : Descriptor
    {
        public PropertyDescriptor(IGlobal global, JsDictionaryObject owner, string name)
            : base(owner, name)
        {
            this.global = global;
            Enumerable = false;
        }

        private IGlobal global;

        public JsFunction GetFunction { get; set; }
        public JsFunction SetFunction { get; set; }

        public override JsInstance Get(JsDictionaryObject that)
        {
            //JsDictionaryObject that = global.Visitor.CallTarget;
            global.Visitor.ExecuteFunction(GetFunction, that, JsInstance.EMPTY);
            return global.Visitor.Returned;
        }

        public override void Set(JsDictionaryObject that, JsInstance value)
        {
            if (SetFunction == null)
                throw new JsException(global.TypeErrorClass.New());
            //JsDictionaryObject that = global.Visitor.CallTarget;
            global.Visitor.ExecuteFunction(SetFunction, that, new JsInstance[] { value });
        }

        internal override DescriptorType DescriptorType
        {
            get { return DescriptorType.Accessor; }
        }
    }

    [Serializable]
    public class PropertyDescriptor<T> : PropertyDescriptor
        where T : JsInstance
    {
        public PropertyDescriptor(IGlobal global, JsDictionaryObject owner, string name, Func<T, JsInstance> get)
            : base(global, owner, name)
        {
            GetFunction = global.FunctionClass.New<T>(get);
            GetFunction.Scope[JsInstance.THIS] = null;
        }

        public PropertyDescriptor(IGlobal global, JsDictionaryObject owner, string name, Func<T, JsInstance> get, Func<T, JsInstance[], JsInstance> set)
            : this(global, owner, name, get)
        {
            SetFunction = global.FunctionClass.New<T>(set);
            SetFunction.Scope[JsInstance.THIS] = null;
        }
    }
}
