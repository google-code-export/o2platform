using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.External.IKVM.IKVM;

namespace O2.External.IKVM.IKVM
{
    public class JavaExec
    {
        static JavaExec()
        {
            IKVMInstall.checkIKVMInstallation();
        }

        public static string executeJavaFile(string fileToExecute)
        {
                return executeJavaFile(fileToExecute, "");
        }

        public static string executeJavaFile(string fileToExecute, string arguments)
        {            
            var classToExecute = Path.GetFileNameWithoutExtension(fileToExecute);
            var classPath = Path.GetDirectoryName(fileToExecute);
            var executionArguments = string.Format(IKVMConfig.IKVMExecution_Script, classPath, classToExecute, arguments);
            return Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(IKVMConfig.IKVMExecutable, executionArguments);
        }        

        // if we pass a callback for logging we need to start a IKVM shell
        public static Process executeJavaFile(string fileToExecute, DataReceivedEventHandler dataReceivedCallBack)
        {            
            var IKVMShell = new JavaShell();
            IKVMShell.compileJavaFile(fileToExecute);
            IKVMShell.executeClassFile(dataReceivedCallBack);
            //IKVMShell.startJavaShell(dataReceivedCallBack, fileToExecute);
            return IKVMShell.IKVMProcess;            
        }
                        

        public static Process startIKVMShell(DataReceivedEventHandler dataReceivedCallBack)
        {
            var IKVMShell = new JavaShell();
            IKVMShell.startJavaShell(dataReceivedCallBack,"");
            return IKVMShell.IKVMProcess;            
        }


    }
}