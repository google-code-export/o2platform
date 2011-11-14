// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.O2Findings;
using O2.DotNetWrappers.Windows;
using O2.Interfaces.O2Findings;

namespace O2.ImportExport.Misc.CodeCrawler
{
    public class O2AssesmentLoad_CodeCrawler :  IO2AssessmentLoad
    {
        public string engineName { get; set; }
        public O2AssesmentLoad_CodeCrawler()
        {
            engineName = "O2AssesmentLoad_CodeCrawler";
        }

        public bool canLoadFile(string fileToTryToLoad)
        {
            return Path.GetExtension(fileToTryToLoad) == ".owasp";
        }

        public IO2Assessment loadFile(string fileToLoad)
        {
            var codeCrawlerObject = Serialize.getDeSerializedObjectFromXmlFile(fileToLoad, typeof (Xsd.DocumentElement));
            if (codeCrawlerObject == null || false == codeCrawlerObject is Xsd.DocumentElement)
                return null;
            return createO2AssessmentFromCodeCrawlerObject((Xsd.DocumentElement)codeCrawlerObject, Path.GetFileNameWithoutExtension(fileToLoad));            
        }

        private IO2Assessment createO2AssessmentFromCodeCrawlerObject(Xsd.DocumentElement codeCrawlerObject, String fileName)
        {
            var o2Assessment = new O2Assessment();
            o2Assessment.name = "CodeCrawler Import of: " + fileName;
            foreach(var threat in codeCrawlerObject.ThreatList)
            {
                var o2Finding = new O2Finding
                                    {
                                        vulnName = threat.Threat,
                                        vulnType = threat.Threat,
                                        context = threat.Description,
                                        severity = threat.Level,
                                        confidence = 2,
                                        lineNumber = threat.Line,
                                        file = fileName
                                    };
                o2Finding.text.Add(threat.Description);
                o2Assessment.o2Findings.Add(o2Finding);
            }
            return o2Assessment;
        }

        public bool importFile(string fileToLoad, IO2Assessment o2Assessment)
        {
            var loadedO2Assessment = loadFile(fileToLoad);
            if (loadedO2Assessment!= null)
            {
                o2Assessment.o2Findings = loadedO2Assessment.o2Findings;
                return true;
            }
            return false;
        }

    }
}
