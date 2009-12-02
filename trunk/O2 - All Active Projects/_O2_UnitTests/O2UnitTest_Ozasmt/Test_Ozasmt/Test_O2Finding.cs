using System.Collections.Generic;
using NUnit.Framework;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.UnitTests.Test_Ozasmt.Test_Ozasmt
{
    [TestFixture]
    public class Test_O2Finding
    {
        public IO2AssessmentLoad o2AssessmentLoad = new O2AssessmentLoad_OunceV6();
        public IO2AssessmentSave o2AssessmentSave = new O2AssessmentSave_OunceV6();

        [Test]
        public void CreateFinding()
        {
            string sFileToCreate = DI.config.TempFileNameInTempDirectory;
            const string file = @"c:\O2\Temp\testFile.cs";
            const uint record_id = 1;
            const uint line_number = 2;
            const uint column_number = 3;
            const uint actionobject_id = 4;
            const byte severity = 3;
            const byte confidence = 2;
            const bool exclude = false;
            const uint ordinal = 1;
            const string context = "context";
            const string vuln_name = "vuln_name";
            const string caller_name = "caller_name";
            const string vuln_type = "vuln_type";
            const string project_name = "project_name";
            const string property_ids = "property_ids";
            var o2Assessment = new O2Assessment();
            // create test O2Finding objects
            var o2Finding1 = new O2Finding
                                 {
                                     actionObject = actionobject_id,
                                     confidence = confidence,
                                     file = file,
                                     columnNumber = column_number,
                                     exclude = exclude,
                                     lineNumber = line_number,
                                     ordinal = ordinal,
                                     recordId = record_id,
                                     severity = severity,
                                     context = context,
                                     vulnName = vuln_name,
                                     callerName = caller_name,
                                     vulnType = vuln_type,
                                     projectName = project_name,
                                     propertyIds = property_ids
                                 };
            var o2Finding2 = new O2Finding(vuln_name, vuln_type, context, caller_name);

            // add O2Findings and saved assessment run
            o2Assessment.o2Findings.Add(o2Finding1);
            o2Assessment.o2Findings.Add(o2Finding2);
            o2Assessment.save(o2AssessmentSave, sFileToCreate);

            // check that file created is ok
            var loadedO2Assessment = new O2Assessment(o2AssessmentLoad, sFileToCreate);
            Assert.IsTrue(loadedO2Assessment.o2Findings.Count == 2, "There should be 2 findings saved");
            IO2Finding loadedO2Fiding = loadedO2Assessment.o2Findings[0];
            Assert.IsTrue(loadedO2Fiding.actionObject == actionobject_id, "actionobject_id");
            Assert.IsTrue(loadedO2Fiding.confidence == confidence, "confidence");
            Assert.IsTrue(loadedO2Fiding.file == file, "file");
            Assert.IsTrue(loadedO2Fiding.columnNumber == column_number, "column_number");
            Assert.IsTrue(loadedO2Fiding.exclude == exclude, "exclude");
            Assert.IsTrue(loadedO2Fiding.lineNumber == line_number, "line_number");
            Assert.IsTrue(loadedO2Fiding.ordinal == ordinal, "ordinal");
            Assert.IsTrue(loadedO2Fiding.recordId == record_id, "record_id");
            Assert.IsTrue(loadedO2Fiding.severity == severity, "severity");
            Assert.IsTrue(loadedO2Fiding.context == context, "context");
            Assert.IsTrue(loadedO2Fiding.vulnName == vuln_name, "vuln_name");
            Assert.IsTrue(loadedO2Fiding.callerName == caller_name, "caller_name");
            Assert.IsTrue(loadedO2Fiding.vulnType == vuln_type, "vuln_type");
            Assert.IsTrue(loadedO2Fiding.projectName == project_name, "project_name");
            Assert.IsTrue(loadedO2Fiding.propertyIds == property_ids, "property_ids");
        }

        [Test]
        public void CreateFinding_WithTrace()
        {
            string sFileToCreate = DI.config.TempFileNameInTempDirectory;
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

            var o2Assessment = new O2Assessment();
            // Finding #1
            var o2Finding1 = new O2Finding("vulnName.Testing.TraceCreation", "vulnType.CustomType",
                                           "This is the Context",
                                           "This is the caller");
            o2Finding1.o2Traces.Add(new O2Trace
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
            o2Assessment.o2Findings.Add(o2Finding1);

            // Finding #1
            const string sinkText = "this is a sink";
            const string methodOnSinkPath = "method call on sink path";
            const string methodOnSourcePath = "method call on source path";
            const string sourceText = "this is a source";
            var o2Finding2 = new O2Finding("Vulnerability.Name", "Vulnerability.Type");

            var o2Trace = new O2Trace("Class.Signature", "Method executed");

            var o2TraceOnSinkPath = new O2Trace(methodOnSinkPath, TraceType.Type_0);
            o2TraceOnSinkPath.childTraces.Add(new O2Trace(sinkText, TraceType.Known_Sink));

            var o2TraceOnSourcePath = new O2Trace(methodOnSourcePath, TraceType.Type_0);
            o2TraceOnSourcePath.childTraces.Add(new O2Trace(sourceText, TraceType.Source));

            o2Trace.childTraces.Add(o2TraceOnSourcePath);

            o2Trace.childTraces.Add(o2TraceOnSinkPath);

            o2Finding2.o2Traces = new List<IO2Trace> {o2Trace};

            o2Assessment.o2Findings.Add(o2Finding2);

            // save assessment file
            o2Assessment.save(o2AssessmentSave, sFileToCreate);

            // check if data was saved correctly 
            var loadedO2Assessment = new O2Assessment(o2AssessmentLoad, sFileToCreate);

            List<IO2Finding> loadedO2Findings = loadedO2Assessment.o2Findings;
            Assert.IsTrue(loadedO2Assessment.o2Findings.Count == 2, "There should be 2 findings in the Assessment File");

            // in o2Findings1
            Assert.IsTrue(loadedO2Assessment.o2Findings[0].o2Traces.Count == 1,
                          "There should be 1 Trace in the Finding #1");

            IO2Trace loadedO2Trace = loadedO2Findings[0].o2Traces[0];
            Assert.IsTrue(loadedO2Trace.clazz == clazz, "clazz");
            Assert.IsTrue(loadedO2Trace.columnNumber == column_number, "columnNumber");
            Assert.IsTrue(loadedO2Trace.context == context, "context");
            Assert.IsTrue(loadedO2Trace.file == file, "file");
            Assert.IsTrue(loadedO2Trace.lineNumber == line_number, "lineNumber");
            Assert.IsTrue(loadedO2Trace.method == method, "method");
            Assert.IsTrue(loadedO2Trace.ordinal == ordinal, "ordinal");
            Assert.IsTrue(loadedO2Trace.signature == signature, "signature");
            Assert.IsTrue(loadedO2Trace.taintPropagation == taintPropagation, "taintPropagation");
            Assert.IsTrue(loadedO2Trace.text[0] == text[0], "text");

            // in o2Findings2
            Assert.IsTrue(loadedO2Assessment.o2Findings[1].o2Traces.Count == 1,
                          "There should be 1 Trace in the Finding #2");
            Assert.IsTrue(loadedO2Assessment.o2Findings[1].o2Traces[0].childTraces.Count == 2,
                          "There should be 2 child traces in this trace");

            Assert.IsNotNull(OzasmtUtils.getKnownSink(loadedO2Assessment.o2Findings[1].o2Traces), "Could not find Sink");
            Assert.IsTrue(OzasmtUtils.getKnownSink(loadedO2Assessment.o2Findings[1].o2Traces).clazz == sinkText,
                          "Sink text didn't match");

            Assert.IsTrue(OzasmtUtils.getSource(loadedO2Assessment.o2Findings[1].o2Traces).clazz == sourceText,
                          "Source text didn't match");
        }

        [Test]
        public void FindingConstructors()
        {
            var o2Finding1 = new O2Finding();
            Assert.That(o2Finding1.vulnName == "", "vulnName was not empty");
            Assert.That(o2Finding1.vulnType == "", "vulnType was not empty");
            Assert.That(o2Finding1.context == "", "context was not empty");
            Assert.That(o2Finding1.callerName == "", "callerName was not empty");
            Assert.That(o2Finding1.exclude == false, "exclude was not false");
            var vulnName2 = "test vulnName2";
            var vulnType2 = "test vulnType2";
            var o2Finding2 = new O2Finding(vulnName2, vulnType2);
            Assert.That(o2Finding2.vulnName == vulnName2, "vulnName2 was not assigned value");
            Assert.That(o2Finding2.vulnType == vulnType2, "vulnType2 was not assigned value");
            Assert.That(o2Finding1.callerName == "", "callerName was not empty");
            Assert.That(o2Finding2.context == "", "context was not empty");
            Assert.That(o2Finding2.exclude == false, "exclude was not false");
            var vulnName3 = "test vulnName3";
            var vulnType3 = "test vulnType3";
            var context3 = "test context3";
            var callerName3 = "test vulnType3";
            var o2Finding3 = new O2Finding(vulnName3, vulnType3, context3, callerName3);
            Assert.That(o2Finding3.vulnName == vulnName3, "vulnName3 was not assigned value");
            Assert.That(o2Finding3.vulnType == vulnType3, "vulnType3 was not assigned value");
            Assert.That(o2Finding3.context == context3, "context3 was not assigned value");
            Assert.That(o2Finding3.callerName == callerName3, "callerName3 was not assigned value");
            Assert.That(o2Finding3.exclude == false, "exclude was not false");
        }
    }
}