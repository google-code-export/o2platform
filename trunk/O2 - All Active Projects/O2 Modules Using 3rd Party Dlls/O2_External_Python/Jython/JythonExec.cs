// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Windows;
using O2.External.Python.Jython;

namespace O2.External.Python.Jython
{
    public class JythonExec
    {
        public static string executePythonFile(string fileToExecute)
        {
            return executePythonFile(fileToExecute, "");
        }
        public static string executePythonFile(string fileToExecute, string arguments)
        {
            return new JythonShell().executePythonFile(fileToExecute, arguments);
        }

        // if we pass a callback for logging we need to start a python shell
        public static Process executePythonFile(string fileToExecute, DataReceivedEventHandler dataReceivedCallBack)
        {
            var jythonShell = new JythonShell();
            jythonShell.startJythonShell(dataReceivedCallBack, fileToExecute);
            return jythonShell.jythonProcess;
        }

        public static string executePythonScript(string scriptToExecute)
        {
            return executePythonScripts(new List<string> {scriptToExecute});
        }
        public static string executePythonScripts(List<string> scriptsToExecute)
        {
            return new JythonShell().executeScript(scriptsToExecute);
        }

        public static void openJythonShellOnCmdExe()
        {
            JythonShell.openJythonShellOnCmdExe();
        }

        public static Process startJythonShell(DataReceivedEventHandler dataReceivedCallBack)
        {
            var jythonShell = new JythonShell();
            jythonShell.startJythonShell(dataReceivedCallBack);
            return jythonShell.jythonProcess;            
        }
    }
}
