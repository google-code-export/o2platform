// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.Network;
using O2.DotNetWrappers.Windows;
//using O2.Views.ASCX.classes.MainGUI;
//using O2.Views.ASCX.ExtensionMethods;
//O2File:WebscarabConversation.cs

namespace O2.XRules.Database.APIs
{
	public class API_WebScarab
    {    
    
    	public List<IWebscarabConversation> loadConversationsFile(string conversationFile)    	
    	{
    		var webScarabConversations = new List<IWebscarabConversation>();
            if (!File.Exists(conversationFile))
            {
                "Could not find webscarab conversation file: {0}".error(conversationFile);
            }
            else
            {
                List<string> fileLines = Files.getFileLines(conversationFile);
                string requestAndResponseFiles = Path.Combine(Path.GetDirectoryName(conversationFile), "conversations");
                "There are {0} lines in the loaded file: {1}".info(fileLines.Count, conversationFile );
                IWebscarabConversation currentConversation = null;
                foreach (string line in fileLines)
                {
                    DictionaryEntry parsedLine = getParsedLine(line);
                    if (parsedLine.Key != null)
                    {
                        switch (parsedLine.Key.ToString())
                        {
                            case "### Conversation ":
                                if (currentConversation != null)
                                {
                                    webScarabConversations.Add(currentConversation);
                                }
                                currentConversation = new WebscarabConversation();
                                currentConversation.id = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "RESPONSE_SIZE":
                                currentConversation.RESPONSE_SIZE = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "WHEN":
                                currentConversation.WHEN = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "METHOD":
                                currentConversation.METHOD = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "COOKIE":
                                currentConversation.COOKIE = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "STATUS":
                                currentConversation.STATUS = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "URL":
                                currentConversation.URL = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "TAG":
                                currentConversation.TAG = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "ORIGIN":
                                currentConversation.ORIGIN = parsedLine.Value.ToString();
                                goto Label_039B;

                            case "XSS-GET":
                                currentConversation.XSS_GET.Add(parsedLine.Value.ToString());
                                goto Label_039B;

                            case "CRLF-GET":
                                currentConversation.CRLF_GET.Add(parsedLine.Value.ToString());
                                goto Label_039B;

                            case "SET-COOKIE":
                                currentConversation.SET_COOKIE.Add(parsedLine.Value.ToString());
                                goto Label_039B;

                            case "XSS-POST":
                                currentConversation.XSS_POST.Add(parsedLine.Value.ToString());
                                goto Label_039B;
                        }
                        "Key value not handled: {0} for {1}".error(parsedLine.Key.ToString(), parsedLine.Value.ToString());
                    }
                Label_039B:
                    if (currentConversation != null)
                    {
                        currentConversation.request = string.Format(@"{0}\{1}-request", requestAndResponseFiles, currentConversation.id);
                        currentConversation.response = string.Format(@"{0}\{1}-response", requestAndResponseFiles, currentConversation.id);
                    }
                }
            }
            return webScarabConversations;
    	}
    	
    	public static DictionaryEntry getParsedLine(string line)
        {
            int indexOfFirstColon = line.IndexOf(':');
            if (indexOfFirstColon > -1)
            {
                string key = line.Substring(0, indexOfFirstColon);
                return new DictionaryEntry(key, line.Substring(indexOfFirstColon + 2));
            }
            return new DictionaryEntry(null, null);
        }
    }
    
    
}
