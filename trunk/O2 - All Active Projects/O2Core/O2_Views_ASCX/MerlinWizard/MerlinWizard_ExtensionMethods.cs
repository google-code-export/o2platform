using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
//O2Tag_AddReferenceFile:Merlin.dll
using Merlin;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using System.Threading;



namespace O2.Views.ASCX.MerlinWizard
{
    public static class Ascx_ExtensionMethods
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
    	    	
    	public static Thread show(this List<IStep> steps, string wizardName)
    	{
    		return MerlinUtils.runWizardWithSteps(steps, wizardName);
    	}      

        public static Thread startWizard(this List<IStep> steps, string wizardName)
        {
            return MerlinUtils.runWizardWithSteps(steps, wizardName);
        }

        public static TextBox createTextBox(string message)
        {
            var textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.ReadOnly = true;
            textBox.Text = message;
            textBox.Select(0, 0);
            return textBox;
        }

        public static TemplateStep createStepWithTextBox(string stepTitle, string message)
        {
            var textBox = createTextBox(message);
            return createStepWithTextBox(stepTitle, textBox);
        }

        public static TemplateStep createStepWithTextBox(string stepTitle, TextBox textBox)
        {
            var newStep = new TemplateStep(textBox, 10, stepTitle);
            return newStep;
        }

        public static IStep add_Message(this List<IStep> steps, string stepTitle, string message)
        {

            var newStep = createStepWithTextBox(stepTitle, message);
            steps.Add(newStep);
            return newStep;
        }

        public static IStep add_Action(this List<IStep> steps, string stepTitle, Action<IStep> action)
        {
            var textBox = createTextBox("");
            var newStep = createStepWithTextBox(stepTitle, textBox);

            //newStep.NextHandler = ()=>  action(textBox);
            newStep.OnComponentAction = action;
            steps.Add((IStep)newStep);
            return newStep;
        }

        // misc business logic 
        public static string getPathFromStep(this IStep step, int stepId)
        {
            if (step.Controller.steps.Count > stepId)
            {
                var firstControl = step.Controller.steps[stepId].FirstControl;
                if (firstControl != null && firstControl is ascx_Directory)
                {
                    var directory = (ascx_Directory)firstControl;
                    return directory.getCurrentDirectory();
                }
            }
            return "";
        }



        // Misc helper functions
        public static void sleep(this IStep step, int value)
        {
            Processes.Sleep(value);
        }

        public static void allowNext(this IStep step, bool value)
        {
            step.Controller.wizardForm.invokeOnThread(() => step.Controller.allowNext(value));
        }

        public static void allowBack(this IStep step, bool value)
        {
            step.Controller.wizardForm.invokeOnThread(() => step.Controller.allowBack(value));
        }

        public static void setText(this IStep step, string message)
        {
            if (step.FirstControl != null)
                step.Controller.wizardForm.invokeOnThread(() => step.FirstControl.Text = message);
        }

        public static void appendText(this IStep step, string messageFormat, params Object[] variables)
        {
            step.appendText(string.Format(messageFormat, variables));
        }

        public static void appendLine(this IStep step, string messageFormat, params Object[] variables)
        {
            step.appendText(string.Format(messageFormat + Environment.NewLine, variables));
        }

        public static void appendLine(this IStep step, string message)
        {
            step.appendText(message + Environment.NewLine);
        }

        public static void appendText(this IStep step, string message)
        {
            if (step.FirstControl != null)
                step.Controller.wizardForm.invokeOnThread(() => step.FirstControl.Text += message);
        }
    }
}
