// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Kernel.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.JoinTraces.ascx
{
    public partial class ascx_JoinSinksToSources
    {
        private bool runOnLoad = true;
        private void onLoad()
        {
            if (DesignMode==false && runOnLoad)
            {
                // configure default Sinks findingsViewer control settings
                findingsViewer_Sinks.addO2AssessmentLoadEngine(new O2AssessmentLoad_OunceV6());
                findingsViewer_Sinks.setTraceTreeViewVisibleStatus(true);
                findingsViewer_Sinks.setFilter1Value("Sink");
                findingsViewer_Sinks.setFilter2Value("Source");
                // configure default Sources findingsViewer control settings
                findingsViewer_Sources.addO2AssessmentLoadEngine(new O2AssessmentLoad_OunceV6());                
                findingsViewer_Sources.setTraceTreeViewVisibleStatus(true);
                findingsViewer_Sources.setFilter1Value("Source");
                findingsViewer_Sources.setFilter2Value("Sink");
                findingsViewer_JoinnedTraces.setTraceTreeViewVisibleStatus(true);
                runOnLoad = false;
            }
        }

        public Thread loadFileIntoSinks(string fileToLoad)
        {
            return findingsViewer_Sinks.loadO2Assessment(fileToLoad);
        }

        public Thread loadFileIntoSources(string fileToLoad)
        {
            return findingsViewer_Sources.loadO2Assessment(fileToLoad);
        }

        public ascx_FindingsViewer getFindingsViewerObjectFor_Sinks()
        {
            return findingsViewer_Sinks;
        }

        public ascx_FindingsViewer getFindingsViewerObjectFor_Sources()
        {
            return findingsViewer_Sources;
        }

        public ascx_FindingsViewer getFindingsViewerObjectFor_JoinnedTraces()
        {
            return findingsViewer_JoinnedTraces;
        }

        public void calculateJoinnedTraces()
        {            
            try
            {
                var matches2 = from O2Finding sinkFinding in getFindingsIn_Sinks()
                               join O2Finding sourceFinding in getFindingsIn_Sources()
                               on sinkFinding.Sink equals sourceFinding.Source
                               where (
                                    sinkFinding.Sink != "" && sourceFinding.Source != "" 
                                    //&& sourceFinding.LostSink == ""
         /*                          &&
                                       sinkFinding.lineNumber == sourceFinding.lineNumber &&
                                       sinkFinding.file == sourceFinding.file*/
                               )
                               select new { sinkFinding, sourceFinding };
                
                var findingsResults = new List<IO2Finding>();

                foreach (var match in matches2)
                {
                    //var aa = match.sinkFinding.Sink;
                    //var bb = match.sourceFinding.Source;

                    foreach (var o2TraceInSource in match.sourceFinding.o2Traces)
                    {
                        var joinnedFinding = (O2Finding)OzasmtCopy.createCopy(match.sinkFinding);
                        var sink = joinnedFinding.getSink();

                        sink.traceType = TraceType.Type_4;

                        var joinnedTrace = new O2Trace("{TraceJoin}");
                        sink.childTraces.Add(joinnedTrace);
                        //findingsResults.Add(OzasmtGlue.createCopyAndGlueTraceSinkWithSource(match.sinkFinding, o2TraceInSource));
                        joinnedTrace.childTraces.Add(OzasmtCopy.createCopy(o2TraceInSource));

                        joinnedFinding.vulnName = joinnedFinding.Sink;

                        findingsResults.Add(joinnedFinding);
                    }
                }
                findingsViewer_JoinnedTraces.loadO2Findings(findingsResults);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in calculateJoinnedTraces");
                throw;
            }                     
        }

        private void updateTreeView(IEnumerable<string> sinks)
        {
            var treeView = findingsViewer_JoinnedTraces.getResultsTreeView();
            treeView.invokeOnThread(
                () =>
                    {
                        treeView.Nodes.Clear();
                        foreach (var sink in sinks)
                            treeView.Nodes.Add(sink);
                    });
        }

        public void refreshSinks()
        {
            findingsViewer_Sinks.refreshView();
        }

        public void refreshSources()
        {
            findingsViewer_Sources.refreshView();
        }

        public void refreshJoinnedTraces()
        {
            findingsViewer_JoinnedTraces.refreshView();
        }

        public List<IO2Finding> getFindingsIn_Sinks()
        {
            return findingsViewer_Sinks.getFindingsFromTreeView();
        }

        public List<IO2Finding> getFindingsIn_Sources()
        {
            return findingsViewer_Sources.getFindingsFromTreeView();
        }

        public IEnumerable<IO2Finding> getCurrentFindingsIn_JoinnedTraces()
        {
            return findingsViewer_JoinnedTraces.currentO2Findings;
        }

        public IEnumerable<string> getSinks(IEnumerable<O2Finding> findings)
        {
            return (from O2Finding finding in findings
                    orderby finding.Sink
                    where finding.Sink != ""
                    select finding.Sink).Distinct();
        }

        public IEnumerable<string> getSources(IEnumerable<O2Finding> findings)
        {
            return (from O2Finding finding in findings
                    orderby finding.Source
                    where finding.Source != ""
                    select finding.Source).Distinct();
        }
    }
}
