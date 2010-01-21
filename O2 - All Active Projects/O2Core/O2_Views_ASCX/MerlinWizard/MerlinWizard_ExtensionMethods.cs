using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
//O2Tag_AddReferenceFile:Merlin.dll
using Merlin;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;

using O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods;


namespace O2.Views.ASCX.MerlinWizard
{
    public static class Ascx_ExtensionMethods
    {

		public static void append_Line(this IStep step, string message, bool extraLineAfter)
		{
			step.append_Line(message, extraLineAfter, false);
		}
		public static void append_Line(this IStep step, string message, bool extraLineAfter, bool extraLineBefore)
        {
        	if (extraLineBefore)
        		step.append_Line();
            step.append_Text(message + Environment.NewLine);
            if (extraLineAfter)
            	step.append_Line();
        }        
		public static void append_Line(this IStep step)
        {
            step.append_Text(Environment.NewLine);
        }
        // Misc helper functions
       

           

        
    }
}
