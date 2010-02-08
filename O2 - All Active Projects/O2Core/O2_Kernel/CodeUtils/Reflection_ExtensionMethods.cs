using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Kernel.CodeUtils
{
    public static class Reflection_ExtensionMethods
    {
        public static IReflection reflection =  PublicDI.reflection;

        public static bool verbose; // = false

        public static object ctor(this string className, string assembly, params object[] parameters)
        {
            var obj = reflection.createObject(assembly, className, parameters);
            if (verbose)
                if (obj == null)
                    PublicDI.log.error("in ctor, could not created object: {0}!{1}", assembly, className);
                else
                    PublicDI.log.debug("in ctor, created object of type: {0}", obj.GetType());
            return obj;
        }

        public static Object ctor(this Type type, params object[] constructorParams)
        {
            return PublicDI.reflection.createObject(type, constructorParams);
        }
        
        public static Assembly assembly(this string assemblyName)
        {
            return PublicDI.reflection.getAssembly(assemblyName);
        }

        public static Type type(this Assembly assembly, string typeName)
        {
            return PublicDI.reflection.getType(assembly, typeName);
        }

        public static Type type(this string assemblyName, string typeName)
        {
            return PublicDI.reflection.getType(assemblyName, typeName);
        }

        public static Type type(this object _object)
        {
            return _object.GetType();
        }
        
        public static object prop(this Type type, string propertyName)
        {
            return PublicDI.reflection.getProperty(propertyName, type);
        }

        public static object prop(this object liveObject, string propertyName)
        {
            return PublicDI.reflection.getProperty(propertyName, liveObject);
        }
        
        public static object field(this Type type, string fieldName)
        {
            return PublicDI.reflection.getField(type, fieldName);
        }

        public static object invoke(this object liveObject, string methodName)
        {
            return liveObject.invoke(methodName, new object[] { });
        }

        public static object invoke(this object liveObject, string methodName, params object[] parameters)
        {
            return reflection.invoke(liveObject, methodName, parameters);                        
        }

    }
}