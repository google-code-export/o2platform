using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.Windows;
using O2.Kernel.CodeUtils;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.DataLayer_OunceV6;
using O2.Rules.OunceLabs.RulesUtils;
using O2.Scanner.OunceLabsCLI.Scan;

namespace O2.Scanner.OunceLabsCLI.Utils
{
    public class O2scanresults
    {
        public bool bCallBacksOnControlFlowGraphs_And_ExternalSinks;
        public bool bCallBacksOnEdges_And_ExternalSinks;
        public bool bCreateCirDataFile;
        public bool bCreateCirDumpMode;
        public bool bDeleteAllRulesForCirCreation;
        public bool bDeleteCreatedAssessmentFile;
        public bool bRestartIISAfterScan;
        private bool bScannerBusy;
        public bool bScanWithExistingRules;
        public bool bScanWithNoRules;
        public bool bSourcesAndSinks;
        public bool bStoreControlFlowBlockRawDataInsideCirDataFile;
        public Callbacks.dMethod_Object dProcessCompletionCallback;
        public Callbacks.dMethod_String logCallback;
        public String sApplicationToScan = "";

        public String sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks =
            "_CallBacksOnControlFlowGraphs_And_ExternalSinks.ozasmt";

        public String sAssessmentFile_CallBacksOnEdges_And_ExternalSinks = "_CallBacksOnEdges_And_ExternalSinks.ozasmt";
        public String sAssessmentFile_CirDataScan = "_CirDataScan.ozasmt";
        public String sAssessmentFile_ScanWithExistingRules = "_ScanWithExistingRules.ozasmt";
        public String sAssessmentFile_ScanWithNoRules = "_ScanWithNoRules.ozasmt";
        public String sAssessmentFile_SourcesAndSinks = "_SourcesAndSinks.ozasmt";
        public String sCirDataFile = "";
        //public CirData fcdCirData;
        public String sDbId = "";
        public String sPathToCirDumpFiles = "";
        private String sTargetAssessmentFile = "";
        public String sTargetScan = "";

        public MySqlRules_OunceV6 mySqlRules_OunceV6 = new MySqlRules_OunceV6();

        public O2scanresults()
        {
        }

        public O2scanresults(String sPathToCirDumpFiles)
        {
            this.sPathToCirDumpFiles = sPathToCirDumpFiles;
            bCreateCirDataFile = true;
        }

        public bool scanApplication(String applicationToScan)
        {
            if (false == File.Exists(applicationToScan))
            {
                DI.log.error("in scanApplication, could not file application file: {0}", applicationToScan);
                return false;
            }

            sApplicationToScan = applicationToScan;
            // set standard savedfilelocations
            sAssessmentFile_ScanWithExistingRules = sApplicationToScan + sAssessmentFile_ScanWithExistingRules;
            sAssessmentFile_ScanWithNoRules = sApplicationToScan + sAssessmentFile_ScanWithNoRules;
            sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks = sApplicationToScan +
                                                                             sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks;
            sAssessmentFile_CallBacksOnEdges_And_ExternalSinks = sApplicationToScan +
                                                                 sAssessmentFile_CallBacksOnEdges_And_ExternalSinks;
            sAssessmentFile_SourcesAndSinks = sApplicationToScan + sAssessmentFile_SourcesAndSinks;
            sAssessmentFile_CirDataScan = sApplicationToScan + sAssessmentFile_CirDataScan;

            bool bContinueWithScans = true;
            try
            {
                DI.log.debug("Scanning Application: {0}", applicationToScan);
                sTargetScan = applicationToScan;
                //	Utils.debugBreak();

                if (bScanWithExistingRules)
                {
                    _scanApplication(applicationToScan, sAssessmentFile_ScanWithExistingRules);
                    bContinueWithScans = false;
                    // no need to do anything else since we don't want to change the existing rule set (which is what we need the CirDump for)
                }

                if (bContinueWithScans && bScanWithNoRules)
                {
                    mySqlRules_OunceV6.DeleteAllRulesFromDatabase();
                    _scanApplication(applicationToScan, sAssessmentFile_ScanWithNoRules);
                    bContinueWithScans = false; // since this is only used to tests (to make sure we can scan it
                }
                // CreateCirDataFile  (using rules so that the CIR creation process is as quick as possible)
                if (bContinueWithScans)
                {
                    if (bCreateCirDataFile)
                    {
                        if (sPathToCirDumpFiles == "")
                            sPathToCirDumpFiles =
                                Files.checkIfDirectoryExistsAndCreateIfNot(
                                    Path.Combine(DI.config.O2TempDir, "_CirDumps"));
                        if (CirDumps.preCirDumpGeneration(sPathToCirDumpFiles))
                        {
                            if (bDeleteAllRulesForCirCreation)
                                mySqlRules_OunceV6.DeleteAllRulesFromDatabase();

                            _scanApplication(applicationToScan, sAssessmentFile_CirDataScan);
                        }
                    }

                    // CallBacksOnControlFlowGraphs_And_ExternalSinks
                    if (bCallBacksOnControlFlowGraphs_And_ExternalSinks)
                    {
                        //O2RulePack orpO2RulePack = OunceRules.createRules_CallBacksOnControlFlowGraphs_And_ExternalSinks(this.sCirDataFile);
                        String sRulePackFile = sCirDataFile + "_CallBacksOnControlFlowGraphs_And_ExternalSinks" +
                                               ".O2RulePack";
                        if (File.Exists(sRulePackFile) == false)
                            DI.log.error("in scanApplication: Could not file rule pack to load :{0}", sRulePackFile);
                        else
                        {
                            O2RulePack orpO2RulePack = O2RulePackUtils.loadRulePack(sRulePackFile);
                            mySqlRules_OunceV6.DeleteAllRulesFromDatabase();
                            mySqlRules_OunceV6.addRulesToDatabase(true, orpO2RulePack);
                            _scanApplication(applicationToScan,
                                             sAssessmentFile_CallBacksOnControlFlowGraphs_And_ExternalSinks);
                        }
                    }

                    // CallBacksOnEdges_And_ExternalSinks
                    if (bCallBacksOnEdges_And_ExternalSinks)
                    {
                        //O2RulePack orpO2RulePack = OunceRules.createRules_CallBacksOnEdges_And_ExternalSinks(this.sCirDataFile);
                        String sRulePackFile = sCirDataFile + "_CallBacksOnEdges_And_ExternalSinks" + ".O2RulePack";
                        if (File.Exists(sRulePackFile) == false)
                            DI.log.error("in scanApplication: Could not file rule pack to load :{0}", sRulePackFile);
                        else
                        {
                            O2RulePack orpO2RulePack = O2RulePackUtils.loadRulePack(sRulePackFile);
                            mySqlRules_OunceV6.DeleteAllRulesFromDatabase();
                            mySqlRules_OunceV6.addRulesToDatabase(true, orpO2RulePack);
                            _scanApplication(applicationToScan, sAssessmentFile_CallBacksOnEdges_And_ExternalSinks);
                        }
                    }

                    // bSourcesAndSinks
                    if (bSourcesAndSinks)
                    {
                        //O2RulePack orpO2RulePack = OunceRules.createRules_SourcesAndSinks(this.sCirDataFile);
                        String sRulePackFile = sCirDataFile + "_SourcesAndSinks" + ".O2RulePack";
                        if (File.Exists(sRulePackFile) == false)
                            DI.log.error("in scanApplication: Could not file rule pack to load :{0}", sRulePackFile);
                        else
                        {
                            O2RulePack orpO2RulePack = O2RulePackUtils.loadRulePack(sRulePackFile);
                            mySqlRules_OunceV6.DeleteAllRulesFromDatabase();
                            mySqlRules_OunceV6.addRulesToDatabase(true, orpO2RulePack);
                            _scanApplication(applicationToScan, sAssessmentFile_SourcesAndSinks);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in scanApplication:{0}", ex.Message);
                return false;
            }
            if (bRestartIISAfterScan)
                new Thread(Processes.resetIIS).Start();

            if (dProcessCompletionCallback != null)
                dProcessCompletionCallback.Invoke(this);

            return true;
        }

        /*public void connectToCore()
		{
			// only need to conect to Core once (to login and initialize JNBridge)
		 //   Core.restartCore();
             DI.log.info("Connecting to Core");
            Processes.killProcess("OunceScanner");  // instead of restarting core, just kill all OunceScanner.exe Processes
			if (vars.get("JnBrigeInitialized") == null)
			{                
				//o2.ounce.core.Core.initializeJnbridge(OunceScanner.BasePath);	    	
                ScannerBridge.initializeBridge(OunceScanner.BasePath);
                Logger.initLogging(OunceScanner.BasePath);
				vars.set("JnBrigeInitialized" , "yes");                
				//OunceScanner.stopScanner();
				//OunceScanner.startScanner(OunceScanner.BasePath);				
			}
			Core.loginIntoScanner();				
		}*/

        public bool _scanApplication(String sApplicationToScans, String targetAssessmentFile)
        {
            try
            {
                bScannerBusy = true;
                sTargetAssessmentFile = targetAssessmentFile;
                new CliScanning().scanApplication(sApplicationToScan, targetAssessmentFile, logCallback,
                                                  _scanApplication_InternalCallback);
                waitForScannerToBeAvailableAgain();
                return true;
            }
            catch (Exception ex)
            {
                DI.log.error("in _scanApplication: {0}", ex.Message);
            }

            return false;
        }

        public void _scanApplication_InternalCallback(object Process)
        {
            if (bDeleteCreatedAssessmentFile || bCreateCirDataFile)
                File.Delete(sTargetAssessmentFile);

            if (bCreateCirDataFile)
            {                
                sCirDataFile = sApplicationToScan + ".CirData";
                logCallback("Creating CirData file: " + sCirDataFile);
                createConsolidatedCirDataFile();
                CirDumps.postCirDumpGeneration(sPathToCirDumpFiles);
                logCallback("CirData file created: " + sCirDataFile);
            }

            bScannerBusy = false;
        }

        private void waitForScannerToBeAvailableAgain()
        {
            if (bScannerBusy)
            {
                DI.log.debug("Scanner is Busy so will wait before executing the next task");
                while (bScannerBusy)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                }
                DI.log.debug("Scanner is now available again...");
            }
        }

        private void createConsolidatedCirDataFile()
        {
            CirDumpsUtils.createConsolidatedCirDataFile(sPathToCirDumpFiles, sCirDataFile,
                                                        bStoreControlFlowBlockRawDataInsideCirDataFile);
        }
    }
}