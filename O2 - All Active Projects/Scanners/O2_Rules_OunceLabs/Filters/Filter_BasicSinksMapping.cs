// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.Rules;
using O2.Rules.OunceLabs.RulesUtils;

namespace O2.Rules.OunceLabs.Filters
{
    class Filter_BasicSinksMapping
    {
        public static List<IO2Finding> applyFilter(ICollection<IO2Finding> targetO2Findings, List<IO2Rule>  o2RulesToUse)
        {
            var filterName = "BasicSinksMapping"; 

            DI.log.info("Applying filter {0} to {1} findings using {2} rules", filterName, targetO2Findings.Count,
                        o2RulesToUse.Count);

            var indexedRules = IndexedO2Rules.indexOnSinks(o2RulesToUse);

            // list to hold mapped findings
            var mappedFidings = new List<IO2Finding>();
            foreach(O2Finding o2Finding in targetO2Findings)
            {
                var sink = o2Finding.Sink;
                foreach (var newFinding in FiltersUtils.applySinkRuleToFindingAndTrace(o2Finding, sink, indexedRules))
                    mappedFidings.Add(newFinding);
            }
            DI.log.info("There were {0} findings mapped", mappedFidings.Count);
            return mappedFidings;
        }
    }
}
