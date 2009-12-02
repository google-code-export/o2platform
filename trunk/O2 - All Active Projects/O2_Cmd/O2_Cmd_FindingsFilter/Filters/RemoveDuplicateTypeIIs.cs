// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System.Collections.Generic;
using System.Linq;
using O2.DotNetWrappers.O2CmdShell;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Cmd.FindingsFilter.Filters
{
    public class RemoveDuplicateTypeIIs
    {
        // function consumed from Ozasmt.UniqueFindings function
        public static void removeDuplicateTypeIIsFromAssessment(IO2Assessment o2Assessment)
        {
            O2Cmd.log.write("\n  Removing duplicate Type II findings");
            var findingsByActionObjectFileAndLineNumber = groupFindingsByActionObjectIdFileNameAndLineNumber(o2Assessment.o2Findings);            
            var findingsToRemove = getFindingsToRemove(findingsByActionObjectFileAndLineNumber);
            removeFindingsFromAssessment(o2Assessment, findingsToRemove);
            OzasmtLinqUtils.saveFindings(findingsToRemove, "", "duplicateFindings");
            //new O2Assessment(findingsToRemove).save(new O2AssessmentSave_OunceV6(),ozasmtToSaveDuplicatedFindings);
            O2Cmd.log.write("");  
        }

        // function used in the Removing Duplicate Type IIs.pdf document
        public static void removeTypeIIs(string ozasmtToProcess)
            {
                O2Cmd.log.write("This function will remove duplicate Type IIs");
                var o2Assessment = OzasmtLinqUtils.getO2Assessment(ozasmtToProcess);
                O2Cmd.log.write("There are {0} findings in the ozasmt file loaded", o2Assessment.o2Findings.Count);
                var findingsByActionObjectFileAndLineNumber = groupFindingsByActionObjectIdFileNameAndLineNumber(o2Assessment.o2Findings);
                //listFindingsMappings(findingsByActionObjectFileAndLineNumber);
                var findingsToRemove = getFindingsToRemove(findingsByActionObjectFileAndLineNumber);
                removeFindingsFromAssessment(o2Assessment, findingsToRemove);
                saveAssessment(o2Assessment);
                saveFindingsAsNewAssessment(findingsToRemove);
            }

        private static void saveFindingsAsNewAssessment(List<IO2Finding> findingsToRemove)
        {
            var tempO2Assessment = new O2Assessment(findingsToRemove);
            var savedAssessmentFile = tempO2Assessment.save(new O2AssessmentSave_OunceV6());
            O2Cmd.log.write("O2Assessment WITH duplicate findings saved to: {0}", savedAssessmentFile);
        }

        private static void saveAssessment(IO2Assessment o2Assessment)
        {
            var savedAssessmentFile = o2Assessment.save(new O2AssessmentSave_OunceV6());
            O2Cmd.log.write("O2Assessment WITHOUT duplicate findings saved to: {0}", savedAssessmentFile);
        }

        private static void removeFindingsFromAssessment(IO2Assessment o2Assessment, List<IO2Finding> findingsToRemove)
        {
            foreach (var o2FindingToRemove in findingsToRemove)
                o2Assessment.o2Findings.Remove(o2FindingToRemove);
        }

        public static List<IO2Finding> getFindingsToRemove(Dictionary<string, List<IO2Finding>> findingsMappings)
        {
            var findingsToRemove = new List<IO2Finding>();
            foreach (var uniqueString in findingsMappings.Keys)
            {
                var o2FindingsThatMatchUniqueString = findingsMappings[uniqueString];
                // 1st condition: There are at least two findings whose unique signature match
                if (o2FindingsThatMatchUniqueString.Count > 1)
                {
                    // 2nd condition: There is one (and only one) finding with no traces
                    var iFindingsWithNoTraces = 0;
                    IO2Finding findingWithNoTraces = null;
                    foreach (var o2Finding in o2FindingsThatMatchUniqueString)
                        if (o2Finding.o2Traces.Count == 0)
                        {
                            iFindingsWithNoTraces++;
                            findingWithNoTraces = o2Finding;
                        }
                    if (iFindingsWithNoTraces == 1)
                        findingsToRemove.Add(findingWithNoTraces);
                    // 3rd Condition: There is at least one finding with traces 
                    // we dont need to look for this one since it is already covered by the previous two conditions
                }
            }
    //        O2Cmd.log.write("There are {0} findings to remove", findingsToRemove.Count);
            return findingsToRemove;
        }

        /*public static void listFindingsMappings(Dictionary<string, List<IO2Finding>> findingsMappings)
        {
            foreach (var uniqueString in findingsMappings.Keys)
            {                
//              
          //      if (findingsMappings[uniqueString].Count > 1 && (from O2Finding o2Finding in findingsMappings[uniqueString] where o2Finding.o2Traces.Count == 0 select o2Finding).Count() > 0)
          //      {
                    O2Cmd.log.write("\n Action Object, File & Line Number: {0}", uniqueString);
                    foreach (var o2Finding in findingsMappings[uniqueString])
                        O2Cmd.log.write("   vulnName, #root traces,line number, file: {0} : {1} : {2} : {3}",
                                  o2Finding.vulnName, o2Finding.o2Traces.Count, o2Finding.lineNumber, o2Finding.file);
            //    }
            }
        }*/

        public static Dictionary<string, List<IO2Finding>> groupFindingsByActionObjectIdFileNameAndLineNumber(List<IO2Finding> findingsToFilter)
        {
            var results = new Dictionary<string, List<IO2Finding>> ();            
            foreach(var o2Finding in findingsToFilter)
            {
                var uniqueString = string.Format("{0}_{1}_{2}", o2Finding.actionObject, o2Finding.file,
                                                 o2Finding.lineNumber);
                if (false == results.ContainsKey(uniqueString))
                    results.Add(uniqueString, new List<IO2Finding>());
                results[uniqueString].Add(o2Finding);
            }
      //      O2Cmd.log.write("There are {0} unique string combinations", results.Keys.Count);
            return results;
        }
    }
}
