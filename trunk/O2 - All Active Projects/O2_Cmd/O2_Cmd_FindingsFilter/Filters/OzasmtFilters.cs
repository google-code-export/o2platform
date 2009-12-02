// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using O2.DotNetWrappers.O2CmdShell;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Cmd.FindingsFilter.Filters
{
    public class OzasmtFilters
    {
        
        public static void onlyTraces(string ozasmtFile)
        {
            OzasmtLinqUtils.applyFilterToAssessmentFileAndSaveResult(ozasmtFile, onlyTraces, "Only Traces");
        }

        

        private static IEnumerable<IO2Finding> onlyTraces(IO2Assessment o2Assessment)
        {
            if (o2Assessment == null)
                return null;
            O2Cmd.log.write("--> Executing Filter: Create assessment with only Findings with traces");
            return from IO2Finding finding in o2Assessment.o2Findings
                                     where finding.o2Traces.Count > 0
                                     select finding;            
        }

        public static void noTraces(string ozasmtFile)
        {
            OzasmtLinqUtils.applyFilterToAssessmentFileAndSaveResult(ozasmtFile, noTraces, "No Traces");            
        }

        private static IEnumerable<IO2Finding> noTraces(IO2Assessment o2Assessment)
        {
            if (o2Assessment == null)
                return null;

            O2Cmd.log.write("--> Executing Filter: Create assessment with only Findings NO traces");
            return from IO2Finding finding in o2Assessment.o2Findings
                                     where finding.o2Traces.Count == 0
                                     select finding;            
        }


        public static void allFindings(string ozasmtFile)
        {
            OzasmtLinqUtils.applyFilterToAssessmentFileAndSaveResult(ozasmtFile, allFindings, "All Findings");
        }

        private static IEnumerable<IO2Finding> allFindings(IO2Assessment o2Assessment)
        {
            if (o2Assessment == null)
                return null;

            O2Cmd.log.write("--> Executing Filter: Create assessment with ALL only Findings (i.e. no filter applied)");
            return from IO2Finding finding in o2Assessment.o2Findings select finding;            
        }

        public static void onlyHighs(string ozasmtFile)
        {
            OzasmtLinqUtils.applyFilterToAssessmentFileAndSaveResult(ozasmtFile, onlyHighs, "Only Highs");
        }

        private static IEnumerable<IO2Finding> onlyHighs(IO2Assessment o2Assessment)
        {            
            if (o2Assessment == null)
                return null;

            O2Cmd.log.write("#) Executing Filter: Create assessment with only Findings with traces");
            return from IO2Finding finding in o2Assessment.o2Findings
                                     where finding.severity == 0
                                     select finding;            
        }

        public static void onlyVulnerabilities(string ozasmtFile)
        {
            IO2Assessment o2Assessment = OzasmtLinqUtils.getO2Assessment(ozasmtFile);
            if (o2Assessment == null)
                return;

            O2Cmd.log.write("#) Executing Filter: : Create assessment with only Findings with Vulnerabilities");
            var filteredO2Findings = from IO2Finding finding in o2Assessment.o2Findings
                                     where finding.confidence == 1
                                     select finding;
            OzasmtLinqUtils.saveFindings(filteredO2Findings, ozasmtFile, "Only Vulnerabilities");
        }

        public static void oneFilePerConfidence(string ozasmtFile)
        {
            IO2Assessment o2Assessment = OzasmtLinqUtils.getO2Assessment(ozasmtFile);
            if (o2Assessment == null)
                return;

            O2Cmd.log.write("--> Executing Filter: Create one assessment file per Vulnerability Type (Vulnerability, Type I and Type II");

            byConfidence(ozasmtFile, o2Assessment, 1, "Only Vulnerabilities");
            byConfidence(ozasmtFile, o2Assessment, 2, "Only Type I");
            byConfidence(ozasmtFile, o2Assessment, 3, "Only TYpe II");            
        }

        private static void byConfidence(string ozasmtFile, IO2Assessment o2Assessment, int confidence, string scanType)
        {
            O2Cmd.log.write("\n> Filtering by {0} \n", scanType);
            var filteredO2Findings = from IO2Finding finding in o2Assessment.o2Findings
                                     where finding.confidence == confidence
                                     select finding;
            OzasmtLinqUtils.saveFindings(filteredO2Findings, ozasmtFile, scanType);
        }

        public static void oneFilePerSeverity(string ozasmtFile)
        {
            IO2Assessment o2Assessment = OzasmtLinqUtils.getO2Assessment(ozasmtFile);
            if (o2Assessment == null)
                return;

            O2Cmd.log.write("--> Executing Filter: Create one assessment file per Severity Type (High, Medium, Low, Info");

            bySeverity(ozasmtFile, o2Assessment, 0, "Only High");
            bySeverity(ozasmtFile, o2Assessment, 1, "Only Medium");
            bySeverity(ozasmtFile, o2Assessment, 2, "Only Low");
            bySeverity(ozasmtFile, o2Assessment, 3, "Only Info");
         
        }

        private static void bySeverity(string ozasmtFile, IO2Assessment o2Assessment, int severity,string scanType)
        {
            O2Cmd.log.write("\n> Filtering by {0} \n", scanType);
            var filteredO2Findings = from IO2Finding finding in o2Assessment.o2Findings
                                     where finding.severity == severity
                                     select finding;
            OzasmtLinqUtils.saveFindings(filteredO2Findings, ozasmtFile, scanType);
        }

        
        /*public static void uniqueTraces(string ozasmtFile)
        {
            uniqueTraces(ozasmtFile, "");
        }*/

        public static void uniqueTraces(string ozasmtFile)//, string uniqueNameFilter)
        {
                OzasmtLinqUtils.applyFilterToAssessmentFileAndSaveResult(ozasmtFile, uniqueTraces, "Unique Traces",true);
        }

        private static IEnumerable<IO2Finding> uniqueTraces(IO2Assessment o2Assessment)        
        {
            if (o2Assessment == null)
                return null;
            O2Cmd.log.write("--> Executing Filter: UniqueTraces, i.e. 'Unique Findings per Vulnerability Type per File per Line of Code'");            

            // first remove duplicate findings (since their existence will affect the uniqueTraces calculations:

            RemoveDuplicateTypeIIs.removeDuplicateTypeIIsFromAssessment(o2Assessment);
            var uniqueVulnerabilities = new Dictionary<String, List<IO2Finding>>();
            // first populate a dictionary with all findings mapped to vulnType
            foreach (var o2Finding in o2Assessment.o2Findings)
              //  if (o2Finding.o2Traces.Count > 0)
                {
                    if (false == uniqueVulnerabilities.ContainsKey(o2Finding.vulnType))
                        uniqueVulnerabilities.Add(o2Finding.vulnType, new List<IO2Finding>());
                    uniqueVulnerabilities[o2Finding.vulnType].Add(o2Finding);
                }

            var uniqueFileNameAndLines = new Dictionary<String, List<IO2Finding>>();
            // then populate nother dictionary with the file_lineNumber combination 
            foreach (var type in uniqueVulnerabilities.Keys)
            {
                foreach (O2Finding o2Finding in uniqueVulnerabilities[type])
                {
                    var uniquename = string.Format("{0}_{1}_{2}_{3}", type, o2Finding.file, o2Finding.lineNumber, o2Finding.Source);
                    if (false == uniqueFileNameAndLines.ContainsKey(uniquename))
                        uniqueFileNameAndLines.Add(uniquename, new List<IO2Finding>());
                    uniqueFileNameAndLines[uniquename].Add(o2Finding);
                }
                //PublicDI.log.info("vuln name: {0} with {1} entries", type, uniqueVulnerabilities[type].Count);                
            }

            // finally
            // a) create a new Assessment file with 1 example each
            var o2FindingsToSave = new List<IO2Finding>();
            O2Cmd.log.write("  Creating one assessment file with 1 example each");
            foreach (var uniqueName in uniqueFileNameAndLines.Keys)
            {
                var o2SampleO2Finding = uniqueFileNameAndLines[uniqueName][0];
                o2SampleO2Finding.context = String.Format("There were {0} similar traces that ended up in this vulntype+file+line combination:       {1}",
                    uniqueFileNameAndLines[uniqueName].Count, uniqueName);
                o2FindingsToSave.Add(o2SampleO2Finding);
            }
            return o2FindingsToSave;

            /*OzasmtLinqUtils.saveFindings(o2FindingsToSave, ozasmtFile,"Unique Traces");        

            // b) create one file per unique combination that matches uniqueName
            if (uniqueNameFilter != "")
            {
                O2Cmd.log.write(
                    "  [Debug mode]Creating one assessment file per unique VulnType_Filename_LineNUmber combination");
                // create temp directory to hold files
                OzasmtLinqUtils.dirToSaveCreatedFilteredFiles = Path.Combine(OzasmtLinqUtils.dirToSaveCreatedFilteredFiles,
                                                                         Path.GetFileNameWithoutExtension(ozasmtFile) +
                                                                         "_all_UniqueTraces");
                Files.checkIfDirectoryExistsAndCreateIfNot(OzasmtLinqUtils.dirToSaveCreatedFilteredFiles);
                int numberOfFilesCreated = 0;
                foreach (var uniqueName in uniqueFileNameAndLines.Keys)
                {
                    if (uniqueNameFilter == "All" || (uniqueName.IndexOf(uniqueNameFilter) > -1 || RegEx.findStringInString(uniqueName,uniqueNameFilter)))
                    {
                        var o2FindingsForUniqueName = uniqueFileNameAndLines[uniqueName];
                        OzasmtLinqUtils.saveFindings(o2FindingsForUniqueName, ozasmtFile,                            
                            uniqueName + " ( " + o2FindingsForUniqueName.Count + " Findings )", false);
                        numberOfFilesCreated++;
                        if (numberOfFilesCreated % 100 == 0)
                            O2Cmd.log.write("     {0} files created so far", numberOfFilesCreated);
                    }
                }
                O2Cmd.log.write("  {0} files where created and saved to directory {1}", numberOfFilesCreated,OzasmtLinqUtils.dirToSaveCreatedFilteredFiles);
            }
             */
        }


    }
}
