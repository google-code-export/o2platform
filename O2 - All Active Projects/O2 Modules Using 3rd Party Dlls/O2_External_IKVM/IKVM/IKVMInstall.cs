// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.External.IKVM;

namespace O2.External.IKVM.IKVM
{
    class IKVMInstall
    {

        public static bool checkIKVMInstallation()
        {
            Files.checkIfDirectoryExistsAndCreateIfNot(IKVMConfig.jarStubsCacheDir);
            if (checkIfJavaPathIsCorrectlySet())
            {
                if (Directory.Exists(IKVMConfig._IKVMRuntimeDir) && File.Exists(IKVMConfig.IKVMCompilerExecutable) && File.Exists(IKVMConfig.IKVMStubExecutable))
                    return true;
                installIKVM();
                return (Directory.Exists(IKVMConfig._IKVMRuntimeDir) && File.Exists(IKVMConfig.IKVMCompilerExecutable) && File.Exists(IKVMConfig.IKVMStubExecutable));
            }
            return false;
        }

        public static void installIKVM()
        {

            if (false == File.Exists(IKVMConfig.zippedIKVMRunTime))
                DI.log.error("in installIKVM, could not find zippedIKVMRunTime: {0}", IKVMConfig.zippedIKVMRunTime);
            else
            {
                DI.log.info("Installing IKVM to: {0}", IKVMConfig._IKVMRuntimeDir);
                new zipUtils().unzipFile(IKVMConfig.zippedIKVMRunTime, IKVMConfig._IKVMRuntimeDir);
                if (Directory.Exists(IKVMConfig._IKVMRuntimeDir))
                    DI.log.info("IKVM sucessfully installed");
                else
                    DI.log.error("Problem installing/unziping _IKVMRuntimeDir: {0}", IKVMConfig._IKVMRuntimeDir);
                //JavaShell.testIKVM();
            }
        }

        public static bool checkIfJavaPathIsCorrectlySet()
        {
            var javaHome = Environment.GetEnvironmentVariable("Java_Home");
            if (string.IsNullOrEmpty(javaHome))
                DI.log.error("in checkIfJavaPathIsCorrectlySet, could not find Java_Home variable");
            else
            {
                var javaCompiler = Path.Combine(javaHome, @"bin\javac.exe");
                if (!File.Exists(javaCompiler))
                    DI.log.error("in checkIfJavaPathIsCorrectlySet, could not find javaCompiler executable: {0}", javaCompiler);
                {
                    
                    var javaParser = Path.Combine(javaHome, @"bin\javap.exe");
                    if (!File.Exists(javaParser))
                        DI.log.error("in checkIfJavaPathIsCorrectlySet, could not find javaParser executable: {0}",
                                     javaParser);
                    else
                    {
                        IKVMConfig.javaCompilerExecutable = javaCompiler;
                        IKVMConfig.javaParserExecutable = javaParser;

                        return true;
                    }
                }
            }            
            return false;
        }

        public static void uninstallIKVM()
        {
            if (Directory.Exists(IKVMConfig._IKVMRuntimeDir))
                Files.deleteFolder(IKVMConfig._IKVMRuntimeDir, true);
        }
        
    }
}
