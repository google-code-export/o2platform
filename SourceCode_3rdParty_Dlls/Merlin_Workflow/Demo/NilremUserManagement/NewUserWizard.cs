using System;
using System.Collections.Generic;
using System.Text;

using Merlin;
using MerlinStepLibrary;
using System.Windows.Forms;
using NilremUserManagement.Properties;
using System.IO;
using System.Drawing;

namespace NilremUserManagement.CustomSteps
{
    static class NewUserWizard
    {
        delegate bool AnswerValidation(string answer);
        public static User RunNewUserWizard()
        {
            //Create a list of steps
            List<IStep> steps = new List<IStep>();
            User newUser = new User();

            //Step 1. Welcome message
            TextBox t = new TextBox();
            t.Multiline = true;
            t.ScrollBars = ScrollBars.Vertical;
            t.ReadOnly = true;
            t.Text = Resources.WelcomeMessage;
            t.Select(0, 0);
            steps.Add(new TemplateStep(t, 10, "Welcome"));

            //Step 2. Role selection
            var roleStep = new SelectionStep("Role Selection", "Please select the user's role:",
                Enum.GetNames(typeof(User.UserRole)));
            roleStep.NextHandler = () =>
            {
                newUser.Role = (User.UserRole)Enum.Parse(typeof(User.UserRole), roleStep.Selected as string);
                return true;
            };
            steps.Add(roleStep);

            //Step 3. User Details
            var userFormStep = new TextFormStep("User Details");
            userFormStep.Subtitle = "This step allows you to specify the user's personal information.";
            userFormStep.Prompt = "Please provide the following user information:";
            var userIdQuestion = userFormStep.AddQuestion("User &ID:", Validation.NonEmpty);
            var fullNameQuestion = userFormStep.AddQuestion("Full &Name:", Validation.NonEmpty);
            var passwordQuestion = userFormStep.AddQuestion("&Password (6 or more characters):",
                Validation.MinLength(6), '*');
            var passwordQuestionRetype = userFormStep.AddQuestion("&Retype Password:",
                Validation.MinLength(6), '*');
            var emailAddressQuestion = userFormStep.AddQuestion("&E-Mail address:", Validation.NonEmpty);
            steps.Add(userFormStep);
            userFormStep.NextHandler = () =>
            {
                if (!passwordQuestion.Answer.Equals(passwordQuestionRetype.Answer))
                {
                    MessageBox.Show("The password does not match the retyped password.",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                newUser.FullName = fullNameQuestion.Answer;
                newUser.Email = emailAddressQuestion.Answer;
                newUser.Password = passwordQuestion.Answer;
                newUser.UserId = userIdQuestion.Answer;
                return true;
            };

            //Step 4. Picture Selection step. This step features the selection of a file.
            var pictureSelectionStep = new FileSelectionStep("User picture selection", "Please provide a picture for this user.");
            pictureSelectionStep.Filter = "Images|*.bmp;*.jpg;*.gif;*.tif;*.png|All Files (*.*)|*.*";
            pictureSelectionStep.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            pictureSelectionStep.NextHandler = () =>
            {
                if (File.Exists(pictureSelectionStep.SelectedFullPath))
                {
                    newUser.Picture = Image.FromFile(pictureSelectionStep.SelectedFullPath);
                    return true;
                }
                else
                {
                    MessageBox.Show("Selected image does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            };
            pictureSelectionStep.AllowNextStrategy =
                () => !string.IsNullOrEmpty(pictureSelectionStep.SelectedFullPath);
            steps.Add(pictureSelectionStep);

            //Step 5. Preview step. This step features a custom UI implemented separately.
            steps.Add(new CustomSteps.PreviewStep(newUser));

            //Run the wizard with the steps defined above
            WizardController wizardController = new WizardController(steps);
            wizardController.LogoImage = Resources.NerlimWizardHeader;
            var wizardResult = wizardController.StartWizard("New User");

            //If the user clicked "Cancel", don't add the user
            if (wizardResult == WizardController.WizardResult.Cancel)
            {
                newUser = null;
            }

            return newUser;
        }

    }
}
