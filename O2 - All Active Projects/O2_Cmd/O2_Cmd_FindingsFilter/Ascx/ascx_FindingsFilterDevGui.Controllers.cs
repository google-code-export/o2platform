// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using O2.Cmd.FindingsFilter.Filters;

namespace O2.Cmd.FindingsFilter.Ascx
{
    public partial class ascx_FindingsFilterDevGui
    {
        private bool runOnLoad = true;

        public void onLoad()
        {
            if (false == DesignMode && runOnLoad)
            {
                runOnLoad = false;
                loadCustomScripts();
            }
        }

        private void loadCustomScripts()
        {
            sourceCodeEditor.loadSampleScripts(typeof(CustomScripts));
            sourceCodeEditor.compileSourceCode();
            //findingsViewerControlName.invokeOnAscx("loadSampleScripts", new object[] { typeWithSampleScripts });

            //findingsViewerControlName.invokeOnAscx("compileSourceCode");
        }

        public Thread loadOzasmtFile(string fileToLoad)
        {
            return findingsFilter.loadOzasmtFile(fileToLoad);
        }
    }
}
