using System;
using System.Collections.Generic;
using O2.DotNetWrappers.Windows;


namespace O2.Rnd.JavaVelocityAnalyzer.classes
{
    internal class velocityloader
    {
        // global vars
        public static String sExtensionOfProcessedVelocityFiles = ".vm.ProcessedVelocityFile.txt";
        public static String sExtensionOfVelocityFiles = ".vm";

        public static List<String> getProcessedVelocityFilesFromFolder(String sTargetFolder, bool bRecursiveSearch)
        {
            return Files.getListOfAllFilesFromDirectory(sTargetFolder, bRecursiveSearch,
                                                        "*" + sExtensionOfProcessedVelocityFiles);
        }

        public static bool isFileAVelocityProcessedFile(String sFileToCheck)
        {
            return (sFileToCheck.IndexOf(sExtensionOfProcessedVelocityFiles) > -1) ? true : false;
        }
    }
}