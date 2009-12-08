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
using O2.Kernel;
using O2.Kernel.Interfaces.O2Core;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Network;
using O2.Views.ASCX;
using O2.Views.ASCX.CoreControls;
using O2.Kernel.Interfaces.Views;
using O2.Views.ASCX.classes;
using O2.Views.ASCX.MerlinWizard;
using O2.DotNetWrappers.Zip;
using Merlin;
using MerlinStepLibrary;
using System.Threading;
using O2.Core.XRules.Classes;

namespace O2.Core.XRules._Wizards
{		
	public class Wizard_SyncViaSvn
	{
		private static IO2Log log = PublicDI.log;
		
        public static string testTargetFolder = PublicDI.config.getTempFolderInTempDirectory("Svn To Sync");

        public Thread test_runWizard()
        {
            return runWizard(SvnApi.svnO2DatabaseRulesFolder, testTargetFolder);        	
        }

        public Thread runWizard(string svnUrl, string targetFolder)
        {
        	var steps = new List<IStep>();
        	var message = string.Format("This workflow will Syncronize the local copy of O2's Rule Database with the lastest version at O2's SVN code repository" + 
        								"{0}{0}SVN Url = {1}" + 
        								"{0}{0}Local Folder = {2}" + 
        								"{0}{0}Note that the local O2 Rule Database will be deleted!", Environment.NewLine, svnUrl.Replace("%20"," ") , targetFolder);
        	steps.add_Message("Confirm", message);
        	steps.add_Action("Download Files", (step) => downloadFiles (step, svnUrl, targetFolder));
        	steps.add_Directory("Downloaded Files", targetFolder);
        	return steps.startWizard("Sync Rules Database via SVN");            
        }
        
        public void downloadFiles(IStep step, string svnUrl, string targetFolder)
        {
        	O2Thread.mtaThread(
			()=> {			
        		step.allowNext(false);
				step.allowBack(false);						
				step.appendLine(" .... Deleting local database: {0}", targetFolder);
				Files.deleteAllFilesFromDir(targetFolder);
				step.appendLine(" .... Calculating files to download");
				var svnMappedUrls= SvnApi.getSvnMappedUrls(svnUrl,true);
				step.appendLine(" .... There are {0} files & folders to download {1}" , svnMappedUrls.Count(), Environment.NewLine);
				foreach(var svnMappedUrl in svnMappedUrls)				
				{
					step.appendLine("   * Downloading: {0}", svnMappedUrl.FullPath.Replace(svnUrl, ""));
					SvnApi.download(svnMappedUrl, svnUrl, targetFolder);
				}
				
				step.appendLine("{0}{0} .... Download complete", Environment.NewLine);
															
				step.allowNext(true);
				step.allowBack(true);						
			});			
        }        
	}		
}
