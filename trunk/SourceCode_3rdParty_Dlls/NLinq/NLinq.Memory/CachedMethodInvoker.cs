using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Evaluant.NLinq.Memory
{
    class CachedMethodInvoker: IMethodInvoker
    {
        static Dictionary<Type, Dictionary<string, MethodInfo>> _Cache = new Dictionary<Type, Dictionary<string, MethodInfo>>();

        public bool Invoke(object subject, string method, object[] parameters, ref object result)
        {
            Type[] types = new Type[parameters.Length]; 
            
            for (int j = 0; j < parameters.Length; j++)
            {
                types[j] = parameters[j].GetType();
            }

            lock (_Cache)
            {
                MethodInfo methodInfo = null;

                Type type = subject.GetType();

                if (_Cache.ContainsKey(type))
                {
                    if (!_Cache[type].ContainsKey(method))
                    {
                        _Cache[type].Add(method, methodInfo = type.GetMethod(method, types));
                    }
                    else
                    {
                        methodInfo = _Cache[type][method];
                    }
                }
                else
                {
                    _Cache.Add(type, new Dictionary<string, MethodInfo>());
                    _Cache[type].Add(method, methodInfo = type.GetMethod(method, types));
                }

                if (methodInfo != null)
                {
                    result = methodInfo.Invoke(subject, parameters);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
