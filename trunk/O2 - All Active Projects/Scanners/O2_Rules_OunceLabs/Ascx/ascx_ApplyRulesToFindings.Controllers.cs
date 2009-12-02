using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;
using O2.Kernel.Interfaces.Rules;
using O2.Rules.OunceLabs.Filters;
using O2.Rules.OunceLabs.RulesUtils;
using O2.Views.ASCX.O2Findings;

namespace O2.Rules.OunceLabs.Ascx
{
	public partial class ascx_ApplyRulesToFindings
	{
        public enum AvailableFilters
        {
            BasicSinksMapping,
            MapSinksToAllTraces,
            MapSourcesToAllTraces,            
            MapFirstSourcesThenSinksToAllTraces,
            CreateAllPartialTraces
        }

        public Thread loadO2RulePack(string pathToO2RulePack)
        {
            return rulePackViewer.loadO2RulePack(pathToO2RulePack);            
        }

        public ascx_RulePackViewer getRulePackViewerControl()
        {
            return rulePackViewer;
        }
        public ascx_FindingsViewer getResultsFindingsViewerControl()
        {
            return findingsViewerMappedFindings;
        }

        public ascx_FindingsViewer getSourceFindingsViewerControl()
        {
            return findingsViewerSourceFindings;
        }

        public Thread executeFilter(AvailableFilters filterToApply, bool addFindingsWithNoMatches, O2Thread.FuncVoidT1<List<IO2Finding>> onCompletion)
        {
	        var o2TargetO2Findings = findingsViewerSourceFindings.currentO2Findings;
	        var o2RulesToUse = rulePackViewer.currentO2RulePack.o2Rules;
            return O2Thread.mtaThread(
                () =>
                    {
                        List<IO2Finding> mappedFidings = null ;
                        List<IO2Rule> o2Rules = o2RulesToUse.Cast<IO2Rule>().ToList();
                        switch (filterToApply)
                        {
                            case AvailableFilters.BasicSinksMapping:
                                DI.log.info("Executing filter: BasicSinksMapping");
                                mappedFidings = Filter_BasicSinksMapping.applyFilter(o2TargetO2Findings, o2Rules);
                                break;
                            case AvailableFilters.CreateAllPartialTraces:
                                DI.log.info("Executing filter: CreateAllPartialTraces");
                                mappedFidings = Filter_CreateAllPartialTraces.applyFilter(o2TargetO2Findings, o2Rules);
                                break;
                            case AvailableFilters.MapSinksToAllTraces:
                                DI.log.info("Executing filter: MapSinksToAllTraces");
                                mappedFidings = Filter_MapSinksToAllTraces.applyFilter(o2TargetO2Findings, o2Rules, addFindingsWithNoMatches);
                                break;
                            case AvailableFilters.MapSourcesToAllTraces:
                                DI.log.info("Executing filter: MapSourcesToAllTraces");
                                mappedFidings = Filter_MapSourcesToAllTraces.applyFilter(o2TargetO2Findings, o2Rules);
                                break;       
                            case AvailableFilters.MapFirstSourcesThenSinksToAllTraces:
                                DI.log.info("Executing filter: MapFirstSourcesThenSinksToAllTraces which has two steps");
                                DI.log.info("Step 1): MapFirstSourcesThenSinksToAllTraces->MapSourcesToAllTraces");
                                var sourceMappings = Filter_MapSourcesToAllTraces.applyFilter(o2TargetO2Findings, o2Rules);
                                DI.log.info("Step 2): MapFirstSourcesThenSinksToAllTraces->MapSinksToAllTraces");
                                mappedFidings = Filter_MapSinksToAllTraces.applyFilter(sourceMappings, o2Rules, addFindingsWithNoMatches);
                                break;                                          
                        }
                        if (onCompletion != null)
                            onCompletion(mappedFidings);
                    });
        }

	    private bool invokeOnTraceSelectedEvent = true;

        public void onTraceSelectedEvent(IO2Trace o2SelectedTrace)
        {
            if (o2SelectedTrace != null && invokeOnTraceSelectedEvent)
            {
                DI.log.info("onTraceSelectedEvent :{0}", o2SelectedTrace.signature);
                rulePackViewer.editRule(o2SelectedTrace.signature);
            }
        }

        public void onFindingSelectedEvent(IO2Finding o2FindingSelected)
        {
            if (o2FindingSelected != null)
            {
                invokeOnTraceSelectedEvent = false;
                Thread.Sleep(100);  // wait a litle bit so that the onTraceSelectedEvent fires first
                invokeOnTraceSelectedEvent = true;
                DI.log.info("onFindingSelectedEvent :{0}", o2FindingSelected.vulnName);
                rulePackViewer.editRule(o2FindingSelected.vulnName);
            }
        }

        public void onFindingsViewerFolderSelectedEvent(string selectedText)
        {
            DI.log.info("onFindingsViewerFolderSelectedEvent :{0}", selectedText);
            rulePackViewer.editRule(selectedText);
        }


	}
}
