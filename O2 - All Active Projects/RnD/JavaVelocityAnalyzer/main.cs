// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using O2.External.WinFormsUI.Forms;
using O2.External.WinFormsUI.O2Environment;
using O2.Rnd.JavaVelocityAnalyzer.ascx;

namespace O2.Rnd.JavaVelocityAnalyzer
{
    internal static class main
    {                
        private static void Main()
        {            
            if (O2AscxGUI.launch("Java Velocity Parser"))
            {
                O2AscxGUI.openAscx(typeof (ascx_JavaVelocityAnalyzer));
            }            
        }
    }
}
