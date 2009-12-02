// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.External.WinFormsUI.O2Environment;
using O2.Kernel.CodeUtils;

namespace O2.Scanner.OunceLabsCLI.Utils
{
    public class scanWithRules
    {
        public static bool ScanWithExistingRules(String sApplicationToScan, String sScanResultsFolder)
        {
            return ScanWithExistingRules(sApplicationToScan, sScanResultsFolder, null);
        }

        public static bool ScanWithExistingRules(String sApplicationToScan, String sScanResultsFolder,
                                                 Callbacks.dMethod_Object dProcessCompletionCallback)
        {
            var srScanResults = new O2scanresults();
            srScanResults.bScanWithExistingRules = true;
            srScanResults.dProcessCompletionCallback = dProcessCompletionCallback;
            bool bScanResult = srScanResults.scanApplication(sApplicationToScan);
            SaveAssessmentFiles(srScanResults, sScanResultsFolder);
            DebugMsg.saveLogIntoFile(Path.Combine(sScanResultsFolder,
                                                  Path.GetFileNameWithoutExtension(sApplicationToScan) +
                                                  " - Scan_WithExistingRules.txt"));
            return bScanResult;
        }

        public static bool ScanWithNoRules(String sApplicationToScan, String sScanResultsFolder)
        {
            return ScanWithNoRules(sApplicationToScan, sScanResultsFolder,null);
        }
        public static bool ScanWithNoRules(String sApplicationToScan, String sScanResultsFolder,
                                           Callbacks.dMethod_Object dProcessCompletionCallback)
        {
            var srScanResults = new O2scanresults();
            srScanResults.bScanWithNoRules = true;
            bool bScanResult = srScanResults.scanApplication(sApplicationToScan);            
            DebugMsg.saveLogIntoFile(Path.Combine(sScanResultsFolder,
                                                  Path.GetFileNameWithoutExtension(sApplicationToScan) +
                                                  " - Scan_WithNoRules.txt"));
            return bScanResult;
        }


        public static void ScanWithSelectedAutomaticRuleSets(String sApplicationToScan, String sScanResultsFolder,
                                                             bool bCallBacksOnControlFlowGraphs_And_ExternalSinks,
                                                             bool bCallBacksOnEdges_And_ExternalSinks,
                                                             bool bSourcesAndSinks, String sExtraLogNameText,
                                                             Callbacks.dMethod_Object dProcessCompletionCallback,
                                                             Callbacks.dMethod_String logCallback)
        {
            O2Thread.mtaThread(() =>
                                   {
                                       DebugMsg.bLogCache = true; // enable LogCache (so that we can save it at the end
                                       var srScanResults = new O2scanresults();
                                       //if (File.Exists(CalculateCirDataFileNameInResultsFolder(sApplicationToScan, sScanResultsFolder)))
                                       //   srScanResults.sCirDataFile = CalculateCirDataFileNameInResultsFolder(sApplicationToScan, sScanResultsFolder);
                                       // load or create CirData file
                                       if (File.Exists(sApplicationToScan + ".CirData"))
                                           srScanResults.sCirDataFile = sApplicationToScan + ".CirData";
                                       else
                                           srScanResults.bCreateCirDataFile = true;
                                       // set scanning options (i.e. what to scan)
                                       srScanResults.bCallBacksOnControlFlowGraphs_And_ExternalSinks =
                                           bCallBacksOnControlFlowGraphs_And_ExternalSinks;
                                       srScanResults.bCallBacksOnEdges_And_ExternalSinks =
                                           bCallBacksOnEdges_And_ExternalSinks;
                                       srScanResults.bSourcesAndSinks = bSourcesAndSinks;

                                       srScanResults.dProcessCompletionCallback = dProcessCompletionCallback;
                                       srScanResults.logCallback = logCallback;

                                       // trigger scan
                                       bool bScanResult = srScanResults.scanApplication(sApplicationToScan);


                                       //  SaveCirDataFile(srScanResults, sScanResultsFolder);
                                       //SaveAssessmentFiles(srScanResults, sScanResultsFolder);
                                       DebugMsg.saveLogIntoFile(Path.Combine(sScanResultsFolder,
                                                                             Path.GetFileNameWithoutExtension(
                                                                                 sApplicationToScan) + " - " +
                                                                             sExtraLogNameText + ".txt"));
                                       DebugMsg.bLogCache = false;
                                   });

        }


        /*  public static void CreateCirDataFile(O2scanresults srScanResults, String sScanResultsFolder, Callbacks.dMethod_Object dProcessCompletionCallback)
        {
            String sTargetCirFile = CalculateCirDataFileNameInResultsFolder(srScanResults.sApplicationToScan, sScanResultsFolder);
            if (File.Exists(srScanResults.sCirDataFile) && srScanResults.sCirDataFile != sTargetCirFile)
            {
                //tring sZipFileName = Path.Combine(sScanResultsFolder, Path.GetFileName(sTargetCirFile) + ".zip");                
                File.Copy(srScanResults.sCirDataFile, sTargetCirFile,true);
                //zipUtils.zipFile(srScanResults.sCirDataFile, sZipFileName);                
                //File.Move(srScanResults.sCirDataFile, sTargetCirFile);
            }
        }*/

        public static void SaveAssessmentFiles(O2scanresults srScanResults, String sScanResultsFolder)
        {
            if (File.Exists(srScanResults.sAssessmentFile_ScanWithExistingRules))
                //Files.MoveFile(srScanResults.sAssessmentFile_ScanWithExistingRules, Path.Combine(sScanResultsFolder, Path.GetFileNameWithoutExtension(srScanResults.sApplicationToScan) + "_ScanWithExistingRules.ozasmt"));
                Files.MoveFile(srScanResults.sAssessmentFile_ScanWithExistingRules, Path.Combine(sScanResultsFolder, "ScanWithExistingRules.ozasmt"));
            if (File.Exists(srScanResults.sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks))
                //Files.MoveFile(srScanResults.sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks, Path.Combine(sScanResultsFolder, Path.GetFileNameWithoutExtension(srScanResults.sApplicationToScan) + "_CallBacksOnControlFlowGraphs_And_ExternalSinks.ozasmt"));
                Files.MoveFile(srScanResults.sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks, Path.Combine(sScanResultsFolder, "CallBacksOnControlFlowGraphs_And_ExternalSinks.ozasmt"));
            if (File.Exists(srScanResults.sAssessmentFile_CallBacksOnEdges_And_ExternalSinks))
                //Files.MoveFile(srScanResults.sAssessmentFile_CallBacksOnEdges_And_ExternalSinks, Path.Combine(sScanResultsFolder, Path.GetFileNameWithoutExtension(srScanResults.sApplicationToScan) + "_CallBacksOnEdges_And_ExternalSinks.ozasmt"));
                Files.MoveFile(srScanResults.sAssessmentFile_CallBacksOnEdges_And_ExternalSinks, Path.Combine(sScanResultsFolder, "CallBacksOnEdges_And_ExternalSinks.ozasmt"));
            if (File.Exists(srScanResults.sAssessmentFile_SourcesAndSinks))
                //Files.MoveFile(srScanResults.sAssessmentFile_SourcesAndSinks, Path.Combine(sScanResultsFolder, Path.GetFileNameWithoutExtension(srScanResults.sApplicationToScan) + "_SourcesAndSinks.ozasmt"));         
                Files.MoveFile(srScanResults.sAssessmentFile_SourcesAndSinks, Path.Combine(sScanResultsFolder, "SourcesAndSinks.ozasmt"));         
        }

        public static String CalculateCirDataFileNameInResultsFolder(string sApplicationToScan,
                                                                     string sScanResultsFolder)
        {
            return Path.Combine(sScanResultsFolder, Path.GetFileNameWithoutExtension(sApplicationToScan) + ".CirData");
        }

        public static String ProcessZipFileAndGetApplicationFileToScan(String sFileToTest, String sTargetFolder)
        {
            DI.log.debug("Unzipping File Contents to:{0}",
                         sTargetFolder.Replace(AppDomain.CurrentDomain.BaseDirectory, ""));
            new zipUtils().unzipFile(sFileToTest, sTargetFolder);
            var lsApplicationFiles = new List<string>();
            Files.getListOfAllFilesFromDirectory(lsApplicationFiles, sTargetFolder, true, "*.paf", false);
            DI.log.debug("Searching for *.paf files");
            if (lsApplicationFiles.Count == 0)
            {
                DI.log.debug("No *.paf was found, searching for Searching for *.sln files");
                Files.getListOfAllFilesFromDirectory(lsApplicationFiles, sTargetFolder, true, "*.sln", false);
            }
            if (lsApplicationFiles.Count == 0)
            {
                DI.log.error("No *.paf or *.sln files where found on the zip file uploaded, zip file will be deleted");
                File.Delete(sFileToTest);
            }
            else if (lsApplicationFiles.Count > 1)
                DI.log.error(
                    "More than 1 *.paf or *.sln file was found in the zip file uploaded (one 1 *.paf or *.sln per zip file is supported");
            else
            {
                String sApplicationToScan = lsApplicationFiles[0];
                DI.log.debug("Found Application to Scan:{0}", Path.GetFileName(sApplicationToScan));
                return sApplicationToScan;
            }
            return "";
        }
    }
}
