// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Views.ASCX.O2Findings;

namespace O2.Core.CIR.Ascx
{
    public partial class ascx_FindingsSplitter
    {
        private bool runOnLoad = true;
        private void onLoad()
        {
            if (runOnLoad && DesignMode == false)
            {
                
            }
        }
        public ascx_FindingsViewer getFindingsViewer_toProcess()
        {
            return findingsViewer_ToProcess;
        }


        public ascx_CirDataViewer getCirDataViewer_ToProcess()
        {
            return cirDataViewer_ToProcess;
        }

        
    }
}
