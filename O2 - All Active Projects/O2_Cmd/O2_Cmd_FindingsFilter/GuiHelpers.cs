// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.Cmd.FindingsFilter.Filters;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2CmdShell;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Kernel.Interfaces.Views;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Cmd.FindingsFilter
{
    public class GuiHelpers
    {        

      /*  public static void openScriptEditor()
        {

            
        }*/

        public static void gui()
        {
            O2Thread.mtaThread(
                () =>
                    {                        
                        if (O2AscxGUI.launch("Findings Filter GUI"))
                        {
                            openFindingsViewerAndSourceCodeEditor();
                            // enable opening findings file references
                            HandleO2MessageOnSD.setO2MessageFileEventListener();
                            /*KO2MessageQueue.getO2KernelQueue().onMessages += o2Message => HandleO2MessageOnSD.
                                                                                              o2MessageHelper_Handle_IM_FileOrFolderSelected
                                                                                              (o2Message,
                                                                                               null);*/

                        }
                    });
        }

        [O2CmdHide]
        public static void openFindingsViewerAndSourceCodeEditor()
        {
            var sourceCodeEditorControlName = "Scripts Editor for Findings Filter";
            var findingsViewerControlName = "Findings Filter";

            openFindingsFilterControl(findingsViewerControlName);

            openSourceCodeEditorControl(sourceCodeEditorControlName);

            loadSampleScripts(sourceCodeEditorControlName, typeof(CustomScripts));
            O2DockUtils.setDockContentPosition(findingsViewerControlName);
        }

        [O2CmdHide]
        public static void openFindingsFilterControl(string findingsViewerControlName)
        {
            O2AscxGUI.openAscx(typeof(Ascx.ascx_FindingsFilter), O2DockState.Document,
                                               findingsViewerControlName);
        }

        [O2CmdHide]
        public static void openSourceCodeEditorControl(string sourceCodeEditorControlName)
        {
            O2AscxGUI.openAscx(typeof(ascx_SourceCodeEditor), O2DockState.Document,
                                               sourceCodeEditorControlName);

        }

        [O2CmdHide]
        public static void loadSampleScripts(string findingsViewerControlName, Type typeWithSampleScripts)
        {
            findingsViewerControlName.invokeOnAscx("loadSampleScripts", new object[] {  typeWithSampleScripts});

            findingsViewerControlName.invokeOnAscx("compileSourceCode");
        }
    }
}
