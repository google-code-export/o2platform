using System.Collections.Generic;
using System.Xml;
using O2.DotNetWrappers.Filters;
using O2.DotNetWrappers.O2Findings;
using O2.Kernel.Interfaces.O2Findings;

namespace O2.Tool.WebInspectConverter.Converter
{
    public class WebInspectConverter
    {
        private const string sqlInjectionEngineId = "9722923f-f8d3-49c2-90bd-7c0e15901c18";

        public static List<IO2Finding> loadWebInspectResultsAndReturnO2FindingsFor_SqlInjection_PoC1(
            string webInspectResultsFile)
        {
            var results = new List<IO2Finding>();
            var webInspectResults = new XmlDocument();
            webInspectResults.Load(webInspectResultsFile);
            List<XmlNode> sessionsCheckFoundWithEngineId = getSessionsCheckFoundWithEngineId(webInspectResults,
                                                                                             sqlInjectionEngineId);
            foreach (XmlNode sessionCheckFound in sessionsCheckFoundWithEngineId)
            {
                // ReSharper disable PossibleNullReferenceException
                string sessionId = sessionCheckFound["VulnerableSessionID"].InnerText;

                List<XmlNode> sessionsFoundWithSessionId = getSessionsWithSessionID(webInspectResults, sessionId);
                foreach (XmlNode session in sessionsFoundWithSessionId)
                {
                    string fullURL = session["FullURL"].InnerText;
                    string attackParamDescriptor = session["AttackParamDescriptor"].InnerText;
                    if (attackParamDescriptor.IndexOf(':') > -1)
                        attackParamDescriptor = attackParamDescriptor.Split(new[] {':'})[1];
                    string attackDescriptor = session["AttackDescriptor"].InnerText;
                    var o2Finding = new O2Finding
                                        {
                                            o2Traces = new List<IO2Trace> { new O2Trace("WebInspect -> Ounce Mapping")},
                                            context = attackDescriptor,
                                            vulnName = fullURL,
                                            vulnType = "WebInspect Vulnerability"
                                        };
                    var source = new O2Trace(fullURL, TraceType.Source);
                    source.childTraces.Add(new O2Trace(attackDescriptor));

                    var Sink = new O2Trace(attackParamDescriptor)
                                   {
                                       traceType = TraceType.Known_Sink,
                                   };

                    source.childTraces.Add(Sink);

                    o2Finding.o2Traces[0].childTraces.Add(source);

                    results.Add(o2Finding);
                }
                // ReSharper restore PossibleNullReferenceException
            }
            return results;
        }


        public static List<IO2Finding> loadWebInspectResultsAndReturnO2FindingsFor_SqlInjection_PoC2(
            string webInspectResultsFile)
        {
            var results = new List<IO2Finding>();
            var webInspectResults = new XmlDocument();
            webInspectResults.Load(webInspectResultsFile);
            List<XmlNode> sessionsCheckFoundWithEngineId = getSessionsCheckFoundWithEngineId(webInspectResults,
                                                                                             sqlInjectionEngineId);
            foreach (XmlNode sessionCheckFound in sessionsCheckFoundWithEngineId)
            {
                // ReSharper disable PossibleNullReferenceException
                string sessionId = sessionCheckFound["VulnerableSessionID"].InnerText;

                List<XmlNode> sessionsFoundWithSessionId = getSessionsWithSessionID(webInspectResults, sessionId);
                foreach (XmlNode session in sessionsFoundWithSessionId)
                {
                    string attackParam = session["AttackParamDescriptor"].InnerText;
                    // Hack to handle crl#: form parameter names in ASP.NET
                    if (attackParam.IndexOf(':') > -1)
                        attackParam = attackParam.Split(new[] {':'})[1];
                    string attackPayload = session["AttackDescriptor"].InnerText;

                    var filteredUrl = new FilteredUrl(session["FullURL"].InnerText);
                    foreach (var word in filteredUrl.words)
                    {
                        var sink = new O2Trace("WebInspect:   " + filteredUrl.pathAndPageAndParameters,
                                               TraceType.Known_Sink)
                                       {
                                           context = attackPayload,
                                           method = attackParam
                                       };
                        //var sink = new O2Trace("WebInspect:   " + attackParam, TraceType.Known_Sink);
                        //source.childTraces.Add(sink);
                        var o2Trace = new O2Trace("WebInspect -> Ounce Mapping (Sql Injection)");
                        //o2Trace.childTraces.Add(source);
                        o2Trace.childTraces.Add(sink);
                        //source.context = "This is the context of the Source";
                        //sink.context = attackPayload;
                        var o2Finding = new O2Finding
                                            {
                                                o2Traces = new List<IO2Trace> { o2Trace},
                                                context = attackPayload,
                                                vulnName = word + "_" + attackParam,
                                                vulnType = "Sql Injection (from WebInspect)"
                                            };
                        results.Add(o2Finding);
                    }


/*                   
                   
                   
                   
                   var o2Finding = new O2Finding
                   {
                       o2Trace = new O2Trace("WebInspect -> Ounce Mapping"),
                       context = attackDescriptor,
                       vulnName = fullURL,
                       vulnType = "WebInspect Vulnerability"
                   };
                   var source = new O2Trace(fullURL, TraceType.Source);
                   source.childTraces.Add(new O2Trace(attackDescriptor));

                   var Sink = new O2Trace(attackParamDescriptor)
                   {
                       traceType = TraceType.Known_Sink
                   };

                   source.childTraces.Add(Sink);

                   o2Finding.o2Trace.childTraces.Add(source);

                   results.Add(o2Finding);*/
                }
                // ReSharper restore PossibleNullReferenceException
            }
            return results;
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