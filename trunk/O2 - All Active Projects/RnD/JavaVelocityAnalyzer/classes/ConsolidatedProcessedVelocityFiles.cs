// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using O2.Rnd.JavaVelocityAnalyzer;
using O2.Rnd.JavaVelocityAnalyzer.classes;

namespace O2.Rnd.JavaVelocityAnalyzer.classes
{
    public class ConsolidatedProcessedVelocityFiles
    {
        private readonly Dictionary<String, ProcessedVelocityFile> dLoadedProcessedVelocityFiles =
            new Dictionary<string, ProcessedVelocityFile>();

        public void addProcessedVelocityFiles(List<String> lFilesToLoad, String sRootDirectory)
        {
            foreach (String sFileToLoad in lFilesToLoad)
                addProcessedVelocityFile(sFileToLoad, sRootDirectory);
        }

        public void addProcessedVelocityFile(String sFileToLoad, String sRootDirectory)
        {
            if (dLoadedProcessedVelocityFiles.ContainsKey(sFileToLoad))
                DI.log.error("Trying to load a Process Velocity File that is already loaded: {0}", sFileToLoad);
            else
            {
                var pvfProcessedVelocityFile = new ProcessedVelocityFile(sFileToLoad, sRootDirectory);
                if (pvfProcessedVelocityFile.bLoadedOk)
                    dLoadedProcessedVelocityFiles.Add(sFileToLoad, pvfProcessedVelocityFile);
            }
        }

        public void removeProcessedVelocityFile(String sFileToRemove)
        {
            if (dLoadedProcessedVelocityFiles.ContainsKey(sFileToRemove))
                dLoadedProcessedVelocityFiles.Remove(sFileToRemove);
        }

        public List<String> getListOfLoadedFiles()
        {
            return getListOfLoadedFiles("");
        }

        public List<String> getListOfLoadedFiles(String sTextToRemove)
        {
            if (sTextToRemove == "")
                return new List<String>(dLoadedProcessedVelocityFiles.Keys.ToArray());
            else
            {
                var lsFilteredResults = new List<string>();
                foreach (string sLoadedFile in dLoadedProcessedVelocityFiles.Keys)
                    lsFilteredResults.Add(sLoadedFile.Replace(sTextToRemove, ""));
                return lsFilteredResults;
            }
        }

        public List<ProcessedVelocityFile> getListWithProcessedLoadedFilesObjects()
        {
            return new List<ProcessedVelocityFile>(dLoadedProcessedVelocityFiles.Values.ToArray());
        }

        public List<String> getCompleteListOfVars()
        {
            var lsAllVars = new List<string>();
            foreach (ProcessedVelocityFile pvfFile in dLoadedProcessedVelocityFiles.Values)
            {
                List<String> lsVars = pvfFile.getVars();
                foreach (String sVar in lsVars)
                    if (false == lsAllVars.Contains(sVar))
                        lsAllVars.Add(sVar);
            }
            return lsAllVars;
        }

        public List<String> getCompleteListOfMethods()
        {
            var lsAllMethods = new List<string>();
            foreach (ProcessedVelocityFile pvfFile in dLoadedProcessedVelocityFiles.Values)
            {
                List<String> lsMethods = pvfFile.getFunctions();
                foreach (String sMethod in lsMethods)
                    if (false == lsAllMethods.Contains(sMethod))
                        lsAllMethods.Add(sMethod);
            }
            return lsAllMethods;
        }
    }
}
