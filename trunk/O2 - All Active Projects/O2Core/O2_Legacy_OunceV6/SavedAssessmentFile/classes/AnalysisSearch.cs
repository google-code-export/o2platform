// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Misc;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class AnalysisSearch
    {
        #region SearchType enum

        public enum SearchType
        {
            Vuln_Type,
            Finding_Text,
            Finding_Context,
            Finding_SourceCode,
            Trace_Text,
            Trace_Context,
            Trace_SourceCode,
            Sink_Text,
            Sink_Context,
            Sink_SourceCode,
            Source_Text,
            Source_Context,
            Source_SourceCode,
            Severity,
            File_Name
        }

        #endregion

        public static List<String> getListWithSearchFilters()
        {
            var lsSearchFilters = new List<String>(new[]
                                                       {
                                                           "Vuln Type",
                                                           "Vuln Name",
                                                           "Context",
                                                           "Source",
                                                           "Sink",
                                                           "Lost Sink",
                                                           "Known Sink",
                                                           "Severity",
                                                           "Confidence",
                                                           "File",
                                                           "Search Text"
                                                       });
            return lsSearchFilters;
        }

        public static List<String> getListWithSearchTypes()
        {
            var lsSearchType = new List<String>();
            lsSearchType.AddRange(Enum.GetNames(typeof (SearchType)));

            /*new String[] {                        
           "Vuln_Type",
           "Finding_Text",
           "Finding_Context",
           "Finding_SourceCode",
           "Trace_Text",
           "Trace_Context",
           "Trace_SourceCode",
           "Sink_Text",
           "Sink_Context",
           "Sink_SourceCode",
           "Source_Text",
           "Source_Context",
           "Source_SourceCode",
           "Severity",
           "File_Name", 
            "Search Text Match"});*/
            return lsSearchType;
        }


        public static bool doesIdExistInSmartTraceCall_Recursive(CallInvocation[] cCallInvocations,
                                                                 UInt32 uSmartTraceCallID, TraceType tTraceType)
        {
            foreach (CallInvocation cCallInvocation in cCallInvocations)
            {
                if (cCallInvocation.sig_id == uSmartTraceCallID && cCallInvocation.trace_type == (int) tTraceType)
                    return true;
                if (null != cCallInvocation.CallInvocation1)
                {
                    bool bResult = doesIdExistInSmartTraceCall_Recursive(cCallInvocation.CallInvocation1,
                                                                         uSmartTraceCallID, tTraceType);
                    if (bResult)
                        return bResult;
                }
            }
            return false;
        }

        // trace_type values
        //    1 = root 
        //    2 = source
        //    3 = sink
        //    4 = ??
        //    5 = lost sink

        public static int findTraceTypeInSmartTrace_Recursive_returnSigId(CallInvocation[] cCallInvocations,
                                                                          TraceType tTraceType)
        {
            if (cCallInvocations != null)
                foreach (CallInvocation cCallInvocation in cCallInvocations)
                {
                    if (cCallInvocation.trace_type == (int) tTraceType)
                        return (int) cCallInvocation.sig_id;
                    if (null != cCallInvocation.CallInvocation1)
                    {
                        int iResult = findTraceTypeInSmartTrace_Recursive_returnSigId(cCallInvocation.CallInvocation1,
                                                                                      tTraceType);
                        if (iResult != -1)
                            return iResult;
                    }
                }
            return -1;
        }

        public static CallInvocation findTraceTypeInSmartTrace_Recursive_returnCallInvocation(
            CallInvocation[] cCallInvocations, TraceType tTraceType)
        {
            if (cCallInvocations != null)
                foreach (CallInvocation cCallInvocation in cCallInvocations)
                {
                    if (cCallInvocation.trace_type == (int) tTraceType)
                        return cCallInvocation;
                    if (null != cCallInvocation.CallInvocation1)
                    {
                        CallInvocation ciResult =
                            findTraceTypeInSmartTrace_Recursive_returnCallInvocation(cCallInvocation.CallInvocation1,
                                                                                     tTraceType);
                        if (ciResult != null)
                            return ciResult;
                    }
                }
            return null;
        }

        public static bool findTraceTypeInSmartTrace_Recursive_returnReverseListOfCallInvocation(
            CallInvocation[] cCallInvocations, TraceType tTraceType,
            List<CallInvocation> lcaReverseListOfCallInvocation)
        {
            if (cCallInvocations != null && lcaReverseListOfCallInvocation != null)
                foreach (CallInvocation cCallInvocation in cCallInvocations)
                {
                    if (cCallInvocation.trace_type == (int) tTraceType) // when we found it start populating the list
                    {
                        lcaReverseListOfCallInvocation.Add(cCallInvocation);
                        return true;
                    }
                    if (null != cCallInvocation.CallInvocation1)
                    {
                        if (
                            findTraceTypeInSmartTrace_Recursive_returnReverseListOfCallInvocation(
                                cCallInvocation.CallInvocation1, tTraceType, lcaReverseListOfCallInvocation))
                        {
                            // means we had a match on a lower node so we need to add the current one
                            lcaReverseListOfCallInvocation.Add(cCallInvocation);
                            return true;
                        }
                    }
                }
            return false;
        }


        public static CallInvocation fromSourceFindFirstTraceWithAChildSink(AssessmentAssessmentFileFinding fFinding,
                                                                            O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
        {
            // first get a list of all Calls to the source
            var lciReverseListOfCalls = new List<CallInvocation>();
            if (findTraceTypeInSmartTrace_Recursive_returnReverseListOfCallInvocation(fFinding.Trace,
                                                                                      TraceType.Source,
                                                                                      lciReverseListOfCalls))
                // now find the first trace that has a sink as a child                
                foreach (CallInvocation ciCallInvocation in lciReverseListOfCalls)
                {
                    CallInvocation ciSink =
                        findTraceTypeInSmartTrace_Recursive_returnCallInvocation(ciCallInvocation.CallInvocation1,
                                                                                 TraceType.Known_Sink);
                    if (ciSink != null)
                        return ciCallInvocation;
                }
            return null;
        }

        /* public static CallInvocation findTraceTypeInSmartTrace_Recursive_returnCallInvocationBelowTraceFound(CallInvocation[] cCallInvocations, Analysis.TraceType tTraceType, CallInvocation ciPreviousCallInvocation)
        {
            if (cCallInvocations != null)
                foreach (CallInvocation cCallInvocation in cCallInvocations)
                {
                    if (cCallInvocation.trace_type == (int)tTraceType)
                    {
                        if (ciPreviousCallInvocation.CallInvocation1.Length == 1)
                        {
                             DI.log.error("in findTraceTypeInSmartTrace_Recursive_returnCallInvocationBelowTraceFound: trace of type {0} did not had a node below", tTraceType.ToString());
                            return ciPreviousCallInvocation.CallInvocation1[0];         // this is actually a bad case
                        }
                        else
                            // now that we found the trace we need to get the one below it
                            return ciPreviousCallInvocation.CallInvocation1[1];
                    }
                    if (null != cCallInvocation.CallInvocation1)
                    {
                        CallInvocation ciResult = findTraceTypeInSmartTrace_Recursive_returnCallInvocationBelowTraceFound(cCallInvocation.CallInvocation1, tTraceType, cCallInvocation);
                        if (ciResult != null)
                            return ciResult;
                    }
                }
            return null;
        }*/


        public static CallInvocation findTraceTypeAndSignatureInSmartTrace_Recursive_returnCallInvocation(
            CallInvocation[] cCallInvocations, TraceType tTraceType, String sSignature,
            O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            foreach (CallInvocation cCallInvocation in cCallInvocations)
            {
                if (cCallInvocation.trace_type == (int) tTraceType &&
                    sSignature == OzasmtUtils_OunceV6.getStringIndexValue(cCallInvocation.sig_id, fadO2AssessmentDataOunceV6))
                    return cCallInvocation;
                if (null != cCallInvocation.CallInvocation1)
                {
                    CallInvocation ciResult =
                        findTraceTypeAndSignatureInSmartTrace_Recursive_returnCallInvocation(
                            cCallInvocation.CallInvocation1, tTraceType, sSignature, fadO2AssessmentDataOunceV6);
                    if (ciResult != null)
                        return ciResult;
                }
            }
            return null;
        }


        public static List<FindingViewItem> calculateDictionaryWithFilteredFindingsResults(
            List<FindingsResult> lfrFindingsResults, ref Dictionary<String, List<FindingViewItem>> dFilteredFindings,
            String sFindingFilter, bool bUniqueList)
        {
            bool bSkipAddingItemsToReturnList = false;
            dFilteredFindings = new Dictionary<string, List<FindingViewItem>>();
            var lfviFindingViewItems = new List<FindingViewItem>();
            int iMaxItemToAdd = 1000;
            int iMaxUniqueFindings = 10000;
            var lsTextOfItemsAdded = new List<string>();
            if (lfrFindingsResults != null)
            {
                //Calculate unique List of findings
                var lfUniqueFindings = new List<AssessmentAssessmentFileFinding>();

                foreach (FindingsResult frFindingsResult in lfrFindingsResults)
                {
                    //if (false == lfUniqueFindings.Contains(frFindingsResult.fFinding))
                    //{
                    //    lfUniqueFindings.Add(frFindingsResult.fFinding);
                    String sText = "";
                    switch (sFindingFilter)
                    {
                        case "Vuln Name":
                            sText = frFindingsResult.sVulnerabilityName;
                            break;
                        case "Vuln Type":
                            sText = frFindingsResult.sVulnerabilityType;
                            break;
                        case "Context":
                            sText = frFindingsResult.sContext;
                            break;
                        case "SourceCode":
                            sText = frFindingsResult.sSourceCodeSnippet;
                            break;
                        case "File":
                            sText = frFindingsResult.sSourceCodeSnippet;
                            break;
                        case "Source":
                            sText = frFindingsResult.sSource;
                            break;
                        case "Sink":
                            sText = frFindingsResult.sSink;
                            break;
                        case "Lost Sink":
                            sText = frFindingsResult.sLostSink;
                            break;
                        case "Known Sink":
                            sText = frFindingsResult.sKnownSink;
                            break;
                        case "Severity":
                            sText = frFindingsResult.sSeverity;
                            break;
                        case "Confidence":
                            sText = frFindingsResult.sConfidence;
                            break;
                        case "Search Text":
                            sText = frFindingsResult.sStringThatMatchedCriteria;
                            break;
                        default:
                            sText = "NOT IMPLEMENTED YET";
                            break;
                    }
                    // create the global dictionary with the items indexed by sText
                    if (sText != null)
                    {
                        var fviFindingViewItem = new FindingViewItem(frFindingsResult.fFinding, sText, frFindingsResult,
                                                                     frFindingsResult.oadO2AssessmentDataOunceV6);
                        if (false == dFilteredFindings.ContainsKey(sText))
                            dFilteredFindings.Add(sText, new List<FindingViewItem>());
                        dFilteredFindings[sText].Add(fviFindingViewItem);

                        if (false == bSkipAddingItemsToReturnList)
                        {
                            // create the main list of fidings that match sFindingFilter
                            if (sText != "" &&
                                (bUniqueList == false || (bUniqueList && false == lsTextOfItemsAdded.Contains(sText))))
                            {
                                lfviFindingViewItems.Add(fviFindingViewItem);

                                if (bUniqueList)
                                    lsTextOfItemsAdded.Add(sText);
                            }
                        }
                    }
                    //}
                    //else
                    //{ 
                    //}
                    if (lfviFindingViewItems.Count > iMaxItemToAdd)
                    {
                        DI.log.error("Max number of items reach ({0}), canceling adding search results to listbox",
                                     iMaxItemToAdd);
                        break;
                    }
                    if (false == bSkipAddingItemsToReturnList && lfUniqueFindings.Count > iMaxUniqueFindings)
                    {
                        DI.log.error(
                            "Max number of unique Findings reached ({0}), canceling adding search results to listbox",
                            iMaxUniqueFindings);
                        bSkipAddingItemsToReturnList = true;
                        // break;
                    }
                }
            }
            return lfviFindingViewItems;
        }

        #region Nested type: FindingsResult

        public class FindingsResult
        {
            public bool bFindingHasTraces;
            public bool bInToString_ShowApplicationAndProjectName = true;
            public CallInvocation ciCallInvocation;
            public AssessmentAssessmentFile fFile;
            public AssessmentAssessmentFileFinding fFinding;
            public O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6;
            public SearchCriteria scSearchCriteria;

            public String sStringThatMatchedCriteria;
            // to quickly show the string that trigered the search result             

            public FindingsResult(O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
            {
                this.oadO2AssessmentDataOunceV6 = oadO2AssessmentDataOunceV6;
            }

            // delay resolving variables
            public String sFindingText
            {
                get { return "{NOT IMPLEMENTED}"; }
            }

            public String sSignature
            {
                get
                {
                    if (ciCallInvocation == null)
                        //Analysis.getStringIndexValue(fFinding.s
                        return "No call invocation data";
                    else
                        return OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadO2AssessmentDataOunceV6);
                }
            }

            public String sContext
            {
                get { return fFinding.context; }
            }

            public String sSourceCodeSnippet
            {
                get
                {
                    if (ciCallInvocation == null)
                        //Analysis.getStringIndexValue(fFinding.s
                        return "No call invocation data";
                    else
                        return OzasmtUtils_OunceV6.getLineFromSourceCode(ciCallInvocation, oadO2AssessmentDataOunceV6);
                }
            }

            public String sFile
            {
                get
                {
                    if (ciCallInvocation == null)
                        //Analysis.getStringIndexValue(fFinding.s
                        return "No call invocation data";
                    else
                        return OzasmtUtils_OunceV6.getFileIndexValue(ciCallInvocation.fn_id, oadO2AssessmentDataOunceV6);
                }
            }

/*            public Analysis.TraceType ttTraceType
            {
                get
                {
                    if (ciCallInvocation == null)
                        //Analysis.getStringIndexValue(fFinding.s
                        return Analysis.TraceType.
                    else
                        return (Analysis.TraceType)Enum.Parse(Analysis.TraceType, ciCallInvocation.trace_type.ToString());
                }
            }*/

            public String sVulnerabilityName
            {
                get
                {
                    return fFinding.vuln_name ?? OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_name_id),
                                                                                         oadO2AssessmentDataOunceV6);
                }
            }

            public String sVulnerabilityType
            {
                get
                {
                    return fFinding.vuln_type ?? OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_type_id),
                                                                                         oadO2AssessmentDataOunceV6);
                }
            }

            /* DC - Removed in order to make the SavedAssessmentFile Project non Ounce dependent
            public String sActionObjectSignature
            {
                get
                {
                    return Lddb.getActionObjectName(fFinding.actionobject_id.ToString());
                }
            }
             */

            public String sConfidence
            {
                get
                {
                    switch (fFinding.confidence)
                    {
                        case 0:
                            return "0 - Vulnerability";
                        case 1:
                            return "1 - Type I";
                        case 2:
                            return "2 - Type II";
                        case 3:
                            return "3 - Info";
                        default:
                            return "UNKNOWN CONFIDENCE ID: " + fFinding.severity;
                    }
                    //return fFinding.confidence.ToString();
                }
            }

            public String sSeverity
            {
                get
                {
                    switch (fFinding.severity)
                    {
                        case 0:
                            return "0 - High";
                        case 1:
                            return "1 - Medium";
                        case 2:
                            return "2 - Low";
                        case 3:
                            return "3 - Info";
                        default:
                            return "UNKNOWN SEVERITY ID: " + fFinding.severity;
                    }
                }
            }

            public String sSource
            {
                get
                {
                    var iSigId =
                        (UInt32)
                        findTraceTypeInSmartTrace_Recursive_returnSigId(fFinding.Trace, TraceType.Source);
                    return OzasmtUtils_OunceV6.getStringIndexValue(iSigId, oadO2AssessmentDataOunceV6);
                }
            }

            public String sSink
            {
                get
                {
                    if (fFinding.Trace == null)
                        return (fFinding.vuln_name != null)
                                   ? fFinding.vuln_name
                                   : OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_name_id),
                                                                             oadO2AssessmentDataOunceV6);
                    String sResult = sLostSink;
                    if (sResult != "")
                        return sResult;
                    else
                        return sKnownSink;
                }
            }

            public String sLostSink
            {
                get
                {
                    var iSigId =
                        (UInt32)
                        findTraceTypeInSmartTrace_Recursive_returnSigId(fFinding.Trace, TraceType.Lost_Sink);
                    return OzasmtUtils_OunceV6.getStringIndexValue(iSigId, oadO2AssessmentDataOunceV6);
                }
            }

            public String sKnownSink
            {
                get
                {
                    var iSigId =
                        (UInt32)
                        findTraceTypeInSmartTrace_Recursive_returnSigId(fFinding.Trace, TraceType.Known_Sink);
                    return OzasmtUtils_OunceV6.getStringIndexValue(iSigId, oadO2AssessmentDataOunceV6);
                }
            }

            public override string ToString()
            {
                if (bInToString_ShowApplicationAndProjectName)
                    return oadO2AssessmentDataOunceV6 + " - " + sStringThatMatchedCriteria;
                else
                    return sStringThatMatchedCriteria;
            }
        }

        #endregion

        #region Nested type: GUI

        public class GUI
        {
            public static void populateFindingsResults_TextMatches(List<FindingsResult> lfrFindingsResults,
                                                                   ListBox lbTargetListBox, bool bUniqueList,
                                                                   bool
                                                                       bInFindindResult_ToString_ShowApplicationAndProjectName)
            {
                int iMaxItemToAdd = 5000;
                // clear DataGridView with results
                lbTargetListBox.Items.Clear();
                var lsTextOfItemsAdded = new List<string>();
                if (lfrFindingsResults != null)
                    foreach (FindingsResult frFindingsResult in lfrFindingsResults)
                    {
                        frFindingsResult.bInToString_ShowApplicationAndProjectName =
                            bInFindindResult_ToString_ShowApplicationAndProjectName;
                        if (bUniqueList == false ||
                            (bUniqueList && false == lsTextOfItemsAdded.Contains(frFindingsResult.ToString())))
                        {
                            lbTargetListBox.Items.Add(frFindingsResult);
                            if (lbTargetListBox.Items.Count > iMaxItemToAdd)
                            {
                                DI.log.error(
                                    "Max number of items reach ({0}), canceling adding search results to listbox",
                                    iMaxItemToAdd);
                                break;
                            }
                            if (bUniqueList)
                                lsTextOfItemsAdded.Add(frFindingsResult.ToString());
                        }
                    }
            }

            public static void populateWithDictionaryOfFilteredFindings_TreeView(TreeView tvTargetTreeView,
                                                                                 Dictionary
                                                                                     <String, List<FindingViewItem>>
                                                                                     dFilteredFindings)
            {
                tvTargetTreeView.Nodes.Clear();
                tvTargetTreeView.CheckBoxes = true;
                //     tvTargetTreeView.Tag = dFilteredFindings;
                foreach (String sText in dFilteredFindings.Keys)
                {
                    TreeNode tnNewTreeNode =
                        O2Forms.newTreeNode(String.Format("{0}  ({1})", sText, dFilteredFindings[sText].Count), sText, 0,
                                            dFilteredFindings[sText]);
                    tnNewTreeNode.Checked = true;
                    tvTargetTreeView.Nodes.Add(tnNewTreeNode);
                }
            }

            public static void populateWithDictionaryOfFilteredFindings_ListBox(ListBox lbTargetListBox,
                                                                                Dictionary
                                                                                    <String, List<FindingViewItem>>
                                                                                    dFilteredFindings)
            {
                lbTargetListBox.Items.Clear();
                //      lbTargetListBox.Tag = dFilteredFindings;
                foreach (String sText in dFilteredFindings.Keys)
                    lbTargetListBox.Items.Add(dFilteredFindings[sText][0]); // add the first one              
            }

            public static void populateWithListOfFilteredFindings_ListBox(ListBox lbTargetListBox,
                                                                          List<FindingViewItem> lviFindingViewItem)
            {
                lbTargetListBox.Items.Clear();
                lbTargetListBox.Tag = lviFindingViewItem;
                foreach (FindingViewItem lFindingViewItem in lviFindingViewItem)
                    lbTargetListBox.Items.Add(lFindingViewItem);
            }

            public static void populateWithListOfFilteredFindings_TreeView(TreeView tvTargetTreeView,
                                                                           List<FindingViewItem> lviFindingViewItem,
                                                                           Dictionary<String, List<FindingViewItem>>
                                                                               dFilteredFindings)
            {
                tvTargetTreeView.Nodes.Clear();
                //lbTargetListBox.Tag = lviFindingViewItem;
                foreach (FindingViewItem lFindingViewItem in lviFindingViewItem)
                {
                    int iImageIndex = 0;
                    if (lFindingViewItem.fFinding.Trace != null)
                        iImageIndex = 1;

                    //TreeNode tnTreeNode = tvTargetTreeView.Nodes.Add(lFindingViewItem.ToString());
                    tvTargetTreeView.Nodes.Add(O2Forms.newTreeNode(lFindingViewItem.ToString(),
                                                                   lFindingViewItem.ToString(), iImageIndex,
                                                                   lFindingViewItem));
                    //else
                    //    tnTreeNode.ImageIndex = -1;
                }
                tvTargetTreeView.Sort();
            }
        }

        #endregion

        #region Nested type: SavedAssessmentSearch

        public class SavedAssessmentSearch
        {
            //public Analysis.o2AssessmentDataOunceV6 fadO2AssessmentData;                         // object with source materials (namely the File and String Indexes)
            public bool bAutoLoadFindingsResultsAndTargetFindings = true;
            public bool bDropDuplicateSmartTraces = true;
            public bool bIgnoreRootCallInvocation = true;
            public bool bSearchInFinding_Context = true;
            public bool bSearchInFinding_Name = true;
            public bool bSearchInFinding_OnlySinks = true;
            public bool bSearchInFinding_OnlySources = true;
            public bool bSearchInFinding_SourceCode;

            public bool bUse_IfAvailable_VarWithFadO2AssessmentData = true;

            public Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile> dtfTargetFindings =
                new Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile>();

            // list of finding to run search

            private Analysis.FindingFilter ffFindingFilter = Analysis.FindingFilter.AllFindings;
            public List<O2AssessmentData_OunceV6> lfadO2AssessmentData;
            public List<FindingsResult> lfrFindingsResults = new List<FindingsResult>();
            public List<SearchCriteria> lscSearchCriteria;

            public SavedAssessmentSearch()
            {
            }

            public SavedAssessmentSearch(String sAssessmentFileToLoad)
            {
                lfadO2AssessmentData = new List<O2AssessmentData_OunceV6>();
                loadDataFromSavedXmlAssessmentFile(sAssessmentFileToLoad);
            }

            public override string ToString()
            {
                String sToStringText = "Search: ";
                foreach (SearchCriteria scSearchCriteria in lscSearchCriteria)
                    sToStringText += String.Format("  {0}:{1}  ", scSearchCriteria.stSearchType,
                                                   scSearchCriteria.sSearchText);
                return sToStringText;
            }

            public void setTargetO2AssessmentDataList(List<O2AssessmentData_OunceV6> lfadO2AssessmentData)
            {
                this.lfadO2AssessmentData = lfadO2AssessmentData;
            }

            public void loadDataFromSavedXmlAssessmentFile(String sAssessmentFileToLoad)
            {
                O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6 = new O2AssessmentData_OunceV6();
                if (bUse_IfAvailable_VarWithFadO2AssessmentData && null != vars.get(sAssessmentFileToLoad))
                    fadO2AssessmentDataOunceV6 = (O2AssessmentData_OunceV6) vars.get(sAssessmentFileToLoad);
                else
                {
                    // load assessment
                    O2Timer tTimer = new O2Timer("Assessment load").start();
                    Analysis.loadAssessmentFile(sAssessmentFileToLoad, true, ref fadO2AssessmentDataOunceV6);
                    tTimer.stop();
                    // Calculate Xrefs into fadO2AssessmentDataOunceV6                         
                    Analysis.populateDictionariesWithXrefsToLoadedAssessment(ffFindingFilter, bDropDuplicateSmartTraces,
                                                                             bIgnoreRootCallInvocation,
                                                                             fadO2AssessmentDataOunceV6);
                    // set variable using the assessment filename
                    vars.set_(sAssessmentFileToLoad, fadO2AssessmentDataOunceV6);
                }
                lfadO2AssessmentData.Add(fadO2AssessmentDataOunceV6);
            }

            public void searchUsingCriteria(string sTextToSearch, String sSearchType)
            {
                var stSearchType = (SearchType) Enum.Parse(typeof (SearchType), sSearchType);
                var scSearchCriteria = new SearchCriteria(sTextToSearch, stSearchType);
                searchUsingCriteria(scSearchCriteria);
            }

            public void searchUsingCriteria(string sTextToSearch)
            {
                searchUsingCriteria(new SearchCriteria(sTextToSearch));
            }

            public void searchUsingCriteria(SearchCriteria scSearchCriteria)
            {
                searchUsingCriteria(new List<SearchCriteria>(new[] {scSearchCriteria}));
            }

            public void searchUsingCriteria(List<SearchCriteria> lscSearchCriteria)
            {
                this.lscSearchCriteria = lscSearchCriteria;
                executeSearch();
            }

            public void executeSearch()
            {
                // list to keep track of all results
                var frConsolidatedFidingsResults = new List<FindingsResult>();
                foreach (O2AssessmentData_OunceV6 fadO2AssessmentData in lfadO2AssessmentData)
                {
                    // load original set of targets
                    loadFindingsDictionaireAsTargetFindings(fadO2AssessmentData.dFindings);

                    executeSearch(fadO2AssessmentData);
                    // save file results
                    foreach (FindingsResult frFindingResult in lfrFindingsResults)
                        frConsolidatedFidingsResults.Add(frFindingResult);
                    // reset findings list
                    lfrFindingsResults = new List<FindingsResult>();
                }
                // put it back on this.lfrFindingsResults
                lfrFindingsResults = frConsolidatedFidingsResults;
            }

            public void executeSearch(O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
            {
                String sTextToSearch = "";
                foreach (SearchCriteria scSearchCriteria in lscSearchCriteria)
                {
                    if (lfrFindingsResults.Count > 0)
                        // if there are findings results make them the current target findings
                        loadFindingsResultsAsTargetFindings(lfrFindingsResults);
                    /*if (scSearchCriteria.stSearchType == SearchType.File_Name) // we need to handle this one diferently
                    {
                        foreach(var file in fadO2AssessmentDataOunceV6.dFindings.Keys)
                            file.
                    }
                    else*/
                    foreach (AssessmentAssessmentFileFinding fFinding in dtfTargetFindings.Keys)
                    {
                        if (scSearchCriteria.bSearchOnFindingsWithNoTraces ||
                            (scSearchCriteria.bSearchOnFindingsWithNoTraces == false && fFinding.Trace != null))
                        {
                            switch (scSearchCriteria.stSearchType)
                            {
                                case SearchType.Vuln_Type:
                                    searchInStringAndAddFindingResult(
                                        fFinding.vuln_type ??
                                        OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_type_id),
                                                                                fadO2AssessmentDataOunceV6), scSearchCriteria,
                                        fFinding, fadO2AssessmentDataOunceV6);
                                    break;
                                case SearchType.Finding_Text:
                                    searchInStringAndAddFindingResult(
                                        fFinding.vuln_name ??
                                        OzasmtUtils_OunceV6.getStringIndexValue(UInt32.Parse(fFinding.vuln_name_id),
                                                                                fadO2AssessmentDataOunceV6), scSearchCriteria,
                                        fFinding, fadO2AssessmentDataOunceV6);
                                    break;
                                case SearchType.Finding_Context:
                                    searchInStringAndAddFindingResult(fFinding.context, scSearchCriteria, fFinding,
                                                                      fadO2AssessmentDataOunceV6);
                                    break;
                                case SearchType.Finding_SourceCode:
                                    sTextToSearch = Files.getLineFromSourceCode(fFinding.line_number,
                                                                                Files.loadSourceFileIntoList(
                                                                                    fadO2AssessmentDataOunceV6.dFindings[
                                                                                        fFinding].filename));
                                    if (sTextToSearch == "")
                                        DI.log.error("in executeSearch: Finding_SourceCode : sTextToSearch == \"\"");
                                    else
                                        searchInStringAndAddFindingResult(sTextToSearch, scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    break;
                                case SearchType.Sink_Text:
                                case SearchType.Sink_Context:
                                case SearchType.Sink_SourceCode:
                                    if (scSearchCriteria.bSearchOnFindingsWithNoTraces && fFinding.Trace == null)
                                    {
                                        switch (scSearchCriteria.stSearchType)
                                        {
                                            case SearchType.Sink_Text:
                                                sTextToSearch = fFinding.vuln_name ??
                                                                OzasmtUtils_OunceV6.getStringIndexValue(
                                                                    UInt32.Parse(fFinding.vuln_name_id),
                                                                    fadO2AssessmentDataOunceV6);
                                                break;
                                            case SearchType.Sink_Context:
                                                sTextToSearch = fFinding.context;
                                                break;
                                            case SearchType.Sink_SourceCode:
                                                AssessmentAssessmentFile fFile =
                                                    fadO2AssessmentDataOunceV6.dFindings[fFinding];
                                                sTextToSearch = Files.getLineFromSourceCode(fFile.filename,
                                                                                            fFinding.line_number);
                                                break;
                                        }
                                        searchInStringAndAddFindingResult(sTextToSearch, scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    }
                                    else
                                    {
                                        CallInvocation ciSink =
                                            findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                                     TraceType.
                                                                                                         Known_Sink);
                                        // try geting the known sink value
                                        //                                    sTextToSearch = Analysis.getSmartTraceNameOfTraceType(fFinding.Trace, Analysis.TraceType.Known_Sink, this.fadO2AssessmentDataOunceV6);
                                        if (ciSink == null) // if that didn't work get the lost sink value
                                            ciSink =
                                                findTraceTypeInSmartTrace_Recursive_returnCallInvocation(
                                                    fFinding.Trace, TraceType.Lost_Sink);
                                        if (ciSink == null)
                                            DI.log.error(
                                                "in executeSearch: something is wrong with trace since no known sinks or lost sinks were found");
                                        else
                                        {
                                            switch (scSearchCriteria.stSearchType)
                                            {
                                                case SearchType.Sink_Text:
                                                    sTextToSearch = OzasmtUtils_OunceV6.getStringIndexValue(ciSink.sig_id,
                                                                                                            fadO2AssessmentDataOunceV6);
                                                    break;
                                                case SearchType.Sink_Context:
                                                    sTextToSearch = OzasmtUtils_OunceV6.getStringIndexValue(ciSink.cxt_id,
                                                                                                            fadO2AssessmentDataOunceV6);
                                                    break;
                                                case SearchType.Sink_SourceCode:
                                                    sTextToSearch = OzasmtUtils_OunceV6.getLineFromSourceCode(ciSink,
                                                                                                              fadO2AssessmentDataOunceV6);
                                                    break;
                                            }

                                            searchInStringAndAddFindingResult(sTextToSearch, scSearchCriteria,
                                                                              fFinding,
                                                                              fadO2AssessmentDataOunceV6);
                                        }
                                    }
                                    break;
                                case SearchType.Source_Text:
                                case SearchType.Source_Context:
                                case SearchType.Source_SourceCode:
                                    if (fFinding.Trace == null)
                                    {
                                        switch (scSearchCriteria.stSearchType)
                                        {
                                            case SearchType.Source_Text:
                                                sTextToSearch = fFinding.vuln_name ??
                                                                OzasmtUtils_OunceV6.getStringIndexValue(
                                                                    UInt32.Parse(fFinding.vuln_name_id),
                                                                    fadO2AssessmentDataOunceV6);
                                                break;
                                            case SearchType.Source_Context:
                                                sTextToSearch = fFinding.context;
                                                break;
                                            case SearchType.Source_SourceCode:
                                                sTextToSearch = "";
                                                break;
                                        }
                                        searchInStringAndAddFindingResult(sTextToSearch, scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    }
                                    else
                                    {
                                        CallInvocation ciSource =
                                            findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                                     TraceType.
                                                                                                         Source);

                                        //                                    sTextToSearch = Analysis.getSmartTraceNameOfTraceType(fFinding.Trace, Analysis.TraceType.Source, this.fadO2AssessmentDataOunceV6);
                                        if (ciSource == null)
                                            DI.log.error(
                                                "in executeSearch: something is wrong with trace since no source was found");
                                        else
                                        {
                                            switch (scSearchCriteria.stSearchType)
                                            {
                                                case SearchType.Source_Text:
                                                    sTextToSearch = OzasmtUtils_OunceV6.getStringIndexValue(ciSource.sig_id,
                                                                                                            fadO2AssessmentDataOunceV6);
                                                    break;
                                                case SearchType.Source_Context:
                                                    sTextToSearch = OzasmtUtils_OunceV6.getStringIndexValue(ciSource.cxt_id,
                                                                                                            fadO2AssessmentDataOunceV6);
                                                    break;
                                                case SearchType.Source_SourceCode:
                                                    sTextToSearch = OzasmtUtils_OunceV6.getLineFromSourceCode(ciSource,
                                                                                                              fadO2AssessmentDataOunceV6);
                                                    break;
                                            }
                                            searchInStringAndAddFindingResult(sTextToSearch, scSearchCriteria,
                                                                              fFinding,
                                                                              fadO2AssessmentDataOunceV6);
                                        }
                                    }
                                    break;
                                case SearchType.Trace_Text:
                                case SearchType.Trace_Context:
                                case SearchType.Trace_SourceCode:
                                    var lciMatches = new List<CallInvocation>();
                                    bool bMatch = findTextInSmartTrace_Recursive(fFinding.Trace, scSearchCriteria,
                                                                                 lciMatches, fFinding,
                                                                                 fadO2AssessmentDataOunceV6);
                                    if (scSearchCriteria.bNegativeSearch && bMatch == false)
                                    {
                                        // use the vulnName when we have a negative match
                                        String sVulnName = fFinding.vuln_name ?? OzasmtUtils_OunceV6.getStringIndexValue(
                                                                                     UInt32.Parse(
                                                                                         fFinding.vuln_name_id),
                                                                                     fadO2AssessmentDataOunceV6);
                                        addFindingToListOfFindingsResults(sVulnName, scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    }
                                    break;
                                case SearchType.Severity:
                                    if (RegEx.findStringInString("High", scSearchCriteria.sSearchText) &&
                                        fFinding.severity == 0)
                                        addFindingToListOfFindingsResults("High", scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    if (RegEx.findStringInString("Medium", scSearchCriteria.sSearchText) &&
                                        fFinding.severity == 1)
                                        addFindingToListOfFindingsResults("Medium", scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    if (RegEx.findStringInString("Low", scSearchCriteria.sSearchText) &&
                                        fFinding.severity == 2)
                                        addFindingToListOfFindingsResults("Low", scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    if (RegEx.findStringInString("Info", scSearchCriteria.sSearchText) &&
                                        fFinding.severity == 3)
                                        addFindingToListOfFindingsResults("Info", scSearchCriteria, fFinding,
                                                                          fadO2AssessmentDataOunceV6);
                                    break;

                                case SearchType.File_Name:
                                    var file = fadO2AssessmentDataOunceV6.dFindings[fFinding];
                                    if (file != null && file.filename !=null)
                                        if (RegEx.findStringInString(file.filename, scSearchCriteria.sSearchText))
                                            addFindingToListOfFindingsResults(file.filename, scSearchCriteria, fFinding,fadO2AssessmentDataOunceV6);
                                    break;
                            }
                        }

                    }
                    scSearchCriteria.storeFindingsResult(lfrFindingsResults);
                    // if there are no findings don't continue search
                    if (lfrFindingsResults.Count == 0)
                        return;
                }
            }

            private bool searchInStringAndAddFindingResult(String sTextToSearch, SearchCriteria scCurrentSearchCriteria,
                                                           AssessmentAssessmentFileFinding fFinding,
                                                           O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
            {
                bool bRegExMatch = RegEx.execRegExOnText_hasMatches(scCurrentSearchCriteria.reRegex, sTextToSearch);
                if ((bRegExMatch && scCurrentSearchCriteria.bNegativeSearch == false) ||
                    (bRegExMatch == false && scCurrentSearchCriteria.bNegativeSearch))
                {
                    addFindingToListOfFindingsResults(sTextToSearch, scCurrentSearchCriteria, fFinding,
                                                      fadO2AssessmentDataOunceV6);
                }
                return bRegExMatch;
            }

            private void addFindingToListOfFindingsResults(String sTextToSearch, SearchCriteria scCurrentSearchCriteria,
                                                           AssessmentAssessmentFileFinding fFinding,
                                                           O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
            {
                var frFindingResult = new FindingsResult(fadO2AssessmentDataOunceV6);
                frFindingResult.sStringThatMatchedCriteria = sTextToSearch;
                frFindingResult.fFinding = fFinding;
                frFindingResult.fFile = dtfTargetFindings[fFinding];
                frFindingResult.scSearchCriteria = scCurrentSearchCriteria;
                // so that we can trace back to the criteria that created this Finding Result
                lfrFindingsResults.Add(frFindingResult);
            }

            public void loadFindingsDictionaireAsTargetFindings(
                Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile> dtDFindingsToLoad)
            {
                dtfTargetFindings.Clear();
                lfrFindingsResults.Clear();
                if (dtDFindingsToLoad != null)
                    foreach (AssessmentAssessmentFileFinding fFinding in dtDFindingsToLoad.Keys)
                        dtfTargetFindings.Add(fFinding, dtDFindingsToLoad[fFinding]);
            }

            public void loadFindingsResultsAsTargetFindings(List<FindingsResult> lfrFindingsResultsToLoad)
            {
                dtfTargetFindings.Clear();
                foreach (FindingsResult frFindingResult in lfrFindingsResultsToLoad)
                    if (false == dtfTargetFindings.ContainsKey(frFindingResult.fFinding))
                        dtfTargetFindings.Add(frFindingResult.fFinding, frFindingResult.fFile);
                lfrFindingsResults.Clear();
            }


            public void createSavedAssessmentFileWithCurrentFindingsResults()
            {
                //Analysis.saveFilteredAssessmentRun(
            }

            public bool findTextInSmartTrace_Recursive(CallInvocation[] cCallInvocations,
                                                       SearchCriteria scSearchCriteria, List<CallInvocation> lciMatches,
                                                       AssessmentAssessmentFileFinding fFinding,
                                                       O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
            {
                if (cCallInvocations == null)
                    return false;
                foreach (CallInvocation ciCallInvocation in cCallInvocations)
                {
                    // execute searches
                    String sTextToSearch = "";
                    switch (scSearchCriteria.stSearchType)
                    {
                        case SearchType.Trace_Text:
                            sTextToSearch = OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, fadO2AssessmentDataOunceV6);
                            break;
                        case SearchType.Trace_Context:
                            sTextToSearch = OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.cxt_id, fadO2AssessmentDataOunceV6);
                            break;
                        case SearchType.Trace_SourceCode:
                            if (ciCallInvocation.line_number > 0)
                                sTextToSearch = OzasmtUtils_OunceV6.getLineFromSourceCode(ciCallInvocation, fadO2AssessmentDataOunceV6);
                            break;
                    }

                    if (scSearchCriteria.bNegativeSearch)
                    {
                        if (RegEx.execRegExOnText_hasMatches(scSearchCriteria.reRegex, sTextToSearch))
                            // if we have a match remove this trace
                            return true;
                    }
                    else if (sTextToSearch != "")
                    {
                        searchInStringAndAddFindingResult(sTextToSearch, scSearchCriteria, fFinding, fadO2AssessmentDataOunceV6);
                        //  // stop searching when we have a match                            
                    }
                    // transverse the other call
                    if (null != ciCallInvocation.CallInvocation1)
                        if (findTextInSmartTrace_Recursive(ciCallInvocation.CallInvocation1, scSearchCriteria,
                                                           lciMatches, fFinding, fadO2AssessmentDataOunceV6))
                            return true;
                }
                return false;
            }
        }

        #endregion

        #region Nested type: SearchCriteria

        public class SearchCriteria
        {
            public bool bNegativeSearch; // means we want to find the items that don't match the reg ex
            public bool bSearchOnFindingsWithNoTraces; // default to false

            public List<FindingsResult> lfrFindingsResults;
            // store individual results since that will allow a the view of the individual search results (and not just the final one)            

            public Regex reRegex;
            public String sSearchText;
            public SearchType stSearchType;

            public SearchCriteria(String sText)
            {
                setSearchCriteria(sText, SearchType.Finding_Text, bSearchOnFindingsWithNoTraces, bNegativeSearch);
            }

            public SearchCriteria(String sText, SearchType stSearchType)
            {
                setSearchCriteria(sText, stSearchType, bSearchOnFindingsWithNoTraces, bNegativeSearch);
            }

            public SearchCriteria(String sText, SearchType stSearchType, bool bSearchOnFindingsWithNoTraces,
                                  bool bNegativeSearch)
            {
                setSearchCriteria(sText, stSearchType, bSearchOnFindingsWithNoTraces, bNegativeSearch);
            }

            public override string ToString()
            {
                return String.Format("{0} : {1}", stSearchType, sSearchText);
            }


            public void setSearchCriteria(String sText, SearchType stSearchType, bool bSearchOnFindingsWithNoTraces,
                                          bool bNegativeSearch)
            {
                sSearchText = sText;
                reRegex = RegEx.createRegEx(sText);
                this.stSearchType = stSearchType;
                this.bSearchOnFindingsWithNoTraces = bSearchOnFindingsWithNoTraces;
                this.bNegativeSearch = bNegativeSearch;
            }

            public void storeFindingsResult(List<FindingsResult> lfrFindingsResultToStore)
            {
                lfrFindingsResults = new List<FindingsResult>();
                foreach (FindingsResult frFindingsResult in lfrFindingsResultToStore)
                    lfrFindingsResults.Add(frFindingsResult);
            }
        }

        #endregion
    }
}
