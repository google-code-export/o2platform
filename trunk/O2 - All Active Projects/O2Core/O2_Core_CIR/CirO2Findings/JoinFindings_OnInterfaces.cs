using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.CIR;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Core.CIR.CirO2Findings
{
    public class JoinFindings_OnInterfaces
    {
        public static List<IO2Finding> mapInterfaces(List<IO2Finding> o2Findings, List<IO2Finding> sourcesMappedToInterfaces, ICirData cirData)
        {
            if (o2Findings == null || cirData == null)
                return null;
            //var sourcesMappedToInterfaces = getSourcesMappedToInterfaces(o2Findings, cirData);
            var sources = getSources(sourcesMappedToInterfaces);
            var lostSinks = getLostSinks(o2Findings);//o2Assessment.o2Findings);
            //var sources = getSources(o2Assessment.o2Findings);

            DI.log.info("There are {0} unique Lost Sinks", lostSinks.Count);
            DI.log.info("There are {0} unique Sources", sources.Count);
            var joinedFindings = mapSinksToSources(lostSinks, sources);
            return joinedFindings;
        }

        public static List<IO2Finding> mapSinksToSources(Dictionary<string, List<IO2Finding>> lostSinks, Dictionary<string, List<IO2Finding>> sources)
        {
            DI.log.info("Looking for matches");
            var results = new List<IO2Finding>();
            foreach (string lostSinkSignature in lostSinks.Keys)
                if (sources.ContainsKey(lostSinkSignature))
                {
                    foreach (O2Finding lostSink in lostSinks[lostSinkSignature])
                        foreach (O2Finding source in sources[lostSinkSignature])
                        {
                            var sinkCopy = (O2Finding)OzasmtCopy.createCopy(lostSink);
                            var sourceCopy = (O2Finding)OzasmtCopy.createCopy(source);

                            var lostSinkNode = sinkCopy.getLostSink();
                            lostSinkNode.traceType = TraceType.Type_4;

                            var interfaceJoinPoint = new O2Trace("---- Interface join --- ");
                            interfaceJoinPoint.childTraces.AddRange(sourceCopy.o2Traces);
                            lostSinkNode.childTraces.Add(interfaceJoinPoint);

                            sinkCopy.vulnName = sinkCopy.Sink;
                            results.Add(sinkCopy);
                            //return results;
                        }
                }
            //log.info("We have a match on: {0}", lostSink);
            return results;
        }

        public static List<IO2Finding> getSourcesMappedToInterfaces(List<IO2Finding> o2Findings, ICirData cirData)
        {
            var results = new List<IO2Finding>();
            if (cirData == null || o2Findings == null)
                return results;
            foreach (O2Finding o2Finding in o2Findings)
            {
                var source = o2Finding.getSource();
                if (source != null)
                {
                    if (cirData.dFunctions_bySignature.ContainsKey(source.signature))
                    {
                        var cirFunction = cirData.dFunctions_bySignature[source.signature];
                        var cirClass = cirFunction.ParentClass;
                        if (cirClass.dSuperClasses.Count > 0)
                        {
                            var sourceSig = cirFunction.FunctionSignature.Substring(cirClass.Signature.Length);
                            //log.info(cirFunction.FunctionSignature);


                            foreach (var superClass in cirClass.dSuperClasses.Values)
                            {
                                foreach (var function in superClass.dFunctions.Values)
                                {
                                    var interfaceName = function.FunctionSignature.Substring(superClass.Signature.Length);
                                    if (sourceSig == interfaceName)
                                    {
                                        // we have an implementation of a method:  
                                        var newO2Finding = OzasmtCopy.createCopy(o2Finding);

                                        var currentSource = ((O2Finding)newO2Finding).getSource();
                                        currentSource.traceType = TraceType.Type_4;
                                        currentSource.signature = "[old source]:   " + currentSource.signature;

                                        var interfaceTrace = new O2Trace(function.FunctionSignature);

                                        interfaceTrace.traceType = TraceType.Source;

                                        //interfaceTrace.childTraces.Add(new O2Trace("[interface function Signature (that matched)]:  " + sourceSig));
                                        //interfaceTrace.childTraces.Add(new O2Trace("interface: " + interfaceName));
                                        //interfaceTrace.childTraces.Add(new O2Trace("source: " + currentSource.signature));
                                        //interfaceTrace.childTraces.Add(new O2Trace("interface: " + function.FunctionSignature));
                                        //interfaceTrace.childTraces.AddRange(newO2Finding.o2Traces);
                                        interfaceTrace.childTraces.AddRange(currentSource.childTraces);

                                        newO2Finding.o2Traces = new List<IO2Trace>();
                                        newO2Finding.o2Traces.Add(interfaceTrace);

                                        results.Add(newO2Finding);
                                        //return results;
                                    }
                                }
                            }

                        }
                        //log.info(" {2}  :  {0} -> {1}", cirFunction.FunctionSignature, cirClass.Name, cirClass.dSuperClasses.Count);
                    }
                }
            }
            DI.log.info("There were {0} sources mapped to its interfaces (i.e. superclasses))", results.Count);
            return results;
        }

        public static Dictionary<string, List<IO2Finding>> getLostSinks(List<IO2Finding> o2Findings)
        {
            var results = new Dictionary<string, List<IO2Finding>>();
            foreach (O2Finding o2Finding in o2Findings)
            {
                var lostSink = o2Finding.getLostSink();
                if (lostSink != null)
                {
                    if (false == results.ContainsKey(lostSink.signature))
                        results.Add(lostSink.signature, new List<IO2Finding>());
                    results[lostSink.signature].Add(o2Finding);
                }
            }
            return results;
        }

        public static Dictionary<string, List<IO2Finding>> getSources(List<IO2Finding> o2Findings)
        {
            var results = new Dictionary<string, List<IO2Finding>>();
            foreach (O2Finding o2Finding in o2Findings)
            {
                var source = o2Finding.getSource();
                if (source != null)
                {
                    if (false == results.ContainsKey(source.signature))
                        results.Add(source.signature, new List<IO2Finding>());
                    results[source.signature].Add(o2Finding);
                }
            }
            return results;
        }
    }
}
