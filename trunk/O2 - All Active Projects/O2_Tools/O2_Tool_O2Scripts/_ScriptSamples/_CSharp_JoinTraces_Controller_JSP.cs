using System.Collections.Generic;
using System.Linq;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.O2Scripts._ScriptSamples
{
    public class CSharp_JoinTraces_Controller_JSP
    {
        public static IO2Log log = O2.Kernel.PublicDI.log;

        public static string ozasmtFileToLoad = @"E:\O2\Demodata\_petClinic_Scan_CurrentRules.ozasmt";

        /*public static void loadAssessmentFileAndShowAllFindings()
        {
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
            ascx_FindingsViewer.openInFloatWindow(o2Assessment.o2Findings);
        }*/


        public static void joinTraces()
        {
            var sinkFindings = new List<IO2Finding>();
            var sourceFindings = new List<IO2Finding>();

            findTracesToJoin(sinkFindings, sourceFindings);

            fixSinkVulnNamesBasedOnSinkContextHashMapKey("Findings_With_HashMap_To_Join_", sinkFindings);

            fixSourceVulnNamesBasedOnSinkContextHashMapKey("Findings_With_HashMap_To_Join_", sourceFindings);

            var results = joinTracesWhereSinkMatchesSource(sinkFindings, sourceFindings);
            ascx_FindingsViewer.openInFloatWindow(results);
        }

        public static void findTracesToJoin()
        {
            var sinkFindings = new List<IO2Finding>();
            var sourceFindings = new List<IO2Finding>();
            findTracesToJoin(sinkFindings, sourceFindings);

            //results.AddRange(sinkFindings);
            //results.AddRange(sourceFindings);				

            //ascx_FindingsViewer.openInFloatWindow(results.ToList());	
        }

        public static void findTracesToJoin(List<IO2Finding> sinkFindings, List<IO2Finding> sourceFindings)
        {
            findTracesToJoin("org.springframework.ui.Model.addAttribute", "${", sinkFindings, sourceFindings);

            var results = new List<IO2Finding>();

        }

        public static void findTracesToJoin(string sinkMethodToFind, string sourceMethodToFind,
                                                 List<IO2Finding> sinkFindings, List<IO2Finding> sourceFindings)
        {
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);

            foreach (O2Finding o2Finding in o2Assessment.o2Findings)
                if (o2Finding.Sink.IndexOf(sinkMethodToFind) > -1)
                    sinkFindings.Add(o2Finding);
                else if (o2Finding.SourceContext.IndexOf(sourceMethodToFind) > -1)
                    sourceFindings.Add(o2Finding);
            log.info("There are {0} sinkFindings ( sink ~= {1} )", sinkFindings.Count, sinkMethodToFind);
            log.info("There are {0} sourceFindings ( source ~= {1})", sourceFindings.Count, sourceMethodToFind);

            //ascx_FindingsViewer.openInFloatWindow(results.ToList());
        }       

        public static void fixSinkVulnNamesBasedOnSinkContextHashMapKey(string vulnNamePrefix, List<IO2Finding> sinkFindings)
        {
            //foreach(O2Finding o2Finding in sinkFindings)
            //	log.info("{0} -> {1}",  o2Finding.vulnName, o2Finding.SinkContext);
            foreach (O2Finding o2Finding in sinkFindings)
            {
                var hashTagName = extractNameFromContext(o2Finding.SinkContext, "\"", "\"");
                if (hashTagName != "")
                {
                    o2Finding.vulnName = vulnNamePrefix + "_" + hashTagName;
                    //o2Finding.vulnName = o2Finding.vulnName.Replace("addAttribute", "attribute");
                }
            }

            //ascx_FindingsViewer.openInFloatWindow(sinkFindings);
        }

        public static void fixSourceVulnNamesBasedOnSinkContextHashMapKey(string vulnNamePrefix, List<IO2Finding> sourceFindings)
        {
            foreach (O2Finding o2Finding in sourceFindings)
            {
                var hashTagName = extractNameFromContext(o2Finding.SourceContext, "${", "}");
                if (hashTagName != "")
                {
                    //log.info(hashTagName);
                    o2Finding.vulnName = vulnNamePrefix + "_" + hashTagName;
                    //o2Finding.vulnName = o2Finding.vulnName.Replace("addAttribute", "attribute");
                }
            }
            //ascx_FindingsViewer.openInFloatWindow(sourceFindings);
        }

        public static string extractNameFromContext(string textToProcess, string leftKeyword, string rigthKeyword)
        {
            var indexOfFirstQuote = textToProcess.IndexOf(leftKeyword);
            if (indexOfFirstQuote > -1)
            {
                var subString = textToProcess.Substring(indexOfFirstQuote + leftKeyword.Length);
                var indexOf2ndQuote = subString.IndexOf(rigthKeyword);
                if (indexOf2ndQuote > -1)
                    return subString.Substring(0, indexOf2ndQuote);
            }

            return "";
        }

        public static List<IO2Finding> joinTracesWhereSinkMatchesSource(List<IO2Finding> sinkFindings, List<IO2Finding> sourceFindings)
        {
            var results = new List<IO2Finding>();
            foreach (var o2SinkFinding in sinkFindings)
                foreach (var o2SourcesFinding in sourceFindings)
                    if (o2SourcesFinding.vulnName.IndexOf(o2SinkFinding.vulnName) > -1)
                    {
                        var o2NewFinding = (O2Finding)OzasmtCopy.createCopy(o2SinkFinding);
                        results.Add(o2NewFinding);
                        var sink = o2NewFinding.getSink();

                        var newTrace = new O2Trace(
                        string.Format("O2 Auto Join Point::: {0}   ->  {1}", o2SinkFinding.vulnName, o2SourcesFinding.vulnName));
                        newTrace.traceType = TraceType.Type_4;

                        sink.childTraces.Add(newTrace);

                        newTrace.childTraces.AddRange(o2SourcesFinding.o2Traces);

                        log.info("we have a match: {0}        ->        {1}", o2SinkFinding.vulnName, o2SourcesFinding.vulnName);

                    }
            return results;
        }

    }
}