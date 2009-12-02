// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Diagnostics;
using System.IO;
using O2.DotNetWrappers.Windows;


namespace O2.Tool.HostLocalWebsite.classes
{
    internal class webservices
    {
        public static Process pWebServiceProcess;
        //public static String sExe = @"C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.exe";
        public static String sExe = DI.config.CurrentExecutableDirectory +  "\\WebDev.WebServer.exe";
        public static String sParamsString = "/port:\"{0}\" /path:\"{1}\" /vpath:\"{2}\"";
        public static String sPath = DI.config.O2TempDir;
        public static String sPort = "1580";
        public static String sVPath = "/" + Path.GetFileName(DI.config.O2TempDir);

        public static String sWebServiceURL = @"http://localhost:{0}{1}";

        public static void StartWebService()
        {
            pWebServiceProcess = Processes.startProcess(sExe, String.Format(sParamsString, sPort, sPath, sVPath));
        }

        public static String GetWebServiceURL()
        {
            return String.Format(sWebServiceURL, sPort, sVPath);
        }

        public static void StopWebService()
        {
            if (pWebServiceProcess != null && pWebServiceProcess.HasExited == false)
                pWebServiceProcess.Kill();
        }

        public static void setExe(String sNewValueFor_Exe)
        {
            if (sNewValueFor_Exe != "")
                sExe = sNewValueFor_Exe;
        }

        public static void setPort(String sNewValueFor_Port)
        {
            if (sNewValueFor_Port != "")
                sPort = sNewValueFor_Port;
        }

        public static void setPath(String sNewValueFor_Path)
        {
            if (sNewValueFor_Path != "")
                sPath = sNewValueFor_Path;
        }

        public static void setVPath(String sNewValueFor_VPath)
        {
            if (sNewValueFor_VPath != "")
                sVPath = sNewValueFor_VPath;
        }
    }
}
