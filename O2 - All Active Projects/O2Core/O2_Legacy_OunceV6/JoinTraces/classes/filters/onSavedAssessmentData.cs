// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces.classes.filters
{
    public class onSavedAssessmentData
    {
        public static void MakeAllLostSinksIntoKnownSinks(O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6)
        {
            foreach (AssessmentAssessmentFileFinding fFinding in oadO2AssessmentDataOunceV6.dFindings.Keys)
            {
                CallInvocation ciLostSink =
                    AnalysisSearch.findTraceTypeInSmartTrace_Recursive_returnCallInvocation(fFinding.Trace,
                                                                                            TraceType.Lost_Sink);
                if (ciLostSink != null)
                {
                    ciLostSink.trace_type = (UInt32) TraceType.Known_Sink;
                }
            }
        }
    }
}
