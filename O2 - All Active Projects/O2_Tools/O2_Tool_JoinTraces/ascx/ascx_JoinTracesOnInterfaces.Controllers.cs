// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using O2.Core.CIR.Ascx;
using O2.Core.CIR.CirO2Findings;
using O2.Core.CIR.CirUtils;
using O2.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.JoinTraces.ascx
{
    public partial class ascx_JoinTracesOnInterfaces
    {

        public ascx_FindingsViewer getBaseFindingsControl()
        {
            return findingsViewer_BaseFindings;
        }

        public ascx_CirDataViewer getBaseCirDataViewerControl()
        {
            return cirDataViewer;
        }

        public Thread loadBaseFindings(string pathToAssessmentFileToLoad)
        {
            return findingsViewer_BaseFindings.loadO2Assessment(pathToAssessmentFileToLoad);
        }

        public void loadBaseCir(string pathToCirDataFileToLoad)
        {
            cirDataViewer.loadFile(pathToCirDataFileToLoad);
        }

        public void calculateSourcesMappedToInterfaces(bool includeOriginalFindings)
        {
            var baseO2Findings = findingsViewer_BaseFindings.currentO2Findings;
            var cirData = cirDataViewer.getCirDataObject();            
            var sourcesMappedToInterfaces = JoinFindings_OnInterfaces.getSourcesMappedToInterfaces(baseO2Findings,cirData);
            findingsViewer_SourcesMappedToInterfaces.loadO2Findings(sourcesMappedToInterfaces,true);

            findingsViewer_DynamicJoin.loadO2Findings(baseO2Findings, true);
            var results = new List<IO2Finding>();            
            while (true)
            {
                if (includeOriginalFindings)
                    results.AddRange(baseO2Findings);         // will add both Joined-up ones and original findings
                DI.log.info("************************* MAPPING INTERFACES **************");
                var joinedFindings = JoinFindings_OnInterfaces.mapInterfaces(baseO2Findings, sourcesMappedToInterfaces,
                                                                             cirData);
                if (joinedFindings.Count == 0)
                    break;                                
                baseO2Findings = joinedFindings;
                if (false == includeOriginalFindings)
                    results.AddRange(baseO2Findings);       // will only add the joined up ones
            }
            // now remove the findings with Lost Sinks in sourcesMappedToInterfaces (since these should have already been mapped
            var finalResults = new List<IO2Finding>();
            var sources = JoinFindings_OnInterfaces.getSources(sourcesMappedToInterfaces);
            foreach(O2.DotNetWrappers.O2Findings.O2Finding o2Finding in results)
            {
                var lostSink = o2Finding.LostSink;
                if (lostSink == "")
                    finalResults.Add(o2Finding);
                else
                    if (false == sources.ContainsKey(lostSink))
                        finalResults.Add(o2Finding);                   
            }
                //if (false == sourcesMappedToInterfaces.Contains(o2Finding.LostSink))
            //var join2 = JoinFindings_OnInterfaces.mapInterfaces(joinedFindings, sourcesMappedToInterfaces, cirData);
            //var join3 = JoinFindings_OnInterfaces.mapInterfaces(join2, sourcesMappedToInterfaces, cirData);
            findingsViewer_AutoMappingOfInterfaces.loadO2Findings(finalResults, true);



        }

        private void dynamicJoin_onTraceSelected(IO2Trace o2TraceSelected)
        {
            if (o2TraceSelected.traceType == TraceType.Lost_Sink)
            {
                var sources = JoinFindings_OnInterfaces.getSources(findingsViewer_SourcesMappedToInterfaces.currentO2Findings);                
                if (sources.ContainsKey(o2TraceSelected.signature))
                {
                    findingsViewers_withSourcesForInterfaces.loadO2Findings(sources[o2TraceSelected.signature],true);
                }

            }
            
        }
    }
}
