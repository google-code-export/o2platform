using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    internal enum DescriptorType
    {
        Value,
        Accessor,
        Clr
    }

    [Serializable]
    public abstract class Descriptor : JsInstance
    {
        public Descriptor(JsDictionaryObject owner, string name)
        {
            this.Owner = owner;
            Name = name;
        }

        public string Name { get; set; }

        public bool Enumerable { get; set; }
        public bool Configurable { get; set; }
        public bool Writable { get; set; }
        public JsDictionaryObject Owner { get; set; }

        public void Delete()
        {
            if (!Configurable)
                throw new JintException();
        }

        public override bool IsClr
        {
            get { return false; }
        }

        public abstract JsInstance Get(JsDictionaryObject that);
        public abstract void Set(JsDictionaryObject that, JsInstance value);

        internal abstract DescriptorType DescriptorType { get; }

        /// <summary>
        /// 8.10.5
        /// </summary>
        /// <param name="global"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static Descriptor ToPropertyDesciptor(IGlobal global, JsDictionaryObject owner, string name, JsInstance jsInstance)
        {
            if (jsInstance.Class != JsObject.TYPEOF)
            {
                throw new JsException(global.TypeErrorClass.New("The target object has to be an instance of an object"));
            }

            JsObject obj = (JsObject)jsInstance;
            if ((obj.HasProperty("value") || obj.HasProperty("writable")) && (obj.HasProperty("set") || obj.HasProperty("get")))
            {
                throw new JsException(global.TypeErrorClass.New("The property cannot be both writable and have get/set accessors or cannot have both a value and an accessor defined"));
            }

            Descriptor desc;
            JsInstance result = null;

            if (obj.HasProperty("value"))
            {
                desc = new ValueDescriptor(owner, name, obj["value"]);
            }
            else
            {
                desc = new PropertyDescriptor(global, owner, name);
            }

            if (obj.TryGetProperty("enumerable", out result))
            {
                desc.Enumerable = result.ToBoolean();
            }

            if (obj.TryGetProperty("configurable", out result))
            {
                desc.Configurable = result.ToBoolean();
            }

            if (obj.TryGetProperty("writable", out result))
            {
                desc.Writable = result.ToBoolean();
            }

            if (obj.TryGetProperty("get", out result))
            {
                if (result.Class != JsFunction.TYPEOF)
                {
                    throw new JsException(global.TypeErrorClass.New("The getter has to be a function"));
                }

                ((PropertyDescriptor)desc).GetFunction = (JsFunction)result;
            }

            if (obj.TryGetProperty("set", out result))
            {
                if (result.Class != JsFunction.TYPEOF)
                {
                    throw new JsException(global.TypeErrorClass.New("The setter has to be a function"));
                }

                ((PropertyDescriptor)desc).SetFunction = (JsFunction)result;
            }

            return desc;
        }

        public override object Value
        {
            get
            {
                throw new NotSupportedException();
                //return Get();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public static readonly string TYPEOF = "descriptor";

        public override string Class
        {
            get { return TYPEOF; }
        }
    }
}
