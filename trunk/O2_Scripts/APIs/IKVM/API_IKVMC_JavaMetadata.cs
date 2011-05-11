// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.XRules.Database.Utils;
 
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs.IKVM
{	
	[Serializable]
    public class API_IKVMC_Java_Metadata
    {
    	[XmlAttribute] 
    	public string FileProcessed {get; set;}  
    	[XmlElement("JavaClass")]
    	public List<Java_Class> Classes {get; set;}
    	
    	public API_IKVMC_Java_Metadata()
    	{    		
    		Classes = new List<Java_Class>();
    	}
    	
    	public override string ToString()
    	{
    		return "{0}            [{1} classes]".format(FileProcessed.fileName(), Classes.size());
    	}
	}
	
	[Serializable]
	public class Java_Class
	{		
		[XmlAttribute] public string Signature {get;set;}
		[XmlAttribute] public string Name {get;set;}
		[XmlAttribute] public string SuperClass {get;set;}
		[XmlAttribute] public string SourceFile {get;set;}
		[XmlAttribute] public string IsAbstract {get;set;}
		[XmlAttribute] public string IsInterface {get;set;}
		[XmlAttribute] public string IsInternal {get;set;}
		[XmlAttribute] public string IsPublic {get;set;}
					   public Object[] Annotations {get;set;}
					   public List<string> Interfaces {get;set;}					   
					   public List<Java_Field> Fields {get;set;}
					   public List<Java_Method> Methods {get;set;}
					   public List<ConstantPool> ConstantsPool {get;set;}					   
		
		public Java_Class()
		{
			Interfaces = new List<string>();
			Fields = new List<Java_Field>();
			Methods = new List<Java_Method>();
			ConstantsPool = new List<ConstantPool>();
		}
		
		public override string ToString()
    	{
    		return Name;
    		//return "{0}            [{1} methods]".format(Name, Methods.size());
    	}
	}
	
	
	[Serializable]
	public class Java_Field
	{
		[XmlAttribute] 				public string Name {get;set;}
		[XmlAttribute] 				public string Signature {get;set;}		
		[XmlAttribute] 				public string ConstantValue {get;set;}
	}
	
	[Serializable]
	public class Java_Method 
	{
		[XmlAttribute] 				public string Name {get;set;}
		[XmlAttribute] 				public string ParametersAndReturnValue {get;set;}		
		[XmlAttribute] 				public string ClassName {get;set;}
		[XmlAttribute]				public string Signature {get;set;}
		[XmlAttribute] 				public string IsAbstract {get;set;}
		[XmlAttribute] 			  	public string IsClassInitializer {get;set;}		
		[XmlAttribute] 			  	public string IsNative {get;set;}		
		[XmlAttribute] 			  	public string IsPublic {get;set;}
		[XmlAttribute] 			  	public string IsStatic {get;set;}		
									public Object[] Annotations {get;set;}
									public Object[] ParameterAnnotations {get;set;}
		[XmlElement("Variable")]	public List<Java_Variable> Variables {get;set;}
		[XmlElement("LineNumber")]	public List<LineNumber> LineNumbers {get;set;}
		[XmlElement("Instruction")] public List<Java_Instruction> Instructions {get;set;}
		
		public Java_Method()
		{
			Variables = new List<Java_Variable>();
			Instructions = new List<Java_Instruction>();
			LineNumbers = new List<LineNumber>();
		}
		
		public override string ToString()
    	{
    		return "{0} {1}".format(Name, ParametersAndReturnValue);
    	}
	}
	
	[Serializable]
	public class ConstantPool
	{
		[XmlAttribute] public int Id {get;set;}
		[XmlAttribute] public string Type {get;set;}
		[XmlAttribute] public string Value {get;set;}
		[XmlAttribute] public bool ValueEncoded {get;set;}
		
		public ConstantPool()
		{
		}
		
		public ConstantPool(int id, string type, string value)
		{
			Id = id;
			Type = type;
			Value = value;
		}
		
		public ConstantPool(int id, string type, string value, bool valueEncoded) : this(id, type, value)
		{
			ValueEncoded = valueEncoded;
		}
		
		public override string ToString()	
		{
			return (ValueEncoded) ? Value.base64Decode() : Value;
		}
		
	}
	
	[Serializable]
	public class Java_Instruction
	{		
		[XmlAttribute] public int Pc {get;set;}	
		[XmlAttribute] public string OpCode {get;set;}		
		[XmlAttribute] public int TargetIndex {get;set;}				
		[XmlAttribute] public int Line_Number {get;set;}
	}		
	
	[Serializable]
	public class Java_Variable
	{		
		[XmlAttribute] public string Descriptor {get;set;}		
		[XmlAttribute] public int Index {get;set;}		
		[XmlAttribute] public int Length {get;set;}		
		[XmlAttribute] public string Name {get;set;}		
		[XmlAttribute] public int Start_Pc {get;set;}			
	}		
	
	[Serializable]
	public class LineNumber
	{
		[XmlAttribute] public int Line_Number {get;set;}
		[XmlAttribute] public int Start_Pc {get;set;}		
	}
	
	
	public static class API_IKVMC_Java_Metadata_ExtensionMethods
	{
		public static Dictionary<string, Java_Class> getClasses_IndexedBySignature(this API_IKVMC_Java_Metadata javaMetadata)
		{
			var classesBySignature = new Dictionary<string, Java_Class>();
			foreach(var _class in javaMetadata.Classes)
				classesBySignature.add(_class.Signature, _class);
			return classesBySignature;
		}
		
		public static List<int> uniqueTargetIndexes(this Java_Method method)
		{
			return  (from instruction in method.Instructions
		 			 where instruction.TargetIndex > 0
					 select instruction.TargetIndex).Distinct().toList();
		}
	}
	
	
}