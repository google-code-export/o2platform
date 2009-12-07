using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.Views.ASCX.CoreControls;
//O2Tag_AddReferenceFile:Merlin.dll
using Merlin;


namespace O2.Views.ASCX.MerlinWizard
{
    public static class Ascx_ExtensionMethods
    {
    	public static IStep add_ChooseDirectory(this List<IStep> steps, string stepName)
    	{
    		var directory = new ascx_Directory();
			directory._ViewMode = ascx_Directory.ViewMode.Simple_With_LocationBar;
            directory._HideFiles = true;
            directory.refreshDirectoryView();
    		var newStep = new TemplateStep(directory, 0, stepName);
    		steps.Add(newStep);
    		return newStep;    		    		
    	}
    	
    	public static string show(this List<IStep> steps, string wizardName)
    	{
    		return MerlinUtils.runWizardWithSteps(steps, wizardName);
    	}
    }
}
