// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.Rules;
using O2.Rules.OunceLabs.RulesUtils;

namespace O2.Rules.OunceLabs.Filters
{
    class Filter_MapSinksToAllTraces
    {
        public static List<IO2Finding> applyFilter(IEnumerable<IO2Finding> o2TargetO2Findings, List<IO2Rule> o2Rules, bool addFindingsWithNoMatches)
        {
            var findingsCreated = new List<IO2Finding>();
            var numberOfItemsToProcess = o2TargetO2Findings.Count();
            var itemsProcessed = 0;
            // create index of Sinks
            var indexedRules = IndexedO2Rules.indexOnSinks(o2Rules);  // IndexedO2Rules.indexAll(o2Rules); 
            // process add findings            
            foreach(IO2Finding targetO2Finding in o2TargetO2Findings)
            {
                var currentNumberOfCreatedFindings = findingsCreated.Count;
                // create copy of parent with no child traces
                var parentO2Finding = OzasmtCopy.createCopy(targetO2Finding,false);                
                // start recursive loop that will invoke the Lambda callback for every node                
                if (invokeOnAllPartialTraces(targetO2Finding.o2Traces, parentO2Finding.o2Traces,
                                             (o2PartialTraces, o2Trace) => applyRuleToTrace(o2Trace, o2PartialTraces, parentO2Finding, findingsCreated, indexedRules)))
                {
                    // if invokeOnAllPartialTraces returned true (which means we didn't hit a don't propagate item) check if there was any rule created with this finding                    
                    if (addFindingsWithNoMatches)
                        if (currentNumberOfCreatedFindings == findingsCreated.Count)
                        {
                            parentO2Finding.vulnType = "_O2.Lost.Sources";
                                // These are Lost sources (i.e. findings where the Sink happens first than the source
                            parentO2Finding.vulnName = ((O2Finding) parentO2Finding).Sink;
                                // this patch should be done when the joinned traces are created
                            findingsCreated.Add(parentO2Finding);
                        }
                }
                //update user on progress
                itemsProcessed++;
                if (itemsProcessed % 5000 == 0)
                    DI.log.info("applyFllter_MapSinksToAllTraces: {0} Findings created so far  [{1}/{2}]", findingsCreated.Count, itemsProcessed, numberOfItemsToProcess);
            }
            DI.log.info("applyFllter_MapSinksToAllTraces: {0} Findings created in total", findingsCreated.Count );
            return findingsCreated;
        }

        private static bool applyRuleToTrace(IO2Trace o2Trace, ICollection<IO2Trace> o2PartialTraces, IO2Finding parentO2Finding, List<IO2Finding> findingsCreated, IDictionary<string, List<IO2Rule>> indexedRules)
        {

//            if (o2Trace.signature.IndexOf("System.Data.SqlClient.SqlCommand") > -1)
//                DI.log.info(o2Trace.signature);
            var signatureToFind = MakeSignatureCompatibleWithOunceRules(o2Trace.signature);


 //           if (signatureToFind.IndexOf("System.Data.SqlClient") > -1)
 //               DI.log.info(signatureToFind);
            
            if (indexedRules.ContainsKey(signatureToFind))  // means we have a match
            {
                // rename to shouldAbortRulesCreation
                if (shouldAbortRulesExecution(indexedRules[signatureToFind]))
                {
                    if (o2Trace.traceType == TraceType.Known_Sink || o2Trace.traceType == TraceType.Lost_Sink)
                        return false;                    
                    return true;
                }
                // check if we are a sink at the root of the tree with no child nodes (and if so skip trace creation)
                if (parentO2Finding.o2Traces.Count == 0 )//; && (o2Trace.traceType == TraceType.Known_Sink || o2Trace.traceType == TraceType.Lost_Sink || o2Trace.traceType == TraceType.Root_Call))                
                    return true;                
                // check if there are no sources on the trace
                if (((O2Finding)parentO2Finding).Source == "")
                    return false;

                var newTrace = OzasmtCopy.createCopy(o2Trace, false); //create new trace (which will be modified
                newTrace.traceType = TraceType.Known_Sink; // make the trace  a sink
                o2PartialTraces.Add(newTrace); // add it to the partial trace
                
                var newFindingWithSinkTrace = OzasmtCopy.createCopy(parentO2Finding); // create template finding which will be applied the rules
                findingsCreated.AddRange(FiltersUtils.applySinkRuleToFindingAndTrace(newFindingWithSinkTrace, signatureToFind, indexedRules)); // apply rules and add resulting findings to findingsCreated list
                //remove the new trace since the invokeOnAllPartialTraces loop will add its own copy
                o2PartialTraces.Remove(newTrace);

            }            
            return true; // in this case return true since we want to process ALL traces
        }

        private static String MakeSignatureCompatibleWithOunceRules(string signatureToFix)
        {
            if (signatureToFix.IndexOf("..ctor") > -1)
            {
                var filteredSiganture = new FilteredSignature(signatureToFix);
                if (filteredSiganture.lsFunctionClass_Parsed.Count > 1)
                    signatureToFix = signatureToFix.Replace(".ctor", filteredSiganture.lsFunctionClass_Parsed[filteredSiganture.lsFunctionClass_Parsed.Count - 2]).Replace(".(", "("); ;
            }
            signatureToFix = signatureToFix.Replace("..ctor", "");
            return FilteredSignature.makeDotNetSignatureCompatibleWithOunceRules(signatureToFix);
        }

        private static bool shouldAbortRulesExecution(IEnumerable<IO2Rule> rulesToCheck)
        {
            foreach(var o2Rule in rulesToCheck)
                if(o2Rule.RuleType == O2RuleType.NotASink)
                    return true;
            return false;
        }

        public static bool invokeOnAllPartialTraces(IEnumerable<IO2Trace> o2TracesToFollow, ICollection<IO2Trace> o2PartialTraces, Func<ICollection<IO2Trace> ,IO2Trace,bool> onTraceEnter)
        {
            // process all traces
            foreach (var o2TraceToFollow in o2TracesToFollow)
            {
                // invoke onTraceEnter and only continue if true
                if (onTraceEnter(o2PartialTraces,o2TraceToFollow))
                {
                    // create a copy of the current trace
                    var newO2Trace = OzasmtCopy.createCopy(o2TraceToFollow, false);                    
                    // add it to the trace we are building
                    o2PartialTraces.Add(newO2Trace);
                    
                    if (false == invokeOnAllPartialTraces(o2TraceToFollow.childTraces, newO2Trace.childTraces, onTraceEnter))
                        return false; // means we are not supposed to continue with this trace
                }
                else
                    return false;
            }
            return true;
        }
    }
}
