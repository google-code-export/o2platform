using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Native
{
    [Serializable]
    public abstract class JsConstructor : JsFunction
    {
        public IGlobal Global { get; set; }

        public JsConstructor(IGlobal global)
        {
            Global = global;
            if (global.ObjectClass != null)
                Prototype = global.ObjectClass.New(this);
        }

        public abstract void InitPrototype(IGlobal global);
    }
}
