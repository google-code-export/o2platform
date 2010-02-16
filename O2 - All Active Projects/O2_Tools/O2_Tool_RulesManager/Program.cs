// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using O2.Cmd.FindingsFilter.Ascx;
using O2.Core.CIR.Ascx;
using O2.DotNetWrappers.DotNet;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;
using O2.Rules.OunceLabs.Ascx;
using O2.Scanner.OunceLabsCLI.Ascx;
using O2.Views.ASCX.DataViewers;
using O2.Views.ASCX.O2Findings;
using O2.Core.XRules.Ascx;

namespace O2.Tool.RulesManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string staticViewerControlName = "Findings Viewer";

            // this will make files to be opened on the main Document window   
            HandleO2MessageOnSD.setO2MessageFileEventListener(staticViewerControlName);

            // add load and save engines for Ounce Ozasmt file formats
            OunceAvailableEngines.addAvailableEnginesToControl(typeof(ascx_FindingsViewer));

            if (O2AscxGUI.launch("Rules Manager", 1000, 800))
            {
                O2AscxGUI.openAscx(typeof(ascx_Scan), O2DockState.DockLeft, "Scan");
                O2AscxGUI.openAscx(typeof(ascx_FindingsViewer), O2DockState.DockLeft, staticViewerControlName);
                O2DockUtils.setDocState("Scan", staticViewerControlName, DockStyle.Bottom);

                O2AscxGUI.openAscx(typeof(ascx_CirDataViewer), O2DockState.DockRight, "Cir Data Viewer");

                O2AscxGUI.openAscx(typeof(ascx_RulePackViewer), O2DockState.DockTop, "Rule Pack Viewer");

                O2DockUtils.setPaneHeight("Rule Pack Viewer", 500);

                O2AscxGUI.addControlToMenu(typeof(ascx_FindingsFilterDevGui), O2DockState.Document, "Fidings Filter");

                O2AscxGUI.addControlToMenu(typeof(ascx_SourceCodeEditor), O2DockState.Document, "Source Code Editor");
                O2AscxGUI.addControlToMenu(typeof(ascx_FunctionsViewer), O2DockState.Document, "Functions Signatures");
                O2AscxGUI.addControlToMenu(typeof(ascx_ApplyRulesToFindings), O2DockState.Document, "O2 'Call-Flow Scanner' (Apply Rules To Findings)");
                O2AscxGUI.openAscx(typeof(ascx_XRules_Editor), O2DockState.Document, "O2 XRules Editor");
 
            }
        }
    }
}
