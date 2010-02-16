// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Cmd.SpringMvc.Ascx;
using O2.Cmd.SpringMvc.Scripts;
using O2.Core.CIR.Ascx;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2CmdShell;
using O2.External.Python.Ascx;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.ImportExport.OunceLabs;
using O2.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;
using O2.Views.ASCX.O2Findings;

namespace O2.Cmd.SpringMvc
{
    public class Gui
    {        

        /*  public static void openScriptEditor()
        {

            
        }*/

        public static void gui()
        {
            O2Cmd.log.write("Opening up GUI");
            O2Thread.mtaThread(
                () =>
                    {
                        OunceAvailableEngines.addAvailableEnginesToControl(typeof(ascx_FindingsViewer));
                        if (O2AscxGUI.launch("O2 Spring MVC"))
                        {
                            HandleO2MessageOnSD.setO2MessageFileEventListener();                            

                            //O2AscxGUI.addControlToMenu(typeof (ascx_SpringMvcAnalyzer), O2DockState.Document,
                            //                           "Experimental - Spring MVC Analyzer");
                            O2AscxGUI.addControlToMenu(typeof(ascx_PythonCmdShell), O2DockState.Document,
                                                       "Experimental - Python Cmd Shell");

                            O2AscxGUI.addControlToMenu(typeof(ascx_Scripts));

                            O2AscxGUI.openAscx(typeof(ascx_CirDataViewer), O2DockState.DockRight, "Cir Viewer");



                            O2AscxGUI.openAscx(typeof(ascx_ExploitSpringMvc), O2DockState.Document, "Exploit Spring MVC");

                            O2AscxGUI.openAscx(typeof(ascx_CreateSpringMvcMappings),O2DockState.Document, "Create Spring MVC Mappings");

                            //O2AscxGUI.openAscx(typeof(ascx_JoinControllersWithFindings),O2DockState.DockRight, "Cir Viewer");

                            

                            O2AscxGUI.addControlToMenu(typeof(ascx_FindingsViewer));                                               
                            
                            //O2AscxGUI.addControlToMenu(typeof(ascx_SourceCodeEditor));
                            //openFindingsViewerAndSourceCodeEditor();
                            // enable opening findings file references
                            
                            /*KO2MessageQueue.getO2KernelQueue().onMessages += o2Message => HandleO2MessageOnSD.
                                                                                              o2MessageHelper_Handle_IM_FileOrFolderSelected
                                                                                              (o2Message,
                                                                                               null);*/
                            
                        }
                    });
            O2AscxGUI.waitForAscxGuiClose();
        }

        /*[O2CmdHide]
        public static void openFindingsViewerAndSourceCodeEditor()
        {
            var sourceCodeEditorControlName = "Scripts Editor for Findings Filter";
            var findingsViewerControlName = "Findings Filter";

            //openFindingsFilterControl(findingsViewerControlName);

            //openSourceCodeEditorControl(sourceCodeEditorControlName);

            //loadSampleScripts(sourceCodeEditorControlName, typeof(CustomScripts));
            O2DockUtils.setDockContentPosition(findingsViewerControlName);
        }*/

        //[O2CmdHide]
   /*     public static void openFindingsFilterControl(string findingsViewerControlName)
        {
            O2AscxGUI.openAscx(typeof(Ascx.ascx_FindingsFilter), O2DockState.Document,
                               findingsViewerControlName);
        }

        [O2CmdHide]
        public static void openSourceCodeEditorControl(string sourceCodeEditorControlName)
        {
            O2AscxGUI.openAscx(typeof(ascx_SourceCodeEditor), O2DockState.Document,
                               sourceCodeEditorControlName);

        }*/

        /*[O2CmdHide]
        public static void loadSampleScripts(string findingsViewerControlName, Type typeWithSampleScripts)
        {
            findingsViewerControlName.invokeOnAscx("loadSampleScripts", new object[] {  typeWithSampleScripts});

            findingsViewerControlName.invokeOnAscx("compileSourceCode");
        }*/
    }
}
