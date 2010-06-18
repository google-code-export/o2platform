using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Jint.Native;
using System.Reflection.Emit;

namespace Jint
{
    class CachedMethodInvoker : IMethodInvoker
    {
        private ExecutionVisitor visitor;

        Dictionary<string, Dictionary<string, MethodInfo>> cache = new Dictionary<string, Dictionary<string, MethodInfo>>();

        public CachedMethodInvoker(ExecutionVisitor visitor)
        {
            this.visitor = visitor;
        }

        public string GetCacheKey(string method, object[] parameters, Type[] generics)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(method).Append(';');
            foreach (object o in parameters)
            {
                if (o != null)
                {
                    sb.Append(o.GetType().FullName).Append(';');
                }
                else
                {
                    sb.Append("null").Append(';');
                }
            }
            if (generics.Length > 0)
            {
                sb.Append("<");
                foreach (Type o in generics)
                {
                    sb.Append(o.FullName);
                }
                sb.Append("]");
            }

            return sb.ToString();
        }

        public MethodInfo Invoke(object subject, string method, object[] parameters, Type[] generics)
        {
            MethodInfo methodInfo = null;
            string key = GetCacheKey(method, parameters, generics);

            // Static evaluation
            bool isStaticCall = subject is Type;
            Type type = isStaticCall ? (Type)subject : subject.GetType();

            if (!cache.ContainsKey(type.FullName))
            {
                cache.Add(type.FullName, new Dictionary<string, MethodInfo>());
            }

            if (!cache[type.FullName].ContainsKey(key))
            {
                List<MethodInfo> ms = new List<MethodInfo>();

                foreach (var m in isStaticCall ? type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy) : type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
                {
                    // check name of method and parameters number
                    if (m.Name == method && m.GetParameters().Length == parameters.Length)
                    {

                        // is the method is generic ? 
                        if (m.GetGenericArguments().Length > 0)
                        {
                            // generics can be empty is the type can be inferred from the parameters
                            ms.Add(m.MakeGenericMethod(generics));
                        }
                        else
                        {
                            ms.Add(m);
                        }

                    }

                }

                #region Search exact parameter types
                foreach (var m in ms)
                {
                    ParameterInfo[] pis = m.GetParameters();

                    bool compatible = true;
                    // check types compatibility
                    for (int i = 0; i < pis.Length; i++)
                    {
                        // polymorphic 
                        if (parameters[i] != null && pis[i].ParameterType == parameters[i].GetType())
                        {
                            continue;
                        }
                        else if (parameters[i] == null && !pis[i].ParameterType.IsValueType)
                        {
                            continue;
                        }
                        else
                        {
                            compatible = false;
                            break;
                        }
                    }

                    if (!compatible)
                    {
                        continue;
                    }

                    methodInfo = m;
                    break;
                }
                #endregion


                if (methodInfo == null)
                {
                    #region Search compatible parameter types
                    foreach (var m in ms)
                    {
                        ParameterInfo[] pis = m.GetParameters();

                        bool compatible = true;
                        // check types compatibility
                        for (int i = 0; i < pis.Length; i++)
                        {
                            // plymorphic 
                            if (parameters[i] != null && pis[i].ParameterType.IsAssignableFrom(parameters[i].GetType()))
                            {
                                continue;
                            }
                            else if (parameters[i] == null && pis[i].ParameterType.IsByRef)
                            {
                                continue;
                            }
                            else
                            {
                                compatible = false;
                                break;
                            }
                        }

                        if (!compatible)
                        {
                            continue;
                        }

                        methodInfo = m;
                        break;
                    }
                    #endregion
                }

                if (methodInfo == null)
                {
                    #region Search matching parameter types
                    foreach (var m in ms)
                    {
                        ParameterInfo[] pis = m.GetParameters();

                        bool compatible = TryGetAppropriateParameters(parameters, pis, subject);

                        if (!compatible)
                        {
                            continue;
                        }

                        methodInfo = m;
                        break;
                    }
                    #endregion
                }

                if (methodInfo != null)
                {
                    cache[type.FullName].Add(key, methodInfo);
                }
            }
            else
            {
                methodInfo = cache[type.FullName][key];
                ParameterInfo[] pis = methodInfo.GetParameters();

                GetAppropriateParameters(parameters, pis, subject);

            }

            return methodInfo;
        }

        public void GetAppropriateParameters(object[] parameters, Type[] pis, object subject)
        {
            if (!TryGetAppropriateParameters(parameters, pis, subject))
                throw new JintException("Could not get appropriate parameters");
        }

        public void GetAppropriateParameters(object[] parameters, ParameterInfo[] pis, object subject)
        {
            if (pis == null)
                return;
            GetAppropriateParameters(parameters, ConvertParameterInfos(pis), subject);
        }

        public static Type[] ConvertParameterInfos(ParameterInfo[] pis)
        {
            Type[] ps = new Type[pis.Length];
            for (int i = 0; i < pis.Length; i++)
            {
                ps[i] = pis[i].ParameterType;
            }

            return ps;
        }

        public bool TryGetAppropriateParameters(object[] parameters, ParameterInfo[] pis, object subject)
        {
            if (pis == null)
                return true;
            return TryGetAppropriateParameters(parameters, ConvertParameterInfos(pis), subject);
        }

        public bool TryGetAppropriateParameters(object[] parameters, Type[] pis, object subject)
        {
            bool compatible = true;
            for (int i = 0; i < pis.Length; i++)
            {
                // plymorphic 
                if (parameters[i] != null && pis[i].IsInstanceOfType(parameters[i]))
                {
                    continue;
                }
                else if (parameters[i] == null && !pis[i].IsValueType)
                {
                    continue;
                }

                try
                {
                    if (pis[i].IsArray)
                    {
                        // try to convert every elements
                        JsArray array = parameters[i] as JsArray;

                        if (array == null)
                        {
                            compatible = false;
                            break;
                        }

                        Array newArray = Array.CreateInstance(pis[i], array.Length);

                        for (int k = 0; k < array.Length; k++)
                        {
                            newArray.SetValue(array[k.ToString()], k);
                        }
                    }
                    else if (typeof(Delegate).IsAssignableFrom(pis[i])) // wrap the JsFunction to a Delegate
                    {
                        DelegateWrapper dw = new DelegateWrapper();
                        dw.Visitor = visitor;
                        dw.Function = (JsFunction)parameters[i];
                        dw.That = visitor.Global.WrapClr(subject);

                        DynamicMethod dm = DelegateWrapper.GenerateDynamicMethod(pis[i]); // void (int)

                        parameters[i] = dm.CreateDelegate(pis[i], dw);
                    }
                    else if (pis[i].IsEnum && parameters[i] is string)
                    {
                        string[] enumNames = Enum.GetNames(pis[i]);
                        foreach (string name in ((string)parameters[i]).Split(' '))
                        {
                            if (Array.IndexOf(enumNames, name) < 0)
                            {
                                compatible = false;
                                break;
                            }
                        }
                        if (!compatible)
                            break;
                        parameters[i] = Enum.Parse(pis[i], (string)parameters[i]);
                    }
                    else
                    {
                        parameters[i] = Convert.ChangeType(parameters[i], pis[i]);
                    }
                }
                catch
                {
                    compatible = false;
                    break;
                }
            }
            return compatible;
        }
    }
}
