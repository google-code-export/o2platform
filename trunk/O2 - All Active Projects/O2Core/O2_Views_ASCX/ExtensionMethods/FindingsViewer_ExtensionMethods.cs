using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Views.ASCX.O2Findings;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;

namespace O2.Views.ASCX.ExtensionMethods
{
    public static class FindingsViewer_ExtensionMethods
    {
        public static ascx_FindingsViewer add_FindingsViewer(this Control control)
        {
            "O2_ImportExport_OunceLabs.dll".assembly()
                                           .type("OunceAvailableEngines")
                                           .invokeStatic(addAvailableEnginesToControl, new object[] {typeof(ascx_FindingsViewer)});
            var findingsViewer = control.add_Control<ascx_FindingsViewer>();
            return findingsViewer;
        }

        public static List<IO2Finding> o2Findings(this ascx_FindingsViewer findingsViewer)
        {
            return findingsViewer.getFindingsFromTreeView();
        }

        public static ascx_FindingsViewer show(this ascx_FindingsViewer findingsViewer, List<IO2Finding> o2Findings)
        {
            findingsViewer.clearO2Findings();
            findingsViewer.loadO2Findings(o2Findings);
            return findingsViewer;
        }

        public static string save(this ascx_FindingsViewer findingsViewer)
        {
            return O2.XRules.Database._Rules.XUtils_Findings_v0_1.saveFindings(findingsViewer.o2Findings());
        }
    }
}
