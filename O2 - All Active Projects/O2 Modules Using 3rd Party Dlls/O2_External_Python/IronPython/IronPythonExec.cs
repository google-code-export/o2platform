// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.Diagnostics;

namespace O2.External.Python.IronPython
{
    public class IronPythonExec
    {
        public static string executePythonFile(string fileToExecute)
        {
            return executePythonFile(fileToExecute, "");
        }

        public static string executePythonFile(string fileToExecute, string arguments)
        {
            return new IronPythonShell().executePythonFile(fileToExecute, arguments);
        }

        // if we pass a callback for logging we need to start a python shell
        public static Process executePythonFile(string fileToExecute, DataReceivedEventHandler dataReceivedCallBack)
        {
            var ironPythonShell = new IronPythonShell();
            ironPythonShell.startIronPythonShell(dataReceivedCallBack, fileToExecute);
            return ironPythonShell.ironPythonProcess;            
        }
        
        public static string executePythonScript(string scriptToExecute)
        {
            return executePythonScripts(new List<string> {scriptToExecute});
        }
        public static string executePythonScripts(List<string> scriptsToExecute)
        {
            return new IronPythonShell().executeScript(scriptsToExecute);
        }

        public static void openIronPythonShellOnCmdExe()
        {
            IronPythonShell.openIronPythonShellOnCmdExe();
        }

        public static Process startIronPythonShell(DataReceivedEventHandler dataReceivedCallBack)
        {
            var jythonShell = new IronPythonShell();
            jythonShell.startIronPythonShell(dataReceivedCallBack);
            return jythonShell.ironPythonProcess;            
        }
    }
}
