using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using O2.DotNetWrappers.ExtensionMethods;
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
                    PublicDI.log.info("Fetching url: {0}", urlToFetch);
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
                PublicDI.log.error("Error in getUrlContents: {0}", ex.Message);
                return "";
            }
        }
        
        public static String getUrlContents_POST(String urlToFetch, string postData)
        {
            return getUrlContents_POST(urlToFetch, Encoding.ASCII.GetBytes(postData));
        }

        public static String getUrlContents_POST(String urlToFetch, byte[] postData)
        {        
            try
            {   
                
                WebRequest webRequest = WebRequest.Create(urlToFetch);                
                // setup POST details:

                webRequest.Method = "POST";
                webRequest.ContentLength = postData.Length;
                webRequest.ContentType = "application/x-www-form-urlencoded";
                Stream dataStream = webRequest.GetRequestStream();
                dataStream.Write(postData, 0, postData.Length);
                dataStream.Close();

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
                PublicDI.log.error("Error in getUrlContents: {0}", ex.Message);
                return "";
            }
        }                 

		public static string downloadBinaryFile(string urlOfFileToFetch)
		{
			return downloadBinaryFile(urlOfFileToFetch, true);
		}
				
		
        public static string downloadBinaryFile(string urlOfFileToFetch, bool saveUsingTempFileName)
        {
        	string targetFile = String.Format("{0}.{1}", 
                									(saveUsingTempFileName) ? PublicDI.config.TempFileNameInTempDirectory + "_" : PublicDI.config.O2TempDir,
                                                    Path.GetFileName(urlOfFileToFetch));
            return downloadBinaryFile(urlOfFileToFetch,targetFile);
        }
        
        public static string downloadBinaryFile(string urlOfFileToFetch, string targetFileOrFolder)
        {
        	var targetFile = targetFileOrFolder;
        	if (Directory.Exists(targetFileOrFolder))
        		targetFile = Path.Combine(targetFileOrFolder, Path.GetFileName(urlOfFileToFetch));
        		
        	PublicDI.log.debug("Downloading Binary File {0}", urlOfFileToFetch);
            var webClient = new WebClient();
            try
            {                
                byte[] pageData = webClient.DownloadData(urlOfFileToFetch);
                Files.WriteFileContent(targetFile, pageData);
                PublicDI.log.debug("Downloaded File saved to: {0}", targetFile);
                return targetFile;
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex);
            }
            return null;
        }

        public static List<String> downloadZipFileAndExtractFiles(string urlOfFileToFetch)
        {
            var webClient = new WebClient();
            try
            {
                string tempFileName = String.Format("{0}_{1}.zip", PublicDI.config.TempFileNameInTempDirectory,
                                                    Path.GetFileNameWithoutExtension(urlOfFileToFetch));
                byte[] pageData = webClient.DownloadData(urlOfFileToFetch);
                Files.WriteFileContent(tempFileName, pageData);
                List<string> extractedFiles = new zipUtils().unzipFileAndReturtListOfUnzipedFiles(tempFileName);
                File.Delete(tempFileName);
                return extractedFiles;
            }
            catch (Exception ex)
            {
                PublicDI.log.ex(ex);
            }
            return null;
        }
        
        public static string checkIfFileExistsAndDownloadIfNot(string urlToDownloadFile)
        {
            return checkIfFileExistsAndDownloadIfNot(urlToDownloadFile.fileName(), urlToDownloadFile);
        }

        public static string checkIfFileExistsAndDownloadIfNot(string file , string urlToDownloadFile)
        {
        	if (File.Exists(file))
        		return file;
        	var localTempFile = Path.Combine(PublicDI.config.O2TempDir, file);
        	if (File.Exists(localTempFile))
        		return localTempFile;
            downloadBinaryFile(urlToDownloadFile, localTempFile);
        	//var downloadedFile = downloadBinaryFile(urlToDownloadFile, false /*saveUsingTempFileName*/);
            if (File.Exists(localTempFile))
        	{
                if (Path.GetExtension(localTempFile) != ".zip")
                    return localTempFile;

                List<string> extractedFiles = new zipUtils().unzipFileAndReturtListOfUnzipedFiles(localTempFile, PublicDI.config.O2TempDir);
        		if (extractedFiles != null)
        			foreach(var extractedFile in  extractedFiles)
        				if (Path.GetFileName(extractedFile) == file)
        					return extractedFile;        					        		        		        		
        	}
        	return "";
        }
      
    }
}
