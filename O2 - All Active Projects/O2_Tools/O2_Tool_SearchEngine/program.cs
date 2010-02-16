// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Core.CIR.Ascx.DotNet;
using O2.Core.FileViewers.Ascx;
using O2.Core.FileViewers.Ascx.O2Rules;
using O2.Core.FileViewers.Ascx.tests;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs;
using O2.Interfaces.Views;
using O2.Tool.SearchEngine.Ascx;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.DataViewers;
using O2.Views.ASCX.O2Findings;

namespace O2.Tool.SearchEngine
{
    internal static class program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        private static void Main()
        {
            // this will make files to be opened on the main Document window   
            HandleO2MessageOnSD.setO2MessageFileEventListener();        

            if (O2AscxGUI.launch("O2 Tool - Search Engine"))
            // O2Messages.openGUI();
            {
                var fileMappings = (ascx_FileMappings)O2AscxGUI.openAscx(typeof (ascx_FileMappings), O2DockState.DockLeft, "File Mappings");

                //fileMappings.loadFilesFromFolder(DI.config.CurrentExecutableDirectory);
             //   O2Messages.openControlInGUI(typeof (ascx_TextSearch), O2DockState.Document, "Text Search");

                O2AscxGUI.openAscx(typeof(ascx_SearchTargets), O2DockState.DockRight, "Search Targets");                               
                O2AscxGUI.openAscx(typeof (ascx_SearchResults), O2DockState.DockTop, "Search Results");

                O2AscxGUI.addControlToMenu(typeof(ascx_FunctionsViewer), O2DockState.Document, "Signatures Viewer");

                OunceAvailableEngines.addAvailableEnginesToControl(typeof (ascx_FindingsViewer));
                //ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
                O2AscxGUI.addControlToMenu(typeof(ascx_FindingsViewer), O2DockState.Document, "Findings Viewer");
                O2AscxGUI.addControlToMenu(typeof(ascx_SourceCodeEditor), O2DockState.Document, "Source Code Editor");

                O2AscxGUI.addControlToMenu(typeof (ascx_TilesDefinition_xml));
                O2AscxGUI.addControlToMenu(typeof(ascx_J2EE_web_xml));
                O2AscxGUI.addControlToMenu(typeof (ascx_Validation_xml));
                O2AscxGUI.addControlToMenu(typeof (ascx_Struts_config_xml));
                O2AscxGUI.addControlToMenu(typeof (ascx_StrutsMappings_ManualMapping));
                O2AscxGUI.addControlToMenu(typeof (ascx_DotNet_Dependencies));
                                

                O2AscxGUI.addControlToMenu(typeof(ascx_O2Rules_Struts),O2DockState.Document, "O2 Rules Struts");
                                
                

            }

            /*var o2Message = new KO2GenericMessage("Text Search") {messageType = O2MessageType.AddControlToGUI};
            o2Message.messageData.Add(typeof(ascx_TextSearch));
            o2Message.messageData.Add(O2DockState.Document);


            DI.o2MessageQueue.sendMessage(o2Message);

            DI.o2MessageQueue.sendMessage(new KO2GenericMessage("Search Criteria", O2MessageType.AddControlToGUI,
                                                         new List<object>
                                                             {typeof (ascx_SearchCriteria), O2DockState.DockLeft}));*/
            /*DI.windowsForms.openAscx(typeof(ascx_TextSearch),O2DockState.Document,"Text Search");
            DI.windowsForms.openAscx(typeof(ascx.ascx_SearchCriteria), O2DockState.DockLeft,"Search Criteria");
            DI.windowsForms.openAscx(typeof(ascx.ascx_SearchTargets), O2DockState.DockLeft, "Search Targets");
            DI.windowsForms.openAscx(typeof(ascx.ascx_SearchResults), O2DockState.Document,"Search Results");
            DI.windowsForms.openAscx(typeof(ascx_TextSearch), O2DockState.Document, "Text Search");
            DI.windowsForms.WaitForGuiClose();*/
          
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            /*String sFormTitle = core.Exec.getFormTitle_forClickOnce("Text Search");
            Exec.execControl(sFormTitle, typeof (ascx_TextSearch));*/

            

            

           /* DI.log = new WinFormsUILog();


            if (DI.config.setDI("O2_Views_ASCX.dll", "O2.Views.ASCX.DI", "windowsForms", new WindowsFormsImpl()) &&
                DI.config.setDI("O2_Views_ASCX.dll", "O2.Views.ASCX.DI", "log", new WinFormsUILog())) 
            {
                //Views.ASCX.DI.assemblyAnalysis = new WindowsFormsImpl();            
                var logViewer = new O2DockContent(typeof (ascx_LogViewer));
                var textSearch = new O2DockContent(typeof (ascx_TextSearch));

                new O2DockPanel(new List<O2DockContent> {logViewer, textSearch});
            }
            else
                DI.log.reportCriticalErrorToO2Developers(null, null, "in Main, there was a problem setting 'windowsForms' DI");
            * */
        }
    }
}
