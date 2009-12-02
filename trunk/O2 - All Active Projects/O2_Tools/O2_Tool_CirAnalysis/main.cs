using System;
using O2.Core.CIR.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Views;


namespace O2.Tool.CirAnalysis
{
    internal static class main
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        private static void Main()
        {
            if (O2AscxGUI.launch("Cir Analysis"))
            {
                // use during active development to make the LogViewer visible
                //O2AscxGUI.setLogViewerDockState(O2DockState.DockBottom); 

                // load controls
                O2AscxGUI.openAscx(typeof(ascx_CirCreator), O2DockState.DockBottomAutoHide, "Cir Creator");                                
                O2AscxGUI.openAscx(typeof(ascx_CirViewer_CirData), O2DockState.Document, "Cir Viewer (Legacy Version)");
                O2AscxGUI.openAscx(typeof(ascx_CirDataViewer), O2DockState.Document, "Cir Viewer");
                O2AscxGUI.openAscx(typeof(ascx_CirAnalysis), O2DockState.DockTop, "Cir Analysis");
                // load O2Kernel in CirAnalysis
                var cirAnalysis = (ascx_CirAnalysis) O2AscxGUI.getAscx("Cir Analysis");
                cirAnalysis.loadO2CirDataFile(DI.config.ExecutingAssembly);    

            }            
        }
    }
}