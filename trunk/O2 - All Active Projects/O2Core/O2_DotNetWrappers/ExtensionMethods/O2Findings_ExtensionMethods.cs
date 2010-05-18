using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;
using O2.Interfaces.O2Findings;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.ExtensionMethods;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class O2Findings_ExtensionMethods
    {
        #region set

        public static List<IO2Finding> set_VulnNameAndType(this List<IO2Finding> o2Findings, string vulnName, string vulnType)
        {
            foreach (var o2Finding in o2Findings)
            {
                o2Finding.vulnName = vulnName;
                o2Finding.vulnType = vulnType;
            }
            return o2Findings;
        }
        public static List<IO2Finding> set_VulnName(this List<IO2Finding> o2Findings, string vulnName)
        {
            foreach (var o2Finding in o2Findings)
                o2Finding.vulnName = vulnName;
            return o2Findings;
        }

        public static List<IO2Finding> set_VulnType(this List<IO2Finding> o2Findings, string vulnType)
        {
            foreach (var o2Finding in o2Findings)
                o2Finding.vulnType = vulnType;
            return o2Findings;
        }

        #endregion

        #region gets

        public static List<string> sinks(this List<IO2Finding> o2Findings)
        {
            var sinks = from o2Finding in o2Findings select ((O2Finding)o2Finding).Sink;
            return sinks.toList();
        }

        public static List<string> sources(this List<IO2Finding> o2Findings)
        {
            var sources = from o2Finding in o2Findings select ((O2Finding)o2Finding).Source;
            return sources.toList();
        }

        #endregion


        #region filters

        public static List<IO2Finding> filter_SinkStartsWith(this List<IO2Finding> o2Findings, string text)
        {
            var matches = from o2Finding in o2Findings
                          where ((O2Finding)o2Finding).Sink.starts(text)
                          select o2Finding;
            return matches.toList();
        }

        public static List<IO2Finding> applyRule(this List<IO2Finding> o2Findings, string sinkToFind, string vulnName)
        {
            var results = new List<IO2Finding>();
            foreach (var iO2Finding in o2Findings)
            {
                var o2Finding = (O2Finding)iO2Finding;
                if ((o2Finding).Sink.contains(sinkToFind))
                {
                    o2Finding.vulnName = vulnName;
                    results.add(o2Finding);
                }
            }
            return results;
        }
        #endregion
    }
}
