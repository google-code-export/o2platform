using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Merlin;
using O2.DotNetWrappers.DotNet;

namespace O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods
{
    public static class Ex_Wizard_GUI
    {
        public static void allowNext(this IStep step, bool value)
        {
            step.Controller.wizardForm.invokeOnThread(() => step.Controller.allowNext(value));
        }

        public static void allowBack(this IStep step, bool value)
        {
            step.Controller.wizardForm.invokeOnThread(() => step.Controller.allowBack(value));
        }

        public static void allowCancel(this IStep step, bool value)
        {
            step.Controller.wizardForm.invokeOnThread(() => step.Controller.allowCancel(value));
        } 
    }
}
