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
using O2.Views.ASCX.MerlinWizard;
using O2.DotNetWrappers.Zip;
using Merlin;
using MerlinStepLibrary;
using System.Threading;
using O2.Core.XRules.Classes;
using O2.Views.ASCX.MerlinWizard.O2Wizard_ExtensionMethods;

namespace O2.Core.XRules._Wizards
{		
    [O2Wizard]
	public class Wizard_SyncViaSvn
	{
		private static IO2Log log = PublicDI.log;
		
        public static string testTargetFolder = PublicDI.config.getTempFolderInTempDirectory("Svn To Sync");

        public Thread test_runWizard()
        {
            return runWizard(SvnApi.svnO2DatabaseRulesFolder, testTargetFolder);        	
        }
        [StartWizard]
        public Thread runWizard(string svnUrl, string targetFolder)
        {
            var o2Wizard = new O2Wizard("Sync Rules Database via SVN");
        	//var steps = new List<IStep>();
        	var message = string.Format("This workflow will Syncronize the local copy of O2's Rule Database with the lastest version at O2's SVN code repository" + 
        								"{0}{0}SVN Url = {1}" + 
        								"{0}{0}Local Folder = {2}" + 
        								"{0}{0}Note that the local O2 Rule Database will be deleted!", Environment.NewLine, svnUrl.Replace("%20"," ") , targetFolder);
            o2Wizard.Steps.add_Message("Confirm", message);
            o2Wizard.Steps.add_Action("Download Files", (step) => downloadFiles(step, svnUrl, targetFolder));
            o2Wizard.Steps.add_Directory("Downloaded Files", targetFolder);
            return o2Wizard.start();
            //return o2Wizard.start();

        	//return steps.startWizard("Sync Rules Database via SVN");            
        }
        
        public void downloadFiles(IStep step, string svnUrl, string targetFolder)
        {
        	O2Thread.mtaThread(
			()=> {			
        		step.allowNext(false);
				step.allowBack(false);						
				step.append_Line(" .... Deleting local database: {0}", targetFolder);
				Files.deleteFolder(targetFolder,true);
				step.append_Line(" .... Calculating files to download");
                var svnMappedUrls = SvnApi.HttpMode.getSvnMappedUrls(svnUrl, true);
				step.append_Line(" .... There are {0} files & folders to download {1}" , svnMappedUrls.Count(), Environment.NewLine);
				foreach(var svnMappedUrl in svnMappedUrls)				
				{
					step.append_Line("   * Downloading: {0}", svnMappedUrl.FullPath.Replace(svnUrl, ""));
					SvnApi.HttpMode.download(svnMappedUrl, svnUrl, targetFolder);
				}
				
				step.append_Line("{0}{0} .... Download complete", Environment.NewLine);
															
				step.allowNext(true);
				step.allowBack(true);						
			});			
        }        
	}		
}
