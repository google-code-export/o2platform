// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.CIR;
using O2.Legacy.OunceV6.JoinTraces.classes;
using O2.Legacy.OunceV6.JoinTraces.classes.filters;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces
{
    public partial class ascx_JoinDotNetWebServices
    {
        public ICirData cdCirData = new CirData();
        private Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock = new Dictionary<string, O2TraceBlock_OunceV6>();
        public int iMaxNumberOfCallsInOneTrace = 52; // use this to control massive recursive calls
        public int iMaxRepeatedCallsInPreviousTraces = 32; // use this to control massive recursive calls
        public TreeView tvRawData;
        public TreeView tvSinksData;
        public TreeView tvSourcesData;

        private void createAllTracesAfterDotNetWebServicesMapping()
        {
            step1();
        }

        private void step1()
        {
            var o2AssessmentDataItemsToProcess = new List<O2AssessmentData_OunceV6>();
            foreach (O2AssessmentData_OunceV6 o2AssessmentData in lbTargetSavedAssessmentFiles.Items)
                o2AssessmentDataItemsToProcess.Add(o2AssessmentData);

            btCreateTraces.Enabled = false;
            JoinTracesUtils.proccessLoadedFiles(o2AssessmentDataItemsToProcess, cbMakeLostSinksIntoSinks.Checked,
                                                (_dO2TraceBlock, _tvRawData) =>
                                                    {
                                                        dO2TraceBlock = _dO2TraceBlock;
                                                        tvRawData = _tvRawData;
                                                        this.invokeOnThread(
                                                            () =>
                                                                {
                                                                    step2();
                                                                    step3();
                                                                    //btProcessLoadedFiles.Enabled = true;                                            
                                                                });
                                                        return true;
                                                    });
        }

        private void step2()
        {
            onRawData.dotNet.mapDotNetWebServices(tvRawData);
        }

        private void step3()
        {
            btCreateTraces.Enabled = false;
            DI.log.debug("Creating Traces");
            O2Timer tTimer = new O2Timer("Creating Traces").start();


            string textFilter = "";
            //TreeView _tvRawData = tvRawData;
            ICirData _cdCirData = cdCirData;
            Dictionary<String, O2TraceBlock_OunceV6> _dO2TraceBlock = dO2TraceBlock;
            Dictionary<string, O2TraceBlock_OunceV6> dRawData = JoinTracesUtils.calculateDictionaryWithRawData(tvRawData);
            const bool bOnlyProcessTracesWithNoCallers = false;

            string targetFolder = DI.config.O2TempDir;
            string fileNamePrefix = Path.GetFileNameWithoutExtension(lbCirFileLoaded.Text);
            const bool bCreateFileWithAllTraces = true;
            const bool bCreateFileWithUniqueTraces = false;
            const bool bDropDuplicateSmartTraces = false;
            const bool bIgnoreRootCallInvocation = false;            

            JoinTracesUtils.createAssessessmentFileWithJoinnedTraces(
                textFilter, dRawData, _cdCirData, _dO2TraceBlock,
                bOnlyProcessTracesWithNoCallers, targetFolder, fileNamePrefix, bCreateFileWithAllTraces,
                bCreateFileWithUniqueTraces, bDropDuplicateSmartTraces, bIgnoreRootCallInvocation, null,
                sAssessmentFile =>
                {

                    this.invokeOnThread(
                        () =>
                        {
                            lbCreatedAssessmentFile.Text = sAssessmentFile;
                            lbCreatedAssessmentFile.Visible = true;

                            btCreateTraces.Enabled = true;
                        });
                    tTimer.stop();
                    if (sAssessmentFile != "")
                        findingsViewerfor_JoinnedTraces.loadO2Assessment(sAssessmentFile);
                    return sAssessmentFile;
                });
        }

        private void handleDrop(string fileOrFolder)
        {
            lbTargetSavedAssessmentFiles.Enabled = false;
            btCreateTraces.Enabled = false;            
            if (Directory.Exists(fileOrFolder))
                foreach (String sFile in Files.getFilesFromDir_returnFullPath(fileOrFolder))                
                    LoadFile(sFile);                            
            else
                if (File.Exists(fileOrFolder))
                    LoadFile(fileOrFolder);            
            lbTargetSavedAssessmentFiles.Enabled = true;
            btCreateTraces.Enabled = true;
        }

        public void LoadFile(String sFileToLoad)
        {
            if (Path.GetExtension(sFileToLoad).ToLower() == ".cirdata")
            {
                cdCirData = CirLoad.loadSerializedO2CirDataObject(sFileToLoad);
                lbCirFileLoaded.Text = Path.GetFileName(sFileToLoad);
            }
            else
            {                
                if (Path.GetExtension(sFileToLoad).ToLower() == ".xml" ||
                    Path.GetExtension(sFileToLoad).ToLower() == ".ozasmt")
                {
                    var oadO2AssessmentDataOunceV6 = loadAssessmentRunFileAndAddItToList(sFileToLoad);
                    if (oadO2AssessmentDataOunceV6 != null)
                    {
                        foreach (object oItem in lbTargetSavedAssessmentFiles.Items)
                            if (oItem.ToString() == oadO2AssessmentDataOunceV6.ToString())
                            {
                                DI.log.error(
                                    "in loadAssessmentRunFileAndAddItToList, file is already in the list of F1AssessmentRun objects");                                
                                return;
                            }
                        lbTargetSavedAssessmentFiles.Items.Add(oadO2AssessmentDataOunceV6);
                    }
                }
                else
                    DI.log.debug("Skipping loading file (since it is one of type: .cirdata, .xml or .ozasmt: {0}",
                                 sFileToLoad);
            }
        }

        // ReSharper disable ConditionIsAlwaysTrueOrFalse
        public static O2AssessmentData_OunceV6 loadAssessmentRunFileAndAddItToList(String sPathToFile)
        {
            var bDropDuplicateSmartTraces = true;
            var bIgnoreRootCallInvocation = true;
            var ffFindingFilter = Analysis.FindingFilter.AllFindings;
            O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6 = null;
            O2Timer tTimer = new O2Timer("Loaded SavedAssessmentFile").start();
            Analysis.loadAssessmentFile(sPathToFile, false, ref oadO2AssessmentDataOunceV6);
            // Calculate Xrefs into fadAssessmentData                               

            Analysis.populateDictionariesWithXrefsToLoadedAssessment(ffFindingFilter, bDropDuplicateSmartTraces,

                                                                     bIgnoreRootCallInvocation, oadO2AssessmentDataOunceV6);
            tTimer.stop();
            return oadO2AssessmentDataOunceV6;
        }
        // ReSharper restore ConditionIsAlwaysTrueOrFalse
        
    }
}
