// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.ImportExport.Misc.Xsd;
using O2.Interfaces.O2Findings;

namespace O2.ImportExport.Misc.WebScarab
{
    public class O2AssesmentLoad_WebScarab :  IO2AssessmentLoad
    {
        public string engineName { get; set; }
        public O2AssesmentLoad_WebScarab()
        {
            engineName = "O2AssesmentLoad_WebScarab";
        }

        // rewrite in a more optimized way
        public bool canLoadFile(string fileToTryToLoad)
        {
            if (Path.GetFileName(fileToTryToLoad) == "conversationlog")
                return true;
            //if (Path.GetExtension(fileToTryToLoad) == ".xml")
            //    return Serialize.getDeSerializedObjectFromXmlFile(fileToTryToLoad, typeof(Xsd.BugCollection)) != null;
            return false;
        }

        public bool importFile(string fileToLoad, IO2Assessment o2Assessment)
        {
            var loadedO2Assessment = loadFile(fileToLoad);
            if (loadedO2Assessment != null)
            {
                o2Assessment.o2Findings = loadedO2Assessment.o2Findings;
                return true;
            }
            return false;
        }

        public IO2Assessment loadFile(string fileToLoad)
        {
            return createO2AssessmentFromWebScarabFile(fileToLoad);            
        }

        public static IO2Assessment createO2AssessmentFromWebScarabFile(string conversationFile)
        {
            var o2Assessment = new O2Assessment();
            try
            {
                o2Assessment.name = "Webscarab Import of: " + conversationFile;
                var webScarabConversations = new List<IWebscarabConversation>();
                if (false == File.Exists(conversationFile))
                    DI.log.error("Could not find webscarab conversation file: {0}", conversationFile);
                else
                {                                        
                    var fileLines = Files.getFileLines(conversationFile);
                    var requestAndResponseFiles = Path.Combine(Path.GetDirectoryName(conversationFile), "conversations");
                    DI.log.info("There are {0} lines in the loaded file: {1}", fileLines.Count, conversationFile);
                    IWebscarabConversation currentConversation = null;
                    foreach (var line in fileLines)
                    {
                        var parsedLine = getParsedLine(line);
                        if (parsedLine.Key != null)
                        {
                            switch (parsedLine.Key.ToString())
                            {
                                case "### Conversation ":
                                    if (currentConversation != null)
                                        webScarabConversations.Add(currentConversation);
                                    currentConversation = new WebscarabConversation();
                                    currentConversation.id = parsedLine.Value.ToString();
                                    //log.info("{0}   =  :  = {1} ", parsedLine.Key , parsedLine.Value);
                                    break;
                                case "RESPONSE_SIZE":
                                    currentConversation.RESPONSE_SIZE = parsedLine.Value.ToString();
                                    break;
                                case "WHEN":
                                    currentConversation.WHEN = parsedLine.Value.ToString();
                                    break;
                                case "METHOD":
                                    currentConversation.METHOD = parsedLine.Value.ToString();
                                    break;
                                case "COOKIE":
                                    currentConversation.COOKIE = parsedLine.Value.ToString();
                                    break;
                                case "STATUS":
                                    currentConversation.STATUS = parsedLine.Value.ToString();
                                    break;
                                case "URL":
                                    currentConversation.URL = parsedLine.Value.ToString();
                                    break;
                                case "TAG":
                                    currentConversation.TAG = parsedLine.Value.ToString();
                                    break;
                                case "ORIGIN":
                                    currentConversation.ORIGIN = parsedLine.Value.ToString();
                                    break;
                                case "XSS-GET":
                                    currentConversation.XSS_GET.Add(parsedLine.Value.ToString());
                                    break;
                                case "CRLF-GET":
                                    currentConversation.CRLF_GET.Add(parsedLine.Value.ToString());
                                    break;
                                case "SET-COOKIE":
                                    currentConversation.SET_COOKIE.Add(parsedLine.Value.ToString());
                                    break;
                                case "XSS-POST":
                                    currentConversation.XSS_POST.Add(parsedLine.Value.ToString());
                                    break;
                                default:
                                    DI.log.error("Key value not handled: {0} for {1}", parsedLine.Key.ToString(),
                                                 parsedLine.Value.ToString());
                                    break;
                            }
                        }


                        if (currentConversation != null)
                        {
                            currentConversation.request = String.Format("{0}\\{1}-request", requestAndResponseFiles, currentConversation.id);
                            currentConversation.response = String.Format("{0}\\{1}-response", requestAndResponseFiles, currentConversation.id);

                        }
                    }
                    
                }
                var o2Findings = createFindingsFromConversation(webScarabConversations);
                o2Assessment.o2Findings = o2Findings;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in createO2AssessmentFromWebScarabFile");
            }
            return o2Assessment;
        }

        public static List<IO2Finding> createFindingsFromConversation(List<IWebscarabConversation> webScarabConversations)
        {
            var o2Findings = new List<IO2Finding>();
            foreach (var conversation in webScarabConversations)
            {
                var o2Finding = new O2Finding();
                if (conversation.TAG != null && conversation.TAG != "")
                    o2Finding.vulnType = conversation.TAG;
                else
                    o2Finding.vulnType = "Tag not defined";
                o2Finding.vulnName = conversation.METHOD + ": " + conversation.URL;

                addTrace(o2Finding, conversation.COOKIE, "COOKIE");
                addTrace(o2Finding, conversation.STATUS, "STATUS");
                addTrace(o2Finding, conversation.ORIGIN, "ORIGIN");
                addTrace(o2Finding, conversation.URL, "URL");                
                addTrace(o2Finding, conversation.XSS_GET, "XSS_GET");
                addTrace(o2Finding, conversation.CRLF_GET, "CRLF_GET");
                addTrace(o2Finding, conversation.SET_COOKIE, "SET_COOKIE");
                addTrace(o2Finding, conversation.XSS_POST, "XSS_POST");   
                
                // add request and response
                var requestTrace = new O2Trace("request: " + conversation.request) {file = conversation.request};
                // requestTrace.context = Files.getFileContents(requestTrace.file);

                var responseTrace = new O2Trace("response: " + conversation.response) {file = conversation.response};
                // responseTrace.context = Files.getFileContents(responseTrace.file);

                o2Finding.o2Traces.Add(requestTrace);
                o2Finding.o2Traces.Add(responseTrace);
                o2Findings.Add(o2Finding);
                
            }
            return o2Findings;
        }

        public static void addTrace(IO2Finding o2Finding, string value, string key)
        {
            if (value != null)
                o2Finding.o2Traces.Add(new O2Trace(key + " = " + value));
        }

        public static void addTrace(IO2Finding o2Finding, List<string> values, string key)
        {
            foreach (var value in values)
                o2Finding.o2Traces.Add(new O2Trace(key + " = " + value));
        }
        public static DictionaryEntry getParsedLine(string line)
        {
            var indexOfFirstColon = line.IndexOf(':');
            if (indexOfFirstColon > -1)
            {
                var key = line.Substring(0, indexOfFirstColon);
                var value = line.Substring(indexOfFirstColon + 2);
                return new DictionaryEntry(key, value);
            }
            return new DictionaryEntry(null, null);
        }


        private string tryToResolveFullFilePath(string partialFilePath, BugCollection findBugsObject)
        {
            if (partialFilePath != null && findBugsObject.Project != null && findBugsObject.Project.SrcDir != null)
                foreach (var srcDir in findBugsObject.Project.SrcDir)
                {
                    var filePath = Path.Combine(srcDir, partialFilePath);
                    if (File.Exists(filePath))
                        return filePath;
                }
            return "";
        }
        

    }
}
