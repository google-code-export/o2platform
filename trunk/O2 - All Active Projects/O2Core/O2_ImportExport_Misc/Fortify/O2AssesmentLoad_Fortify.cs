// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.ImportExport.Misc.Fortify
{
    public class O2AssesmentLoad_Fortify :  IO2AssessmentLoad
    {
        public string engineName { get; set; }
        public O2AssesmentLoad_Fortify()
        {
            engineName = "O2AssesmentLoad_Fortify";
        }

        public bool canLoadFile(string fileToTryToLoad)
        {
            return Path.GetExtension(fileToTryToLoad) == ".fvdl";
        }

        public IO2Assessment loadFile(string fileToLoad)
        {
            var fortifyObject = Serialize.getDeSerializedObjectFromXmlFile(fileToLoad, typeof(Xsd.FVDL));
            if (fortifyObject == null || false == fortifyObject is Xsd.FVDL)
                return null;
            return createO2AssessmentFromFortifyObject((Xsd.FVDL)fortifyObject, Path.GetFileNameWithoutExtension(fileToLoad));            
        }

        private IO2Assessment createO2AssessmentFromFortifyObject(Xsd.FVDL fortifyObject, String fileName)
        {            
            var o2Assessment = new O2Assessment();
            o2Assessment.name = "Fortify Import of: " + fileName;
            foreach (var vulnerability in fortifyObject.Vulnerabilities)
            {
                /* foreach(var threat in codeCrawlerObject.ThreatList)*/
                try
                {
                    var o2Finding = new O2Finding
                                        {
                                            vulnName = (vulnerability.AnalysisInfo.Local != null) ?
                                                vulnerability.AnalysisInfo.Local.SourceRef.FunctionCall.Function.name :
                                                "vulnerability.AnalysisInfo.Local was NULL",
                                            vulnType = vulnerability.ClassInfo.Type,
                                            //context = vulnerability.ClassInfo.
                                            severity = (byte) vulnerability.InstanceInfo.Confidence,
                                            confidence = (byte) vulnerability.InstanceInfo.InstanceSeverity
                                            //lineNumber = .
                                            //file = fileName
                                        };
                    //                foreach(var ads in vulnerability.AnalysisInfo.Dataflow)
                    //                o2Finding.text.Add(threat.Description);
                    try
                    {
                        if (vulnerability.AnalysisInfo.Dataflow != null)
                        {
                            if (vulnerability.AnalysisInfo.Dataflow.Source != null)
                            {
                                var sourceTrace = new O2Trace();
                                sourceTrace.signature = vulnerability.AnalysisInfo.Dataflow.Source.Context.Function.name;
                                if (vulnerability.AnalysisInfo.Dataflow.Source.SourceRef.FunctionCall != null)
                                    sourceTrace.signature += ".." + vulnerability.AnalysisInfo.Dataflow.Source.SourceRef.FunctionCall.Function.name;
                                if (vulnerability.AnalysisInfo.Dataflow.Source.SourceRef.FunctionEntry != null)
                                    sourceTrace.signature += ".:." + vulnerability.AnalysisInfo.Dataflow.Source.SourceRef.FunctionEntry.Function.name;
                                sourceTrace.traceType = TraceType.Source;
                                o2Finding.o2Traces.Add(sourceTrace);
                            }
                            foreach (var path in vulnerability.AnalysisInfo.Dataflow.Path)
                            {
                                IO2Trace o2Trace = null;
                                //if (path.Context != null && path.Context.Function != null)
                                o2Trace = new O2Trace(path.Context.Function.name);
                                if (path.SourceRef != null)
                                    if (path.SourceRef.Statement != null)
                                    {
                                        //     o2Trace = new O2Trace(path.SourceRef.Statement.type);
                                        //     
                                    }
                                    else if (path.SourceRef.FunctionCall != null)
                                    {
                                        o2Trace.signature += ".." + path.SourceRef.FunctionCall.Function.name;
                                    }
                                    else
                                    {
                                    }
                                if (o2Trace != null)
                                    o2Finding.o2Traces.Add(o2Trace);
                                //   o2Trace
                            }
                            if (vulnerability.AnalysisInfo.Dataflow.Sink != null)
                            {
                                var sinkTrace = new O2Trace();
                                sinkTrace.signature = vulnerability.AnalysisInfo.Dataflow.Sink.Context.Function.name;
                                if (vulnerability.AnalysisInfo.Dataflow.Sink.SourceRef.FunctionCall != null)
                                    sinkTrace.signature += ".." + vulnerability.AnalysisInfo.Dataflow.Sink.SourceRef.FunctionCall.Function.name;                                
                                sinkTrace.traceType = TraceType.Known_Sink;
                                o2Finding.o2Traces.Add(sinkTrace);
                                o2Finding.vulnName = sinkTrace.signature;
                            }
                        }
                    } catch (Exception ex)
                    {
                        DI.log.ex(ex, "in createO2AssessmentFromFortifyObject - add trace");
                    }
                    o2Assessment.o2Findings.Add(o2Finding);
                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in createO2AssessmentFromFortifyObject - add finding");
                }
            }
            return o2Assessment;
        }

        public bool importFile(string fileToLoad, IO2Assessment o2Assessment)
        {
            var loadedO2Assessment = loadFile(fileToLoad);
            if (loadedO2Assessment != null)
            {
                o2Assessment.o2Findings = loadedO2Assessment.o2Findings;
                return true;
            }
            return false;
        }

    }
}
