// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.XRules.Database.Utils;

//O2File:API_IKVMC_JavaMetadata.cs
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs
//O2Ref:IKVM.Runtime.dll
//O2Ref:IKVM.OpenJDK.Util.dll
//O2Ref:IKVM.OpenJDK.Core.dll
//O2Ref:IKVM.Reflection.dll
//O2Ref:ikvmc.exe
//O2Ref:ICSharpCode.SharpZipLib.dll

namespace O2.XRules.Database.APIs.IKVM
{
    public class API_IKVMC	
    {
    	public Assembly IkvmcAssembly   { get; set;}
    	public Type StaticCompiler  { get; set;}
    	public object IkvmRuntime 	  { get; set;}
    	public object IkvmRuntimeJni  { get; set;}
    	public object IkvmcCompiler   { get; set;}
    	public object CompilerOptions { get; set;}
    	
    	public API_IKVMC()
    	{
    		setup();
    	}
    	
    	public void setup()
    	{    		
    		IkvmcAssembly = "ikvmc.exe".assembly();
    		StaticCompiler = IkvmcAssembly.type("StaticCompiler");
			IkvmRuntime = StaticCompiler.invokeStatic("LoadFile",Environment.CurrentDirectory.pathCombine("IKVM.Runtime.dll")); 
			PublicDI.reflection.setField((FieldInfo)StaticCompiler.field("runtimeAssembly"),IkvmRuntime);  				
			
			IkvmRuntimeJni = StaticCompiler.invokeStatic("LoadFile",Environment.CurrentDirectory.pathCombine("IKVM.Runtime.JNI.dll")); 
			PublicDI.reflection.setField((FieldInfo)StaticCompiler.field("runtimeJniAssembly"),IkvmRuntimeJni);  
			
			IkvmcCompiler =  IkvmcAssembly.type("IkvmcCompiler").ctor();
			
			CompilerOptions = IkvmcAssembly.type("CompilerOptions").ctor();
			PublicDI.reflection.setField((FieldInfo)StaticCompiler.field("toplevel"),CompilerOptions);  
    	}
	}
	
	public static class API_IKVMC_ExtensionMethods_CreateJavaMetadata
	{
		public static Dictionary<string, byte[]> getRawClassesData_FromFile_ClassesOrJar(this API_IKVMC ikvmc, string classOrJar)
		{
			var targets = typeof(List<>).MakeGenericType(new System.Type[] { ikvmc.CompilerOptions.type()}).ctor();
			var args = classOrJar.wrapOnList().GetEnumerator();
			ikvmc.IkvmcCompiler.invoke("ParseCommandLine", args, targets, ikvmc.CompilerOptions);  				
			var compilerOptions =  (targets as IEnumerable).first();
			var classes =   (Dictionary<string, byte[]>) compilerOptions.field("classes");  
			return classes;
		}				
		
		public static object createClassFile(this API_IKVMC ikvmc, byte[] bytes)
		{
			// 0 = ClassFileParseOptions.None, 
			// 1 = ClassFileParseOptions.LocalVariableTable , 
			// 2 = ClassFileParseOptions.LineNumberTable
			return ikvmc.createClassFile(bytes,0);			
		}
		
		public static object createClassFile(this API_IKVMC ikvmc, byte[] bytes, int classFileParseOptions)
		{
			var classFileType = ikvmc.IkvmcAssembly.type("ClassFile");
			var ctor = classFileType.ctors().first();					
		 
			var classFile = ctor.Invoke(new object[] {bytes,0, bytes.Length, null, classFileParseOptions });   
			if (classFile.notNull())
				return classFile;
			"[API_IKVMC] in createClassObject failed to create class".info();
			return null;
		}
		 
		public static API_IKVMC_Java_Metadata create_JavaMetadata(this API_IKVMC ikvmc, string fileToProcess)
		{
			var o2Timer = new O2Timer("Created JavaData for {0}".format(fileToProcess)).start();
			var javaMetaData = new API_IKVMC_Java_Metadata();
			javaMetaData.FileProcessed = fileToProcess;
			var classes = ikvmc.getRawClassesData_FromFile_ClassesOrJar(fileToProcess);
			foreach(var item in classes)
			{
				var name = item.Key;
				var bytes = item.Value;
				var classFile = ikvmc.createClassFile(bytes,1);				// value of 1 gets the local variables
				var javaClass = new Java_Class {						
													Name = name,
													SourceFile = classFile.prop("SourceFileAttribute").str(),
													IsAbstract = classFile.prop("IsAbstract").str(),
													IsInterface = classFile.prop("IsInterface").str(),
													IsInternal = classFile.prop("IsInternal").str(),
													IsPublic = classFile.prop("IsPublic").str(),
													SuperClass = classFile.prop("SuperClass").str()
											 };
				
				javaClass.ConstantsPool = classFile.getConstantPoolEntries();
				javaClass.map_Annotations(classFile)
						 .map_Interfaces(classFile)
						 .map_Fields(classFile)
						 .map_Methods(classFile);
						 				
				javaClass.map_LineNumbers(ikvmc.createClassFile(bytes,2)); // for this we need to call createClassFile with the value of 2 (to get the source code references)
				javaMetaData.Classes.Add(javaClass);												
				//break; // 
			}
			o2Timer.stop();
			return javaMetaData;
		}
		
		public static Java_Class map_Annotations(this Java_Class javaClass, object classFile)
		{
			var annotations = (Object[])classFile.prop("Annotations");
			if (annotations.notNull())			
				javaClass.Annotations = annotations;			
			return javaClass;
		}
		
		public static Java_Method map_Annotations(this Java_Method javaMethod, object method)
		{
			var annotations = (Object[])method.prop("Annotations");
			if (annotations.notNull())			
				javaMethod.Annotations = annotations;			
				
			var parameterAnnotations = (Object[])method.prop("ParameterAnnotations");
			if (parameterAnnotations.notNull())			
				javaMethod.ParameterAnnotations = parameterAnnotations;								
				
			return javaMethod;
		}
		
		public static Java_Class map_Interfaces(this Java_Class javaClass, object classFile)
		{
			var interfaces = (IEnumerable)classFile.prop("Interfaces");						
			foreach(var _interface in interfaces)
				javaClass.Interfaces.Add(_interface.prop("Name").str());				
			return javaClass;
		}
		
		public static Java_Class map_Fields(this Java_Class javaClass, object classFile)
		{
			//var interfaces = (IEnumerable)classFile.prop("Interfaces");						
			//foreach(var _interface in interfaces)
			//	javaClass.Interfaces.Add(_interface.prop("Name").str());				
			var fields = (IEnumerable)classFile.prop("Fields");
			foreach(var field in fields)			
				javaClass.Fields.Add(new Java_Field
										{
											Name = field.prop("Name").str(),
											Signature = field.prop("Signature").str(),
											ConstantValue= field.prop("ConstantValue").notNull() 
																? field.prop("ConstantValue").str()
																: null
										});				
			
			return javaClass;
		}
		
		public static Java_Class map_Methods(this Java_Class javaClass, object classFile)
		{
			var mappedConstantsPools = javaClass.ConstantsPool.getDictionaryWithValues();
			var methods = (IEnumerable)classFile.prop("Methods");
			foreach(var method in methods)
			{		
				var javaMethod = new Java_Method {
													Name = method.prop("Name").str(),
													ParametersAndReturnValue = method.prop("Signature").str(),													
													ClassName = javaClass.Name,
													IsAbstract = method.prop("IsAbstract").str(), 
													IsClassInitializer = method.prop("IsClassInitializer").str(), 
													IsNative = method.prop("IsNative").str(), 
													IsPublic = method.prop("IsPublic").str(), 
													IsStatic = method.prop("IsStatic").str()
												 };
				javaMethod.Signature = "{0}{1}".format(javaMethod.Name,javaMethod.ParametersAndReturnValue);												 								
				
				javaMethod.map_Annotations(method)
						  .map_Variables(method);
				
				
				var instructions = (IEnumerable) method.prop("Instructions");
				if (instructions.notNull())
				{	
					foreach(var instruction in instructions)
					{
						var javaInstruction = new Java_Instruction();
						javaInstruction.Pc = instruction.prop("PC").str().toInt(); 
						javaInstruction.OpCode = instruction.prop("NormalizedOpCode").str(); 
						javaInstruction.TargetIndex =  instruction.prop("TargetIndex").str().toInt();
						javaMethod.Instructions.Add(javaInstruction);						
					}
				}
				
				javaClass.Methods.Add(javaMethod);
			}
			return javaClass;
		}
		
		public static Java_Method map_Variables(this Java_Method javaMethod, object method)
		{			
			var localVariablesTable = (IEnumerable)method.prop("LocalVariableTableAttribute");
			if (localVariablesTable.notNull())			
				foreach(var localVariable in localVariablesTable)
					javaMethod.Variables.Add(new Java_Variable  {
																	Descriptor = localVariable.field("descriptor").str(), 
																	Index = localVariable.field("index").str().toInt(), 
																	Length = localVariable.field("length").str().toInt(), 
																	Name = localVariable.field("name").str(), 
																	Start_Pc = localVariable.field("start_pc").str().toInt()
																});							
			return javaMethod;
		}
		
		//we need to do this because the IKVMC doesn't have a option to get both fields and source code references at the same time
		public static Java_Class map_LineNumbers(this Java_Class javaClass, object classFileWithLineNumbers)
		{
			var methodsWithLineNumbers = ((IEnumerable)classFileWithLineNumbers.prop("Methods"));
			var currentMethodId = 0;
			foreach(var methodWithLineNumbers in methodsWithLineNumbers)
			{
				var javaMethod = javaClass.Methods[currentMethodId];
				var lineNumberTableAttributes = (IEnumerable)methodWithLineNumbers.prop("LineNumberTableAttribute");
				if (lineNumberTableAttributes.notNull())
					foreach(var lineNumberTableAttribute in lineNumberTableAttributes)
						javaMethod.LineNumbers.Add(new LineNumber {
																	Line_Number = lineNumberTableAttribute.field("line_number").str().toInt(),
																	Start_Pc = lineNumberTableAttribute.field("start_pc").str().toInt()																		
																  });
				currentMethodId++;																  
			}						
			return javaClass;
		}
		
		public static List<ConstantPool> add_Entry(this List<ConstantPool>  constantsPool,int id, string type, string value)
		{
			var constantPool = new ConstantPool(id, type, value);
			constantsPool.Add(constantPool);
			return constantsPool;
		}
		public static List<ConstantPool> getConstantPoolEntries(this object classFile)
		{
			var constantsPool = new List<ConstantPool>();
			
			var constantPoolRaw = new Dictionary<int,object>();
			var constantPool =  (IEnumerable)classFile.field("constantpool");  
			var index = 0;
			foreach(var constant in constantPool)
				constantPoolRaw.add(index++, constant); 
				
			//var constantPoolValues = new Dictionary<int,string>();	 
		//	var stillToMap = new List<object>();
			for(int i=0; i < constantPoolRaw.size() ; i++)
			{
				var currentItem = constantPoolRaw[i];
				var currentItemType = currentItem.str();
				switch(currentItemType)
				{
					case "IKVM.Internal.ClassFile+ConstantPoolItemClass":											
						constantsPool.add_Entry(i, currentItemType.remove("IKVM.Internal.ClassFile+ConstantPoolItem"), currentItem.prop("Name").str());						
						break;
					case "IKVM.Internal.ClassFile+ConstantPoolItemMethodref":						
					case "IKVM.Internal.ClassFile+ConstantPoolItemFieldref":									
						constantsPool.add_Entry(i,currentItemType.remove("IKVM.Internal.ClassFile+ConstantPoolItem"), 
												"{0}.{1} : {2}".format(currentItem.prop("Class"),
												   	 				   currentItem.prop("Name"),
												   	 				   currentItem.prop("Signature")));									
						break;
					case "IKVM.Internal.ClassFile+ConstantPoolItemNameAndType":	 // skip this one since don;t know what they point to
						//constantPoolValues.Add(i,"IKVM.Internal.ClassFile+ConstantPoolItemNameAndType");	
						break; 
					case "IKVM.Internal.ClassFile+ConstantPoolItemString":
					case "IKVM.Internal.ClassFile+ConstantPoolItemInteger":
					case "IKVM.Internal.ClassFile+ConstantPoolItemFloat":
					case "IKVM.Internal.ClassFile+ConstantPoolItemDouble": 
					case "IKVM.Internal.ClassFile+ConstantPoolItemLong":
						var value = currentItem.prop("Value").str();
						value = value.base64Encode();
						//value = value.replace("&#x", "&amp;#x"); //HACK to deal with BUG in .NET Serialization and Deserialization (to reseatch further)
						constantsPool.add_Entry(i,currentItemType.remove("IKVM.Internal.ClassFile+ConstantPoolItem"),value);
						break;
					case "IKVM.Internal.ClassFile+ConstantPoolItemInterfaceMethodref": // doesn't have any value to use
						break;
					case "[null value]":
						//constantsPool.add_Entry(i,"[null value]", null);						
						break; 
					default:
						"Unsupported constantPoll type: {0}".error(currentItem.str());
						break;
				}		
			}
			return constantsPool;	
		}		
		
		public static Dictionary<int,string> getDictionaryWithValues(this  List<ConstantPool> constantsPool)
		{
			var dictionary = new Dictionary<int,string>();
			foreach(var item in constantsPool)
				dictionary.Add(item.Id, item.Value);
			return dictionary;
		}
	}
	
	public static class API_IKVMC_ExtensionMethods_CreateDotNetAssemblies
	{
		public static string createAssembly_FromFile_ClasssesOrJar(this API_IKVMC ikvmc, string classOrJar)
		{				
			var console = "IKVM Stored Console out and error".capture_Console();
			var targetFile = "_IKVM_Dlls".tempDir(false).pathCombine("{0}.dll".format(classOrJar.fileName()));
			
			var args = new List<string> {	
											classOrJar,
											"-out:{0}".format(targetFile)
										} .GetEnumerator();
			//return args.toList();								
			var targets = typeof(List<>).MakeGenericType(new System.Type[] { ikvmc.CompilerOptions.type()}).ctor();

			ikvmc.IkvmcCompiler.invoke("ParseCommandLine", args, targets, ikvmc.CompilerOptions); 

			//ikvmcCompiler.details();
			var compilerClassLoader = ikvmc.IkvmcAssembly.type("CompilerClassLoader");
			var compile = compilerClassLoader.method("Compile"); 
			
			var createCompiler = compilerClassLoader.method("CreateCompiler"); 
			
			PublicDI.reflection.invokeMethod_Static(compile, new object[] {null,targets});  
			//ikvmcCompiler.details(); 
			return console.readToEnd();
		}
		
	}
}