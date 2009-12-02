using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using O2.DotNetWrappers.NetSDK;
using O2.DotNetWrappers.Windows;

namespace O2.External.O2Mono.MonoCecil
{
    public class StandAloneExe
    {        
        //public StandAloneExe(MethodInfo targetMethodInfo)
        //{
//
 //       }        
        public static string createMainPointingToMethodInfo(MethodInfo targetMethodInfo)
        {
            var exeName = targetMethodInfo.Name;

            var cecilAssemblyBuilder = new CecilAssemblyBuilder(exeName, AssemblyKind.Console);
            TypeDefinition tdType = cecilAssemblyBuilder.addType("O2StandAloneExe", "Program");
            MethodDefinition mdMain = cecilAssemblyBuilder.addMainMethod(tdType);
            cecilAssemblyBuilder.codeBlock_CallToMethod(mdMain, targetMethodInfo);
            var exeFileCreated = cecilAssemblyBuilder.Save(DI.config.O2TempDir);
            
            DI.log.info("Exe file created: {0}", exeFileCreated);
            return exeFileCreated;
        }

        public static string createPackagedDirectoryForMethod(MethodInfo targetMethod)
        {
            return createPackagedDirectoryForMethod(targetMethod, "");
        }

        public static string createPackagedDirectoryForMethod(MethodInfo targetMethod, string loadDllsFrom)
        {
            var standAloneExe = createMainPointingToMethodInfo(targetMethod);

            if (File.Exists(standAloneExe))
            {
                var createdDirectory = CecilAssemblyDependencies.populateDirectoryWithAllDependenciesOfAssembly(standAloneExe, loadDllsFrom);
                if (Directory.Exists(createdDirectory))
                {
                    var exeInCreatedDirectory = Path.Combine(createdDirectory, Path.GetFileName(standAloneExe));
                    if (File.Exists(exeInCreatedDirectory))
                    {
                        var newDirectoryName = DI.config.getTempFolderInTempDirectory(targetMethod.Name);
                        Files.copyFilesFromDirectoryToDirectory(createdDirectory, newDirectoryName);
                        Files.deleteFolder(createdDirectory);

                        var exeInFinalDirectory = Path.Combine(newDirectoryName, Path.GetFileName(standAloneExe));
                        var pdbFile = IL.createPdb(exeInFinalDirectory, false);
                        DI.log.debug("created temp exe for method execution: {0}", exeInFinalDirectory);
                        return exeInFinalDirectory;
                    }
                }
            }
            DI.log.error("in createPackagedDirectoryForMethod: could not create exe for method");
            return "";
        }
    }
}
