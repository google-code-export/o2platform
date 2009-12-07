using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//O2Tag_AddReferenceFile:Merlin.dll
using Merlin;
using MerlinStepLibrary;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;

namespace O2.Views.ASCX.MerlinWizard
{
    public class MerlinUtils
    {
		public static string runWizardWithSteps(List<IStep> steps, string wizardName)
		{
			var result = "";
			// this needs to run on an STA thread because some controls might require Drag & Drop support
			var wizardThread = O2Thread.staThread(
				()=> {
						WizardController wizardController = new WizardController(steps);
			            //wizardController.LogoImage = Resources.NerlimWizardHeader;
			            var wizardResult = wizardController.StartWizard(wizardName);
			            result = wizardResult.ToString();	//WizardController.WizardResult
			         });
			wizardThread.Join();
			return result;
		}
    }
}
