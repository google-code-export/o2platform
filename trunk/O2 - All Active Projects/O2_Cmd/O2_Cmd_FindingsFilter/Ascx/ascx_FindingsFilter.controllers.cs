// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Kernel;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.O2Findings;
using O2.Views.ASCX.O2Findings;

namespace O2.Cmd.FindingsFilter.Ascx
{
    public partial class ascx_FindingsFilter
    {        

        private bool execOnLoad = true;

        private void beforeInitializeComponent()
        {
            // load engines
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
            ascx_FindingsViewer.o2AssessmentSave = new O2AssessmentSave_OunceV6();
        } 

        private void onLoad()
        {
            if (execOnLoad)
            {
                PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;
                findingsViewer_SourceFindings.loadO2Assessment(@"SampleOzasmt\6.0.ozasmt");
              //  loadFilters(typeof(Filters.Ozasmt));              
                execOnLoad = false;
            }            
        }

        void o2MessageQueue_onMessages(IO2Message o2Message)
        {
            if (o2Message is IM_DotNetAssemblyAvailable)
            {
                var dotNetAssemblly = (IM_DotNetAssemblyAvailable) o2Message;
                var types = DI.reflection.getTypes(DI.reflection.getAssembly(dotNetAssemblly.pathToAssembly));
                if (types.Count() > 0)
                    loadFilters(types[0]);
            }
        }

        public void loadFilters(Type filtersToLoad)
        {
            tvAvailableFilters.invokeOnThread(
                () =>
                    {
                        tvAvailableFilters.Nodes.Clear();
                        //foreach (var methodAvailable in DI.reflection.getMethods(filtersToLoad, BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
                        foreach (
                            var methodAvailable in
                                DI.reflection.getMethods(filtersToLoad,
                                                         BindingFlags.NonPublic | BindingFlags.Static |
                                                         BindingFlags.DeclaredOnly))
                            if (methodAvailable.GetParameters().Count() == 1 &&
                                methodAvailable.GetParameters()[0].ParameterType.Name == "IO2Assessment")
                                O2Forms.newTreeNode(tvAvailableFilters.Nodes, methodAvailable.Name,
                                                    methodAvailable.ToString(), 0,
                                                    methodAvailable);
                    });
        }
        

        private void applyFilter(MethodInfo filterToApply)
        {
            try
            {

                var tempO2Assessment = new O2Assessment
                                           {
                                               o2Findings = findingsViewer_SourceFindings.currentO2Findings
                                           };
                var methodParams = new object[] {tempO2Assessment};
                var filteredO2Findings = (IEnumerable<IO2Finding>) filterToApply.Invoke(null, methodParams);
                if (filteredO2Findings == null)
                    filteredO2Findings = new List<IO2Finding>();
                findingsViewer_Results.loadO2Findings(filteredO2Findings.ToList(), true);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in applyFilter", true);
            }            
        }

        public Thread loadOzasmtFile(string fileToLoad)
        {
            return findingsViewer_SourceFindings.loadO2Assessment(fileToLoad);
        }
    }
}
