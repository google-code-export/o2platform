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
// extra references and the namespaces they import
//O2Tag_AddReferenceFile:nunit.framework.dll

using Merlin;
using MerlinStepLibrary;
using System.Threading;

namespace O2.Script
{	
	public class testAscx
	{
		private static IO2Log log = PublicDI.log;

        public Thread runWizard_BackupFolder(string startFolder)
			{
			 	var targetFolder = Path.Combine(PublicDI.config.O2TempDir,"..\\_o2_Backups");
			 	Files.checkIfDirectoryExistsAndCreateIfNot(targetFolder);
                return runWizard_BackupFolder(startFolder, targetFolder);
			}

        public Thread runWizard_BackupFolder(string startFolder, string targetFolder)
        {
            var steps = new List<IStep>();
            steps.add_ChooseDirectory("Choose Directory To Backup", startFolder);
            steps.add_ChooseDirectory("Choose Directory To Store Zip file", targetFolder);
            steps.add_Action("Confirm backup action", confirmBackupAction);
            steps.add_Action("Backing up files", executeTask);
            //steps.add_Message("All OK", "This is a message and all is OK");
            //steps.add_Message("Problem", "Something went wrong");

            return steps.startWizard("Backup folder: " + startFolder);            
        }
		
		public void confirmBackupAction(IStep step)
		{			
			O2Thread.mtaThread(
			()=> {
				step.setText("");				
				var sourceDirectory = step.getPathFromStep(0);
				var targetDirectory = step.getPathFromStep(1);
				var targetFile = calculateTargetFileName(sourceDirectory, targetDirectory);
				step.appendText("You are about to create a backup of the folder: {1}{1}\t{0} {1}{1} ", sourceDirectory, Environment.NewLine);
				step.appendText("into the file: {1}{1}\t{0}{1}{1}", targetFile, Environment.NewLine);
				step.appendText("Do you want to processed");
			});
		}
		
		
		public void executeTask(IStep step)
		{			
			O2Thread.mtaThread(
			()=> {
			
				var sourceDirectory = step.getPathFromStep(0);
				var targetDirectory = step.getPathFromStep(1);
				var targetFile = calculateTargetFileName(sourceDirectory, targetDirectory);
								
				step.allowNext(false);
				step.allowBack(false);						
				step.appendLine(" .... creating zip file ....");
				
				new zipUtils().zipFolder(sourceDirectory, targetFile);
				if (File.Exists(targetFile))
					step.appendLine("File Created: {0}", targetFile);	
				else
					step.appendLine("There was a problem creating the file: {0}", targetFile);	
				step.appendLine("all done");
				step.allowNext(true);
				step.allowBack(true);									
			});		
		}
		
		public string calculateTargetFileName(string sourceDirectory, string targetDirectory)
		{
			var filename = string.Format("O2 Backup for ({0}) done on ({1}).zip",sourceDirectory, DateTime.Now.ToString());
			filename  = Files.getSafeFileNameString(filename);
			return Path.Combine(targetDirectory, filename + ".zip");
		}						
	}		
}
