using System;
using System.Collections.Generic;
using System.Text;
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.DotNetWrappers.DotNet
{
    public static class Reflection_ExtensionMethods
    {
        static IReflection reflection =  PublicDI.reflection;

        public static object ctor(this string className, string assembly, params object[] parameters)
        {
            var obj = reflection.createObject(assembly, className, parameters);
            if (obj == null)
                PublicDI.log.error("in ctor, could not created object: {0}!{1}", assembly, className);
            else
                PublicDI.log.debug("in ctor, created object of type: {0}", obj.GetType());
            return obj;
        }

        public static object invoke(this object liveObject, string methodName)
        {
            return liveObject.invoke(methodName, new object[] { });
        }

        public static object invoke(this object liveObject, string methodName, params object[] parameters)
        {
            var result = reflection.invoke(liveObject, methodName, parameters);
            /*if (result != null)
                PublicDI.log.debug("invoke returned type: {0}", result.GetType());*/
            return result;
        }
    }
}