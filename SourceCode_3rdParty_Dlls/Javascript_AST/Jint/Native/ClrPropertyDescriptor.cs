using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    public class ClrPropertyDescriptor : Descriptor
    {
        IGlobal global;
        IPropertyGetter getter;

        public ClrPropertyDescriptor(IPropertyGetter getter, IGlobal global, JsDictionaryObject owner, string propertyName)
            : base(owner, propertyName)
        {
            this.global = global;
            this.getter = getter;
        }

        public override JsInstance Get(JsDictionaryObject that)
        {
            object value = getter.GetValue(that.Value, Name).GetValue(that.Value, null);
            return global.Visitor.Return(global.WrapClr(value));
        }

        public override void Set(JsDictionaryObject that, JsInstance value)
        {
            object[] nativeValue = JsClr.ConvertParameters(value);
            getter.GetValue(that.Value, Name, nativeValue).SetValue(that.Value, nativeValue[0], null);
        }

        internal override DescriptorType DescriptorType
        {
            get { return DescriptorType.Clr; }
        }
    }
}
