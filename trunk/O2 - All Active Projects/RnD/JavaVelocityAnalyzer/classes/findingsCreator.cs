using System;
using System.Collections.Generic;
using O2.DotNetWrappers.Filters;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;
using O2.Legacy.OunceV6.SavedAssessmentFile.classes;
using O2.Legacy.OunceV6.TraceViewer;

namespace O2.Rnd.JavaVelocityAnalyzer.classes
{
    public class findingsCreator
    {
        public static String createFindingsFromVMFiles(ConsolidatedProcessedVelocityFiles cpvfVelocityFiles,
                                                       ascx_TraceViewer ascxTraceViewer)
        {
            String sTemplateSpringModeMapName =
                "org.springframework.ui.ModelMap.addAttribute_{0}(java.lang.String;java.lang.Object):org.springframework.ui.ModelMap";
            var lfrFindingsResult = new List<AnalysisSearch.FindingsResult>();
            foreach (ProcessedVelocityFile pvFile in cpvfVelocityFiles.getListWithProcessedLoadedFilesObjects())
            {
                foreach (String sMethod in pvFile.getFunctions())
                {
                    var fsFilteredSignature = new FilteredSignature(sMethod, ',');
                    if (fsFilteredSignature.sParameters == "")
                        // if there are no parameters just add them method as both source and sink
                    {
                        lfrFindingsResult.Add(createFindingsResultForSourceAndSink(sMethod, sMethod,
                                                                                   "Velocity.Finding.Function_noParam",
                                                                                   pvFile.getNormalizedFileName(),
                                                                                   "Velocity.Finding.Function_noParam",
                                                                                   pvFile.getNormalizedFileName(),
                                                                                   pvFile.sFullPathToOriginalFile
                                                  ));
                    }
                    else
                    {
                        foreach (String sParameter in fsFilteredSignature.lsParameters_Parsed)
                        {
                            String sVelocityVariableName =
                                sParameter.Replace("$", "").Replace("!", "").Replace("{", "").Replace("}", "");

                            String sSourceSignature = String.Format(sTemplateSpringModeMapName, sVelocityVariableName);
                            String sSinkSignature = sMethod;
                            lfrFindingsResult.Add(createFindingsResultForSourceAndSink(sSourceSignature, sSinkSignature,
                                                                                       "Velocity.Finding.Function_withParam",
                                                                                       pvFile.getNormalizedFileName(),
                                                                                       "Velocity.Finding.Function_withParam",
                                                                                       pvFile.getNormalizedFileName(),
                                                                                       pvFile.sFullPathToOriginalFile
                                                      ));
                        }
                    }
                }

                foreach (String sVar in pvFile.getVars())
                {
                    String sVelocityVariableName = sVar.Replace("$", "").Replace("!", "").Replace("{", "").Replace("}",
                                                                                                                   "");
                    String sSourceSignature = String.Format(sTemplateSpringModeMapName, sVelocityVariableName);
                    String sSinkSignature = sVar;
                    lfrFindingsResult.Add(createFindingsResultForSourceAndSink(sSourceSignature, sSinkSignature,
                                                                               "Velocity.Finding.Variable",
                                                                               pvFile.getNormalizedFileName(),
                                                                               "Velocity.Finding.Variable",
                                                                               pvFile.getNormalizedFileName(),
                                                                               pvFile.sFullPathToOriginalFile
                                              ));
                    //FindingViewItem fviFindingViewItem = new FindingViewItem(nfNewFinding.fFinding, nfNewFinding.oadNewO2AssessmentData);            
                    //ascxTraceViewer.setTraceDataAndRefresh(fviFindingViewItem);
                }
            }
            String sNewAssessmentFile = DI.config.TempFileNameInTempDirectory;

            CustomAssessmentFile.create_CustomSavedAssessmentRunFile_From_FindingsResult_List(lfrFindingsResult,
                                                                                              sNewAssessmentFile);
            return sNewAssessmentFile;
        }

        public static AnalysisSearch.FindingsResult createFindingsResultForSourceAndSink(String sSourceSignature,
                                                                                         String sSinkSignature,
                                                                                         String sFakeActionObjectId,
                                                                                         String sVulnName,
                                                                                         String sVulnType,
                                                                                         String sCallerName,
                                                                                         String sFileName)
        {
            var nfNewFinding = new VirtualTraces.NewFinding();
            nfNewFinding.setFinding_FileName(sFileName);
            nfNewFinding.setFinding_VulnName(sVulnName);
            nfNewFinding.setFinding_VulnType(sVulnType);
            nfNewFinding.setFinding_fakeActionObjectId(sFakeActionObjectId);
            nfNewFinding.setFinding_CallerName(sCallerName);

            CallInvocation ciRootNode = nfNewFinding.setRootTrace(sVulnName);
            CallInvocation ciSource = nfNewFinding.addCallToCall(sSourceSignature, ciRootNode, TraceType.Source);
            CallInvocation ciNode = nfNewFinding.addCallToCall(sCallerName, ciSource, TraceType.Root_Call);
            CallInvocation ciSink = nfNewFinding.addCallToCall(sSinkSignature, ciNode, TraceType.Known_Sink);


            var frFindingsResult = new AnalysisSearch.FindingsResult(nfNewFinding.oadNewO2AssessmentDataOunceV6);
            frFindingsResult.fFinding = nfNewFinding.fFinding;
            frFindingsResult.fFile = nfNewFinding.fFile;
            return frFindingsResult;
        }
    }
}