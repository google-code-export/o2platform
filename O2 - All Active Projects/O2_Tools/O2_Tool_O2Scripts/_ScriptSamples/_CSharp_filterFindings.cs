using System.Collections.Generic;
using System.Linq;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Core;
using O2.Kernel.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.O2Scripts._ScriptSamples
{
    public class CSharp_FindingsFilter
    {
        public static IO2Log log = O2.Kernel.PublicDI.log;

        public static string ozasmtFileToLoad = @"E:\O2\Demodata\wg.ozasmt";

        public static void loadAssessmentFileAndShowAllFindings()
        {
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
            ascx_FindingsViewer.openInFloatWindow(o2Assessment.o2Findings);
        }

        public static void filterFindings_usingForEachLoop()
        {
            string message = string.Format("Hello O2 World");
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
            log.info("Assessment file loaded with {0} findings", o2Assessment.o2Findings.Count);
            var results = new List<IO2Finding>();
            foreach (O2Finding o2Finding in o2Assessment.o2Findings)
                if (o2Finding._SinkToSource.IndexOf("Attribute") > -1)
                    results.Add(o2Finding);
            log.info("There are {0} findings that match filter", results.Count);
            var newAssessmentFile = new O2Assessment(results);
            var savedFile = newAssessmentFile.save(new O2AssessmentSave_OunceV6());
            log.info("Filtered results saved to: {0}", savedFile);
            ascx_FindingsViewer.openInFloatWindow(results.ToList());
        }

        public static void filterFindings_usingLinq()
        {
            string message = string.Format("Hello O2 World");
            var o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtFileToLoad);
            log.info("Assessment file loaded with {0} findings", o2Assessment.o2Findings.Count);

            var results = from O2Finding finding in o2Assessment.o2Findings
                          where finding._SinkToSource.IndexOf("Attribute") > -1
                          select (IO2Finding)finding;
            log.info("There are {0} findings that match filter", results.ToList().Count);
            ascx_FindingsViewer.openInFloatWindow(results.ToList());
        }

    }
}