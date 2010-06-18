using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint.Native
{
    public class ClrMethodDescriptor : Descriptor
    {
        public ClrMethodDescriptor(JsDictionaryObject owner, string name)
            : base(owner, name)
        {

        }

        public override JsInstance Get(JsDictionaryObject that)
        {
            return new JsClrMethodInfo(Name);
        }

        public override void Set(JsDictionaryObject that, JsInstance value)
        {
            throw new NotImplementedException();
        }

        internal override DescriptorType DescriptorType
        {
            get { return DescriptorType.Clr; }
        }
    }
}
