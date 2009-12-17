using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Merlin;
using O2.DotNetWrappers.DotNet;


namespace O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods
{
    public static class Ex_Windows_Forms
    {
        public static TextBox createTextBox(string message)
        {
            var textBox = new TextBox();
            textBox.Multiline = true;
            textBox.WordWrap = false;
            textBox.ScrollBars = ScrollBars.Both;
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

        public static void setText(this TextBox textBox, string text)
        {
            textBox.invokeOnThread(() => textBox.Text = text);
        }

        public static void add_CheckBox(this IStep step, string checkBoxText, bool initialCheckedValue, Action<bool> onChange)
        {
            step.UI.invokeOnThread(
                () =>
                {
                    var checkBox = new CheckBox();
                    checkBox.Text = checkBoxText;
                    checkBox.AutoSize = true;
                    checkBox.Checked = initialCheckedValue;
                    if (onChange != null)
                        checkBox.CheckedChanged += (sender, e) => onChange(checkBox.Checked);
                    // at the moment this assumes that the control[0] is a contained of (for example) type FlowLayoutPanel
                    step.UI.Controls[0].Controls.Add(checkBox);
                });
        }
    }
}
