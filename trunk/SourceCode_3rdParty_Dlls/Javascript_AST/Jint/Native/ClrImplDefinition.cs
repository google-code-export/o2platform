using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;
using Jint.Delegates;
using System.Reflection;

namespace Jint.Native
{
    /// <summary>
    /// Wraps a delegate which returns a value (a getter)
    /// </summary>
    [Serializable]
    public class ClrImplDefinition<T> : JsFunction
        where T : JsInstance
    {
        Delegate impl;
        private int length;
        bool hasParameters;

        private ClrImplDefinition(bool hasParameters)
        {
            this.hasParameters = hasParameters;
        }

        public ClrImplDefinition(Func<T, JsInstance[], JsInstance> impl)
            : this(impl, -1)
        {
        }

        public ClrImplDefinition(Func<T, JsInstance[], JsInstance> impl, int length)
            : this(true)
        {
            this.impl = impl;
            this.length = length;
        }

        public ClrImplDefinition(Func<T, JsInstance> impl)
            : this(impl, -1)
        {
        }

        public ClrImplDefinition(Func<T, JsInstance> impl, int length)
            : this(false)
        {
            this.impl = impl;
            this.length = length;
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            try
            {
                JsInstance result;
                if (hasParameters)
                    result = impl.DynamicInvoke(new object[] { that, parameters }) as JsInstance;
                else
                    result = impl.DynamicInvoke(new object[] { that }) as JsInstance;

                visitor.Return(result);
                return result;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
            catch (ArgumentException e)
            {
                var constructor = that.Prototype["constructor"] as JsFunction;
                throw new JsException(visitor.Global.TypeErrorClass.New("incompatible type: " + constructor == null ? "" : constructor.Name));
            }
            catch (Exception e)
            {
                if (e.InnerException is JsException)
                {
                    throw e.InnerException;
                }

                throw;
            }
        }

        public override int Length
        {
            get
            {
                if (length == -1)
                    return base.Length;
                return length;
            }
        }

        public override string ToString()
        {
            return String.Format("function {0}() { [native code] }", impl.Method.Name);
        }

    }
}
