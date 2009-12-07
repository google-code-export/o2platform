using System;
using System.Collections.Generic;
using System.Text;

using Merlin;


namespace NilremUserManagement.CustomSteps
{
    class PreviewStep : TemplateStep
    {
        private User user;

        public PreviewStep(User user)
            : base()
        {
            Title = "Preview User Information";
            var ui = new PreviewUI();
            this.UI = ui;
            this.user = user;
            // Once the step is reached, show the settings for the user.
            // The user data will not be loaded until this step is reached.
            this.StepReachedHandler = () =>
            {
                ui.DisplayUser(this.user);
            };
        }
    }
}
