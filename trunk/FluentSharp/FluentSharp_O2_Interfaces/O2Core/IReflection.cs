// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentSharp.O2.Interfaces.O2Core
{
    public interface IReflection
    {
        // GET 

        Assembly getAssembly(string pathToAssemblyToLoad);
        Assembly getCurrentAssembly();
        List<Assembly> getAssembliesInCurrentAppDomain();

        List<Module> getModules(Assembly assembly);

        List<Type> getTypes(Assembly assembly);
        List<Type> getTypes(Module module);
        List<Type> getTypes(Type type);
        Type getType(string assemblyName, string typeToFind);
        Type getType(Assembly assembly, string typeToFind);
        Type getType(Type type, string typeToFind);
        Type getType(string typeToFind);
        string getComObjectTypeName(object comObject);
        List<Type> GetObjectsTypes(object[] objectsToGetType);
        Dictionary<string, List<Type>> getDictionaryWithTypesMappedToNamespaces(Module module);

        List<MethodInfo> getMethods(string pathToassemblyToProcess);
        List<MethodInfo> getMethods(Assembly assembly);
        List<MethodInfo> getMethods(Type type);
        List<MethodInfo> getMethods(Type type, BindingFlags bindingFlags);
        List<MethodInfo> getMethods(Type type, Attribute attribute);        
        MethodInfo getMethod(string pathToAssembly, string methodName);
        MethodInfo getMethod(string pathToAssembly, string methodName, object[] methodParameters);
        MethodInfo getMethod(Type controlType, string methodName);        
        MethodInfo getMethod(Type type, string methodName, object[] methodParameters);

        List<string> getParametersName(MethodInfo method);
        List<Type> getParametersType(MethodInfo method);
        List<ParameterInfo> getParameters(MethodInfo method);

        List<MemberInfo> getMembers(Assembly assembly);
        List<MemberInfo> getMembers(Type type);

        Attribute getAttribute(MethodInfo method, Type type);
        List<Attribute> getAttributes(MethodInfo methodInfo);
        List<Attribute> getAttributes(Type targetType);
        List<Attribute> getAttributes(Assembly assembly);

        List<FieldInfo> getFields(Type type);
        FieldInfo getField(Type type, string fieldName);
        Object getFieldValue(FieldInfo fieldToGet, Object oTargetObject);
        Object getFieldValue(String sFieldToGet, Object oTargetObject);

        PropertyInfo getPropertyInfo(String sPropertyToGet, Type targetType);
        PropertyInfo getPropertyInfo(String sPropertyToGet, Type targetType, bool bVerbose);
        Object getProperty(String type, String property, object liveObject);
        Object getProperty(PropertyInfo propertyToGet);
        Object getProperty(PropertyInfo propertyToGet, object liveObject);
        Object getProperty(String sPropertyToGet, Object oTargetObject);
        Object getProperty(String sPropertyToGet, Object oTargetObject, bool bVerbose);
        List<PropertyInfo> getProperties(Object targetObject);
        List<PropertyInfo> getProperties(Type type);
        Object getInstance(Type tTypeToCreate);

        object[] getRealObjects(object[] objectsToConvert);

        // get xx with attribute
        List<MethodInfo> getTypesWithAttribute(Assembly assembly, Type attributeType);
        List<MethodInfo> getMethodsWithAttribute(Assembly assembly, Type attributeType);
        List<MethodInfo> getMethodsWithAttribute(Type targetType, Type attributeType);

        //Object getLiveObject(object liveObject, string typeToFind);

        // LOAD

        Assembly loadAssembly(string assemblyToLoad);
        bool loadAssemblyAndCheckIfAllTypesCanBeLoaded(string assemblyFile);

        // INVOKE

        void invokeASync(MethodInfo execute, Action<object> onMethodExecutionCompletion);
        void invokeASync(object oLiveObject, MethodInfo execute, Action<object> onMethodExecutionCompletion);
        void invokeASync(object oLiveObject, MethodInfo methodInfo, object[] methodParameters,Action<object> onMethodExecutionCompletion);

        bool invokeMethod_returnSucess(object oLiveObject, string sMethodToInvoke, object[] methodParameters);

        object invoke(MethodInfo methodInfo);
        object invoke(MethodInfo methodInfo, object[] methodParameters);
        object invoke(object oLiveObject, MethodInfo methodInfo);
        object invoke(object oLiveObject, MethodInfo methodInfo, object[] methodParameters);
        object invoke(object oLiveObject, string sMethodToInvoke, object[] methodParameters);

        object invokeMethod_Instance(MethodInfo methodInfo);
        object invokeMethod_InstanceStaticPublicNonPublic(Object oLiveObject, String sMethodToInvoke, Object[] methodParameters);        
        object invokeMethod_Static(Type tMethodToInvokeType, string sMethodToInvokeName, object[] methodParameters);
        object invokeMethod_Static(String sMethodToInvokeType, string sMethodToInvokeName, object[] methodParameters);
        Object invokeMethod_Static(MethodInfo methodToExecute);
        Object invokeMethod_Static(MethodInfo methodToExecute, object[] oParams);

        // SET
        bool setField(FieldInfo fieldToSet, object fieldValue);
        bool setField(FieldInfo fieldToSet, object liveObject, object fieldValue);
        bool setField(String fieldToSet, Type targetType, object fieldValue);
        bool setField(String fieldToSet, object targetObject, object fieldValue);

        bool setProperty(PropertyInfo propertyToSet,object propertyValue);
        bool setProperty(PropertyInfo propertyToSet, object liveObject, object propertyValue);
        bool setProperty(String sPropertyToSet, Type targetType, object propertyValue);
        bool setProperty(String sPropertyToSet, object oTargetObject, object propertyValue);

        // CREATE

        object createObject(String assemblyToLoad, String typeToCreateObject, params object[] constructorArguments);
        object createObject(Assembly assembly, Type typeToCreateObject, params object[] constructorArguments);
        object createObject(Assembly assembly, String typeToCreateObject);
        object createObject(Assembly assembly, String typeToCreateObject, params object[] constructorArguments);
        object createObject(Type type, params object[] constructorArguments);
        object createObjectUsingDefaultConstructor(Type tTypeToCreateObject);       
    }
}