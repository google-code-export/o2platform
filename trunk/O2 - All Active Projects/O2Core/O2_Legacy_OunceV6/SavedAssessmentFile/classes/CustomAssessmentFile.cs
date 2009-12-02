// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using O2.DotNetWrappers.DotNet;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class CustomAssessmentFile
    {
        public static String saveAssessmentSearchResultsAsNewAssessmentRunFile(
            AnalysisSearch.SavedAssessmentSearch sasSavedAsssessmentSearch)
        {
            bool bCreateFileWithAllTraces = true;
            bool bCreateFileWithUniqueTraces = false;
            // the next two are only used if bCreateFileWithUniqueTraces is set to true
            bool bDropDuplicateSmartTraces = true;
            bool bIgnoreRootCallInvocation = false;
            return saveAssessmentSearchResultsAsNewAssessmentRunFile(sasSavedAsssessmentSearch, "",
                                                                     bCreateFileWithAllTraces,
                                                                     bCreateFileWithUniqueTraces,
                                                                     bDropDuplicateSmartTraces,
                                                                     bIgnoreRootCallInvocation);
        }

        public static String saveAssessmentSearchResultsAsNewAssessmentRunFile(
            AnalysisSearch.SavedAssessmentSearch sasSavedAsssessmentSearch, String sPathToNewAssessmentFile,
            bool bCreateFileWithAllTraces, bool bCreateFileWithUniqueTraces, bool bDropDuplicateSmartTraces,
            bool bIgnoreRootCallInvocation)
        {
            String sTargetFilename = "";
            if (bCreateFileWithAllTraces || bCreateFileWithUniqueTraces) // see if we have something to do 
            {
                sPathToNewAssessmentFile = createAssessmentRunFileWithAllTracesFromSavedAssessmentSearch(sPathToNewAssessmentFile, sasSavedAsssessmentSearch, out sTargetFilename);


                if (bCreateFileWithUniqueTraces)                
                    sTargetFilename = fromAssessmentRunFileCreateNewFileWithUniqueTraces(sPathToNewAssessmentFile, bDropDuplicateSmartTraces, bIgnoreRootCallInvocation);                

                if (bCreateFileWithAllTraces == false)
                    // delete it if we said we were only interested on bCreateFileWithUniqueTraces                
                    File.Delete(sPathToNewAssessmentFile);                
                
            }
            return sTargetFilename; // sPathToNewAssessmentFile;
        }

        private static string fromAssessmentRunFileCreateNewFileWithUniqueTraces(string sPathToNewAssessmentFile, bool bDropDuplicateSmartTraces, bool bIgnoreRootCallInvocation)
        {
            string sTargetFilename;
            DI.log.debug("Create file with unique traces");
            // 
            O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6NewFile = null;
            Analysis.loadAssessmentFile(sPathToNewAssessmentFile, false, ref oadO2AssessmentDataOunceV6NewFile);


            Analysis.FindingNameFormat ffnFindingNameFormat = Analysis.FindingNameFormat.FindingType;
            bool bChangeFindingData = false;

            var ffsmFilter = new AnalysisFilters.filter_FindSmartTraces(bDropDuplicateSmartTraces,
                                                                        bIgnoreRootCallInvocation,
                                                                        ffnFindingNameFormat, bChangeFindingData);
            AssessmentRun arFilteredAssessmentRun =
                Analysis.createFilteredAssessmentRunObjectBasedOnCriteria(ffsmFilter, oadO2AssessmentDataOunceV6NewFile);
            DI.log.debug("Completed process of filtering to remove duplicate findings");
            sTargetFilename = sPathToNewAssessmentFile + "_UniqueTraces.ozasmt";
            Analysis.saveFilteredAssessmentRun(arFilteredAssessmentRun, sTargetFilename,
                                               oadO2AssessmentDataOunceV6NewFile);
            return sTargetFilename;
        }

        private static string createAssessmentRunFileWithAllTracesFromSavedAssessmentSearch(string sPathToNewAssessmentFile, AnalysisSearch.SavedAssessmentSearch sasSavedAsssessmentSearch, out string sTargetFilename)
        {
            DI.log.debug("Creating SavedAssessmentFile from traces calculated");
            if (sPathToNewAssessmentFile == "")
                sPathToNewAssessmentFile = DI.config.TempFileNameInTempDirectory;
                    //if (sPathToNewAssessmentFile.IndexOf(".xml") == -1)
            sPathToNewAssessmentFile += ".ozasmt";
            var maxFindingsToSave = 25000;
            if (sasSavedAsssessmentSearch.lfrFindingsResults.Count > maxFindingsToSave)
            {
                DI.log.debug("NOTE: due to the large number of findings ({0}) multiple assessment files will be created (each with a maximum of {1} findings",
                    sasSavedAsssessmentSearch.lfrFindingsResults.Count, maxFindingsToSave);
                var index = 1;
                var itemsProcessed = 1;
                var lfrFindingsResults = new List<AnalysisSearch.FindingsResult>();

                foreach (AnalysisSearch.FindingsResult frFindingsResult in sasSavedAsssessmentSearch.lfrFindingsResults)
                {
                    //lfrFindingsResults.GetRange(0, 0);
                    lfrFindingsResults.Add(frFindingsResult);
                    if ((itemsProcessed++) % maxFindingsToSave == 0)
                    {
                        sTargetFilename = sPathToNewAssessmentFile.Replace(".ozasmt", string.Format("_part_{0}.ozasmt", index++));
                        DI.log.debug("Saving {0} findings to {1}", lfrFindingsResults.Count, sTargetFilename);
                        create_CustomSavedAssessmentRunFile_From_FindingsResult_List(lfrFindingsResults, sTargetFilename);
                        lfrFindingsResults = new List<AnalysisSearch.FindingsResult>();
                    }                    
                }
                // do the remaining ones
                sTargetFilename = sPathToNewAssessmentFile.Replace(".ozasmt", string.Format("_part_{0}.ozasmt", index));
                DI.log.debug("Saving {0} findings to {1}", lfrFindingsResults.Count, sTargetFilename);
                create_CustomSavedAssessmentRunFile_From_FindingsResult_List(lfrFindingsResults, sTargetFilename);
                sPathToNewAssessmentFile = sTargetFilename;   // assigned the last one to this
            }
            else
            {
                DI.log.debug("There are {0} findings to save", sasSavedAsssessmentSearch.lfrFindingsResults.Count);
                // we always need to create this one    
                //DI.log.debug("Create file with all traces");
                
                //create_CustomSavedAssessmentRunFile_From_FindingsResult_List(lfrFindingsResults_UniqueFindings,
                create_CustomSavedAssessmentRunFile_From_FindingsResult_List(sasSavedAsssessmentSearch.lfrFindingsResults, sPathToNewAssessmentFile);                
            }
            sTargetFilename = sPathToNewAssessmentFile;
            return sPathToNewAssessmentFile;
            /*
            DI.log.debug("Calculate  List of Findings to save");

            var lfUniqueFindings = new List<AssessmentAssessmentFileFinding>();
            var lfrFindingsResults_UniqueFindings = new List<AnalysisSearch.FindingsResult>();
            var itemsProcessed = 0;
            var itemsToProcess = sasSavedAsssessmentSearch.lfrFindingsResults.Count;
            foreach (AnalysisSearch.FindingsResult frFindingsResult in sasSavedAsssessmentSearch.lfrFindingsResults)
            {
                if (false == lfUniqueFindings.Contains(frFindingsResult.fFinding))
                {
                    lfUniqueFindings.Add(frFindingsResult.fFinding);
                    lfrFindingsResults_UniqueFindings.Add(frFindingsResult);
                }
                if ((itemsProcessed++) % 1000 == 0)
                    DI.log.info("Processed [{0}/{1}] - {2}", itemsProcessed, itemsToProcess, lfUniqueFindings.Count);
            }
            DI.log.debug("There are {0} findings to save", lfrFindingsResults_UniqueFindings.Count);*/            
        }

        public static void create_CustomSavedAssessmentRunFile_From_FindingsResult_Dictionary(
            Dictionary<String, List<FindingViewItem>> dFilteredFindings, String sPathToNewAssessmentFile)
        {
            foreach (string sItem in dFilteredFindings.Keys)
            {
                var lfrFindingsResults = new List<AnalysisSearch.FindingsResult>();

                foreach (FindingViewItem fviFindingViewItem in dFilteredFindings[sItem])
                    lfrFindingsResults.Add(fviFindingViewItem.frFindingResult);

                //dFilteredFindings[sItem][0].`
                String sPathToNewAssessmentFile_Item = String.Format("{0}_{1}.xml", sPathToNewAssessmentFile,
                                                                     sItem.Replace('<', '_').Replace('>', '_').Replace(
                                                                         ';', ',').Replace('$', '-').Replace(':', ' ').Replace('\\', '_').Replace('/', '_'));
                create_CustomSavedAssessmentRunFile_From_FindingsResult_List(lfrFindingsResults,
                                                                             sPathToNewAssessmentFile_Item);
            }
            DI.log.debug("Completed Creation of {0} custom assessment files", dFilteredFindings.Keys.Count);
        }

        public static void create_CustomSavedAssessmentRunFile_From_FindingsResult_List(
            List<AnalysisSearch.FindingsResult> lfrFindingsResults, String sPathToNewAssessmentFile)
        {
            O2Timer tTimer =
                new O2Timer("Creating new AssessmentRun File " + sPathToNewAssessmentFile).start();
            if (lfrFindingsResults.Count == 0)
                DI.log.error("Aborting, no findings to save");
            else
            {
                AssessmentRun arNewAssessmentRun = OzasmtUtils_OunceV6.getDefaultAssessmentRunObject();
                arNewAssessmentRun.name = "Search Results Project";
                var lFilesAndFindingsToAdd =
                    new Dictionary<AssessmentAssessmentFile, List<AssessmentAssessmentFileFinding>>();
                var dNewStringIndex = new Dictionary<String, UInt32>();
                var dNewFileIndex = new Dictionary<String, UInt32>();
                foreach (AnalysisSearch.FindingsResult frFindingsResult in lfrFindingsResults)
                {
                    if (false == lFilesAndFindingsToAdd.ContainsKey(frFindingsResult.fFile))
                        // doesn't exist so we need to add it
                    {
                        lFilesAndFindingsToAdd.Add(frFindingsResult.fFile, new List<AssessmentAssessmentFileFinding>());
                    }

                    lFilesAndFindingsToAdd[frFindingsResult.fFile].Add(
                        VirtualTraces.createNewFindingFromExistingOne(frFindingsResult.fFinding, dNewStringIndex,
                                                                      dNewFileIndex,
                                                                      frFindingsResult.oadO2AssessmentDataOunceV6));
                }
                arNewAssessmentRun.StringIndeces = OzasmtUtils_OunceV6.createStringIndexArrayFromDictionary(dNewStringIndex);
                arNewAssessmentRun.FileIndeces = OzasmtUtils_OunceV6.createFileIndexArrayFromDictionary(dNewFileIndex);

                var lafNewAssessmentFilesToAdd = new List<AssessmentAssessmentFile>();
                foreach (AssessmentAssessmentFile afOriginalFile in lFilesAndFindingsToAdd.Keys)
                {
                    AssessmentAssessmentFile afNewFile =
                        VirtualTraces.createNewAssessmentFileFromExistingOne(afOriginalFile);
                    afNewFile.Finding = lFilesAndFindingsToAdd[afOriginalFile].ToArray();
                    lafNewAssessmentFilesToAdd.Add(afNewFile);
                }
                arNewAssessmentRun.Assessment.Assessment[0].AssessmentFile = lafNewAssessmentFilesToAdd.ToArray();

                DI.log.info("New assessmentRun file created in memory, now saving it");
                // and save the serialized object as an Xml file
                OzasmtUtils_OunceV6.createSerializedXmlFileFromAssessmentRunObject(arNewAssessmentRun, sPathToNewAssessmentFile);
                tTimer.stop();
            }
            // if (false)
            //     o2Messages.sendMessage("open ViewAssessmentRun," + sPathToNewAssessmentFile);
        }
    }
}
