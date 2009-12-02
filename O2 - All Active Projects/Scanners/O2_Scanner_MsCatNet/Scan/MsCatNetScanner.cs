using System;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Controllers;
using O2.Scanner.MsCatNet.Converter;

namespace O2.Scanner.MsCatNet.Scan
{
    public class MsCatNetScanner
    {
        //   public String scanResults;
        //private Callbacks.dMethod_Object invokeOnScanCompletion;
        //  public Callbacks.dMethod_String logCallback;                

        /*public static bool scanDLl(ScanTarget_DotNet scntTarget)
        {
            scanDLl(scntTarget, "");
        }*/

        //   public static bool scanDLl(ScanTarget scntTarget, String sPathToSaveResultsFile)
        //  { 
        //  }

        public bool executeCatNet(string fileToScan)
        {
            return executeCatNet(fileToScan, null, null, false);
        }

        public bool executeCatNet(string fileToScan, bool convertToOzasmt)
        {
            return executeCatNet(fileToScan, null, null, convertToOzasmt);
        }

        public bool executeCatNet(string fileToScan, string resultsFile, bool convertToOzasmt)
        {
            return executeCatNet(fileToScan, resultsFile, null, null, convertToOzasmt);
        }

        public bool executeCatNet(string fileToScan, Callbacks.dMethod_String logCallback,
                                  Callbacks.dMethod_Object onProcessEndCallback, bool convertToOzasmt)
        {
            string resultsFile = Path.Combine(DI.config.O2TempDir,
                                              Path.GetFileNameWithoutExtension(fileToScan) + "xml");
            return executeCatNet(fileToScan, resultsFile, logCallback, onProcessEndCallback, convertToOzasmt);
        }

        private bool executeCatNet(string fileToScan, string resultsFile,
                                   Callbacks.dMethod_String logCallback,
                                   Callbacks.dMethod_Object onProcessEndCallback,
                                   bool convertToOzasmt)
        {
            //string targetDirectory = (Path.GetDirectoryName(fileToScan));
            try
            {
                // if ("" != Files.checkIfDirectoryExistsAndCreateIfNot(targetDirectory))
                if (File.Exists(fileToScan))
                {
                     DI.log.info("Scanning Dll {0} and saving results here {1}", fileToScan, resultsFile);
                    string sExecArguments = String.Format("/file:\"{0}\" /report:\"{1}\"", fileToScan, resultsFile);

                    Process msCatNetProcess = startCatNetProcessWithParameters(sExecArguments, logCallback);


                    Processes.waitForProcessExitAndInvokeCallBankOncompletion(msCatNetProcess,
                                                                              (executedProcess) =>
                                                                                  {
                                                                                      if (onProcessEndCallback !=
                                                                                          null)
                                                                                          onProcessEndCallback(
                                                                                              executedProcess);
                                                                                      if (convertToOzasmt &&
                                                                                          File.Exists(resultsFile))
                                                                                          new CatNetConverter(
                                                                                              resultsFile).convert();
                                                                                  }, true);
                    return true;
                }

                 DI.log.error("File to scan doesn't exist: {0}", fileToScan);
                //executeCatNetCommand(sExecArguments, logCallback, onProcessEndCallback, convertToOzasmt);
            }
            catch (Exception ex)
            {
                 DI.log.ex(ex, "in MsCatScan.executeCatNetCommandWithArguments");
            }

            return false;
        }

//public static void executeCatNetCommand(string catNetArguments)
//{
//    executeCatNetCommand(catNetArguments, null, internalOnScanCompleteCallback);
//}

/*  public static void executeCatNetCommand(string catNetArguments, Callbacks.dMethod_String logCallback, Callbacks.dMethod_Object onProcessEndCallback, bool convertToOzasmt)
        {
          
                                                                                                 , true);   */


        public void scan(IScanTarget scanTarget)
        {
            scan(scanTarget, false);
        }


        public void scan(IScanTarget scanTarget, bool convertToOzasmt)
        {
            scan(scanTarget, null, null, convertToOzasmt);
        }

        public void scan(IScanTarget scanTarget, Callbacks.dMethod_String logCallback,
                         Callbacks.dMethod_Object onProcessEndCallback, bool convertToOzasmt)
        {
            if (false == MsCatNetConfig.isCatScannerAvailable())
            {
                DI.log.error("Could not find MSCatNet Scanner on this box, aborting scan");
            }
            string fileToScan = scanTarget.Target;
            string reportFile = Path.Combine(scanTarget.WorkDirectory, Path.GetFileName(fileToScan) + ".xml");
            executeCatNet(fileToScan, reportFile, logCallback, onProcessEndCallback, convertToOzasmt);
        }

        /*   public static void runMSCatNetScan(string fileToScan, string pathToSaveResults, bool convertToOzasmt )
        {
            executeCatNet(fileToScan,pathToSaveResults, )
        }*/

        internal Process startCatNetProcessWithParameters(string parameters, Callbacks.dMethod_String logCallback)
        {
            try
            {
                 DI.log.info("Scanning CAT.NET exe with parameters: {0}", parameters);
                return Processes.startProcessAsConsoleApplication(MsCatNetConfig.pathToCatCmdExe,
                                                                  parameters,
                                                                  (sender, e) =>
                                                                      {
                                                                          if (!string.IsNullOrEmpty(e.Data))
                                                                              if (logCallback != null)
                                                                                  logCallback(e.Data);
                                                                              else
                                                                                   DI.log.info("[MsCatNet] {0}",
                                                                                                 e.Data);
                                                                      });
            }
            catch (Exception ex)
            {
                 DI.log.ex(ex, "in MsCatScan.startCatNetProcessWithParameters");
            }
            return null;
        }
    }
}