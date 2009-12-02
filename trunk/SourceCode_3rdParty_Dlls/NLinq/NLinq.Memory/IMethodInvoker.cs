using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Memory
{
    public interface IMethodInvoker
    {
        bool Invoke(object subject, string method, object[] parameters, ref object result);
    }
}
