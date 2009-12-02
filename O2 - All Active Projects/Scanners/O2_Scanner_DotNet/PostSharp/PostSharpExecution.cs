using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Extensibility;
using PostSharp;
using System.IO;

namespace O2.Scanner.DotNet.PostSharp
{
    public class PostSharpExecution
    {
        // method based on the internal method of PostSharp.Core.dll 
        // PostSharp.Extensibility.CommandLineProgram
        // private static CommandLineReturnCode InternalMain(string[] args)

        public static bool runPostSharpOnAssembly(string targetAssembly)
        {
            return runPostSharpOnAssembly(targetAssembly, true);
        }

        public static bool runPostSharpOnAssembly(string targetAssembly, bool createBackup)
        {
            try
            {
                var tempSourceFile = targetAssembly + ".tmp";
                var fileName = targetAssembly; 
                if (createBackup)
                {                    
                    O2.DotNetWrappers.Windows.Files.MoveFile(targetAssembly, tempSourceFile);
                    //File.Move(targetAssembly, tempSourceFile);      // we need to do this because the postSharp injection is locking the targetAssembly
                    
                    if (false == (File.Exists(tempSourceFile) && false == File.Exists(targetAssembly)))
                    {
                        DI.log.error("Error in runPostSharpOnAssembly, something went wrong with file move");
                        return false;
                    }
                    fileName = tempSourceFile;
                }

                var projectConfigurationFile = @"C:\_DinisTest\tools\PostSharp 1.5\Default.psproj";

                PostSharpObjectSettings settings = new PostSharpObjectSettings();
                settings.CreatePrivateAppDomain = true;
                settings.Settings["Trace"] = "false";

                ApplicationInfo.SetSetting("Trace", "false");

                PropertyCollection properties = new PropertyCollection();

                var tempOutputFile = DI.config.getTempFileInTempDirectory(".exe");
                properties["ResolvedReferences"] = ".";
                properties["SearchPath"] = ".";
                properties["Output"] = targetAssembly; //+".exe"; // @"C:\O2\_tempDir\PS_XTraceTest.exe";
                properties["IntermediateDirectory"] = @"C:\O2\_tempDir";
                properties["CleanIntermediate"] = "true";
                properties["SignAssembly"] = "false";
                properties["PrivateKeyLocation"] = ".";

                ProjectInvocationParameters invocationParameters =
                    new ProjectInvocationParameters(projectConfigurationFile);
                invocationParameters.Properties.Merge(properties);
                ModuleLoadStrategy moduleLoadStrategy = settings.DisableReflection
                                                            ? ((ModuleLoadStrategy)
                                                               new ModuleLoadDirectFromFileStrategy(fileName))
                                                            : ((ModuleLoadStrategy)
                                                               new ModuleLoadReflectionFromFileStrategy(fileName));
                using (IPostSharpObject obj2 = PostSharpObject.CreateInstance(settings, null))
                {
                    obj2.InvokeProject(new ProjectInvocation(invocationParameters, moduleLoadStrategy));
                }
                if (createBackup)
                {
                    File.Delete(tempSourceFile);
                    // if we want to be able to delete this file we need to set settings.CreatePrivateAppDomain = true;
                    if (false == File.Exists(tempSourceFile) && File.Exists(targetAssembly))
                        return true;
                }
                else                
                    return true;                            
            }
            catch (Exception ex)
            {
                DI.log.info("Error in runPostSharpOnAssembly: {0}", ex.Message);
            }
            return false;
        }

        public static bool InsertHooksAndRunPostSharpOnAssembly(string targetAssembly)        
        {
            return InsertHooksAndRunPostSharpOnAssembly(targetAssembly,targetAssembly);
        }
               
        public static bool InsertHooksAndRunPostSharpOnAssembly(string sourceAssembly, string targetAssembly)
        {
            return InsertHooksAndRunPostSharpOnAssembly(sourceAssembly, targetAssembly, "", "");
        }

        public static bool InsertHooksAndRunPostSharpOnAssembly(string targetAssembly, string typeToHook, string methodToHook)
        {
            return InsertHooksAndRunPostSharpOnAssembly(targetAssembly, targetAssembly, typeToHook, methodToHook);
        }

        public static bool InsertHooksAndRunPostSharpOnAssembly(string sourceAssembly, string targetAssembly, string typeToHook, string methodToHook)
        {
            if (false == File.Exists(sourceAssembly))
            {
                DI.log.error("sourceAssembly not found: {0}", sourceAssembly);
                return false;
            }
            if (PostSharpUtils.containsO2PostSharpHooks(sourceAssembly))
                if (false == BackupRestoreFiles.restore(sourceAssembly))
                {
                    DI.log.error("Aborting PostSharp O2 Hook Insert, since they are already there and there was no backup: {0}", targetAssembly);
                    return false;
                }
            InjectAttributes.InsertHooks(sourceAssembly, targetAssembly, typeToHook, methodToHook, true);
            if (PostSharpExecution.runPostSharpOnAssembly(targetAssembly))                
                return true;

            // if it fails, restore the source assembly
            BackupRestoreFiles.restore(sourceAssembly);
            return false;                
        }
    }
}
