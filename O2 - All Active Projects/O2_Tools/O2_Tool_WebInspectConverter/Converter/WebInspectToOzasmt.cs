// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Tool.WebInspectConverter.Converter
{
    public class WebInspectToOzasmt
    {
        public static List<IO2Finding> createO2FindingsFromWebInspectResults(WebInspectResults webInspectResults)
        {
            var results = new List<IO2Finding>();
            foreach (WebInspectFinding webInspectFinding in webInspectResults.webInspectFindings)
            {
                foreach (string word in new FilteredUrl(webInspectFinding.fullUrl).words)
                    results.Add(createO2FindingFromWebInspectFinding(webInspectFinding, word));
            }
            return results;
        }

        public static O2Finding createO2FindingFromWebInspectFinding(WebInspectFinding webInspectFinding, string keyword)
        {
            var o2Trace = new O2Trace("WebInspect -> Ounce Mapping (Sql Injection)");
            IO2Trace sink = createSink(webInspectFinding);
            o2Trace.childTraces.Add(sink);

            return new O2Finding
                       {
                           o2Traces = new List<IO2Trace> {o2Trace},
                           //context = webInspectFinding.payload,
                           context = webInspectFinding.fullUrl,
                           vulnName = keyword + "_" + webInspectFinding.param,
                           vulnType = "Sql Injection (from WebInspect)"
                       };
        }

        public static IO2Trace createSink(WebInspectFinding webInspectFinding)
        {
            var filteredUrl = new FilteredUrl(webInspectFinding.fullUrl);

            return new O2Trace("WebInspect:   " + filteredUrl.pathAndPageAndParameters, TraceType.Known_Sink)
                       {
                           context = webInspectFinding.payload,
                           method = webInspectFinding.param
                       };
        }
    }
}
