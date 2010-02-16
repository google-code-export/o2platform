// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)


using O2.Core.CIR.Ascx;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs;
using O2.Interfaces.Views;
using O2.Tool.O2Scripts._ScriptSamples;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.DataViewers;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.O2Scripts
{
    class Program
    {
        static void Main(string[] args)
        {
            OunceAvailableEngines.addAvailableEnginesToControl(typeof(ascx_FindingsViewer));

            if (O2AscxGUI.launch("O2 Scripts"))
            {

                var scriptsFolder = (ascx_ScriptsFolder)O2AscxGUI.openAscx(typeof(ascx_ScriptsFolder), O2DockState.DockLeft, "Sample Scripts");
                scriptsFolder.loadSampleScripts(new ScriptSamples(),true);

                var sourceCodeEditor = (ascx_SourceCodeEditor)O2AscxGUI.openAscx(typeof (ascx_SourceCodeEditor), O2DockState.Document, "Script Editor");
                sourceCodeEditor.loadSampleScripts();

             /*   O2AscxGUI.addControlToMenu("O2 Object Model", () =>
                                                                  {
                                                                      var cirDataViewer = (ascx_CirDataViewer)O2AscxGUI.openAscx(typeof(ascx_CirDataViewer), O2DockState.DockRight, "O2 Object Model");
                                                                      cirDataViewer.loadO2ObjectModel();
                                                                      //DI.log.info("code executed")
                                                                  });
              */ 
                //
                O2AscxGUI.addControlToMenu(typeof(ascx_FunctionsViewer), O2DockState.Document, "Functions Viewer");

                // preloaded menu items                
                O2AscxGUI.addControlToMenu(typeof(ascx_FileMappings), O2DockState.Document, "File Mappings");

                O2AscxGUI.addControlToMenu(typeof(ascx_FindingsViewer), O2DockState.Document, "Findings Viewer");                                
                HandleO2MessageOnSD.setO2MessageFileEventListener();        // set this up so that we can open files from the Findings Viewer
            }
        }
    }
}
