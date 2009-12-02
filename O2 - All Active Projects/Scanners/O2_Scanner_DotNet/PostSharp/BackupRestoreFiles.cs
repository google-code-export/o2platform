using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using O2.DotNetWrappers.Windows;

namespace O2.Scanner.DotNet.PostSharp
{
    public class BackupRestoreFiles
    {
        static string stringToAppendToFile = ".O2PostSharpBackup";

        public static bool doesBackupExist(string pathToFile)
        {
            return File.Exists(getBackupFileName(pathToFile));
        }

        public static void backup(string pathToFile)
        {
            var backupFile = getBackupFileName(pathToFile);
            if (File.Exists(backupFile))
                DI.log.error("Backup file already exists, aborting backup process for : {0}", pathToFile);
            else
                File.Copy(pathToFile, backupFile);
        }

        public static bool restore(string pathToFile)
        {
            var backupFile = getBackupFileName(pathToFile);
            if (false == File.Exists(backupFile))
                DI.log.error("In Dll Restore:Could not find backup file for: {0}", pathToFile);
            else
            {
                if(File.Exists(pathToFile))
                    if (false == Files.deleteFile(pathToFile))
                    {
                        DI.log.error("In Dll Restore: Could not delete file to restore: {0}", pathToFile);
                        return false;
                    }
                File.Copy(backupFile, pathToFile,true);
                //File.Delete(backupFile);
                return true;
            }
            return false;
        }

        public static string getBackupFileName(string pathToFile)
        {
            return string.Format("{0}{1}", pathToFile, stringToAppendToFile);
        }
    }
}
