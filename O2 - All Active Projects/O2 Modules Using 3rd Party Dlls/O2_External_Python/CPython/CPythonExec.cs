using System.Collections.Generic;
using System.Diagnostics;
using O2.External.Python.CPython;

namespace O2.External.Python.CPython
{
    public class CPythonExec
    {
        public static string executePythonFile(string fileToExecute)
        {
            return executePythonFile(fileToExecute, "");
        }

        public static string executePythonFile(string fileToExecute, string arguments)
        {
            return new CPythonShell().executePythonFile(fileToExecute, arguments);
        }

        // if we pass a callback for logging we need to start a python shell
        public static Process executePythonFile(string fileToExecute, DataReceivedEventHandler dataReceivedCallBack)
        {
            var cpythonShell = new CPythonShell();
            cpythonShell.startCPythonShell(dataReceivedCallBack, fileToExecute);
            return cpythonShell.cpythonProcess;
        }

        public static string executePythonScript(string scriptToExecute)
        {
            return executePythonScripts(new List<string> {scriptToExecute});
        }
        public static string executePythonScripts(List<string> scriptsToExecute)
        {
            return new CPythonShell().executeScript(scriptsToExecute);
        }

        public static void openCPythonShellOnCmdExe()
        {
            CPythonShell.openCPythonShellOnCmdExe();
        }

        public static Process startCPythonShell(DataReceivedEventHandler dataReceivedCallBack)
        {
            var cpythonShell = new CPythonShell();
            cpythonShell.startCPythonShell(dataReceivedCallBack);
            return cpythonShell.cpythonProcess;
        }
    }
}