// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;

namespace O2.XRules.Database
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {

            ascx_Execute_Scripts.startControl();
            //typeof(ascx_Execute_Scripts).showAsForm("Execute *.cs *.o2 and *.cs scripts", 400,400);
            //new Wizard_XRule_Exec_Simple().startWizard();
        }                
    }
}