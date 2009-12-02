// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Reflection;
using O2.DotNetWrappers.Windows;
using O2.Rnd.AspectDngHook;


namespace O2.Rnd.AspectDngHook
{
    public class DngConfig
    {
        public static String extractAspectDngExeToTempFolder()
        {
            String sFileLocation = Path.Combine(DI.config.O2TempDir, "aspectdng.exe");
            if (Files.WriteFileContent(sFileLocation, Resources.aspectdng))
                return sFileLocation;
            else
                return "";
        }

        public static String extractCecilDllToTempFolder()
        {
            String sFileLocation = Path.Combine(DI.config.O2TempDir, "cecil.dll");
            if (Files.WriteFileContent(sFileLocation, Resources.cecil))
                return sFileLocation;
            else
                return "";
        }

        public static String copyCurrentDllToTempFolder()
        {
            Assembly aThisAssembly = Assembly.GetExecutingAssembly();
            String sPathToCurrentAsssembly = aThisAssembly.Location;
            String sTargetFile = Path.Combine(DI.config.O2TempDir,
                                              Path.GetFileName(sPathToCurrentAsssembly));
            File.Copy(sPathToCurrentAsssembly, sTargetFile, true);
            return sTargetFile;
        }
    }
}
