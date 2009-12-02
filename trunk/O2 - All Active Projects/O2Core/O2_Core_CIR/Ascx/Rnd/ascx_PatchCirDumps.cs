// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.Core.CIR;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirObjects;
using O2.Core.CIR.CirUtils;
using O2.Core.CIR.CirUtils;

namespace O2.Core.CIR.Ascx.Rnd
{
    public partial class ascx_PatchCirDumps : UserControl
    {
        private CirDataAnalysis cdaO2CirDataAnalysis = new CirDataAnalysis();

        public ascx_PatchCirDumps()
        {
            InitializeComponent();
        }

        private void ascx_DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            loadO2CirDataFile(oObject.ToString(),true);
        }

        private void ascx_PatchCirDumps_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                ascx_DropObject1.setText("asd");
            }
        }

        public void loadO2CirDataFile(String sFileToLoad, bool useCachedVersionIfAvailable)
        {
            if (sFileToLoad.IndexOf(".CirData") > -1 || CirLoad.isFileACirDumpFile(sFileToLoad))
            {
                //CirDataAnalysis fdaO2CirDataAnalysis;
                if (cbClearPreviousO2CirData.Checked)
                    cdaO2CirDataAnalysis = new CirDataAnalysis();                
                CirDataAnalysisUtils.addO2CirDataFile(cdaO2CirDataAnalysis, sFileToLoad, useCachedVersionIfAvailable);
                lbCirFileLoaded.Text = "";
                foreach (String sLoadedO2CirData in cdaO2CirDataAnalysis.dCirDataFilesLoaded.Keys)
                    lbCirFileLoaded.Text += Path.GetFileName(sLoadedO2CirData);

                btFindClassesWithNoControlFlowGraphs_Click(null, null);
            }
        }

        private void btFindClassesWithNoControlFlowGraphs_Click(object sender, EventArgs e)
        {
            findAndFixLoadedCirData();
            DI.log.info("Done");
        }

        public void findAndFixLoadedCirData()
        {
            foreach (CirClass ccCirClass in cdaO2CirDataAnalysis.dCirClass.Keys)
            {
                if (ccCirClass.bClassHasMethodsWithControlFlowGraphs == false)
                {
                    var lsFunctions = new List<string>();
                    foreach (CirFunction ccCirFunction in ccCirClass.dFunctions.Values)
                    {
                        String sSignatureWithoutClass =
                            ccCirFunction.FunctionSignature.Replace(ccCirFunction.ParentClass.Signature, "");
                        lsFunctions.Add(sSignatureWithoutClass);
                    }

                    if (lsFunctions.Count > 0)
                        foreach (CirClass ccCirClassToMap in cdaO2CirDataAnalysis.dCirClass.Keys)
                            if (ccCirClassToMap.bClassHasMethodsWithControlFlowGraphs)
                                foreach (String sFunction in ccCirClassToMap.dFunctions.Keys)
                                    if (sFunction.IndexOf(lsFunctions[0]) > -1) // first only find one, then match all
                                    {
                                        var lsFunctionsInTargetClass = new List<string>();
                                        foreach (CirFunction ccCirFunction in ccCirClassToMap.dFunctions.Values)
                                        {
                                            String sSignatureWithoutClass =
                                                ccCirFunction.FunctionSignature.Replace(
                                                    ccCirFunction.ParentClass.Signature, "");
                                            lsFunctionsInTargetClass.Add(sSignatureWithoutClass);
                                        }
                                        bool bFoundAllFunctions = true;
                                        foreach (string sFunctionsToMatch in lsFunctions)
                                            if (lsFunctionsInTargetClass.Contains(sFunctionsToMatch) == false)
                                            {
                                                bFoundAllFunctions = false;
                                                break;
                                            }
                                        if (bFoundAllFunctions)
                                        {
                                            const string sStatus = "";
                                            if (false == ccCirClassToMap.dSuperClasses.ContainsValue(ccCirClass))
                                                // check if we don't already have this mapping                                                
                                                if (cbOnlyMapWithExtra_Imp.Checked)
                                                {
                                                    if (ccCirClassToMap.Name == ccCirClass.Name + "Imp")
                                                    {
                                                        DI.log.debug(
                                                            "We have a match, it looks like {0} implements {1}  - {2} ",
                                                            ccCirClassToMap.Name, ccCirClass.Name, sStatus);
                                                        if (cbFixCirDumpFiles.Checked)
                                                        {
                                                            DI.log.info("Fixing xrefs");
                                                            ccCirClassToMap.dSuperClasses.Add(ccCirClass.Signature,
                                                                                              ccCirClass);
                                                            ccCirClass.dIsSuperClassedBy.Add(
                                                                ccCirClassToMap.Signature, ccCirClassToMap);
                                                        }
                                                    }
                                                }
                                                else if (ccCirClassToMap.Name.Contains(ccCirClass.Name))
                                                    DI.log.debug(
                                                        "We have a match, it looks like {0} implements {1}  - {2} ",
                                                        ccCirClassToMap.Name, ccCirClass.Name, sStatus);
                                        }
                                        // we have a match
                                    }
                }
            }
            if (cbFixCirDumpFiles.Checked)
            {
                DI.log.info("Since cbFixCirDumpFiles is checked, lets Serialize again the loaded CirData files");
                foreach (String sLoadedO2CirData in cdaO2CirDataAnalysis.dCirDataFilesLoaded.Keys)
                {
                    CirDataUtils.saveSerializedO2CirDataObjectToFile(cdaO2CirDataAnalysis.dCirDataFilesLoaded[sLoadedO2CirData],sLoadedO2CirData);
                }
            }
        }
    }
}
