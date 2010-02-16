// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.O2Findings;
using O2.Interfaces.O2Findings;
using O2.Interfaces.Rules;
using O2.Rules.OunceLabs.RulesUtils;

namespace O2.Rules.OunceLabs.Filters
{
    public class Filter_MapSourcesToAllTraces
    {
        public static List<IO2Finding> applyFilter(IEnumerable<IO2Finding> o2TargetO2Findings, List<IO2Rule> o2Rules)
        {
            var findingsCreated = new List<IO2Finding>();
            var numberOfItemsToProcess = o2TargetO2Findings.Count();
            var itemsProcessed = 0;
            // create index of Sinks
            var indexedRules = IndexedO2Rules.indexOnSources(o2Rules); // IndexedO2Rules.indexAll(o2Rules); 
            // process add findings            
            foreach (IO2Finding targetO2Finding in o2TargetO2Findings)
            {                
                // for this one we are actually going to use the current finding
                IO2Finding targetO2Finding1 = targetO2Finding;
                // start recursive loop that will invoke the Lambda callback for every node                               
                if (invokeOnAllPartialTraces(targetO2Finding.o2Traces,
                                             o2Trace => applyRuleToTrace(o2Trace, targetO2Finding1, findingsCreated, indexedRules)))
                {
             
/*                    // if invokeOnAllPartialTraces returned true (which means we didn't hit a don't propagate item) check if there was any rule created with this finding
                    if (currentNumberOfCreatedFindings == findingsCreated.Count)
                    {
                        parentO2Finding.vulnType = "Unmapped Finding"; // add it to this category
                        parentO2Finding.vulnName = ((O2Finding) parentO2Finding).Sink;
                            // this patch should be done when the joinned traces are created
                        findingsCreated.Add(parentO2Finding);
                    }*/
                }
                //update user on progress
                itemsProcessed++;
                if (itemsProcessed%5000 == 0)
                    DI.log.info("applyFllter_MapSinksToAllTraces: {0} Findings created so far  [{1}/{2}]",
                                findingsCreated.Count, itemsProcessed, numberOfItemsToProcess);
            }
            DI.log.info("applyFllter_MapSinksToAllTraces: {0} Findings created in total", findingsCreated.Count);
            return findingsCreated;
        }

        public static bool invokeOnAllPartialTraces(IEnumerable<IO2Trace> o2TracesToFollow, Func<IO2Trace, bool> onTraceEnter)
        {
            // process all traces
            foreach (var o2TraceToFollow in o2TracesToFollow)
            {
                // invoke onTraceEnter and only continue if true
                if (onTraceEnter(o2TraceToFollow))
                {                   
                    // following into all child traces
                    if (false == invokeOnAllPartialTraces(o2TraceToFollow.childTraces, onTraceEnter))
                        return false; // means we are not supposed to continue with this trace
                }
                else
                    return false;
            }
            return true;
        }


        private static bool applyRuleToTrace(IO2Trace o2Trace, IO2Finding parentO2Finding, List<IO2Finding> findingsCreated, IDictionary<string, List<IO2Rule>> indexedRules)
        {



            var signatureToFind = o2Trace.signature;
              if (indexedRules.ContainsKey(signatureToFind))  // means we have a match
              {
                  if (o2Trace.traceType == TraceType.Source)
                  {
                      // if the trace is of TraceType.Source by there is a rule with O2RuleType.NotASource we need are not going to add the current finding
                      foreach (var o2Rule in indexedRules[signatureToFind])
                          if (o2Rule.RuleType == O2RuleType.NotASource)
                              return false;
                      // since this is a Source, we can just add it and terminate the trace (this assumes that there is only one trace per finding
                      findingsCreated.Add(parentO2Finding);
                      return true;
                  }
                  // if we have a source lets add it has a new finding
                  foreach (var o2Rule in indexedRules[signatureToFind])
                      if (o2Rule.RuleType != O2RuleType.NotASource)
                      {
                          // before we copy the finding we have to sort out who is a source in this finding
                          var currentSource = ((O2Finding)parentO2Finding).getSource();
                          // make it a normal trace
                          if (currentSource !=null)
                            currentSource.traceType = TraceType.Type_4;
                          // then save the current trace trace type
                          var currentO2TraceTraceType = o2Trace.traceType;
                          // set it to Source
                          o2Trace.traceType = TraceType.Source;                          
                          // copy the whole finding and traces
                          var newSourceFinding = OzasmtCopy.createCopy(parentO2Finding);
                          // add it to the list of created findings
                          findingsCreated.Add(newSourceFinding);
                          // and restore the trace types we modified above
                        if (currentSource !=null)
                               currentSource.traceType = TraceType.Source;
                           o2Trace.traceType = currentO2TraceTraceType;
                          // all done :)
                      }

            /*      // rename to shouldAbortRulesCreation
                  if (shouldAbortRulesExecution(indexedRules[signatureToFind]))
                  {
                      if (o2Trace.traceType == TraceType.Known_Sink || o2Trace.traceType == TraceType.Lost_Sink)
                          return false;
                      return true;
                  }
                  // check if we are a sink at the root of the tree with no child nodes (and if so cancel)
                  if (parentO2Finding.o2Traces.Count == 0)//; && (o2Trace.traceType == TraceType.Known_Sink || o2Trace.traceType == TraceType.Lost_Sink || o2Trace.traceType == TraceType.Root_Call))                
                      return true;
                  // check if there are no sources on the trace
                  if (((O2Finding)parentO2Finding).Source == "")
                      return false;

                  var newTrace = OzasmtCopy.createCopy(o2Trace, false); //create new trace (which will be modified
                  newTrace.traceType = TraceType.Known_Sink; // make the trace  a sink
                  o2PartialTraces.Add(newTrace); // add it to the partial trace

                  var newFindingWithSinkTrace = OzasmtCopy.createCopy(parentO2Finding); // create template finding which will be applied the rules
                  findingsCreated.AddRange(FiltersUtils.applySinkRuleToFindingAndTrace(newFindingWithSinkTrace, o2Trace.signature, indexedRules)); // apply rules and add resulting findings to findingsCreated list
                  //remove the new trace since the invokeOnAllPartialTraces loop will add its own copy
                  o2PartialTraces.Remove(newTrace);
               */ 
              }
            return true; // in this case return true since we want to process ALL traces
        }
/*
        private static bool shouldAbortRulesExecution(IEnumerable<IO2Rule> rulesToCheck)
        {
            foreach (var o2Rule in rulesToCheck)
                if (o2Rule.RuleType == O2RuleType.NotASink)
                    return true;
            return false;
        }*/


    }
}
