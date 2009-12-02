using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace O2.Rules.OunceLabs
{
    // Need to make this version specific
    public class Services_OunceV6
    {
        public static ServiceController scOunceAppServer = new ServiceController("OunceAppServer");
        public static ServiceController scOunceCore = new ServiceController("OunceCore");
        public static ServiceController scOunceLicenseManager = new ServiceController("Ounce License Manager");
        public static ServiceController scOunceMySql = new ServiceController("Ounce MySQL");

        public static String sPathToOsa = @"C:\Program Files\Ounce Labs\bin\osa.exe";
        public static String sPathToOsaWorkingDir = @"C:\Program Files\Ounce Labs\bin";
        public static String sPathToOunceJavaw = @"C:\Program Files\Ounce Labs\jre\bin\javaw.exe";
        public static String sPathToScanPropertiesFile = @"C:\Program Files\Ounce Labs\config\scan.properties";

        public static bool stopService_Core()
        {            
            return stopService(scOunceCore); // stop service
        }

        public static void stopService_AppServer()
        {
            stopService(scOunceAppServer);
        }

        public static void startService_Core()
        {
            startService(scOunceCore);
        }

        public static void startService_MySql()
        {
            startService(scOunceMySql);
        }

        public static void stopService_MySql()
        {
            stopService(scOunceMySql);
        }

        public static bool isRunning_MySql()
        {
            return scOunceMySql.Status == ServiceControllerStatus.Running;
        }

        public static void startService_AppServer()
        {
            startService(scOunceAppServer);
        }

        public static void startOsa()
        {
            var pProcess = new Process {StartInfo = {FileName = sPathToOsa, WorkingDirectory = sPathToOsaWorkingDir}};
            pProcess.Start();
            DI.log.debug("Process {0} started", sPathToOsa);
        }

        public static void stopOsa()
        {
            foreach (Process pProcess in Process.GetProcesses())
            {
                // find and kill processes called with "OunceSecurityAnalyst"                
                if (pProcess.ProcessName == "OunceSecurityAnalyst")
                {
                    pProcess.Kill();
                    DI.log.debug("Killed Process {0}", pProcess.ProcessName);
                }
                // find and kill javaw processes which load sPathToOunceJavaw
                if (pProcess.ProcessName == "javaw")
                {
                    foreach (ProcessModule pmProcessModule in pProcess.Modules)
                        if (pmProcessModule.FileName == sPathToOunceJavaw)
                        {
                            pProcess.Kill();
                            DI.log.debug("Killed Process {0}", pProcess.ProcessName);
                        }
                }
            }
        }

        public static bool stopService(ServiceController scTargetService)
        {
            try
            {
                int iMaxWaitTime = 0;
                scTargetService.Refresh();
                if (scTargetService.Status == ServiceControllerStatus.Running)
                {
                    DI.log.debug("Service is started, so trying to stop it");
                    scTargetService.Stop();
                    while (scTargetService.Status != ServiceControllerStatus.Stopped)
                    {
                        if (iMaxWaitTime++ > 10)
                        {
                            DI.log.debug("Service Restart time-out. Aborting...");
                            return false;
                        }
                        Application.DoEvents();
                        Thread.Sleep(500);
                        scTargetService.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.error("in stopService:{0}", ex.Message);
            }
            DI.log.debug("Service Status: {0}", scTargetService.Status.ToString());
            return true;
        }

        public static bool startService(ServiceController scTargetService)
        {
            int iMaxWaitTime = 0;
            scTargetService.Refresh();
            if (scTargetService.Status == ServiceControllerStatus.Stopped)
            {
                DI.log.debug("Service is stopped, so trying to start it");

                scTargetService.Start();
                while (scTargetService.Status != ServiceControllerStatus.Running)
                {
                    if (iMaxWaitTime++ > 10)
                    {
                        DI.log.debug("Service Restart time-out. Aborting...");
                        return false;
                    }
                    Application.DoEvents();
                    Thread.Sleep(500);
                    scTargetService.Refresh();
                }
            }
            DI.log.debug("Service Status: {0}", scOunceCore.Status.ToString());
            return true;
        }

        public static bool restartService(ServiceController scTargetService)
        {
            DI.log.info("Restarting Service: {0}", scTargetService.DisplayName);
            if (stopService(scTargetService))
                return startService(scTargetService);
            return false;
        }
    }
}