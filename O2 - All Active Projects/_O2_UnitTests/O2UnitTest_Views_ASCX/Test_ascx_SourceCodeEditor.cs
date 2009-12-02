using NUnit.Framework;
using O2.DotNetWrappers.Windows;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;

namespace O2.UnitTests.Test_O2ViewsASCX
{
    [TestFixture]
    public class Test_ascx_SourceCodeEditor
    {
        private const string sourceCodeEditorControl = "Source Code Editor";

        private const string hardCodedPathToSampleScriptToEdit = @"E:\O2\_SourceCode_O2\O2_Tools\O2_Tool_FindingsQuery\_ScriptSamples\A_OzamtManipulation.cs";
        //@"E:\O2\_SourceCode_O2\O2Core\O2_Core_Ozasmt\_ScriptSamples\A_OzamtManipulation.cs";
        //private string SourceCodeFile = Path.Combine(DI.config.O2TempDir, "_3_OzamtManipulation.cs");

        [SetUp, Ignore("Test not completed")]
        public void openGui()
        {
            // use this to use the main O2 GUi
           /* O2AscxGUI.launch();
            O2AscxGUI.openAscx(typeof (ascx_SourceCodeEditor), O2DockState.Document,sourceCodeEditorControl); */
            // use this to only open the ascx_SourceCodeEditor control
            O2AscxGUI.openAscxAsForm(typeof(ascx_SourceCodeEditor), sourceCodeEditorControl);
            Assert.That(O2AscxGUI.isAscxLoaded(sourceCodeEditorControl), "sourceCodeEditorControl was not loaded");

            //sourceCodeEditorControl.invokeOnAscx("loadSampleScripts", new object[] {typeof (OzasmtScriptSamples)});
        //    Assert.That(((List<string>)sourceCodeEditorControl.invokeOnAscx("getSampleScriptsNames")).Count > 0, "getSampleScriptsNames.Count == 0 ");



            O2AscxGUI.invokeOnAscxControl(sourceCodeEditorControl, "loadSourceCodeFile", new object[] { hardCodedPathToSampleScriptToEdit });

            //Processes.Sleep(1000);
            /*var sourceCodeFromAscxControl = (string) O2AscxGUI.invokeOnAscxControl(sourceCodeEditorControl, "getSourceCode");
            Assert.That(sourceCodeFromAscxControl == OzasmtScriptSamples._1_OzamtManipulation,
                        "Source code was not correctly loaded");*/
        }

        [Test]
        public void compileAndRun()
        {
        }
       
        [TearDown]
        public void closeGui()
        {
            
            O2AscxGUI.closeAscxParent(sourceCodeEditorControl);
            //O2AscxGUI.waitForAscxGuiClose();
        }

    }
}
