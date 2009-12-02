using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Evaluant.NLinq.Memory
{
    public class ReflectionPropertyGetter : IPropertyGetter 
    {
        public object GetValue(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty);

            if (propertyInfo == null)
            {
                throw new NLinqException("Property not found: " + propertyName + " in " + obj.GetType().ToString());
            }

            return propertyInfo.GetValue(obj, null);
        }
    }
}
