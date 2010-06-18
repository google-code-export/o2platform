﻿using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;
using Jint.Delegates;

namespace Jint.Native
{
    [Serializable]
    public class JsFunctionWrapper : JsFunction
    {
        public Func<JsInstance[] ,JsInstance> Delegate { get; set; }

        public JsFunctionWrapper(Func<JsInstance[], JsInstance> d)
        {
            Delegate = d;
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            try
            {
                //visitor.CurrentScope["this"] = visitor.Global;
                JsInstance result = Delegate.DynamicInvoke(new object[] { parameters }) as JsInstance;
                visitor.Return(result == null ? JsUndefined.Instance : result);

                return that;
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

        public override string ToString()
        {
            return String.Format("function {0}() {{ [native code] }}", Delegate.Method.Name);
        }
    }
}
