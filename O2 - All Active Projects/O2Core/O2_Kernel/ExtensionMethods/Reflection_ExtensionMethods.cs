using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using O2.Interfaces.O2Core;

namespace O2.Kernel.ExtensionMethods
{
    public static class Reflection_ExtensionMethods
    {
        public static IReflection reflection =  PublicDI.reflection;

        public static bool verbose; // = false            	        	        

        #region ctor

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
        
        #endregion

        #region assembly

        public static Assembly assembly(this string assemblyName)
        {
            return PublicDI.reflection.getAssembly(assemblyName);
        }
    
        #endregion

        #region type

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

        public static string typeName(this object _object)
        {
            return _object.type().Name;
        }

        public static string typeFullName(this object _object)
        {
            return _object.type().FullName;
        }
	

        #endregion

        #region properties

        public static List<PropertyInfo> properties(this Type type)
        {
            return PublicDI.reflection.getProperties(type);
        }
        
        public static object property(this Type type, string propertyName)
        {
            return type.prop(propertyName);
        }

        public static object prop(this Type type, string propertyName)
        {
            return PublicDI.reflection.getProperty(propertyName, type);
        }
    
        public static object prop(this object liveObject, string propertyName)
        {
            return PublicDI.reflection.getProperty(propertyName, liveObject);
        }

        public static void prop(this object _liveObject, PropertyInfo propertyInfo, object value)
        {
            PublicDI.reflection.setProperty(propertyInfo, _liveObject, value);
        }

        public static object property(this object liveObject, string propertyName)
        {
            return liveObject.prop(propertyName);
        }

        public static void prop(this object _liveObject, string propertyName, object value)
        {
            PublicDI.reflection.setProperty(propertyName, _liveObject, value);
        }

        #endregion

        #region methods
        
        public static List<MethodInfo> methods(this Type type)
        {
            return PublicDI.reflection.getMethods(type);
        }

        public static List<MethodInfo> methods(this Assembly assembly)
        {
            return PublicDI.reflection.getMethods(assembly);
        }        		

        #endregion

        #region fields

        public static List<FieldInfo> fields(this Type type)
        {
            return PublicDI.reflection.getFields(type);
        }

        public static object field(this object liveObject, string fieldName)
        {
            return PublicDI.reflection.getFieldValue(fieldName, liveObject);
        }

        public static object field(this Type type, string fieldName)
        {
            return PublicDI.reflection.getField(type, fieldName);
        }

        public static object fieldValue(this Type type, string fieldName)
        {
            var fieldInfo = (FieldInfo)type.field(fieldName);
            return PublicDI.reflection.getFieldValue(fieldInfo, null);
        }

        #endregion

        #region invoke 

        public static object invoke(this object liveObject, string methodName)
        {
            return liveObject.invoke(methodName, new object[] { });
        }

        public static object invoke(this object liveObject, string methodName, params object[] parameters)
        {
            return reflection.invoke(liveObject, methodName, parameters);
        }

        public static object invokeStatic(this Type type, string methodName, params object[] parameters)
        {
            return PublicDI.reflection.invokeMethod_Static(type, methodName, parameters);
        }
        
        public static void invoke(this object _object, MethodInvoker methodInvoker)
        {
            if (methodInvoker != null)
                methodInvoker();
        }

        public static object invoke(this MethodInfo methodInfo)
        {
            return PublicDI.reflection.invoke(methodInfo);
        }

        public static object invoke(this MethodInfo methodInfo, params object[] parameters)
        {
            return PublicDI.reflection.invoke(methodInfo, parameters);
        }
        #endregion 
    }
}