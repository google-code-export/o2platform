using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Merlin;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
using System.Windows.Forms;
using O2.DotNetWrappers.DotNet;

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

            newStep.OnComponentLoad += (step) => directory.refreshDirectoryView();

            steps.Add(newStep);
            return newStep;
        }

        public static IStep add_Message(this List<IStep> steps, string stepTitle, Func<string> messageToAdd)
        {
            //var message = messageToAdd();
            var initialMessage = "initial message: ";
            var newStep = Ex_Windows_Forms.createStepWithTextBox(stepTitle, initialMessage);
            newStep.OnComponentAction =
                (step) =>
                {
                    step.setText(messageToAdd());
                };
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


        public static IStep add_Control(this List<IStep> steps, Type controlType, string stepTitle, string stepSubTitle)
        {
            return add_Control(steps, controlType, stepTitle, stepSubTitle, null);
        }

        public static IStep add_Control(this List<IStep> steps, Type controlType, string stepTitle, string stepSubTitle, Action<IStep> onComponentLoad)
        {
            //control.AllowDrop = false;
            var newStep = new TemplateStep(controlType);//, 10, stepTitle);
            newStep.Title = stepTitle;
            newStep.Subtitle = stepSubTitle;
            newStep.OnComponentAction = onComponentLoad;
            steps.Add(newStep);
            return newStep;
        }

        public static IStep add_Control(this List<IStep> steps, Control control, string stepTitle, string stepSubTitle)
        {
            return add_Control(steps, control, stepTitle, stepSubTitle, null);
        }

        public static IStep add_Control(this List<IStep> steps, Control control, string stepTitle, string stepSubTitle, Action<IStep> onComponentLoad)
        {
            control.AllowDrop = false;
            var newStep = new TemplateStep(control, 10, stepTitle);
            newStep.Subtitle = stepSubTitle;
            newStep.OnComponentAction = onComponentLoad;
            steps.Add(newStep);
            return newStep;
        }

        public static IStep add_SelectFolder(this List<IStep> steps, string stepTitle, string defaultFolder, Action<string> setResult)
        {
            // textbox
            var textBox = new TextBox();
            textBox.TextChanged += (sender, e) =>
            {
                setResult(textBox.Text);
                PublicDI.log.info("in TextChanged");
            };
            textBox.Text = defaultFolder;
            textBox.Width = 400;
            textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            textBox.AllowDrop = true;
            textBox.DragDrop += (sender, e) => textBox.setText(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
            textBox.DragEnter += (sender, e) => e.Effect = DragDropEffects.Copy;

            // button
            var button = new Button();
            button.Text = "Select Folder";
            button.Width += 20;
            button.Click += (sender, e) =>
            {
                var folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = defaultFolder;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    textBox.Text = folderBrowserDialog.SelectedPath;
            };

            // panel
            var panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Fill;// AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            panel.Controls.Add(textBox);
            panel.Controls.Add(button);

            var newStep = new TemplateStep(panel, 10, stepTitle);
            steps.Add(newStep);
            return newStep;
        }    	 

    }
}
