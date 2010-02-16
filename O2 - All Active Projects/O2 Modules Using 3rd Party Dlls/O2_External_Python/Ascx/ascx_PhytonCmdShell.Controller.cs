// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.Python.CPython;
using O2.External.Python.Jython;
using O2.External.Python.IronPython;

namespace O2.External.Python.Ascx
{
    public partial class ascx_PythonCmdShell
    {
        private bool runOnLoad = true;
        public Process pythonExeProcess;
        public StreamWriter processStdInput;

        private void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                runOnLoad = false;                

                // default values 
                tbPathToIronPython.Text = IronPythonConfig.IronPythonInstallationDir;//@"C:\Program Files\IronPython 2.0.1\ipy.exe";
                tbPathToJPython.Text = JythonConfig.JythonInstallationDir;//@"E:\O2\tests\JPython\jython2.5.0\jython.jar";
                
                //startIronPythonShell();
            }
        }

        public void startIronPythonShell()
        {
            startPythonShell("starting IronPython Shell", IronPythonExec.startIronPythonShell);
            /*    
            setStartButtonsEnableState(false);
            logInfoMessage("starting IronPython Shell");            
            pythonExeProcess = IronPythonExec.startIronPythonShell(dataReceivedCallBack);
            //Processes.startProcessAndRedirectIO(pathToPythonExe, "", ref processStdInput, dataReceivedCallBack, dataReceivedCallBack);
            if (pythonExeProcess != null)
                processStdInput = pythonExeProcess.StandardInput;
           */ 
        }
        // logInfoMessage("starting IronPython Shell");                        
        // startPythonShell(tbPathToIronPython.Text);
        // pythonExeProcess = Processes.startProcessAndRedirectIO(pathToPythonExe, "", ref processStdInput, dataReceivedCallBack, dataReceivedCallBack);

        public void startJythonShell()
        {
            startPythonShell("starting Jython Shell", JythonExec.startJythonShell);
            /*    
                setStartButtonsEnableState(false);
                logInfoMessage("starting Jython Shell");            
                pythonExeProcess = JythonExec.startJythonShell(dataReceivedCallBack);                
                if (pythonExeProcess != null)
                    processStdInput = pythonExeProcess.StandardInput;
             * */
        }

        // this is not working (the shell output is not being relayed (the same way it is for IronPython and Jython))
        private void startCPythonShell()
        {
            startPythonShell("starting CPython Shell", CPythonExec.startCPythonShell);
            /*    setStartButtonsEnableState(false);
                logInfoMessage("starting CPython Shell");            
                pythonExeProcess = CPythonExec.startCPythonShell(dataReceivedCallBack);
            
                if (pythonExeProcess != null)
                    processStdInput = pythonExeProcess.StandardInput;*/
        }     
   
        
        public void startPythonShell(string logMessage, Func<DataReceivedEventHandler,Process> pythonEngineToUse)
        {
            setStartButtonsEnableState(false);
            logInfoMessage(logMessage);
            pythonExeProcess = pythonEngineToUse(dataReceivedCallBack);

            if (pythonExeProcess != null)
            {
                processStdInput = pythonExeProcess.StandardInput;
                testPythonShell();
            }
        }

        private void testPythonShell()
        {
            executeCommand("2+2");
        }

        /*public void startPythonShell(string pathToPythonExe)
        {
            setStartButtonsEnableState(false);
            
//            tbCommandToExecute.Enabled = false;

            if (File.Exists(pathToPythonExe))
            {
                switch (Path.GetExtension(pathToPythonExe))
                {
                    case ".exe":                       
//                        pythonExeProcess = Processes.startProcessAndRedirectIO(pathToPythonExe, "", ref processStdInput, dataReceivedCallBack, dataReceivedCallBack);
                        break;
                    case ".jar":
                        string arguments = "-jar " + pathToPythonExe + @" -Dpython.path=E:\O2\tests\JPython\jython2.5.0\javassist.jar";
                        pythonExeProcess = Processes.startProcessAndRedirectIO("java.exe", arguments, ref processStdInput, dataReceivedCallBack, dataReceivedCallBack);
                        //pythonExeProcess = Processes.startProcess("java.exe", "-jar " + pathToPythonExe);
                        break;
                }
                if (processStdInput!=null)
                    processStdInput = pythonExeProcess.StandardInput;
                //processStdInput.WriteLine("help()");
            }
        }   */     

        private void dataReceivedCallBack(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != "")
            {
                this.invokeOnThread(
                    () =>
                        {
                            //var currentCommand = tbCommandToExecute.Text;
                            var dataReceived = string.Format("\t:{0}:", e.Data);

                            // handle the multiple >>>  that exist when we issue commands that dont return its data imediately
                            while (dataReceived.IndexOf(">>> >>> ") > -1)
                                dataReceived = dataReceived.Replace(">>> >>> ", ">>> ");                            

                            logDataReceived(dataReceived);

                            this.invokeOnThread(
                                () =>
                                    {
                                        //tbCommandToExecute.Enabled = true;
                                        tbCommandToExecute.Focus();
                                    });

                        });
            }

        }

        private static void appendTextToRichTextBox(RichTextBox targetRichTextBox, string textToWrite, Color color)
        {
            // add text at the end
            targetRichTextBox.AppendText(textToWrite + Environment.NewLine);
            targetRichTextBox.SelectionStart = targetRichTextBox.TextLength - textToWrite.Length - 1;
            targetRichTextBox.SelectionLength = textToWrite.Length;
            targetRichTextBox.SelectionColor = color;
            targetRichTextBox.SelectionStart = targetRichTextBox.TextLength;        // remove the selection            
            targetRichTextBox.ScrollToCaret();                                      // scroll RTB to then end of the content
        }


        private void executeCommand(string cmdToExecute)
        {
            executeCommand(cmdToExecute,true);
        }

        private void executeCommand(string cmdToExecute, bool addToHistory)
        {
            if (processStdInput != null)
            {
                if (!string.IsNullOrEmpty(cmdToExecute))
                {
                    // add config check box to control the echo of the command
                    logCommandExecuted(cmdToExecute);
                    processStdInput.WriteLine(cmdToExecute);
                    tbCommandToExecute.Focus();
                    if (addToHistory)
                        addToCommandHistory(cmdToExecute);
                }
            }
            else
                logError("Could not execute command because there is no Python shell available: " + cmdToExecute);

        }

        private void addToCommandHistory(string cmdToExecute)
        {
            this.invokeOnThread(
                () =>
                    {
                        lbCommandHistory.Items.Add(cmdToExecute);
                        lbCommandHistory.SelectedIndex = lbCommandHistory.Items.Count - 1;
                    });
        }

        public void logDataReceived(string messageToLog)
        {
            this.invokeOnThread(() => appendTextToRichTextBox(rtbShellWindow, messageToLog, Color.Black));
        }

        public void logInfoMessage(string messageToLog)
        {
            this.invokeOnThread(() => appendTextToRichTextBox(rtbShellWindow, messageToLog, Color.DarkGray));
        }

        public void logCommandExecuted(string messageToLog)
        {
            this.invokeOnThread(() => appendTextToRichTextBox(rtbShellWindow, messageToLog, Color.Green));
        }

        public void logError(string messageToLog)
        {
            this.invokeOnThread(() => appendTextToRichTextBox(rtbShellWindow, messageToLog, Color.Red));
        }

        private void killCurrentPythonProcess()
        {
            try
            {
                
                if (pythonExeProcess != null)
                {
                    logInfoMessage("Stopping current Python process");
                    pythonExeProcess.Kill();
                    pythonExeProcess = null;
                }
                else
                    logInfoMessage("There is no Python Process to stop");                
            }
            catch (Exception ex)
            {

                DI.log.ex(ex);
            }
            setStartButtonsEnableState(true);
        }
    

        private void setStartButtonsEnableState(bool state)
        {
            this.invokeOnThread(
                () =>
                    {
                        btStartIronPython.Enabled = state;
                        btStartJython.Enabled = state;
                        btKillProcess.Enabled = !state;
                    });
        }

    }
}
