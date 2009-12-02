using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Memory
{
    public interface IPropertyGetter
    {
        object GetValue(object obj, string propertyName);
    }
}
