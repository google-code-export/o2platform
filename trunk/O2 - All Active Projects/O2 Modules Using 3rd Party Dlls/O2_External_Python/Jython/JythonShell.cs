// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.External.Python.Jython
{
    public class JythonShell
    {
        public string dataReceived;
        public StreamWriter inputStream;
        public Process jythonProcess;
        //public int maxWaitTimeForJytonCommandExecution = 1000;      // in MSeconds

        private const string JYTHON_CMD_EXIT = "exit()";        
        

        public string executePythonFile(string pythonFileToExecute, string pythonScriptArguments)
        {
            JythonInstall.checkJythonInstallation();
            var executionArguments = string.Format(JythonConfig.jythonJavaExecution_PythonScript,
                                                   JythonConfig._jythonRuntimeDir, pythonFileToExecute, pythonScriptArguments);
            return Processes.startProcessAsConsoleApplicationAndReturnConsoleOutput("java.exe", executionArguments);
        }

        public bool startJythonShell()
        {
            return startJythonShell(_dataReceivedCallBack);
        }

        public bool startJythonShell(DataReceivedEventHandler  dataReceivedCallBack)
        {
            return startJythonShell(dataReceivedCallBack,"");
        }

        public bool startJythonShell(DataReceivedEventHandler  dataReceivedCallBack , string extraExecutionArguments)
        {
            try
            {
                var executionArguments = string.Format(JythonConfig.jythonJavaExecution_PythonShell,
                                                       JythonConfig._jythonRuntimeDir);
                if (extraExecutionArguments!="")
                    executionArguments += " " + extraExecutionArguments;
                dataReceived = null;
                inputStream = null;
                jythonProcess = null;
                jythonProcess = Processes.startProcessAndRedirectIO("java.exe", executionArguments, ref inputStream,
                                                                    dataReceivedCallBack, dataReceivedCallBack);
                if (inputStream != null)
                    return true;

                DI.log.error("in startJythonShell , jythonProcess.inputStream == null");
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
            if (startJythonShell())
            {
                DI.log.debug("Jython Executing {0} Script(s)", scriptsToExecute.Count);
                foreach (var scriptToExecute in scriptsToExecute)
                    writeScriptToJythonShell(scriptToExecute);
                exitfromJythonShell();
                DI.log.info("Jython Execution: Data Received: {0}", dataReceived);
                return dataReceived;
            }            
            return "";
        }

        public void writeScriptToJythonShell(string scriptToWrite)
        {
            inputStream.WriteLine(scriptToWrite);
        }

        public void exitfromJythonShell()
        {
            inputStream.WriteLine(JYTHON_CMD_EXIT);
            jythonProcess.WaitForExit();
        }        

        private void _dataReceivedCallBack(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data) == false)
            {
                dataReceived += e.Data;
            }
        }



        public static bool testJython()
        {
            return testJython("print 2+3", "5");
        }

        public static bool testJython(string scriptToExecute, string expectedDataReceived)
        {
            var jythonExecution = new JythonShell();            
            var dataReceived = jythonExecution.executeScript(scriptToExecute);
            return dataReceived == expectedDataReceived;
        }

        public static Process openJythonShellOnCmdExe()
        {
            var executionArguments = string.Format(JythonConfig.jythonJavaExecution_PythonShell,JythonConfig._jythonRuntimeDir);            
            return Processes.startProcess("java.exe", executionArguments);            
        }
        
    }
}
