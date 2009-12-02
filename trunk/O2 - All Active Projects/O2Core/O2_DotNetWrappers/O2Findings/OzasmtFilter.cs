using System.Collections.Generic;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.DotNetWrappers.O2Findings
{
    public class OzasmtFilter
    {
        public static List<IO2Finding> getFindingsWithSink(List<IO2Finding> findings, string regExToFind)
        {
            var results = new List<IO2Finding>();
            foreach (IO2Finding o2Finding in findings)
            {
                IO2Trace sink = OzasmtUtils.getKnownSink(o2Finding.o2Traces);
                if (sink != null && sink.signature != "" && RegEx.findStringInString(sink.signature, regExToFind))
                    results.Add(o2Finding);
            }
            return results;
        }
    }
}