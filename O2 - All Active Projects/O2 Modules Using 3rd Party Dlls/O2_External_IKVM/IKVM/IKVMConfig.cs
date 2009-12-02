// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.External.IKVM;
using O2.External.IKVM.IKVM;
using O2.DotNetWrappers.Windows;

namespace O2.External.IKVM.IKVM
{
    public class IKVMConfig
    {
        /*static IKVMConfig()
        {
            IKVMInstall.checkIKVMInstallation();
        }*/

        public static string _IKVMRuntimeDir = Path.Combine(DI.config.O2TempDir, "_IKVM_Runtime"); //_IKVM_Runtime
        public static string zippedIKVMRunTime = "_IKVM_Runtime.zip";
        public static string IKVMExecutable = Path.Combine(_IKVMRuntimeDir, "ikvm.exe");
        public static string IKVMCompilerExecutable = Path.Combine(_IKVMRuntimeDir, "ikvmc.exe");
        public static string IKVMCompilerArgumentsFormat = "\"{0}\" -out:\"{1}\"";
        public static string IKVMStubExecutable = Path.Combine(_IKVMRuntimeDir, "ikvmstub.exe");
        public static string IKVMExecution_Script = "-classpath \"{0};\" {1} {2}";
        public static string jarStubsCacheDir = Path.Combine(_IKVMRuntimeDir, "jarStubsCacheDir");
        public static string convertedJarsDir = Path.Combine(_IKVMRuntimeDir, "convertedJarsDir");        
        public static string javaCompilerExecutable ="";
        public static string javaParserExecutable = "";
        

        public static string IKVMInstallationDir
        {
            get
            {
                IKVMInstall.checkIKVMInstallation();
                return _IKVMRuntimeDir;
            }
            set
            {
                IKVMInstall.uninstallIKVM();
                _IKVMRuntimeDir = value;
                IKVMInstall.installIKVM();
            }
        }


    }
}
