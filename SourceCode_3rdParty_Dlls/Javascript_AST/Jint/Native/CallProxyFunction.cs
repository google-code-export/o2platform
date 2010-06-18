using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class CallProxyFunction : JsFunction
    {
        public JsFunction Callee { get; set; }

        public CallProxyFunction(JsFunction callee)
        {
            Callee = callee;
        }

        public override JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            visitor.CallFunction(Callee, that, parameters);
            return visitor.Result;
        }
    }
}
