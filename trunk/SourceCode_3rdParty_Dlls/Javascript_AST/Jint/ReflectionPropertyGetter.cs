using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint
{
    public class ReflectionPropertyGetter : IPropertyGetter 
    {
        public PropertyInfo GetValue(object obj, string propertyName)
        {
            if (obj == null)
            {
                return null;
            }
            
            // Static evaluation
            bool isStaticCall = obj is Type;
            Type type = isStaticCall ? (Type)obj : obj.GetType();

            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty);

            if (propertyInfo == null)
            {
                throw new JintException("Property not found: " + propertyName + " in " + obj.GetType().ToString());
            }

            return propertyInfo;
        }

        public PropertyInfo GetValue(object obj, string propertyName, params object[] parameters)
        {
            if (obj == null)
            {
                return null;
            }

            // Static evaluation
            bool isStaticCall = obj is Type;
            Type type = isStaticCall ? (Type)obj : obj.GetType();

            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty);

            if (propertyInfo == null)
            {
                throw new JintException("Property not found: " + propertyName + " in " + obj.GetType().ToString());
            }

            return propertyInfo;
        }
    }
}
