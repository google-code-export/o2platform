using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Merlin;
using FluentSharp.O2.DotNetWrappers.Windows;

namespace FluentSharp.O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods
{
    public static class Ex_Misc_Utils
    {
        public static void sleep(this IStep step, int value)
        {
            Processes.Sleep(value);
        }
    }
}
