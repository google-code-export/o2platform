// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Scanner.OunceLabsCLI.Utils;

//using O2.analysis;

namespace O2.Scanner.OunceLabsCLI.Scan
{
    public class CliScanning
    {
        public Process scanProcess=null;

        public bool isScanRunning()
        {
            return scanProcess != null && scanProcess.HasExited;
        }

        public bool cancelScan()
        {
            try
            {
                if (scanProcess != null)
                    scanProcess.Kill();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool scanApplication(string sApplicationToScan, string sPathToSaveAssessmentFile)
        {
            return scanApplication(sApplicationToScan, sPathToSaveAssessmentFile, null, null);
        }

        public bool scanProject(string sProjectToScan, string sPathToSaveAssessmentFile)
        {
            String sTempApplicationFile = ScanSupport.createTempApplicationFileForProject(sProjectToScan);

            if (sTempApplicationFile != "")
                return false;

            bool bResult = scanApplication(sTempApplicationFile, sPathToSaveAssessmentFile, null, null);

            File.Delete(sTempApplicationFile);
            return bResult;
        }

        public bool scanApplication(string sApplicationToScan, string sPathToSaveAssessmentFile,
                                    Callbacks.dMethod_String logCallback, Callbacks.dMethod_Object onProcessEndCallback)
        {
            string cliParameters = "script \"" +
                                   getCliScript_ScanApplication(sApplicationToScan, sPathToSaveAssessmentFile) + "\"";

            scanProcess = startCliProcessWithParameters(cliParameters, logCallback);


            Processes.waitForProcessExitAndInvokeCallBankOncompletion(scanProcess,
                                                                      executedProcess =>
                                                                          {
                                                                              if (onProcessEndCallback != null)
                                                                                  onProcessEndCallback(executedProcess);
                                                                          }, true);
            return true;

            /*      Process pProcess = Processes.startProcessAsConsoleApplication(
                OunceCore.getPathToOunceInstallDirectory() + @"\bin\cli.exe", "script \"" + getCliScript_ScanApplication(sApplicationToScan,sPathToSaveAssessmentFile)+"\"");
            if (dProcessCompletionCallback != null)            
                Processes.waitForProcessExitAndInvokeCallBankOncompletion(pProcess, logCallback,  dProcessCompletionCallback, true);            
            return true;*/
        }


        internal Process startCliProcessWithParameters(string parameters, Callbacks.dMethod_String logCallback)
        {
            try
            {
                string cliExe = Path.Combine(OunceCore.getPathToOunceInstallDirectory(),
                                             OunceCore.getCliExecutableName());
                            // OunceCore.getPathToOunceInstallDirectory() + @"\bin\cli.exe";

                DI.log.info("Scanning Ounce's CLI exe with parameters: {0}", parameters);
                return Processes.startProcessAsConsoleApplication(cliExe, parameters,
                                                                  (sender, e) =>
                                                                      {
                                                                          if (!string.IsNullOrEmpty(e.Data))
                                                                              if (logCallback != null)
                                                                                  logCallback(e.Data);
                                                                              else
                                                                                  DI.log.info("[CLI] {0}", e.Data);
                                                                      });
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in OunceCliScanning.startCliProcessWithParameters");
            }
            return null;
        }

        public String getCliScript_ScanApplication(string sApplicationToScan, string sPathToSaveAssessmentFile)
        {
            String sScriptTemplate =
                string.Format("login {0} {1} {2}",
                    OunceCore.OunceCoreLogin_IPAddress, OunceCore.OunceCoreLogin_UserName, OunceCore.OunceCoreLogin_Password) + Environment.NewLine +
                "DEL \"" + Path.GetFileNameWithoutExtension(sApplicationToScan) + "\"" + Environment.NewLine +
                "OA \"" + sApplicationToScan + "\"" + Environment.NewLine +
                "List" + Environment.NewLine +
                "Scan \"" + sPathToSaveAssessmentFile + "\"" + Environment.NewLine;
            String sScriptFile = DI.config.TempFileNameInTempDirectory;
            Files.WriteFileContent(sScriptFile, sScriptTemplate);
            DI.log.debug("Created CLI Script file on :{0}", sScriptFile);
            return sScriptFile;
        }
    }
}
