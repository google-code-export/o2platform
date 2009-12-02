// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.External.IKVM;
using O2.External.IKVM.Ascx;
using O2.External.IKVM.ScriptSamples;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.Kernel.Interfaces.Views;

namespace O2.Tool.JavaExecution
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
             // load python execution engines
            IKVMEngine.addCurrentIKVMEnginesTo_ascx_SourceCodeEditor();
            
            // make setO2MessageFileEventListener
            HandleO2MessageOnSD.setO2MessageFileEventListener();

            if (O2AscxGUI.launch("O2 Tool - Java Execution"))
            {
                //O2AscxGUI.openAscx(typeof (ascx_JavaExecution));
                //O2AscxGUI.openAscx(DI.reflection.getType("O2_External_SharpDevelop.dll", "ascx_Scripts"));

                var scriptsFolder = (ascx_ScriptsFolder)O2AscxGUI.openAscx(typeof(ascx_ScriptsFolder), O2DockState.DockLeft, "scripts folder"); 
                scriptsFolder.loadSampleScripts(new JavaSamples(),true);

                O2AscxGUI.openAscx(typeof (ascx_JavaExecution));
            }
        }
    }
}
