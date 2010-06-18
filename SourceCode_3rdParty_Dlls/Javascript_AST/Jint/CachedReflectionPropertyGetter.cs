using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Jint
{
    public class CachedReflectionPropertyGetter : IPropertyGetter
    {
        IMethodInvoker methodInvoker;

        public CachedReflectionPropertyGetter(IMethodInvoker invoker)
        {
            methodInvoker = invoker;
        }

        Dictionary<Type, Dictionary<string, PropertyInfo>> _Cache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        #region IPropertyGetter Members

        public PropertyInfo GetValue(object obj, string propertyName, params object[] parameters)
        {
            if (obj == null)
            {
                return null;
            }

            PropertyInfo propertyInfo = null;

            // Static evaluation
            bool isStaticCall = obj is Type;
            Type type = isStaticCall ? (Type)obj : obj.GetType();

            if (_Cache.ContainsKey(type))
            {
                if (!_Cache[type].ContainsKey(propertyName))
                {
                    foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.GetProperty))
                    {
                        if (pi.Name != propertyName)
                            continue;
                        ParameterInfo[] propertyParameters = pi.GetIndexParameters();
                        if (propertyParameters == null || propertyParameters.Length == 0)
                        {
                            propertyInfo = pi;
                            if (propertyParameters.Length < parameters.Length)
                            {
                                object[] setValueParameter = new object[] { parameters[parameters.Length - 1] };
                                if (methodInvoker.TryGetAppropriateParameters(setValueParameter, new Type[] { propertyInfo.PropertyType }, obj))
                                    parameters[parameters.Length - 1] = setValueParameter[0];
                            }
                            break;
                        }
                        if (methodInvoker.TryGetAppropriateParameters(parameters, propertyParameters, obj))
                        {
                            if (propertyParameters.Length < parameters.Length)
                            {
                                object[] setValueParameter = new object[] { parameters[parameters.Length - 1] };
                                if (methodInvoker.TryGetAppropriateParameters(setValueParameter, new Type[] { pi.PropertyType }, obj))
                                {
                                    parameters[parameters.Length - 1] = setValueParameter[0];
                                    propertyInfo = pi;
                                    break;
                                }
                            }
                            else
                            {
                                propertyInfo = pi;
                                break;
                            }
                        }
                    }
                    _Cache[type].Add(propertyName, propertyInfo);
                }
                else
                {
                    propertyInfo = _Cache[type][propertyName];
                    if (propertyInfo != null)
                    {
                        ParameterInfo[] propertyParameters = propertyInfo.GetIndexParameters();
                        methodInvoker.GetAppropriateParameters(parameters, propertyParameters, obj);
                        if (parameters.Length > propertyParameters.Length)
                        {
                            object[] setValueParameter = new object[] { parameters[parameters.Length - 1] };
                            if (methodInvoker.TryGetAppropriateParameters(setValueParameter, new Type[] { propertyInfo.PropertyType }, obj))
                                parameters[parameters.Length - 1] = setValueParameter[0];
                        }
                    }
                }
            }
            else
            {
                _Cache.Add(type, new Dictionary<string, PropertyInfo>());
                propertyInfo = GetValue(obj, propertyName, parameters);
            }


            return propertyInfo;
        }

        #endregion
    }
}