using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Windows;

namespace O2.External.IKVM.IKVM
{
    public class IKVMUtils
    {
        public static string convertJarFileIntoDotNetAssembly(string pathToJarFile, string targetDirectory)
        {
            if (File.Exists(pathToJarFile) && Path.GetExtension(pathToJarFile) == ".jar")
            {
                var destinationFile = Path.Combine(targetDirectory,
                                                   Path.GetFileNameWithoutExtension(pathToJarFile) + ".dll");
                Files.deleteFile(destinationFile);
                var executionParameters = string.Format(IKVMConfig.IKVMCompilerArgumentsFormat, pathToJarFile,
                                                        destinationFile);
                var executionResult =
                    Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(IKVMConfig.IKVMCompilerExecutable,
                                                                                     executionParameters);
                if (File.Exists(destinationFile))
                    return destinationFile;
                
                DI.log.error("in IKVMUtils.convertJarToDotNetAssembly, Jar file was not Converted into .Net Dll");
            }
            else
                DI.log.error("in IKVMUtils.convertJarToDotNetAssembly, only jar files are supported");
            return "";
        }
    }
}
