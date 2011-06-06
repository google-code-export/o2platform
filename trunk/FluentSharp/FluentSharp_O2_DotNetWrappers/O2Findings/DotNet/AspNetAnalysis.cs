// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using FluentSharp.O2.DotNetWrappers.DotNet;
using FluentSharp.O2.Interfaces.O2Findings;

namespace FluentSharp.O2.DotNetWrappers.O2Findings.DotNet
{
    public class AspNetAnalysis
    {
        public static List<String> getAspNetPageFromO2Findings(List<IO2Finding> o2Findings)
        {
            DI.log.info("in getAspNetPageFromO2Findings");
            //var stringToMatch = ".*aspx";
            var findings = new List<String>();

            foreach (IO2Finding o2Finding in o2Findings)
            {
                if (o2Finding.callerName.IndexOf("aspx") > -1)
                {
                    if (false == findings.Contains(o2Finding.callerName))
                        findings.Add(o2Finding.callerName);
                }
            }

            DI.log.info("Found {0} unique calls with aspx", findings.Count);
            foreach (string finding in findings)
                DI.log.info("   {0}", finding);

            return findings;
        }

        public static List<IO2Finding> findWebControlSources(List<IO2Finding> o2Findings)
        {
            var methodsToFind = new RegEx("System.Web.UI.WebControls.*get_Text");
            //var methodsToFind = new RegEx("HttpRequest");
            var results = new List<IO2Finding>();
            foreach (IO2Finding o2Finding in o2Findings)
            {
                IO2Trace source = ((O2Finding)o2Finding).getSource();
                if (source != null && methodsToFind.find(source.ToString()))
                    // && o2Finding.getSource.ToString() != "")            
                {
                    if (source.context.Contains("txt"))
                    {
                        // DI.log.info(source + " -> " + (o2Finding.getSink != null ? o2Finding.getSink.ToString() : ""));
                        string variableName = OzasmtContext.getVariableNameFromThisObject(source);
                        // DI.log.info(o2Finding.o2Trace + "  :::  " + );// + "    :    " + source.context);
                        foreach (IO2Trace o2Trace in o2Finding.o2Traces)
                        {
                            List<string> wordsFromSignature =
                                OzasmtUtils.getListWithWordsFromSignature(o2Trace.signature);
                            foreach (string word in wordsFromSignature)
                            {
                                //           var sourceO2Trace = new O2Trace("OunceLabs:  " + word);
                                //           var sinkO2Trace = new O2Trace("OunceLabs:   " + variableName);
                                //           sinkO2Trace.childTraces.Add(o2Finding.o2Trace);
                                //           sourceO2Trace.childTraces.Add(sinkO2Trace);

                                var newO2Finding = new O2Finding
                                                       {
                                                           vulnType = "ASP.NET Attack Surface",
                                                           vulnName = word + "_" + variableName,
                                                           //o2Trace = sourceO2Trace
                                                           o2Traces = o2Finding.o2Traces
                                                       };
                                results.Add(newO2Finding);
                            }
                        }
                    }
                    // DI.log.info("    " + o2Finding.getSource + " -> " + o2Finding.getSource.context + "\n\n");
                }
            }
            return results;
        }

        /*public static List<O2Finding> findMethodCalled(String sDllToLoad)
        {
            return CecilUtils.findInAssemblyVariable(sDllToLoad);
        }*/
    }
}
