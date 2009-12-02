// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Windows;

namespace O2.External.IKVM.IKVM
{
    public class JavaCompile
    {

        static JavaCompile()
        {
            IKVMInstall.checkIfJavaPathIsCorrectlySet();
        }

        public static string compileJavaFile(string fileToCompile)
        {
            var expectedClassFile = fileToCompile.Replace(".java", ".class");
            if (Files.deleteFile(expectedClassFile))
            {
                var javaCompilerArgumentsFormat = "-classpath \"{0}\\*\" \"{1}\"";                
                var compilationArguments = string.Format(javaCompilerArgumentsFormat, IKVMConfig.jarStubsCacheDir,
                                                         fileToCompile); //IKVMConfig.javaCompilerArguments
                var processExecResult = Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(IKVMConfig.javaCompilerExecutable,
                                                                                     compilationArguments);
                DI.log.info("Compilation result: {0}", processExecResult);
                if (File.Exists(expectedClassFile))
                    return expectedClassFile;
            }
            return "";
        }

        public static string createJarStubForDotNetDll(string dllToConvert, string targetDirectory)
        {
            if (IKVMInstall.checkIKVMInstallation())
            {
                var processExecutionArguments = string.Format("\"{0}\"", dllToConvert);
                var processExecResult =
                    Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(IKVMConfig.IKVMStubExecutable,
                                                                                     processExecutionArguments,targetDirectory,  true);
                DI.log.info(processExecResult);


                /*var createdJarFile =
                    Path.Combine(Path.GetDirectoryName(dllToConvert), Path.GetFileNameWithoutExtension(dllToConvert)) +
                    ".jar";
                */
                var createdJarFile = Path.Combine(targetDirectory, Path.GetFileNameWithoutExtension(dllToConvert)) +".jar";
                if (File.Exists(createdJarFile))
                {
                    //var jarFileInTargetDirectory = Files.Copy(createdJarFile, targetDirectory);
                    DI.log.info("Created Jar file: {0}", createdJarFile);
                    return (createdJarFile);
                }
                DI.log.info("Was not able to create Jar file for dll: {0}", dllToConvert);
            }
            return "";
        }
    }
}
