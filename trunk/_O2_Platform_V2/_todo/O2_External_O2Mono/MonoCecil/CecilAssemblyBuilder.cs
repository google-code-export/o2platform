// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using O2.External.O2Mono;
using MethodAttributes=Mono.Cecil.MethodAttributes;
using TypeAttributes=Mono.Cecil.TypeAttributes;

namespace O2.External.O2Mono.MonoCecil
{
    public class CecilAssemblyBuilder
    {
        public AssemblyDefinition assemblyDefinition;
        public AssemblyKind assemblyKind;
        public String assemblyName;
        public ModuleDefinition mainModule;

        public CecilAssemblyBuilder(String sAssemblyName)
        {
            CreateAssemblyDefinition(sAssemblyName, AssemblyKind.Dll);
            // default to dll since it dooesn't need an entry point
        }

        public CecilAssemblyBuilder(String sAssemblyName, AssemblyKind akAssemblyKind)
        {
            CreateAssemblyDefinition(sAssemblyName, akAssemblyKind);
        }

        private void CreateAssemblyDefinition(String sAssemblyName, AssemblyKind akAssemblyKind)
        {
            assemblyName = sAssemblyName;
            assemblyKind = akAssemblyKind;
            assemblyDefinition = AssemblyFactory.DefineAssembly(sAssemblyName, TargetRuntime.NET_2_0, akAssemblyKind);
            mainModule = assemblyDefinition.MainModule;
        }

        public void addDummyType()
        {
            addType("DummyNamespace", "DummyClass");
            // it seems that in order to clone types one needs to have already a class in there
        }

        public void addType(TypeDefinition typeToAdd)
        {
            mainModule.Types.Add(typeToAdd.Clone());
        }

        public TypeDefinition addType(String sTypeNameSpace, String sTypeName)
        {
            return addType(sTypeNameSpace, sTypeName, TypeAttributes.Class | TypeAttributes.Public,
                           getTypeReference(typeof (Object)));
        }

        public TypeDefinition addType(String sTypeNameSpace, String sTypeName, TypeAttributes taTypeAttributes,
                                      TypeReference trTypeReference)
        {
            var tdNewType = new TypeDefinition(sTypeName, sTypeNameSpace, taTypeAttributes, trTypeReference);
            mainModule.Types.Add(tdNewType);
            return tdNewType;
        }

        public MethodDefinition createMethod_StaticVoid(String sMethodName)
        {
            return createMethod(sMethodName, MethodAttributes.Public | MethodAttributes.Static,
                                getTypeReference(typeof (void)));
        }

        public MethodDefinition createMethod(String sMethodName, MethodAttributes maMethodAttributes,
                                             TypeReference trReturnType)
        {
            var newMethod = new MethodDefinition(sMethodName, maMethodAttributes, trReturnType);
            newMethod.Body.CilWorker.Emit(OpCodes.Ret);
            return newMethod;
        }

        public MethodDefinition addMainMethod(TypeDefinition tdTargetType)
        {
            MethodDefinition mainMethod = createMethod_StaticVoid("Main");
            tdTargetType.Methods.Add(mainMethod);
            assemblyDefinition.EntryPoint = mainMethod;
            return mainMethod;
        }


        public String Save(String targetFolderOrFileName)
        {
            if (Directory.Exists(targetFolderOrFileName))
                return Save(targetFolderOrFileName, assemblyName);
            return Save(DI.config.O2TempDir, targetFolderOrFileName);
        }


        public string Save(string sTargetDirectory, string fileName)
        {
            string fileToCreate = Path.Combine(sTargetDirectory, fileName);
            if (fileToCreate.IndexOf(".exe") == -1 && fileToCreate.IndexOf(".dll") == -1)
                switch (assemblyKind)
                {
                    case AssemblyKind.Console:
                    case AssemblyKind.Windows:
                        fileToCreate += ".exe";
                        break;
                    case AssemblyKind.Dll:
                        fileToCreate += ".dll";
                        break;
                }
            
            AssemblyFactory.SaveAssembly(assemblyDefinition, fileToCreate);
            
            return fileToCreate;
        }

        public TypeReference getTypeReference(Type tTypeToGet)
        {
            return mainModule.Import(tTypeToGet);
        }

        public MethodReference getMethodReference(MethodInfo miMethodInfo)
        {
            if (miMethodInfo != null)
                return mainModule.Import(miMethodInfo);

            return null;
        }


        public void codeBlock_ConsoleWriteLine(MethodDefinition mdTargetMethod, String sText)
        {
            CilWorker cwCliWorker = mdTargetMethod.Body.CilWorker;

            var lsInstructions = new List<Instruction>
                                     {
                                         cwCliWorker.Create(OpCodes.Ldstr, sText),
                                         cwCliWorker.Create(OpCodes.Call,
                                                            getMethodReference(
                                                                typeof (Console).GetMethod("WriteLine",
                                                                                           new[] {typeof (string)})))
                                     };

            CecilOpCodeUtils.addInstructionsToMethod_InsertAtEnd(mdTargetMethod, lsInstructions);
        }

        public void codeBlock_ReadLine(MethodDefinition mdTargetMethod)
        {
            CilWorker cwCliWorker = mdTargetMethod.Body.CilWorker;
            var lsInstructions = new List<Instruction>
                                     {
                                         cwCliWorker.Create(OpCodes.Call,
                                                            getMethodReference(typeof (Console).GetMethod("ReadLine",
                                                                                                          new Type[] {}))),
                                         cwCliWorker.Create(OpCodes.Pop)
                                     };
            CecilOpCodeUtils.addInstructionsToMethod_InsertAtEnd(mdTargetMethod, lsInstructions);
        }

        public void codeBlock_PressEnter(MethodDefinition mdTargetMethod)
        {
            codeBlock_ConsoleWriteLine(mdTargetMethod, "\nPress Enter");
            codeBlock_ReadLine(mdTargetMethod);
        }

        public void codeBlock_CallToMethod(MethodDefinition targetMethod, MethodInfo methodToCall)
        {
            CilWorker cliWorker = targetMethod.Body.CilWorker;
            var lsInstructions = new List<Instruction>
                                     {                                         
                                         cliWorker.Create(OpCodes.Call, getMethodReference(methodToCall))                                                            
                                     };
            CecilOpCodeUtils.addInstructionsToMethod_InsertAtEnd(targetMethod, lsInstructions);            
        }
    }
}
