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

        public static string assemblyLocation(this Type type)
        {
            return type.Assembly.Location;
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

        public static List<Type> types(this Assembly assembly)
        {
            return PublicDI.reflection.getTypes(assembly);
        }

        public static List<Type> types(this Type type)
        {
            return PublicDI.reflection.getTypes(type);
        }

        public static string typeName(this object _object)
        {
            return _object.type().Name;
        }

        public static string typeFullName(this object _object)
        {
            return _object.type().FullName;
        }

        public static string name(this Type type)
        {
            return type.Name;
        }

        public static string fullName(this Type type)
        {
            return type.FullName;
        }

        public static string comTypeName(this object _object)
        {
            return PublicDI.reflection.getComObjectTypeName(_object);
        }

        #endregion

        #region properties

        public static List<PropertyInfo> properties(this Type type)
        {
            return PublicDI.reflection.getProperties(type);
        }
        
        public static List<PropertyInfo> properties_public_declared(this Type type, BindingFlags bindingFlags)
        { 
            return type.properties(BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        public static List<PropertyInfo> properties(this Type type, BindingFlags bindingFlags)
        {            
            return new List<PropertyInfo>(type.GetProperties(bindingFlags));
        }
        
        public static object property(this Type type, string propertyName)
        {
            return type.prop(propertyName);
        }

        public static Type propertyType(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType;
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

        public static List<MethodInfo> methods_public(this Type type)
        {
            return PublicDI.reflection.getMethods(type, BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance);
        }

        public static List<MethodInfo> methods_private(this Type type)
        {
            return PublicDI.reflection.getMethods(type, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance);
        }

        public static List<MethodInfo> methods_declared(this Type type)
        {
            return PublicDI.reflection.getMethods(type, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        }

        public static List<MethodInfo> methods_static(this Type type)
        {
            return PublicDI.reflection.getMethods(type, BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public static List<MethodInfo> methods_instance(this Type type)
        {
            return PublicDI.reflection.getMethods(type, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public static MethodInfo firstMethod(this Assembly assembly)
    	{    		
    		foreach(var method in assembly.methods())
    			if (false == method.IsSpecialName)      // to skip the methods created by properties
    			    return method;        	
        	return null;
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

        public static void field(this object liveObject, string fieldName, object value)
        {
            PublicDI.reflection.setField(fieldName, liveObject, value);
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

        #region parameters

        public static List<ParameterInfo> parameters(this MethodInfo methodInfo)
        {
            return PublicDI.reflection.getParameters(methodInfo);
        }

        #endregion

        #region interfaces

        public static List<Type> interfaces(this Type type)
        {
            return new List<Type>( type.GetInterfaces() );
        }

        #endregion


    }
}