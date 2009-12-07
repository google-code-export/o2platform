using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;
using O2.Kernel;

namespace O2.DotNetWrappers.Network
{
    public class Web
    {

        public static string saveUrlContents(string urlToFetch)
        {   
            // first try to save the file using the original name
            var targetFile = Path.Combine(PublicDI.config.O2TempDir, Path.GetFileName(urlToFetch));
            if (File.Exists(targetFile)) // but give it a unique name if that file alredy exists
                targetFile = string.Format("{0}_{1}", PublicDI.config.TempFileNameInTempDirectory, Path.GetFileName(urlToFetch));
                //PublicDI.config.getTempFileInTempDirectory(Path.GetExtension(urlToFetch));
            return saveUrlContents(urlToFetch, targetFile);
        }
        public static string saveUrlContents(string urlToFetch, string targetFile)
        {
            var urlContents = getUrlContents(urlToFetch);
            if (urlContents != "")
            {
                if (Files.WriteFileContent(targetFile, urlContents))
                    return targetFile;
            }
            return "";

        }
        public static String getUrlContents(String urlToFetch)
        {
            return getUrlContents(urlToFetch, false);
        }

        public static String getUrlContents(String urlToFetch, bool verbose)
        {
            try
            {
                if (verbose)
                    DI.log.info("Fetching url: {0}", urlToFetch);
                WebRequest webRequest = WebRequest.Create(urlToFetch);
                WebResponse rResponse = webRequest.GetResponse();
                Stream sStream = rResponse.GetResponseStream();
                var srStreamReader = new StreamReader(sStream);
                string sHtml = srStreamReader.ReadToEnd();
                sStream.Close();
                srStreamReader.Close();
                rResponse.Close();
                return sHtml;
            }
            catch (Exception ex)
            {
                DI.log.error("Error in getUrlContents: {0}", ex.Message);
                return "";
            }
        }

        public static string downloadBinaryFile(string urlOfFileToFetch)
        {
            var webClient = new WebClient();
            try
            {
                string tempFileName = String.Format("{0}_{1}.zip", DI.config.TempFileNameInTempDirectory,
                                                    Path.GetFileNameWithoutExtension(urlOfFileToFetch));
                byte[] pageData = webClient.DownloadData(urlOfFileToFetch);
                Files.WriteFileContent(tempFileName, pageData);
                return tempFileName;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
            }
            return null;
        }

        public static List<String> downloadZipFileAndExtractFiles(string urlOfFileToFetch)
        {
            var webClient = new WebClient();
            try
            {
                string tempFileName = String.Format("{0}_{1}.zip", DI.config.TempFileNameInTempDirectory,
                                                    Path.GetFileNameWithoutExtension(urlOfFileToFetch));
                byte[] pageData = webClient.DownloadData(urlOfFileToFetch);
                Files.WriteFileContent(tempFileName, pageData);
                List<string> extractedFiles = new zipUtils().unzipFileAndReturtListOfUnzipedFiles(tempFileName);
                File.Delete(tempFileName);
                return extractedFiles;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
            }
            return null;
        }
      
    }
}
