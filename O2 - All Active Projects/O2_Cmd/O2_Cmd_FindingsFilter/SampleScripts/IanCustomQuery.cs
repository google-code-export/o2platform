using System;
using System.Collections.Generic;
using System.Linq;
using O2.Cmd.FindingsFilter.Filters;
using O2.DotNetWrappers.O2CmdShell;
using O2.Kernel.Interfaces.O2Findings;


namespace O2.Cmd.FindingsFilter.SampleScripts
{
    public class IanCustomQuery
    {

        public static void HelloWorld()
        {
            // You can write text directly to the console
            Console.WriteLine("Hello world");
            // or use the O2Cmd API
            O2Cmd.log.write("Hello again");
        }

        public static void allVulnsAndType1(string ozasmtFile)
        {
            OzasmtLinqUtils.applyFilterToAssessmentFileAndSaveResult(ozasmtFile, allVulnsAndType1, "All Vulns And Types I");
        }


        private static IEnumerable<IO2Finding> allVulnsAndType1(IO2Assessment o2Assessment)
        {
            if (o2Assessment == null)
                return null;
            O2Cmd.log.write("--> Executing Ian Custom Filter: Create assessment with only Vulnerabilities and Type 1 ");
            return from IO2Finding finding in o2Assessment.o2Findings
                   where finding.confidence < 3
                   select finding;            
        }
    }
}