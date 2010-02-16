// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.O2Findings;
using O2.Interfaces.O2Findings;

namespace O2.UnitTests.Test_Ozasmt
{
    public class OzasmtTestHelpers
    {
        public static O2Finding CreateFinding_WithNoTrace()
        {
            return new O2Finding("vulnName.Testing.TraceCreation_#1", "vulnType.CustomType_#1",
                                           "This is the Context_#1",
                                           "This is the caller_#1");
        }

        public static O2Finding CreateFinding_WithTrace()
        {
            const uint line_number = 2;
            const uint column_number = 3;
            const uint ordinal = 1;
            const string context = "TraceContext";
            const string signature = "TraceSignature";
            const string clazz = "class.this.trace.is.in";
            const string file = @"c:\o2\temp\file\trace\is\in.cs";
            const string method = "methodExectuted";
            const uint taintPropagation = 0;
            var text = new List<string> {"this is a text inside a trace"};



            var o2Finding = new O2Finding("Vulnerability.Name", "Vulnerability.Type");

            o2Finding.o2Traces.Add(new O2Trace
                                        {
                                            clazz = clazz,
                                            columnNumber = column_number,
                                            context = context,
                                            file = file,
                                            lineNumber = line_number,
                                            method = method,
                                            ordinal = ordinal,
                                            signature = signature,
                                            taintPropagation = taintPropagation,
                                            text = text,
                                        });

            
            const string sinkText = "this is a sink";
            const string methodOnSinkPath = "method call on sink path";
            const string methodOnSourcePath = "method call on source path";
            const string sourceText = "this is a source";

            
            var o2Trace = new O2Trace("Class.Signature", "Method executed");

            var o2TraceOnSinkPath = new O2Trace(methodOnSinkPath, TraceType.Type_0);
            o2TraceOnSinkPath.childTraces.Add(new O2Trace(sinkText, TraceType.Known_Sink));

            var o2TraceOnSourcePath = new O2Trace(methodOnSourcePath, TraceType.Type_0);
            o2TraceOnSourcePath.childTraces.Add(new O2Trace(sourceText, TraceType.Source));

            o2Trace.childTraces.Add(o2TraceOnSourcePath);

            o2Trace.childTraces.Add(o2TraceOnSinkPath);

            o2Finding.o2Traces = new List<IO2Trace> {o2Trace};

            return o2Finding;

        }

        public static O2Assessment createO2Assessment()
        {
            var o2Assessment = new O2Assessment();
            o2Assessment.o2Findings.Add(CreateFinding_WithTrace());
            o2Assessment.o2Findings.Add(CreateFinding_WithNoTrace());
            return o2Assessment;        
        }

   /*     public static string createO2AssessmentFile()
        {
            var tempO2AssessmentFile = DI.config.getTempFileInTempDirectory("ozasmt");
            createO2AssessmentFile(tempO2AssessmentFile);
            return tempO2AssessmentFile;
            
        }

        public static void createO2AssessmentFile(string fileName)
        {
            createO2Assessment().save(fileName);            
        }*/
    }
}
