// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.External.SharpDevelop;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.Misc.AppScanDE;
using O2.ImportExport.Misc.CodeCrawler;
using O2.ImportExport.Misc.FindBugs;
//using O2.ImportExport.Misc.Fortify;
using O2.ImportExport.Misc.WebScarab;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6_1;
using O2.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.FindingsViewer
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            KO2MessageQueue.getO2KernelQueue().onMessages += 
                o2Message => HandleO2MessageOnSD.o2MessageHelper_Handle_IM_FileOrFolderSelected(o2Message, DI.staticViewerControlName); 

            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6_1());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssesmentLoad_FindBugs());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssesmentLoad_CodeCrawler());
            //ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssesmentLoad_Fortify());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssesmentLoad_AppScanDE());
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssesmentLoad_WebScarab());
            
            ascx_FindingsViewer.o2AssessmentSave = new O2AssessmentSave_OunceV6();

            if (O2AscxGUI.launch("Findings Viewer"))
            {
                O2AscxGUI.openAscx(typeof (ascx_FindingsViewer), O2DockState.Document, DI.staticViewerControlName);
                //O2AscxGUI.openAscx(typeof(ascx_Scripts), O2DockState.Document, "Scripts");
            }
        }
        
    }
}
