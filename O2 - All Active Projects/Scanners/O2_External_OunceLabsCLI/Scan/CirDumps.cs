// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Text;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.Controllers;
using O2.Kernel.CodeUtils;
using O2.Scanner.OunceLabsCLI.Utils;
using O2.Views.ASCX.classes.MainGUI;

namespace O2.Scanner.OunceLabsCLI.Scan
{
    public class CirDumps
    {
        public static String sSettingsFilename = "_CreateCirDumps.ozsettings";
        //public static String sFileWithCirDumpSettings = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        public static String sFileWithCirDumpSettings = Path.Combine(DI.config.O2TempDir, sSettingsFilename);

        

        public static bool preCirDumpGeneration(String sPathToCirDumpFiles)
        {
            if (false == File.Exists(sFileWithCirDumpSettings))
                Files.WriteFileContent(sFileWithCirDumpSettings,
                                       Encoding.ASCII.GetString(CirDumps_Resources._CreateCirDumps));
            if (false == Directory.Exists(sPathToCirDumpFiles))
                DI.log.error("in preCirDumpGeneration: Path provided to store CIRDump files doesn't exist: {0}",
                             sPathToCirDumpFiles);
            else
            {
                String sOunceConfigDir = OunceCore.getPathToOunceConfig();
                if (sOunceConfigDir != "" && File.Exists(sFileWithCirDumpSettings))
                {
                    File.Copy(sFileWithCirDumpSettings, getTempSettingsFile(), true);
                    setTargetFolderInTempSettingsFile(getTempSettingsFile(), sPathToCirDumpFiles);
                    Files.deleteFilesFromDirThatMatchPattern(sPathToCirDumpFiles, "CirDump*.xml");
                    DI.log.info("Sucessfully configured Ounce to create CirDumps here : {0}", sPathToCirDumpFiles);
                    return true;
                }

                DI.log.error("Could not find set temp Ounce Settings file with CIRDump flags");
            }
            return false;
        }

        public static void postCirDumpGeneration(String sPathToCirDumpFiles)
        {
            Files.deleteFilesFromDirThatMatchPattern(sPathToCirDumpFiles, "CirDump*.xml");
            Files.deleteFile(getTempSettingsFile());
            DI.log.info("Deleted Settings files, so Ounce will no longer generate CirDump files");
        }

        public static String getTempSettingsFile()
        {
            return Path.Combine(OunceCore.getPathToOunceConfig(), sSettingsFilename);
        }

        public static void setTargetFolderInTempSettingsFile(String sTempSettingsFile, String sPathToCirDumpFiles)
        {
            String sFileContents = Files.getFileContents(sTempSettingsFile);
            sFileContents = sFileContents.Replace("{TargetPath}", sPathToCirDumpFiles);
            Files.WriteFileContent(sTempSettingsFile, sFileContents);
        }


        public static bool createCirDump(IScanTarget scScanTarget)
        {
            if (scScanTarget != null)
                return createCirDump(scScanTarget.ApplicationFile, scScanTarget.WorkDirectory, null, null, false, false);
            return false;
        }

        public static bool createCirDump(IScanTarget scScanTarget, bool bDeleteAllRulesFromDbBeforeScan)
        {
            if (scScanTarget != null)
                return createCirDump(scScanTarget.ApplicationFile, scScanTarget.WorkDirectory, null,null,
                                     bDeleteAllRulesFromDbBeforeScan, false);
            return false;
        }

        public static bool createCirDump(IScanTarget scScanTarget, Callbacks.dMethod_Object dProcessCompletionCallback, Callbacks.dMethod_String _logCallback,
                                         bool bDeleteAllRulesFromDbBeforeScan,
                                         bool bStoreControlFlowBlockRawDataInsideCirDataFile)
        {
            if (scScanTarget!=null)
                return createCirDump(scScanTarget.ApplicationFile, scScanTarget.WorkDirectory, dProcessCompletionCallback, _logCallback,
                                     bDeleteAllRulesFromDbBeforeScan, bStoreControlFlowBlockRawDataInsideCirDataFile);
            return false;
        }

        public static bool createCirDump(String sApplicationToScan, String sScanResultsFolder)
        {
            return createCirDump(sApplicationToScan, sScanResultsFolder, null, (logEntry) => DI.log.info(logEntry), false,
                          false);
        }

        public static bool createCirDump(String sApplicationToScan, String sScanResultsFolder,
                                         Callbacks.dMethod_Object dProcessCompletionCallback, Callbacks.dMethod_String _logCallback,
                                         bool bDeleteAllRulesFromDbBeforeScan,
                                         bool bStoreControlFlowBlockRawDataInsideCirDataFile)
        {
            var srScanResults = new O2scanresults
                                    {
                                        bDeleteAllRulesForCirCreation = bDeleteAllRulesFromDbBeforeScan,
                                        dProcessCompletionCallback = dProcessCompletionCallback,
                                        logCallback = _logCallback,
                                        bCreateCirDataFile = true,
                                        bStoreControlFlowBlockRawDataInsideCirDataFile =
                                            bStoreControlFlowBlockRawDataInsideCirDataFile,
                                        bDeleteCreatedAssessmentFile = true
                                    };
            // srScanResults.sPathToCirDumpFiles = sScanResultsFolder; // o2.rules.scan.CalculateCirDataFileNameInResultsFolder(sApplicationToScan, sScanResultsFolder);

            bool bScanResult = srScanResults.scanApplication(sApplicationToScan);


            //o2.rules.scan.SaveCirDataFile(srScanResults, sScanResultsFolder);
            DebugMsg.saveLogIntoFile(Path.Combine(sScanResultsFolder,
                                                  Path.GetFileNameWithoutExtension(sApplicationToScan) +
                                                  " - CreateConsolidatedCirDump.txt"));

            return bScanResult;
        }
    }
}
