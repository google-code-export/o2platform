using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Scanner.DotNet._TestScripts;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.Scanner.DotNet.PostSharp;

namespace O2.Scanner.DotNet.Ascx
{
    partial class ascx_PatchAndMonitor
    {
        bool runOnLoad = true;
        public string hookedAssembly = "";

        public void onLoad()
        {
            if (DesignMode == false && runOnLoad)
            {
                loadTestScript();
                runOnLoad = false;                
            }
        }

        private void loadTestScript()
        {
            var testScript = Path.Combine(DI.config.O2TempDir, "TestScript_MultipleCalls.cs");
            if (false == File.Exists(testScript))
                Files.WriteFileContent(testScript, TestScripts.TestScript_MultipleCalls);
            sourceCodeEditor.loadSourceCodeFile(testScript);
            sourceCodeEditor.compileSourceCode();
            var assembly = sourceCodeEditor.compiledAssembly;
            cirDataViewer.loadFile(assembly.Location);
            
        }

        private void insertHooksIntoNewAssembly()
        {
            var sourceAssembly = sourceCodeEditor.compiledAssembly.Location;
            var hookedAssembly = DI.config.getTempFileInTempDirectory(Path.GetFileName(sourceAssembly));
            PostSharpExecution.InsertHooksAndRunPostSharpOnAssembly(sourceAssembly, hookedAssembly);
        } 
    }
}
