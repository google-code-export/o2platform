using System;
using System.Collections.Generic;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Legacy.OunceV6.SavedAssessmentFile.classes
{
    public class AnalysisAssessmentFile
    {
        public static List<String> getListOf_TraceSignatures_ByTraceType(String sPathToSavedXmlFile,
                                                                         TraceType ttTraceType,
                                                                         ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            var lsMatches = new List<string>();
            bool bVerbose = false;
            if (fadO2AssessmentDataOunceV6 == null)
                Analysis.loadAssessmentFile(sPathToSavedXmlFile, bVerbose, ref fadO2AssessmentDataOunceV6);
            if (null != fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                foreach (Assessment aAssessment in fadO2AssessmentDataOunceV6.arAssessmentRun.Assessment.Assessment)
                    foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                        if (null != afAssessmentFile.Finding)
                            foreach (AssessmentAssessmentFileFinding aaffFinding in afAssessmentFile.Finding)
                            {
                                String sSignature = Analysis.getSmartTraceNameOfTraceType(aaffFinding.Trace, ttTraceType,
                                                                                          fadO2AssessmentDataOunceV6);
                                if (sSignature != "" && false == lsMatches.Contains(sSignature))
                                    lsMatches.Add(sSignature);
                            }
            return lsMatches;
        }

        public static List<String> getListOf_Sources(String sPathToSavedXmlFile,
                                                     ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            TraceType ttTraceType = TraceType.Source;
            return getListOf_TraceSignatures_ByTraceType(sPathToSavedXmlFile, ttTraceType, ref fadO2AssessmentDataOunceV6);
        }

        public static List<String> getListOf_Sinks(String sPathToSavedXmlFile, ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            //   Analysis.TraceType ttTraceType = Analysis.TraceType.Known_Sink;
            List<String> lsSinks = getListOf_KnownSinks(sPathToSavedXmlFile, ref fadO2AssessmentDataOunceV6);
            lsSinks.AddRange(getListOf_LostSinks(sPathToSavedXmlFile, ref fadO2AssessmentDataOunceV6));
            return lsSinks;
        }

        public static List<String> getListOf_LostSinks(String sPathToSavedXmlFile,
                                                       ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            TraceType ttTraceType = TraceType.Lost_Sink;
            return getListOf_TraceSignatures_ByTraceType(sPathToSavedXmlFile, ttTraceType, ref fadO2AssessmentDataOunceV6);
        }

        public static List<String> getListOf_KnownSinks(String sPathToSavedXmlFile,
                                                        ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            TraceType ttTraceType = TraceType.Known_Sink;
            return getListOf_TraceSignatures_ByTraceType(sPathToSavedXmlFile, ttTraceType, ref fadO2AssessmentDataOunceV6);
        }


        public static List<String> getListOf_LostSinks_Unique(String sPathToSavedXmlFile)
        {
            O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6 = null;
            return getListOf_LostSinks_Unique(sPathToSavedXmlFile, ref fadO2AssessmentDataOunceV6);
        }

        public static List<String> getListOf_LostSinks_Unique(String sPathToSavedXmlFile,
                                                              ref O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6)
        {
            var lMatches = new List<string>();


            bool bChangeFindingData = false;
            Analysis.FindingNameFormat ffnFindingNameFormat = Analysis.FindingNameFormat.FindingType;
            var ffulsFilter = new AnalysisFilters.filter_FindUniqueLostSinks(ffnFindingNameFormat, bChangeFindingData);
            List<AssessmentAssessmentFileFinding> laaffFindings = getListOfFindingsUsingFilter(sPathToSavedXmlFile,
                                                                                               ffulsFilter,
                                                                                               ref fadO2AssessmentDataOunceV6);
            foreach (AssessmentAssessmentFileFinding aaffFinding in laaffFindings)
                lMatches.Add(Analysis.getSmartTraceNameOfTraceType(aaffFinding.Trace, TraceType.Lost_Sink,
                                                                   fadO2AssessmentDataOunceV6));
            return lMatches;
        }

        public static List<AssessmentAssessmentFileFinding> getListOfFindingsUsingFilter(String sPathToSavedXmlFile,
                                                                                         AnalysisFilters.
                                                                                             filter_FindUniqueLostSinks
                                                                                             ffulsFilter,
                                                                                         ref O2AssessmentData_OunceV6
                                                                                             fadO2AssessmentDataOunceV6)
        {
            var laaffFinding = new List<AssessmentAssessmentFileFinding>();
            try
            {
                bool bVerbose = false;
                var lsMatches = new List<string>();
                Analysis.loadAssessmentFile(sPathToSavedXmlFile, bVerbose, ref fadO2AssessmentDataOunceV6);
                AssessmentRun arFilteredAssessmentRun =
                    Analysis.createFilteredAssessmentRunObjectBasedOnCriteria(ffulsFilter, fadO2AssessmentDataOunceV6);
                if (null != arFilteredAssessmentRun.Assessment.Assessment)
                    foreach (Assessment aAssessment in arFilteredAssessmentRun.Assessment.Assessment)
                        foreach (AssessmentAssessmentFile afAssessmentFile in aAssessment.AssessmentFile)
                            if (null != afAssessmentFile.Finding)
                                foreach (AssessmentAssessmentFileFinding aaffFinding in afAssessmentFile.Finding)
                                    laaffFinding.Add(aaffFinding);
            }
            catch (Exception ex)
            {
                DI.log.error("getListOfFindingsUsingFilter: {0}", ex.Message);
            }
            return laaffFinding;
        }

        public static void createSavedAssessmentFileWith_LostSinks_Unique(String sPathToFindingsXmlFile_Source,
                                                                          String sPathToFindingsXmlFile_Target)
        {
            O2AssessmentData_OunceV6 fadO2AssessmentDataOunceV6 = null;
            bool bChangeFindingData = true;
            bool bVerbose = false;
            Analysis.loadAssessmentFile(sPathToFindingsXmlFile_Source, bVerbose, ref fadO2AssessmentDataOunceV6);
            Analysis.FindingNameFormat ffnFindingNameFormat = Analysis.FindingNameFormat.Sink;
            Analysis.createAssessmentFileWithLostSinks_OneExampleEach(sPathToFindingsXmlFile_Target,
                                                                      ffnFindingNameFormat, bChangeFindingData,
                                                                      fadO2AssessmentDataOunceV6);
        }
    }
}