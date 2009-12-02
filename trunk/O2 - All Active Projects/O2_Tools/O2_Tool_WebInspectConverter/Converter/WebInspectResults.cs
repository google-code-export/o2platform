using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.Zip;

namespace O2.Tool.WebInspectConverter.Converter
{
    public class WebInspectResults
    {
        public List<string> processedWebInspectScanFiles = new List<string>();
        public List<WebInspectFinding> webInspectFindings = new List<WebInspectFinding>();


        public void loadWebInspectScanFiles(string fileToProcess)
        {
            try
            {
                if (processedWebInspectScanFiles.Contains(fileToProcess))
                    return;
                string tempDirectory = Path.Combine(DI.config.O2TempDir,
                                                    Path.GetFileNameWithoutExtension(fileToProcess));
                //var tempFile = DI.config.TempFileNameInTempDirectory + ".zip";
                new zipUtils().unzipFile(fileToProcess, tempDirectory);
                var unzippedFilesToProcess = Files.getFilesFromDir_returnFullPath(tempDirectory);
                foreach (var unzippedFile in unzippedFilesToProcess)
                    loadWebInspectXmlFile(unzippedFile);

                processedWebInspectScanFiles.Add(fileToProcess);
            }
            catch (Exception ex)
            {
                DI.log.error("in loadWebInspectScanFiles: {0}", ex.Message);
            }
        }

        public void loadWebInspectXmlFile(string fileToProcess)
        {
            DI.log.info("Processing Web inspect Xml File {0}", fileToProcess);
            var webInspectResults = new XmlDocument();
            webInspectResults.Load(fileToProcess);
            // ReSharper disable PossibleNullReferenceException
            foreach (XmlElement sessionCheckFound in getSessionCheckFound(webInspectResults))
            {
                string sessionId = sessionCheckFound["VulnerableSessionID"].InnerText;
                foreach (XmlNode session in getSessionsWithSessionID(webInspectResults, sessionId))
                {
                    var webInspectFinding = new WebInspectFinding
                                                {
                                                    fullUrl = session["FullURL"].InnerText,
                                                    //filteredUrl = new FilteredUrl(session["FullURL"].InnerText),
                                                    payload = session["AttackDescriptor"].InnerText,
                                                    param = session["AttackParamDescriptor"].InnerText,
                                                    method = session["Method"].InnerText,
                                                    engineId = sessionCheckFound["EngineID"].InnerText,
                                                    sessionId = sessionId
                                                };
                    // hack to deal with ctl: in ParamName
                    webInspectFinding.param = webInspectFinding.param.Replace("%3A", ":");
                    if (webInspectFinding.param.IndexOf(':') > -1)
                        webInspectFinding.param = webInspectFinding.param.Split(new[] {':'})[1];
                    if (isFindingUnique(webInspectFinding))
                        webInspectFindings.Add(webInspectFinding);
                    //                 DI.log.info(webInspectFinding.method + "   -   " + webInspectFinding.param + "   :   " + webInspectFinding.fullUrl);
                }
            }
            // ReSharper restore PossibleNullReferenceException
            /*
               
           var sessionsCheckFoundWithEngineId = getSessionsCheckFoundWithEngineId(webInspectResults, sqlInjectionEngineId);
           foreach (XmlNode sessionCheckFound in sessionsCheckFoundWithEngineId)
           {
               // ReSharper disable PossibleNullReferenceException
               var sessionId = sessionCheckFound["VulnerableSessionID"].InnerText;

               var sessionsFoundWithSessionId = getSessionsWithSessionID(webInspectResults, sessionId);
               foreach (XmlNode session in sessionsFoundWithSessionId)
               {
                   var attackParam = session["AttackParamDescriptor"].InnerText;
                   // Hack to handle crl#: form parameter names in ASP.NET
                   if (attackParam.IndexOf(':') > -1)
                       attackParam = attackParam.Split(new char[] {':'})[1];
                   var attackPayload = session["AttackDescriptor"].InnerText;

                   var filteredUrl = new FilteredUrl(session["FullURL"].InnerText);
               }
           }
*/
        }

        public bool isFindingUnique(WebInspectFinding webInspectFinding)
        {
            foreach (WebInspectFinding currentFinding in webInspectFindings)
                if (currentFinding.ToString() == webInspectFinding.ToString())
                    return false;
            return true;
        }

        private static XmlNodeList getSessionCheckFound(XmlDocument webInspectResults)
        {
            return webInspectResults.GetElementsByTagName("SessionCheckFound");
        }

        private static List<XmlNode> getSessionsWithSessionID(XmlDocument webInspectResults, string sessionId)
        {
            return getXmlNodeListFromElementTagAndChildElementInnerText(webInspectResults, "Session", "SessionID",
                                                                        sessionId);
        }

        private static List<XmlNode> getSessionsCheckFoundWithEngineId(XmlDocument webInspectResults, string EngineId)
        {
            return getXmlNodeListFromElementTagAndChildElementInnerText(webInspectResults, "SessionCheckFound",
                                                                        "EngineID", EngineId);
        }

        private static List<XmlNode> getXmlNodeListFromElementTagAndChildElementInnerText(XmlDocument webInspectResults,
                                                                                          string elementName,
                                                                                          string subElementName,
                                                                                          string subElementValue)
        {
            var results = new List<XmlNode>();
            XmlNodeList sessions = webInspectResults.GetElementsByTagName(elementName);
            foreach (XmlNode session in sessions)
            {
                // ReSharper disable PossibleNullReferenceException
                if (session[subElementName].InnerText == subElementValue)
                    results.Add(session);
                // ReSharper restore PossibleNullReferenceException               
            }
            return results;
        }
    }
}