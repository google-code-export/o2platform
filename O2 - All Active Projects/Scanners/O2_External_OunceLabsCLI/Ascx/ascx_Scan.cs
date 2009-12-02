// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;


namespace O2.Scanner.OunceLabsCLI.Ascx
{
    public partial class ascx_Scan : UserControl
    {

        public ascx_Scan()
        {
            InitializeComponent();
            onLoad();
        }
        

        private void lbResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            }

        private void adoScanDropArea_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            handleDrop(oObject.ToString(), onFolderDropSearchRecursively, cbAutoScanOnFileDrop.Checked);
        }


        private void btScan_Click(object sender, EventArgs e)
        {

            if (btScan.Text == scanButton_ScanText)
            {
                scanSelectedTarget();
                btScan.Text = scanButton_CancelText;
            }
            else
            {
                cancelCurrentScan();
                btScan.Text = scanButton_ScanText;
            }
        }

        private void llClearScanTargets_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lbScanTargets.Items.Clear();
        }

        

        private void cbOnFolderDropSearchRecursively_CheckedChanged(object sender, EventArgs e)
        {
            onFolderDropSearchRecursively = cbOnFolderDropSearchRecursively.Checked;
        }

        private void llDeleteAddFilesInScanResultsFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Files.deleteAllFilesFromDir(targetDirectory.getCurrentDirectory());
        }

        private void btCreateCir_Click(object sender, EventArgs e)
        {
            createCirForSelectedTarget(cbStoreControlFlowBlockRawDataInsideCirDataFile.Checked);
        }

        private void llScanResults_seeOzasmt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            targetDirectory.setFileFilter("*.ozasmt");
        }

        private void llScanResults_seeCirData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            targetDirectory.setFileFilter("*.CirData");
        }

        private void llScanResults_seeAllFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            targetDirectory.setFileFilter("*.*");
        }

        private void lbScanTargets_DragDrop(object sender, DragEventArgs e)
        {
            handleDrop(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e), onFolderDropSearchRecursively, cbAutoScanOnFileDrop.Checked);
        }

        private void lbScanTargets_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        /* public void scanApplication()
        {
            o2.core.DebugMsg.timer tTimer = new o2.core.DebugMsg.timer("Scanned Application: " + Path.GetFileNameWithoutExtension(sApplicationToScan)).start();
            //start a scanner if not started already
            if (!OunceScanner.IsInitialized)
                OunceScanner.startScanner(null);
            OunceScanner _scanner = new OunceScanner();  
            String sTempAssessmentFile = _scanner.scanApplication(this.sApplicationToScan);


            /* No need to do this now since we can't drag and drop objects between processes)
            o2.analysis.Analysis.O2AssessmentData fadAssessmentData = null;            
            o2.analysis.Analysis.loadAssessmentFile(sTempAssessmentFile, false, ref fadAssessmentData);
            o2.analysis.Analysis.populateDictionariesWithXrefsToLoadedAssessment(o2.analysis.Analysis.FindingFilter.AllFindings, true, true, fadAssessmentData);
            lbResults.Items.Add(fadAssessmentData);
            
          
            lbResults.Items.Add(sTempAssessmentFile);
            tTimer.stop();
        }

        public void scanProject(String sProjectToScan)
        {
            switch (Path.GetExtension(sProjectToScan))
            {    
                default:              
                    {
                        this.sProjectToScan = sProjectToScan;
                        adoScanProject.setText(sButtonDefaultText_ScanProject + " : " + Path.GetFileNameWithoutExtension(this.sProjectToScan));
                        setScanAreasEnableState(false);
                        scanProject();
                        setScanAreasEnableState(true);
                    }
                    break;
            }
        }

        public void scanProject()
        {
            o2.core.DebugMsg.timer tTimer = new o2.core.DebugMsg.timer("Scanned Project {0}" + Path.GetFileNameWithoutExtension(sProjectToScan)).start();

            String sTempApplication = sProjectToScan + "_TempApplication.paf";
            o2.analysis.ScanSupport.createTempApplicationFileForProject(sProjectToScan,sTempApplication);
            if (!OunceScanner.IsInitialized)
                OunceScanner.startScanner(null);
            OunceScanner _scanner = new OunceScanner();
            String sTempAssessmentFile = _scanner.scanProject(this.sProjectToScan);

            /* No need to do this now since we can't drag and drop objects between processes)
            //File.Delete(sTempApplication);
                        
            //String sTempProjectFile = config.getTempFileNameInF1TempDirectory();
            //files.WriteFileContent(sTempProjectFile, sProjectFileContents);
            
            o2.analysis.Analysis.O2AssessmentData oadAssessmentData = null;
            o2.analysis.Analysis.loadAssessmentFile(sTempAssessmentFile, false, ref oadAssessmentData);
            o2.analysis.Analysis.populateDictionariesWithXrefsToLoadedAssessment(o2.analysis.Analysis.FindingFilter.AllFindings, true, true, oadAssessmentData);
            if (oadAssessmentData != null)
                lbResults.Items.Add(oadAssessmentData);
             * / 

            //  File.Delete(sTempAssessmentFile);

            lbResults.Items.Add(sTempAssessmentFile);
            tTimer.stop();

        }
        public void scanFiles()
        { }
        */
        
        // private void createSerializedXmlFileFromAssessmentRunObject(ounceLabs.F1.core_lib.xsd.SavedAssessment_5_03.AssessmentRun assessmentRun, string p)
        //  {
        //      throw new Exception("The method or operation is not implemented.");
        //  }

    }
}
