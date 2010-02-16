// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class AnalysisFilters
    {
        /*public static String filterName(String sStringToFilter, bool bShowParameters, bool bShowReturnClass, bool bShowNamespace, int iNamespaceDepth)
        {
            if (dFilteredFuntionSignatures.ContainsKey(sStringToFilter))
                return dFilteredFuntionSignatures[sStringToFilter].getFilteredSignature(bShowParameters,bShowReturnClass,bShowNamespace,iNamespaceDepth);                

            FilteredSignature fsFilteredSignature = new FilteredSignature(sStringToFilter);
            dFilteredFuntionSignatures.Add(sStringToFilter, fsFilteredSignature);
            return fsFilteredSignature.getFilteredSignature(bShowParameters,bShowReturnClass,bShowNamespace,iNamespaceDepth);
            /*
            if (true == bShowParameters && true == bShowReturnClass)
                return sStringToFilter;

            if (true == bShowParameters && false == bShowReturnClass)
            {
                int iIndexOfFirstColon = sStringToFilter.IndexOf(':');
                if (iIndexOfFirstColon > -1)
                    return sStringToFilter.Substring(0, iIndexOfFirstColon);
            }

            if (false == bShowParameters && true == bShowReturnClass)
            {
                int iIndexOfFirstLeftParenthesis = sStringToFilter.IndexOf('(');
                if (iIndexOfFirstLeftParenthesis > -1)
                {
                    String sNoParameters = sStringToFilter.Substring(0, iIndexOfFirstLeftParenthesis);
                    int iIndexOfFirstColon = sStringToFilter.IndexOf(':');
                    if (iIndexOfFirstColon > -1)
                        return sNoParameters + sStringToFilter.Substring(iIndexOfFirstColon);
                    else
                        return sNoParameters;
                }
            }

            if (false == bShowParameters && false == bShowReturnClass)
            {
                int iIndexOfFirstLeftParenthesis = sStringToFilter.IndexOf('(');
                if (iIndexOfFirstLeftParenthesis > -1)
                {
                    String sNoParamsAndNoReturnClass = sStringToFilter.Substring(0, iIndexOfFirstLeftParenthesis);

                    int iLastIndexOfDot = sNoParamsAndNoReturnClass.LastIndexOf('.');
                    if (bShowNamespace == true)
                    {                            
                        return sNoParamsAndNoReturnClass;
                    }
                    else
                    {
                        if (iLastIndexOfDot == -1)
                            return sNoParamsAndNoReturnClass;
                        else
                            return sNoParamsAndNoReturnClass.Substring(iLastIndexOfDot);
                    }

                }
            }
            */
        //return sStringToFilter;
        //}

        #region Nested type: filter

        public class filter
        {
            public virtual bool applyFilterAndPopulateList(AssessmentRun arAssessmentRun,
                                                           AssessmentAssessmentFileFinding fFinding,
                                                           List<AssessmentAssessmentFileFinding>
                                                               lfFindingsThatMatchCriteria,
                                                           List<AssessmentAssessmentFile> lafFilteredAssessmentFiles)
            {
                return false;
            }

            public bool filterDuplicateFindings(List<AssessmentAssessmentFile> lafFilteredAssessmentFiles,
                                                List<AssessmentAssessmentFileFinding> lfFindingsThatMatchCriteria,
                                                AssessmentAssessmentFileFinding fNewFinding,
                                                bool bIgnoreRootCallInvocation)
            {
                // search the current temp list of Findings (for the current file
                foreach (AssessmentAssessmentFileFinding fFinding in lfFindingsThatMatchCriteria)
                    if (fFinding.Trace != null && fFinding.Trace != null)
                        if (areCallInvoctionObjectsEqual(fFinding.Trace[0], fNewFinding.Trace[0],
                                                         bIgnoreRootCallInvocation))
                            //  bIgnoreRootCallInvocation this will remove all SmartTraces where only the root item (at the top) is different
                            return false; // we found an equal so return                

                // and if there are other AssessmentFiles already process it, also analyze them                                                                
                if (lafFilteredAssessmentFiles != null && lafFilteredAssessmentFiles.Count > 0)
                {
                    foreach (AssessmentAssessmentFile afAssessmentFile in lafFilteredAssessmentFiles)
                        foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                            if (fFinding.Trace != null && fFinding.Trace != null)
                                if (areCallInvoctionObjectsEqual(fFinding.Trace[0], fNewFinding.Trace[0],
                                                                 bIgnoreRootCallInvocation))
                                    //  bIgnoreRootCallInvocation this will remove all SmartTraces where only the root item (at the top) is different
                                    return false;
                    // we found an equal so return                                                                                            
                }
                // if we make it this far, means that the current smart trace is unique
                lfFindingsThatMatchCriteria.Add(fNewFinding); // only add the different ones*/        
                return (true);
            }

            // recursive function that compares two SmartTraces
            public bool areCallInvoctionObjectsEqual(CallInvocation ciExistingCallInvocation,
                                                     CallInvocation ciNewCallInvocation, bool bIgnoreRootCallInvocation)
            {
                // first check if the functions called are different (note that we ignore the value of .fn_id which the one that indicates which file it is used)
                if (false == bIgnoreRootCallInvocation)
//                    if (ciExistingCallInvocation.sig_id != ciNewCallInvocation.sig_id)        // originally i used the signature but that was losing a number of different traces    
                    if (ciExistingCallInvocation.cxt_id != ciNewCallInvocation.cxt_id)
                        // going to use the context id since that is a much better representation of the trace's contents
                        return false;
                bIgnoreRootCallInvocation = false; // after the first time always do the check above

                // then check the childs of both trees
                if (ciExistingCallInvocation.CallInvocation1 == null && ciNewCallInvocation.CallInvocation1 == null)
                    // if both are null they are equal
                    return true;
                if (ciExistingCallInvocation.CallInvocation1 == null || ciNewCallInvocation.CallInvocation1 == null)
                    // if only one of them is null, then they are different
                    return false;
                if (ciExistingCallInvocation.CallInvocation1.Length != ciNewCallInvocation.CallInvocation1.Length)
                    // if they have different number of child notes they are different
                    return false;
                for (int i = 0; i < ciExistingCallInvocation.CallInvocation1.Length; i++)
                    if (i < ciNewCallInvocation.CallInvocation1.Length)
                        // need to double check if this is still needed since we now have the lenght check above
                    {
                        bool bResult = areCallInvoctionObjectsEqual(ciExistingCallInvocation.CallInvocation1[i],
                                                                    ciNewCallInvocation.CallInvocation1[i],
                                                                    bIgnoreRootCallInvocation);
                        if (false == bResult)
                            return false;
                    }
                // if we make it this far means they are equal                
                return true;
            }

            public void applyFindingNameFormat(AssessmentRun arAssessmentRun, AssessmentAssessmentFileFinding fFinding,
                                               Analysis.FindingNameFormat ffnFindingNameFormat)
            {
                switch (ffnFindingNameFormat)
                {
                    case Analysis.FindingNameFormat.FindingType: // do nothing in these cases
                        break;
                    case Analysis.FindingNameFormat.FindingType_Sink:

                        fFinding.vuln_type += "        " +
                                              resolveSink(arAssessmentRun, fFinding.Trace[0].CallInvocation1);
                        break;
                    case Analysis.FindingNameFormat.FindingType_Source:
                        fFinding.vuln_type += "        " +
                                              resolveSource(arAssessmentRun, fFinding.Trace[0].CallInvocation1);
                        break;
                    case Analysis.FindingNameFormat.Sink:
                        fFinding.vuln_type = "        " +
                                             resolveSink(arAssessmentRun, fFinding.Trace[0].CallInvocation1);
                        break;
                    case Analysis.FindingNameFormat.Source:
                        fFinding.vuln_type = "        " +
                                             resolveSource(arAssessmentRun, fFinding.Trace[0].CallInvocation1);
                        break;
                    case Analysis.FindingNameFormat.Sink_Source:
                        fFinding.vuln_type = resolveSink(arAssessmentRun, fFinding.Trace[0].CallInvocation1) +
                                             "        " +
                                             resolveSource(arAssessmentRun, fFinding.Trace[0].CallInvocation1);
                        break;
                    case Analysis.FindingNameFormat.Source_Sink:
                        fFinding.vuln_type = resolveSource(arAssessmentRun, fFinding.Trace[0].CallInvocation1) +
                                             "        " +
                                             resolveSink(arAssessmentRun, fFinding.Trace[0].CallInvocation1);
                        break;
                }
            }

            private String resolveSource(AssessmentRun arAssessmentRun, CallInvocation[] cCallInvocation)
            {
                String sSource = "Source: " +
                                 getSmartTraceCallName(arAssessmentRun, cCallInvocation, TraceType.Source);
                return sSource;
            }

            private String resolveSink(AssessmentRun arAssessmentRun, CallInvocation[] cCallInvocation)
            {
                String sSink = getSmartTraceCallName(arAssessmentRun, cCallInvocation, TraceType.Known_Sink);
                if (sSink != "") // LostSink case
                    sSink = "Sink: " + sSink;
                else
                    sSink = "LostSink: " +
                            getSmartTraceCallName(arAssessmentRun, cCallInvocation, TraceType.Lost_Sink);
                return sSink;
            }

            private String getSmartTraceCallName(AssessmentRun arAssessmentRun, CallInvocation[] cCallInvocation,
                                                 TraceType tTraceType)
            {
                int iSmartTraceIndex = AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnSigId(cCallInvocation,
                                                                                                      tTraceType);
                if (iSmartTraceIndex > 0)
                    return arAssessmentRun.StringIndeces[iSmartTraceIndex - 1].value;
                else
                    return "";
            }
        }

        #endregion

        #region Nested type: filter_FindActionObject

        public class filter_FindActionObject : filter
        {
            private readonly bool bChangeFindingData;
            private readonly bool bFilterDuplicateFindings;
            private readonly bool bIgnoreRootCallInvocation;
            private readonly Analysis.FindingNameFormat ffnFindingNameFormat;
            public bool bDropFindingsWithNoTraces;
            public String sActionObjectIdToFind;

            public filter_FindActionObject(String sActionObjectIdToFind, bool bDropFindingsWithNoTraces,
                                           bool bFilterDuplicateFindings, bool bIgnoreRootCallInvocation,
                                           Analysis.FindingNameFormat ffnFindingNameFormat, bool bChangeFindingData)
            {
                this.sActionObjectIdToFind = sActionObjectIdToFind;
                this.bDropFindingsWithNoTraces = bDropFindingsWithNoTraces;
                this.bFilterDuplicateFindings = bFilterDuplicateFindings;
                this.bIgnoreRootCallInvocation = bIgnoreRootCallInvocation;
                this.ffnFindingNameFormat = ffnFindingNameFormat;
                this.bChangeFindingData = bChangeFindingData;
            }

            public override bool applyFilterAndPopulateList(AssessmentRun arAssessmentRun,
                                                            AssessmentAssessmentFileFinding fFinding,
                                                            List<AssessmentAssessmentFileFinding>
                                                                lfFindingsThatMatchCriteria,
                                                            List<AssessmentAssessmentFile> lafFilteredAssessmentFiles)
            {
                if (sActionObjectIdToFind == fFinding.actionobject_id.ToString())
                    // and the actionObject matches the filter
                {
                    if (false == bDropFindingsWithNoTraces)
                    {
                        lfFindingsThatMatchCriteria.Add(fFinding);
                        // always add to the list when bDropFindingsWithNoTraces is false
                        return true;
                    }
                    else if (null != fFinding.Trace)
                        // when bDropFindingsWithNoTraces only add the ones with traces                                                         
                    {
                        if (bChangeFindingData) // if required changed the name of this finding
                            applyFindingNameFormat(arAssessmentRun, fFinding, ffnFindingNameFormat);

                        if (bFilterDuplicateFindings)
                            // and if  bFilterDuplicateFindings is true, consolidate the Trace into similar ones
                            return filterDuplicateFindings(lafFilteredAssessmentFiles, lfFindingsThatMatchCriteria,
                                                           fFinding, bIgnoreRootCallInvocation);
                        else
                        {
                            lfFindingsThatMatchCriteria.Add(fFinding);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion

        #region Nested type: filter_FindLostSinks

        public class filter_FindLostSinks : filter
        {
            private readonly bool bChangeFindingData;
            private readonly bool bDropDuplicateSmartTraces;
            private readonly bool bIgnoreRootCallInvocation;
            private readonly Analysis.FindingNameFormat ffnFindingNameFormat;

            public filter_FindLostSinks(bool bDropDuplicateSmartTraces, bool bIgnoreRootCallInvocation,
                                        Analysis.FindingNameFormat ffnFindingNameFormat, bool bChangeFindingData)
            {
                this.bDropDuplicateSmartTraces = bDropDuplicateSmartTraces;
                this.bIgnoreRootCallInvocation = bIgnoreRootCallInvocation;
                this.ffnFindingNameFormat = ffnFindingNameFormat;
                this.bChangeFindingData = bChangeFindingData;
            }

            public override bool applyFilterAndPopulateList(AssessmentRun arAssessmentRun,
                                                            AssessmentAssessmentFileFinding fFinding,
                                                            List<AssessmentAssessmentFileFinding>
                                                                lfFindingsThatMatchCriteria,
                                                            List<AssessmentAssessmentFile> lafFilteredAssessmentFiles)
            {
                if (fFinding.Trace != null)
                {
                    int iLostSinkId = AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnSigId(fFinding.Trace,
                                                                                                     TraceType.
                                                                                                         Lost_Sink);
                    if (iLostSinkId > 0) // need to figure out what happens when iLostSinkId =0
                    {
                        if (bChangeFindingData) // if required changed the name of this finding
                            applyFindingNameFormat(arAssessmentRun, fFinding, ffnFindingNameFormat);
                        if (bDropDuplicateSmartTraces)
                            return filterDuplicateFindings(lafFilteredAssessmentFiles, lfFindingsThatMatchCriteria,
                                                           fFinding, bIgnoreRootCallInvocation);
                        else
                        {
                            lfFindingsThatMatchCriteria.Add(fFinding);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion

        #region Nested type: filter_FindSmartTrace_byID

        public class filter_FindSmartTrace_byID : filter
        {
            private readonly bool bChangeFindingData;
            private readonly bool bDropDuplicateSmartTraces;
            private readonly bool bIgnoreRootCallInvocation;
            private readonly Analysis.FindingNameFormat ffnFindingNameFormat;
            private readonly TraceType tTraceType;
            private readonly UInt32 uSmartTraceCallID;

            public filter_FindSmartTrace_byID(UInt32 uSmartTraceCallID, TraceType tTraceType,
                                              bool bDropDuplicateSmartTraces, bool bIgnoreRootCallInvocation,
                                              Analysis.FindingNameFormat ffnFindingNameFormat, bool bChangeFindingData)
            {
                this.uSmartTraceCallID = uSmartTraceCallID;
                this.bDropDuplicateSmartTraces = bDropDuplicateSmartTraces;
                this.bIgnoreRootCallInvocation = bIgnoreRootCallInvocation;
                this.ffnFindingNameFormat = ffnFindingNameFormat;
                this.bChangeFindingData = bChangeFindingData;
                this.tTraceType = tTraceType;
            }

            public override bool applyFilterAndPopulateList(AssessmentRun arAssessmentRun,
                                                            AssessmentAssessmentFileFinding fFinding,
                                                            List<AssessmentAssessmentFileFinding>
                                                                lfFindingsThatMatchCriteria,
                                                            List<AssessmentAssessmentFile> lafFilteredAssessmentFiles)
            {
                if (fFinding.Trace != null)
                {
                    if (AnalysisSearch.doesIdExistInSmartTraceCall_Recursive(fFinding.Trace[0].CallInvocation1,
                                                                             uSmartTraceCallID, tTraceType))
                    {
                        if (bChangeFindingData) // if required changed the name of this finding
                            applyFindingNameFormat(arAssessmentRun, fFinding, ffnFindingNameFormat);

                        if (bDropDuplicateSmartTraces)
                            return filterDuplicateFindings(lafFilteredAssessmentFiles, lfFindingsThatMatchCriteria,
                                                           fFinding, bIgnoreRootCallInvocation);
                        else
                        {
                            lfFindingsThatMatchCriteria.Add(fFinding);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion

        #region Nested type: filter_FindSmartTraces

        public class filter_FindSmartTraces : filter
        {
            private readonly bool bChangeFindingData;
            private readonly bool bDropDuplicateSmartTraces;
            private readonly bool bIgnoreRootCallInvocation;
            private readonly Analysis.FindingNameFormat ffnFindingNameFormat;

            public filter_FindSmartTraces(bool bDropDuplicateSmartTraces, bool bIgnoreRootCallInvocation,
                                          Analysis.FindingNameFormat ffnFindingNameFormat, bool bChangeFindingData)
            {
                this.bDropDuplicateSmartTraces = bDropDuplicateSmartTraces;
                this.bIgnoreRootCallInvocation = bIgnoreRootCallInvocation;
                this.ffnFindingNameFormat = ffnFindingNameFormat;
                this.bChangeFindingData = bChangeFindingData;
            }

            public override bool applyFilterAndPopulateList(AssessmentRun arAssessmentRun,
                                                            AssessmentAssessmentFileFinding fFinding,
                                                            List<AssessmentAssessmentFileFinding>
                                                                lfFindingsThatMatchCriteria,
                                                            List<AssessmentAssessmentFile> lafFilteredAssessmentFiles)
            {
                if (fFinding.Trace != null)
                {
                    if (bChangeFindingData) // if required changed the name of this finding
                        applyFindingNameFormat(arAssessmentRun, fFinding, ffnFindingNameFormat);

                    if (bDropDuplicateSmartTraces)
                        return filterDuplicateFindings(lafFilteredAssessmentFiles, lfFindingsThatMatchCriteria, fFinding,
                                                       bIgnoreRootCallInvocation);
                    else
                    {
                        lfFindingsThatMatchCriteria.Add(fFinding);
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region Nested type: filter_FindUniqueLostSinks

        public class filter_FindUniqueLostSinks : filter
        {
            private readonly bool bChangeFindingData;
            private readonly Analysis.FindingNameFormat ffnFindingNameFormat;
            private readonly List<int> iLostSinksProcessed = new List<int>();

            public filter_FindUniqueLostSinks(Analysis.FindingNameFormat ffnFindingNameFormat, bool bChangeFindingData)
            {
                this.ffnFindingNameFormat = ffnFindingNameFormat;
                this.bChangeFindingData = bChangeFindingData;
            }

            public override bool applyFilterAndPopulateList(AssessmentRun arAssessmentRun,
                                                            AssessmentAssessmentFileFinding fFinding,
                                                            List<AssessmentAssessmentFileFinding>
                                                                lfFindingsThatMatchCriteria,
                                                            List<AssessmentAssessmentFile> lafFilteredAssessmentFiles)
            {
                if (fFinding.Trace != null)
                {
                    int iLostSinkId = AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnSigId(fFinding.Trace,
                                                                                                     TraceType.
                                                                                                         Lost_Sink);
                    if (iLostSinkId > 0) // need to figure out what happens when iLostSinkId =0
                    {
                        if (false == iLostSinksProcessed.Contains(iLostSinkId))
                        {
                            if (bChangeFindingData) // if required changed the name of this finding
                                applyFindingNameFormat(arAssessmentRun, fFinding, ffnFindingNameFormat);
                            lfFindingsThatMatchCriteria.Add(fFinding);
                            iLostSinksProcessed.Add(iLostSinkId);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion
    }
}
