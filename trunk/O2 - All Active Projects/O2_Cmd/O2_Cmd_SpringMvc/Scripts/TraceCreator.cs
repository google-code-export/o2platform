// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2CmdShell;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.Cmd.SpringMvc.Scripts
{
    public class TraceCreator
    {
        public static bool saveTempFindingsAsSeparateAssessmentFiles = false;

        public static void test()
        {
            O2Cmd.log.write("This is a test");
        }

        public static string generateJspTraces(string ozasmtFileToLoad)
        {
            var o2Assessment = loadOzasmt(ozasmtFileToLoad);
            return generateJspTraces(o2Assessment.o2Findings, DI.config.getTempFolderInTempDirectory("_"));
        }
        public static string generateJspTraces(List<IO2Finding> o2Findings, string targetFolder)
        {
            var findingsWithMapPut = getFindingsWithSink(o2Findings,"java.util.Map.put(java.lang.Object;java.lang.Object):java.lang.Object");
            findingsWithMapPut.AddRange(getFindingsWithSink(o2Findings, "org.springframework.ui.Model.addAttribute(java.lang.String;java.lang.Object):org.springframework.ui.Model"));
            modifySinksForFindingsWithMapPut(findingsWithMapPut, "java.util.Map.", "java.util.Map.put ( \"");
            modifySinksForFindingsWithMapPut(findingsWithMapPut, "java.util.Map.", "org.springframework.ui.Model.addAttribute ( \"");

            var typeIITraces = getTypeIITraces(o2Findings);
            var findingsWithJspEl = getJspELStatements(typeIITraces);
            createJspELTraces(findingsWithJspEl);

            if (saveTempFindingsAsSeparateAssessmentFiles)
            {
                var pathToNewOzasmtFile = Path.Combine(targetFolder, "findingsWithMapPut.ozasmt");
                saveFindingsAsNewOzasmtFile("findingsWithMapPut", findingsWithMapPut, pathToNewOzasmtFile);

                pathToNewOzasmtFile = Path.Combine(targetFolder, "findingsWithJspEl.ozasmt");
                saveFindingsAsNewOzasmtFile("findingsWithJspEl", findingsWithJspEl, pathToNewOzasmtFile);
            }
            // join traces
            var joinedFindings = joinTraces(findingsWithMapPut, findingsWithJspEl);
            
            // saved it
            var pathToJoinedFindingsOzasmtFile = Path.Combine(targetFolder, "joinedFindings.ozasmt");
            saveFindingsAsNewOzasmtFile("joinedFindings", joinedFindings, pathToJoinedFindingsOzasmtFile);
            return pathToJoinedFindingsOzasmtFile;
        }        

        public static List<IO2Finding> joinTraces(List<IO2Finding> findingsToJoinOnSinks, List<IO2Finding> findingsToJoinOnSources)
        {
            var gluedFindingVulnName = "Spring Mvc Glued finding";
            return OzasmtGlue.glueOnSinkToAproximateSourceNameMatch(findingsToJoinOnSinks, findingsToJoinOnSources, gluedFindingVulnName);
        }


        public static IO2Assessment loadOzasmt(string ozasmtFileToLoad)
        {
            if (File.Exists(ozasmtFileToLoad))
            {
                var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
                O2Cmd.log.write("The Ozasmt file loaded has: {0} findings", o2Assessment.o2Findings.Count);
                return o2Assessment;
            }
            return null;
        }

        public static List<IO2Finding> getFindingsWithSink(List<IO2Finding> o2Findings, string sinkToFind)
        {
            var results = from O2Finding o2Finding in o2Findings
                          where o2Finding.Sink == sinkToFind 
                          select (IO2Finding)o2Finding;
            O2Cmd.log.write("There are {0} finding with Sink == {1}", results.Count(), sinkToFind);
            return results.ToList();
        }
        
        private static void modifySinksForFindingsWithMapPut(IEnumerable<IO2Finding> findingsWithMapPut, string newSinkValue, string stringToFind)
        {
            foreach (O2Finding o2Finding in findingsWithMapPut)
            {
                var extratedValue = extractMapPutKey(o2Finding.context, stringToFind);
                if (extratedValue != o2Finding.context)
                {
                    o2Finding.Sink = newSinkValue + extratedValue;

                    // make the vulnName of this finding the modified value of the sink
                    o2Finding.vulnName = newSinkValue + extratedValue;
                }
            }
        }

        private static string extractMapPutKey(string contextString , string stringToFind)
        {            
            var indexOfJavaUtilMap = contextString.IndexOf(stringToFind);
            if (indexOfJavaUtilMap == -1)
                return contextString;            
            indexOfJavaUtilMap += stringToFind.Length;
            var result = contextString.Substring(indexOfJavaUtilMap);
            var indexOfQuote = result.IndexOf('"');
            if (indexOfQuote == -1)
                return result;
            return result.Substring(0, indexOfQuote);
        }

        public static List<IO2Finding> getTypeIITraces(List<IO2Finding> o2Findings)
        {
            var typeIITraces = new List<IO2Finding>();
            foreach (var o2Finding in o2Findings)
                if (o2Finding.o2Traces.Count ==0)
                    typeIITraces.Add(o2Finding);
            O2Cmd.log.write("There are {0} Type II traces", typeIITraces.Count);
            return typeIITraces;                            
        }

        public static List<IO2Finding> getJspELStatements(List<IO2Finding> o2Findings)
        {
            var findingsWithJspEl = new List<IO2Finding>();            
            foreach (var o2Finding in o2Findings)
                if (RegEx.findStringInString(o2Finding.context,@"\${.*}"))                
                    findingsWithJspEl.Add(o2Finding);                   
            O2Cmd.log.write("There are {0} fidings with JSP EL", findingsWithJspEl.Count);
            return findingsWithJspEl;
        }

        public static void saveFindingsAsNewOzasmtFile(string assessmentName, List<IO2Finding> o2Findings, string pathToNewOzasmtFile)
        {
            var o2Assessment = new O2Assessment
                                   {
                                       name = assessmentName, 
                                       o2Findings = o2Findings
                                   };
            if (o2Assessment.save(new O2AssessmentSave_OunceV6(), pathToNewOzasmtFile))
                O2Cmd.log.write("Ozasmt file created with {0} findings: {1}", o2Findings.Count, pathToNewOzasmtFile);
        }

        public static void createJspELTraces(List<IO2Finding> o2Findings)
        {
            foreach (var o2Finding in o2Findings)
            {
                // root trace            
                var rootTrace = new O2Trace(getRootNodeStringValue(o2Finding.file));

                var jspElValue = extractJspElValue(o2Finding.context);
                // trace with JSP EL value
                var jspElTrace = new O2Trace(jspElValue);
                jspElTrace.traceType = TraceType.Source;
                rootTrace.childTraces.Add(jspElTrace); // add jspElTrace as a child trace of rootTrace

                //sink trace 
                var sinkTrace = new O2Trace(jspElValue);        // also map this to the JSPEl value since that is the one we will want to filter by
                sinkTrace.traceType = TraceType.Known_Sink;
                jspElTrace.childTraces.Add(sinkTrace); // add sinkTrace as a child trace of jspElTrace

                // add the vulnName (println in most cases) as an extrasink trace 
                var extraSinkTrace = new O2Trace(o2Finding.vulnName);                
                sinkTrace.childTraces.Add(extraSinkTrace); // add extraSinkTrace as a child trace of sinkTrace

                // add root trace to finding's traces colection
                o2Finding.o2Traces.Add(rootTrace);

                // add source code mappings to all traces:
                rootTrace.file = jspElTrace.file = sinkTrace.file = o2Finding.file;
                rootTrace.lineNumber = jspElTrace.lineNumber = sinkTrace.lineNumber = o2Finding.lineNumber;
            }
        }
        
        private static string extractJspElValue(string contextValue)
        {
            var indexOfElStart = contextValue.IndexOf("${");
            var indexOfElEnd = contextValue.LastIndexOf("}");
            if (indexOfElStart == -1 || indexOfElEnd == -1 || indexOfElEnd < indexOfElStart) 
                return contextValue;
            indexOfElStart += 2;
            return "java.util.Map." + contextValue.Substring(indexOfElStart, indexOfElEnd - indexOfElStart);
        }

        private static string getRootNodeStringValue(string file)
        {
            var keyString = @"\WEB-INF";
            var indexOfKeyString = file.IndexOf(keyString);
            if (indexOfKeyString == -1)
                return file;
            return file.Substring(indexOfKeyString);
        }

        

        #region additional code samples
        // using 'foreach' analysis mode
    /*    public static void listUniqueLostSinks(string ozasmtFileToLoad)
        {
            var uniqueLostSinks = new List<String>();
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
            foreach(O2Finding o2Finding in o2Assessment.o2Findings)
            {
                var lostSinkValue = o2Finding.LostSink;
                if (lostSinkValue!= "" && false == uniqueLostSinks.Contains(lostSinkValue)) 
                    uniqueLostSinks.Add(lostSinkValue);
            }
            O2Cmd.log.write("There are {0} unique Lost Sinks in the loaded assessment file", uniqueLostSinks.Count);
            foreach (string lostSink in uniqueLostSinks)
                O2Cmd.log.write("   {0}", lostSink);
        }*/

        // using LINQ
        /*public static void listUniqueLostSinks(string ozasmtFileToLoad)
        {
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);

            var uniqueLostSinks =(from O2Finding o2Finding in o2Assessment.o2Findings select o2Finding.LostSink).Distinct();                            

            O2Cmd.log.write("There are {0} unique Lost Sinks in the loaded assessment file", uniqueLostSinks.Count());
            foreach (string lostSink in uniqueLostSinks)
                O2Cmd.log.write("   {0}", lostSink);
        }*/
    #endregion additional code samples
    }
}
