// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Core.CIR.CirUtils;
using O2.DotNetWrappers.DotNet;
using O2.Kernel.Interfaces.CIR;

namespace O2.Core.CIR.Ascx
{
	public partial class ascx_CirTrace
	{
        public ICirFunction rootCirFunction { get; set; }

        public void viewCirFunction(ICirFunction _rootCirFunction)
        {
            this.invokeOnThread(
                () =>
                    {
                        rootCirFunction = _rootCirFunction;
                        cirTreeView.Nodes.Clear();
                        ViewHelpers.addCirFunctionToTreeNodeCollection(rootCirFunction, "", cirTreeView.Nodes, true);
                        createTracesAndShowIt(rootCirFunction);
                        cirTreeView.ExpandAll();
                    });
                                               
        }

        private void createTracesAndShowIt(ICirFunction cirFunction)
        {
            traceTreeView.loadO2Finding(CirTraces.createO2FindingFromCirFunction(cirFunction));
        }
	}
}
