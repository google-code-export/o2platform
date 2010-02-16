// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.O2Findings;
using O2.Interfaces.O2Findings;
using O2.Interfaces.Rules;

namespace O2.Rules.OunceLabs.Filters
{
    class Filter_CreateAllPartialTraces
    {
        public static List<IO2Finding> applyFilter(IEnumerable<IO2Finding> o2TargetO2Findings, List<IO2Rule> o2Rules)
        {
            var findingsCreated = new List<IO2Finding>();
            var numberOfItemsToProcess = o2TargetO2Findings.Count();
            var itemsProcessed = 0;
            foreach (IO2Finding targetO2Finding in o2TargetO2Findings)
            {
                var parentO2Finding = OzasmtCopy.createCopy(targetO2Finding, false);
                //parentO2Finding.o2Traces = new List<IO2Trace>();
                createAllPartialTraces(targetO2Finding.o2Traces, parentO2Finding.o2Traces, parentO2Finding, findingsCreated);
                itemsProcessed++;
                if (itemsProcessed % 5000 == 0)
                    DI.log.info("applyFllter_MapSinksToAllTraces: {0} Findings created so far  [{1}/{2}]", findingsCreated.Count, itemsProcessed, numberOfItemsToProcess);
            }
            DI.log.info("applyFllter_MapSinksToAllTraces: {0} Findings created in total", findingsCreated.Count);
            return findingsCreated;
        }

        public static void createAllPartialTraces(IEnumerable<IO2Trace> o2TracesToFollow, ICollection<IO2Trace> o2PartialTraces, IO2Finding parentFinding, ICollection<IO2Finding> findingsCreated)
        {
            // process all traces
            foreach (var o2TraceToFollow in o2TracesToFollow)
            {
                // create a copy of the current trace
                var newO2Trace = OzasmtCopy.createCopy(o2TraceToFollow, false);
                //newO2Trace.childTraces = new List<IO2Trace>();   // remove the child traces
                // add it to the trace we are building
                o2PartialTraces.Add(newO2Trace);
                // create a copy of the parent finding
                var newO2Finding = OzasmtCopy.createCopy(parentFinding);
                findingsCreated.Add(newO2Finding);
                // and trigger the recursive execution on all child traces
                createAllPartialTraces(o2TraceToFollow.childTraces, newO2Trace.childTraces, parentFinding, findingsCreated);
            }
        }
    }
}
