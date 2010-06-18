using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    [Serializable]
    public class JsScope : JsDictionaryObject
    {
        private Descriptor thisDescriptor;
        private Descriptor argumentsDescriptor;

        public JsScope()
        {
            Prototype = JsUndefined.Instance;
        }

        public const string TYPEOF = "scope";

        public override string Class
        {
            get { return TYPEOF; }
        }

        public override object Value
        {
            get { return null; }
            set { }
        }

        public override bool HasOwnProperty(string key)
        {
            bool hasOwnProperty = base.HasOwnProperty(key);
            if (!hasOwnProperty && Extensible && Prototype.Class == JsScope.TYPEOF)
            {
                return Prototype.HasOwnProperty(key);
            }
            else
            {
                return hasOwnProperty;
            }
        }

        public override void DefineOwnProperty(string key, JsInstance value)
        {
            if (Extensible)
            {
                base.DefineOwnProperty(key, value);
            }
            else
            {
                Prototype.DefineOwnProperty(key, value);
            }
        }

        public override bool HasProperty(string key)
        {
            if (Prototype.Class != JsScope.TYPEOF && Prototype.Class != JsArguments.TYPEOF)
            {
                return HasOwnProperty(key);
            }

            return base.HasProperty(key);
        }

        public override JsInstance this[string index]
        {
            get
            {
                if (index == THIS && thisDescriptor != null)
                    return thisDescriptor.Get(this);
                if (index == ARGUMENTS && argumentsDescriptor != null)
                    return argumentsDescriptor.Get(this);
                return base[index];
            }
            set
            {
                if (Extensible)
                {
                    if (index == THIS)
                    {
                        if (thisDescriptor != null)
                            thisDescriptor.Set(this, value);
                        else
                        {
                            DefineOwnProperty(index, thisDescriptor = new ValueDescriptor(this, index, value));
                        }
                    }
                    if (index == ARGUMENTS)
                    {
                        if (argumentsDescriptor != null)
                            argumentsDescriptor.Set(this, value);
                        else
                        {
                            DefineOwnProperty(index, argumentsDescriptor = new ValueDescriptor(this, index, value));
                        }
                    }

                    base[index] = value;
                }
                else
                {
                    Prototype[index] = value;
                }
            }
        }
    }
}
