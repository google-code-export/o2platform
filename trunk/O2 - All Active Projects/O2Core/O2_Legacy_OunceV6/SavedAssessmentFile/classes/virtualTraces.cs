using System;
using System.Collections.Generic;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class VirtualTraces
    {
        private static String sExternalSourceString = "<external_source>";

        //public static createNewO2AssessmentDataAndFindingFrom

        public static FindingViewItem createNewFindingViewItemFromFindingViewItem(
            FindingViewItem fviFindingViewItemToDuplicate)
        {
            var nfviNewFindingViewItem = new NewFindingViewItem();

            AssessmentAssessmentFileFinding fNewFinding =
                nfviNewFindingViewItem.AddNewFindingFromExistingOne(fviFindingViewItemToDuplicate.fFinding,
                                                                    fviFindingViewItemToDuplicate.oadO2AssessmentDataOunceV6);

            nfviNewFindingViewItem.updateOadStringLists();

            return nfviNewFindingViewItem.getFindingViewItemForLastFindingAdded();
        }

        public static FindingViewItem connectTwoFindingNewItems(FindingViewItem fviJoinAtSink,
                                                                FindingViewItem fviJoinAtSource)
        {
            var nfviNewFindingViewItem = new NewFindingViewItem();

            AssessmentAssessmentFileFinding fNewFinding =
                nfviNewFindingViewItem.AddNewFindingFromExistingOne(fviJoinAtSink.fFinding,
                                                                    fviJoinAtSink.oadO2AssessmentDataOunceV6);

            if (false == nfviNewFindingViewItem.appendTrace_FindingSourceToFindingSink(fNewFinding, fviJoinAtSource))
            {
                /*  DI.log.info("___ appendTrace_FindingSourceToFindingSink error, happened for Source: {0}", o2.analysis.Analysis.getSource(fNewFinding, fviJoinAtSink.oadO2AssessmentDataOunceV6));
                CallInvocation ciSink = AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fNewFinding.Trace, Analysis.TraceType.Known_Sink);
                if (ciSink != null)
                     DI.log.info("appendTrace_FindingSourceToFindingSink error, happened for Sink: {0}", Analysis.getStringIndexValue(ciSink.sig_id,fviJoinAtSink.oadO2AssessmentDataOunceV6));
                return null;*/
            }

//            oadNewO2AssessmentDataOunceV6.arAssessmentRun.Assessment = new AssessmentRunAssessment();

            nfviNewFindingViewItem.updateOadStringLists();

            return nfviNewFindingViewItem.getFindingViewItemForLastFindingAdded();
        }

        public static void createAssessmentFileWithVirtualTraces_fromTwoSourceAssessmentFiles(String sAssessmentFile1,
                                                                                              String sAssessmentFile2,
                                                                                              String sTargetFile)
        {
            O2AssessmentData_OunceV6 fadF1AssessmentData_sAssessmentFile1 = null;
            O2AssessmentData_OunceV6 fadF1AssessmentData_sAssessmentFile2 = null;


            // get list of traces with 
            List<AssessmentAssessmentFileFinding> lfFindingsWithTraces_sAssessmentFile2 =
                AnalysisUtils.getListOfAllFindingsWithTraces(sAssessmentFile2, ref fadF1AssessmentData_sAssessmentFile2);

            // calculate traces to join
            var dTracesToAppend = new Dictionary<String, List<CallInvocation>>();

            foreach (AssessmentAssessmentFileFinding fFinding in lfFindingsWithTraces_sAssessmentFile2)
                if (fFinding.Trace != null && fFinding.Trace[0] != null && fFinding.Trace[0].CallInvocation1 != null &&
                    fFinding.Trace[0].CallInvocation1.Length > 1)
                    if (
                        OzasmtUtils_OunceV6.getStringIndexValue(fFinding.Trace[0].CallInvocation1[0].sig_id,
                                                                fadF1AssessmentData_sAssessmentFile2).IndexOf(
                            sExternalSourceString) > -1)
                    {
                        String sSignatureOfCallbackFunction =
                            OzasmtUtils_OunceV6.getStringIndexValue(fFinding.Trace[0].CallInvocation1[1].sig_id,
                                                                    fadF1AssessmentData_sAssessmentFile2);
                        if (false == dTracesToAppend.ContainsKey(sSignatureOfCallbackFunction))
                            dTracesToAppend.Add(sSignatureOfCallbackFunction, new List<CallInvocation>());

                        dTracesToAppend[sSignatureOfCallbackFunction].Add(fFinding.Trace[0].CallInvocation1[1]);
                    }


            // get sinks to append traces
            List<String> lsSinks_sAssessmentFile1 = AnalysisAssessmentFile.getListOf_KnownSinks(sAssessmentFile1, ref fadF1AssessmentData_sAssessmentFile1);
            Analysis.populateDictionariesWithXrefsToLoadedAssessment(Analysis.FindingFilter.SmartTraces, true, true,
                                                                     fadF1AssessmentData_sAssessmentFile1);

            var dNewStringIndex = new Dictionary<String, UInt32>();
            foreach (
                AssessmentRunStringIndex siStringIndex in
                    fadF1AssessmentData_sAssessmentFile1.arAssessmentRun.StringIndeces)
                dNewStringIndex.Add(siStringIndex.value, siStringIndex.id);
            var dNewFileIndex = new Dictionary<String, UInt32>();
            foreach (AssessmentRunFileIndex siStringIndex in fadF1AssessmentData_sAssessmentFile1.arAssessmentRun.FileIndeces)
                dNewFileIndex.Add(siStringIndex.value, siStringIndex.id);

            TraceType tTraceType = TraceType.Known_Sink;

            foreach (String sSink in lsSinks_sAssessmentFile1)
                if (dTracesToAppend.ContainsKey(sSink))
                {
                    List<AssessmentAssessmentFileFinding> lfFindingsWithSink =
                        AnalysisUtils.getListOfFindingsWithTraceAndSignature(sSink, tTraceType,
                                                                             fadF1AssessmentData_sAssessmentFile1);
                    foreach (AssessmentAssessmentFileFinding fFindingToJoin in lfFindingsWithSink)
                    {
                        var lfNewFindinds = new List<AssessmentAssessmentFileFinding>();

                        foreach (CallInvocation ciCallInvocationToAppend in dTracesToAppend[sSink])
                        {
                            // append trace 

                            AssessmentAssessmentFileFinding fNewFinding = createNewFindingFromExistingOne(
                                fFindingToJoin, dNewStringIndex, dNewFileIndex, fadF1AssessmentData_sAssessmentFile1);
                            CallInvocation ciSinkNode =
                                AnalysisSearch.findTraceTypeAndSignatureInSmartTrace_Recursive_returnCallInvocation(
                                    fNewFinding.Trace, tTraceType, sSink, fadF1AssessmentData_sAssessmentFile1);
                            ciSinkNode.trace_type = (int) TraceType.Source;
                            var lciTempNewCallInvocation = new List<CallInvocation>(); // used by the recursive function
                            ciSinkNode.CallInvocation1 = updateAssessmentRunWithTraceReferences_recursive(
                                lciTempNewCallInvocation,
                                //new CallInvocation[] { ciCallInvocationToAppend },
                                ciCallInvocationToAppend.CallInvocation1,
                                dNewStringIndex,
                                dNewFileIndex,
                                fadF1AssessmentData_sAssessmentFile2);

                            lfNewFindinds.Add(fNewFinding);
                        }
                        AssessmentAssessmentFile fFile = fadF1AssessmentData_sAssessmentFile1.dFindings[fFindingToJoin];
                        var lfFindingsInCurrentFile = new List<AssessmentAssessmentFileFinding>(fFile.Finding);
                        lfFindingsInCurrentFile.Remove(fFindingToJoin);
                        lfFindingsInCurrentFile.AddRange(lfNewFindinds);
                        fFile.Finding = lfFindingsInCurrentFile.ToArray();
                    }
                }

            // update indexes
            fadF1AssessmentData_sAssessmentFile1.arAssessmentRun.StringIndeces =
                OzasmtUtils_OunceV6.createStringIndexArrayFromDictionary(dNewStringIndex);
            fadF1AssessmentData_sAssessmentFile1.arAssessmentRun.FileIndeces =
                OzasmtUtils_OunceV6.createFileIndexArrayFromDictionary(dNewFileIndex);

            //String sTargetFile = config.getTempFileNameInF1TempDirectory();
            OzasmtUtils_OunceV6.createSerializedXmlFileFromAssessmentRunObject(
                fadF1AssessmentData_sAssessmentFile1.arAssessmentRun, sTargetFile);
            DI.log.debug("Joined assesment saved to:{0}", sTargetFile);
        }


        public static AssessmentAssessmentFile createNewAssessmentFileFromExistingOne(
            AssessmentAssessmentFile afOriginalFile)
        {
            // need to create a new one since we don't want to add the findings of the current file
            var afNewFile = new AssessmentAssessmentFile();
            afNewFile.error_status = afOriginalFile.error_status;
            afNewFile.filename = afOriginalFile.filename;
            afNewFile.last_modified_time = afOriginalFile.last_modified_time;
            return afNewFile;
        }

        public static AssessmentAssessmentFileFinding createNewFindingFromExistingOne(
            AssessmentAssessmentFileFinding fOriginalFinding, Dictionary<String, UInt32> dNewStringIndex,
            Dictionary<String, UInt32> dNewFileIndex, O2AssessmentData_OunceV6 fadOriginalO2AssessmentDataOunceV6)
        {
            if (fOriginalFinding != null && fOriginalFinding.Trace != null)
            {
                var fFinding = new AssessmentAssessmentFileFinding();
                fFinding.actionobject_id = fOriginalFinding.actionobject_id;
                fFinding.caller_name = fOriginalFinding.caller_name;
                //fFinding.caller_name_id = fOriginalFinding.caller_name_id;
                fFinding.caller_name_id = (fOriginalFinding.caller_name_id == null)
                                              ? null
                                              : updateNewAssessmentRunWithStringID(
                                                    UInt32.Parse(fOriginalFinding.caller_name_id), dNewStringIndex,
                                                    fadOriginalO2AssessmentDataOunceV6).ToString();
                fFinding.confidence = fOriginalFinding.confidence;
                fFinding.context = fOriginalFinding.context;
                fFinding.exclude = fOriginalFinding.exclude;
                fFinding.line_number = fOriginalFinding.line_number;
                fFinding.ordinal = fOriginalFinding.ordinal;
                fFinding.project_name = fOriginalFinding.project_name;
                fFinding.property_ids = fOriginalFinding.property_ids;
                fFinding.record_id = fOriginalFinding.record_id;
                fFinding.severity = fOriginalFinding.severity;
                fFinding.Text = fOriginalFinding.Text;
                fFinding.vuln_name = fOriginalFinding.vuln_name;
                fFinding.vuln_name_id = (fOriginalFinding.vuln_name_id == null)
                                            ? null
                                            : updateNewAssessmentRunWithStringID(
                                                  UInt32.Parse(fOriginalFinding.vuln_name_id), dNewStringIndex,
                                                  fadOriginalO2AssessmentDataOunceV6).ToString();
                fFinding.vuln_type = fOriginalFinding.vuln_type;
                fFinding.vuln_type_id = (fOriginalFinding.vuln_type_id == null)
                                            ? null
                                            : updateNewAssessmentRunWithStringID(
                                                  UInt32.Parse(fOriginalFinding.vuln_type_id), dNewStringIndex,
                                                  fadOriginalO2AssessmentDataOunceV6).ToString();
                //fFinding.vuln_name = (fOriginalFinding.vuln_name != null) ? fOriginalFinding.vuln_name : Analysis.getStringIndexValue(UInt32.Parse(fOriginalFinding.vuln_name_id), fadOriginalO2AssessmentDataOunceV6);
                //fFinding.vuln_type = (fOriginalFinding.vuln_type != null) ? fOriginalFinding.vuln_type : Analysis.getStringIndexValue(UInt32.Parse(fOriginalFinding.vuln_type_id), fadOriginalO2AssessmentDataOunceV6);

                var lciNewCallInvocation = new List<CallInvocation>();
                // fOriginalFinding.Trace = updateAssessmentRunWithTraceReferences_recursive(lciNewCallInvocation, fOriginalFinding.Trace, dNewStringIndex, dNewFileIndex, fadOriginalO2AssessmentDataOunceV6);
                fFinding.Trace = updateAssessmentRunWithTraceReferences_recursive(lciNewCallInvocation,
                                                                                  fOriginalFinding.Trace,
                                                                                  dNewStringIndex, dNewFileIndex,
                                                                                  fadOriginalO2AssessmentDataOunceV6);
                return fFinding;
            }
            return fOriginalFinding;
        }

        // we need to create new CallInvocation Objects because we need to change them
        public static CallInvocation[] updateAssessmentRunWithTraceReferences_recursive(
            List<CallInvocation> lciNewCallInvocation, CallInvocation[] aciOriginalCallInvocation,
            Dictionary<String, UInt32> dNewStringIndex, Dictionary<String, UInt32> dNewFileIndex,
            O2AssessmentData_OunceV6 fadOriginalO2AssessmentDataOunceV6)
        {
            if (aciOriginalCallInvocation == null)
                return null;
            else
            {
                foreach (CallInvocation ciOriginalCallInvocation in aciOriginalCallInvocation)
                {
                    var ciNewCallInvocation = new CallInvocation();
                    ciNewCallInvocation.cn_id = updateNewAssessmentRunWithStringID(ciOriginalCallInvocation.cn_id,
                                                                                   dNewStringIndex,
                                                                                   fadOriginalO2AssessmentDataOunceV6);
                    ciNewCallInvocation.column_number = ciOriginalCallInvocation.column_number;
                    ciNewCallInvocation.cxt_id = updateNewAssessmentRunWithStringID(ciOriginalCallInvocation.cxt_id,
                                                                                    dNewStringIndex,
                                                                                    fadOriginalO2AssessmentDataOunceV6);
                    ciNewCallInvocation.fn_id = updateNewAssessmentRunWithFileID(ciOriginalCallInvocation.fn_id,
                                                                                 dNewFileIndex,
                                                                                 fadOriginalO2AssessmentDataOunceV6);
                    ciNewCallInvocation.line_number = ciOriginalCallInvocation.line_number;
                    ciNewCallInvocation.mn_id = updateNewAssessmentRunWithStringID(ciOriginalCallInvocation.mn_id,
                                                                                   dNewStringIndex,
                                                                                   fadOriginalO2AssessmentDataOunceV6);
                    ciNewCallInvocation.ordinal = ciOriginalCallInvocation.ordinal;
                    ciNewCallInvocation.sig_id = updateNewAssessmentRunWithStringID(ciOriginalCallInvocation.sig_id,
                                                                                    dNewStringIndex,
                                                                                    fadOriginalO2AssessmentDataOunceV6);
                    ciNewCallInvocation.taint_propagation = ciOriginalCallInvocation.taint_propagation;
                    ciNewCallInvocation.Text = ciOriginalCallInvocation.Text;
                    ciNewCallInvocation.trace_type = ciOriginalCallInvocation.trace_type;

                    var lciNewCallInvocation_Child = new List<CallInvocation>();
                    ciNewCallInvocation.CallInvocation1 =
                        updateAssessmentRunWithTraceReferences_recursive(lciNewCallInvocation_Child,
                                                                         ciOriginalCallInvocation.CallInvocation1,
                                                                         dNewStringIndex, dNewFileIndex,
                                                                         fadOriginalO2AssessmentDataOunceV6);

                    lciNewCallInvocation.Add(ciNewCallInvocation);
                }
                return lciNewCallInvocation.ToArray();
            }
        }

        public static UInt32 updateNewAssessmentRunWithFileID(UInt32 uIdToUpdate,
                                                              Dictionary<String, UInt32> dNewFileIndex,
                                                              O2AssessmentData_OunceV6 fadOriginalO2AssessmentDataOunceV6)
        {
            if (uIdToUpdate == 0)
                return 0;
            String sTextToUpdate = OzasmtUtils_OunceV6.getFileIndexValue(uIdToUpdate, fadOriginalO2AssessmentDataOunceV6);
            if (dNewFileIndex.ContainsKey(sTextToUpdate))
                return dNewFileIndex[sTextToUpdate];

            UInt32 uNewId = (UInt32) dNewFileIndex.Count + 1;
            dNewFileIndex.Add(sTextToUpdate, uNewId);
            return uNewId;
        }

        public static UInt32 updateNewAssessmentRunWithStringID(UInt32 uIdToUpdate,
                                                                Dictionary<String, UInt32> dNewStringIndex,
                                                                O2AssessmentData_OunceV6 fadOriginalO2AssessmentDataOunceV6)
        {
            if (uIdToUpdate == 0)
                return 0;
            String sTextToUpdate = OzasmtUtils_OunceV6.getStringIndexValue(uIdToUpdate, fadOriginalO2AssessmentDataOunceV6);
            if (dNewStringIndex.ContainsKey(sTextToUpdate))
                return dNewStringIndex[sTextToUpdate];

            UInt32 uNewId = (UInt32) dNewStringIndex.Count + 1;
            dNewStringIndex.Add(sTextToUpdate, uNewId);
            return uNewId;
        }

        #region Nested type: NewFinding

        public class NewFinding
        {
            public AssessmentAssessmentFile fFile = new AssessmentAssessmentFile();
            public AssessmentAssessmentFileFinding fFinding = new AssessmentAssessmentFileFinding();
            public O2AssessmentData_OunceV6 oadNewO2AssessmentDataOunceV6 = new O2AssessmentData_OunceV6();
            private UInt32 uFileNameId;

            public NewFinding()
            {
                oadNewO2AssessmentDataOunceV6.arAssessmentRun = OzasmtUtils_OunceV6.getDefaultAssessmentRunObject();
                oadNewO2AssessmentDataOunceV6.dFindings =
                    new Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile>();
                oadNewO2AssessmentDataOunceV6.dFindings.Add(fFinding, fFile);
                fFinding.actionobject_id = 0;
                fFinding.caller_name_id = "0";
                fFinding.cxt_id = "0";
                fFinding.vuln_name_id = "0";
                fFinding.vuln_type_id = "0";
            }

            public CallInvocation setRootTrace(string sRootTraceText)
            {
                var ciCallInvocation = new CallInvocation();
                UInt32 uRootTraceText = OzasmtUtils_OunceV6.addTextToStringIndexes(sRootTraceText,
                                                                                   oadNewO2AssessmentDataOunceV6.arAssessmentRun);
                ciCallInvocation.sig_id = uRootTraceText;
                ciCallInvocation.fn_id = 1;
                ciCallInvocation.trace_type = (UInt32) TraceType.Root_Call;
                fFinding.Trace = new[] {ciCallInvocation};
                return ciCallInvocation;
            }

            public CallInvocation addCallToCall(String sNewCallName, CallInvocation ciTargetCallInvocation,
                                                TraceType ttTraceType)
            {
                var ciNewCallInvocation = new CallInvocation();
                UInt32 uCall = OzasmtUtils_OunceV6.addTextToStringIndexes(sNewCallName, oadNewO2AssessmentDataOunceV6.arAssessmentRun);
                ciNewCallInvocation.sig_id = uCall;
                ciNewCallInvocation.cxt_id = uCall;
                // by default make these the same (the context is used to remove duplicate findings)                
                ciNewCallInvocation.fn_id = 1;
                // add file mapping so that the viewer's can point to the vm file          
                ciNewCallInvocation.trace_type = (UInt32) ttTraceType;
                if (ciTargetCallInvocation.CallInvocation1 == null)
                    ciTargetCallInvocation.CallInvocation1 = new[] {ciNewCallInvocation};
                else
                {
                    var lTargetCallTraces = new List<CallInvocation>(ciTargetCallInvocation.CallInvocation1);
                    lTargetCallTraces.Add(ciNewCallInvocation);
                    ciTargetCallInvocation.CallInvocation1 = lTargetCallTraces.ToArray();
                }
                return ciNewCallInvocation;
            }

            public void setFinding_ActionObjectId(UInt32 uActionObjectId)
            {
                fFinding.actionobject_id = uActionObjectId;
            }

            public void setFinding_fakeActionObjectId(String sFakeActionObject)
            {
                fFinding.actionobject_id = OzasmtUtils_OunceV6.addTextToStringIndexes(sFakeActionObject,
                                                                                      oadNewO2AssessmentDataOunceV6.arAssessmentRun);
            }

            public void setFinding_CallerName(String sCallerName)
            {
                fFinding.caller_name_id =
                    OzasmtUtils_OunceV6.addTextToStringIndexes(sCallerName, oadNewO2AssessmentDataOunceV6.arAssessmentRun).ToString();
            }

            public void setFinding_Context(String sContext)
            {
                fFinding.cxt_id =
                    OzasmtUtils_OunceV6.addTextToStringIndexes(sContext, oadNewO2AssessmentDataOunceV6.arAssessmentRun).ToString();
            }

            public void setFinding_LineNumber(ushort uLineNumber)
            {
                fFinding.line_number = uLineNumber;
            }

            public void setFinding_VulnName(String sVulnName)
            {
                fFinding.vuln_name_id =
                    OzasmtUtils_OunceV6.addTextToStringIndexes(sVulnName, oadNewO2AssessmentDataOunceV6.arAssessmentRun).ToString();
            }

            public void setFinding_VulnType(String sVulnType)
            {
                fFinding.vuln_type_id =
                    OzasmtUtils_OunceV6.addTextToStringIndexes(sVulnType, oadNewO2AssessmentDataOunceV6.arAssessmentRun).ToString();
            }

            public void setFinding_FileName(String sFilename)
            {
                uFileNameId = OzasmtUtils_OunceV6.addTextToFileIndexes(sFilename, oadNewO2AssessmentDataOunceV6.arAssessmentRun);
                fFile.filename = sFilename;
            }
        }

        #endregion

        #region Nested type: NewFindingViewItem

        public class NewFindingViewItem
        {
            private readonly Dictionary<String, UInt32> dNewFileIndex = new Dictionary<String, UInt32>();
            private readonly Dictionary<String, UInt32> dNewStringIndex = new Dictionary<String, UInt32>();

            private readonly List<AssessmentAssessmentFileFinding> lfNewFindinds =
                new List<AssessmentAssessmentFileFinding>();

            private AssessmentAssessmentFileFinding fLastFindingAdded;
            public O2AssessmentData_OunceV6 oadNewO2AssessmentDataOunceV6 = new O2AssessmentData_OunceV6();

            public NewFindingViewItem()
            {
                oadNewO2AssessmentDataOunceV6.arAssessmentRun = OzasmtUtils_OunceV6.getDefaultAssessmentRunObject();
                //oadNewO2AssessmentDataOunceV6.arAssessmentRun = new AssessmentRun();
                oadNewO2AssessmentDataOunceV6.arAssessmentRun.name = "O2 genereated trace";
            }

            public AssessmentAssessmentFileFinding AddNewFindingFromExistingOne(
                AssessmentAssessmentFileFinding fFinding, O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
            {
                AssessmentAssessmentFileFinding fNewFinding = createNewFindingFromExistingOne(fFinding, dNewStringIndex,
                                                                                              dNewFileIndex,
                                                                                              oadO2AssessmentDataOunceV6);
                lfNewFindinds.Add(fNewFinding);

                //AssessmentAssessmentFile fNewFile = AnalysisSearch.createNewAssessmentFileFromExistingOne(fviJoinAtSink.oadO2AssessmentDataOunceV6.dFindings[fviJoinAtSink.fFinding]);
                AssessmentAssessmentFile fNewFile =
                    createNewAssessmentFileFromExistingOne(oadO2AssessmentDataOunceV6.dFindings[fFinding]);
                fNewFile.Finding = new[] {fNewFinding};

                oadNewO2AssessmentDataOunceV6.dFindings =
                    new Dictionary<AssessmentAssessmentFileFinding, AssessmentAssessmentFile>();
                oadNewO2AssessmentDataOunceV6.dFindings.Add(fNewFinding, fNewFile);
                fLastFindingAdded = fNewFinding;
                return fNewFinding;
            }

            public FindingViewItem getFindingViewItemForLastFindingAdded()
            {
                if (fLastFindingAdded != null)
                    return new FindingViewItem(fLastFindingAdded, oadNewO2AssessmentDataOunceV6);
                else
                    return null;
            }

            public bool appendTrace_FindingSourceToFindingSink(AssessmentAssessmentFileFinding fJoinAtSink,
                                                               FindingViewItem fviJoinAtSource)
            {
                //Get the Sink of the first trace                        
                CallInvocation ciSinkNode =
                    AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fJoinAtSink.Trace,
                                                                                            TraceType.Known_Sink);
                if (ciSinkNode == null)
                {
                    //              DI.log.error("in appendTrace_FindingSourceToFindingSink, could not find the Sink of fviJoinAtSink");
                    return false;
                }

                // get the source of the 2nd trace

                // There are 3 possible Gluing Scenarios
                //   a source that has child nodes (when it is a callback)
                //   a source trace that has a compatible signature with the sink trace (when it was creted via a source of tainded data rule).  For this one we will have to find the correct injection point
                //   a source trace that has nothing do with the source (interfaces gluing for example) and we have the same two cases above
                // the strategy to find a gluing point (on the fviJoinAtSource is to find the first trace that has a sink

                // try to get case 1 see if the current source has child nodes
                CallInvocation ciSourceNode =
                    AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(
                        fviJoinAtSource.fFinding.Trace, TraceType.Source);

                if (ciSourceNode == null)
                {
                    DI.log.error(
                        "in appendTrace_FindingSourceToFindingSink, could not find the Source of fviJoinAtSource");
                    return false;
                }

                if (ciSourceNode.CallInvocation1 == null) // means we are case 2 or 3     
                {
                    CallInvocation ciSourceNodeWithSink =
                        AnalysisSearch.fromSourceFindFirstTraceWithAChildSink(fviJoinAtSource.fFinding,
                                                                              fviJoinAtSource.oadO2AssessmentDataOunceV6);
                    if (ciSourceNodeWithSink != null)
                        // if we found this it means that we are now on Trace that the first child node goes to the source and the 2nd goes to the Sink
                        ciSourceNode = ciSourceNodeWithSink.CallInvocation1[1];
                }

                // make the previous Sink that Type 4 that doesn't seem to be used (could make it sources but it is cleaner with using this extra trace type for the joins
                ciSinkNode.trace_type = (int) TraceType.Type_4;


                CallInvocation[] aciCallInvocation;
                if (AnalysisUtils.getSink(fJoinAtSink, oadNewO2AssessmentDataOunceV6) ==
                    AnalysisUtils.getSource(fviJoinAtSource.fFinding, fviJoinAtSource.oadO2AssessmentDataOunceV6))
                    aciCallInvocation = ciSourceNode.CallInvocation1;
                else
                    aciCallInvocation = new[] {ciSourceNode};
                var lciTempNewCallInvocation = new List<CallInvocation>(); // used by the recursive function

                ciSinkNode.CallInvocation1 = updateAssessmentRunWithTraceReferences_recursive(
                    lciTempNewCallInvocation,
                    aciCallInvocation,
                    dNewStringIndex,
                    dNewFileIndex,
                    fviJoinAtSource.oadO2AssessmentDataOunceV6);


                return true;
            }

            public void updateOadStringLists()
            {
                oadNewO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces =
                    OzasmtUtils_OunceV6.createStringIndexArrayFromDictionary(dNewStringIndex);
                oadNewO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces =
                    OzasmtUtils_OunceV6.createFileIndexArrayFromDictionary(dNewFileIndex);
            }
        }

        #endregion
    }
}