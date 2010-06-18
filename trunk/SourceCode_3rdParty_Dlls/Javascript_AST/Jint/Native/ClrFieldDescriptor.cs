using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    public class ClrFieldDescriptor : Descriptor
    {
        IGlobal global;
        IFieldGetter getter;

        public ClrFieldDescriptor(IFieldGetter getter, IGlobal global, JsDictionaryObject owner, string propertyName)
            : base(owner, propertyName)
        {
            this.global = global;
            this.getter = getter;
        }

        public override JsInstance Get(JsDictionaryObject that)
        {
            object value = getter.GetValue(that.Value, Name).GetValue(that.Value);
            return global.Visitor.Return(global.WrapClr(value));
        }

        public override void Set(JsDictionaryObject that, JsInstance value)
        {
            object nativeValue = JsClr.ConvertParameter(value);
            getter.GetValue(that.Value, Name, ref nativeValue).SetValue(that.Value, nativeValue);
        }

        internal override DescriptorType DescriptorType
        {
            get { return DescriptorType.Clr; }
        }
    }

}
