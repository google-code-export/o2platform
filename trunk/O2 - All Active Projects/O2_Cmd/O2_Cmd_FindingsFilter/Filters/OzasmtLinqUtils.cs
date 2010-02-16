// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2CmdShell;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Interfaces.O2Findings;

namespace O2.Cmd.FindingsFilter.Filters
{
    public class OzasmtLinqUtils
    {
        public static List<IO2AssessmentLoad> availableOzamstLoadEngines = new List<IO2AssessmentLoad>();
        public static List<IO2AssessmentSave> availableOzamstSaveEngines = new List<IO2AssessmentSave>();
        public static string dirToSaveCreatedFilteredFiles;

        //        public static int hardCodedMaxSaveLimit = 2500;

        static OzasmtLinqUtils()
        {
            availableOzamstLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            availableOzamstSaveEngines.Add(new O2AssessmentSave_OunceV6());
            availableOzamstLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
            dirToSaveCreatedFilteredFiles = Path.Combine(DI.config.CurrentExecutableDirectory, "Filtered Files");
            Files.checkIfDirectoryExistsAndCreateIfNot(dirToSaveCreatedFilteredFiles);
        }
        public static void availableEngines()
        {
            O2Cmd.log.write("These are the Ozasmt Engines available\n");
            O2Cmd.log.write("\n   To load \n");
            foreach (var loadEngine in availableOzamstLoadEngines)
                O2Cmd.log.write("   - {0} ", loadEngine.engineName);
            O2Cmd.log.write("\n   To Save \n");
            foreach (var saveEngine in availableOzamstSaveEngines)
                O2Cmd.log.write("   - {0} ", saveEngine.engineName);
        }

        public static IO2AssessmentLoad getLoadEngineForFile(string ozasmtFile)
        {
            foreach (var loadEngine in availableOzamstLoadEngines)
                if (loadEngine.canLoadFile(ozasmtFile))
                    return loadEngine;
            O2Cmd.log.write("Error: in getLoadEngineForFile, could not find load engine for file: {0}", ozasmtFile);
            return null;
        }

        public static IO2Assessment getO2Assessment(string ozasmtFile)
        {
            var loadEngine = getLoadEngineForFile(ozasmtFile);
            if (loadEngine != null)
            {
                O2Cmd.log.write("\n[engine {0}]: loading and converting file: {1}", loadEngine.engineName, ozasmtFile);
                var timer = new O2Timer("File Loaded").start();
                var o2Assessment = loadEngine.loadFile(ozasmtFile);
                timer.stop();
                O2Cmd.log.write("[engine {0}]: There are {1} Findings in the loaded file\n", loadEngine.engineName, o2Assessment.o2Findings.Count);
                return o2Assessment;
            }
            return null;
        }

        public static string saveFindings(IEnumerable<IO2Finding> o2FindingsToSave, string originalFileName, string filterType)
        {
            return saveFindings(o2FindingsToSave, originalFileName, filterType,true);
        }

        public static string saveFindings(IEnumerable<IO2Finding> o2FindingsToSave, string originalFileName, string filterType, bool verbose)
        {
            if (verbose)
                O2Cmd.log.write("  There are {0} findings that matched the filter: {1} Traces", o2FindingsToSave.Count(),
                      filterType);

            var newAssessmentName =
                Path.GetFileNameWithoutExtension(originalFileName) +
                ((filterType != "") ? (" - " + 
                Files.getSafeFileNameString(filterType)) : "") + ".ozasmt";
            var fileToCreate = Path.Combine(dirToSaveCreatedFilteredFiles, newAssessmentName);
            var saveEngine = availableOzamstSaveEngines[0];
            // there is only one engine at the moment so hardcoding this to the first one
            saveEngine.save(newAssessmentName, o2FindingsToSave, fileToCreate);
            if (verbose)
                O2Cmd.log.write("  Filtered File Saved to: {0}", fileToCreate);
            return fileToCreate;
        }
        public static void applyFilterToAssessmentFileAndSaveResult(string ozasmtFile, Func<IO2Assessment, IEnumerable<IO2Finding>> filterToApply, string filterType)
        {
            applyFilterToAssessmentFileAndSaveResult(ozasmtFile, filterToApply, filterType, false);
        }

        public static void applyFilterToAssessmentFileAndSaveResult(string ozasmtFile, Func<IO2Assessment, IEnumerable<IO2Finding>> filterToApply,string filterType, bool addOriginalOzasmtStats)
        {
            IO2Assessment o2Assessment = getO2Assessment(ozasmtFile);
            var filteredO2Findings = filterToApply(o2Assessment);
            var newOzasmtFile = saveFindings(filteredO2Findings, ozasmtFile,filterType );
//            O2Cmd.log.write("  newOzasmtFile :{0}", newOzasmtFile);
            if (File.Exists(newOzasmtFile) && addOriginalOzasmtStats)
                PublishToCore.copyAssessmentStats(ozasmtFile, newOzasmtFile);
                
        }
    }
}
