using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.CIR;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;

namespace O2.Legacy.OunceV6.JoinTraces.classes
{
    public class JoinTracesUtils
    {
        public static O2AssessmentData_OunceV6 loadAssessmentRunFileAndAddItToList(String sPathToFile)
        {
            var bDropDuplicateSmartTraces = true;
            var bIgnoreRootCallInvocation = true;
            var ffFindingFilter = Analysis.FindingFilter.AllFindings;
            O2AssessmentData_OunceV6 oadO2AssessmentDataOunceV6 = null;
            O2Timer tTimer = new O2Timer("Loaded SavedAssessmentFile").start();
            Analysis.loadAssessmentFile(sPathToFile, false, ref oadO2AssessmentDataOunceV6);
            // Calculate Xrefs into fadAssessmentData                               

            Analysis.populateDictionariesWithXrefsToLoadedAssessment(ffFindingFilter, bDropDuplicateSmartTraces,

                                                                     bIgnoreRootCallInvocation, oadO2AssessmentDataOunceV6);            
            tTimer.stop();
            return oadO2AssessmentDataOunceV6;
        }

        public static void proccessLoadedFiles(List<O2AssessmentData_OunceV6> o2AssessmentDataItemsToProcess, bool bMakeLostSinksIntoSinks, Func<Dictionary<String, O2TraceBlock_OunceV6>, TreeView, bool> onComplete)
        {
            O2Thread.mtaThread(
                () =>
                    {                        
                        var dO2TraceBlock = new Dictionary<String, O2TraceBlock_OunceV6>();
                        var lO2AssessmentData = new List<O2AssessmentData_OunceV6>();
                        DI.log.info("proccessLoadedFiles: loading {0} files to analyze", o2AssessmentDataItemsToProcess.Count);
                        foreach (O2AssessmentData_OunceV6 oadO2AssessmentData in o2AssessmentDataItemsToProcess)
                            lO2AssessmentData.Add(oadO2AssessmentData);
                        DI.log.info("proccessLoadedFiles: proccess Loaded Files Into O2TraceBlock Dictionary");
                        analyzer.proccessLoadedFilesIntoO2TraceBlockDictionary(dO2TraceBlock, lO2AssessmentData, bMakeLostSinksIntoSinks);
                        DI.log.info("proccessLoadedFiles: map {0} O2TraceBlocks Into TreeView", dO2TraceBlock.Count);
                        var tvRawData = new TreeView();
                        analyzer.calculateO2TraceBlocksIntoTreeView(dO2TraceBlock, ref tvRawData);
                        onComplete(dO2TraceBlock, tvRawData);
                    });
        }

        public static List<TreeNode> createListOfNormalizedTraces(
            string textFilter, Dictionary<String, O2TraceBlock_OunceV6> dRawData,
            ICirData cdCirData, Dictionary<String, O2TraceBlock_OunceV6> dO2TraceBlock, 
            bool bOnlyProcessTracesWithNoCallers)
        {
            //var tvCreatedTraces_NormalizedTracesView = new TreeView();
            var normalizedTracesView = analyzer.ResolveNormalizeTraceFor(textFilter, dRawData,
                                                                         cdCirData, dO2TraceBlock,
                                                                         bOnlyProcessTracesWithNoCallers);


            DI.log.info("getListOfNormalizedTraces");
            List<TreeNode> ltnNormalizedTraces = analyzer.getListOfNormalizedTraces(normalizedTracesView);
            return ltnNormalizedTraces;
        }

        public static AnalysisSearch.SavedAssessmentSearch createSavedAssessmentSearchObjectFromNormalizedTraces(List<TreeNode> ltnNormalizedTraces)
        {
            //     if (tvTempTreeView.Nodes.Count > 0)
            //     {
            var sasSavedAssessmentSearch = new AnalysisSearch.SavedAssessmentSearch();
            //       foreach (TreeNode tnTreeNode in tvTempTreeView.Nodes)
            DI.log.debug("There are {0} Traces in ltnTraces to process", ltnNormalizedTraces.Count);
            foreach (TreeNode tnTreeNode in ltnNormalizedTraces)
            {
                FindingViewItem fviJoinedFindingViewItem =
                    creator.createJoinedUpFindingViewItemFromTreeNodeWithFindingViewItemAsTags(tnTreeNode);
                if (fviJoinedFindingViewItem != null && fviJoinedFindingViewItem.fFinding != null)
                {
                    var frFindingsResult =
                        new AnalysisSearch.FindingsResult(fviJoinedFindingViewItem.oadO2AssessmentDataOunceV6)
                            {
                                fFinding = fviJoinedFindingViewItem.fFinding,
                                fFile =
                                    fviJoinedFindingViewItem.oadO2AssessmentDataOunceV6.dFindings[
                                    fviJoinedFindingViewItem.fFinding]
                            };
                    sasSavedAssessmentSearch.lfrFindingsResults.Add(frFindingsResult);
                    if (sasSavedAssessmentSearch.lfrFindingsResults.Count % 2500 == 0)
                        DI.log.debug("Create Trace # {0}/{1}", sasSavedAssessmentSearch.lfrFindingsResults.Count, ltnNormalizedTraces.Count);
                }
            }
            return sasSavedAssessmentSearch;

        }

        public static string createAssessmentFileFromSavedAssessmentSearchObject(
            AnalysisSearch.SavedAssessmentSearch sasSavedAssessmentSearch, string targetFolder, string fileNamePrefix, bool bCreateFileWithAllTraces, 
            bool bCreateFileWithUniqueTraces, bool bDropDuplicateSmartTraces, bool bIgnoreRootCallInvocation)
        {
            String sTargetFile = "";

            if (Directory.Exists(targetFolder))
                sTargetFile = Path.Combine(targetFolder, fileNamePrefix + "_ALLTRACES");


            return CustomAssessmentFile.saveAssessmentSearchResultsAsNewAssessmentRunFile(
                sasSavedAssessmentSearch,
                sTargetFile,
                bCreateFileWithAllTraces,bCreateFileWithUniqueTraces, bDropDuplicateSmartTraces,bIgnoreRootCallInvocation);
        }

        public static void createAssessessmentFileWithJoinnedTraces(
            string textFilter, Dictionary<string, O2TraceBlock_OunceV6> _dRawData, ICirData _cdCirData,
            Dictionary<string, O2TraceBlock_OunceV6> _dO2TraceBlock, bool bOnlyProcessTracesWithNoCallers,
            string targetFolder, string fileNamePrefix, bool bCreateFileWithAllTraces,
            bool bCreateFileWithUniqueTraces, bool bDropDuplicateSmartTraces, bool bIgnoreRootCallInvocation,
            O2Thread.FuncVoidT1<List<TreeNode>> viewNormalizedTraces, Func<string, string> onCompletion)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        DI.log.debug("\n\n\ncreateAssessessmentFileWithJoinnedTraces: step 1 - createListOfNormalizedTraces\n\n\n");
                        var ltnNormalizedTraces = createListOfNormalizedTraces(textFilter, _dRawData, _cdCirData, _dO2TraceBlock,bOnlyProcessTracesWithNoCallers);

                        if (ltnNormalizedTraces.Count ==0)
                        {
                            DI.log.error("There were no normalized traces, aborting");
                            onCompletion("");
                            return;
                        }

                        if (viewNormalizedTraces != null)
                            viewNormalizedTraces(ltnNormalizedTraces);

                        DI.log.debug("\n\n\ncreateAssessessmentFileWithJoinnedTraces: step 2 - createSavedAssessmentSearchObjectFromNormalizedTraces\n\n\n");

                        var sasSavedAssessmentSearch = createSavedAssessmentSearchObjectFromNormalizedTraces(ltnNormalizedTraces);

                        DI.log.debug("\n\n\ncreateAssessessmentFileWithJoinnedTraces: step 3 - createAssessmentFileFromSavedAssessmentSearchObject\n\n\n");

                        var sAssessmentFile = createAssessmentFileFromSavedAssessmentSearchObject(
                            sasSavedAssessmentSearch, targetFolder, fileNamePrefix, bCreateFileWithAllTraces,
                            bCreateFileWithUniqueTraces, bDropDuplicateSmartTraces, bIgnoreRootCallInvocation);

                        DI.log.debug("\n\n\ncreateAssessessmentFileWithJoinnedTraces: completed\n\n\n");
                        onCompletion(sAssessmentFile);
                    });
        }

        public static Dictionary<string, O2TraceBlock_OunceV6> calculateDictionaryWithRawData(TreeView _tvRawData)
        {
            var dRawData = new Dictionary<String, O2TraceBlock_OunceV6>();
            foreach (TreeNode node in _tvRawData.Nodes)
                dRawData.Add(node.Text, (O2TraceBlock_OunceV6)node.Tag);
            return dRawData;

        }
    }
}
