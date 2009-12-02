using System;
using System.Collections.Generic;

namespace O2.Kernel.Interfaces
{
    public interface IZipUtils
    {
        void zipFile(string strFileToZip, string strTargetZipFileName);
        void zipFolder(string strPathOfFolderToZip, string strTargetZipFileName);
        List<String> getListOfFilesInZip(String sZipFileToLoad);
        string unzipFile(string fileToUnzip);
        string unzipFile(string fileToUnzip, string targetFolder);
        List<string> unzipFileAndReturtListOfUnzipedFiles(string fileToUnzip);
        List<string> unzipFileAndReturtListOfUnzipedFiles(string fileToUnzip, string targetFolder);
    }
}