// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Core.CIR.CirUtils
{
    public class CirTraces
    {
        public static string createO2AssessmentWithCallFlowTraces(ICirDataAnalysis cirDataAnalysis)
        {
            DI.log.info("Creating O2Assessment With Call Flow Traces");
            var timer = new O2Timer("Created list of finding").start();            
            var cirFunctionsToProcess = cirDataAnalysis.dCirFunction_bySignature.Values;
            var o2Findings  = createO2FindingsFromCirFunctions(cirFunctionsToProcess);
            timer.stop();
            timer = new O2Timer("Saved Assessment").start();
            var o2Assessment = new O2Assessment();
            o2Assessment.o2Findings = o2Findings;
            var savedFile = o2Assessment.save(new O2AssessmentSave_OunceV6());
            DI.log.info("Saved O2Asssessment file created: {0}", savedFile);
            timer.stop();
            return savedFile;
        }

        public static List<IO2Finding> createO2FindingsFromCirFunctions(IEnumerable <ICirFunction> cirFunctionsToProcess)
        {
            int itemsToProcess = cirFunctionsToProcess.Count();
            var o2Findings = new List<IO2Finding>();
            foreach (var cirFunction in cirFunctionsToProcess)
            {
                //HACK: in case this is a Ounce Scan generated trace, we will need  FunctionsCalledSequence = FunctionsCalledUniqueList
                if (cirFunction.FunctionsCalledUniqueList.Count > 0 && cirFunction.FunctionsCalled.Count == 0)
                    foreach (var cirFunctionCalled in cirFunction.FunctionsCalledUniqueList)
                        cirFunction.FunctionsCalled.Add(new CirFunctionCall(cirFunctionCalled));
                // now we can continue
                if (cirFunction.FunctionsCalled.Count > 0) // Don't add trace that don't call anybody
                {
                    o2Findings.Add(createO2FindingFromCirFunction(cirFunction));
                    if (o2Findings.Count % 10 == 0)
                        DI.log.info("{0} / {1}", o2Findings.Count, itemsToProcess);
                }
            }
            return o2Findings;
        }

        /// <summary>
        /// this will only return the first finding created (which will have all sinks marked) 
        /// </summary>
        /// <param name="cirFunction"></param>
        /// <returns></returns>
        public static IO2Finding createO2FindingFromCirFunction(ICirFunction cirFunction)
        {
            var createdO2Findings = createO2FindingsFromCirFunction(cirFunction, false);
            if (createdO2Findings.Count != 1)
            {
                DI.log.error(
                    "in createO2FindingsFromCirFunction, something is wrong since createdO2Findings.Count != 1, and it is: {0}",
                    createdO2Findings.Count);
                return null;
            }
            return createdO2Findings[0];
        }

        public static List<IO2Finding> createO2FindingsFromCirFunction(ICirFunction cirFunction, bool createNewFindingOnExternalCall)
        {
            var o2FindingsCreated = new List<IO2Finding>();
            var filteredSignature = new FilteredSignature(cirFunction);
            var functionSignature = filteredSignature.sSignature;
            var rootO2Finding = new O2Finding
                                    {
                                        method = cirFunction.ClassNameFunctionNameAndParameters,
                                        vulnName = functionSignature,
                                        vulnType = "O2.CirGeneratedTrace",
                                        severity = 2,
                                        confidence = 2,
                                        file= cirFunction.File                                        
                                    };
            if (cirFunction.File != null)
            {
                UInt32 lineNumber;
                if (UInt32.TryParse(cirFunction.FileLine, out lineNumber))
                    rootO2Finding.lineNumber = lineNumber;
            }
            createTracesAndFindingsFromCirFunction(cirFunction, rootO2Finding.file, rootO2Finding.lineNumber, rootO2Finding.o2Traces, new List<IO2Trace>(), rootO2Finding, o2FindingsCreated, createNewFindingOnExternalCall);
            // make the first trace a Source
            rootO2Finding.o2Traces[0].traceType = TraceType.Source;
            // and add it to the list of Findings Created
            o2FindingsCreated.Add(rootO2Finding);
            return o2FindingsCreated;
        }
        /// <summary>
        /// This will populate the parent finding with all traces from the provided ICirFunction
        /// caution: use the createNewFindingOnExternalCall carefully since it can create a stupid amount of traces (and it is much slower)
        /// </summary>
        /// <param name="cirFunction"></param>
        /// <param name="lineNumber"></param>
        /// <param name="o2Traces"></param>
        /// <param name="parentTraces"></param>
        /// <param name="rootO2Finding"></param>
        /// <param name="o2FindingsCreated"></param>
        /// <param name="createNewFindingOnExternalCall"></param>
        /// <param name="fileName"></param>
        public static void createTracesAndFindingsFromCirFunction(ICirFunction cirFunction, string fileName, UInt32 lineNumber,List<IO2Trace> o2Traces, List<IO2Trace> parentTraces, IO2Finding rootO2Finding, List<IO2Finding> o2FindingsCreated, bool createNewFindingOnExternalCall)
        {
            int maxParentDepth = 10; //30; //10;
            var maxNumberOfTraces = 20; //50; //300; //50
            var filteredSignature = new FilteredSignature(cirFunction);
            var functionSignature = filteredSignature.sSignature;

            var o2Trace = new O2Trace(functionSignature, cirFunction.ClassNameFunctionNameAndParameters)
                              {
                                  file = fileName,
                                  lineNumber = lineNumber
                              };

            // add file references

            // handle the case where this is a recursive call or a call to a method already added in the current tree
            var recursiveCall = false;
            foreach(var o2ParentTrace in parentTraces)
                if (o2ParentTrace.signature == functionSignature)
                {
                    recursiveCall = true;
                    break;
                }
            parentTraces.Add(o2Trace);
            // add this trace to the current trace tree (since we might need to create a copy of it below
            o2Traces.Add(o2Trace);
            if (recursiveCall)
            {
                var nodeText = String.Format("{0} : {1} : {2}", cirFunction, "....(Recursive Call so not expanding child traces", functionSignature);
                o2Trace.childTraces.Add(new O2Trace(nodeText));
            }
            else
            {
                if (parentTraces.Count > maxParentDepth)
                    o2Trace.childTraces.Add(new O2Trace(" ... {Max trace depth reached} (" + maxParentDepth + ")"));
                else
                {
                    // 
                    var numberOfTraces = OzasmtUtils.getAllTraces(rootO2Finding.o2Traces);                    
                    if (numberOfTraces.Count > maxNumberOfTraces)
                    {
                        o2Trace.childTraces.Add(new O2Trace("**** Max number of traces reached(" + maxNumberOfTraces + ") aborting trace execution"));
                        return; 
                    }               

                    if (cirFunction.FunctionsCalled.Count == 0) // means we don't have the code for this one, so 
                    {                   
                        // let make it a lost sink
                        var originalTraceTypeValue = o2Trace.traceType;     // we might need this below
                        o2Trace.traceType = TraceType.Lost_Sink;
                        if (createNewFindingOnExternalCall)   // and if createNewFindingOnExternalCall add it as finding                           
                        {
                            // create a copy of the parent finding (which incudes the above trace
                            var newFinding = OzasmtCopy.createCopy(rootO2Finding);
                            // make the first call a source (so that we have a source-> pair
                            newFinding.o2Traces[0].traceType = TraceType.Source;
                            // add it 
                            o2FindingsCreated.Add(newFinding);
                            // since the crawl will continue we must restore the originalTraceTypeValue
                            o2Trace.traceType = originalTraceTypeValue;
                        }
                    }
                    else
                        foreach (var functionCalled in cirFunction.FunctionsCalled)
                            createTracesAndFindingsFromCirFunction(functionCalled.cirFunction, functionCalled.fileName, (UInt32)functionCalled.lineNumber, o2Trace.childTraces, parentTraces, rootO2Finding, o2FindingsCreated, createNewFindingOnExternalCall);
                }
            }
            
            // now remove the signature since we are only interrested in non repeats on the same parent
            parentTraces.Remove(o2Trace);
        }
    }
}
