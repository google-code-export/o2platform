// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;  
using System.Collections.Generic;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;

//O2File:_Extra_methods_Items.cs

namespace O2.XRules.Database.Utils
{	
	
	public static class _Extra_Reflection_ExtensionMethods
	{
		//Reflection				
		public static object field<T>(this object _object, string fieldName)
		{
			var type = typeof(T);  
			return _object.field(type, fieldName);
		}
		
		public static object field(this object _object, Type type, string fieldName)
		{			
			"**** type:{0}".error(type.typeName());
			var fieldInfo =  (FieldInfo)type.field(fieldName);
			return PublicDI.reflection.getFieldValue(fieldInfo, type);
		}
		
		public static List<ConstructorInfo> ctors(this Type type)
		{
			return type.GetConstructors(System.Reflection.BindingFlags.NonPublic | 
									    System.Reflection.BindingFlags.Public | 
									    System.Reflection.BindingFlags.Instance).toList();
		}
		


		
		public static T property<T>(this object _object, string propertyName)
		{						
			if (_object.notNull())
			{
				var result = _object.property(propertyName);
				if (result.notNull())
					return (T)result;
			}
			return default(T);
		}
		
		public static object property(this object _object, string propertyName, object value)
		{
			var propertyInfo = PublicDI.reflection.getPropertyInfo(propertyName, _object.type());  
			if (propertyInfo.isNull())
				"Could not find property {0} in type {1}".error(propertyName, _object.type());  
			else
			{
				PublicDI.reflection.setProperty(propertyInfo, _object,value);    
		//		"set {0}.{1} = {2}".info(_object.type(),propertyName, value);
			}
			return _object;

		}
		
		public static List<string> names(this List<PropertyInfo> properties)
		{
			return (from property in properties
					select property.Name).toList();
		}
		
		public static List<object> propertyValues(this object _object)
		{
			var propertyValues = new List<object>();
			var names = _object.type().properties().names();
			foreach(var name in names)
				propertyValues.Add(_object.prop(name));
			return propertyValues;
		}
		
		public static MethodInfo method(this Type type, string name)
		{
			foreach(var method in type.methods())
			{				
				if (method.Name == name)
					return method;
			}
			return null;
		}
		
		public static List<System.Attribute> attributes(this List<MethodInfo> methods)
		{
			return (from method in methods 
					from attribute in method.attributes()
					select attribute).toList();
		}
		public static List<System.Attribute> attributes(this MethodInfo method)
		{
			return PublicDI.reflection.getAttributes(method);
		}
		
		public static List<MethodInfo> withAttribute(this Assembly assembly, string attributeName)
		{
			return assembly.methods().withAttribute(attributeName);
		}
		
		public static List<MethodInfo> withAttribute(this List<MethodInfo> methods, string attributeName)
		{ 
			return (from method in methods 
					from attribute in method.attributes()		  
					where attributeName == (attribute.TypeId as Type).Name.remove("Attribute")
					select method).toList();						
		}
		
		public static List<MethodInfo> methodsWithAttribute<T>(this Assembly assembly)
			where T : Attribute
		{
			return assembly.methods().withAttribute<T>();
		}
		
		public static List<MethodInfo> withAttribute<T>(this List<MethodInfo> methods)
			where T : Attribute
		{
			return (from method in methods 
					from attribute in method.attributes()		  
					where attribute is T
					select method).toList();						
		}

		
		public static string signature(this MethodInfo methodInfo)
		{
			return "mscorlib".assembly()
							 .type("RuntimeMethodInfo")
							 .invokeStatic("ConstructName",methodInfo)
							 .str();
		}
		
		public static object enumValue(this Type enumType, string value)
		{
			return enumType.enumValue<object>(value);
		}
		public static T enumValue<T>(this Type enumType, string value)
		{
			var fieldInfo = (FieldInfo) enumType.field(value);
			if (fieldInfo.notNull())
				return (T)fieldInfo.GetValue(enumType);
			return default(T);
		}
		
		public static List<AssemblyName> referencedAssemblies(this Assembly assembly)
		{
			return assembly.GetReferencedAssemblies().toList();
		}
		
		public static Assembly assembly(this AssemblyName assemblyName)
		{
			return assemblyName.str().assembly();
		}
		
		public static List<MethodInfo> methods(this List<Type> types)
		{
			return (from type in types
					from method in type.methods()
					select method).toList();					
		}
		
		
		public static List<T> attributes<T>(this MethodInfo methods)
			where T : Attribute
		{
			return methods.attributes().attributes<T>();
		}
		
		public static List<T> attributes<T>(this List<MethodInfo> methods)
			where T : Attribute
		{
			return methods.attributes<T>();
		}
		
		public static List<T> attributes<T>(this List<Attribute> attributes)
			where T : Attribute
		{
			return (from attribute in attributes
					where attribute is T
					select (T)attribute).toList();
		}
				
		
		public static List<Attribute> attributes(this List<MethodInfo> methods, string name)
		{
			return methods.attributes().withName(name);
		}
		
		public static Attribute attribute(this MethodInfo methodInfo, string name)
		{
			foreach(var attribute in methodInfo.attributes())
				if (attribute.name() == name)
					return attribute;
			return null;			
		}
		
		public static T attribute<T>(this MethodInfo methodInfo)
			where T : Attribute
		{
			return methodInfo.attributes<T>().first();			
		}
		
		public static string name(this Attribute attribute)
		{
			return attribute.typeName().remove("Attribute");
		}
		
		public static List<string> names(this List<Attribute> attributes)
		{
			return (from attribute in attributes
					select attribute.name()).toList();
		}
		
		public static List<Attribute> withName(this List<Attribute> attributes, string name)
		{
			return (from attribute in attributes
					where attribute.name() == name
					select attribute).toList();
		}
		//Array		
		
		public static Array createArray<T>(this Type arrayType,  params T[] values)			
		{
			try
			{
				if (values.isNull())
					return  Array.CreateInstance (arrayType,0);	
					
				var array =  Array.CreateInstance (arrayType,values.size());	
				
				if (values.notNull())
					for(int i=0 ; i < values.size() ; i ++)
						array.SetValue(values[i],i);
				return array;				 				
			}
			catch(Exception ex)
			{
				ex.log("in Array.createArray");
			}
			return null;
		}
		
		
		
		//WebServices SOAP methods
		public static List<MethodInfo> webService_SoapMethods(this Assembly assembly)
		{
			var soapMethods = new List<MethodInfo >(); 
			foreach(var type in assembly.types())
				soapMethods.AddRange(type.webService_SoapMethods());
			return soapMethods;
					
		}
		public static List<MethodInfo> webService_SoapMethods(this object _object)
		{
			Type type = (_object is Type) 	
							? (Type)_object
							: _object.type();				
			var soapMethods = new List<MethodInfo >(); 
			foreach(var method in type.methods())
				foreach(var attribute in method.attributes())
					if (attribute.typeFullName() == "System.Web.Services.Protocols.SoapDocumentMethodAttribute" ||
					    attribute.typeFullName() == "System.Web.Services.Protocols.SoapRpcMethodAttribute")
						soapMethods.Add(method);
			return soapMethods;
		}
		
		public static Items property_Values_AsStrings(this object _object)
		{		
			var propertyValues_AsStrings = new Items();
			foreach(var property in _object.type().properties())				
				propertyValues_AsStrings.add(property.Name.str(), _object.property(property.Name).str());
			return propertyValues_AsStrings;
		}

	}
}
    	