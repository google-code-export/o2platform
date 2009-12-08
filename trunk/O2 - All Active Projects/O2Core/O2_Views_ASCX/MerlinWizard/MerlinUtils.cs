using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//O2Tag_AddReferenceFile:Merlin.dll
using Merlin;
using MerlinStepLibrary;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using System.Threading;

namespace O2.Views.ASCX.MerlinWizard
{
    public class MerlinUtils
    {
        public static Thread runWizardWithSteps(List<IStep> steps, string wizardName)
        {
            return runWizardWithSteps(steps, wizardName, null);
        }
		public static Thread runWizardWithSteps(List<IStep> steps, string wizardName, Action<WizardController, WizardController.WizardResult> onCompletion)
		{			
			// this needs to run on an STA thread because some controls might require Drag & Drop support

			return O2Thread.staThread(
				()=> {
						WizardController wizardController = new WizardController(steps);
			            //wizardController.LogoImage = Resources.NerlimWizardHeader;
			            var wizardResult = wizardController.StartWizard(wizardName);
                        if (onCompletion != null)
                            onCompletion(wizardController, wizardResult);
			         });						
		}
    }
}
