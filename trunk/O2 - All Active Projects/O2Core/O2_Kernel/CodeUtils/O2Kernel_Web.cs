﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using O2.Kernel.ExtensionMethods;

namespace O2.Kernel.CodeUtils
{
    public class O2Kernel_Web
    {

        //need these settings of the WebClient and GetResponseStream Http .NET clients methods will hang
        //for more references see:
        //  http://www.cnblogs.com/anders06/archive/2007/01/23/627698.html
        //  http://blogs.msdn.com/b/darrenj/archive/2005/03/07/386655.aspx
        //  http://stackoverflow.com/questions/1043234/strange-timeout-webexception-in-http-get-using-webclient 
        //  http://stackoverflow.com/questions/1485508/the-operation-has-timed-out-with-webclient-downloadfile-and-correct-urls 
        //   
        public static void ApplyNetworkConnectionHack()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 4096;
            System.Net.ServicePointManager.CheckCertificateRevocationList = true;
        }

        public bool httpFileExists(string url)
        {
            return httpFileExists(url, false);
        }

        public bool httpFileExists(string url, bool showError)
        {
            var webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webRequest.Method = "HEAD";
            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                return (webResponse.StatusCode == HttpStatusCode.OK && 
                        webResponse.ResponseUri.str() == url);
            }
            catch (Exception ex)
            {
                if (showError)
                    ex.log("in Web.httpFileExists");
                return false;
            }
        }

        public string downloadBinaryFile(string urlOfFileToFetch, string targetFileOrFolder)
        {
            var targetFile = targetFileOrFolder;
            if (Directory.Exists(targetFileOrFolder))
                targetFile = Path.Combine(targetFileOrFolder, Path.GetFileName(urlOfFileToFetch));

            PublicDI.log.debug("Downloading Binary File {0}", urlOfFileToFetch);
            lock (this)
            {
                using (WebClient webClient = new WebClient())
                {        
                    try
                    {                                                
                        byte[] pageData = webClient.DownloadData(urlOfFileToFetch);
                        O2Kernel_Files.WriteFileContent(targetFile, pageData);
                        PublicDI.log.debug("Downloaded File saved to: {0}", targetFile);

                        webClient.Dispose();

                        GC.Collect();       // because of WebClient().GetRequestStream prob
                        return targetFile;
                    }
                    catch (Exception ex)
                    {
                        PublicDI.log.ex(ex);
                    }
                }
            }
            GC.Collect();       // because of WebClient().GetRequestStream prob
            return null;
        }

    }
}
