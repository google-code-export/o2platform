// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.Cmd.SpringMvc.Objects;
using O2.Core.CIR.Ascx;
using O2.Core.CIR.CirObjects;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.CIR;

namespace O2.Cmd.SpringMvc.Classes
{
    public class CirViewingUtils
    {
        public static void openCirDataFileInCirViewerControl(ICirDataAnalysis cirDataAnalysis, string cirViewerControlName)
        {
            if (cirDataAnalysis != null)
            {
                var ascxCirViewer = (ascx_CirDataViewer) O2AscxGUI.getAscx(cirViewerControlName);
                if (ascxCirViewer != null)
                {
                    ascxCirViewer.loadCirDataAnalysisObject(cirDataAnalysis);
                    ascxCirViewer.updateCirDataStats();
                }
            }

        }

       
    }
}
