using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Scanner.OunceLabsCLI.Ascx
{
    partial class ascx_ScanWizard
    {
        bool runOnLoad = true;

        public void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                directoryWithResults.openDirectory(DI.config.getTempFolderInTempDirectory("_Scan_Wizard_Results"));
                runOnLoad = false;
            }
        }
    }
}
