using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Merlin;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;

namespace O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods
{
    public static class EX_O2_Ascx
    {

        public static IStep add_Directory(this List<IStep> steps, string stepName)
        {
            return steps.add_Directory(stepName, PublicDI.config.O2TempDir);
        }

        public static IStep add_Directory(this List<IStep> steps, string stepName, string startDirectory)
        {
            var directory = new ascx_Directory();
            directory.AllowDrop = false;    // to deal with the "DragDrop registration did not succeed" problem
            directory._ViewMode = ascx_Directory.ViewMode.Simple_With_LocationBar;
            directory._HideFiles = true;
            directory.openDirectory(startDirectory);
            directory.refreshDirectoryView();
            directory._WatchFolder = true;
            var newStep = new TemplateStep(directory, 0, stepName);
            steps.Add(newStep);
            return newStep;
        }

        public static IStep add_Message(this List<IStep> steps, string stepTitle, string message)
        {

            var newStep = Ex_Windows_Forms.createStepWithTextBox(stepTitle, message);
            steps.Add(newStep);
            return newStep;
        }

        public static IStep add_Action(this List<IStep> steps, string stepTitle, Action<IStep> action)
        {
            var textBox = Ex_Windows_Forms.createTextBox("");
            var newStep = Ex_Windows_Forms.createStepWithTextBox(stepTitle, textBox);

            //newStep.NextHandler = ()=>  action(textBox);
            newStep.OnComponentAction = action;
            steps.Add((IStep)newStep);
            return newStep;
        }


    }
}
