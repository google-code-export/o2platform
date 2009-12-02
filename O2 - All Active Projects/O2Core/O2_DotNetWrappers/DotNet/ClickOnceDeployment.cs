using System;
//using System.Deployment.Application;
using System.Threading;
using System.Windows.Forms;

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
            //return ApplicationDeployment.IsNetworkDeployed;
            return false;
        }
    }
}