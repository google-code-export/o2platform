// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Kernel.CodeUtils;
using O2.Core.CIR.CirUtils;
using O2.Kernel.Interfaces.Controllers;
using O2.Scanner.OunceLabsCLI.Scan;
using O2.Scanner.OunceLabsCLI.ScanTargets;
using O2.DotNetWrappers.DotNet;
using O2.Views.ASCX.O2Findings;

namespace O2.Scanner.OunceLabsCLI.Ascx
{
    public partial class ascx_Scan
    {
        private bool runOnLoad = true;
        private const string scanButton_ScanText = "start scan";
        private const string scanButton_CancelText = "cancel scan";
        public CliScanning cliScanning;
        public bool onFolderDropSearchRecursively = false;

        private void onLoad()
        {
            if (false == DesignMode && runOnLoad)
            {
                targetDirectory._ShowFileSize = true;
                targetDirectory.setDirectory(targetDirectory.getCurrentDirectory() + "\\" + "_Scan_control");
                targetDirectory.setFileFilter("*.ozasmt");
                adoScanDropArea.setText("Drop Files to Process Here  -  \n supports: *.dll, *.exe,*.sln and most Ounce generated files \n (paf, ppf, gaf, ewf,epf, etc...)");
                runOnLoad = false;
                btScan.Text = scanButton_ScanText;
                loadDefaultScanTarget();
            }
        }

        private void loadDefaultScanTarget()
        {
            IScanTarget scanTarget = CreateScanTarget.createScanTargetsFromFile(DI.config.ExecutingAssembly, targetDirectory.getCurrentDirectory(), false /*autoAppendTargetName */);
            if (scanTarget != null)
            {
                lbScanTargets.Items.Add(scanTarget);
                lbScanTargets.SelectedIndex = 0;
            }

        }

        /*private void handleDrop(Eve oObject)
        {
            handleDrop(Dnd.tryToGetFileOrDirectoryFromDroppedObject(oObject.ToString()));
        }*/



        private void handleDrop(string fileOrFolder, bool searchRecursively, bool scanOnDrop)
        {
            List<IScanTarget> scanTargets = CreateScanTarget.createScanTargetsFromFileOrFolder(fileOrFolder,
                                                                                               targetDirectory.getCurrentDirectory(),
                                                                                               false /*autoAppendTargetName */,
                                                                                                searchRecursively /*searchForScanFilesRecursively*/);

            this.invokeOnThread(
                () =>
                    {
                        foreach (var scanTarget in scanTargets)
                        {

                            lbScanTargets.Items.Add(scanTarget);
                            //lbStatus.Text = Path.GetFileName(scanTarget.Target);
                            //scanApplication(scanTarget);
                        }
                        if (scanTargets.Count == 1)
                        {
                            lbScanTargets.SelectedItem = scanTargets[0];
                            if (scanOnDrop)
                                scanSelectedTarget();
                        }
                    });
        }


        public void scanApplication(IScanTarget scanTarget)
        {
            string sSaveAssessmentTo = Path.Combine(scanTarget.WorkDirectory,
                                                    Path.GetFileNameWithoutExtension(
                                                        scanTarget.ApplicationFile) +
                                                    "_Scan_CurrentRules.ozasmt");

            DI.log.debug("Saved Assessment File will be saved to : {0}", sSaveAssessmentTo);
            // update Gui
            this.invokeOnThread(() => lbStatus.Text = "Scanning: " + Path.GetFileName(scanTarget.Target));
            // set global cliScanning object (so that we can cancel this scan)
            cliScanning = new CliScanning();
            // start scanning
            cliScanning.scanApplication(scanTarget.ApplicationFile, sSaveAssessmentTo,
                                       onScanLogEvent
                                                                                                ,
                                        scanResult => onScanCompleted(sSaveAssessmentTo));
        }

        private void onScanLogEvent(string logEntry)
        {
            this.invokeOnThread
                (() =>
                     {
                         lbScanLog.Text = logEntry;
                         DI.log.info(logEntry);
                     });
        }

        private void onScanCompleted(string savedAssessmentPath)
        {
            this.invokeOnThread(
                () =>
                    {
                        lbStatus.Text = "Scan Completed ";
                        btScan.Text = scanButton_ScanText;
                        ascx_FindingsViewer.openInFloatWindow(savedAssessmentPath);
                    }); 
        }

        private void cancelCurrentScan()
        {
            if (cliScanning != null)
                cliScanning.cancelScan();
        }

        private void scanSelectedTarget()
        {
            if (lbScanTargets.SelectedItem != null && lbScanTargets.SelectedItem is IScanTarget)
                scanApplication((IScanTarget)lbScanTargets.SelectedItem);            
        }

        private void createCirForSelectedTarget(bool storeControlFlowBlockRawDataInsideCirDataFile)
        {
            if (lbScanTargets.SelectedItem != null && lbScanTargets.SelectedItem is IScanTarget)
            {
                setControlsEnableState(false);
                var currentScanTarget = (IScanTarget) lbScanTargets.SelectedItem;
                CirDumps.createCirDump(currentScanTarget, onCirDataCompletion, onScanLogEvent, false /*bDeleteAllRulesFromDbBeforeScan*/,
                                       storeControlFlowBlockRawDataInsideCirDataFile);            
            }            
        }

        private void setControlsEnableState(bool controlEnableState)
        {
            this.invokeOnThread(
                () =>
                    {
                        btCreateCir.Enabled = controlEnableState;
                        btScan.Enabled = controlEnableState;
                        lbScanTargets.Enabled = controlEnableState;
                        adoScanDropArea.Enabled = controlEnableState;
                        if (controlEnableState)
                            lbStatus.Text = "Scan Completed ";
                        else
                            lbStatus.Text = "Scanning selected target";
                    });
        }

        private void onCirDataCompletion(object  pProcess)
        {
            setControlsEnableState(true);
            var currentScanTarget = (IScanTarget) lbScanTargets.SelectedItem;
            var cirDataFile = currentScanTarget.ApplicationFile + ".CirData";
            if (File.Exists(cirDataFile))
            {
                var cirData = CirLoad.loadFile(cirDataFile);    
                //var cirData = CirLoad.loadSerializedO2CirDataObject(cirDataFile);
                O2Messages.setCirData(cirData);
            }

        }
    }
}
