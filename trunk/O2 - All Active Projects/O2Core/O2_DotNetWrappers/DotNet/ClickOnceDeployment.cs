// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
//using System.Deployment.Application;
using System.Threading;
using System.Windows.Forms;
using O2.DotNetWrappers.Network;
using O2.Kernel.ExtensionMethods;
using O2.Kernel;

namespace O2.DotNetWrappers.DotNet
{
    public class ClickOnceDeployment
    {
        public static bool cancelUpdateChecks;
        public static int delayBetweenChecks = 10 * 60 *1000;  // (check every 10 minutes
        public static int numberOfChecksPerformed;

        // removing this functionality since it makes some uses unconfortable with it
        /*
        public static void startThreadFor_checkForClickOnceUpdatesAndInstall()
        {
            new Thread(checkForClickOnceUpdatesAndInstall).Start();
        }

        public static bool isApplicationBeingExecutedViaClickOnceDeployment()
        {
            return ApplicationDeployment.IsNetworkDeployed;
        }

        public static void checkForClickOnceUpdatesAndInstall()
        {
            // make sure we are running from a ClickOnce executable			
            if (!isApplicationBeingExecutedViaClickOnceDeployment())
            {
                DI.log.info("Application was not deployed using ClickOnce so skipping O2 Auto Update Checks");
                return;
            }

            while (false == cancelUpdateChecks)
            {
                try
                {
                    Thread.Sleep(delayBetweenChecks);
                    ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
                    DI.log.info("Checking for Updates to this O2 Module [{0}]", numberOfChecksPerformed++);
                    UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();


                    // Check if update is actually available.
                    if (info.UpdateAvailable)
                    {
                        // Check if update is required. If not, ask user if they actually want to install.
                        //if (!info.IsUpdateRequired)
                        //                        {
                        cancelUpdateChecks = true;
                        DialogResult dialogResult =
                            MessageBox.Show(
                                "There is an update available for " +
                                ((DI.windowsFormMainO2Form != null) ? DI.windowsFormMainO2Form.Text : "(HOST FORM)") +
                                ".\n\n Would you like to download the installer and update this version? \n\n(if you cancel you will not be asked again during this session}\n\n",
                                "O2 Auto Update", MessageBoxButtons.OKCancel);
                        if (DialogResult.OK == dialogResult)
                        {
                            DI.log.info("Update is going to be installed");
                            updateCheck.Update();
                            DI.log.info("all done, ready for restart");
                            DI.log.showMessageBox(
                                "This O2 module was successfull upgraded, please click OK to restart (note that you will lose any unsaved changes)");
                            DI.log.info("retarting");
                            Application.Restart();
                        }
                        //                        }
                    }

                }
                catch (Exception ex)
                {
                    DI.log.ex(ex, "in checkForClickOnceUpdatesAndInstall");
                }
            }
        }
        */

        
        public static string getClickOnceScriptPath()
        {
            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null &&
                AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Length > 0)
            {
                var file = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                PublicDI.log.debug("ClickOnce file raw: {0}", file);
                file = file.Replace("file:///", "");
                file = WebEncoding.urlDecode(file);
                file = file.Replace("/", "\\");
                PublicDI.log.debug("ClickOnce file final: {0}", file);
                return file;
            }
            return "";
        }
    
        public static String getFormTitle_forClickOnce(String sFormName)
        {
            var executionMode = "O2 Binaries folder";
            if (DI.config.CurrentExecutableDirectory.IndexOf("Documents and Settings") > -1)
                executionMode = "ClickOnce install";
            else
                if (DI.config.CurrentExecutableDirectory.IndexOf(@"Program Files\O2 - Ounce Open") > -1)
                    executionMode = "MSI install";         
            return String.Format("{0}  ({1})", sFormName, executionMode);
            // removing the System.Deployment reference so that we can run this on Mono
            // need to find another way to detect ClickOnce deployment and the current version (above is a sort of hacked way)
            //   if (ApplicationDeployment.IsNetworkDeployed)
            //       return String.Format("{0}  ({1})", sFormName, ApplicationDeployment.CurrentDeployment.CurrentVersion);
        }

        public static bool isClickOnceDeployment()
        {
            var systemDeploymentDll = "System.Deployment".assembly();

            var value = (bool)systemDeploymentDll.type("ApplicationDeployment").prop("IsNetworkDeployed");
            //return ApplicationDeployment.IsNetworkDeployed;
            "isClickOnceDeployment: {0}".info(value);
            return value;
        }
    }
}
