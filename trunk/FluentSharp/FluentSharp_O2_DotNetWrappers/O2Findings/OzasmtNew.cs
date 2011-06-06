// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using FluentSharp.O2.Interfaces.O2Findings;

namespace FluentSharp.O2.DotNetWrappers.O2Findings
{
    public class OzasmtNew
    {
        public static void createO2AssessmentFromTraces(IO2AssessmentSave o2AssessmentSave,string fileToCreate, List<IO2Trace> traces)
        {
            var o2Assessment = new O2Assessment();            
            foreach (IO2Trace trace in traces)
                o2Assessment.o2Findings.Add(createO2FindingFromTrace(trace));
            o2Assessment.save(o2AssessmentSave, fileToCreate);
        }

        public static IO2Finding createO2FindingFromTrace(IO2Trace o2Trace)
        {
            var o2Finding = new O2Finding
                                {
                                    callerName = o2Trace.signature,
                                    context = o2Trace.context,
                                    columnNumber = o2Trace.columnNumber,
                                    file = o2Trace.file,
                                    lineNumber = o2Trace.lineNumber,
                                    o2Traces = new List<IO2Trace> {OzasmtCopy.createCopy(o2Trace)},
                                    ordinal = o2Trace.ordinal,
                                    text = OzasmtCopy.createCopy(o2Trace.text),
                                    vulnName = o2Trace.signature,
                                    vulnType = "FindingFromTrace"
                                };
            return o2Finding;
        }
    }
}
