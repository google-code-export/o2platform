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