using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Jint.Native;

namespace Jint
{
    class CachedConstructorInvoker : IConstructorInvoker
    {
        IMethodInvoker methodInvoker;

        public CachedConstructorInvoker(IMethodInvoker methodInvoker)
        {
            this.methodInvoker = methodInvoker;
        }

        Dictionary<Type, Dictionary<string, ConstructorInfo>> _Cache = new Dictionary<Type, Dictionary<string, ConstructorInfo>>();

        public string GetCacheKey(object[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object o in parameters)
            {
                sb.Append(o.GetType().FullName).Append(';');
            }

            return sb.ToString();
        }

        public ConstructorInfo Invoke(Type type, object[] parameters)
        {
            ConstructorInfo constructorInfo = null;
            string key = GetCacheKey(parameters);

            // Static evaluation

            if (!_Cache.ContainsKey(type))
            {
                _Cache.Add(type, new Dictionary<string, ConstructorInfo>());
            }

            if (!_Cache[type].ContainsKey(key))
            {
                List<ConstructorInfo> ms = new List<ConstructorInfo>();

                var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                foreach (var m in constructors)
                {
                    // check name of method and parameters number
                    if (m.GetParameters().Length == parameters.Length)
                    {
                        ms.Add(m);
                    }
                }

                foreach (var m in ms)
                {
                    ParameterInfo[] pis = m.GetParameters();

                    if (methodInvoker.TryGetAppropriateParameters(parameters, pis, null))
                    {
                        constructorInfo = m;
                        break;
                    }
                }

                if (constructorInfo != null)
                {
                    _Cache[type].Add(key, constructorInfo);
                }
            }
            else
            {
                constructorInfo = _Cache[type][key];
                methodInvoker.TryGetAppropriateParameters(parameters, constructorInfo.GetParameters(), null);
            }

            return constructorInfo;
        }
    }
}
