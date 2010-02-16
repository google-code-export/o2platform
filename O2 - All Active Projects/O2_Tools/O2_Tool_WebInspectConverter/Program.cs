// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs;
using O2.Interfaces.Views;
using O2.Tool.WebInspectConverter.ascx;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.WebInspectConverter
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //SpringExec.LoadConfigAndStartGUI("WebInspect.xml");
            //SpringExec.loadDefaultConfigFile();

            //ascx_WebInspectOzasmMapperGui
            //ascx_WebInspectPoC

            OunceAvailableEngines.addAvailableEnginesToControl(typeof(ascx_FindingsViewer));

            if (O2AscxGUI.launch())
            {
                O2AscxGUI.openAscx(typeof(ascx_WebInspectPoC), O2DockState.DockTop, "ascx_WebInspectPoC");
                O2AscxGUI.openAscx(typeof(ascx_WebInspectOzasmMapperGui), O2DockState.Document, "ascx_WebInspectOzasmMapperGui");
                //O2AscxGUI.openAscx(typeof(ascx_FindingEditor), O2DockState.DockLeftAutoHide, "ascx_FindingEditor");
                //O2AscxGUI.openAscx(typeof(ascx_FindingsViewer), O2DockState.DockLeftAutoHide, "ascx_FindingsViewer");                   
            }
            //new O2DockPanel(typeof(ascx_WebInspectOzasmMapperGui));
                

            /*         Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var controlsToLoad = new List<O2DockContent>
                                     {                          
                                         new O2DockContent(typeof (ascx_FindingEditor), DockState.DockLeftAutoHide),
                                         new O2DockContent(typeof (ascx_WebInspectPoC), DockState.DockBottomAutoHide),

                                         new O2DockContent(typeof (ascx_Directory), DockState.DockLeftAutoHide),
                                         new O2DockContent(typeof (ascx_LogViewer), DockState.DockRightAutoHide),
                                         
                                         new O2DockContent(typeof (ascx_WebInspectOzasmMapperGui), DockState.Document)
                                     };
        

        O2DockPanel.openO2DockContentInNewDockPanel(controlsToLoad);     */
        }
    }
}
