using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint
{
    public interface IPropertyGetter
    {
        PropertyInfo GetValue(object obj, string propertyName, params object[] parameters);
    }
}
