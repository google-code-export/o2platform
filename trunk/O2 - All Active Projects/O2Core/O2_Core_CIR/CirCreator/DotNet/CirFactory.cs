// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Pdb;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono.MonoCecil;
using O2.Kernel;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.CirCreator.DotNet
{
    public class CirFactory
    {
        /// <summary>
        /// this will return the current CirFunction object for the signature provided or create a new CirFunction
        /// object and return it
        /// </summary>
        /// <param name="cirData"></param>
        /// <param name="functionSignature"></param>
        /// <param name="functionType"></param>
        /// <returns></returns>
        public ICirFunction getCirFunction(ICirData cirData, string functionSignature, string functionType)
        {
            try
            {
            
                if (cirData.dFunctions_bySignature.ContainsKey(functionSignature))
                    return cirData.dFunctions_bySignature[functionSignature];

                // create the function reference                
                var newCirFunction = new CirFunction(functionSignature);
                cirData.dFunctions_bySignature.Add(functionSignature, newCirFunction);

                // add it to the respective CirClas

                var cirClass = getCirClass(cirData,functionType);
                cirClass.dFunctions.Add(functionSignature, newCirFunction);

                return newCirFunction;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactory.getCirFunction", true);
                return null;
            }
        }

        /// <summary>
        /// this will return the current CirClass object for the signature provided or create a new CirClass
        /// object and return it 
        /// </summary>
        /// <param name="cirData"></param>
        /// <param name="classSignature"></param>
        /// <returns></returns>
        private static ICirClass getCirClass(ICirData cirData, string classSignature)
        {
            try
            {
                if (cirData.dClasses_bySignature.ContainsKey(classSignature))
                    return cirData.dClasses_bySignature[classSignature];
                var newCirClass = new CirClass(classSignature);
                cirData.dClasses_bySignature.Add(classSignature, newCirClass);
                return newCirClass;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactory.getCirClass", true);
                return null;
            }
        }


        private ICirFunction processMemberReference(ICirData cirData, IMemberReference memberDefinition, SequencePoint sequencePoint)
        {
            switch (memberDefinition.GetType().Name)
            {
                case "MethodReference":
                    return processMethodReference(cirData, (MethodReference)memberDefinition, sequencePoint);
                case "MethodDefinition":
                    return processMethodDefinition(cirData, (MethodDefinition)memberDefinition, sequencePoint);
                default:
                    DI.log.error("in CirFactory.processMethodMember, unsupported MethodMember: {0}", memberDefinition.GetType().Name);
                    break;
            }
            return null;
        }

        public ICirFunction processMethodReference(ICirData cirData, MethodReference methodReference, SequencePoint sequencePoint)
        {
            try
            {                
                var functionSignature = CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(methodReference);
                var functionType = CirFactoryUtils.getTypeUniqueSignatureFromTypeReference(methodReference.DeclaringType);
                var cirFunction = getCirFunction(cirData, functionSignature, functionType);

                cirFunction.CecilSignature = methodReference.ToString();
                cirFunction.ReturnType = methodReference.ReturnType.ReturnType.FullName;
                cirFunction.ParentClass = getCirClass(cirData, CirFactoryUtils.getTypeUniqueSignatureFromTypeReference(methodReference.DeclaringType));
                cirFunction.ParentClassFullName = methodReference.DeclaringType.FullName;
                cirFunction.ParentClassName = methodReference.DeclaringType.Name;
                //
                cirFunction.Module = (methodReference.DeclaringType.Module != null) ? methodReference.DeclaringType.Module.Assembly.ToString() : "[NullModule]";
                //cirFunction.Module = (methodReference.DeclaringType.Module != null) ? methodReference.DeclaringType.Module.Name : "[NullModule]";
                cirFunction.FunctionName = methodReference.Name;
                cirFunction.FunctionNameAndParameters = CecilUtils.getMethodNameAndParameters(methodReference);                

                cirFunction.ClassNameFunctionNameAndParameters = string.Format("{0}.{1}",
                                                                               cirFunction.ParentClassFullName,
                                                                               cirFunction.FunctionNameAndParameters);
                cirFunction.SymbolDef = Guid.NewGuid().ToString();
                /*if (sequencePoint != null)
                {
                    if (string.IsNullOrEmpty(cirFunction.File) == false)
                    { 
                    }
                    cirFunction.File = sequencePoint.Document.Url;
                    cirFunction.FileLine = sequencePoint.StartColumn.ToString();
                }*/
                
               
                //methodReference.ReturnType  // to implement since we need to add reference to a CirClass
                return cirFunction;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactory.processMethodReference", true);
                return null;
            }

        }


        public ICirFunction processMethodDefinition(ICirData cirData, MethodDefinition methodDefinition, SequencePoint sequencePoint)
        {
            try
            {
                //var functionSignature = methodDefinition.ToString();
                var functionSignature = CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(methodDefinition);
                var functionClass = CirFactoryUtils.getTypeUniqueSignatureFromTypeReference(methodDefinition.DeclaringType);
                var cirFunction = getCirFunction(cirData, functionSignature, functionClass);
                if (false == cirFunction.HasBeenProcessedByCirFactory)
                {

                    if (methodDefinition.CustomAttributes != null && methodDefinition.CustomAttributes.Count > 0)
                    {
                        foreach (CustomAttribute customAttribute in methodDefinition.CustomAttributes)
                        {
                            var constructorSignature = CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(customAttribute.Constructor);
                            var cirAttribute = new CirAttribute(constructorSignature);
                            foreach (var constructorParameter in customAttribute.ConstructorParameters)
                            {
                                var type = constructorParameter.GetType().FullName;
                            }
                            if (customAttribute.Fields.Count > 0 || customAttribute.Properties.Count > 0)
                            {
                            }
                            PublicDI.log.debug("Added attribute {0} to {1}", customAttribute.Constructor.Name, cirFunction.FunctionName);
                            cirFunction.FunctionAttributes.Add(cirAttribute);
                        }
                    }
                    

                    // map the common values with MethodReference
                    processMethodReference(cirData, methodDefinition, sequencePoint);

                    cirFunction.HasBeenProcessedByCirFactory = true;  // we need to put this in here or we will have an infinite loop on recursive functions
                    cirFunction.HasControlFlowGraph = true;           // ControlFlowGraph is use by the Viewers to determine if we have more than just a reference to this method
                    cirFunction.ParentClass.bClassHasMethodsWithControlFlowGraphs = true;  // also mark the parent class

                    cirFunction.IsStatic = methodDefinition.IsStatic;
                    cirFunction.IsUnmanaged = methodDefinition.IsUnmanaged;
                    cirFunction.IsUnmanagedExport = methodDefinition.IsUnmanagedExport;
                    cirFunction.IsVirtual = methodDefinition.IsVirtual;
                    cirFunction.IsSetter = methodDefinition.IsSetter;
                    cirFunction.IsGetter = methodDefinition.IsGetter;
                    cirFunction.IsRuntime = methodDefinition.IsRuntime;
                    cirFunction.IsPublic = methodDefinition.IsPublic;
                    cirFunction.IsPrivate = methodDefinition.IsPrivate;
                    cirFunction.IsPInvokeImpl = methodDefinition.IsPInvokeImpl;
                    cirFunction.IsNative = methodDefinition.IsNative;
                    cirFunction.IsManaged = methodDefinition.IsManaged;
                    cirFunction.IsInternalCall = methodDefinition.IsInternalCall;
                    cirFunction.IsIL = methodDefinition.IsIL;
                    cirFunction.IsConstructor = methodDefinition.IsConstructor;
                    cirFunction.IsAbstract = methodDefinition.IsAbstract;
                    cirFunction.HasSecurity = methodDefinition.HasSecurity;
                    cirFunction.HasBody = methodDefinition.HasBody;

                    // try to find the location of the current method by going for the first line of the first method
                    if (methodDefinition.HasBody)
                        foreach (Instruction instruction in methodDefinition.Body.Instructions)
                            if (instruction.SequencePoint != null )
                            {
                                cirFunction.File = instruction.SequencePoint.Document.Url;
                                if (instruction.SequencePoint.StartLine == 16707566) // means there is no source code ref                                
                                    cirFunction.FileLine = "0";
                                else
                                    cirFunction.FileLine = instruction.SequencePoint.StartLine.ToString();
                                break;
                            }
                    
                    // map method parameters (this could be on the MethodReference but if so we would have to check for doing it more than once:
                    foreach (ParameterDefinition parameter in methodDefinition.Parameters)
                    {
                        ICirFunctionParameter functionParameter = new CirFunctionParameter
                        {
                            ParameterName = parameter.ToString(),
                            ParameterType = parameter.ParameterType.FullName,
                            Constant = (parameter.Constant != null) ? parameter.Constant.ToString() : "",
                            HasConstant = parameter.HasConstant,
                            HasDefault = parameter.HasDefault,
                            Method = parameter.Method.ToString()
                        };

                        cirFunction.FunctionParameters.Add(functionParameter);
                    }

                    // map the calls made and the IsCalledBy                   
                    foreach (var methodCalled in CecilUtils.getMethodsCalledInsideMethod(methodDefinition))
                    {
                        ICirFunction cirCalledFunction = processMemberReference(cirData, methodCalled.memberReference, methodCalled.sequencePoint);

                        if (cirCalledFunction != null)
                        {
                            // store the fucntion called sequence
                            cirFunction.FunctionsCalled.Add(new CirFunctionCall(cirCalledFunction,methodCalled.sequencePoint)); 
                            // store the unique list of funcions called
                            if (false == cirFunction.FunctionsCalledUniqueList.Contains(cirCalledFunction))
                                cirFunction.FunctionsCalledUniqueList.Add(cirCalledFunction);

                            // map the FunctionCalled and FunctionIsCalledBy

                            var cirFunctionCall = new CirFunctionCall(cirCalledFunction, sequencePoint);
                            //cirFunction.FunctionsCalled.Add(cirFunctionCall);                            
                            cirCalledFunction.FunctionIsCalledBy.Add(cirFunctionCall);

                            
                            //if (false == cirCalledFunction.FunctionIsCalledBy.Contains(cirFunction))
                            //    cirCalledFunction.FunctionIsCalledBy.Add(cirFunction);
                        }
                    }
                    
                }
                // to implement if needed
              /*  foreach (var methodOverride in methodDefinition.Overrides)
                {
                    var name = methodOverride.GetType();
                }*/

                return cirFunction;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactory.processMethodDefinition", true);
                return null;
            }            
        }

        

        public ICirClass processTypeDefinition(ICirData cirData, TypeDefinition typeDefinition)
        {
            try
            {
                var classSignature = CirFactoryUtils.getTypeUniqueSignatureFromTypeReference(typeDefinition);
                var cirClass = getCirClass(cirData, classSignature);

                
                if (typeDefinition.CustomAttributes != null && typeDefinition.CustomAttributes.Count > 0)
                {
                    foreach (CustomAttribute customAttribute in typeDefinition.CustomAttributes)
                    {
                        var constructorSignature = CirFactoryUtils.getFunctionUniqueSignatureFromMethodReference(customAttribute.Constructor);                        
                        var cirAttribute = new CirAttribute(constructorSignature);
                        foreach (var constructorParameter in customAttribute.ConstructorParameters)
                        {
//                            var type = constructorParameter.GetType().FullName;
                            cirAttribute.Parameters.Add(constructorParameter.ToString(), constructorParameter.GetType().FullName);
                        }
                        if (customAttribute.Fields.Count > 0 || customAttribute.Properties.Count > 0)
                        { 
                        }
                        cirClass.ClassAttributes.Add(cirAttribute);
                    }
                }

                
                if (false == cirClass.HasBeenProcessedByCirFactory)
                {
                    cirClass.HasBeenProcessedByCirFactory = true;
                    cirClass.Name = typeDefinition.Name;
                    cirClass.FullName = typeDefinition.FullName;
                    cirClass.Module = typeDefinition.Module.Name;
                    cirClass.Namespace = cirClass.Namespace;
                    cirClass.SymbolDef = Guid.NewGuid().ToString();
                    cirClass.IsAbstract = typeDefinition.IsAbstract;
                    //                cirClass.IsAnonymous = typeDefinition.
                    cirClass.IsClass = typeDefinition.IsClass;
                    cirClass.IsEnum = typeDefinition.IsEnum;
                    cirClass.IsInterface = typeDefinition.IsInterface;
                    cirClass.IsImport = typeDefinition.IsImport;
                    cirClass.IsNotPublic = typeDefinition.IsNotPublic;
                    cirClass.IsPublic = typeDefinition.IsPublic;
                    cirClass.HasSecurity = typeDefinition.HasSecurity;             
                }
                else
                    DI.log.info("This Class has already been processed, so only adding any possible extra methods: {0}", cirClass.Name);
                // handle constuctors

                foreach (MethodDefinition methodDefinition in typeDefinition.Constructors)
                {
                    ICirFunction cirFunction = processMethodDefinition(cirData, methodDefinition, null);
                    if (false == cirClass.dFunctions.ContainsValue(cirFunction))
                        cirClass.dFunctions.Add(cirFunction.FunctionSignature, cirFunction);

                    // try to find source code reference
                    if (cirFunction.FunctionsCalled.Count > 0 && cirClass.FileLine == null)
                        if (false == string.IsNullOrEmpty(cirFunction.FunctionsCalled[0].fileName))
                        {
                            cirClass.File = cirFunction.FunctionsCalled[0].fileName;
                            cirClass.FileLine = "0";    // set it to zero and try to map it later
                        }
                }
                // handle methods
                foreach (MethodDefinition methodDefinition in typeDefinition.Methods)
                {
                    ICirFunction cirFunction = processMethodDefinition(cirData, methodDefinition, null);
                    if (false == cirClass.dFunctions.ContainsValue(cirFunction))
                        cirClass.dFunctions.Add(cirFunction.FunctionSignature, cirFunction);

                }                
                return cirClass;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in CirFactory.processTypeDefinition", true);
                return null;
            }
        }
        public void loadAndMapSymbols(AssemblyDefinition assemblyDefinition, string assemblyPath)
        {
            try
            {
                if (assemblyPath != null)
                {
                    var pdbFile = assemblyPath.Replace(Path.GetExtension(assemblyPath), ".pdb");                    
                    if (File.Exists(pdbFile))
                    {
                        string unit = assemblyPath;
                        ModuleDefinition modDef = assemblyDefinition.MainModule;
                        var pdbFactory = new PdbFactory();
                        ISymbolReader reader = pdbFactory.CreateReader(modDef, unit);
                        modDef.LoadSymbols(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in loadAndMapSymbols: {0]", ex.Message);
            }            
        }
        
        public void processAssemblyDefinition(ICirData cirData, string assemblyPath)
        {
            processAssemblyDefinition(cirData, CecilUtils.getAssembly(assemblyPath), assemblyPath);
        }        

        public void processAssemblyDefinition(ICirData cirData, AssemblyDefinition assemblyDefinition, string assemblyPath)
        {
            if (cirData != null && assemblyDefinition != null)
            {
                var typesInAssembly = CecilUtils.getTypes(assemblyDefinition);
                loadAndMapSymbols(assemblyDefinition, assemblyPath);
                foreach (var typeInAssembly in typesInAssembly)
                    processTypeDefinition(cirData, typeInAssembly);
            }
            else
                DI.log.error("in processAssemblyDefinition, either cirData or assemblyDefinition was null");
        }
        

        internal string processAssemblyAndSaveAsCirDataFile(ICirData cirData, string fileToProcess, string directoryToSaveCirDataFile)
        {
            processAssemblyDefinition(cirData, fileToProcess);
            if (cirData.dClasses_bySignature.Count == 0)
                DI.log.error("There were no classes in created cirData file, so canceling save");
            else
            {
                Files.checkIfDirectoryExistsAndCreateIfNot(directoryToSaveCirDataFile);
                var savedFileName = Path.Combine(directoryToSaveCirDataFile,Path.GetFileName(fileToProcess) + ".CirData");
                CirDataUtils.saveSerializedO2CirDataObjectToFile(cirData, savedFileName);
                return savedFileName;
            }
            return "";
        }

        public string convertAssemblyIntoCirDataFile(string assemblyToProcess)
        {
            string targetDirectory = DI.config.O2TempDir;
            return convertAssemblyIntoCirDataFile(assemblyToProcess, targetDirectory);
            
        }

        public string convertAssemblyIntoCirDataFile(string assemblyToProcess, string targetDirectory)
        {
            ICirData cirData = new CirData();
            return processAssemblyAndSaveAsCirDataFile(cirData, assemblyToProcess, targetDirectory);            
        }

        public CirDataAnalysis createCirDataAnalysisObject(List<string> assembliesToLoad)
        {
            ICirData cirData = new CirData();            
            foreach(var assemblyToLoad in assembliesToLoad)
                processAssemblyDefinition(cirData, assemblyToLoad);
            if (cirData.dClasses_bySignature.Count >0)
            {
                return new CirDataAnalysis(cirData);
            }
            DI.log.error("in createCirDataAnalysisObject there were no clases in CirData file");
            return null;            
        }
    }
}
