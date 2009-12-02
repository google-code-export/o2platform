// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Kernel.CodeUtils
{
    public class O2Kernel_Files
    {        

        // also on O2_CoreLib
        public static String Copy(String sSourceFile, String sTargetFileOrFolder)
        {
            string sTargetFile = sTargetFileOrFolder;
            if (Directory.Exists(sTargetFile))
                sTargetFile = Path.Combine(sTargetFile, Path.GetFileName(sSourceFile));
            try
            {
                File.Copy(sSourceFile, sTargetFile, true);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in O2Kernel_Files.Copy");
            }
            return sTargetFile;
        }

        public static String checkIfDirectoryExistsAndCreateIfNot(String directory)
        {
            try
            {
                if (Directory.Exists(directory))
                    return directory;
                Directory.CreateDirectory(directory);
                if (Directory.Exists(directory))
                    return directory;
            }
            catch (Exception e)
            {
                DI.log.error("Could not create directory: {0} ({1})", directory, e.Message);
            }
            return "";
        }

        public static string getTempFolderName()
        {
            String sTempFileName = Path.GetTempFileName();
            File.Delete(sTempFileName);
            return Path.GetFileNameWithoutExtension(sTempFileName);
        }

        public static string getTempFileName()
        {
            String sTempFileName = Path.GetTempFileName();
            File.Delete(sTempFileName);
            return Path.GetFileName(sTempFileName);
        }
    }
}
