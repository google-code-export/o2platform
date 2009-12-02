// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using O2.DotNetWrappers.Windows;
using O2.External.O2Mono;
using O2.External.O2Mono.Ascx;
using O2.External.WinFormsUI.O2Environment;

namespace O2.Tool.O2reflector
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // set DI's needed 
            DI.config.setDI("O2_Views_ASCX.dll", "DI", "assemblyAnalysis", new AssemblyAnalysisImpl());
            DI.config.setDI("O2_Views_Controlers.dll", "DI", "monoCecil", new O2MonoCecil());
            DI.config.setDI("O2_Views_Controlers.dll", "DI", "reflectionASCX", new O2FormsReflectionASCX());
            

            //SpringExec.loadDefaultConfigFile();
            new O2DockPanel(typeof(ascx_O2Reflector));
        }
    }
}
