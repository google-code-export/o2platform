// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.Controllers;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.RulesUtils;
using O2.Scanner.MsCatNet.Scan;
using O2.Scanner.OunceLabsCLI.Scan;
using O2.Scanner.OunceLabsCLI.ScanTargets;
using O2.Scanner.OunceLabsCLI.Utils;
using O2.Views.ASCX.classes;

namespace O2.Scanners.Ascx
{
    public partial class ascx_WillItScan
    {
        private CliScanning cliScanning;

        private bool runOnLoad = true;

        private void onLoad()
        {
            if (runOnLoad && DesignMode == false)
            {
                rbScannerMSCatNet.Enabled = MsCatNetConfig.isCatScannerAvailable();
                adManualTestTempFiles.simpleMode_withAddressBar();
                adManualTestTempFiles._ShowFileSize = true;
                ascx_DropObject1.setText("Drop Files to Process Here  -  \n supports: *.dll, *.exe,*.sln and most Ounce generated files \n (paf, ppf, gaf, ewf,epf, etc...)");
                ado_AddFilesOrDirectoryToScanBundle.setText("Drop Files or Folders to add to Scan Bundle");
                tbWorkDirectory.Text = DI.config.O2TempDir;
                updateLabelWithNumberOfRulesInDatabase();
                runOnLoad = false;
            }
        }

        private void cancelCurrentOunceCLIScan()
        {
            if (cliScanning==null)
                DI.log.error("There is no Ounce CLI Scan");
            else
            {
                if (cliScanning.cancelScan())
                    DI.log.info("Ounce CLI Scan canceled");
                else
                    DI.log.error("Count NOT cancel Ounce CLI Scan");
            }
        }

        public void processTargetFilesDroppedItem(string sItemToLoad)
        {
            if (Directory.Exists(sItemToLoad))
            {
                foreach (String sFile in Files.getFilesFromDir_returnFullPath(sItemToLoad))
                    File.Copy(sFile, Path.Combine(adManualTestTempFiles.getCurrentDirectory(), Path.GetFileName(sFile)));
            }
            else
            {
                if (File.Exists(sItemToLoad))
                    File.Copy(sItemToLoad,
                              Path.Combine(adManualTestTempFiles.getCurrentDirectory(), Path.GetFileName(sItemToLoad)));
            }
            adManualTestTempFiles.refreshDirectoryView();
        }

        public void createRulePacks()
        {
            String sCirDataFile = scCurrentScanTarget.ApplicationFile + ".CirData";
            //_SourcesAndSinks
            O2RulePack o2RulePack_SourcesAndSinks = O2RulePackUtils.createRules_SourcesAndSinks(sCirDataFile);
            O2RulePackUtils.saveRulePack(sCirDataFile, "_SourcesAndSinks",o2RulePack_SourcesAndSinks);

            //_CallBacksOnEdges_And_ExternalSinks
            O2RulePack o2RulePack_CallBacksOnEdges_And_ExternalSinks = O2RulePackUtils.createRules_CallBacksOnEdges_And_ExternalSinks(sCirDataFile);
            O2RulePackUtils.saveRulePack(sCirDataFile, "_CallBacksOnEdges_And_ExternalSinks",o2RulePack_CallBacksOnEdges_And_ExternalSinks);

            //_CallBacksOnControlFlowGraphs_And_ExternalSinks
            O2RulePack o2RulePack_CallBacksOnControlFlowGraphs_And_ExternalSinks = O2RulePackUtils.createRules_CallBacksOnControlFlowGraphs_And_ExternalSinks(sCirDataFile);
            O2RulePackUtils.saveRulePack(sCirDataFile, "_CallBacksOnControlFlowGraphs_And_ExternalSinks",o2RulePack_CallBacksOnControlFlowGraphs_And_ExternalSinks);


            adManualTestTempFiles.refreshDirectoryView();
            DI.log.debug("Rule Packs creation complete");
        }

        public void scanWithExistingRules()
        {
            if (scCurrentScanTarget != null)
            {
                string sSaveAssessmentTo = Path.Combine(scCurrentScanTarget.WorkDirectory,
                                                        Path.GetFileNameWithoutExtension(
                                                            scCurrentScanTarget.ApplicationFile) +
                                                        "_Scan_CurrentRules.ozasmt");

                setStatusOfActionButtons(false);
                rtbLogFileForCurrentScan.Clear(); // clear log file
                DI.log.debug("Saved Assessment File will be saved to : {0}", sSaveAssessmentTo);
                btCreateCirDumpWithExistingRules.Enabled = rbScannerOunceCore.Checked;
                if (rbScannerOunceCore.Checked)
                {
                    logCallback(
                        string.Format(
                            "\n\n Starting Ounce CLI Scan of \n\n \t {0} \n\n who will save the resulting assessment file to: \n\n \t {1} \n\n ",
                            scCurrentScanTarget.ApplicationFile, sSaveAssessmentTo));
                    cliScanning = new CliScanning();
                    cliScanning.scanApplication(scCurrentScanTarget.ApplicationFile, sSaveAssessmentTo,
                                                      logCallback, scanCompleted_Callback);
                }
                else if (rbScannerMSCatNet.Checked)
                {
                    cbEnableNoOutOfTheBoxRules.Checked = false;
                    if (scCurrentScanTarget.GetType() == typeof(ScanTarget_DotNet))
                        new MsCatNetScanner().scan(scCurrentScanTarget, logCallback, scanCompleted_Callback,
                                                   false);
                    else
                        DI.log.error("CAT.NET scanner only supports DotNet projects");
                }
                else if (rbScannerAppScanDE.Checked)
                {
                    
                }

            }
        }

        public void downloadDemoFile(String sFileToDownload)
        {
            if (string.IsNullOrEmpty(sFileToDownload))
                DI.log.error("in downloadDemoFile: No file provided");
            else
            {
                string sTargetFile = Path.Combine(DI.config.O2TempDir, Path.GetFileName(sFileToDownload));
                WebRequests.downloadFileUsingAscxDownload(sFileToDownload, sTargetFile, addFileOrFolderAsScanTargets);
            }
        }

        private void scanCompleted_Callback(Object pProcess)
        {
            try
            {
                if (InvokeRequired)
                    O2Forms.executeMethodThreadSafe(this, this, "scanCompleted_Callback", new[] { pProcess });

                DI.log.debug("scanCallBack_ScanCompleted");
                //     redirectLogToOriginalLogViewer();
                // adManualTestTempFiles.refreshDirectoryView();
                setStatusOfActionButtons(true);
                scanProcessBar.invokeOnThread(delegate
                                                  {
                                                      scanProcessBar.Value = scanProcessBar.Maximum;
                                                      return null;
                                                  });
                
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "scanCompleted_Callback");
            }
        }

        public void addFileOrFolderAsScanTargets(string fileOrFolder)
        {
            if (lbTargetFiles.InvokeRequired)
                lbTargetFiles.Invoke(new EventHandler(delegate { addFileOrFolderAsScanTargets(fileOrFolder); }));
            else
            {
                //tbWorkDirectory.Text = Path.GetDirectoryName(itemToProcess);
                //tbWorkDirectory.Text = DI.config.O2TempDir;
                List<IScanTarget> scanTargets = CreateScanTarget.createScanTargetsFromFileOrFolder(fileOrFolder,
                                                                                                   tbWorkDirectory.Text,
                                                                                                   cbAutoAppendTargetName
                                                                                                       .Checked);
                foreach (IScanTarget scanTarget in scanTargets)                                    
                    lbTargetFiles.Items.Add(scanTarget);                

                setupGuiAfterTargetLoad();
                scanProcessBar.Enabled = false;                
            }
        }

        public void setupGuiAfterTargetLoad()
        {
            if (lbTargetFiles.Items.Count == 1)
                lbTargetFiles.SelectedIndex = 0;

            btScanWithExistingRules.Enabled = lbTargetFiles.Items.Count > 0;
        }

        public void loadInManualTestGui(IScanTarget scScanTarget)
        {
            scCurrentScanTarget = scScanTarget;
            lbManualTestTargetFile.Text = scScanTarget.ApplicationFile;
            adManualTestTempFiles.openDirectory(scScanTarget.WorkDirectory);

            setStatusOfActionButtons(true);
        }

        public void unloadCurrentScanTarget()
        {
            scCurrentScanTarget = null;
            lbManualTestTargetFile.Text = "";

            if (lbTargetFiles.Items.Count > 0)
                setStatusOfActionButtons(false);
            else
                setStatusOfActionButtons(true);
        }

        private void createCirDumpsForSelectedFile()
        {
            if (scCurrentScanTarget != null)
            {
                setStatusOfActionButtons(false);                

                CirDumps.createCirDump(scCurrentScanTarget, scanCompleted_Callback, logCallback, true,
                                       cbStoreControlFlowBlockRawDataInsideCirDataFile.Checked);            
            }
        }

        public void setStatusOfActionButtons(bool bState)
        {
            if (btScanWithExistingRules.InvokeRequired)
                btScanWithExistingRules.Invoke(new EventHandler(delegate { setStatusOfActionButtons(bState); }));
            else
            {
                btScanWithExistingRules.Enabled = bState;
                btCreateCirDumpWithExistingRules.Enabled = bState;
                btCreateAssessmentFiles.Enabled = bState;
                btCreateCirDumpForSelectedFile.Enabled = bState;
                btCreateO2RulePacks.Enabled = bState;
                updateLabelWithNumberOfRulesInDatabase();
                if (false == bState)
                    scanProcessBar.Value = 0;
            }
        }

        public void logCallback(string text)
        {
            if (rtbLogFileForCurrentScan.okThread(delegate { logCallback(text); }))
            {
                if (cbShowLogExecutionLog.Checked)
                    rtbLogFileForCurrentScan.Text = text + Environment.NewLine + rtbLogFileForCurrentScan.Text;
                // also send it to the cache unless there is an error
                DI.log.logToChache(text);
                if (text.ToLower().IndexOf("error")> -1) //todo: add a better way to find out errors in the logs received
                    DI.log.error("Error in scan callback: {0}", text);
                incScanProgressBar();
            }
        }

        private void createAssessementFiles()
        {                        
            setStatusOfActionButtons(false);

            // get scan target
            var scScanTarget = (IScanTarget)lbTargetFiles.SelectedItem;

            if (scScanTarget != null)
            {
                O2Thread.mtaThread(() =>
                                       {
                                           // if there are no rule packs in the temp folder means that this is the first time we are here so, let's create them
                                           if (Files.getFiles(scScanTarget.WorkDirectory, "*.O2RulePack").Count == 0)
                                               createRulePacks();

                                           // trigger scan with selected rules
                                           scanWithRules.ScanWithSelectedAutomaticRuleSets(scScanTarget.ApplicationFile,
                                                                                           scScanTarget.WorkDirectory,
                                                                                           cbCallBacksOnControlFlowGraphs_And_ExternalSinks.Checked,
                                                                                           cbCallBacksOnEdges_And_ExternalSinks.Checked,
                                                                                           cbSourcesAndSinks.Checked,
                                                                                           "WillItScan", scanCompleted_Callback, logCallback);
                                       });                                
            }
        }

        private void createCirDataViaLabelLink()
        {
            if (scCurrentScanTarget != null)
            {
                String sPathToCirDumpFiles = tbPathToRawCirDataFiles.Text;
                // Files.checkIfDirectoryExistsAndCreateIfNot(Path.Combine(scCurrentScanTarget.getWorkDirectory(), "_CirDumps"));
                String sCirDataFile = scCurrentScanTarget.ApplicationFile + ".CirData";
                CirDumpsUtils.createConsolidatedCirDataFile(sPathToCirDumpFiles, sCirDataFile,
                                                            cbStoreControlFlowBlockRawDataInsideCirDataFile.Checked);
            }
            else
                DI.log.error("in createCirDataViaLabelLink, there is no Target Selected");
        }

        public void incScanProgressBar()
        {
            this.invokeOnThread(delegate
                                    {
                                        scanProcessBar.Enabled = true;
                                        scanProcessBar.Value++;
                                        if (scanProcessBar.Value == scanProcessBar.Maximum)
                                            scanProcessBar.Value = 0;                                        
                                        return null;
                                    });
        }
    }
}
