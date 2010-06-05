using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using O2.Kernel.ExtensionMethods;
using System.IO;

namespace O2.Kernel.CodeUtils
{
    public class O2Svn
    {
        public static List<string> AssembliesCheckedIfExists = new List<string>();
        public static string O2SVN_ExternalDlls = "http://o2platform.googlecode.com/svn/trunk/O2 - All Active Projects/_3rdPartyDlls/";
        public static string O2SVN_Binaries = "http://o2platform.googlecode.com/svn/trunk/O2_Binaries/";

        public static void clear_AssembliesCheckedIfExists()
        {
            AssembliesCheckedIfExists = new List<string>();
        }

        public void tryToFetchAssemblyFromO2SVN(string assemblyToLoad)
        {
            string localFilePath = "";
            var thread = O2Kernel_O2Thread.mtaThread(
                () =>
                {
                    if (AssembliesCheckedIfExists.Contains(assemblyToLoad))     // for performace reasons only check this once
                        return;
                    "Trying to fetch assembly from O2's SVN repository: {0}".info(assemblyToLoad);
                    AssembliesCheckedIfExists.Add(assemblyToLoad);
                    if (Path.GetExtension(assemblyToLoad) == ".dll" ||
                        Path.GetExtension(assemblyToLoad) == ".exe")  // if there is no valid extension is it most likely a GAC reference
                    {
                        var currentApplicationPath = PublicDI.config.CurrentExecutableDirectory;
                        localFilePath = Path.Combine(currentApplicationPath, assemblyToLoad);
                        if (File.Exists(localFilePath))
                            return;
                        var webLocation1 = "{0}{1}".format(O2SVN_ExternalDlls, assemblyToLoad);
                        if (new O2Kernel_Web().httpFileExists(webLocation1))
                        {
                            new O2Kernel_Web().downloadBinaryFile(webLocation1, localFilePath);
                        }
                        else
                        {
                            var webLocation2 = "{0}{1}".format(O2SVN_Binaries, assemblyToLoad);
                            if (new O2Kernel_Web().httpFileExists(webLocation2))
                            {
                                new O2Kernel_Web().downloadBinaryFile(webLocation2, localFilePath);
                            }
                        }
                        if (File.Exists(localFilePath))
                        {
                            "Assembly sucessfully fetched from O2SVN: {0}".info(localFilePath);
                            return;
                        }
                    }
                });
            var maxWait = 30;
            if (thread.Join(maxWait * 1000) == false)
            {
                if (File.Exists(localFilePath))                
                    "TimeOut (of {1} secs) was reached, but Assembly was sucessfully fetched from O2SVN: {0}".info(maxWait,localFilePath);                                    
                else
                    "error while tring to fetchAssembly: {0} (max wait of {1} seconds reached)".error(assemblyToLoad, maxWait);
                return;
            }
            //var localPath = Path.Combine
            //return false;
        }
    }
}
