// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class Analysis
    {
        #region FindingFilter enum

        public enum FindingFilter
        {
            AllFindings,
            SmartTraces,
            SmartTraces_LostSink,
            SmartTraces_LostSink_Unique,
            NoSmartTraces,
        }

        #endregion

        #region FindingNameFormat enum

        public enum FindingNameFormat
        {
            FindingType,
            FindingType_Source,
            FindingType_Sink,
            Source,
            Sink,
            Sink_Source,
            Source_Sink
        }

        #endregion

        #region SmartTraceFilter enum

        public enum SmartTraceFilter
        {
            MethodName,
            Context,
            SourceCode
        }

        #endregion

        public static string sFileLoaded = "";

        public static O2AssessmentData_OunceV6 loadAssessmentFile(string sXmlFileToLoad)
        {
            return loadAssessmentFile(sXmlFileToLoad, false, false);
        }

        public static O2AssessmentData_OunceV6 loadAssessmentFile(string sXmlFileToLoad, bool bVerbose, bool bResolveReferences)
        {
            O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6 = null;
            loadAssessmentFile(sXmlFileToLoad, bVerbose, ref oadO2AssessmentDataOunceV6);
            if (oadO2AssessmentDataOunceV6 != null && bResolveReferences)
                populateDictionariesWithXrefsToLoadedAssessment(oadO2AssessmentDataOunceV6);
            return oadO2AssessmentDataOunceV6;
        }

        public static TimeSpan loadAssessmentFile(string sXmlFileToLoad, bool bVerbose,
                                                  ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            try
            {
                if (sXmlFileToLoad == null)
                    return new TimeSpan();
                fadO2AssessmentDataOunceV6 = new O2AssessmentData_OunceV6();
                DateTime dStartTime = DateTime.Now;
                // if (sXmlFileToLoad.IndexOf(".xml") == -1 && sXmlFileToLoad.IndexOf("ozasmt") == -1)
                // {
                //      DI.log.error("Invalid Assessment file provided {0}", sXmlFileToLoad);
                //     return new TimeSpan(); 
                // }
                if (bVerbose)
                    DI.log.info("Loading Assessment Xml File {0}", sFileLoaded);
                sFileLoaded = sXmlFileToLoad;

                fadO2AssessmentDataOunceV6.arAssessmentRun = OzasmtUtils_OunceV6.getAssessmentRunObjectFromXmlFile(sXmlFileToLoad);
                fadO2AssessmentDataOunceV6.sDb_id = OzasmtUtils_OunceV6.fromAssessmentFile_get_DbId(sXmlFileToLoad);
                //  populateDictionariesWithXrefsToLoadedAssessment();              // no need to this here 

                DateTime dEndTime = DateTime.Now;
                TimeSpan tsLoadAssessmentTime = dEndTime - dStartTime;
                if (bVerbose)
                    DI.log.info("Loaded Assessment Xml File: {0} in {1}.{2} seconds", sFileLoaded,
                                tsLoadAssessmentTime.Seconds.ToString(), tsLoadAssessmentTime.Milliseconds.ToString());

                fixFilePaths(fadO2AssessmentDataOunceV6);

                return tsLoadAssessmentTime;
            }
            catch (Exception ex)
            {
                DI.log.error("in loadAssessmentFile({0})   : {1}", sXmlFileToLoad, ex.Message);
                return new TimeSpan();
            }
        }


        public static void fixFilePaths(O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            String sFilePathVariableName = (vars.get("FilePathVariableName") == null)
                                               ? ""
                                               : vars.get("FilePathVariableName").ToString();
            String sFilePathVariablePath = (vars.get("sFilePathVariablePath") == null)
                                               ? ""
                                               : vars.get("sFilePathVariablePath").ToString();

            if (sFilePathVariableName == "" || sFilePathVariablePath == "")
                return;
            for (int i = 0; i < fadO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces.Length; i++)
            {
                fadO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces[i].value =
                    fadO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces[i].value.Replace(sFilePathVariableName,
                                                                                            sFilePathVariablePath);
            }
        }

        public static void populateDictionariesWithXrefsToLoadedAssessment(O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
        {
            FindingFilter ffFindingFilter = FindingFilter.AllFindings;
            bool bDropDuplicateSmartTraces = false;
            bool bIgnoreRootCallInvocation = false;
            populateDictionariesWithXrefsToLoadedAssessment(ffFindingFilter, bDropDuplicateSmartTraces,
                                                            bIgnoreRootCallInvocation, oadO2AssessmentDataOunceV6);
        }

        // this is used for quick queries (these dictionaries act like pointers to interresting stuff
        public static void populateDictionariesWithXrefsToLoadedAssessment(FindingFilter ffFindingFilter,
                                                                           bool bDropDuplicateSmartTraces,
                                                                           bool bIgnoreRootCallInvocation,
                                                                           O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
        {
            try
            {
                DateTime dtStart = DateTime.Now;
                // reset Dictionary objects
                oadO2AssessmentDataOunceV6.dAssessmentFiles =
                    new Dictionary<AssessmentAssessmentFile, List<AssessmentAssessmentFileFinding>>();
                oadO2AssessmentDataOunceV6.dVulnerabilityType = new Dictionary<string, List<AssessmentAssessmentFileFinding>>();
                oadO2AssessmentDataOunceV6.dFindings =
                    new Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile>();
                oadO2AssessmentDataOunceV6.dActionObjects = new Dictionary<uint, List<AssessmentAssessmentFileFinding>>();
                oadO2AssessmentDataOunceV6.dFindings_CallInvocation =
                    new Dictionary<AssessmentAssessmentFileFinding, List<CallInvocation>>();

                // make no changes to the finding's data
                FindingNameFormat ffnFindingNameFormat = FindingNameFormat.FindingType;
                bool bChangeFindingData = false;

                // create filter
                var fFilter = new AnalysisFilters.filter();

                if (ffFindingFilter == FindingFilter.SmartTraces)
                    //AnalysisFilters.filter_FindSmartTraces ffsmSmartTraces = 
                    fFilter = new AnalysisFilters.filter_FindSmartTraces(bDropDuplicateSmartTraces,
                                                                         bIgnoreRootCallInvocation, ffnFindingNameFormat,
                                                                         bChangeFindingData);
                else if (ffFindingFilter == FindingFilter.SmartTraces_LostSink)
                    fFilter = new AnalysisFilters.filter_FindLostSinks(bDropDuplicateSmartTraces,
                                                                       bIgnoreRootCallInvocation, ffnFindingNameFormat,
                                                                       bChangeFindingData);
                else if (ffFindingFilter == FindingFilter.SmartTraces_LostSink_Unique)
                    fFilter = new AnalysisFilters.filter_FindUniqueLostSinks(ffnFindingNameFormat, bChangeFindingData);
                // create list to contain all findings that match criteria
                oadO2AssessmentDataOunceV6.lfAllFindingsThatMatchCriteria = new List<AssessmentAssessmentFileFinding>();

                var lsAssessmentFiles = new List<String>();
                if (StringsAndLists.notNull(oadO2AssessmentDataOunceV6.arAssessmentRun, typeof (AssessmentRun).Name))
                    if (null != oadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                        foreach (Assessment aAssessment in oadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                            if (null != aAssessment.AssessmentFile)
                                foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                                {
                                    if (afAssessmentFile.Finding != null)
                                    {
                                        // create list to contain findings (from the current file) that match criteria
                                        var lfFindingsThatMatchCriteria = new List<AssessmentAssessmentFileFinding>();
                                        foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                        {
                                            // populate Findings Dictionary (dFindings)
                                            oadO2AssessmentDataOunceV6.dFindings.Add(fFinding, afAssessmentFile);
                                            // create list for dictionary with finding CallList
                                            oadO2AssessmentDataOunceV6.dFindings_CallInvocation.Add(fFinding,
                                                                                                    new List<CallInvocation>());
                                            // calculate CallList
                                            if (fFinding.Trace != null)
                                                AnalysisUtils.getListWithMethodsCalled_Recursive(fFinding.Trace,
                                                                                                 oadO2AssessmentDataOunceV6.
                                                                                                     dFindings_CallInvocation
                                                                                                     [fFinding],
                                                                                                 oadO2AssessmentDataOunceV6,
                                                                                                 SmartTraceFilter.
                                                                                                     MethodName);

                                            /*    Analysis.addCallsToNode_Recursive(fFinding.Trace, tnTempNode, fadO2AssessmentData, stfSmartTraceFilter);
                                                List<TreeNode> tnAllNodes = forms.getListWithAllNodesFromTreeView(tnTempNode.Nodes);
                                                foreach (TreeNode tnNode in tnAllNodes)
                                                    tnFinding.Nodes.Add((TreeNode)tnNode.Clone());*/

                                            // process filtered Findings
                                            if (ffFindingFilter == FindingFilter.AllFindings ||
                                                ffFindingFilter == FindingFilter.NoSmartTraces && fFinding.Trace == null)
                                            {
                                                lfFindingsThatMatchCriteria.Add(fFinding);
                                                oadO2AssessmentDataOunceV6.lfAllFindingsThatMatchCriteria.Add(fFinding);
                                            }
                                            else // which is this case
                                            {
                                                // run filter for the findings that have a trace
                                                if ((ffFindingFilter == FindingFilter.SmartTraces ||
                                                     ffFindingFilter == FindingFilter.SmartTraces_LostSink ||
                                                     ffFindingFilter == FindingFilter.SmartTraces_LostSink_Unique)
                                                    && fFinding.Trace != null)

                                                    //applyFilter(fFilter, lfFindingsThatMatchCriteria, fFinding, fadO2AssessmentData.arAssessmentRun);
                                                    if (applyFilter(fFilter,
                                                                    oadO2AssessmentDataOunceV6.lfAllFindingsThatMatchCriteria,
                                                                    fFinding, oadO2AssessmentDataOunceV6.arAssessmentRun))
                                                        lfFindingsThatMatchCriteria.Add(fFinding);
                                                    else
                                                    {
                                                    }
                                            }
                                        }
                                        // populate Assessment Files Dictionary (dAssessmentFiles)
                                        if (lfFindingsThatMatchCriteria.Count > 0)
                                        {
                                            oadO2AssessmentDataOunceV6.dAssessmentFiles.Add(afAssessmentFile,
                                                                                            lfFindingsThatMatchCriteria);
                                            // fadO2AssessmentData.lfAllFindingsThatMatchCriteria.AddRange(lfFindingsThatMatchCriteria);
                                        }
                                    }
                                }
                // populate lfAllFindingsThatMatchCriteria
                foreach (AssessmentAssessmentFileFinding fFinding in oadO2AssessmentDataOunceV6.lfAllFindingsThatMatchCriteria)
                {
                    String sVulnType = (fFinding.vuln_type != null)
                                           ? fFinding.vuln_type
                                           : OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_type_id),
                                                                                     oadO2AssessmentDataOunceV6);
                    //if (sVulnType != "Vulnerability.Sink.O2" && sVulnType !=  "Vulnerability.Source.O2")
                    //  { 
                    //  }
                    // VulnerabilityTypes
                    if (false == oadO2AssessmentDataOunceV6.dVulnerabilityType.ContainsKey(sVulnType))
                        // means this is the first Finding of this type
                        oadO2AssessmentDataOunceV6.dVulnerabilityType[sVulnType] = new List<AssessmentAssessmentFileFinding>();
                    oadO2AssessmentDataOunceV6.dVulnerabilityType[sVulnType].Add(fFinding);

                    // ActionObjects
                    if (false == oadO2AssessmentDataOunceV6.dActionObjects.ContainsKey(fFinding.actionobject_id))
                        // means this is the first Finding of this type
                        oadO2AssessmentDataOunceV6.dActionObjects[fFinding.actionobject_id] =
                            new List<AssessmentAssessmentFileFinding>();
                    oadO2AssessmentDataOunceV6.dActionObjects[fFinding.actionobject_id].Add(fFinding);
                }

                // fix externalSource source mapping issue
                fixExternalSourceMappingIssue(ref oadO2AssessmentDataOunceV6);
                TimeSpan spTimeSpan = DateTime.Now - dtStart;
                DI.log.info("Populated Dictionaries With Xrefs To Loaded Assessment in {0}.{1} seconds",
                            spTimeSpan.Minutes.ToString(), spTimeSpan.Milliseconds.ToString());
            }
            catch (Exception e)
            {
                DI.log.error("In populateDictionariesWithXrefsToLoadedAssessment: {0}", e.Message);
            }
        }

        public static void fixExternalSourceMappingIssue(ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            foreach (AssessmentAssessmentFileFinding fFinding in fadO2AssessmentDataOunceV6.dFindings.Keys)
            {
                CallInvocation ciCallInvocation =
                    AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                            TraceType.Source);
                if (fFinding.Trace != null && fFinding.Trace[0] != null && fFinding.Trace[0].CallInvocation1 != null)
                {
                    String sSource1 = OzasmtUtils_OunceV6.getStringIndexValue(fFinding.Trace[0].CallInvocation1[0].sig_id,
                                                                              fadO2AssessmentDataOunceV6);
                    if (sSource1.IndexOf("<external_source>") > -1)
                    {
                        fFinding.Trace[0].CallInvocation1[0].trace_type = (Int32) TraceType.Root_Call;
                        // change the current source to a normal trace 
                        fFinding.Trace[0].CallInvocation1[1].trace_type = (Int32) TraceType.Source;
                        // change the next trace item to a source
                        //fFinding.Trace[0].CallInvocation1[0].trace_type = (Int32)TraceType.Known_Sink;
                        String sSource2 = OzasmtUtils_OunceV6.getStringIndexValue(fFinding.Trace[0].CallInvocation1[1].sig_id,
                                                                                  fadO2AssessmentDataOunceV6);
                    }
                }
                //    if (ciCallInvocation != null)
                //       ciCallInvocation.cxt_id =10;
            }
        }

        /*  public static void addFindingToDictionaries(AssessmentAssessmentFileFinding fFinding)
        {                       
            // populate ActionObjects Dictionary (dActionObjects)
            if (false == dActionObjects.ContainsKey(fFinding.actionobject_id))
            {
                List<AssessmentAssessmentFileFinding> lfFindingsInActionObject = new List<AssessmentAssessmentFileFinding>();
                lfFindingsInActionObject.Add(fFinding);
                dActionObjects.Add(fFinding.actionobject_id, lfFindingsInActionObject);
            }
            else
            {
                dActionObjects[fFinding.actionobject_id].Add(fFinding);
            }
        }*/


        // this list contains the number of items on each entry

        /* DC - Removed in order to make the SavedAssessmentFile Project non Ounce dependent
        public static void calculateActionObjects_into_DataGridView(DataGridView dgvToPopulate, o2AssessmentDataOunceV6 fadO2AssessmentData)
        {
           
            List<String> lsActionObjects = getListWithUsedActionObjects(fadO2AssessmentData);
            
            if (lsActionObjects.Count > 0)
            {                               
                String sActionObjects = "";
                for (int i = 0; i < lsActionObjects.Count; i++)
                {
                    sActionObjects += " id = " + lsActionObjects[i];
                    if (i + 1 < lsActionObjects.Count) // means we are not in the one before last
                        sActionObjects += " or ";
                }
                String sSqlQuery = Lddb.getSqlQueryForRetrivingActionObjectsData(sActionObjects);
                
                // mySql.runQueryAndPopulateDataGrid(dgvToPopulate, sSqlQuery);
                // get DataTable with ActionObjectId
                dgvToPopulate.Columns.Clear();
                System.Data.DataTable dtDataTable = o2.ounce.datalayer.mysql.OunceMyql.getDataTableFromSqlQuery(sSqlQuery, true);
                // if dtDataTable contains data, bind it to DataGridView

                if (dtDataTable.Rows.Count > 0)
                {

                    dtDataTable.Columns.Add("# Findings",typeof(Int32));
                    dtDataTable.Columns.Add("# SmartTraces", typeof(Int32));
                    dtDataTable.Columns.Add("# SmartTraces ND", typeof(Int32));
                    dtDataTable.Columns.Add("# SmartTraces IR", typeof(Int32));
                    dtDataTable.Columns.Add("# LostSinks", typeof(Int32));
                    dtDataTable.Columns.Add("# LostSinks ND", typeof(Int32));
                    dtDataTable.Columns.Add("# LostSinks IR", typeof(Int32));                    
                    foreach (DataRow drRow in dtDataTable.Rows)
                    {
                        if (drRow["id"] != null)
                        {                            
                            UInt32 iActionObjectId = (UInt32)(drRow["id"]);
                            int iFindings = 0, iAssessmentFiles = 0, iSmartTraces = 0, iLostSinks = 0,
                                iSmartTraces_NotDuplicate = 0, iSmartTraces_NotDuplicate_IgnoreRoot = 0, iLostSinks_NotDuplicate = 0, iLostSinks_NotDuplicate_IgnoreRoot = 0;
                            calculateFindingsStatistics(fadO2AssessmentData.arAssessmentRun, iActionObjectId, true,
                                ref iFindings, ref iAssessmentFiles,
                                ref iSmartTraces, ref iLostSinks,
                                ref iSmartTraces_NotDuplicate, ref  iSmartTraces_NotDuplicate_IgnoreRoot,
                                ref iLostSinks_NotDuplicate, ref iLostSinks_NotDuplicate_IgnoreRoot);
                            
                            drRow["# Findings"] = iFindings;
                            drRow["# SmartTraces"] = iSmartTraces;
                            drRow["# SmartTraces ND"] = iSmartTraces_NotDuplicate;
                            drRow["# SmartTraces IR"] = iSmartTraces_NotDuplicate_IgnoreRoot;
                            drRow["# LostSinks"] = iLostSinks;
                            drRow["# LostSinks ND"] = iLostSinks_NotDuplicate;
                            drRow["# LostSinks IR"] = iLostSinks_NotDuplicate_IgnoreRoot;                            

                        }
                    }
                    dgvToPopulate.DataSource = dtDataTable;
                    O2Forms.dataGridView_Utils_MaxColumnsWidth(dgvToPopulate);
                    if (dgvToPopulate.Rows.Count > 0)
                        dgvToPopulate.Columns[0].Selected = true;
                }
            }
        }

        */
        /* DC - Removed in order to make the SavedAssessmentFile Project non Ounce dependent
        public static void createSeparateAssessmentFilesForEachActionObject(FindingNameFormat ffnFindingNameFormat, bool bDropFindingsWithNoTraces, bool bFilterDuplicateFindings, bool bIgnoreRootCallInvocation, bool bChangeFindingData, o2AssessmentDataOunceV6 fadO2AssessmentData)
        {
             DI.log.info("createSeparateAssessmentFilesForEachActionObject Start");
            List<String> lsActionObjects = getListWithUsedActionObjects(fadO2AssessmentData);
            foreach(String sActionObjectId in lsActionObjects)
            {
                createSeparateAssessmentFileForActionObject(sActionObjectId, ffnFindingNameFormat, bDropFindingsWithNoTraces, bFilterDuplicateFindings, bIgnoreRootCallInvocation, bChangeFindingData, fadO2AssessmentData);  
            }
             DI.log.info("createSeparateAssessmentFilesForEachActionObject End");
        }
        */

        public static void createSeparateAssessmentFileForSmartTraceCall(UInt32 uSmartTraceCallID,
                                                                         TraceType tTraceType,
                                                                         FindingNameFormat ffnFindingNameFormat,
                                                                         bool bFilterDuplicateFindings,
                                                                         bool bIgnoreRootCallInvocation,
                                                                         bool bChangeFindingData,
                                                                         O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            String sSmartTraceCall = fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[uSmartTraceCallID - 1].value;
            sSmartTraceCall = sSmartTraceCall.Replace(":", "_").Replace("*", "_");
            // remove the : from SmartTrace name (since it prevents the file from being created)
            String sFileName = calculateTargetFileName(uSmartTraceCallID + "_(" + sSmartTraceCall + ")_LOST_SINK",
                                                       ffnFindingNameFormat);
            DI.log.debug("Creating partial assessment file for Lost Sink: {0}", sFileName);
            createXmlFileWithOnlyFindingsFromSmartTraceCall(sFileName, ffnFindingNameFormat, uSmartTraceCallID,
                                                            tTraceType, bFilterDuplicateFindings,
                                                            bIgnoreRootCallInvocation, bChangeFindingData,
                                                            fadO2AssessmentDataOunceV6);
        }

        /* DC - Removed in order to make the SavedAssessmentFile Project non Ounce dependent
        public static void createSeparateAssessmentFileForActionObject(String sActionObjectId, FindingNameFormat ffnFindingNameFormat, bool bDropFindingsWithNoTraces, bool bFilterDuplicateFindings, bool bIgnoreRootCallInvocation, bool bChangeFindingData, o2AssessmentDataOunceV6 fadO2AssessmentData)
        {
            String sActionObjectName = Lddb.getActionObjectName(sActionObjectId);
            String sFileName = calculateTargetFileName(sActionObjectId + "_(" + sActionObjectName + ")" , ffnFindingNameFormat);
             DI.log.debug("Creating partial assessment file for ActionObjectID: {0}", sFileName);
            createXmlFileWithOnlyFindingsFromActionObject(sFileName, ffnFindingNameFormat, sActionObjectId, bDropFindingsWithNoTraces, bFilterDuplicateFindings, bIgnoreRootCallInvocation, bChangeFindingData, fadO2AssessmentData);
          //  restoreChangedData(bChangeFindingData);  
        }
        */

        public static String calculateTargetFileName(String sExtraTag, FindingNameFormat ffnFindingNameFormat)
        {
            String sTargetDirectory = Path.GetDirectoryName(sFileLoaded) + "\\Analysis";
            if (false == Directory.Exists(sTargetDirectory))
                Directory.CreateDirectory(sTargetDirectory);
            if (ffnFindingNameFormat != FindingNameFormat.FindingType)
                sExtraTag += "       (" + ffnFindingNameFormat + ")";
            return sTargetDirectory + "\\" + Path.GetFileName(sFileLoaded) + "_" + sExtraTag + ".xml";
        }


        public static void createXmlFileWithOnlyFindingsFromActionObject(String sTargetFileName,
                                                                         FindingNameFormat ffnFindingNameFormat,
                                                                         String sActionObjectIdToFind,
                                                                         bool bDropFindingsWithNoTraces,
                                                                         bool bFilterDuplicateFindings,
                                                                         bool bIgnoreRootCallInvocation,
                                                                         bool bChangeFindingData,
                                                                         O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            var ffaoFilter = new AnalysisFilters.filter_FindActionObject(sActionObjectIdToFind,
                                                                         bDropFindingsWithNoTraces,
                                                                         bFilterDuplicateFindings,
                                                                         bIgnoreRootCallInvocation, ffnFindingNameFormat,
                                                                         bChangeFindingData);
            AssessmentRun arFilteredAssessmentRun = createFilteredAssessmentRunObjectBasedOnCriteria(ffaoFilter,
                                                                                                     fadO2AssessmentDataOunceV6);

            saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFileName, fadO2AssessmentDataOunceV6);

            restoreChangedData(bChangeFindingData, fadO2AssessmentDataOunceV6);
        }

        public static void createXmlFileWithOnlyFindingsFromSmartTraceCall(String sTargetFileName,
                                                                           FindingNameFormat ffnFindingNameFormat,
                                                                           UInt32 uSmartTraceCallID,
                                                                           TraceType tTraceType,
                                                                           bool bFilterDuplicateFindings,
                                                                           bool bIgnoreRootCallInvocation,
                                                                           bool bChangeFindingData,
                                                                           O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            var ffstidFilter = new AnalysisFilters.filter_FindSmartTrace_byID(uSmartTraceCallID, tTraceType,
                                                                              bFilterDuplicateFindings,
                                                                              bIgnoreRootCallInvocation,
                                                                              ffnFindingNameFormat, bChangeFindingData);

            AssessmentRun arFilteredAssessmentRun = createFilteredAssessmentRunObjectBasedOnCriteria(ffstidFilter,
                                                                                                     fadO2AssessmentDataOunceV6);

            saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFileName, fadO2AssessmentDataOunceV6);

            restoreChangedData(bChangeFindingData, fadO2AssessmentDataOunceV6);
        }

        // this will make permanate changes to the supplied fadO2AssessmentData (which is why it is nulled at the end)
        public static void createAssessmentFileFromFindingsDictionary(
            Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile> dFindings,
            O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            throw new Exception("createAssessmentFileFromFindingsDictionary not implemented yet");
            /*
            AssessmentRun arAssessmentRun = getDefaultAssessmentRunObject();

            Assessment aAssessment = arAssessmentRun.Assessment.Assessment[0];
            foreach (AssessmentAssessmentFileFinding fFinding in dFindings.Keys)
            { 
                
            }

            fadO2AssessmentDataOunceV6 = null;*/
        }

        public static void saveFilteredAssessmentRun(AssessmentRun arFilteredAssessmentRun, String sTargetFileName,
                                                     O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            if (arFilteredAssessmentRun.Assessment.Assessment[0].AssessmentFile == null)
                DI.log.error(
                    "   .There were no AssessmentFiles (with Fidings) using the current filter, so no file will be created");
            else
            {
                // add the stringIndeces and FileIndexes from the original file  (ideally these should be filtered so that only the ones that are used are include
                arFilteredAssessmentRun.StringIndeces = fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces;
                arFilteredAssessmentRun.FileIndeces = fadO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces;

                // so that we can apply change back to the original project
                arFilteredAssessmentRun.Assessment.owner_name =
                    fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.owner_name;
                arFilteredAssessmentRun.Assessment.owner_type =
                    fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.owner_type;
                arFilteredAssessmentRun.Assessment.Assessment[0].owner_name =
                    fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment[0].owner_name;
                arFilteredAssessmentRun.Assessment.Assessment[0].owner_type =
                    fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment[0].owner_type;


                // and save the serialized object as an Xml file
                OzasmtUtils_OunceV6.createSerializedXmlFileFromAssessmentRunObject(arFilteredAssessmentRun, sTargetFileName);

                // and display a quick analysis
                outputQuickAnalysisOfAssessmentRunObject(arFilteredAssessmentRun);
            }
        }


        public static AssessmentRun createFilteredAssessmentRunObjectBasedOnCriteria(AnalysisFilters.filter fFilter,
                                                                                     O2AssessmentData_OunceV6
                                                                                         fadO2AssessmentDataOunceV6)
        {
            AssessmentRun arFilteredAssessmentRun = OzasmtUtils_OunceV6.getDefaultAssessmentRunObject();

            // create list to contain the filtered AssessmentFiles
            var lafFilteredAssessmentFiles = new List<AssessmentAssessmentFile>();

            if (StringsAndLists.notNull(fadO2AssessmentDataOunceV6.arAssessmentRun, typeof (AssessmentRun).Name))
                if (null != fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                    foreach (Assessment aAssessment in fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                    {
                        foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                        {
                            // create filtered AssesmentFile object
                            var afFilteredAssessmentFile = new AssessmentAssessmentFile();
                            // and copy the important values from the original into it
                            afFilteredAssessmentFile.filename = afAssessmentFile.filename;
                            afFilteredAssessmentFile.error_status = afAssessmentFile.error_status;
                            afFilteredAssessmentFile.last_modified_time = afAssessmentFile.last_modified_time;
                            // create list to contain Findings that match filter
                            var lfFindingsThatMatchCriteria = new List<AssessmentAssessmentFileFinding>();
                            // if there are findings
                            if (null != afAssessmentFile.Finding)
                            {
                                foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                    fFilter.applyFilterAndPopulateList(fadO2AssessmentDataOunceV6.arAssessmentRun, fFinding,
                                                                       lfFindingsThatMatchCriteria,
                                                                       lafFilteredAssessmentFiles);
                                // invoke the respective filter

                                if (lfFindingsThatMatchCriteria.Count > 0)
                                    // if there were findings in this AssessmentFile
                                {
                                    afFilteredAssessmentFile.Finding = lfFindingsThatMatchCriteria.ToArray();
                                    // map them to the Finding array
                                    lafFilteredAssessmentFiles.Add(afFilteredAssessmentFile);
                                    // add add the filtered AssessmentFile to its list
                                }
                            }
                        }
                    }

            // (if there are some)add the filtered AssessentFiles into the Filtered AssessmentRun object
            if (lafFilteredAssessmentFiles.Count > 0)
                arFilteredAssessmentRun.Assessment.Assessment[0].AssessmentFile = lafFilteredAssessmentFiles.ToArray();
            // for now all consolidate all projects into one
            return arFilteredAssessmentRun;
        }

        public static void mapSmartTraceCall_into_DataGridView(DataGridView dgvDataGridView,
                                                               TraceType tTraceType,
                                                               O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            var lsSmartTraceCalls = new List<Int32>();

            dgvDataGridView.Columns.Clear();
            dgvDataGridView.Columns.Add("ID", tTraceType + " ID");
            dgvDataGridView.Columns.Add("Signature", tTraceType + " Signature");
            dgvDataGridView.Columns.Add("Number of Occurences", "Number of Occurences");
            dgvDataGridView.Columns.Add("Number of Occurences ND", "Number of Occurences ND");
            dgvDataGridView.Columns.Add("Number of Occurences IR", "Number of Occurences IR");

            int iFindings = 0, iTracesFound = 0;
            if (StringsAndLists.notNull(fadO2AssessmentDataOunceV6.arAssessmentRun, typeof (AssessmentRun).Name))
                if (null != fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                    foreach (Assessment aAssessment in fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                        foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                            if (null != afAssessmentFile.Finding)
                                foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                {
                                    iFindings++;
                                    if (fFinding.Trace != null)
                                    {
                                        iTracesFound++;
                                        int iSmartTraces =
                                            AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnSigId(
                                                fFinding.Trace, tTraceType);
                                        if (-1 != iSmartTraces && false == lsSmartTraceCalls.Contains(iSmartTraces))
                                            lsSmartTraceCalls.Add(iSmartTraces);
                                    }
                                }

            dgvDataGridView.DataSource = null;
            int iSmartTrace_Sum = 0, iNotDuplicated_Sum = 0, iIgnoreRoot_Sum = 0;
            /// need to figure out what are the cases when iSmartTraceIndex =0
            foreach (int iSmartTraceIndex in lsSmartTraceCalls)
                if (iSmartTraceIndex > 0)
                {
                    int iSmartTrace = 0, iNotDuplicated = 0, iIgnoreRoot = 0;
                    calculateSmartTraceCallStatistics(fadO2AssessmentDataOunceV6.arAssessmentRun, (UInt32) iSmartTraceIndex,
                                                      tTraceType, ref iSmartTrace, ref iNotDuplicated, ref iIgnoreRoot);
                    iSmartTrace_Sum += iSmartTrace;
                    iNotDuplicated_Sum += iNotDuplicated;
                    iIgnoreRoot_Sum += iIgnoreRoot;
                    dgvDataGridView.Rows.Add(new Object[]
                                                 {
                                                     iSmartTraceIndex,
                                                     fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[
                                                         iSmartTraceIndex - 1].value,
                                                     //iKey-1 because of the way StringIndeces is populated   
                                                     iSmartTrace,
                                                     iNotDuplicated,
                                                     iIgnoreRoot
                                                 });
                }
                else
                    DI.log.error("_NOT RECOGNIZED STRING for ID ", iSmartTraceIndex.ToString());


            // reset DataGridView Column width
            O2Forms.dataGridView_Utils_MaxColumnsWidth(dgvDataGridView);
            // all done
            DI.log.info(
                "Found {0} Unique SmartTraces of Type {1}   (In {2} Findings (with {3} traces)). Totals: SmartTraces={4} , NonDuplicated={5} , IgnoreRoot = {6} ",
                lsSmartTraceCalls.Count.ToString(), tTraceType.ToString(), iFindings.ToString(), iTracesFound.ToString(),
                iSmartTrace_Sum.ToString(), iNotDuplicated_Sum.ToString(), iIgnoreRoot_Sum.ToString());
        }

        /*
        public static void findLostSinks_into_DataGridView(DataGridView dgvDataGridView)
        {
            List<String> lsLostSinks = new List<string>();
            Dictionary<Int32, Int32> dLostSinks = new Dictionary<Int32, Int32>();
            
            dgvDataGridView.Columns.Clear();
            dgvDataGridView.Columns.Add("ID", "LostSink ID");
            dgvDataGridView.Columns.Add("LostSink", "LostSink Name");
            dgvDataGridView.Columns.Add("Number of Occurences", "Number of Occurences");
            dgvDataGridView.Columns.Add("Number of Occurences ND", "Number of Occurences ND");
            dgvDataGridView.Columns.Add("Number of Occurences IR", "Number of Occurences IR");
            int iLostSinksFound = 0, iTracesFound = 0, iFindings = 0 ;
           
             if (o2.core.Utils.notNull(arAssessmentRun,typeof(AssessmentRun).Name))
                if (null != arAssessmentRun.Assessment.Assessment)
                    foreach (Assessment aAssessment in arAssessmentRun.Assessment.Assessment)
                    {
                        foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                            if (null != afAssessmentFile.Finding)
                                foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                {
                                    iFindings++;
                                    if (fFinding.Trace != null)
                                    {
                                        iTracesFound++;

                                        int iLostSink_SigId = findInSmartTrace_Recursive(fFinding.Trace[0].CallInvocation1,TraceType.Lost_Sink);
                                        if (iLostSink_SigId != -1)
                                        {
                                            iLostSinksFound++;
                                            if (dLostSinks.ContainsKey(iLostSink_SigId))
                                                dLostSinks[iLostSink_SigId]++;
                                            else
                                                dLostSinks[iLostSink_SigId] = 1;
                                        }
                                    }
                                }
                    }

            dgvDataGridView.DataSource = null;
            foreach (int iKey in dLostSinks.Keys)
                if (iKey > -1)
                {
                    int iSmartTrace = 0, iNonDuplicated = 0, iIgnoreRoot = 0;
                    calculateSmartTraceCallStatistics(arAssessmentRun, (UInt32)iKey, ref tTraceType, iSmartTrace, ref iNonDuplicated, ref iIgnoreRoot);
                    dgvDataGridView.Rows.Add(new Object[] {
                        iKey,  
                        arAssessmentRun.StringIndeces[iKey - 1].value, //iKey-1 because of the way StringIndeces is populated   
                        //dLostSinks[iKey],                                       
                        iSmartTrace,                         
                        iNonDuplicated,
                        iIgnoreRoot
                    });
                }
                else
                    dgvDataGridView.Rows.Add(new Object[] { "_NOT RECOGNIZED STRINGS", dLostSinks[iKey] });
            // reset DataGridView Column width
            forms.dataGridView_Utils_MaxColumnsWidth(dgvDataGridView);
             DI.log.info("Found {0} Unique Lost Sinks , out of {1} Lost Sinks, out of {2} Traces, out of {3} Findings",
                dLostSinks.Keys.Count.ToString(), iLostSinksFound.ToString(), iTracesFound.ToString(), iFindings.ToString());
        }*/

        public static void outputQuickAnalysisOfAssessmentRunObject(AssessmentRun arAssessmentRunToAnalyze)
        {
            int iFindings = 0,
                iAssessmentFiles = 0,
                iSmartTraces = 0,
                iLostSinks = 0,
                iSmartTraces_NotDuplicate = 0,
                iSmartTraces_NotDuplicate_IgnoreRoot = 0,
                iLostSinks_NotDuplicate = 0,
                iLostSinks_NotDuplicate_IgnoreRoot = 0;
            calculateFindingsStatistics(arAssessmentRunToAnalyze,
                                        ref iFindings, ref iAssessmentFiles,
                                        ref iSmartTraces, ref iLostSinks,
                                        ref iSmartTraces_NotDuplicate, ref iSmartTraces_NotDuplicate_IgnoreRoot,
                                        ref iLostSinks_NotDuplicate, ref iLostSinks_NotDuplicate_IgnoreRoot);
            DI.log.info(
                "   .AssessmentRun Stats: {0} Assesment files, {1} Findings, {2} Smart traces (DD={3} - IR={4}) , {5} Lost Sinks (ND={6} - IR={7})",
                iAssessmentFiles.ToString(), iFindings.ToString()
                , iSmartTraces.ToString(), iSmartTraces_NotDuplicate.ToString(),
                iSmartTraces_NotDuplicate_IgnoreRoot.ToString()
                , iLostSinks.ToString(), iLostSinks_NotDuplicate.ToString(),
                iLostSinks_NotDuplicate_IgnoreRoot.ToString());
        }

        // use this one for global queries (since we are not going to filter by ActionObjectID);
        public static void calculateFindingsStatistics(AssessmentRun arAssessmentRunToAnalyze,
                                                       ref int iFindings, ref int iAssessmentFiles,
                                                       ref int iSmartTraces, ref int iLostSinks,
                                                       ref int iSmartTraces_NotDuplicate,
                                                       ref int iSmartTraces_NotDuplicate_IgnoreRoot,
                                                       ref int iLostSinks_NotDuplicate,
                                                       ref int iLostSinks_NotDuplicate_IgnoreRoot)
        {
            UInt32 iActionObjectId = 0; // this could be any number since it is bMatchActionObjectId that decides 
            bool bMatchActionObjectId = false;
            calculateFindingsStatistics(arAssessmentRunToAnalyze, iActionObjectId, bMatchActionObjectId,
                                        ref iFindings, ref iAssessmentFiles,
                                        ref iSmartTraces, ref iLostSinks,
                                        ref iSmartTraces_NotDuplicate, ref iSmartTraces_NotDuplicate_IgnoreRoot,
                                        ref iLostSinks_NotDuplicate, ref iLostSinks_NotDuplicate_IgnoreRoot);
        }

        public static void calculateFindingsStatistics(AssessmentRun arAssessmentRunToAnalyze, UInt32 iActionObjectId,
                                                       bool bMatchActionObjectId,
                                                       ref int iFindings, ref int iAssessmentFiles,
                                                       ref int iSmartTraces, ref int iLostSinks,
                                                       ref int iSmartTraces_NotDuplicate,
                                                       ref int iSmartTraces_NotDuplicate_IgnoreRoot,
                                                       ref int iLostSinks_NotDuplicate,
                                                       ref int iLostSinks_NotDuplicate_IgnoreRoot)
        {
            try
            {
                if (arAssessmentRunToAnalyze == null)
                    return;

                FindingNameFormat ffnFindingNameFormat = FindingNameFormat.FindingType;
                // using default value (we are not going to need this value here (since we are only calculating statistics))
                bool bChangeFindingData = false; // this is the value that prevents changes

                bool bIgnoreRootCallInvocation = false;
                bool bDropDuplicateSmartTraces = false;

                // filters to find all SmartTraces and Lost Sinks
                var ffsmSmartTraces = new AnalysisFilters.filter_FindSmartTraces(bDropDuplicateSmartTraces,
                                                                                 bIgnoreRootCallInvocation,
                                                                                 ffnFindingNameFormat,
                                                                                 bChangeFindingData);
                var fflLostSinks = new AnalysisFilters.filter_FindLostSinks(bDropDuplicateSmartTraces,
                                                                            bIgnoreRootCallInvocation,
                                                                            ffnFindingNameFormat, bChangeFindingData);

                //filters to find SmartTraces and Lost Sinks when droping duplicate smart traces (bDropDuplicateSmartTraces = true;)
                bDropDuplicateSmartTraces = true;
                var ffsmSmartTraces_NotDuplicated = new AnalysisFilters.filter_FindSmartTraces(
                    bDropDuplicateSmartTraces, bIgnoreRootCallInvocation, ffnFindingNameFormat, bChangeFindingData);
                var fflLostSinks_NotDuplicated = new AnalysisFilters.filter_FindLostSinks(bDropDuplicateSmartTraces,
                                                                                          bIgnoreRootCallInvocation,
                                                                                          ffnFindingNameFormat,
                                                                                          bChangeFindingData);

                //filters to find SmartTraces and Lost Sinks when droping duplicate smart traces AND Ignoring the root call invocation (bIgnoreRootCallInvocation = true;)
                bIgnoreRootCallInvocation = true;
                var ffsmSmartTraces_NotDuplicated_IgnoreRoot =
                    new AnalysisFilters.filter_FindSmartTraces(bDropDuplicateSmartTraces, bIgnoreRootCallInvocation,
                                                               ffnFindingNameFormat, bChangeFindingData);
                var fflLostSinks_NotDuplicated_IgnoreRoot =
                    new AnalysisFilters.filter_FindLostSinks(bDropDuplicateSmartTraces, bIgnoreRootCallInvocation,
                                                             ffnFindingNameFormat, bChangeFindingData);


                // create lists to hold results
                var lfFindingsThatMatchCriteria_SmartTraces = new List<AssessmentAssessmentFileFinding>();
                var lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated = new List<AssessmentAssessmentFileFinding>();
                var lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated_IgnoreRoot =
                    new List<AssessmentAssessmentFileFinding>();

                var lfFindingsThatMatchCriteria_LostSinks = new List<AssessmentAssessmentFileFinding>();
                var lfFindingsThatMatchCriteria_LostSinks_NotDuplicated = new List<AssessmentAssessmentFileFinding>();
                var lfFindingsThatMatchCriteria_LostSinks_NotDuplicated_IgnoreRoot =
                    new List<AssessmentAssessmentFileFinding>();


                if (StringsAndLists.notNull(arAssessmentRunToAnalyze, typeof (AssessmentRun).Name) &&
                    null != arAssessmentRunToAnalyze.Assessment.Assessment)
                    foreach (Assessment aAssessment in arAssessmentRunToAnalyze.Assessment.Assessment)
                    {
                        foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                        {
                            iAssessmentFiles++;
                            if (null != afAssessmentFile.Finding)
                                foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                {
                                    if (false == bMatchActionObjectId || fFinding.actionobject_id == iActionObjectId)
                                        // bMatchActionObjectId decides if we filter the results by actionObjectID
                                    {
                                        iFindings++;
                                        if (null != fFinding.Trace)
                                        {
                                            applyFilter(ffsmSmartTraces, lfFindingsThatMatchCriteria_SmartTraces,
                                                        fFinding, arAssessmentRunToAnalyze);
                                            applyFilter(ffsmSmartTraces_NotDuplicated,
                                                        lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated, fFinding,
                                                        arAssessmentRunToAnalyze);
                                            applyFilter(ffsmSmartTraces_NotDuplicated_IgnoreRoot,
                                                        lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated_IgnoreRoot,
                                                        fFinding, arAssessmentRunToAnalyze);
                                            applyFilter(fflLostSinks, lfFindingsThatMatchCriteria_LostSinks, fFinding,
                                                        arAssessmentRunToAnalyze);
                                            applyFilter(fflLostSinks_NotDuplicated,
                                                        lfFindingsThatMatchCriteria_LostSinks_NotDuplicated, fFinding,
                                                        arAssessmentRunToAnalyze);
                                            applyFilter(fflLostSinks_NotDuplicated_IgnoreRoot,
                                                        lfFindingsThatMatchCriteria_LostSinks_NotDuplicated_IgnoreRoot,
                                                        fFinding, arAssessmentRunToAnalyze);
                                        }
                                    }
                                }
                        }
                    }
                iSmartTraces = lfFindingsThatMatchCriteria_SmartTraces.Count;
                iSmartTraces_NotDuplicate = lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated.Count;
                iSmartTraces_NotDuplicate_IgnoreRoot =
                    lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated_IgnoreRoot.Count;
                iLostSinks = lfFindingsThatMatchCriteria_LostSinks.Count;
                iLostSinks_NotDuplicate = lfFindingsThatMatchCriteria_LostSinks_NotDuplicated.Count;
                iLostSinks_NotDuplicate_IgnoreRoot =
                    lfFindingsThatMatchCriteria_LostSinks_NotDuplicated_IgnoreRoot.Count;
            }
            catch (Exception e)
            {
                DI.log.error("In calculateFindingsStatistics: {0}", e.Message);
            }
        }

        public static void calculateSmartTraceCallStatistics(AssessmentRun arAssessmentRunToAnalyze, UInt32 uKey,
                                                             TraceType tTraceType, ref int iSmartTraces,
                                                             ref int iNonDuplicated, ref int iIgnoreRoot)
        {
            if (arAssessmentRunToAnalyze == null)
                return;

            FindingNameFormat ffnFindingNameFormat = FindingNameFormat.FindingType;
            // using default value (we are not going to need this value here (since we are only calculating statistics))
            bool bChangeFindingData = false; // this is the value that prevents changes

            bool bDropDuplicateSmartTraces = false;
            bool bIgnoreRootCallInvocation = false;
            var ffsmSmartTraces = new AnalysisFilters.filter_FindSmartTrace_byID(uKey, tTraceType,
                                                                                 bDropDuplicateSmartTraces,
                                                                                 bIgnoreRootCallInvocation,
                                                                                 ffnFindingNameFormat,
                                                                                 bChangeFindingData);
            bDropDuplicateSmartTraces = true;
            var ffsmSmartTraces_NotDuplicated = new AnalysisFilters.filter_FindSmartTrace_byID(uKey, tTraceType,
                                                                                               bDropDuplicateSmartTraces,
                                                                                               bIgnoreRootCallInvocation,
                                                                                               ffnFindingNameFormat,
                                                                                               bChangeFindingData);
            bIgnoreRootCallInvocation = true;
            var ffsmSmartTraces_NotDuplicated_IgnoreRoot = new AnalysisFilters.filter_FindSmartTrace_byID(uKey,
                                                                                                          tTraceType,
                                                                                                          bDropDuplicateSmartTraces,
                                                                                                          bIgnoreRootCallInvocation,
                                                                                                          ffnFindingNameFormat,
                                                                                                          bChangeFindingData);


            // create lists to hold results
            var lfFindingsThatMatchCriteria_SmartTraces = new List<AssessmentAssessmentFileFinding>();
            var lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated = new List<AssessmentAssessmentFileFinding>();
            var lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated_IgnoreRoot =
                new List<AssessmentAssessmentFileFinding>();

            if (StringsAndLists.notNull(arAssessmentRunToAnalyze, typeof (AssessmentRun).Name) &&
                null != arAssessmentRunToAnalyze.Assessment.Assessment)
                foreach (Assessment aAssessment in arAssessmentRunToAnalyze.Assessment.Assessment)
                {
                    foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                        if (null != afAssessmentFile.Finding)
                            foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                if (null != fFinding.Trace)
                                {
                                    applyFilter(ffsmSmartTraces, lfFindingsThatMatchCriteria_SmartTraces, fFinding,
                                                arAssessmentRunToAnalyze);
                                    applyFilter(ffsmSmartTraces_NotDuplicated,
                                                lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated, fFinding,
                                                arAssessmentRunToAnalyze);
                                    applyFilter(ffsmSmartTraces_NotDuplicated_IgnoreRoot,
                                                lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated_IgnoreRoot,
                                                fFinding, arAssessmentRunToAnalyze);
                                }
                }
            iSmartTraces = lfFindingsThatMatchCriteria_SmartTraces.Count;
            iNonDuplicated = lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated.Count;
            iIgnoreRoot = lfFindingsThatMatchCriteria_SmartTraces_NotDuplicated_IgnoreRoot.Count;
        }

        public static bool applyFilter(AnalysisFilters.filter fFilterToApply,
                                       List<AssessmentAssessmentFileFinding> lfTargetList,
                                       AssessmentAssessmentFileFinding fFinding, AssessmentRun arAssessmentRunToAnalyze)
        {
            List<AssessmentAssessmentFile> lafFilteredAssessmentFiles = null;
            // we are not using this here so make it null (all findings to analyze are provided one by one)

            // invoke filter
            return fFilterToApply.applyFilterAndPopulateList(arAssessmentRunToAnalyze, fFinding, lfTargetList,
                                                             lafFilteredAssessmentFiles);
        }


        public static String getSmartTraceNameOfTraceType(CallInvocation[] cCallInvocations,
                                                          TraceType tTraceType,
                                                          O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            if (cCallInvocations != null)
            {
                int iActionObjectId = AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnSigId(cCallInvocations,
                                                                                                     tTraceType);
                if (iActionObjectId > 0)
                    return fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[iActionObjectId - 1].value;
            }
            return "";
        }

        public static String createAssessmentFileWithAllTraces(bool bDropDuplicateSmartTraces,
                                                               bool bIgnoreRootCallInvocation, string ozasmtFileToLoad,
                                                               string sTargetFilename)
        {
            O2AssessmentData_OunceV6 o2AssessmentDataOunceV6 = loadAssessmentFile(ozasmtFileToLoad);
            return createAssessmentFileWithAllTraces(bDropDuplicateSmartTraces, bIgnoreRootCallInvocation,
                                                     o2AssessmentDataOunceV6, sTargetFilename);
        }

        public static String createAssessmentFileWithAllTraces(bool bDropDuplicateSmartTraces,
                                                               bool bIgnoreRootCallInvocation,
                                                               O2AssessmentData_OunceV6 o2AssessmentDataOunceV6, string sTargetFilename)
        {
            FindingNameFormat ffnFindingNameFormat = FindingNameFormat.FindingType;
            const bool bChangeFindingData = false;
            return createAssessmentFileWithAllTraces(bDropDuplicateSmartTraces, bIgnoreRootCallInvocation,
                                                     ffnFindingNameFormat,
                                                     bChangeFindingData, o2AssessmentDataOunceV6, sTargetFilename);
        }


        public static String createAssessmentFileWithAllTraces(bool bDropDuplicateSmartTraces,
                                                               bool bIgnoreRootCallInvocation,
                                                               FindingNameFormat ffnFindingNameFormat,
                                                               bool bChangeFindingData,
                                                               O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            string sTargetFilename = calculateTargetFileName("ALL_TRACES", ffnFindingNameFormat);
            return createAssessmentFileWithAllTraces(bDropDuplicateSmartTraces, bIgnoreRootCallInvocation,
                                                     ffnFindingNameFormat,
                                                     bChangeFindingData, fadO2AssessmentDataOunceV6, sTargetFilename);
        }

        public static String createAssessmentFileWithAllTraces(bool bDropDuplicateSmartTraces,
                                                               bool bIgnoreRootCallInvocation,
                                                               FindingNameFormat ffnFindingNameFormat,
                                                               bool bChangeFindingData,
                                                               O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6,
                                                               string sTargetFilename)
        {
            var ffsmFilter = new AnalysisFilters.filter_FindSmartTraces(bDropDuplicateSmartTraces,
                                                                        bIgnoreRootCallInvocation, ffnFindingNameFormat,
                                                                        bChangeFindingData);
            AssessmentRun arFilteredAssessmentRun = createFilteredAssessmentRunObjectBasedOnCriteria(ffsmFilter,
                                                                                                     fadO2AssessmentDataOunceV6);
            saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFilename, fadO2AssessmentDataOunceV6);
            DI.log.debug("Custom Assessment File with All Traces created: {0}", sTargetFilename);
            restoreChangedData(bChangeFindingData, fadO2AssessmentDataOunceV6);
            return sTargetFilename;
        }

        public static String createAssessmentFileWithLostSinks(bool bDropDuplicatedSmartTraces,
                                                               bool bIgnoreRootCallInvocation,
                                                               FindingNameFormat ffnFindingNameFormat,
                                                               bool bChangeFindingData,
                                                               O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            // create file to store results using a name that reflects the search criteria
            String sTargetFilenameExtraTag = "LOST_SINKS";
            //  if (bDropDuplicatedSmartTraces) sTargetFilenameExtraTag += "_NoDuplicates";
            //  if (bIgnoreRootCallInvocation)  sTargetFilenameExtraTag += "_IgnoreRoot";
            String sTargetFilename = calculateTargetFileName(sTargetFilenameExtraTag, ffnFindingNameFormat);
            // create filter using provided params
            var fflsFilter = new AnalysisFilters.filter_FindLostSinks(bDropDuplicatedSmartTraces,
                                                                      bIgnoreRootCallInvocation, ffnFindingNameFormat,
                                                                      bChangeFindingData);
            // execute filter and get object with results (custom saved project)
            AssessmentRun arFilteredAssessmentRun = createFilteredAssessmentRunObjectBasedOnCriteria(fflsFilter,
                                                                                                     fadO2AssessmentDataOunceV6);
            // save it
            saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFilename, fadO2AssessmentDataOunceV6);
            // all done
            restoreChangedData(bChangeFindingData, fadO2AssessmentDataOunceV6);
            DI.log.debug("Custom Assessment File with only Lost Sinks created: {0}", sTargetFilename);
            return sTargetFilename;
        }

        /*
        public static void createAssessmentFileWithLostSinks_NoDuplicates(bool bIgnoreRootCallInvocation)
           {
            bool bDropDuplicateSmartTraces = true;
            AnalysisFilters.filter_FindLostSinks fflsFilter = new AnalysisFilters.filter_FindLostSinks(bDropDuplicateSmartTraces, bIgnoreRootCallInvocation);
            AssessmentRun arFilteredAssessmentRun = createFilteredAssessmentRunObjectBasedOnCriteria(fflsFilter);
            String sTargetFilename = calculateTargetFileName("LOST_SINKS_No_Duplicates");
            saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFilename);
             DI.log.debug("Custom Assessment File (non duplicated Lost Sinks) created: {0}", sTargetFilename);
            // since we actually changed the object's contents during this analysis, reload the file
            loadAssessmentFile(sFileLoaded);
        }*/


        public static String createAssessmentFileWithLostSinks_OneExampleEach(FindingNameFormat ffnFindingNameFormat,
                                                                              bool bChangeFindingData,
                                                                              O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            String sTargetFilename = calculateTargetFileName("LOST_SINKS_UNIQUE", ffnFindingNameFormat);
            return createAssessmentFileWithLostSinks_OneExampleEach(sTargetFilename, ffnFindingNameFormat,
                                                                    bChangeFindingData, fadO2AssessmentDataOunceV6);
        }

        public static String createAssessmentFileWithLostSinks_OneExampleEach(String sTargetFilename,
                                                                              FindingNameFormat ffnFindingNameFormat,
                                                                              bool bChangeFindingData,
                                                                              O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            var ffulsFilter = new AnalysisFilters.filter_FindUniqueLostSinks(ffnFindingNameFormat, bChangeFindingData);
            AssessmentRun arFilteredAssessmentRun = createFilteredAssessmentRunObjectBasedOnCriteria(ffulsFilter,
                                                                                                     fadO2AssessmentDataOunceV6);

            saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFilename, fadO2AssessmentDataOunceV6);
            DI.log.debug("Custom Assessment File (with only one example per Lost Sinks)  created: {0}",
                         sTargetFilename);
            restoreChangedData(bChangeFindingData, fadO2AssessmentDataOunceV6);
            return sTargetFilename;
        }


        private static void restoreChangedData(bool bChangeFindingData, O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            // for now reload the entire file (idealy we should just restore the data changed)
            if (bChangeFindingData)
                // since we changed the object's contents during this analysis, reload the file                
                loadAssessmentFile(sFileLoaded, false, ref fadO2AssessmentDataOunceV6);
        }
    }

    public class FindingViewItem
    {
        public AssessmentAssessmentFileFinding fFinding;
        public AnalysisSearch.FindingsResult frFindingResult;
        public O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6;
        public String sText;

        public FindingViewItem()
        {
        }

        public FindingViewItem(AssessmentAssessmentFileFinding fFinding, O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
        {
            this.fFinding = fFinding;
            this.oadO2AssessmentDataOunceV6 = oadO2AssessmentDataOunceV6;
        }

        public FindingViewItem(AssessmentAssessmentFileFinding fFinding, String sText,
                               AnalysisSearch.FindingsResult frFindingResult, O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
        {
            this.fFinding = fFinding;
            this.sText = sText;
            this.oadO2AssessmentDataOunceV6 = oadO2AssessmentDataOunceV6;
            this.frFindingResult = frFindingResult;
        }

        public override string ToString()
        {
            return sText ?? "";  // to deal with '...Attempted to read or write protected memory..' issue ;
        }
    }
}
