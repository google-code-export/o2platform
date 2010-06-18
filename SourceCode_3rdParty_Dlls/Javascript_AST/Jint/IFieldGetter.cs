using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint
{
    public interface IFieldGetter
    {
        FieldInfo GetValue(object obj, string propertyName);
        FieldInfo GetValue(object obj, string propertyName, ref object value);
    }
}
