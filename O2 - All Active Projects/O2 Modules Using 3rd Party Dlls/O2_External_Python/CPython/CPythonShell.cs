// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.External.Python.CPython
{
    public class CPythonShell
    {
        public string dataReceived;
        public StreamWriter inputStream;
        public Process cpythonProcess;
        //public int maxWaitTimeForJytonCommandExecution = 1000;      // in MSeconds

        private const string CPython_CMD_EXIT = "exit()";        
        

        public string executePythonFile(string pythonFileToExecute, string pythonScriptArguments)
        {            
            var executionArguments = string.Format(CPythonConfig._CPythonScript,
                                                   CPythonConfig._CPythonRuntimeDir, pythonFileToExecute, pythonScriptArguments);
            return Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput("java.exe", executionArguments);
        }

        public bool startCPythonShell()
        {
            return startCPythonShell(_dataReceivedCallBack);
        }

        public bool startCPythonShell(DataReceivedEventHandler  dataReceivedCallBack)
        {
            return startCPythonShell(dataReceivedCallBack, "");
        }

        public bool startCPythonShell(DataReceivedEventHandler  dataReceivedCallBack, string executionArguments)
        {
            try
            {
                //var executionArguments = string.Format(CPythonConfig._CPythonShell,
                //                                                       CPythonConfig._CPythonRuntimeDir);

                dataReceived = null;
                inputStream = null;
                cpythonProcess = null;
                cpythonProcess = Processes.startProcessAndRedirectIO(CPythonConfig._CPythonExecutable, executionArguments, ref inputStream,
                                                                     dataReceivedCallBack, dataReceivedCallBack);
                if (inputStream != null)
                    return true;

                DI.log.error("in startCPythonShell , CPythonProcess.inputStream == null");
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
            }
            return false;
        }


        public string executeScript(string scriptToExecute)
        {
            return executeScript(new List<string>{scriptToExecute});
        }

        public string executeScript(List<string> scriptsToExecute)
        {
            if (startCPythonShell())
            {
                DI.log.debug("CPython Executing {0} Script(s)", scriptsToExecute.Count);
                foreach (var scriptToExecute in scriptsToExecute)
                    writeScriptToCPythonShell(scriptToExecute);
                exitfromCPythonShell();
                DI.log.info("CPython Execution: Data Received: {0}", dataReceived);
                return dataReceived;
            }            
            return "";
        }

        public void writeScriptToCPythonShell(string scriptToWrite)
        {
            inputStream.WriteLine(scriptToWrite);
        }

        public void exitfromCPythonShell()
        {
            inputStream.WriteLine(CPython_CMD_EXIT);
            cpythonProcess.WaitForExit();
        }        

        private void _dataReceivedCallBack(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data) == false)
            {
                dataReceived += e.Data;
            }
        }



        public static bool testCPython()
        {
            return testCPython("print 2+3", "5");
        }

        public static bool testCPython(string scriptToExecute, string expectedDataReceived)
        {
            var CPythonExecution = new CPythonShell();            
            var dataReceived = CPythonExecution.executeScript(scriptToExecute);
            return dataReceived == expectedDataReceived;
        }

        public static Process openCPythonShellOnCmdExe()
        {            
            return Processes.startProcess(CPythonConfig._CPythonExecutable, "");            
        }
        
    }
}
