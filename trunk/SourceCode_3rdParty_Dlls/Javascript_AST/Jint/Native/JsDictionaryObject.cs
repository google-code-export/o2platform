using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Jint.Expressions;
using Jint.PropertyBags;

namespace Jint.Native
{

    [Serializable]
    public abstract class JsDictionaryObject : JsInstance, IEnumerable<KeyValuePair<string, JsInstance>>
    {
        protected internal IPropertyBag properties = new MiniCachedPropertyBag();

        public bool Extensible { get; set; }

        public JsDictionaryObject()
        {
            Extensible = true;
            DefineOwnProperty(PROTOTYPE, prototypeDescriptor = new ValueDescriptor(this, PROTOTYPE, JsUndefined.Instance), PropertyAttributes.DontEnum);
        }

        public JsDictionaryObject Prototype
        {
            get
            {
                return this[PROTOTYPE] as JsDictionaryObject;
            }
            set
            {
                this[PROTOTYPE] = value;
            }
        }

        public virtual bool HasProperty(string key)
        {
            JsDictionaryObject obj = this;
            while (true)
            {
                if (obj.HasOwnProperty(key))
                {
                    return true;
                }

                obj = obj.Prototype;

                if (obj == JsUndefined.Instance || obj == JsNull.Instance)
                {
                    return false;
                }
            }
        }

        public virtual bool HasOwnProperty(string key)
        {
            Descriptor desc;
            return properties.TryGet(key, out desc);
        }

        protected internal int length = int.MinValue;

        public virtual int Length
        {
            //length minus prototype
            get
            {
                if (length == int.MinValue)
                {
                    return properties.Count - 1;
                }

                return length;
            }
            set
            {
                length = value;
            }
        }

        public bool HasProperty(JsInstance key)
        {
            return this.HasProperty(key.ToString());
        }

        public bool HasOwnProperty(JsInstance key)
        {
            return this.HasOwnProperty(key.ToString());
        }

        public JsInstance this[JsInstance key]
        {
            get { return this[key.ToString()]; }
            set { this[key.ToString()] = value; }
        }

        protected Descriptor prototypeDescriptor;
        protected Descriptor constructorDescriptor;

        public virtual Descriptor GetDescriptor(string index)
        {
            if (index == PROTOTYPE)
            {
                return prototypeDescriptor;
            }

            if (index == CONSTRUCTOR && constructorDescriptor != null)
            {
                return constructorDescriptor;
            }

            Descriptor result;
            if (properties.TryGet(index, out result))
            {
                if (index == CONSTRUCTOR)
                    constructorDescriptor = result;
                return result;
            }

            JsDictionaryObject prototype = Prototype;
            if (prototype != JsUndefined.Instance && prototype != JsNull.Instance)
            {
                result = Prototype.GetDescriptor(index);
                if (result != null)
                    return result;

                if (index != CONSTRUCTOR)
                {
                    Descriptor ctorDescriptor = GetDescriptor(CONSTRUCTOR);
                    if (ctorDescriptor != null)
                    {
                        JsInstance ctor = ctorDescriptor.Get(this);
                        if (ctor != JsUndefined.Instance && ctor != JsNull.Instance)
                            return ((JsFunction)ctor).Scope.GetDescriptor(index);
                    }
                }

                return result;
            }
            else
                return null;
        }

        public bool TryGetDescriptor(string index, out Descriptor result)
        {
            result = GetDescriptor(index);
            return result != null;
        }

        public bool TryGetProperty(string index, out JsInstance result)
        {
            Descriptor d = GetDescriptor(index);
            if (d == null)
            {
                result = JsUndefined.Instance;
                return false;
            }

            result = d.Get(this);

            return true;
        }

        public virtual JsInstance this[string index]
        {
            get
            {
                Descriptor d = GetDescriptor(index);
                return d != null ? d.Get(this) : JsUndefined.Instance;
            }
            set
            {
                Descriptor d = GetDescriptor(index);
                if (d == null || (d.DescriptorType == DescriptorType.Value && d.Owner != this))
                {
                    bool enumerable = index != PROTOTYPE;

                    if (index == PROTOTYPE)
                    {
                        prototypeDescriptor.Set(this, value);
                    }
                    else
                    {
                        if (d == null)
                            properties.Put(index, new ValueDescriptor(this, index, value) { Enumerable = enumerable });
                        else
                            d.Set(this, value);
                    }
                }
                else
                {
                    d.Set(this, value);
                }
            }
        }

        public void Delete(JsInstance key)
        {
            Delete(key.ToString());
        }

        public void Delete(string index)
        {
            Descriptor d = null;
            if (TryGetDescriptor(index, out d))
            {
                if (d.Configurable)
                {
                //d.Delete();
                    properties.Delete(index);

                if (length > -1)
                    length--;
                }
                else
                {
                    throw new JintException();
                }
            }
        }

        public void DefineOwnProperty(string key, JsInstance value, PropertyAttributes propertyAttributes)
        {
            DefineOwnProperty(key, new ValueDescriptor(this, key, value) { Writable = (propertyAttributes & PropertyAttributes.ReadOnly) == 0, Enumerable = (propertyAttributes & PropertyAttributes.DontEnum) == 0 });
        }

        public virtual void DefineOwnProperty(string key, JsInstance value)
        {
            if (value != null && value.Class == Descriptor.TYPEOF)
            {
                DefineOwnProperty(key, (Descriptor)value);
            }
            else
            {
                DefineOwnProperty(key, new ValueDescriptor(this, key, value));
            }
        }

        public void DefineOwnProperty(string key, Descriptor currentDescriptor)
        {
            Descriptor desc;
            if (properties.TryGet(key, out desc))
            {
                switch (desc.DescriptorType)
                {
                    case DescriptorType.Value:
                        switch (currentDescriptor.DescriptorType)
                        {
                            case DescriptorType.Value:
                                properties.Get(key).Set(this, currentDescriptor.Get(this));
                                break;
                            case DescriptorType.Accessor:
                                properties.Delete(key);
                                properties.Put(key, currentDescriptor);
                                break;
                            case DescriptorType.Clr:
                                throw new NotSupportedException();
                            default:
                                break;
                        }
                        break;
                    case DescriptorType.Accessor:
                        PropertyDescriptor propDesc = (PropertyDescriptor)desc;
                        if (currentDescriptor.DescriptorType == DescriptorType.Accessor)
                        {
                            propDesc.GetFunction = ((PropertyDescriptor)currentDescriptor).GetFunction ?? propDesc.GetFunction;
                            propDesc.SetFunction = ((PropertyDescriptor)currentDescriptor).SetFunction ?? propDesc.SetFunction;
                        }
                        else
                            propDesc.Set(this, currentDescriptor.Get(this));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                properties.Put(key, currentDescriptor);
            }
        }

        #region IEnumerable<KeyValuePair<JsInstance,JsInstance>> Members

        public IEnumerator<KeyValuePair<string, JsInstance>> GetEnumerator()
        {
            foreach (KeyValuePair<string, Descriptor> descriptor in properties)
            {
                if (descriptor.Value.Enumerable)
                    yield return new KeyValuePair<string, JsInstance>(descriptor.Key, descriptor.Value.Get(this));
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        #endregion

        public IEnumerable<JsInstance> GetValues()
        {
            foreach (Descriptor descriptor in properties.Values)
            {
                if (descriptor.Enumerable)
                    yield return descriptor.Get(this);
            }
            yield break;
        }

        public virtual IEnumerable<string> GetKeys()
        {
            var p = Prototype;

            if (p != JsUndefined.Instance && p != JsNull.Instance && p != null)
            {
                foreach (string key in p.GetKeys())
                {
                    if (!HasOwnProperty(key))
                        yield return key;
                }
            }

            foreach (KeyValuePair<string, Descriptor> descriptor in properties)
            {
                if (descriptor.Value.Enumerable)
                    yield return descriptor.Key;
            }
            yield break;
        }

        /// <summary>
        /// non standard
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="p"></param>
        /// <param name="currentDescriptor"></param>
        public JsInstance GetGetFunction(JsDictionaryObject target, JsInstance[] parameters)
        {
            if (parameters.Length <= 0)
            {
                throw new ArgumentException("propertyName");
            }

            if (!target.HasOwnProperty(parameters[0].ToString()))
            {
                return GetGetFunction(target.Prototype, parameters);
            }

            PropertyDescriptor desc = target.properties.Get(parameters[0].ToString()) as PropertyDescriptor;
            if (desc == null)
            {
                return JsUndefined.Instance;
            }

            return (JsInstance)desc.GetFunction ?? JsUndefined.Instance;

        }

        /// <summary>
        /// non standard
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="p"></param>
        /// <param name="currentDescriptor"></param>
        public JsInstance GetSetFunction(JsDictionaryObject target, JsInstance[] parameters)
        {
            if (parameters.Length <= 0)
            {
                throw new ArgumentException("propertyName");
            }

            if (!target.HasOwnProperty(parameters[0].ToString()))
            {
                return GetSetFunction(target.Prototype, parameters);
            }

            PropertyDescriptor desc = target.properties.Get(parameters[0].ToString()) as PropertyDescriptor;
            if (desc == null)
            {
                return JsUndefined.Instance;
            }

            return (JsInstance)desc.SetFunction ?? JsUndefined.Instance;

        }

        public override object Call(IJintVisitor visitor, string function, params JsInstance[] parameters)
        {
            visitor.ExecuteFunction(this[function] as JsFunction, this, parameters);
            return visitor.Returned;
        }

        public override bool IsClr
        {
            get { return false; }
        }

        public bool IsPrototypeOf(JsDictionaryObject target)
        {
            if (target == null)
                return false;
            if (target == JsUndefined.Instance || target == JsNull.Instance)
                return false;
            if (target.Prototype == this)
                return true;
            return IsPrototypeOf(target.Prototype);
        }
    }
}
