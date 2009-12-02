// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.External.O2Mono.MonoCecil;
using Mono.Cecil;
using System.IO;
using O2.DotNetWrappers.Windows;
using System.Diagnostics;

namespace O2.Scanner.DotNet.PostSharp
{
    public class PostSharpUtils
    {

        //public static string sourceAssemblyWithO2PostSharpHooks = Path.Combine(DI.config.CurrentExecutableDirectory,"O2_External_PostSharp.dll");
        public static string sourceAssemblyWithO2PostSharpHooks = "O2_External_PostSharp.dll";
 
        public static void checkPostSharpHooks(string pathToAssembly)
        {
            DI.log.info("\nCheckPostSharpHooks for: {0}", Path.GetFileName(pathToAssembly));
         //   if (arePostSharpHooksInPlace(pathToAssembly))
         //       DI.log.info("   PostSharpHooksInPlace");
            if (arePostSharpAttributesInPlace(pathToAssembly))
                DI.log.info("   PostSharpAttributesInPlace");
            if (arePostSharpDllsAddedAsReferences(pathToAssembly))
                DI.log.info("   PostSharpDllsAddedAsReferences");

            if (containsO2PostSharpHooks(pathToAssembly))
                DI.log.info("   containsO2PostSharpHooks");
        }

        public static bool containsO2PostSharpHooks(string pathToAssembly)
        {            
            var assembly = CecilUtils.getAssembly(pathToAssembly);
            foreach (CustomAttribute customAttribute in assembly.CustomAttributes)
                if (customAttribute.Constructor.DeclaringType.Name == "O2PostSharpHookAttribute")
                    return true;
            return false;
        }

        //public static bool arePostSharpHooksInPlace(string pathToAssembly)
        //{
        //    return false;
        //}

        public static bool arePostSharpAttributesInPlace(string pathToAssembly)
        {
            var assembly = CecilUtils.getAssembly(pathToAssembly);
            foreach (CustomAttribute customAttribute in assembly.CustomAttributes)
            {
                var typeName = customAttribute.Constructor.DeclaringType.Name;
                if (typeName == "XTraceMethodInvocationAttribute" || typeName == "XTraceMethodBoundaryAttribute") 
                    return true;
                //DI.log.info("   c: {0}", customAttribute.Constructor.DeclaringType.Name);
                }
            return false;
        }

        public static bool arePostSharpDllsAddedAsReferences(string pathToAssembly)
        {
            var dependencies = CecilAssemblyDependencies.getListOfDependenciesForAssemblies(new List<string> { pathToAssembly });
            foreach (var dependency in dependencies)
                if (dependency == "PostSharp.Public")//if (Path.GetFileName(dependency) == "PostSharp.Laos.dll")
                    return true;
            return false;
        }

        public static void copyPostSharpDependenciesToFolder(string pathToFileOrfolder)
        {
            if (File.Exists(pathToFileOrfolder))
                pathToFileOrfolder = Path.GetDirectoryName(pathToFileOrfolder);
            if (false == Directory.Exists(pathToFileOrfolder))            
                DI.log.error("In copyPostSharpDependenciesToFolder, Target directory does not exist: {0}", pathToFileOrfolder);
            else
            {
                var postSharpLaosFile = Path.Combine(pathToFileOrfolder, "PostSharp.Laos.dll");
                var postSharpPublicFile = Path.Combine(pathToFileOrfolder, "PostSharp.Public.dll");
                if (false == File.Exists(postSharpLaosFile))
                    Files.WriteFileContent(postSharpLaosFile, SuportFiles.PostSharp_Laos);
                if (false == File.Exists(postSharpPublicFile))
                    Files.WriteFileContent(postSharpPublicFile, SuportFiles.PostSharp_Public);

                if (false == File.Exists(sourceAssemblyWithO2PostSharpHooks))
                    DI.log.error("Could not find assemblyWithO2PostSharpHooks: {0}", sourceAssemblyWithO2PostSharpHooks);
                else
                {
                    var targetAssemblyWithO2PostSharpHooks = Path.Combine(pathToFileOrfolder,sourceAssemblyWithO2PostSharpHooks);
                    //if (false == File.Exists(targetAssemblyWithO2PostSharpHooks))
                        File.Copy(sourceAssemblyWithO2PostSharpHooks, targetAssemblyWithO2PostSharpHooks,true);
                }
            }
        
        }

/*        public static void showAttributes(string assemblyName)
        {
            var assembly = CecilUtils.getAssembly(assemblyName);
            if (assembly != null)
            {
            }
        }

        public static void showAttributes(AssemblyDefinition assembly)
        {
            DI.log.info("------ Showing Attributes for Assembly: " + assembly.Name + " \n");
            foreach (var attribute in assembly.SecurityDeclarations)
                DI.log.info("SecurityDeclarations");
            foreach (CustomAttribute attribute in assembly.CustomAttributes)
                DI.log.info("CustomAttribute: " + attribute.Constructor.ToString());

        }            
  */              

    }
}
