using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint
{
    public class CachedReflectionFieldGetter : IFieldGetter
    {
        IMethodInvoker methodInvoker;

        public CachedReflectionFieldGetter(IMethodInvoker methodInvoker)
        {
            this.methodInvoker = methodInvoker;
        }

        Dictionary<Type, Dictionary<string, FieldInfo>> _Cache = new Dictionary<Type, Dictionary<string, FieldInfo>>();

        public FieldInfo GetValue(object obj, string propertyName)
        {
            object value = null;
            return GetValue(obj, propertyName, ref value);
        }

        public FieldInfo GetValue(object obj, string propertyName, ref object value)
        {
            if (obj == null)
            {
                return null;
            }

            FieldInfo fieldInfo = null;

            // Static evaluation
            bool isStaticCall = obj is Type;
            Type type = isStaticCall ? (Type)obj : obj.GetType();

            if (_Cache.ContainsKey(type))
            {
                if (!_Cache[type].ContainsKey(propertyName))
                {
                    _Cache[type].Add(propertyName, fieldInfo = type.GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty));
                }
                else
                {
                    fieldInfo = _Cache[type][propertyName];
                }
            }
            else
            {
                _Cache.Add(type, new Dictionary<string, FieldInfo>());
                _Cache[type].Add(propertyName, fieldInfo = type.GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty));
            }

            if (value != null)
            {
                object[] setValueParameter = new object[] { value };
                if (methodInvoker.TryGetAppropriateParameters(setValueParameter, new Type[] { fieldInfo.FieldType }, obj))
                    value = setValueParameter[0];
            }


            return fieldInfo;
        }
    }
}