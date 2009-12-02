using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.External.Python.IronPython
{
    public class IronPythonShell
    {
        public string dataReceived;
        public StreamWriter inputStream;
        public Process ironPythonProcess;
        //public int maxWaitTimeForJytonCommandExecution = 1000;      // in MSeconds

        private const string IronPython_CMD_EXIT = "exit()";        

        public string executePythonFile(string pythonFileToExecute, string pythonScriptArguments)
        {
            var executionArguments = string.Format(IronPythonConfig.IronPythonJavaExecution_PythonScript,pythonFileToExecute, pythonScriptArguments);
            return Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput(IronPythonConfig.ironPythonExecutable, executionArguments);
        }

        public bool startIronPythonShell()
        {
            return startIronPythonShell(_dataReceivedCallBack);
        }

        public bool startIronPythonShell(DataReceivedEventHandler dataReceivedCallBack)
        {
            return startIronPythonShell(dataReceivedCallBack, "");
        }

        public bool startIronPythonShell(DataReceivedEventHandler dataReceivedCallBack, string executionArguments)
        {
            try
            {
                //var executionArguments = string.Format(IronPythonConfig.IronPythonJavaExecution_PythonShell,
                //                                       IronPythonConfig._IronPythonRuntimeDir);

                dataReceived = null;
                inputStream = null;
                ironPythonProcess = null;
                ironPythonProcess = Processes.startProcessAndRedirectIO(IronPythonConfig.ironPythonExecutable, executionArguments, ref inputStream,
                                                                        dataReceivedCallBack, dataReceivedCallBack);
                if (inputStream != null)
                    return true;

                DI.log.error("in startIronPythonShell , IronPythonProcess.inputStream == null");
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
            if (startIronPythonShell())
            {
                DI.log.debug("IronPython Executing {0} Script(s)", scriptsToExecute.Count);
                foreach (var scriptToExecute in scriptsToExecute)
                    writeScriptToIronPythonShell(scriptToExecute);
                exitfromIronPythonShell();
                DI.log.info("IronPython Execution: Data Received: {0}", dataReceived);
                return dataReceived;
            }            
            return "";
        }

        public void writeScriptToIronPythonShell(string scriptToWrite)
        {
            inputStream.WriteLine(scriptToWrite);
        }

        public void exitfromIronPythonShell()
        {
            inputStream.WriteLine(IronPython_CMD_EXIT);
            ironPythonProcess.WaitForExit();
        }        

        private void _dataReceivedCallBack(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data) == false)
            {
                var data = e.Data.Replace(">>> ", "");
                if (data != "IronPython 2.0 (2.0.0.0) on .NET 2.0.50727.3053" &&
                    data != "Type \"help\", \"copyright\", \"credits\" or \"license\" for more information." &&
                    data != "")
                {
                    dataReceived += data + Environment.NewLine;
                }
            }
        }

        public static bool testIronPython()
        {
            return testIronPython("print 2+3", "5");
        }

        public static bool testIronPython(string scriptToExecute, string expectedDataReceived)
        {
            var IronPythonExecution = new IronPythonShell();            
            var dataReceived = IronPythonExecution.executeScript(scriptToExecute);
            return dataReceived == expectedDataReceived;
        }

        internal static Process openIronPythonShellOnCmdExe()
        {
            return Processes.startProcess(IronPythonConfig.ironPythonExecutable, "-D -X:TabCompletion -X:ColorfulConsole");
        }
    }
}