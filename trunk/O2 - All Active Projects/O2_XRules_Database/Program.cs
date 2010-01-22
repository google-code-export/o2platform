// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Core.FileViewers.Ascx;
using O2.Core.FileViewers.Ascx.O2Rules;
using O2.Core.FileViewers.Ascx.tests;
using O2.Core.XRules.Ascx;
using O2.Core.XRules.XRulesEngine;
using O2.External.SharpDevelop;
using O2.External.SharpDevelop.Ascx;
using O2.External.WinFormsUI.Forms;
using O2.ImportExport.OunceLabs;
using O2.Kernel.CodeUtils;
using O2.Kernel.Interfaces.Messages;
using O2.Kernel.Interfaces.Views;

using O2.Views.ASCX.O2Findings;
using O2.Core.CIR.Ascx;
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
            new Wizard_XRule_Exec_Simple().startWizard();
        }                
    }
}