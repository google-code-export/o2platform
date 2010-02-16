// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.O2CmdShell;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.O2Findings;

namespace O2.Cmd.FindingsFilter.Filters
{
    public class PublishToCore
    {
        public static void copyAssessmentStats(string ozasmtWithStats, string ozasmtToUpdate)
        {
            O2Cmd.log.write("\n  Adding stats from file {0} to file {1}", ozasmtWithStats, ozasmtToUpdate);
            IO2Assessment o2Assessment = new O2Assessment(new O2AssessmentLoad_OunceV6(), ozasmtToUpdate);
            saveWithAssessmentSourceStats(ozasmtWithStats, o2Assessment, ozasmtToUpdate);
        }

        public static void copyAssessmentStats(string ozasmtSource)//, string ozasmtTarget)
        {
            IO2Assessment o2Assessment = new O2Assessment (new O2AssessmentLoad_OunceV6() ,ozasmtSource);
            O2Cmd.log.write("Assessment loaded had {0} findings", o2Assessment.o2Findings.Count);
            var newAssessmentName = "O2 v.5 - " + ozasmtSource;
            saveWithAssessmentSourceStats(ozasmtSource, o2Assessment, newAssessmentName);
        }

        public static void saveWithAssessmentSourceStats(string ozasmtSource, IO2Assessment o2Assessment, string newAssessmentName)
        {            
            if (new O2AssessmentSave_OunceV6().addAssessmentStatsFromSourceToO2AssessmentAndSaveIt(ozasmtSource, o2Assessment, newAssessmentName))
                O2Cmd.log.write("  File created: {0}", newAssessmentName);
            

        }        
    }
}
