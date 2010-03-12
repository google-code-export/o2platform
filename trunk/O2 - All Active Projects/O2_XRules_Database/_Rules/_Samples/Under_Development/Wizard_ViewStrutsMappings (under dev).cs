// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.Views.ASCX;
using O2.Views.ASCX.CoreControls;
using O2.Views.ASCX.classes;
using O2.External.WinFormsUI.Forms;
using O2.External.SharpDevelop.Ascx;
using O2.Views.ASCX.MerlinWizard;
using O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods;
using O2.DotNetWrappers.Zip;
// extra references and the namespaces they import
//O2Ref:nunit.framework.dll
using NUnit.Framework; 
//O2Ref:merlin.dll
using Merlin;
using MerlinStepLibrary;

namespace O2.Script
{	

	public class WizardModel
	{
		public string pathToFindStrutsFiles {get ; set;}
		public override string ToString()
		{
			return "this is the WizardModel...";
		}
	}
	
	[TestFixture]
	public class Workflow_ViewStrutsMappings
	{
		public static string testApp = @"C:\_Dinis_localDevelop\Java Apps\4207_strutsexamples\samples\struts-cookbook";
		private static IO2Log log = PublicDI.log;
		
        [Test]
        public string runWizard()
        {
            return runWizard(testApp);
        }

        public string runWizard(string startFolder)
        {                      
        	var model = new WizardModel();        
        	var o2Wizard = new O2Wizard("View Struts Mappings", model); 
        	
        	o2Wizard.Steps.add_SelectFolder("Select Web Root folder",testApp,onLoad);
        							  
            /*o2Wizard.Steps.add_SelectFolder("title",  
            								"asd",
            								startFolder, 
            								(selectedFolder) => model.pathToFindStrutsFiles = selectedFolder);
			o2Wizard.Steps.add_Message("Folder Selected" , 
				()=> { 
						return (model.pathToFindStrutsFiles != null)  
								? "You selected folder: " + model.pathToFindStrutsFiles
								: "model.pathToFindStrutsFiles was null";
					  });
            var lastStep = (TemplateStep)o2Wizard.Steps.add_Message("All done", "Thanks for using this wizard");                        
            lastStep.OnComponentAction = (step)=> step.allowCancel(false);
		*/
            o2Wizard.start();
            return "ok";
        }
        
        public void onLoad(string selectedFolder)
        {
        }
	}		
}
