using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.External.O2Mono.MonoCecil;
using Mono.Cecil;
using System.IO;
using O2.External.PostSharp.PostsharpCallbacks;

namespace O2.Scanner.DotNet.PostSharp
{
    public class InjectAttributes
    {
        //public static void addAttributes(string assemblyName, string typeToHook, string methodToHook)
        //{

        public static void InsertHooks(string sourceAssembly, string targetAssembly, string typeToHook, string methodToHook,  bool backupFile)
        {
            // if requested back up the current targetAssembly
            if (backupFile && false == BackupRestoreFiles.doesBackupExist(sourceAssembly))
                BackupRestoreFiles.backup(sourceAssembly);
            if (sourceAssembly != targetAssembly)
                File.Copy(sourceAssembly, targetAssembly); 
                  
            DI.log.info("Adding an attribute to current Assembly");
            var assembly = CecilUtils.getAssembly(targetAssembly);

            addO2PostSharpHookAttribute(assembly);

            if (typeToHook == "" && methodToHook == "")
                addOnMethodInvocationttribute(assembly, assembly.Name.Name, "*");
            else
                if (typeToHook != "" && methodToHook == "")
                    addOnMethodInvocationttribute(assembly, assembly.Name.Name, "*", typeToHook);
                else
                    addOnMethodInvocationttribute(assembly, assembly.Name.Name, "*", typeToHook, methodToHook);
            
            //addOnMethodBoundaryAttribute(assembly, assembly.Name.Name, "*");
            //addOnMethodInvocationttribute(assembly, assembly.Name.Name, "*");            

            //var fileName = Path.GetFileName(targetAssembly);
            //var savedAssemby = assemblyName.Replace(fileName, fileName);

            //var savedAssemby = assemblyName;

            // remove publicKey
            assembly.Name.PublicKey = null;

            AssemblyFactory.SaveAssembly(assembly, targetAssembly);

            DI.log.info("\n  _ Assembly saved to: " + targetAssembly + " \n");

            Hacks.patchAssemblyForCecilPostSharpBug(targetAssembly);
            
        }

        public static void addO2PostSharpHookAttribute(string targetAssembly)
        {
            var assembly = CecilUtils.getAssembly(targetAssembly);

            addO2PostSharpHookAttribute(assembly);

            AssemblyFactory.SaveAssembly(assembly, targetAssembly);
        }

        public static void addO2PostSharpHookAttribute(AssemblyDefinition assembly)
        {
            addCustomAttributeToAssembly(assembly,typeof(O2PostSharpHookAttribute), null);
        }

        public static void addOnMethodBoundaryAttribute(AssemblyDefinition assembly, string targetAssemblyName, string targetNamespace)
        {
            addCustomAttributeToAssembly(assembly, typeof(OnMethodBoundaryAttribute),
                                                                 new Dictionary<System.String, System.String>() { 
                                                     { "AttributeTargetAssemblies", targetAssemblyName},    // "O2_Cmd_FindingsFilter"},
                                                     { "AttributeTargetTypes", targetNamespace}             //"O2.*"}
                                                     });
 
        }

        public static void addOnMethodInvocationttribute(AssemblyDefinition assembly, string targetAssemblyName, string targetNamespace)
        {
            addCustomAttributeToAssembly(assembly, typeof(OnMethodInvocationAttribute),
                                                                 new Dictionary<System.String, System.String>() { 
                                                     { "AttributeTargetAssemblies", targetAssemblyName},    // "O2_Cmd_FindingsFilter"},
                                                     { "AttributeTargetTypes", targetNamespace}             //"O2.*"}
                                                     });
 
        }

        public static void addOnMethodInvocationttribute(AssemblyDefinition assembly, string targetAssemblyName, string targetNamespace, string targetType)
        {
            addCustomAttributeToType(assembly, targetType, typeof(OnMethodInvocationAttribute),
                                                                 new Dictionary<System.String, System.String>() { 
                                                     { "AttributeTargetAssemblies", targetAssemblyName},    // "O2_Cmd_FindingsFilter"},
                                                     { "AttributeTargetTypes", targetNamespace}             //"O2.*"}
                                                     });

        }

        public static void addOnMethodInvocationttribute(AssemblyDefinition assembly, string targetAssemblyName, string targetNamespace, string targetType, string targetMethod)
        {
            addCustomAttributeToMethod(assembly, targetType, targetMethod, typeof(OnMethodInvocationAttribute),
                                                                 new Dictionary<System.String, System.String>() { 
                                                     { "AttributeTargetAssemblies", targetAssemblyName},    // "O2_Cmd_FindingsFilter"},
                                                     { "AttributeTargetTypes", targetNamespace}             //"O2.*"}
                                                     });

        }

        
        /*public static void addXTraceMethodInvocationAttribute(AssemblyDefinition assembly, string targetAssemblyName, string targetNamespace)
        {
            addCustomAttributeToAssembly(assembly, typeof(OnMethodInvocationAttribute),
                                         new Dictionary<System.String, System.String>() { 
                                         { "AttributeTargetAssemblies", targetAssemblyName}, // "mscorlib"},
                                         { "AttributeTargetTypes", targetNamespace} //"System.Console.*"}
                                         });
        }*/

        public static void addCustomAttributeToAssembly(AssemblyDefinition assembly, Type attributeType, Dictionary<System.String, System.String> fields)
        {
            var customAttribute = new CustomAttribute(
                    assembly.MainModule.Import(attributeType.GetConstructor(new Type[] { })));
            if (fields!=null)
                foreach (var field in fields)
                {
                    //customAttribute.SetFieldType(field.Key, assembly.MainModule.Import(typeof(System.String)));
                    customAttribute.SetFieldType(field.Key, assembly.MainModule.Import(field.Value.GetType()));
                    customAttribute.Fields[field.Key] = field.Value;
                }
            assembly.CustomAttributes.Add(customAttribute);
        }

        public static void addCustomAttributeToType(AssemblyDefinition assembly,string typeToAdd, Type attributeType, Dictionary<System.String, System.String> fields)
        {
            var targetType = CecilUtils.getType(assembly, typeToAdd);
            if (targetType == null)
            {
                DI.log.error("in addCustomAttributeToType: could not find type {0} in assembly {1}", typeToAdd, assembly.Name.Name);
            }
            else
            {
                var customAttribute = new CustomAttribute(
                        assembly.MainModule.Import(attributeType.GetConstructor(new Type[] { })));
                if (fields != null)
                    foreach (var field in fields)
                    {
                        //customAttribute.SetFieldType(field.Key, assembly.MainModule.Import(typeof(System.String)));
                        customAttribute.SetFieldType(field.Key, assembly.MainModule.Import(field.Value.GetType()));
                        customAttribute.Fields[field.Key] = field.Value;
                    }
                targetType.CustomAttributes.Add(customAttribute);
            }        
            //assembly.CustomAttributes.Add(customAttribute);
        }


        public static void addCustomAttributeToMethod(AssemblyDefinition assembly, string methodType,string methodToAdd, Type attributeType, Dictionary<System.String, System.String> fields)
        {
            var targetType = CecilUtils.getType(assembly, methodType);
            //var targetType = CecilUtils.getMethodsassembly,methodType, typeToAdd);
            if (targetType == null)
            {
                DI.log.error("in addCustomAttributeToType: could not find type {0} in assembly {1}", methodType, assembly.Name.Name);
            }
            else
            {
                var methods = CecilUtils.getMethods(targetType);
                foreach (var method in methods)
                {
                    if (method.Name == methodToAdd)
                    {
                        var customAttribute = new CustomAttribute(
                                assembly.MainModule.Import(attributeType.GetConstructor(new Type[] { })));
                        if (fields != null)
                            foreach (var field in fields)
                            {
                                //customAttribute.SetFieldType(field.Key, assembly.MainModule.Import(typeof(System.String)));
                                customAttribute.SetFieldType(field.Key, assembly.MainModule.Import(field.Value.GetType()));
                                customAttribute.Fields[field.Key] = field.Value;
                            }
                        method.CustomAttributes.Add(customAttribute);
                    }
                }
            }
            //assembly.CustomAttributes.Add(customAttribute);
        }        
    }
}
