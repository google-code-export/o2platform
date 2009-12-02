using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class AnalysisUtils
    {
        public static String getSink(FindingViewItem fviFindingViewItem)
        {
            return getSink(fviFindingViewItem.fFinding, fviFindingViewItem.oadO2AssessmentDataOunceV6);
        }

        public static String getSink(AssessmentAssessmentFileFinding fFinding, O2AssessmentData_OunceV6 oadF1AssessmentDataOunceV6)
        {
            CallInvocation ciCallInvocation =
                AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                        TraceType.Known_Sink);
            if (ciCallInvocation != null)
                return OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadF1AssessmentDataOunceV6);
            else
                return "";
        }

        public static String getSource(FindingViewItem fviFindingViewItem)
        {
            return getSource(fviFindingViewItem.fFinding, fviFindingViewItem.oadO2AssessmentDataOunceV6);
        }

        public static String getSource(AssessmentAssessmentFileFinding fFinding, O2AssessmentData_OunceV6 oadF1AssessmentDataOunceV6)
        {
            CallInvocation ciCallInvocation =
                AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                        TraceType.Source);
            if (ciCallInvocation != null)
                return OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadF1AssessmentDataOunceV6);

            return "";
        }


        public static String getSinkContext(AssessmentAssessmentFileFinding fFinding,
                                            O2AssessmentData_OunceV6 oadF1AssessmentDataOunceV6)
        {
            CallInvocation ciCallInvocation =
                AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                        TraceType.Known_Sink);
            if (ciCallInvocation != null)
                return OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.cxt_id, oadF1AssessmentDataOunceV6);
            else
                return "";
        }

        public static String getNonSourceOrNonSink(AssessmentAssessmentFileFinding fFinding,
                                                   O2AssessmentData_OunceV6 oadF1AssessmentDataOunceV6)
        {
            CallInvocation ciCallInvocation =
                AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                        TraceType.Root_Call);
            if (ciCallInvocation != null)
                return OzasmtUtils_OunceV6.getStringIndexValue(ciCallInvocation.sig_id, oadF1AssessmentDataOunceV6);
            else
                return "";
        }

        public static void getListWithMethodsCalled_Recursive(CallInvocation[] cCallInvocations,
                                                              List<CallInvocation> lciMethodsCalled,
                                                              O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6,
                                                              Analysis.SmartTraceFilter stfSmartTraceFilter)
        {
            if (cCallInvocations != null)
                foreach (CallInvocation cCall in cCallInvocations)
                {
                    lciMethodsCalled.Add(cCall);
                    //lsMethodsCalled.Add(getTextFromFindingBySmartTraceFilter(cCall,fadO2AssessmentDataOunceV6,stfSmartTraceFilter));
                    getListWithMethodsCalled_Recursive(cCall.CallInvocation1, lciMethodsCalled, fadO2AssessmentDataOunceV6,
                                                       stfSmartTraceFilter);
                }
        }

        public static String getTextFromFindingBySmartTraceFilter(CallInvocation cCall,
                                                                  O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6,
                                                                  Analysis.SmartTraceFilter stfSmartTraceFilter)
        {
            String sText = "";
            //case Analysis.SmartTraceFilter.MethodName:  // Use this as the default (since it will cover for the cases where the context or source are empty
            if (cCall.sig_id == 0 && cCall.fn_id > 0)
                sText = fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[cCall.fn_id - 1].value;
            else if (cCall.sig_id == 0)
                sText = "...";
            else
                sText = fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[cCall.sig_id - 1].value;
            switch (stfSmartTraceFilter)
            {
                case Analysis.SmartTraceFilter.Context:
                    if (0 != cCall.cxt_id)
                        sText = fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[cCall.cxt_id - 1].value;
                    break;
                case Analysis.SmartTraceFilter.SourceCode:
                    List<String> lsSourceCode =
                        Files.loadSourceFileIntoList(
                            fadO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces[cCall.fn_id - 1].value);
                    String sSounceCodeLine = Files.getLineFromSourceCode(cCall.line_number, lsSourceCode);
                    if ("" != sSounceCodeLine)
                    {
                        sText = sSounceCodeLine;
                        sText = sText.Replace("\t", "");
                    }
                    break;
            }
            return sText;
        }

        public static void addCallsToNode_Recursive(CallInvocation[] cCallInvocations, TreeNode tnTargetNode,
                                                    O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6,
                                                    Analysis.SmartTraceFilter stfSmartTraceFilter)
        {
            if (cCallInvocations != null)
                foreach (CallInvocation cCall in cCallInvocations)
                {
                    String sNodeText = "";
                    if (cCall.mn_id > fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces.Length ||
                        cCall.sig_id > fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces.Length)
                        DI.log.error(
                            "In addCallsToNode_Recursive cCall.sig_id or cCall.cxt_id or fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces.Length ");
                    else
                    {
                        sNodeText =
                            getTextFromFindingBySmartTraceFilter(cCall, fadO2AssessmentDataOunceV6, stfSmartTraceFilter).Trim();
                        /*switch (stfSmartTraceFilter)
                        {
                            case Analysis.SmartTraceFilter.MethodName:
                                sNodeText = (cCall.sig_id == 0) ? "" : fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[cCall.sig_id - 1].value;
                                break;
                            case Analysis.SmartTraceFilter.Context:
                                sNodeText = (cCall.cxt_id == 0) ? "" : fadO2AssessmentDataOunceV6.arAssessmentRun.StringIndeces[cCall.cxt_id - 1].value;
                                break;
                            case Analysis.SmartTraceFilter.SourceCode:
                                List<String> lsSourceCode = forms.loadSourceFileIntoList(fadO2AssessmentDataOunceV6.arAssessmentRun.FileIndeces[cCall.fn_id - 1].value);
                                sNodeText = getLineFromSourceCode(cCall.line_number, lsSourceCode);
                                sNodeText = sNodeText.Replace("\t", "");
                                break;
                        }*/
                    }

                    var tnCallNode = new TreeNode(sNodeText) {Tag = cCall};
                    switch (cCall.trace_type)
                    {
                        case 1: // Analysis.TraceType.Root_Call:                        
                            tnCallNode.ForeColor = Color.DarkBlue;
                            break;
                        case 5: // Analysis.TraceType.Lost_Sink:
                            tnCallNode.ForeColor = Color.DarkOrange;
                            break;
                        case 2: // Analysis.TraceType.Source:
                            tnCallNode.ForeColor = Color.DarkRed;
                            break;
                        case 3: // Analysis.TraceType.Known_Sink:
                            tnCallNode.ForeColor = Color.Red;
                            break;
                        case 4: // Analysis.TraceType.Type_4:
                            tnCallNode.ForeColor = Color.Green;
                            break;
                        default:
                            break;
                    }

                    addCallsToNode_Recursive(cCall.CallInvocation1, tnCallNode, fadO2AssessmentDataOunceV6, stfSmartTraceFilter);
                    tnTargetNode.Nodes.Add(tnCallNode);
                }
        }

        public static List<AssessmentAssessmentFileFinding> getListOfAllFindingsWithTraces(String sAssessmentFileToLoad,
                                                                                           ref O2AssessmentData_OunceV6
                                                                                               fadO2AssessmentDataOunceV6)
        {
            var lfFindingsWithTraces = new List<AssessmentAssessmentFileFinding>();
            if (fadO2AssessmentDataOunceV6 == null)
                Analysis.loadAssessmentFile(sAssessmentFileToLoad, false, ref fadO2AssessmentDataOunceV6);
            if (null != fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                foreach (Assessment aAssessment in fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                    foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                        if (null != afAssessmentFile.Finding)
                            foreach (AssessmentAssessmentFileFinding fFinding in afAssessmentFile.Finding)
                                if (fFinding.Trace != null)
                                    lfFindingsWithTraces.Add(fFinding);
            return lfFindingsWithTraces;
        }

        public static List<AssessmentAssessmentFileFinding> getListOfFindingsWithTraceAndSignature(String sSignature,
                                                                                                   TraceType
                                                                                                       tTraceType,
                                                                                                   O2AssessmentData_OunceV6
                                                                                                       fadO2AssessmentDataOunceV6)
        {
            var fFindingsWithTracesAndSignature = new List<AssessmentAssessmentFileFinding>();
            if (fadO2AssessmentDataOunceV6 == null)
            {
                DI.log.error("in getListOfFindingsWithTraceAndSignature: fadO2AssessmentDataOunceV6 must not be null");
                return fFindingsWithTracesAndSignature;
            }
            List<AssessmentAssessmentFileFinding> fFindingsWithTraces = getListOfAllFindingsWithTraces("",
                                                                                                       ref
                                                                                                           fadO2AssessmentDataOunceV6);
            foreach (AssessmentAssessmentFileFinding fFinding in fFindingsWithTraces)
                if (null !=
                    AnalysisSearch.findTraceTypeAndSignatureInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                                        tTraceType,
                                                                                                        sSignature,
                                                                                                        fadO2AssessmentDataOunceV6))
                    fFindingsWithTracesAndSignature.Add(fFinding);
            return fFindingsWithTracesAndSignature;
        }
    }
}