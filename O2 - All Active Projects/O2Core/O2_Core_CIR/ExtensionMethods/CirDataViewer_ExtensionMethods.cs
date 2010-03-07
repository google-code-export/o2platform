using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;
using O2.Core.CIR.Ascx;
using O2.Core.CIR.ExtensionMethods;
using O2.Interfaces.CIR;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.Ascx.MainGUI;
using O2.Core.CIR.CirObjects;

namespace O2.Core.CIR.ExtensionMethods
{
    public static class CirDataViewer_ExtensionMethods
    {
        public static ascx_CirDataViewer show(this ICirData cirData)
        {
            return cirData.show(1);
        }

        public static ascx_CirDataViewer show(this ICirData cirData, int namespaceDepth)
        {
            var cirDataViewer = typeof(ascx_CirDataViewer).openControlAsForm<ascx_CirDataViewer>("CirData", 500, 300);
            cirDataViewer.namespaceDepth(namespaceDepth);
            cirDataViewer.loadCirData(cirData);
            cirDataViewer.showLoadedFunctions();
            return cirDataViewer;
        }

        public static ascx_CirDataViewer show(this ICirClass cirClass)
        {
            var cirData = new CirData();
            cirData.dClasses_bySignature.Add(cirClass.Name, cirClass);
            var cirDataViewer = cirData.show(0);
            return cirDataViewer;
        }

        public static ascx_CirDataViewer namespaceDepth(this ascx_CirDataViewer cirDataViewer, int namespaceDepth)
        {
            var functionViewer = cirDataViewer.getFunctionsViewerControl();
            functionViewer.setNamespaceDepth(namespaceDepth);
            return cirDataViewer;
        }  
    }
}
