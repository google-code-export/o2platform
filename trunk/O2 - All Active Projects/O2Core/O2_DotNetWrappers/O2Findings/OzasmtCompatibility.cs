using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.DotNetWrappers.O2Findings
{
    public class OzasmtCompatibility
    {
        public static void makeCompatibleWithOunceV6(IEnumerable<IO2Finding> o2Findings)
        {
            // fix use of non-OSA supported trace types:
            foreach (var o2Finding in o2Findings)
                foreach (var o2Trace in OzasmtUtils.getListWithAllTraces((O2Finding) o2Finding))
                    switch (o2Trace.traceType)
                    {
                        case TraceType.O2Info:
                        case TraceType.O2JoinSink:
                        case TraceType.O2JoinSource:
                            o2Trace.traceType = TraceType.Type_4;
                            break;

                    }
        }
    }
}
