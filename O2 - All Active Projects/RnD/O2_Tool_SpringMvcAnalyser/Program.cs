// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Reflection;
using System.Windows.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.RnD.SpringMVCAnalyzer.ascx;

namespace O2.RnD.SpringMVCAnalyzer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(String[] asArgs)
        {
            // use this to ensure the correct dlls are loaded
            /*if (false == o2.ounce.core.DependentDlls.ensureDependentDllsExist())
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (asArgs.Length > 0)
                O2.execControl(asArgs[0], Assembly.GetExecutingAssembly());
            else
                O2.execControl("Spring MVC Viewer", typeof (ascx_SpringMvcAnalyzer));*/

            new O2DockPanel(typeof (ascx_SpringMvcAnalyzer));
        }
    }
}
