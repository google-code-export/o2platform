using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Evaluant.NLinq.Memory
{
    public class CachedReflectionPropertyGetter : IPropertyGetter 
    {
        static Dictionary<Type, Dictionary<string, PropertyInfo>> _Cache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        public object GetValue(object obj, string propertyName)
        {
            lock (_Cache)
            {
                PropertyInfo propertyInfo = null;

                Type type = obj.GetType();

                if (_Cache.ContainsKey(type))
                {
                    if (!_Cache[type].ContainsKey(propertyName))
                    {
                        _Cache[type].Add(propertyName, propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty));
                    }
                    else
                    {
                        propertyInfo = _Cache[type][propertyName];
                    }
                }
                else
                {
                    _Cache.Add(type, new Dictionary<string, PropertyInfo>());
                    _Cache[type].Add(propertyName, propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty));
                }

                return propertyInfo.GetValue(obj, null);
            }
        }
    }
}
