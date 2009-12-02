using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using O2.DotNetWrappers.Windows;
using O2.Legacy.CoreLib;

namespace O2.Legacy.CoreLib.O2Core.O2Environment
{
    public class Cmd
    {
        public static String sCurrento2CmdWorkDirectory = DI.o2CorLibConfig.CurrentExecutableDirectory;


        public static String cd()
        {
            return sCurrento2CmdWorkDirectory;
        }

        public static String dir()
        {
            var lsFiles = new List<string>();
            Files.getListOfAllFilesFromDirectory(lsFiles, sCurrento2CmdWorkDirectory, false, "*.*", false);
            var sbResult = new StringBuilder();
            sbResult.AppendLine("Files in directory " + sCurrento2CmdWorkDirectory);
            sbResult.AppendLine();
            foreach (string sFile in lsFiles)
                sbResult.AppendLine(sFile);
            return sbResult.ToString();
        }

        public static void exit()
        {
            Application.Exit();
        }

        public static void notepad()
        {
            Processes.startProcess("Notepad.exe");
        }

        public static void cmdDotexe()
        {
            Processes.startProcess("cmd.exe");
        }

        public static void deleteFilesFromTempDirectory()
        {
            Files.deleteFilesFromDirThatMatchPattern(DI.o2CorLibConfig.O2TempDir, "*.*");
        }


        // can't seem to find in the registry a key to hold the SAR module path
        /*
       public static void open_SearchAssessmentRun()
        {
         
            String sRegistryKeyWhereClickOnceAppsAreInstalled = @"S-1-5-21-817711196-3176212830-1286862028-500\Software\Microsoft\Windows\ShellNoRoam\MUICache";
            Microsoft.Win32.RegistryKey asd = Microsoft.Win32.Registry.Users.OpenSubKey(sRegistryKeyWhereClickOnceAppsAreInstalled);            
            String sPathToSearchAssessmentRun2 = WinRegistry.getKeyValue_Users(@"S-1-5-21-817711196-3176212830-1286862028-500\Software\Microsoft\Windows\ShellNoRoam\MUICache", "SearchAssessmentRun");
             
            // so let's find it manually            

        }*/
    }
}