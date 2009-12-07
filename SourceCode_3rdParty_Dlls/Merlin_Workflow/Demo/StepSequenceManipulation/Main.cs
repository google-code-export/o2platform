using System.Collections.Generic;
using Merlin;
using MerlinStepLibrary;
using System.Windows.Forms;

namespace StepSequenceManipulationDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Creating the step sequence for choosing an apple
            var appleSteps = new List<IStep>();
            appleSteps.Add(new SelectionStep("Apple selection:",
                "Please select an apple sort",
                "Granny Smith", "Macintosh", "Red Delicious"));
            appleSteps.Add(generateMessageStep("Thank you", "Thank you for choosing an apple."));

            //Creating the step sequence for choosing an orange
            var orangeSteps = new List<IStep>();
            orangeSteps.Add(new SelectionStep("Orange selection",
                "Please select an orange sort:",
                "Persian", "Navel", "Valencia"));
            orangeSteps.Add(generateMessageStep("Thank you",
                "Thank you for choosing an orange."));

            //Now create the initial step sequence. This is the sequence
            //we'll feed to the wizard controller when starting the wizard.
            var fruitSelection = new SelectionStep("Fruit selection",
                "Please pick a fruit:",
                "Apples", "Oranges");
            var initialStepSequence = new List<IStep>();
            initialStepSequence.Add(fruitSelection);
            initialStepSequence.Add(new TemplateStep(new TextBox())); //This is a placeholder step.
            //It will never appear in the wizard. We add it so that the Finish button
            //will not appear on the first step.

            WizardController controller = new WizardController(initialStepSequence);

            //In the NextHandler of the first step, we delete the placeholder step,
            //determine which step sequence comes next and add it to the current 
            //step sequence.
            fruitSelection.NextHandler = () =>
            {
                controller.DeleteAllAfterCurrent();
                if (fruitSelection.Selected.Equals("Apples"))
                {
                    controller.AddAfterCurrent(appleSteps);
                }
                else controller.AddAfterCurrent(orangeSteps);
                return true;
            };

            controller.StartWizard("Apples & Oranges");
        }

        /// <summary>
        /// Generates a step that simply displays the provided message.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static IStep generateMessageStep(string title, string message)
        {
            var label = new Label();
            label.Text = message;
            return new TemplateStep(label, 10, title);
        }
    }
}
